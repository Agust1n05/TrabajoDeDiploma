namespace Servicio
{
    abstract public class SER_Componente_382QA
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public SER_Componente_382QA(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
        public SER_Componente_382QA(string nombre)
        {
            Nombre = nombre;
        }
        public SER_Componente_382QA(object[] datos)
        {
            Id = Convert.ToInt16(datos[0]);
            Nombre = Convert.ToString(datos[1]);
        }
        public override string ToString()
        {
            return $"{this.Nombre}";
        }
        public abstract bool TienePermiso_382QA(string nombrePermiso);
    }
}
