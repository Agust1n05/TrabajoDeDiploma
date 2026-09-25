using BE;
using DAL;
using Servicio;

namespace MAP.MAP_SER
{
    public class MAP_Equipo
    {
        DAO_Equipo dao_equipo;
        public MAP_Equipo() => dao_equipo = new();

        public void Agregar(BE_Equipo equipo) =>
            dao_equipo.Agregar(equipo.IdEquipo, equipo.Marca, equipo.Modelo, equipo.Imei, equipo.DescripcionFalla);
    }
}
