using System.Data;

namespace DAL
{
    public class DAO_Idioma
    {
        ComandoBD com;
        private readonly string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Idiomas");
        public DAO_Idioma() => com = new();
        public DataTable ObtenerIdiomasDisponibles() => com.Consultar("SELECT idIdioma, Nombre FROM idioma ORDER BY CASE WHEN idIdioma = 'es-AR' THEN 0 ELSE 1 END, Nombre");
        public Dictionary<string, string> ObtenerTraducciones(string codigo)
        {
            string ruta = Path.Combine(carpeta, codigo + ".json");
            if (!File.Exists(ruta)) throw new Exception("No se encontró el archivo del idioma seleccionado.");
            string contenido = File.ReadAllText(ruta);
            return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(contenido) ?? new Dictionary<string, string>();
        }
    }
}
