using System.Data;

namespace DAL.DAL_SER
{
    public class DAO_Permiso
    {
        ComandoBD com;
        public DAO_Permiso() => com = new();
        public DataTable Consultar() => com.Consultar("SELECT * FROM permiso");
    }
}
