using DAL;
using Servicio;
using System.Data;

namespace MAP
{
    public class MAP_Permiso
    {
        DAO_Permiso dao_permiso;
        public MAP_Permiso() => dao_permiso = new();
        public List<SER_Permiso> Consultar()
        {
            var lista = new List<SER_Permiso>();
            DataTable tabla = dao_permiso.Consultar();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(Mapear(fila));
            }
            return lista;
        }
        private SER_Permiso Mapear(DataRow fila) => new SER_Permiso(Convert.ToInt16(fila[0]), Convert.ToString(fila[1]));
    }
}
