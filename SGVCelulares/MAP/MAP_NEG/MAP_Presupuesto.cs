using DAL;
using DAL.DAL_NEG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.MAP_NEG
{
    public class MAP_Presupuesto
    {
        DAO_Presupuesto dao_presupuesto;
        public MAP_Presupuesto() => dao_presupuesto = new();
    }
}
