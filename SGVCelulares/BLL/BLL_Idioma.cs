using MAP;
using Servicio;

namespace BLL
{
    public class BLL_Idioma
    {
        private static readonly List<IObservadorIdioma> observadores = new List<IObservadorIdioma>();
        private static SER_Idioma idiomaActual = new SER_Idioma("es-AR");
        MAP_Idioma map_idioma;
        BLL_Bitacora bll_bitacora;
        public string IdiomaActual => idiomaActual.Codigo;
        public BLL_Idioma()
        {
            map_idioma = new MAP_Idioma();  
            bll_bitacora = new BLL_Bitacora();
        }
        public void Suscribir(IObservadorIdioma observador)
        {
            if (!observadores.Contains(observador))
                observadores.Add(observador);
        }
        public void Desuscribir(IObservadorIdioma observador)
        {
            observadores.Remove(observador);
        }
        public void CambiarIdioma(string codigo)
        {
            Dictionary<string, string> textos = map_idioma.ObtenerTraducciones(codigo);
            idiomaActual = new SER_Idioma(codigo, textos);
            Notificar();

            SER_Usuario usuario = SER_SesionManager.ObtenerSesion().Usuario;
            if (usuario != null)
            {
                SER_Bitacora bitacora = new SER_Bitacora(usuario, DateTime.Now, "Usuarios", "Cambiar Idioma", 3);
                bll_bitacora.RegistrarBitacora(bitacora);
            }
        }
        public string Traducir(string clave)
        {
            return idiomaActual.Textos.TryGetValue(clave, out string valor) ? valor : clave;
        }
        public List<SER_Idioma> ObtenerIdiomasDisponibles()
        {
            return map_idioma.ObtenerIdiomasDisponibles();
        }
        private void Notificar()
        {
            foreach (IObservadorIdioma observador in observadores.ToArray())
                observador.ActualizarIdioma();
        }
    }
}
