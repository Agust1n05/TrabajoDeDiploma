using DotNetEnv;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class ComandoBD
    {
        private readonly string cadenaConexion;
        public ComandoBD(string? catalogoOverride = null)
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
        public DataTable Consultar(string sql, List<ParametroBD> parametros = null)
        {
            DataTable dt = new DataTable();
            using SqlConnection con = new SqlConnection(cadenaConexion);
            using SqlCommand cmd = new SqlCommand(sql, con);
            ConfigurarComando(cmd, parametros);
            con.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            return dt;
        }
        public int Ejecutar(string sql, List<ParametroBD> parametros = null)
        {
            using SqlConnection con = new SqlConnection(cadenaConexion);
            using SqlCommand cmd = new SqlCommand(sql, con);
            ConfigurarComando(cmd, parametros);
            con.Open();
            return cmd.ExecuteNonQuery();
        }
        public object? Escalar(string sql, List<ParametroBD>? parametros = null)
        {
            using SqlConnection con = new SqlConnection(cadenaConexion);
            using SqlCommand cmd = new SqlCommand(sql, con);
            ConfigurarComando(cmd, parametros);
            con.Open();
            return cmd.ExecuteScalar();
        }
        private void ConfigurarComando(SqlCommand comando, List<ParametroBD>? parametros)
        {
            if (parametros != null)
            {
                foreach (ParametroBD p in parametros)
                {
                    comando.Parameters.Add(p.Nombre, p.Tipo).Value = p.Valor;
                }
            }
        }
    }
}
