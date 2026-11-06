namespace Servicio
{
    public class SER_Permiso_382QA : SER_Componente_382QA
    {
        public SER_Permiso_382QA(int id, string nombre) : base(id, nombre)
        {
        }

        public SER_Permiso_382QA(object[] datos) : base(Convert.ToInt16(datos[0]), Convert.ToString(datos[1]))
        {
        }
        public override bool TienePermiso_382QA(string nombrePermiso)
        {
            return this.Nombre == nombrePermiso;
        }
    }
}
