using MAP;
using Servicio;

namespace BLL
{
    public class BLL_Bitacora
    {
        MAP_Bitacora map_bitacora;
        public BLL_Bitacora()
        {
            map_bitacora = new MAP_Bitacora();
        }
        public void RegistrarBitacora(SER_Bitacora bitacora) => map_bitacora.RegistrarBitacora(bitacora);
        public List<SER_Bitacora> Consultar() => map_bitacora.Consultar();
        public List<object> ConsultarParaGrilla()
        {
            var consulta = from b in Consultar()
                           select new
                           {
                               Usuario = b.Usuario.NombreUsuario,
                               Fecha = b.Fecha.ToShortDateString(),
                               Hora = b.Fecha.ToString("HH:mm"),
                               Modulo = b.Modulo,
                               Evento = b.Evento,
                               Criticidad = b.Criticidad
                           };
            return consulta.ToList<object>();
        }
        public List<object> ConsultarParaGrilla3Dias()
        {
            var consulta = from b in Consultar() where b.Fecha >= (DateTime.Now.AddDays(-3))
                           select new
                           {
                               Usuario = b.Usuario.NombreUsuario,
                               Fecha = b.Fecha.ToShortDateString(),
                               Hora = b.Fecha.ToString("HH:mm"),
                               Modulo = b.Modulo,
                               Evento = b.Evento,
                               Criticidad = b.Criticidad
                           };
            return consulta.ToList<object>();
        }
        public SER_Usuario ConsultarPorNombreUsuario(string nombreUsuario) 
        {
            var cta = (from b in Consultar()
                      where b.Usuario.NombreUsuario == nombreUsuario
                      select new
                      {
                          Nombre = b.Usuario.Nombre,
                          Apellido = b.Usuario.Apellido
                      }).ToList();
            SER_Usuario us = new SER_Usuario();
            us.Nombre = cta[0].Nombre;
            us.Apellido = cta[0].Apellido;
            return us;
        }
        public List<object> ConsultarFiltradoBitacora(string login,string modulo,string evento,int? criticidad,DateTime? fechaInicio,DateTime? fechaFin)
        {
            var consulta = from bit in map_bitacora.Consultar()
                           where
                           (string.IsNullOrWhiteSpace(login)
                           || bit.Usuario.NombreUsuario.Contains(login))
                           &&
                           (string.IsNullOrWhiteSpace(modulo)
                           || bit.Modulo.Contains(modulo))
                           &&
                           (string.IsNullOrWhiteSpace(evento)
                           || bit.Evento.Contains(evento))
                           &&
                           (!criticidad.HasValue
                            || bit.Criticidad == criticidad.Value)
                           &&
                           (!fechaInicio.HasValue
                           || bit.Fecha >= fechaInicio.Value)
                           &&
                           (!fechaFin.HasValue
                           || bit.Fecha <= fechaFin.Value)
                            select new
                           {
                                Usuario = bit.Usuario.NombreUsuario,
                                Fecha = bit.Fecha.ToShortDateString(),
                                Hora = bit.Fecha.ToString("HH:mm"),
                                Modulo = bit.Modulo,
                                Evento = bit.Evento,
                                Criticidad = bit.Criticidad
                           };

            return consulta.ToList<object>();
        }
    }
}
