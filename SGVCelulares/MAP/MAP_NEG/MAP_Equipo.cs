using BE;
using DAL.DAL_NEG;
using System.Data;

namespace MAP.MAP_NEG
{
    public class MAP_Equipo
    {
        DAO_Equipo dao_equipo;
        public MAP_Equipo() => dao_equipo = new();

        /// <summary>Todos los equipos, activos e inactivos.</summary>
        public List<BE_Equipo> Consultar()
        {
            var equipos = new List<BE_Equipo>();
            foreach (DataRow fila in dao_equipo.Consultar().Rows)
                equipos.Add(Mapear(fila));
            return equipos;
        }

        public BE_Equipo? ConsultarPorId(int idEquipo)
        {
            DataTable tabla = dao_equipo.ConsultarPorId(idEquipo);
            return tabla.Rows.Count > 0 ? Mapear(tabla.Rows[0]) : null;
        }

        /// <summary>Equipo por IMEI (activo o inactivo), o null si no existe.</summary>
        public BE_Equipo? ConsultarPorImei(string imei)
        {
            DataTable tabla = dao_equipo.ConsultarPorImei(imei);
            return tabla.Rows.Count > 0 ? Mapear(tabla.Rows[0]) : null;
        }

        /// <summary>Inserta el equipo y devuelve el id generado.</summary>
        public int Agregar(BE_Equipo equipo) => dao_equipo.Agregar(equipo.Marca, equipo.Modelo, equipo.Imei);

        public void Modificar(BE_Equipo equipo) =>
            dao_equipo.Modificar(equipo.IdEquipo, equipo.Marca, equipo.Modelo, equipo.Imei);

        public void CambiarActivo(int idEquipo, bool activo) => dao_equipo.CambiarActivo(idEquipo, activo);

        public int ContarOT(int idEquipo, IEnumerable<string> estados) => dao_equipo.ContarOT(idEquipo, estados);

        private BE_Equipo Mapear(DataRow fila)
        {
            return new BE_Equipo(
                Convert.ToInt32(fila["idEquipo"]),
                Convert.ToString(fila["marca"]),
                Convert.ToString(fila["modelo"]),
                Convert.ToString(fila["imei"])?.Trim(),
                Convert.ToBoolean(fila["Activo"])
            );
        }
    }
}
