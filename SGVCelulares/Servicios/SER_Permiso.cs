namespace Servicio
{
    public class SER_Permiso : SER_Componente
    {
        public SER_Permiso(int id, string nombre) : base(id, nombre)
        {
        }

        public SER_Permiso(object[] datos) : base(Convert.ToInt16(datos[0]), Convert.ToString(datos[1]))
        {
        }
        public override bool TienePermiso(string nombrePermiso)
        {
            return this.Nombre == nombrePermiso;
        }
    }
}
