using BE;
using MAP.MAP_SER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BLL_NEG
{
    public class BLL_Tecnico
    {
        MAP_Tecnico map_tecnico;
        public BLL_Tecnico() => map_tecnico = new();

        // Paso 7 del CU: asigna al técnico disponible con menos OT abiertas
        public BE_Tecnico AsignarTecnico()
        {
            BE_Tecnico tecnico = map_tecnico.ObtenerConMenosOTs();
            if (tecnico == null) throw new Exception("No hay técnicos disponibles para asignar.");
            return tecnico;
        }
    }
}
