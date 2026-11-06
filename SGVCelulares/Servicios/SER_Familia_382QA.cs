namespace Servicio
{
    public class SER_Familia_382QA : SER_Componente_382QA
    {
        public List<SER_Componente_382QA> Componentes { get; set; }
        public SER_Familia_382QA(int id, string nombre) : base(id, nombre)
        {
            Componentes = new List<SER_Componente_382QA>();
        }

        public SER_Familia_382QA(string nombre) : base(nombre)
        {
            Nombre = nombre;
            Componentes = new List<SER_Componente_382QA>();
        }
        public SER_Familia_382QA(object[] datos) : base(Convert.ToInt16(datos[0]), Convert.ToString(datos[1]))
        {
            Componentes = new List<SER_Componente_382QA>();
        }
        public override bool TienePermiso_382QA(string nombrePermiso)
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
