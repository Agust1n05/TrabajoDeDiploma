using System.Data;

namespace DAL
{
    public class DAO_Bitacora
    {
        ComandoBD com;
        public DAO_Bitacora() => com = new();
        public void Agregar(string dni, DateTime fecha, string modulo, string evento, int criticidad)
        {
            var parametros = new List<ParametroBD>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@fecha", SqlDbType.DateTime, fecha),
                new("@modulo", SqlDbType.VarChar, modulo),
                new("@evento", SqlDbType.VarChar, evento),
                new("@criticidad", SqlDbType.TinyInt, criticidad)
            };
            com.Ejecutar("INSERT INTO bitacora(dni,fecha,modulo,evento,criticidad) values (@dni,@fecha,@modulo,@evento,@criticidad)", parametros);
        }
        public DataTable Consultar() => com.Consultar("select * from bitacora");
    }
}
