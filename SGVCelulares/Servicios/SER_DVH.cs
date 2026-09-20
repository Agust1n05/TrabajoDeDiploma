namespace Servicio
{
    public class SER_DVH
    {
        public string NombreTabla { get; set; }
        public string IdRegistro { get; set; }
        public string Valor { get; set; }
        public SER_DVH(string nombreTabla, string idRegistro, string valor)
        {
            NombreTabla = nombreTabla;
            IdRegistro = idRegistro;
            Valor = valor;
        }
        public SER_DVH(object[] datos)
        {
            NombreTabla = datos[0].ToString();
            IdRegistro = datos[1].ToString();
            Valor = datos[2].ToString();
        }
    }
}
