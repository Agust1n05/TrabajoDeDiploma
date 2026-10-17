using DAL.DAL_SER;
using System.Data;

namespace DAL.DAL_NEG
{
    public class DAO_Cliente
    {
        ComandoBD com;
        public DAO_Cliente() => com = new();

        /// <summary>Clientes activos e inactivos. Con dni vacío trae todos.</summary>
        public DataTable Consultar(string dni)
        {
            var parametros = new List<ParametroBD> { new("@dni", SqlDbType.VarChar, dni) };
            return com.Consultar("SELECT dni, Nombre, Apellido, Telefono, Activo FROM Cliente WHERE (@dni = '' OR dni = @dni)", parametros);
        }

        public void Agregar(string dni, string nombre, string apellido, string telefono)
        {
            var parametros = new List<ParametroBD>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@Nombre", SqlDbType.VarChar, nombre),
                new("@Apellido", SqlDbType.VarChar, apellido),
                new("@Telefono", SqlDbType.VarChar, telefono)
            };
            com.Ejecutar("INSERT INTO Cliente (dni, Nombre, Apellido, Telefono, Activo) VALUES (@dni, @Nombre, @Apellido, @Telefono, 1)", parametros);
        }

        public void Modificar(string dni, string nombre, string apellido, string telefono)
        {
            var parametros = new List<ParametroBD>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@Nombre", SqlDbType.VarChar, nombre),
                new("@Apellido", SqlDbType.VarChar, apellido),
                new("@Telefono", SqlDbType.VarChar, telefono)
            };
            com.Ejecutar("UPDATE Cliente SET Nombre = @Nombre, Apellido = @Apellido, Telefono = @Telefono WHERE dni = @dni", parametros);
        }

        /// <summary>Baja lógica (activo = false) o reactivación (activo = true).</summary>
        public void CambiarActivo(string dni, bool activo)
        {
            var parametros = new List<ParametroBD>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@Activo", SqlDbType.Bit, activo)
            };
            com.Ejecutar("UPDATE Cliente SET Activo = @Activo WHERE dni = @dni", parametros);
        }

        /// <summary>Cantidad de OT del cliente que están en alguno de los estados indicados.</summary>
        public int ContarOT(string dni, IEnumerable<string> estados)
        {
            var parametros = new List<ParametroBD> { new("@dni", SqlDbType.VarChar, dni) };
            string filtroEstados = FiltroEstados.Armar(estados, parametros);
            object? res = com.Escalar($"SELECT COUNT(*) FROM OrdenDeTrabajo WHERE dni = @dni AND Estado IN ({filtroEstados})", parametros);
            return Convert.ToInt32(res);
        }
    }
}
