using DAL.DAL_SER;
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

       public DataTable Consultar(string dni)
        {
            var parametros = new List<ParametroBD> { new("@dni", SqlDbType.VarChar, dni) };
            return com.Consultar("SELECT * FROM cliente WHERE (@dni = '' OR dni=@dni)", parametros);
        }
        public void Agregar(string dni, string nombre, string apellido, string telefono)
        {
            var parametros = new List<ParametroBD>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@Nombre", SqlDbType.VarChar, nombre),
                new("@Apellido", SqlDbType.VarChar, apellido),
                new("@Telefono", SqlDbType.VarChar, telefono)
            };
            com.Ejecutar("INSERT INTO cliente (dni, Nombre, Apellido, Telefono) VALUES (@dni, @Nombre, @Apellido, @Telefono)", parametros);
        }
        public void Borrar(string dni)
        {
            var parametros = new List<ParametroBD> { new("@dni", SqlDbType.VarChar, dni) };
            com.Ejecutar("DELETE FROM cliente WHERE dni=@dni", parametros);
        }
        public void Modificar(string dni, string nombre, string apellido, string telefono)
        {
            var parametros = new List<ParametroBD>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@Nombre", SqlDbType.VarChar, nombre),
                new("@Apellido", SqlDbType.VarChar, apellido),
                new("@Telefono", SqlDbType.VarChar, telefono)
            };
            com.Ejecutar("UPDATE cliente SET Nombre=@Nombre, Apellido=@Apellido, Telefono=@Telefono WHERE dni=@dni", parametros);
        }
    }
}
