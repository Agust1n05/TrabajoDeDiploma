using MAP;
using Servicio;

namespace BLL
{
    public class BLL_Permiso
    {
        MAP_Permiso map_permiso;
        public BLL_Permiso()
        {
            map_permiso = new MAP_Permiso();
        }

        public List<SER_Permiso> Consultar()
        {
            return map_permiso.Consultar();
        }
    }
}
