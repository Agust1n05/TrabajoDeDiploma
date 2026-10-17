using DAL.DAL_SER;
using Servicio;
using System.Data;
namespace MAP.MAP_SER
{
    public class MAP_DV
    {
        DAO_DV dao_dv;
        public MAP_DV() => dao_dv = new();
        public void AgregarDVH(SER_DVH dvh) => dao_dv.AgregarDVH(dvh.NombreTabla, dvh.IdRegistro, dvh.Valor);
        public void BorrarDVH(string nombreTabla, string idRegistro) => dao_dv.BorrarDVH(nombreTabla, idRegistro);
        public void ModificarDVH(SER_DVH dvh) => dao_dv.ModificarDVH(dvh.NombreTabla, dvh.IdRegistro, dvh.Valor);
        public void AgregarDVV(SER_DVV dvv) => dao_dv.AgregarDVV(dvv.NombreTabla, dvv.Valor);
        public void BorrarDVV(string nombreTabla) => dao_dv.BorrarDVV(nombreTabla);
        public void ModificarDVV(SER_DVV dvv) => dao_dv.ModificarDVV(dvv.NombreTabla, dvv.Valor);
        public bool ExisteDVH(string nombreTabla, string id) => dao_dv.ExisteDVH(nombreTabla, id);
        public bool ExisteDVV(string nombreTabla) => dao_dv.ExisteDVV(nombreTabla);
        public SER_DVH ObtenerDVH(string nombreTabla, string idRegistro)
        {
            DataTable tabla = dao_dv.ObtenerDVH(nombreTabla, idRegistro);
            return tabla.Rows.Count > 0 ? new SER_DVH(tabla.Rows[0].ItemArray) : null;
        }
        public SER_DVV ObtenerDVV(string nombreTabla)
        {
            DataTable tabla = dao_dv.ObtenerDVV(nombreTabla);
            return tabla.Rows.Count > 0 ? new SER_DVV(tabla.Rows[0].ItemArray) : null;
        }
        public List<string> ConsultarDVHValores(string nombreTabla)
        {
            var valores = new List<string>();
            DataTable tabla = dao_dv.ConsultarDVH(nombreTabla);
            foreach (DataRow fila in tabla.Rows)
            {
                valores.Add(fila["valor"].ToString());
            }
            return valores;
        }
        public List<string> ConsultarDVHIds(string nombreTabla)
        {
            var ids = new List<string>();
            DataTable tabla = dao_dv.ConsultarDVH(nombreTabla);
            foreach (DataRow fila in tabla.Rows)
            {
                ids.Add(fila["idRegistro"].ToString());
            }
            return ids;
        }

        // ---------------- Tablas de negocio ----------------

        public List<SER_FilaNegocio> ConsultarFilasNegocio(string nombreTabla)
        {
            DataTable tabla = dao_dv.ConsultarFilasNegocio(nombreTabla);
            string[] columnas = ObtenerColumnas(tabla);
            var filas = new List<SER_FilaNegocio>();
            foreach (DataRow fila in tabla.Rows)
            {
                filas.Add(new SER_FilaNegocio(columnas, fila.ItemArray));
            }
            return filas;
        }

        public SER_FilaNegocio? ConsultarFilaNegocio(string nombreTabla, string[] columnasId, string[] valoresId)
        {
            DataTable tabla = dao_dv.ConsultarFilaNegocio(nombreTabla, columnasId, valoresId);
            return tabla.Rows.Count > 0 ? new SER_FilaNegocio(ObtenerColumnas(tabla), tabla.Rows[0].ItemArray) : null;
        }

        /// <summary>DVH guardados de una tabla de negocio: IdRegistro → Valor.</summary>
        public Dictionary<string, string> ConsultarDVHNegocio(string nombreTabla)
        {
            var dvh = new Dictionary<string, string>();
            DataTable tabla = dao_dv.ConsultarDVHNegocio(nombreTabla);
            foreach (DataRow fila in tabla.Rows)
            {
                dvh[fila["IdRegistro"].ToString()] = fila["Valor"].ToString();
            }
            return dvh;
        }

        private string[] ObtenerColumnas(DataTable tabla)
        {
            return tabla.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();
        }
    }
}
