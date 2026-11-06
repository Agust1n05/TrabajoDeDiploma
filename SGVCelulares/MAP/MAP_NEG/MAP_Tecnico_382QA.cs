using BE;
using DAL.DAL_NEG;
using System.Data;

namespace MAP.MAP_NEG
{
    public class MAP_Tecnico_382QA
    {
        DAO_Tecnico_382QA dao_tecnico;
        public MAP_Tecnico_382QA() => dao_tecnico = new();

        /// <summary>Todos los técnicos, activos e inactivos.</summary>
        public List<BE_Tecnico_382QA> Consultar_382QA()
        {
            var tecnicos = new List<BE_Tecnico_382QA>();
            foreach (DataRow fila in dao_tecnico.Consultar_382QA("").Rows)
                tecnicos.Add(Mapear_382QA(fila));
            return tecnicos;
        }

        /// <summary>Técnico por DNI (activo o inactivo), o null si no existe.</summary>
        public BE_Tecnico_382QA? ConsultarPorDni_382QA(string dni)
        {
            DataTable tabla = dao_tecnico.Consultar_382QA(dni);
            return tabla.Rows.Count > 0 ? Mapear_382QA(tabla.Rows[0]) : null;
        }

        public void Agregar_382QA(BE_Tecnico_382QA tecnico) =>
            dao_tecnico.Agregar_382QA(tecnico.Dni, tecnico.Nombre, tecnico.Apellido, tecnico.Disponible);

        public void Modificar_382QA(BE_Tecnico_382QA tecnico) =>
            dao_tecnico.Modificar_382QA(tecnico.Dni, tecnico.Nombre, tecnico.Apellido, tecnico.Disponible);

        public void CambiarActivo_382QA(string dni, bool activo) => dao_tecnico.CambiarActivo_382QA(dni, activo);

        public int ContarOT_382QA(string dni, IEnumerable<string> estados) => dao_tecnico.ContarOT_382QA(dni, estados);

        public BE_Tecnico_382QA? ObtenerConMenosOT_382QA(IEnumerable<string> estadosEnCurso)
        {
            DataTable tabla = dao_tecnico.ObtenerConMenosOT_382QA(estadosEnCurso);
            return tabla.Rows.Count > 0 ? Mapear_382QA(tabla.Rows[0]) : null;
        }

        private BE_Tecnico_382QA Mapear_382QA(DataRow fila)
        {
            return new BE_Tecnico_382QA(
                Convert.ToString(fila["Dni"]),
                Convert.ToString(fila["Nombre"]),
                Convert.ToString(fila["Apellido"]),
                Convert.ToBoolean(fila["Disponible"]),
                Convert.ToBoolean(fila["Activo"])
            );
        }
    }
}
