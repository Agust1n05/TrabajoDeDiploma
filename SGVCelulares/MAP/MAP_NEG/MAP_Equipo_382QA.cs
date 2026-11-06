using BE;
using DAL.DAL_NEG;
using System.Data;

namespace MAP.MAP_NEG
{
    public class MAP_Equipo_382QA
    {
        DAO_Equipo_382QA dao_equipo;
        public MAP_Equipo_382QA() => dao_equipo = new();

        /// <summary>Todos los equipos, activos e inactivos.</summary>
        public List<BE_Equipo_382QA> Consultar_382QA()
        {
            var equipos = new List<BE_Equipo_382QA>();
            foreach (DataRow fila in dao_equipo.Consultar_382QA().Rows)
                equipos.Add(Mapear_382QA(fila));
            return equipos;
        }

        public BE_Equipo_382QA? ConsultarPorId_382QA(int idEquipo)
        {
            DataTable tabla = dao_equipo.ConsultarPorId_382QA(idEquipo);
            return tabla.Rows.Count > 0 ? Mapear_382QA(tabla.Rows[0]) : null;
        }

        /// <summary>Equipo por IMEI (activo o inactivo), o null si no existe.</summary>
        public BE_Equipo_382QA? ConsultarPorImei_382QA(string imei)
        {
            DataTable tabla = dao_equipo.ConsultarPorImei_382QA(imei);
            return tabla.Rows.Count > 0 ? Mapear_382QA(tabla.Rows[0]) : null;
        }

        /// <summary>Inserta el equipo y devuelve el id generado.</summary>
        public int Agregar_382QA(BE_Equipo_382QA equipo) => dao_equipo.Agregar_382QA(equipo.Marca, equipo.Modelo, equipo.Imei);

        public void Modificar_382QA(BE_Equipo_382QA equipo) =>
            dao_equipo.Modificar_382QA(equipo.IdEquipo, equipo.Marca, equipo.Modelo, equipo.Imei);

        public void CambiarActivo_382QA(int idEquipo, bool activo) => dao_equipo.CambiarActivo_382QA(idEquipo, activo);

        public int ContarOT_382QA(int idEquipo, IEnumerable<string> estados) => dao_equipo.ContarOT_382QA(idEquipo, estados);

        private BE_Equipo_382QA Mapear_382QA(DataRow fila)
        {
            return new BE_Equipo_382QA(
                Convert.ToInt32(fila["idEquipo"]),
                Convert.ToString(fila["marca"]),
                Convert.ToString(fila["modelo"]),
                Convert.ToString(fila["imei"])?.Trim(),
                Convert.ToBoolean(fila["Activo"])
            );
        }
    }
}
