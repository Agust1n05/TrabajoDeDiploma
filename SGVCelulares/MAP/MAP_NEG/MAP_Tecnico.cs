using BE;
using DAL;
using Servicio;
using System.Data;

namespace MAP.MAP_SER
{
    public class MAP_Tecnico
    {
        DAO_Tecnico dao_tecnico;
        public MAP_Tecnico() => dao_tecnico = new();

        public BE_Tecnico ObtenerConMenosOTs()
        {
            DataTable tabla = dao_tecnico.ObtenerConMenosOTs();
            return tabla.Rows.Count > 0 ? Mapear(tabla.Rows[0]) : null;
        }

        private BE_Tecnico Mapear(DataRow fila)
        {
            return new BE_Tecnico(
                Convert.ToString(fila["dni"]),
                Convert.ToString(fila["Nombre"]),
                Convert.ToString(fila["Apellido"]),
                Convert.ToBoolean(fila["Disponible"])
            );
        }
    }
}
