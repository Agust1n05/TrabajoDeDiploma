using MAP;
using Servicio;

namespace BLL
{
    public class BLL_Backup : IGestorBackup
    {
        MAP_Backup map_backup;
        BLL_Bitacora bll_bitacora;
        public BLL_Backup() { map_backup = new(); bll_bitacora = new(); }
        public void Backup(string ruta)
        {
            map_backup.Backup(ruta);
            SER_Bitacora bitacora = new SER_Bitacora(SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, "Respaldo", "Generar backup", 1);
            bll_bitacora.RegistrarBitacora(bitacora);
        }
        public void Restore(string ruta)
        {
            map_backup.Restore(ruta);
            SER_Bitacora bitacora = new SER_Bitacora(SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, "Respaldo", "Generar restore", 1);
            bll_bitacora.RegistrarBitacora(bitacora);
        }
    }
}
