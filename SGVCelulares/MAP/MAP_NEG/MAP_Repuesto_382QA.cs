using BE;
using DAL.DAL_NEG;
using System.Data;

namespace MAP.MAP_NEG
{
    public class MAP_Repuesto_382QA
    {
        DAO_Repuesto_382QA dao_repuesto;
        public MAP_Repuesto_382QA() => dao_repuesto = new();

        /// <summary>Todos los repuestos, activos e inactivos.</summary>
        public List<BE_Repuesto_382QA> Consultar_382QA()
        {
            var repuestos = new List<BE_Repuesto_382QA>();
            foreach (DataRow fila in dao_repuesto.Consultar_382QA("").Rows)
                repuestos.Add(Mapear_382QA(fila));
            return repuestos;
        }

        /// <summary>Repuesto por código (activo o inactivo), o null si no existe.</summary>
        public BE_Repuesto_382QA? ConsultarPorCodigo_382QA(string codigo)
        {
            DataTable tabla = dao_repuesto.Consultar_382QA(codigo);
            return tabla.Rows.Count > 0 ? Mapear_382QA(tabla.Rows[0]) : null;
        }

        public void Agregar_382QA(BE_Repuesto_382QA repuesto) =>
            dao_repuesto.Agregar_382QA(repuesto.Codigo, repuesto.Descripcion, repuesto.PrecioUnitario, repuesto.StockDisponible);

        /// <summary>False si el stock en la base ya no es stockOriginal (no se modificó nada).</summary>
        public bool Modificar_382QA(BE_Repuesto_382QA repuesto, int stockOriginal) =>
            dao_repuesto.Modificar_382QA(repuesto.Codigo, repuesto.Descripcion, repuesto.PrecioUnitario, repuesto.StockDisponible, stockOriginal) == 1;

        public void CambiarActivo_382QA(string codigo, bool activo) => dao_repuesto.CambiarActivo_382QA(codigo, activo);

        public int ContarOT_382QA(string codigo, IEnumerable<string> estados) => dao_repuesto.ContarOT_382QA(codigo, estados);

        private BE_Repuesto_382QA Mapear_382QA(DataRow fila)
        {
            return new BE_Repuesto_382QA(
                Convert.ToString(fila["Codigo"]),
                Convert.ToString(fila["Descripcion"]),
                Convert.ToDecimal(fila["PrecioUnitario"]),
                Convert.ToInt32(fila["stockDisponible"]),
                Convert.ToBoolean(fila["Activo"])
            );
        }
    }
}
