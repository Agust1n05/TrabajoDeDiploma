using DAL.DAL_SER;
using Servicio;
using System.Data;

namespace MAP.MAP_SER
{
    public class MAP_Permiso_382QA
    {
        DAO_Permiso_382QA dao_permiso;
        public MAP_Permiso_382QA() => dao_permiso = new();
        public List<SER_Permiso_382QA> Consultar_382QA()
        {
            var lista = new List<SER_Permiso_382QA>();
            DataTable tabla = dao_permiso.Consultar_382QA();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(Mapear_382QA(fila));
            }
            return lista;
        }
        private SER_Permiso_382QA Mapear_382QA(DataRow fila) => new SER_Permiso_382QA(Convert.ToInt16(fila[0]), Convert.ToString(fila[1]));
    }
}
