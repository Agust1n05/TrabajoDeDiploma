using System.Data;

namespace DAL.DAL_SER
{
    public class DAO_Backup_382QA
    {
        ComandoBD_382QA com;
        public DAO_Backup_382QA() => com = new("master");
        public void Backup_382QA(string ruta)
        {
            var parametros = new List<ParametroBD_382QA> { new("@ruta", SqlDbType.VarChar, ruta) };
            com.Ejecutar_382QA("BACKUP DATABASE bd_sgvcelulares TO DISK = @ruta WITH INIT", parametros);
        }

        public void Restore_382QA(string ruta)
        {
            var parametros = new List<ParametroBD_382QA> { new("@ruta", SqlDbType.VarChar, ruta) };
            com.Ejecutar_382QA("ALTER DATABASE bd_sgvcelulares SET SINGLE_USER WITH ROLLBACK IMMEDIATE; RESTORE DATABASE bd_sgvcelulares FROM DISK = @ruta WITH REPLACE; ALTER DATABASE bd_sgvcelulares SET MULTI_USER", parametros);
        }
    }
}
