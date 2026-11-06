using BE;
using BLL.BLL_SER;
using MAP.MAP_NEG;
using Servicio;

namespace BLL.BLL_NEG
{
    /// <summary>
    /// CU-NEG-2 Gestionar aprobación de presupuesto: el vendedor registra la respuesta del cliente.
    /// </summary>
    public class BLL_Presupuesto_382QA
    {
        private const string Modulo = "Órdenes de trabajo";

        MAP_Presupuesto_382QA map_presupuesto;
        MAP_OT_382QA map_ot;
        BLL_Bitacora_382QA bll_bitacora;
        BLL_DV_382QA bll_dv;

        public BLL_Presupuesto_382QA()
        {
            map_presupuesto = new MAP_Presupuesto_382QA();
            map_ot = new MAP_OT_382QA();
            bll_bitacora = new BLL_Bitacora_382QA();
            bll_dv = new BLL_DV_382QA();
        }

        /// <summary>Paso 3: detalle del presupuesto de la OT (diagnóstico, mano de obra, repuestos y total).</summary>
        public BE_PresupuestoDetalle_382QA ConsultarDetalle_382QA(int nroOT)
        {
            return map_presupuesto.ConsultarDetallePorOT_382QA(nroOT)
                ?? throw new Exception($"La OT N° {nroOT} no tiene presupuesto.");
        }

        /// <summary>
        /// Pasos 5 a 8. Registra la respuesta del cliente con su fecha y actualiza la OT en la misma transacción:
        ///   aprobado  → presupuesto "Aprobado"  y OT "En reparacion" (disponible para el técnico, CU-NEG-5);
        ///   rechazado → presupuesto "Rechazado" y OT "Sin reparar"  (se entrega sin reparar, CU-NEG-3).
        /// </summary>
        /// <returns>La OT actualizada.</returns>
        public BE_OTResumen_382QA RegistrarRespuesta_382QA(int nroOT, bool aprobado, DateTime? fechaRespuesta)
        {
            // Paso 6: respuesta y fecha ingresadas
            if (!fechaRespuesta.HasValue)
                throw new Exception("Debe ingresar la fecha de respuesta del cliente.");

            if (fechaRespuesta.Value.Date > DateTime.Today)
                throw new Exception("La fecha de respuesta no puede ser posterior a hoy.");

            BE_OTResumen_382QA ot = map_ot.ConsultarResumenPorNro_382QA(nroOT)
                ?? throw new Exception($"No existe la OT N° {nroOT}.");

            if (ot.Estado != EstadosOT_382QA.Presupuestado)
                throw new Exception($"La OT N° {nroOT} está en estado \"{ot.Estado}\": solo se responden presupuestos de OT presupuestadas.");

            BE_PresupuestoDetalle_382QA detalle = ConsultarDetalle_382QA(nroOT);
            if (detalle.EstadoPresupuesto != EstadosPresupuesto_382QA.Pendiente)
                throw new Exception($"El presupuesto de la OT N° {nroOT} ya fue {detalle.EstadoPresupuesto.ToLower()}.");

            // Pasos 7 y 8
            var presupuesto = new BE_Presupuesto_382QA(detalle.IdPresupuesto, detalle.TotalPresupuesto,
                aprobado ? EstadosPresupuesto_382QA.Aprobado : EstadosPresupuesto_382QA.Rechazado,
                fechaRespuesta.Value.Date, nroOT);
            string estadoOTNuevo = aprobado ? EstadosOT_382QA.EnReparacion : EstadosOT_382QA.SinReparar;

            map_presupuesto.RegistrarRespuesta_382QA(presupuesto, EstadosPresupuesto_382QA.Pendiente, EstadosOT_382QA.Presupuestado, estadoOTNuevo);

            bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaPresupuesto, presupuesto.IdPresupuesto);
            bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaOrdenTrabajo, nroOT);
            bll_bitacora.RegistrarBitacora_382QA(new SER_Bitacora_382QA(
                SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, Modulo,
                aprobado ? "Aprobar presupuesto" : "Rechazar presupuesto", 2));

            return map_ot.ConsultarResumenPorNro_382QA(nroOT)!;
        }
    }
}
