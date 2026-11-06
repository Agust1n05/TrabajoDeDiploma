using System.Data;

namespace DAL.DAL_SER
{
    public class DAO_Permiso_382QA
    {
        ComandoBD_382QA com;
        public DAO_Permiso_382QA() => com = new();
        public DataTable Consultar_382QA() => com.Consultar_382QA("SELECT * FROM permiso");
    }
}
