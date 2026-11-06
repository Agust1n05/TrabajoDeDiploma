using Microsoft.Data.SqlClient;

namespace DAL.DAL_SER
{
    /// <summary>
    /// Conexión con una transacción abierta. Solo se obtiene dentro de ComandoBD_382QA.EnTransaccion:
    /// todos los comandos que se ejecutan con ella se confirman o se deshacen juntos.
    /// </summary>
    public class TransaccionBD_382QA
    {
        private readonly SqlConnection conexion;
        private readonly SqlTransaction transaccion;

        internal TransaccionBD_382QA(SqlConnection conexion, SqlTransaction transaccion)
        {
            this.conexion = conexion;
            this.transaccion = transaccion;
        }

        public int Ejecutar_382QA(string sql, List<ParametroBD_382QA>? parametros = null)
        {
            using SqlCommand cmd = new SqlCommand(sql, conexion, transaccion);
            ComandoBD_382QA.ConfigurarComando_382QA(cmd, parametros);
            return cmd.ExecuteNonQuery();
        }

        public object? Escalar_382QA(string sql, List<ParametroBD_382QA>? parametros = null)
        {
            using SqlCommand cmd = new SqlCommand(sql, conexion, transaccion);
            ComandoBD_382QA.ConfigurarComando_382QA(cmd, parametros);
            return cmd.ExecuteScalar();
        }
    }
}
