using DAL;
using Servicio;
using System.Data;

namespace MAP
{
    public class MAP_Idioma
    {
        DAO_Idioma dao_idioma;
        public MAP_Idioma() => dao_idioma = new();
        public List<SER_Idioma> ObtenerIdiomasDisponibles()
        {
            var idiomas = new List<SER_Idioma>();
            DataTable tabla = dao_idioma.ObtenerIdiomasDisponibles();
            foreach (DataRow fila in tabla.Rows)
            {
                idiomas.Add(Mapear(fila));
            }
            return idiomas;
        }
        public Dictionary<string, string> ObtenerTraducciones(string codigo) => dao_idioma.ObtenerTraducciones(codigo);
        private SER_Idioma Mapear(DataRow fila) => new SER_Idioma(Convert.ToString(fila[0]), Convert.ToString(fila[1]));
    }
}
