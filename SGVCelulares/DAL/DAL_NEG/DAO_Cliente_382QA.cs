using DAL.DAL_SER;
using System.Data;

namespace DAL.DAL_NEG
{
    public class DAO_Cliente_382QA
    {
        ComandoBD_382QA com;
        public DAO_Cliente_382QA() => com = new();

        /// <summary>Clientes activos e inactivos. Con dni vacío trae todos.</summary>
        public DataTable Consultar_382QA(string dni)
        {
            var parametros = new List<ParametroBD_382QA> { new("@dni", SqlDbType.VarChar, dni) };
            return com.Consultar_382QA("SELECT dni, Nombre, Apellido, Telefono, Activo FROM Cliente WHERE (@dni = '' OR dni = @dni)", parametros);
        }

        public void Agregar_382QA(string dni, string nombre, string apellido, string telefono)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@Nombre", SqlDbType.VarChar, nombre),
                new("@Apellido", SqlDbType.VarChar, apellido),
                new("@Telefono", SqlDbType.VarChar, telefono)
            };
            com.Ejecutar_382QA("INSERT INTO Cliente (dni, Nombre, Apellido, Telefono, Activo) VALUES (@dni, @Nombre, @Apellido, @Telefono, 1)", parametros);
        }

        public void Modificar_382QA(string dni, string nombre, string apellido, string telefono)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@Nombre", SqlDbType.VarChar, nombre),
                new("@Apellido", SqlDbType.VarChar, apellido),
                new("@Telefono", SqlDbType.VarChar, telefono)
            };
            com.Ejecutar_382QA("UPDATE Cliente SET Nombre = @Nombre, Apellido = @Apellido, Telefono = @Telefono WHERE dni = @dni", parametros);
        }

        /// <summary>Baja lógica (activo = false) o reactivación (activo = true).</summary>
        public void CambiarActivo_382QA(string dni, bool activo)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@Activo", SqlDbType.Bit, activo)
            };
            com.Ejecutar_382QA("UPDATE Cliente SET Activo = @Activo WHERE dni = @dni", parametros);
        }

        /// <summary>Cantidad de OT del cliente que están en alguno de los estados indicados.</summary>
        public int ContarOT_382QA(string dni, IEnumerable<string> estados)
        {
            var parametros = new List<ParametroBD_382QA> { new("@dni", SqlDbType.VarChar, dni) };
            string filtroEstados = FiltroEstados_382QA.Armar_382QA(estados, parametros);
            object? res = com.Escalar_382QA($"SELECT COUNT(*) FROM OrdenDeTrabajo WHERE dni = @dni AND Estado IN ({filtroEstados})", parametros);
            return Convert.ToInt32(res);
        }
    }
}
