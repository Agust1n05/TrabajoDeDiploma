using System.Data;

namespace DAL
{
    public class DAO_Backup
    {
        ComandoBD com;
        public DAO_Backup() => com = new("master");
        public void Backup(string ruta)
        {
            var parametros = new List<ParametroBD> { new("@ruta", SqlDbType.VarChar, ruta) };
            com.Ejecutar("BACKUP DATABASE bd_sgvcelulares TO DISK = @ruta WITH INIT", parametros);
        }

        public void Restore(string ruta)
        {
            var parametros = new List<ParametroBD> { new("@ruta", SqlDbType.VarChar, ruta) };
            com.Ejecutar("ALTER DATABASE bd_sgvcelulares SET SINGLE_USER WITH ROLLBACK IMMEDIATE; RESTORE DATABASE bd_sgvcelulares FROM DISK = @ruta WITH REPLACE; ALTER DATABASE bd_sgvcelulares SET MULTI_USER", parametros);
        }
    }
}
