using DAL.DAL_SER;
using Servicio;
using System.Data;
namespace MAP.MAP_SER
{
    public class MAP_DV_382QA
    {
        DAO_DV_382QA dao_dv;
        public MAP_DV_382QA() => dao_dv = new();
        public void AgregarDVH_382QA(SER_DVH_382QA dvh) => dao_dv.AgregarDVH_382QA(dvh.NombreTabla, dvh.IdRegistro, dvh.Valor);
        public void BorrarDVH_382QA(string nombreTabla, string idRegistro) => dao_dv.BorrarDVH_382QA(nombreTabla, idRegistro);
        public void ModificarDVH_382QA(SER_DVH_382QA dvh) => dao_dv.ModificarDVH_382QA(dvh.NombreTabla, dvh.IdRegistro, dvh.Valor);
        public void AgregarDVV_382QA(SER_DVV_382QA dvv) => dao_dv.AgregarDVV_382QA(dvv.NombreTabla, dvv.Valor);
        public void BorrarDVV_382QA(string nombreTabla) => dao_dv.BorrarDVV_382QA(nombreTabla);
        public void ModificarDVV_382QA(SER_DVV_382QA dvv) => dao_dv.ModificarDVV_382QA(dvv.NombreTabla, dvv.Valor);
        public bool ExisteDVH_382QA(string nombreTabla, string id) => dao_dv.ExisteDVH_382QA(nombreTabla, id);
        public bool ExisteDVV_382QA(string nombreTabla) => dao_dv.ExisteDVV_382QA(nombreTabla);
        public SER_DVH_382QA ObtenerDVH_382QA(string nombreTabla, string idRegistro)
        {
            DataTable tabla = dao_dv.ObtenerDVH_382QA(nombreTabla, idRegistro);
            return tabla.Rows.Count > 0 ? new SER_DVH_382QA(tabla.Rows[0].ItemArray) : null;
        }
        public SER_DVV_382QA ObtenerDVV_382QA(string nombreTabla)
        {
            DataTable tabla = dao_dv.ObtenerDVV_382QA(nombreTabla);
            return tabla.Rows.Count > 0 ? new SER_DVV_382QA(tabla.Rows[0].ItemArray) : null;
        }
        public List<string> ConsultarDVHValores_382QA(string nombreTabla)
        {
            var valores = new List<string>();
            DataTable tabla = dao_dv.ConsultarDVH_382QA(nombreTabla);
            foreach (DataRow fila in tabla.Rows)
            {
                valores.Add(fila["valor"].ToString());
            }
            return valores;
        }
        public List<string> ConsultarDVHIds_382QA(string nombreTabla)
        {
            var ids = new List<string>();
            DataTable tabla = dao_dv.ConsultarDVH_382QA(nombreTabla);
            foreach (DataRow fila in tabla.Rows)
            {
                ids.Add(fila["idRegistro"].ToString());
            }
            return ids;
        }

        // ---------------- Tablas de negocio ----------------

        public List<SER_FilaNegocio_382QA> ConsultarFilasNegocio_382QA(string nombreTabla)
        {
            DataTable tabla = dao_dv.ConsultarFilasNegocio_382QA(nombreTabla);
            string[] columnas = ObtenerColumnas_382QA(tabla);
            var filas = new List<SER_FilaNegocio_382QA>();
            foreach (DataRow fila in tabla.Rows)
            {
                filas.Add(new SER_FilaNegocio_382QA(columnas, fila.ItemArray));
            }
            return filas;
        }

        public SER_FilaNegocio_382QA? ConsultarFilaNegocio_382QA(string nombreTabla, string[] columnasId, string[] valoresId)
        {
            DataTable tabla = dao_dv.ConsultarFilaNegocio_382QA(nombreTabla, columnasId, valoresId);
            return tabla.Rows.Count > 0 ? new SER_FilaNegocio_382QA(ObtenerColumnas_382QA(tabla), tabla.Rows[0].ItemArray) : null;
        }

        /// <summary>DVH guardados de una tabla de negocio: IdRegistro → Valor.</summary>
        public Dictionary<string, string> ConsultarDVHNegocio_382QA(string nombreTabla)
        {
            var dvh = new Dictionary<string, string>();
            DataTable tabla = dao_dv.ConsultarDVHNegocio_382QA(nombreTabla);
            foreach (DataRow fila in tabla.Rows)
            {
                dvh[fila["IdRegistro"].ToString()] = fila["Valor"].ToString();
            }
            return dvh;
        }

        private string[] ObtenerColumnas_382QA(DataTable tabla)
        {
            return tabla.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();
        }
    }
}
