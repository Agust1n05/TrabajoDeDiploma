namespace Servicio
{
    public class SER_Familia : SER_Componente
    {
        public List<SER_Componente> Componentes { get; set; }
        public SER_Familia(int id, string nombre) : base(id, nombre)
        {
            Componentes = new List<SER_Componente>();
        }

        public SER_Familia(string nombre) : base(nombre)
        {
            Nombre = nombre;
            Componentes = new List<SER_Componente>();
        }
        public SER_Familia(object[] datos) : base(Convert.ToInt16(datos[0]), Convert.ToString(datos[1]))
        {
            Componentes = new List<SER_Componente>();
        }
        public override bool TienePermiso(string nombrePermiso)
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
