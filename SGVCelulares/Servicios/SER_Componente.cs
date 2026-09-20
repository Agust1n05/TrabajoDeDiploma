namespace Servicio
{
    abstract public class SER_Componente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public SER_Componente(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
        public SER_Componente(string nombre)
        {
            Nombre = nombre;
        }
        public SER_Componente(object[] datos)
        {
            Id = Convert.ToInt16(datos[0]);
            Nombre = Convert.ToString(datos[1]);
        }
        public override string ToString()
        {
            return $"{this.Nombre}";
        }
        public abstract bool TienePermiso(string nombrePermiso);
    }
}
