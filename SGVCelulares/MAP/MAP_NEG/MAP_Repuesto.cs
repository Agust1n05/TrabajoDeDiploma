using BE;
using DAL.DAL_NEG;
using System.Data;

namespace MAP.MAP_NEG
{
    public class MAP_Repuesto
    {
        DAO_Repuesto dao_repuesto;
        public MAP_Repuesto() => dao_repuesto = new();

        /// <summary>Todos los repuestos, activos e inactivos.</summary>
        public List<BE_Repuesto> Consultar()
        {
            var repuestos = new List<BE_Repuesto>();
            foreach (DataRow fila in dao_repuesto.Consultar("").Rows)
                repuestos.Add(Mapear(fila));
            return repuestos;
        }

        /// <summary>Repuesto por código (activo o inactivo), o null si no existe.</summary>
        public BE_Repuesto? ConsultarPorCodigo(string codigo)
        {
            DataTable tabla = dao_repuesto.Consultar(codigo);
            return tabla.Rows.Count > 0 ? Mapear(tabla.Rows[0]) : null;
        }

        public void Agregar(BE_Repuesto repuesto) =>
            dao_repuesto.Agregar(repuesto.Codigo, repuesto.Descripcion, repuesto.PrecioUnitario, repuesto.StockDisponible);

        /// <summary>False si el stock en la base ya no es stockOriginal (no se modificó nada).</summary>
        public bool Modificar(BE_Repuesto repuesto, int stockOriginal) =>
            dao_repuesto.Modificar(repuesto.Codigo, repuesto.Descripcion, repuesto.PrecioUnitario, repuesto.StockDisponible, stockOriginal) == 1;

        public void CambiarActivo(string codigo, bool activo) => dao_repuesto.CambiarActivo(codigo, activo);

        public int ContarOT(string codigo, IEnumerable<string> estados) => dao_repuesto.ContarOT(codigo, estados);

        private BE_Repuesto Mapear(DataRow fila)
        {
            return new BE_Repuesto(
                Convert.ToString(fila["Codigo"]),
                Convert.ToString(fila["Descripcion"]),
                Convert.ToDecimal(fila["PrecioUnitario"]),
                Convert.ToInt32(fila["stockDisponible"]),
                Convert.ToBoolean(fila["Activo"])
            );
        }
    }
}
