namespace Servicio
{
    public class SER_Rol_382QA 
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<SER_Componente_382QA> Componentes { get; set; }
        public SER_Rol_382QA(int id, string nombre) 
        {
            Id = id;
            Nombre = nombre;
            Componentes = new List<SER_Componente_382QA>();
        }
        public SER_Rol_382QA(string nombre)
        {
            Nombre = nombre;
            Componentes = new List<SER_Componente_382QA>();
        }
        public SER_Rol_382QA(object[] datos)
        {
            Id = Convert.ToInt16(datos[0]);
            Nombre = Convert.ToString(datos[1]);
            Componentes = new List<SER_Componente_382QA>();
        }
        public override string ToString()
        {
            return $"{this.Nombre}";
        }
        public bool TienePermiso_382QA(string nombrePermiso)
        {
            foreach (SER_Componente_382QA hijo in Componentes)
            {
                if (hijo.TienePermiso_382QA(nombrePermiso))
                    return true;
            }
            return false;
        }
    }

}
