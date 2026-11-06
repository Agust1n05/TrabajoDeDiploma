using MAP.MAP_SER;
using Servicio;

namespace BLL.BLL_SER
{
    public class BLL_Idioma_382QA
    {
        private static readonly List<IObservadorIdioma_382QA> observadores = new List<IObservadorIdioma_382QA>();
        private static SER_Idioma_382QA idiomaActual = new SER_Idioma_382QA("es-AR");
        MAP_Idioma_382QA map_idioma;
        BLL_Bitacora_382QA bll_bitacora;
        public string IdiomaActual => idiomaActual.Codigo;
        public BLL_Idioma_382QA()
        {
            map_idioma = new MAP_Idioma_382QA();  
            bll_bitacora = new BLL_Bitacora_382QA();
        }
        public void Suscribir_382QA(IObservadorIdioma_382QA observador)
        {
            if (!observadores.Contains(observador))
                observadores.Add(observador);
        }
        public void Desuscribir_382QA(IObservadorIdioma_382QA observador)
        {
            observadores.Remove(observador);
        }
        public void CambiarIdioma_382QA(string codigo)
        {
            Dictionary<string, string> textos = map_idioma.ObtenerTraducciones_382QA(codigo);
            idiomaActual = new SER_Idioma_382QA(codigo, textos);
            Notificar_382QA();

            SER_Usuario_382QA usuario = SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario;
            if (usuario != null)
            {
                SER_Bitacora_382QA bitacora = new SER_Bitacora_382QA(usuario, DateTime.Now, "Usuarios", "Cambiar Idioma", 3);
                bll_bitacora.RegistrarBitacora_382QA(bitacora);
            }
        }
        public string Traducir_382QA(string clave)
        {
            return idiomaActual.Textos.TryGetValue(clave, out string valor) ? valor : clave;
        }
        public List<SER_Idioma_382QA> ObtenerIdiomasDisponibles_382QA()
        {
            return map_idioma.ObtenerIdiomasDisponibles_382QA();
        }
        private void Notificar_382QA()
        {
            foreach (IObservadorIdioma_382QA observador in observadores.ToArray())
                observador.ActualizarIdioma_382QA();
        }
    }
}
