namespace Servicio
{
    public class SER_Inconsistencia_382QA
    {
        public string NombreTabla { get; set; }
        public string IdRegistro { get; set; }
        public string Mensaje { get; set; }

        public SER_Inconsistencia_382QA(string nombreTabla, string idRegistro, string mensaje)
        {
            NombreTabla = nombreTabla;
            IdRegistro = idRegistro;
            Mensaje = mensaje;
        }
    }
}
