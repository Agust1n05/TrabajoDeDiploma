namespace Servicio
{
    public class SER_SesionManager_382QA
    {
        public SER_Usuario_382QA Usuario { get; set; }
        private static SER_SesionManager_382QA sesion;
        private SER_SesionManager_382QA() { }
        static public SER_SesionManager_382QA ObtenerSesion_382QA()
        {
            if (sesion == null)
            {
                sesion = new SER_SesionManager_382QA();
            }
            return sesion;
        }
        static public void CerrarSesion_382QA() => sesion = null;
    }
}
