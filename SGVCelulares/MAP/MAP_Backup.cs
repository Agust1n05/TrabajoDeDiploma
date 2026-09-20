using DAL;
using Servicio;

namespace MAP
{
    public class MAP_Backup : IGestorBackup
    {
        DAO_Backup dao_backup;
        public MAP_Backup() => dao_backup = new();
        public void Backup(string ruta) => dao_backup.Backup(ruta);
        public void Restore(string ruta) => dao_backup.Restore(ruta);
    }
}
