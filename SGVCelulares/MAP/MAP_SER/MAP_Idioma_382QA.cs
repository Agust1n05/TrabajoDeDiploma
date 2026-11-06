using DAL.DAL_SER;
using Servicio;
using System.Data;

namespace MAP.MAP_SER
{
    public class MAP_Idioma_382QA
    {
        DAO_Idioma_382QA dao_idioma;
        public MAP_Idioma_382QA() => dao_idioma = new();
        public List<SER_Idioma_382QA> ObtenerIdiomasDisponibles_382QA()
        {
            var idiomas = new List<SER_Idioma_382QA>();
            DataTable tabla = dao_idioma.ObtenerIdiomasDisponibles_382QA();
            foreach (DataRow fila in tabla.Rows)
            {
                idiomas.Add(Mapear_382QA(fila));
            }
            return idiomas;
        }
        public Dictionary<string, string> ObtenerTraducciones_382QA(string codigo) => dao_idioma.ObtenerTraducciones_382QA(codigo);
        private SER_Idioma_382QA Mapear_382QA(DataRow fila) => new SER_Idioma_382QA(Convert.ToString(fila[0]), Convert.ToString(fila[1]));
    }
}
