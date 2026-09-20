namespace Servicio
{
    public class SER_Bitacora
    {
        public SER_Usuario Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Evento { get; set; }
        public string Modulo { get; set; }
        public int Criticidad { get; set; }
        public SER_Bitacora(SER_Usuario usuario, DateTime fecha, string modulo, string evento, int criticidad)
        {
            Usuario = usuario;
            Fecha = fecha;
            Modulo = modulo;
            Evento = evento;
            Criticidad = criticidad;
        }
    }
}
