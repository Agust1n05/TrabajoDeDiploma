using BE;
using DAL.DAL_NEG;

namespace MAP.MAP_NEG
{
    public class MAP_Diagnostico_382QA
    {
        DAO_Diagnostico_382QA dao_diagnostico;
        public MAP_Diagnostico_382QA() => dao_diagnostico = new();

        /// <summary>
        /// Guarda el diagnóstico con sus repuestos, el presupuesto (si viene) y el nuevo estado de la OT,
        /// todo en una transacción. Devuelve los ids generados (IdPresupuesto = 0 si no hay presupuesto).
        /// </summary>
        public (int IdDiagnostico, int IdPresupuesto) Registrar_382QA(
            BE_Diagnostico_382QA diagnostico, List<BE_DetalleRepuesto_382QA> repuestos, BE_Presupuesto_382QA? presupuesto,
            string estadoOTEsperado, string estadoOTNuevo)
        {
            return dao_diagnostico.Registrar_382QA(
                diagnostico.NroOT, diagnostico.Detalle, diagnostico.CostoManoObra,
                repuestos.Select(r => (r.Codigo, r.PrecioUnitario, r.CantidadRequerida)).ToList(),
                presupuesto?.TotalPresupuesto, presupuesto?.EstadoPresupuesto ?? EstadosPresupuesto_382QA.Pendiente,
                estadoOTEsperado, estadoOTNuevo);
        }
    }
}
