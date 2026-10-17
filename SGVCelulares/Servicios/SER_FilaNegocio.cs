namespace Servicio
{
    /// <summary>
    /// Fila de una tabla de negocio tal como está guardada en la base, con sus columnas en orden.
    /// Se usa para calcular el dígito verificador horizontal de las tablas de negocio:
    /// todos los atributos del registro, concatenados en el orden de las columnas de la tabla.
    /// </summary>
    public class SER_FilaNegocio
    {
        public string[] Columnas { get; }
        public object?[] Valores { get; }

        public SER_FilaNegocio(string[] columnas, object?[] valores)
        {
            Columnas = columnas;
            Valores = valores;
        }

        public object? this[string columna]
        {
            get
            {
                int indice = Array.FindIndex(Columnas, c => string.Equals(c, columna, StringComparison.OrdinalIgnoreCase));
                if (indice < 0) throw new ArgumentException($"La columna '{columna}' no existe en la fila.");
                return Valores[indice];
            }
        }
    }
}
