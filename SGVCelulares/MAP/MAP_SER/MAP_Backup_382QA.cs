using DAL.DAL_SER;
using Servicio;

namespace MAP.MAP_SER
{
    public class MAP_Backup_382QA : IGestorBackup_382QA
    {
        DAO_Backup_382QA dao_backup;
        public MAP_Backup_382QA() => dao_backup = new();
        public void Backup_382QA(string ruta) => dao_backup.Backup_382QA(ruta);
        public void Restore_382QA(string ruta) => dao_backup.Restore_382QA(ruta);
    }
}
