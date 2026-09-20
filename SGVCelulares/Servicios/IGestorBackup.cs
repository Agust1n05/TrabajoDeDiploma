namespace Servicio
{
    public interface IGestorBackup
    {
        void Backup(string ruta);
        void Restore(string ruta);
    }
}
