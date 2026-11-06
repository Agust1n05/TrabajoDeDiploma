namespace Servicio
{
    public class SER_DVV_382QA
    {
        public string NombreTabla { get; set; }
        public string Valor { get; set; }
        public SER_DVV_382QA(string nombreTabla, string valor)
        {
            NombreTabla = nombreTabla;
            Valor = valor;
        }
        public SER_DVV_382QA(object[] datos)
        {
            NombreTabla = datos[0].ToString();
            Valor = datos[1].ToString();
        }
    }
}
