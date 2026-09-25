using DAL;
using BE;
using System.Data;

namespace MAP.MAP_NEG
{
    public class MAP_OT
    {
        DAO_OT dao_ot;
        public MAP_OT() => dao_ot = new();

        public List<BE_OT> Consultar()
        {
            var lista = new List<BE_OT>();
            DataTable tabla = dao_ot.Consultar();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(Mapear(fila));
            }
            return lista;
        }

        public int Agregar(BE_OT ot) => dao_ot.Agregar(ot.Estado, ot.Dni, ot.IdEquipo, ot.DniTecnico);

        private BE_OT Mapear(DataRow fila)
        {
            return new BE_OT(
                Convert.ToInt32(fila["NroOT"]),
                Convert.ToString(fila["Estado"]),
                Convert.ToString(fila["dni"]),
                Convert.ToString(fila["idEquipo"]),
                Convert.ToString(fila["DniTecnico"])
            );
        }
    }
}