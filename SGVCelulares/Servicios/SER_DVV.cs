namespace Servicio
{
    public class SER_DVV
    {
        public string NombreTabla { get; set; }
        public string Valor { get; set; }
        public SER_DVV(string nombreTabla, string valor)
        {
            NombreTabla = nombreTabla;
            Valor = valor;
        }
        public SER_DVV(object[] datos)
        {
            NombreTabla = datos[0].ToString();
            Valor = datos[1].ToString();
        }
    }
}
