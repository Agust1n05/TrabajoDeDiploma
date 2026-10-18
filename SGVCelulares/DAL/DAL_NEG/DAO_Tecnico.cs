using DAL.DAL_SER;
using System.Data;

namespace DAL.DAL_NEG
{
    public class DAO_Tecnico
    {
        ComandoBD com;
        public DAO_Tecnico() => com = new();

        /// <summary>Técnicos activos e inactivos. Con dni vacío trae todos.</summary>
        public DataTable Consultar(string dni)
        {
            var parametros = new List<ParametroBD> { new("@dni", SqlDbType.VarChar, dni) };
            return com.Consultar("SELECT Dni, Nombre, Apellido, Disponible, Activo FROM Tecnico WHERE (@dni = '' OR Dni = @dni)", parametros);
        }

        public void Agregar(string dni, string nombre, string apellido, bool disponible)
        {
            var parametros = new List<ParametroBD>
            {
                new("@Dni", SqlDbType.VarChar, dni),
                new("@Nombre", SqlDbType.VarChar, nombre),
                new("@Apellido", SqlDbType.VarChar, apellido),
                new("@Disponible", SqlDbType.Bit, disponible)
            };
            com.Ejecutar("INSERT INTO Tecnico (Dni, Nombre, Apellido, Disponible, Activo) VALUES (@Dni, @Nombre, @Apellido, @Disponible, 1)", parametros);
        }

        public void Modificar(string dni, string nombre, string apellido, bool disponible)
        {
            var parametros = new List<ParametroBD>
            {
                new("@Dni", SqlDbType.VarChar, dni),
                new("@Nombre", SqlDbType.VarChar, nombre),
                new("@Apellido", SqlDbType.VarChar, apellido),
                new("@Disponible", SqlDbType.Bit, disponible)
            };
            com.Ejecutar("UPDATE Tecnico SET Nombre = @Nombre, Apellido = @Apellido, Disponible = @Disponible WHERE Dni = @Dni", parametros);
        }

        /// <summary>Baja lógica (activo = false) o reactivación (activo = true).</summary>
        public void CambiarActivo(string dni, bool activo)
        {
            var parametros = new List<ParametroBD>
            {
                new("@Dni", SqlDbType.VarChar, dni),
                new("@Activo", SqlDbType.Bit, activo)
            };
            com.Ejecutar("UPDATE Tecnico SET Activo = @Activo WHERE Dni = @Dni", parametros);
        }

        /// <summary>Cantidad de OT asignadas al técnico que están en alguno de los estados indicados.</summary>
        public int ContarOT(string dni, IEnumerable<string> estados)
        {
            var parametros = new List<ParametroBD> { new("@dni", SqlDbType.VarChar, dni) };
            string filtroEstados = FiltroEstados.Armar(estados, parametros);
            object? res = com.Escalar($"SELECT COUNT(*) FROM OrdenDeTrabajo WHERE DniTecnico = @dni AND Estado IN ({filtroEstados})", parametros);
            return Convert.ToInt32(res);
        }

        /// <summary>
        /// Técnico activo y disponible con menos OT en los estados indicados (en curso).
        /// Los que no tienen ninguna cuentan como 0. Ante empate, el de menor DNI.
        /// </summary>
        public DataTable ObtenerConMenosOT(IEnumerable<string> estadosEnCurso)
        {
            var parametros = new List<ParametroBD>();
            string filtroEstados = FiltroEstados.Armar(estadosEnCurso, parametros);
            return com.Consultar($@"
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
