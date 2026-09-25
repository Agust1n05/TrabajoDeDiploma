using DAL;
using DAL.DAL_NEG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.MAP_NEG
{
    public class MAP_Repuesto
    {
        DAO_Repuesto dao_repuesto;
        public MAP_Repuesto() => dao_repuesto = new();
    }
}
