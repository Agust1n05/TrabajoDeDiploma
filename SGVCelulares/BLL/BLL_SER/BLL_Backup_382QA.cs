using MAP.MAP_SER;
using Servicio;

namespace BLL.BLL_SER
{
    public class BLL_Backup_382QA : IGestorBackup_382QA
    {
        MAP_Backup_382QA map_backup;
        BLL_Bitacora_382QA bll_bitacora;
        public BLL_Backup_382QA() { map_backup = new(); bll_bitacora = new(); }
        public void Backup_382QA(string ruta)
        {
            map_backup.Backup_382QA(ruta);
            SER_Bitacora_382QA bitacora = new SER_Bitacora_382QA(SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, "Respaldo", "Generar backup", 1);
            bll_bitacora.RegistrarBitacora_382QA(bitacora);
        }
        public void Restore_382QA(string ruta)
        {
            map_backup.Restore_382QA(ruta);
            SER_Bitacora_382QA bitacora = new SER_Bitacora_382QA(SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, "Respaldo", "Generar restore", 1);
            bll_bitacora.RegistrarBitacora_382QA(bitacora);
        }
    }
}
