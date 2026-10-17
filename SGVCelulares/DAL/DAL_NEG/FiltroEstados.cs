using DAL.DAL_SER;
using System.Data;

namespace DAL.DAL_NEG
{
    /// <summary>
    /// Arma la lista de parámetros de un "Estado IN (...)" para filtrar órdenes de trabajo por estado.
    /// Ejemplo: Armar(["Recibida", "Presupuestado"], parametros) devuelve "@estado0, @estado1"
    /// y agrega esos dos parámetros a la lista.
    /// </summary>
    internal static class FiltroEstados
    {
        public static string Armar(IEnumerable<string> estados, List<ParametroBD> parametros)
        {
            var nombres = new List<string>();
            int i = 0;
            foreach (string estado in estados)
            {
                string nombre = $"@estado{i++}";
                nombres.Add(nombre);
                parametros.Add(new ParametroBD(nombre, SqlDbType.VarChar, estado));
            }
            if (nombres.Count == 0) throw new ArgumentException("Debe indicarse al menos un estado.");
            return string.Join(", ", nombres);
        }
    }
}
