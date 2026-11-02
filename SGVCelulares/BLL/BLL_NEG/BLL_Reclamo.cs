using BE;
using BLL.BLL_SER;
using MAP.MAP_NEG;
using Servicio;

namespace BLL.BLL_NEG
{
    /// <summary>
    /// Lógica de los reclamos por garantía de PN-02.
    /// CU-NEG-7 Registrar reclamo de garantía: ConsultarReparacionesConGarantia y RegistrarReclamo.
    /// CU-NEG-8 Evaluar reclamo: ObtenerReclamoAccesible, ConsultarReparacionOriginal y EvaluarReclamo.
    /// CU-NEG-9 Reparar equipo por garantía: ObtenerReclamoParaReparar y RepararReclamo.
    /// </summary>
    public class BLL_Reclamo
    {
        private const string Modulo = "Reclamos";

        MAP_Reclamo map_reclamo;
        MAP_OT map_ot;
        MAP_Reparacion map_reparacion;
        BLL_Cliente bll_cliente;
        BLL_Tecnico bll_tecnico;
        BLL_Repuesto bll_repuesto;
        BLL_Bitacora bll_bitacora;
        BLL_DV bll_dv;

        public BLL_Reclamo()
        {
            map_reclamo = new MAP_Reclamo();
            map_ot = new MAP_OT();
            map_reparacion = new MAP_Reparacion();
            bll_cliente = new BLL_Cliente();
            bll_tecnico = new BLL_Tecnico();
            bll_repuesto = new BLL_Repuesto();
            bll_bitacora = new BLL_Bitacora();
            bll_dv = new BLL_DV();
        }

        /// <summary>Todos los reclamos con su OT original, cliente, equipo y técnico, del más nuevo al más viejo.</summary>
        public List<BE_ReclamoResumen> ConsultarResumen() => map_reclamo.ConsultarResumen();

        /// <summary>True si el usuario logueado tiene un rol de técnico (ve solo sus reclamos).</summary>
        public bool UsuarioEsTecnico() => bll_tecnico.SesionEsTecnico();

        /// <summary>
        /// Reclamos que ve el usuario logueado: un técnico ve solo los asignados a él;
        /// un usuario que no es técnico (vendedor, administrador) los ve todos.
        /// </summary>
        public List<BE_ReclamoResumen> ConsultarResumenSegunUsuario()
        {
            List<BE_ReclamoResumen> reclamos = ConsultarResumen();
            if (!bll_tecnico.SesionEsTecnico()) return reclamos;

            BE_Tecnico tecnico = bll_tecnico.ConsultarTecnicoDeSesion()
                ?? throw new Exception("Su usuario no está registrado como técnico activo.");
            return reclamos.Where(r => r.DniTecnico == tecnico.Dni).ToList();
        }

        /// <summary>
        /// CU-NEG-8: el reclamo, si existe y el usuario puede trabajar sobre él. Un técnico solo accede
        /// a los reclamos asignados a él; un usuario que no es técnico (ej. el administrador) accede a cualquiera.
        /// </summary>
        public BE_ReclamoResumen ObtenerReclamoAccesible(int nroReclamo)
        {
            BE_ReclamoResumen reclamo = map_reclamo.ConsultarResumenPorNro(nroReclamo)
                ?? throw new Exception($"No existe el reclamo N° {nroReclamo}.");

            if (bll_tecnico.SesionEsTecnico())
            {
                BE_Tecnico tecnicoSesion = bll_tecnico.ConsultarTecnicoDeSesion()
                    ?? throw new Exception("Su usuario no está registrado como técnico activo.");
                if (reclamo.DniTecnico != tecnicoSesion.Dni)
                    throw new Exception($"El reclamo N° {nroReclamo} está asignado a otro técnico.");
            }

            return reclamo;
        }

        /// <summary>CU-NEG-8: falla, diagnóstico y repuestos utilizados de la OT original del reclamo.</summary>
        public BE_ReparacionOriginal ConsultarReparacionOriginal(int nroReclamo)
        {
            BE_ReclamoResumen reclamo = ObtenerReclamoAccesible(nroReclamo);
            BE_OTResumen ot = map_ot.ConsultarResumenPorNro(reclamo.NroOT)
                ?? throw new Exception($"No existe la OT N° {reclamo.NroOT}.");

            return new BE_ReparacionOriginal
            {
                NroOT = ot.NroOT,
                DescripcionFalla = ot.DescripcionFalla,
                DetalleDiagnostico = map_reclamo.ConsultarDetalleDiagnostico(ot.NroOT) ?? "",
                Repuestos = map_reparacion.ConsultarLineasPorOT(ot.NroOT).Where(l => l.CantidadUtilizada > 0).ToList()
            };
        }

        /// <summary>
        /// CU-NEG-8 Evaluar reclamo. El reclamo tiene que estar "En evaluacion" y el usuario poder trabajar sobre él.
        /// Si la falla está cubierta pasa a "Aprobado" y si no, a "Rechazado" (conserva el técnico).
        /// El cambio de estado es condicional: si otro usuario ya lo modificó, no se guarda.
        /// No toca stock ni repuestos.
        /// </summary>
        public void EvaluarReclamo(int nroReclamo, bool? fallaCubierta, string detalle)
        {
            detalle = detalle?.Trim() ?? "";

            BE_ReclamoResumen reclamo = ObtenerReclamoAccesible(nroReclamo);
            if (reclamo.Estado != EstadosReclamo.EnEvaluacion)
                throw new Exception($"El reclamo N° {nroReclamo} está en estado \"{reclamo.Estado}\" y no se puede evaluar.");

            if (string.IsNullOrWhiteSpace(detalle))
                throw new Exception("El detalle de la evaluación es obligatorio.");
            if (detalle.Length > BE_Reclamo.LargoMaximoDetalleEvaluacion)
                throw new Exception($"El detalle de la evaluación no puede superar los {BE_Reclamo.LargoMaximoDetalleEvaluacion} caracteres.");
            if (fallaCubierta == null)
                throw new Exception("Debe indicar si la falla está cubierta por la garantía.");

            string estadoNuevo = fallaCubierta.Value ? EstadosReclamo.Aprobado : EstadosReclamo.Rechazado;
            if (!map_reclamo.RegistrarEvaluacion(nroReclamo, fallaCubierta.Value, detalle, EstadosReclamo.EnEvaluacion, estadoNuevo))
                throw new Exception($"El reclamo N° {nroReclamo} ya no está en evaluación. Actualice el listado.");

            bll_dv.ActualizarDVNegocio(BLL_DV.TablaReclamo, nroReclamo);
            bll_bitacora.RegistrarBitacora(new SER_Bitacora(
                SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, Modulo, "Evaluar reclamo", 2));
        }

        /// <summary>
        /// CU-NEG-9: reclamo a reparar (accesible para el usuario y "Aprobado" o "En espera de repuesto").
        /// </summary>
        public BE_ReclamoResumen ObtenerReclamoParaReparar(int nroReclamo)
        {
            BE_ReclamoResumen reclamo = ObtenerReclamoAccesible(nroReclamo);
            if (reclamo.Estado != EstadosReclamo.Aprobado && reclamo.Estado != EstadosReclamo.EnEsperaRepuesto)
                throw new Exception($"El reclamo N° {nroReclamo} está en estado \"{reclamo.Estado}\": solo se reparan reclamos aprobados o en espera de repuesto.");
            return reclamo;
        }

        /// <summary>
        /// CU-NEG-9 Reparar equipo por garantía (sin cargo). Valida los repuestos (activos, cantidad mayor a 0, sin repetir)
        /// y el stock actual de cada uno.
        /// Con stock: en una transacción guarda los repuestos, los descuenta del inventario y pasa el reclamo a "Reparado".
        /// Sin stock: no descuenta nada y el reclamo queda "En espera de repuesto" hasta que se vuelva a ejecutar el caso de uso.
        /// Se puede reparar sin repuestos (solo mano de obra).
        /// </summary>
        /// <param name="repuestos">Código del repuesto → cantidad utilizada.</param>
        /// <returns>Los repuestos sin stock suficiente; vacía si el reclamo quedó "Reparado".</returns>
        public List<BE_FaltanteStock> RepararReclamo(int nroReclamo, Dictionary<string, int> repuestos)
        {
            BE_ReclamoResumen reclamo = ObtenerReclamoParaReparar(nroReclamo);
            repuestos ??= new Dictionary<string, int>();

            var faltantes = new List<BE_FaltanteStock>();
            foreach (var (codigo, cantidad) in repuestos)
            {
                BE_Repuesto repuesto = bll_repuesto.ConsultarPorCodigo(codigo)
                    ?? throw new Exception($"El repuesto {codigo} no existe o está dado de baja.");
                if (cantidad <= 0)
                    throw new Exception($"La cantidad del repuesto {repuesto.Codigo} debe ser mayor a 0.");
                if (repuesto.StockDisponible < cantidad)
                    faltantes.Add(new BE_FaltanteStock(repuesto.Codigo, repuesto.Descripcion, cantidad, repuesto.StockDisponible));
            }

            if (faltantes.Count > 0)
            {
                // Sin stock no se descuenta nada: el reclamo queda esperando (si ya estaba esperando, no hay nada que cambiar)
                if (reclamo.Estado != EstadosReclamo.EnEsperaRepuesto)
                {
                    if (!map_reclamo.CambiarEstado(nroReclamo, reclamo.Estado, EstadosReclamo.EnEsperaRepuesto))
                        throw new Exception($"El reclamo N° {nroReclamo} cambió de estado. Actualice el listado.");
                    bll_dv.ActualizarDVNegocio(BLL_DV.TablaReclamo, nroReclamo);
                    RegistrarBitacora("Reclamo en espera de repuesto");
                }
                return faltantes;
            }

            try
            {
                map_reclamo.RegistrarReparacion(nroReclamo, repuestos, reclamo.Estado, EstadosReclamo.Reparado);
            }
            catch (InvalidOperationException ex)
            {
                // Otro usuario cambió el estado o consumió el stock entre la validación y la escritura: no se guardó nada
                throw new Exception(ex.Message + " Vuelva a intentar.");
            }

            bll_dv.ActualizarDVNegocio(BLL_DV.TablaReclamo, nroReclamo);
            foreach (string codigo in repuestos.Keys)
            {
                bll_dv.ActualizarDVNegocio(BLL_DV.TablaDetalleRepuestoReclamo, nroReclamo, codigo);
                bll_dv.ActualizarDVNegocio(BLL_DV.TablaRepuesto, codigo);
            }
            RegistrarBitacora("Reparar reclamo");

            return faltantes;
        }

        private void RegistrarBitacora(string evento) =>
            bll_bitacora.RegistrarBitacora(new SER_Bitacora(
                SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, Modulo, evento, 2));

        /// <summary>
        /// CU-NEG-7 paso 1: OT entregadas con garantía del cliente (vigente o vencida: la vigencia se valida al registrar).
        /// El cliente tiene que estar registrado y activo. Una lista vacía significa que no hay una reparación cubierta.
        /// </summary>
        public List<BE_ReparacionConGarantia> ConsultarReparacionesConGarantia(string dni)
        {
            BE_Cliente cliente = ObtenerCliente(dni);
            return map_reclamo.ConsultarReparacionesConGarantia(cliente.Dni, EstadosOT.Entregado);
        }

        /// <summary>
        /// CU-NEG-7 Registrar reclamo de garantía.
        /// Valida todo antes de escribir en la base, así una validación que falla no deja datos a medias:
        ///   1. El cliente tiene que estar registrado y la OT tiene que ser suya, estar "Entregado" y tener garantía.
        ///      El equipo no puede tener un reclamo abierto ni estar en el taller con una OT sin entregar.
        ///   2. Motivo y descripción obligatorios.
        ///   3. Si la garantía está vencida a la fecha del reclamo, el reclamo queda "Rechazado" (sin técnico);
        ///      si está vigente, queda "En evaluacion" asignado al técnico disponible con menos OT en curso
        ///      (si no hay técnico disponible, no se registra).
        /// Después registra el reclamo ("Pendiente" y luego el estado que resultó, en una sola transacción)
        /// y actualiza DV y bitácora.
        /// </summary>
        /// <returns>El reclamo registrado, con los datos para mostrar la confirmación.</returns>
        public BE_ReclamoResumen RegistrarReclamo(string dni, int nroOT, string motivo, string descripcion)
        {
            motivo = motivo?.Trim() ?? "";
            descripcion = descripcion?.Trim() ?? "";

            // Paso 1: cliente y OT original entregada con garantía
            BE_Cliente cliente = ObtenerCliente(dni);

            BE_ReparacionConGarantia reparacion = map_reclamo.ConsultarReparacionConGarantia(nroOT, EstadosOT.Entregado)
                ?? throw new Exception($"La OT N° {nroOT} no es una reparación entregada con garantía.");

            if (reparacion.DniCliente != cliente.Dni)
                throw new Exception($"La OT N° {nroOT} no pertenece al cliente con DNI {cliente.Dni}.");

            int reclamoAbierto = map_reclamo.ObtenerReclamoDelEquipo(reparacion.IdEquipo, EstadosReclamo.Abiertos);
            if (reclamoAbierto > 0)
                throw new Exception($"El equipo ya tiene el reclamo N° {reclamoAbierto} abierto.");

            int otAbierta = map_ot.ObtenerOTDelEquipo(reparacion.IdEquipo, EstadosOT.Abiertas);
            if (otAbierta > 0)
                throw new Exception($"El equipo está en el taller con la OT N° {otAbierta} sin entregar. No se puede registrar el reclamo.");

            // Paso 2: datos del reclamo
            if (string.IsNullOrWhiteSpace(motivo))
                throw new Exception("El motivo del reclamo es obligatorio.");
            if (motivo.Length > BE_Reclamo.LargoMaximoMotivo)
                throw new Exception($"El motivo no puede superar los {BE_Reclamo.LargoMaximoMotivo} caracteres.");
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new Exception("La descripción del reclamo es obligatoria.");
            if (descripcion.Length > BE_Reclamo.LargoMaximoDescripcion)
                throw new Exception($"La descripción no puede superar los {BE_Reclamo.LargoMaximoDescripcion} caracteres.");

            // Paso 3 (se resuelve antes de escribir): vigencia de la garantía y técnico asignado
            DateTime hoy = DateTime.Today;
            var reclamo = new BE_Reclamo(nroOT, motivo, descripcion, hoy, EstadosReclamo.Pendiente);
            bool vigente = new BE_Garantia(nroOT, reparacion.FechaVencimiento).EstaVigente(hoy);
            if (vigente)
                reclamo.DniTecnico = bll_tecnico.AsignarTecnico().Dni;
            string estadoNuevo = vigente ? EstadosReclamo.EnEvaluacion : EstadosReclamo.Rechazado;

            reclamo.NroReclamo = map_reclamo.Registrar(reclamo, estadoNuevo);

            bll_dv.ActualizarDVNegocio(BLL_DV.TablaReclamo, reclamo.NroReclamo);
            bll_bitacora.RegistrarBitacora(new SER_Bitacora(
                SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, Modulo, "Registrar reclamo", 2));

            return map_reclamo.ConsultarResumenPorNro(reclamo.NroReclamo)
                ?? throw new Exception($"El reclamo N° {reclamo.NroReclamo} se registró pero no se pudo consultar.");
        }

        private BE_Cliente ObtenerCliente(string dni)
        {
            dni = dni?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(dni))
                throw new Exception("Debe ingresar el DNI del cliente.");

            return bll_cliente.ConsultarPorDni(dni)
                ?? throw new Exception($"El DNI {dni} no pertenece a un cliente registrado.");
        }
    }
}
