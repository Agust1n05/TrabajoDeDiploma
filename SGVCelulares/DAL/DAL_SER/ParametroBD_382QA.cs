using System.Data;

namespace DAL.DAL_SER
{
    public class ParametroBD_382QA
    {
        public string Nombre { get; set; }
        public SqlDbType Tipo { get; set; }
        public object Valor { get; set; }
        public ParametroBD_382QA(string nombre, SqlDbType tipo, object valor)
        {
            Nombre = nombre;
            Tipo = tipo;
            Valor = valor;
        }
    }
}
