using System.Data;

namespace DAL.DAL_SER
{
    public class DAO_Idioma_382QA
    {
        ComandoBD_382QA com;
        private readonly string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Idiomas");
        public DAO_Idioma_382QA() => com = new();
        public DataTable ObtenerIdiomasDisponibles_382QA() => com.Consultar_382QA("SELECT idIdioma, Nombre FROM idioma ORDER BY CASE WHEN idIdioma = 'es-AR' THEN 0 ELSE 1 END, Nombre");
        public Dictionary<string, string> ObtenerTraducciones_382QA(string codigo)
        {
            string ruta = Path.Combine(carpeta, codigo + ".json");
            if (!File.Exists(ruta)) throw new Exception("No se encontró el archivo del idioma seleccionado.");
            string contenido = File.ReadAllText(ruta);
            return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(contenido) ?? new Dictionary<string, string>();
        }
    }
}
