using Microsoft.Data.SqlClient;

namespace DAL.DAL_SER
{
    /// <summary>
    /// Conexión con una transacción abierta. Solo se obtiene dentro de ComandoBD.EnTransaccion:
    /// todos los comandos que se ejecutan con ella se confirman o se deshacen juntos.
    /// </summary>
    public class TransaccionBD
    {
        private readonly SqlConnection conexion;
        private readonly SqlTransaction transaccion;

        internal TransaccionBD(SqlConnection conexion, SqlTransaction transaccion)
        {
            this.conexion = conexion;
            this.transaccion = transaccion;
        }

        public int Ejecutar(string sql, List<ParametroBD>? parametros = null)
        {
            using SqlCommand cmd = new SqlCommand(sql, conexion, transaccion);
            ComandoBD.ConfigurarComando(cmd, parametros);
            return cmd.ExecuteNonQuery();
        }

        public object? Escalar(string sql, List<ParametroBD>? parametros = null)
        {
            using SqlCommand cmd = new SqlCommand(sql, conexion, transaccion);
            ComandoBD.ConfigurarComando(cmd, parametros);
            return cmd.ExecuteScalar();
        }
    }
}
