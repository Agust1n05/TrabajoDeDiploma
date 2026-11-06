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
    /// CU-NEG-10 Gestionar entrega de reclamo: ObtenerReclamoParaEntregar y EntregarReclamo.
    /// </summary>
    public class BLL_Reclamo_382QA
    {
        private const string Modulo = "Reclamos";

        MAP_Reclamo_382QA map_reclamo;
        MAP_OT_382QA map_ot;
        MAP_Reparacion_382QA map_reparacion;
        BLL_Cliente_382QA bll_cliente;
        BLL_Tecnico_382QA bll_tecnico;
        BLL_Repuesto_382QA bll_repuesto;
        BLL_Bitacora_382QA bll_bitacora;
        BLL_DV_382QA bll_dv;

        public BLL_Reclamo_382QA()
        {
            map_reclamo = new MAP_Reclamo_382QA();
            map_ot = new MAP_OT_382QA();
            map_reparacion = new MAP_Reparacion_382QA();
            bll_cliente = new BLL_Cliente_382QA();
            bll_tecnico = new BLL_Tecnico_382QA();
            bll_repuesto = new BLL_Repuesto_382QA();
            bll_bitacora = new BLL_Bitacora_382QA();
            bll_dv = new BLL_DV_382QA();
        }

        /// <summary>Todos los reclamos con su OT original, cliente, equipo y técnico, del más nuevo al más viejo.</summary>
        public List<BE_ReclamoResumen_382QA> ConsultarResumen_382QA() => map_reclamo.ConsultarResumen_382QA();

        /// <summary>True si el usuario logueado tiene un rol de técnico (ve solo sus reclamos).</summary>
        public bool UsuarioEsTecnico_382QA() => bll_tecnico.SesionEsTecnico_382QA();

        /// <summary>
        /// Reclamos que ve el usuario logueado: un técnico ve solo los asignados a él;
        /// un usuario que no es técnico (vendedor, administrador) los ve todos.
        /// </summary>
        public List<BE_ReclamoResumen_382QA> ConsultarResumenSegunUsuario_382QA()
        {
            List<BE_ReclamoResumen_382QA> reclamos = ConsultarResumen_382QA();
            if (!bll_tecnico.SesionEsTecnico_382QA()) return reclamos;

            BE_Tecnico_382QA tecnico = bll_tecnico.ConsultarTecnicoDeSesion_382QA()
                ?? throw new Exception("Su usuario no está registrado como técnico activo.");
            return reclamos.Where(r => r.DniTecnico == tecnico.Dni).ToList();
        }

        /// <summary>
        /// CU-NEG-8: el reclamo, si existe y el usuario puede trabajar sobre él. Un técnico solo accede
        /// a los reclamos asignados a él; un usuario que no es técnico (ej. el administrador) accede a cualquiera.
        /// </summary>
        public BE_ReclamoResumen_382QA ObtenerReclamoAccesible_382QA(int nroReclamo)
        {
            BE_ReclamoResumen_382QA reclamo = map_reclamo.ConsultarResumenPorNro_382QA(nroReclamo)
                ?? throw new Exception($"No existe el reclamo N° {nroReclamo}.");

            if (bll_tecnico.SesionEsTecnico_382QA())
            {
                BE_Tecnico_382QA tecnicoSesion = bll_tecnico.ConsultarTecnicoDeSesion_382QA()
                    ?? throw new Exception("Su usuario no está registrado como técnico activo.");
                if (reclamo.DniTecnico != tecnicoSesion.Dni)
                    throw new Exception($"El reclamo N° {nroReclamo} está asignado a otro técnico.");
            }

            return reclamo;
        }

        /// <summary>CU-NEG-8: falla, diagnóstico y repuestos utilizados de la OT original del reclamo.</summary>
        public BE_ReparacionOriginal_382QA ConsultarReparacionOriginal_382QA(int nroReclamo)
        {
            BE_ReclamoResumen_382QA reclamo = ObtenerReclamoAccesible_382QA(nroReclamo);
            BE_OTResumen_382QA ot = map_ot.ConsultarResumenPorNro_382QA(reclamo.NroOT)
                ?? throw new Exception($"No existe la OT N° {reclamo.NroOT}.");

            return new BE_ReparacionOriginal_382QA
            {
                NroOT = ot.NroOT,
                DescripcionFalla = ot.DescripcionFalla,
                DetalleDiagnostico = map_reclamo.ConsultarDetalleDiagnostico_382QA(ot.NroOT) ?? "",
                Repuestos = map_reparacion.ConsultarLineasPorOT_382QA(ot.NroOT).Where(l => l.CantidadUtilizada > 0).ToList()
            };
        }

        /// <summary>
        /// CU-NEG-8 Evaluar reclamo. El reclamo tiene que estar "En evaluacion" y el usuario poder trabajar sobre él.
        /// Si la falla está cubierta pasa a "Aprobado" y si no, a "Rechazado" (conserva el técnico).
        /// El cambio de estado es condicional: si otro usuario ya lo modificó, no se guarda.
        /// No toca stock ni repuestos.
        /// </summary>
        public void EvaluarReclamo_382QA(int nroReclamo, bool? fallaCubierta, string detalle)
        {
            detalle = detalle?.Trim() ?? "";

            BE_ReclamoResumen_382QA reclamo = ObtenerReclamoAccesible_382QA(nroReclamo);
            if (reclamo.Estado != EstadosReclamo_382QA.EnEvaluacion)
                throw new Exception($"El reclamo N° {nroReclamo} está en estado \"{reclamo.Estado}\" y no se puede evaluar.");

            if (string.IsNullOrWhiteSpace(detalle))
                throw new Exception("El detalle de la evaluación es obligatorio.");
            if (detalle.Length > BE_Reclamo_382QA.LargoMaximoDetalleEvaluacion)
                throw new Exception($"El detalle de la evaluación no puede superar los {BE_Reclamo_382QA.LargoMaximoDetalleEvaluacion} caracteres.");
            if (fallaCubierta == null)
                throw new Exception("Debe indicar si la falla está cubierta por la garantía.");

            string estadoNuevo = fallaCubierta.Value ? EstadosReclamo_382QA.Aprobado : EstadosReclamo_382QA.Rechazado;
            if (!map_reclamo.RegistrarEvaluacion_382QA(nroReclamo, fallaCubierta.Value, detalle, EstadosReclamo_382QA.EnEvaluacion, estadoNuevo))
                throw new Exception($"El reclamo N° {nroReclamo} ya no está en evaluación. Actualice el listado.");

            bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaReclamo, nroReclamo);
            bll_bitacora.RegistrarBitacora_382QA(new SER_Bitacora_382QA(
                SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, Modulo, "Evaluar reclamo", 2));
        }

        /// <summary>
        /// CU-NEG-9: reclamo a reparar (accesible para el usuario y "Aprobado" o "En espera de repuesto").
        /// </summary>
        public BE_ReclamoResumen_382QA ObtenerReclamoParaReparar_382QA(int nroReclamo)
        {
            BE_ReclamoResumen_382QA reclamo = ObtenerReclamoAccesible_382QA(nroReclamo);
            if (reclamo.Estado != EstadosReclamo_382QA.Aprobado && reclamo.Estado != EstadosReclamo_382QA.EnEsperaRepuesto)
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
        public List<BE_FaltanteStock_382QA> RepararReclamo_382QA(int nroReclamo, Dictionary<string, int> repuestos)
        {
            BE_ReclamoResumen_382QA reclamo = ObtenerReclamoParaReparar_382QA(nroReclamo);
            repuestos ??= new Dictionary<string, int>();

            var faltantes = new List<BE_FaltanteStock_382QA>();
            foreach (var (codigo, cantidad) in repuestos)
            {
                BE_Repuesto_382QA repuesto = bll_repuesto.ConsultarPorCodigo_382QA(codigo)
                    ?? throw new Exception($"El repuesto {codigo} no existe o está dado de baja.");
                if (cantidad <= 0)
                    throw new Exception($"La cantidad del repuesto {repuesto.Codigo} debe ser mayor a 0.");
                if (repuesto.StockDisponible < cantidad)
                    faltantes.Add(new BE_FaltanteStock_382QA(repuesto.Codigo, repuesto.Descripcion, cantidad, repuesto.StockDisponible));
            }

            if (faltantes.Count > 0)
            {
                // Sin stock no se descuenta nada: el reclamo queda esperando (si ya estaba esperando, no hay nada que cambiar)
                if (reclamo.Estado != EstadosReclamo_382QA.EnEsperaRepuesto)
                {
                    if (!map_reclamo.CambiarEstado_382QA(nroReclamo, reclamo.Estado, EstadosReclamo_382QA.EnEsperaRepuesto))
                        throw new Exception($"El reclamo N° {nroReclamo} cambió de estado. Actualice el listado.");
                    bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaReclamo, nroReclamo);
                    RegistrarBitacora_382QA("Reclamo en espera de repuesto");
                }
                return faltantes;
            }

            try
            {
                map_reclamo.RegistrarReparacion_382QA(nroReclamo, repuestos, reclamo.Estado, EstadosReclamo_382QA.Reparado);
            }
            catch (InvalidOperationException ex)
            {
                // Otro usuario cambió el estado o consumió el stock entre la validación y la escritura: no se guardó nada
                throw new Exception(ex.Message + " Vuelva a intentar.");
            }

            bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaReclamo, nroReclamo);
            foreach (string codigo in repuestos.Keys)
            {
                bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaDetalleRepuestoReclamo, nroReclamo, codigo);
                bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaRepuesto, codigo);
            }
            RegistrarBitacora_382QA("Reparar reclamo");

            return faltantes;
        }

        /// <summary>CU-NEG-10: reclamo listo para entregar ("Reparado" o "Rechazado"), con los datos para mostrar al vendedor.</summary>
        public BE_ReclamoResumen_382QA ObtenerReclamoParaEntregar_382QA(int nroReclamo)
        {
            BE_ReclamoResumen_382QA reclamo = map_reclamo.ConsultarResumenPorNro_382QA(nroReclamo)
                ?? throw new Exception($"No existe el reclamo N° {nroReclamo}.");
            if (reclamo.Estado != EstadosReclamo_382QA.Reparado && reclamo.Estado != EstadosReclamo_382QA.Rechazado)
                throw new Exception($"El reclamo N° {nroReclamo} está en estado \"{reclamo.Estado}\": solo se entregan reclamos reparados o rechazados.");
            return reclamo;
        }

        /// <summary>
        /// CU-NEG-10 Gestionar entrega de reclamo. Sin cobro ni garantía nueva.
        ///   "Reparado" → "Resuelto"; "Rechazado" → "Entregado sin reparar" (el equipo vuelve sin reparar).
        /// El cambio de estado es condicional: si otro usuario ya lo entregó, no se guarda.
        /// </summary>
        public BE_ReclamoResumen_382QA EntregarReclamo_382QA(int nroReclamo)
        {
            BE_ReclamoResumen_382QA reclamo = ObtenerReclamoParaEntregar_382QA(nroReclamo);
            bool reparado = reclamo.Estado == EstadosReclamo_382QA.Reparado;
            string estadoNuevo = reparado ? EstadosReclamo_382QA.Resuelto : EstadosReclamo_382QA.EntregadoSinReparar;

            if (!map_reclamo.CambiarEstado_382QA(nroReclamo, reclamo.Estado, estadoNuevo))
                throw new Exception($"El reclamo N° {nroReclamo} cambió de estado. Actualice el listado.");

            bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaReclamo, nroReclamo);
            RegistrarBitacora_382QA(reparado ? "Entregar reclamo" : "Entregar reclamo sin reparar");

            return map_reclamo.ConsultarResumenPorNro_382QA(nroReclamo)!;
        }

        private void RegistrarBitacora_382QA(string evento) =>
            bll_bitacora.RegistrarBitacora_382QA(new SER_Bitacora_382QA(
                SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, Modulo, evento, 2));

        /// <summary>
        /// CU-NEG-7 paso 1: OT entregadas con garantía del cliente (vigente o vencida: la vigencia se valida al registrar).
        /// El cliente tiene que estar registrado y activo. Una lista vacía significa que no hay una reparación cubierta.
        /// </summary>
        public List<BE_ReparacionConGarantia_382QA> ConsultarReparacionesConGarantia_382QA(string dni)
        {
            BE_Cliente_382QA cliente = ObtenerCliente_382QA(dni);
            return map_reclamo.ConsultarReparacionesConGarantia_382QA(cliente.Dni, EstadosOT_382QA.Entregado);
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
        public BE_ReclamoResumen_382QA RegistrarReclamo_382QA(string dni, int nroOT, string motivo, string descripcion)
        {
            motivo = motivo?.Trim() ?? "";
            descripcion = descripcion?.Trim() ?? "";

            // Paso 1: cliente y OT original entregada con garantía
            BE_Cliente_382QA cliente = ObtenerCliente_382QA(dni);

            BE_ReparacionConGarantia_382QA reparacion = map_reclamo.ConsultarReparacionConGarantia_382QA(nroOT, EstadosOT_382QA.Entregado)
                ?? throw new Exception($"La OT N° {nroOT} no es una reparación entregada con garantía.");

            if (reparacion.DniCliente != cliente.Dni)
                throw new Exception($"La OT N° {nroOT} no pertenece al cliente con DNI {cliente.Dni}.");

            int reclamoAbierto = map_reclamo.ObtenerReclamoDelEquipo_382QA(reparacion.IdEquipo, EstadosReclamo_382QA.Abiertos);
            if (reclamoAbierto > 0)
                throw new Exception($"El equipo ya tiene el reclamo N° {reclamoAbierto} abierto.");

            int otAbierta = map_ot.ObtenerOTDelEquipo_382QA(reparacion.IdEquipo, EstadosOT_382QA.Abiertas);
            if (otAbierta > 0)
                throw new Exception($"El equipo está en el taller con la OT N° {otAbierta} sin entregar. No se puede registrar el reclamo.");

            // Paso 2: datos del reclamo
            if (string.IsNullOrWhiteSpace(motivo))
                throw new Exception("El motivo del reclamo es obligatorio.");
            if (motivo.Length > BE_Reclamo_382QA.LargoMaximoMotivo)
                throw new Exception($"El motivo no puede superar los {BE_Reclamo_382QA.LargoMaximoMotivo} caracteres.");
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new Exception("La descripción del reclamo es obligatoria.");
            if (descripcion.Length > BE_Reclamo_382QA.LargoMaximoDescripcion)
                throw new Exception($"La descripción no puede superar los {BE_Reclamo_382QA.LargoMaximoDescripcion} caracteres.");

            // Paso 3 (se resuelve antes de escribir): vigencia de la garantía y técnico asignado
            DateTime hoy = DateTime.Today;
            var reclamo = new BE_Reclamo_382QA(nroOT, motivo, descripcion, hoy, EstadosReclamo_382QA.Pendiente);
            bool vigente = new BE_Garantia_382QA(nroOT, reparacion.FechaVencimiento).EstaVigente_382QA(hoy);
            if (vigente)
                reclamo.DniTecnico = bll_tecnico.AsignarTecnico_382QA().Dni;
            string estadoNuevo = vigente ? EstadosReclamo_382QA.EnEvaluacion : EstadosReclamo_382QA.Rechazado;

            reclamo.NroReclamo = map_reclamo.Registrar_382QA(reclamo, estadoNuevo);

            bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaReclamo, reclamo.NroReclamo);
            bll_bitacora.RegistrarBitacora_382QA(new SER_Bitacora_382QA(
                SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, Modulo, "Registrar reclamo", 2));

            return map_reclamo.ConsultarResumenPorNro_382QA(reclamo.NroReclamo)
                ?? throw new Exception($"El reclamo N° {reclamo.NroReclamo} se registró pero no se pudo consultar.");
        }

        private BE_Cliente_382QA ObtenerCliente_382QA(string dni)
        {
            dni = dni?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(dni))
                throw new Exception("Debe ingresar el DNI del cliente.");

            return bll_cliente.ConsultarPorDni_382QA(dni)
                ?? throw new Exception($"El DNI {dni} no pertenece a un cliente registrado.");
        }
    }
}
