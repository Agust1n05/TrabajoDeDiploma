using BE;
using DAL.DAL_NEG;
using System.Data;

namespace MAP.MAP_NEG
{
    public class MAP_Reparacion
    {
        DAO_Reparacion dao_reparacion;
        public MAP_Reparacion() => dao_reparacion = new();

        public List<BE_LineaReparacion> ConsultarLineasPorOT(int nroOT)
        {
            var lineas = new List<BE_LineaReparacion>();
            foreach (DataRow fila in dao_reparacion.ConsultarLineasPorOT(nroOT).Rows)
            {
                lineas.Add(new BE_LineaReparacion
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

        public void Registrar(int nroOT, List<BE_DetalleRepuesto> repuestos, string estadoOTEsperado, string estadoOTNuevo)
        {
            dao_reparacion.Registrar(nroOT,
                repuestos.Select(r => (r.IdDiagnostico, r.Codigo, r.CantidadUtilizada)).ToList(),
                estadoOTEsperado, estadoOTNuevo);
        }
    }
}
