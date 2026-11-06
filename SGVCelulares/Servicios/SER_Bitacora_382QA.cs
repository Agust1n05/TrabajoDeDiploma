namespace Servicio
{
    public class SER_Bitacora_382QA
    {
        public SER_Usuario_382QA Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Evento { get; set; }
        public string Modulo { get; set; }
        public int Criticidad { get; set; }
        public SER_Bitacora_382QA(SER_Usuario_382QA usuario, DateTime fecha, string modulo, string evento, int criticidad)
        {
            Usuario = usuario;
            Fecha = fecha;
            Modulo = modulo;
            Evento = evento;
            Criticidad = criticidad;
        }
    }
}
