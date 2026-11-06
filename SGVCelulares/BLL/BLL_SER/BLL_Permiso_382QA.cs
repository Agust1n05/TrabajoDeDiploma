using MAP.MAP_SER;
using Servicio;

namespace BLL.BLL_SER
{
    public class BLL_Permiso_382QA
    {
        MAP_Permiso_382QA map_permiso;
        public BLL_Permiso_382QA()
        {
            map_permiso = new MAP_Permiso_382QA();
        }

        public List<SER_Permiso_382QA> Consultar_382QA()
        {
            return map_permiso.Consultar_382QA();
        }
    }
}
