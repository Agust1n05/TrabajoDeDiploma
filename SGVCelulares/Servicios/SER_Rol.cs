namespace Servicio
{
    public class SER_Rol 
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<SER_Componente> Componentes { get; set; }
        public SER_Rol(int id, string nombre) 
        {
            Id = id;
            Nombre = nombre;
            Componentes = new List<SER_Componente>();
        }
        public SER_Rol(string nombre)
        {
            Nombre = nombre;
            Componentes = new List<SER_Componente>();
        }
        public SER_Rol(object[] datos)
        {
            Id = Convert.ToInt16(datos[0]);
            Nombre = Convert.ToString(datos[1]);
            Componentes = new List<SER_Componente>();
        }
        public override string ToString()
        {
            return $"{this.Nombre}";
        }
        public bool TienePermiso(string nombrePermiso)
        {
            foreach (SER_Componente hijo in Componentes)
            {
                if (hijo.TienePermiso(nombrePermiso))
                    return true;
            }
            return false;
        }
    }

}
