namespace Servicio
{
    public class SER_DVH_382QA
    {
        public string NombreTabla { get; set; }
        public string IdRegistro { get; set; }
        public string Valor { get; set; }
        public SER_DVH_382QA(string nombreTabla, string idRegistro, string valor)
        {
            NombreTabla = nombreTabla;
            IdRegistro = idRegistro;
            Valor = valor;
        }
        public SER_DVH_382QA(object[] datos)
        {
            NombreTabla = datos[0].ToString();
            IdRegistro = datos[1].ToString();
            Valor = datos[2].ToString();
        }
    }
}
