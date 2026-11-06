using DotNetEnv;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL.DAL_SER
{
    public class ComandoBD_382QA
    {
        private readonly string cadenaConexion;
        public ComandoBD_382QA(string? catalogoOverride = null)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string envPath = Path.Combine(basePath, ".env");
            Env.Load(envPath);
            string? c = Environment.GetEnvironmentVariable("DB_CONNECTION");
            if (string.IsNullOrEmpty(c)) throw new Exception("No se encontró la cadena de conexión a la base de datos.");
            if (catalogoOverride != null)
            {
                var builder = new SqlConnectionStringBuilder(c) { InitialCatalog = catalogoOverride };
                cadenaConexion = builder.ConnectionString;
            }
            else cadenaConexion = c;
        }
        public DataTable Consultar_382QA(string sql, List<ParametroBD_382QA> parametros = null)
        {
            DataTable dt = new DataTable();
            using SqlConnection con = new SqlConnection(cadenaConexion);
            using SqlCommand cmd = new SqlCommand(sql, con);
            ConfigurarComando_382QA(cmd, parametros);
            con.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            return dt;
        }
        public int Ejecutar_382QA(string sql, List<ParametroBD_382QA> parametros = null)
        {
            using SqlConnection con = new SqlConnection(cadenaConexion);
            using SqlCommand cmd = new SqlCommand(sql, con);
            ConfigurarComando_382QA(cmd, parametros);
            con.Open();
            return cmd.ExecuteNonQuery();
        }
        public object? Escalar_382QA(string sql, List<ParametroBD_382QA>? parametros = null)
        {
            using SqlConnection con = new SqlConnection(cadenaConexion);
            using SqlCommand cmd = new SqlCommand(sql, con);
            ConfigurarComando_382QA(cmd, parametros);
            con.Open();
            return cmd.ExecuteScalar();
        }

        /// <summary>
        /// Ejecuta varias operaciones en una sola transacción: si alguna falla, se deshacen todas.
        /// Se usa cuando un caso de uso escribe en varias tablas a la vez (ej. CU-NEG-4: diagnóstico,
        /// repuestos, presupuesto y estado de la OT).
        /// </summary>
        public T EnTransaccion_382QA<T>(Func<TransaccionBD_382QA, T> operaciones)
        {
            using SqlConnection con = new SqlConnection(cadenaConexion);
            con.Open();
            using SqlTransaction transaccion = con.BeginTransaction();
            try
            {
                T resultado = operaciones(new TransaccionBD_382QA(con, transaccion));
                transaccion.Commit();
                return resultado;
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }

        internal static void ConfigurarComando_382QA(SqlCommand comando, List<ParametroBD_382QA>? parametros)
        {
            if (parametros != null)
            {
                foreach (ParametroBD_382QA p in parametros)
                {
                    comando.Parameters.Add(p.Nombre, p.Tipo).Value = p.Valor ?? DBNull.Value;
                }
            }
        }
    }
}
