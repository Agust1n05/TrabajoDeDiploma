using DAL.DAL_SER;
using System.Data;

namespace DAL.DAL_NEG
{
    public class DAO_Tecnico_382QA
    {
        ComandoBD_382QA com;
        public DAO_Tecnico_382QA() => com = new();

        /// <summary>Técnicos activos e inactivos. Con dni vacío trae todos.</summary>
        public DataTable Consultar_382QA(string dni)
        {
            var parametros = new List<ParametroBD_382QA> { new("@dni", SqlDbType.VarChar, dni) };
            return com.Consultar_382QA("SELECT Dni, Nombre, Apellido, Disponible, Activo FROM Tecnico WHERE (@dni = '' OR Dni = @dni)", parametros);
        }

        public void Agregar_382QA(string dni, string nombre, string apellido, bool disponible)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@Dni", SqlDbType.VarChar, dni),
                new("@Nombre", SqlDbType.VarChar, nombre),
                new("@Apellido", SqlDbType.VarChar, apellido),
                new("@Disponible", SqlDbType.Bit, disponible)
            };
            com.Ejecutar_382QA("INSERT INTO Tecnico (Dni, Nombre, Apellido, Disponible, Activo) VALUES (@Dni, @Nombre, @Apellido, @Disponible, 1)", parametros);
        }

        public void Modificar_382QA(string dni, string nombre, string apellido, bool disponible)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@Dni", SqlDbType.VarChar, dni),
                new("@Nombre", SqlDbType.VarChar, nombre),
                new("@Apellido", SqlDbType.VarChar, apellido),
                new("@Disponible", SqlDbType.Bit, disponible)
            };
            com.Ejecutar_382QA("UPDATE Tecnico SET Nombre = @Nombre, Apellido = @Apellido, Disponible = @Disponible WHERE Dni = @Dni", parametros);
        }

        /// <summary>Baja lógica (activo = false) o reactivación (activo = true).</summary>
        public void CambiarActivo_382QA(string dni, bool activo)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@Dni", SqlDbType.VarChar, dni),
                new("@Activo", SqlDbType.Bit, activo)
            };
            com.Ejecutar_382QA("UPDATE Tecnico SET Activo = @Activo WHERE Dni = @Dni", parametros);
        }

        /// <summary>Cantidad de OT asignadas al técnico que están en alguno de los estados indicados.</summary>
        public int ContarOT_382QA(string dni, IEnumerable<string> estados)
        {
            var parametros = new List<ParametroBD_382QA> { new("@dni", SqlDbType.VarChar, dni) };
            string filtroEstados = FiltroEstados_382QA.Armar_382QA(estados, parametros);
            object? res = com.Escalar_382QA($"SELECT COUNT(*) FROM OrdenDeTrabajo WHERE DniTecnico = @dni AND Estado IN ({filtroEstados})", parametros);
            return Convert.ToInt32(res);
        }

        /// <summary>
        /// Técnico activo y disponible con menos OT en los estados indicados (en curso).
        /// Los que no tienen ninguna cuentan como 0. Ante empate, el de menor DNI.
        /// </summary>
        public DataTable ObtenerConMenosOT_382QA(IEnumerable<string> estadosEnCurso)
        {
            var parametros = new List<ParametroBD_382QA>();
            string filtroEstados = FiltroEstados_382QA.Armar_382QA(estadosEnCurso, parametros);
            return com.Consultar_382QA($@"
                SELECT TOP 1 t.Dni, t.Nombre, t.Apellido, t.Disponible, t.Activo
                FROM Tecnico t
                INNER JOIN usuario u ON u.dni = t.Dni  -- el usuario del técnico tiene que poder iniciar sesión
                LEFT JOIN OrdenDeTrabajo ot ON ot.DniTecnico = t.Dni AND ot.Estado IN ({filtroEstados})
                WHERE t.Activo = 1 AND t.Disponible = 1 AND u.activo = 1 AND u.bloqueo = 0
                GROUP BY t.Dni, t.Nombre, t.Apellido, t.Disponible, t.Activo
                ORDER BY COUNT(ot.NroOT) ASC, t.Dni ASC", parametros);
        }
    }
}
