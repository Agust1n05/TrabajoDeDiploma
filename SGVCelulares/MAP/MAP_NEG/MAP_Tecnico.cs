using BE;
using DAL.DAL_NEG;
using System.Data;

namespace MAP.MAP_NEG
{
    public class MAP_Tecnico
    {
        DAO_Tecnico dao_tecnico;
        public MAP_Tecnico() => dao_tecnico = new();

        /// <summary>Todos los técnicos, activos e inactivos.</summary>
        public List<BE_Tecnico> Consultar()
        {
            var tecnicos = new List<BE_Tecnico>();
            foreach (DataRow fila in dao_tecnico.Consultar("").Rows)
                tecnicos.Add(Mapear(fila));
            return tecnicos;
        }

        /// <summary>Técnico por DNI (activo o inactivo), o null si no existe.</summary>
        public BE_Tecnico? ConsultarPorDni(string dni)
        {
            DataTable tabla = dao_tecnico.Consultar(dni);
            return tabla.Rows.Count > 0 ? Mapear(tabla.Rows[0]) : null;
        }

        public void Agregar(BE_Tecnico tecnico) =>
            dao_tecnico.Agregar(tecnico.Dni, tecnico.Nombre, tecnico.Apellido, tecnico.Disponible);

        public void Modificar(BE_Tecnico tecnico) =>
            dao_tecnico.Modificar(tecnico.Dni, tecnico.Nombre, tecnico.Apellido, tecnico.Disponible);

        public void CambiarActivo(string dni, bool activo) => dao_tecnico.CambiarActivo(dni, activo);

        public int ContarOT(string dni, IEnumerable<string> estados) => dao_tecnico.ContarOT(dni, estados);

        public BE_Tecnico? ObtenerConMenosOT(IEnumerable<string> estadosEnCurso)
        {
            DataTable tabla = dao_tecnico.ObtenerConMenosOT(estadosEnCurso);
            return tabla.Rows.Count > 0 ? Mapear(tabla.Rows[0]) : null;
        }

        private BE_Tecnico Mapear(DataRow fila)
        {
            return new BE_Tecnico(
                Convert.ToString(fila["Dni"]),
                Convert.ToString(fila["Nombre"]),
                Convert.ToString(fila["Apellido"]),
                Convert.ToBoolean(fila["Disponible"]),
                Convert.ToBoolean(fila["Activo"])
            );
        }
    }
}
