using BE;
using BLL.BLL_SER;
using MAP.MAP_NEG;
using Servicio;

namespace BLL.BLL_NEG
{
    /// <summary>
    /// Lógica de los reclamos por garantía de PN-02.
    /// CU-NEG-7 Registrar reclamo de garantía: ConsultarReparacionesConGarantia y RegistrarReclamo.
    /// </summary>
    public class BLL_Reclamo
    {
        private const string Modulo = "Reclamos";

        MAP_Reclamo map_reclamo;
        MAP_OT map_ot;
        BLL_Cliente bll_cliente;
        BLL_Tecnico bll_tecnico;
        BLL_Bitacora bll_bitacora;
        BLL_DV bll_dv;

        public BLL_Reclamo()
        {
            map_reclamo = new MAP_Reclamo();
            map_ot = new MAP_OT();
            bll_cliente = new BLL_Cliente();
            bll_tecnico = new BLL_Tecnico();
            bll_bitacora = new BLL_Bitacora();
            bll_dv = new BLL_DV();
        }

        /// <summary>Todos los reclamos con su OT original, cliente, equipo y técnico, del más nuevo al más viejo.</summary>
        public List<BE_ReclamoResumen> ConsultarResumen() => map_reclamo.ConsultarResumen();

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
