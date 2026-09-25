using System.Data;

namespace DAL
{
    public class DAO_Cliente
    {
        ComandoBD com;
        public DAO_Cliente() => com = new();

        public bool ExisteCliente(string dni)
        {
            var parametros = new List<ParametroBD> { new("@dni", SqlDbType.VarChar, dni) };
            object? res = com.Escalar("SELECT COUNT(*) FROM cliente WHERE dni=@dni", parametros);
            return Convert.ToInt32(res) > 0;
        }

        public DataTable ConsultarPorDni(string dni)
        {
            var parametros = new List<ParametroBD> { new("@dni", SqlDbType.VarChar, dni) };
            return com.Consultar("SELECT dni, Nombre, Apellido, Telefono FROM cliente WHERE dni=@dni", parametros);
        }
    }
}
