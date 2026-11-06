using System.Data;

namespace DAL.DAL_SER
{
    public class DAO_Bitacora_382QA
    {
        ComandoBD_382QA com;
        public DAO_Bitacora_382QA() => com = new();
        public void Agregar_382QA(string dni, DateTime fecha, string modulo, string evento, int criticidad)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@fecha", SqlDbType.DateTime, fecha),
                new("@modulo", SqlDbType.VarChar, modulo),
                new("@evento", SqlDbType.VarChar, evento),
                new("@criticidad", SqlDbType.TinyInt, criticidad)
            };
            com.Ejecutar_382QA("INSERT INTO bitacora(dni,fecha,modulo,evento,criticidad) values (@dni,@fecha,@modulo,@evento,@criticidad)", parametros);
        }
        public DataTable Consultar_382QA() => com.Consultar_382QA("select * from bitacora");
    }
}
