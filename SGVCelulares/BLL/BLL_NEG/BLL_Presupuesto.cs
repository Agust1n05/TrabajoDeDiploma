using BE;
using BLL.BLL_SER;
using MAP.MAP_NEG;
using Servicio;

namespace BLL.BLL_NEG
{
    /// <summary>
    /// CU-NEG-2 Gestionar aprobación de presupuesto: el vendedor registra la respuesta del cliente.
    /// </summary>
    public class BLL_Presupuesto
    {
        private const string Modulo = "Órdenes de trabajo";

        MAP_Presupuesto map_presupuesto;
        MAP_OT map_ot;
        BLL_Bitacora bll_bitacora;
        BLL_DV bll_dv;

        public BLL_Presupuesto()
        {
            map_presupuesto = new MAP_Presupuesto();
            map_ot = new MAP_OT();
            bll_bitacora = new BLL_Bitacora();
            bll_dv = new BLL_DV();
        }

        /// <summary>Paso 3: detalle del presupuesto de la OT (diagnóstico, mano de obra, repuestos y total).</summary>
        public BE_PresupuestoDetalle ConsultarDetalle(int nroOT)
        {
            return map_presupuesto.ConsultarDetallePorOT(nroOT)
                ?? throw new Exception($"La OT N° {nroOT} no tiene presupuesto.");
        }

        /// <summary>
        /// Pasos 5 a 8. Registra la respuesta del cliente con su fecha y actualiza la OT en la misma transacción:
        ///   aprobado  → presupuesto "Aprobado"  y OT "En reparacion" (disponible para el técnico, CU-NEG-5);
        ///   rechazado → presupuesto "Rechazado" y OT "Sin reparar"  (se entrega sin reparar, CU-NEG-3).
        /// </summary>
        /// <returns>La OT actualizada.</returns>
        public BE_OTResumen RegistrarRespuesta(int nroOT, bool aprobado, DateTime? fechaRespuesta)
        {
            // Paso 6: respuesta y fecha ingresadas
            if (!fechaRespuesta.HasValue)
                throw new Exception("Debe ingresar la fecha de respuesta del cliente.");

            if (fechaRespuesta.Value.Date > DateTime.Today)
                throw new Exception("La fecha de respuesta no puede ser posterior a hoy.");

            BE_OTResumen ot = map_ot.ConsultarResumenPorNro(nroOT)
                ?? throw new Exception($"No existe la OT N° {nroOT}.");

            if (ot.Estado != EstadosOT.Presupuestado)
                throw new Exception($"La OT N° {nroOT} está en estado \"{ot.Estado}\": solo se responden presupuestos de OT presupuestadas.");

            BE_PresupuestoDetalle detalle = ConsultarDetalle(nroOT);
            if (detalle.EstadoPresupuesto != EstadosPresupuesto.Pendiente)
                throw new Exception($"El presupuesto de la OT N° {nroOT} ya fue {detalle.EstadoPresupuesto.ToLower()}.");

            // Pasos 7 y 8
            var presupuesto = new BE_Presupuesto(detalle.IdPresupuesto, detalle.TotalPresupuesto,
                aprobado ? EstadosPresupuesto.Aprobado : EstadosPresupuesto.Rechazado,
                fechaRespuesta.Value.Date, nroOT);
            string estadoOTNuevo = aprobado ? EstadosOT.EnReparacion : EstadosOT.SinReparar;

            map_presupuesto.RegistrarRespuesta(presupuesto, EstadosPresupuesto.Pendiente, EstadosOT.Presupuestado, estadoOTNuevo);

            bll_dv.ActualizarDVNegocio(BLL_DV.TablaPresupuesto, presupuesto.IdPresupuesto);
            bll_dv.ActualizarDVNegocio(BLL_DV.TablaOrdenTrabajo, nroOT);
            bll_bitacora.RegistrarBitacora(new SER_Bitacora(
                SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, Modulo,
                aprobado ? "Aprobar presupuesto" : "Rechazar presupuesto", 2));

            return map_ot.ConsultarResumenPorNro(nroOT)!;
        }
    }
}
