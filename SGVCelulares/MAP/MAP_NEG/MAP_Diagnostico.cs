using BE;
using DAL.DAL_NEG;

namespace MAP.MAP_NEG
{
    public class MAP_Diagnostico
    {
        DAO_Diagnostico dao_diagnostico;
        public MAP_Diagnostico() => dao_diagnostico = new();

        /// <summary>
        /// Guarda el diagnóstico con sus repuestos, el presupuesto (si viene) y el nuevo estado de la OT,
        /// todo en una transacción. Devuelve los ids generados (IdPresupuesto = 0 si no hay presupuesto).
        /// </summary>
        public (int IdDiagnostico, int IdPresupuesto) Registrar(
            BE_Diagnostico diagnostico, List<BE_DetalleRepuesto> repuestos, BE_Presupuesto? presupuesto,
            string estadoOTEsperado, string estadoOTNuevo)
        {
            return dao_diagnostico.Registrar(
                diagnostico.NroOT, diagnostico.Detalle, diagnostico.CostoManoObra,
                repuestos.Select(r => (r.Codigo, r.CantidadRequerida)).ToList(),
                presupuesto?.TotalPresupuesto, presupuesto?.EstadoPresupuesto ?? EstadosPresupuesto.Pendiente,
                estadoOTEsperado, estadoOTNuevo);
        }
    }
}
