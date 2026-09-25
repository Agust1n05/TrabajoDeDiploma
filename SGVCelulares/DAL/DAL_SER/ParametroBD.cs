using System.Data;

namespace DAL.DAL_SER
{
    public class ParametroBD
    {
        public string Nombre { get; set; }
        public SqlDbType Tipo { get; set; }
        public object Valor { get; set; }
        public ParametroBD(string nombre, SqlDbType tipo, object valor)
        {
            Nombre = nombre;
            Tipo = tipo;
            Valor = valor;
        }
    }
}
