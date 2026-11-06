using BE;
using DAL.DAL_NEG;
using System.Data;

namespace MAP.MAP_NEG
{
    public class MAP_Reparacion_382QA
    {
        DAO_Reparacion_382QA dao_reparacion;
        public MAP_Reparacion_382QA() => dao_reparacion = new();

        public List<BE_LineaReparacion_382QA> ConsultarLineasPorOT_382QA(int nroOT)
        {
            var lineas = new List<BE_LineaReparacion_382QA>();
            foreach (DataRow fila in dao_reparacion.ConsultarLineasPorOT_382QA(nroOT).Rows)
            {
                lineas.Add(new BE_LineaReparacion_382QA
                {
                    IdDiagnostico = Convert.ToInt32(fila["IdDiagnostico"]),
                    Codigo = Convert.ToString(fila["Codigo"]),
                    Descripcion = Convert.ToString(fila["Descripcion"]),
                    PrecioUnitario = Convert.ToDecimal(fila["PrecioUnitario"]),
                    CantidadRequerida = Convert.ToInt32(fila["cantidadRequerida"]),
                    CantidadUtilizada = Convert.ToInt32(fila["CantidadUtilizada"]),
                    StockDisponible = Convert.ToInt32(fila["stockDisponible"])
                });
            }
            return lineas;
        }

        public void Registrar_382QA(int nroOT, List<BE_DetalleRepuesto_382QA> repuestos, string estadoOTEsperado, string estadoOTNuevo)
        {
            dao_reparacion.Registrar_382QA(nroOT,
                repuestos.Select(r => (r.IdDiagnostico, r.Codigo, r.CantidadUtilizada)).ToList(),
                estadoOTEsperado, estadoOTNuevo);
        }
    }
}
