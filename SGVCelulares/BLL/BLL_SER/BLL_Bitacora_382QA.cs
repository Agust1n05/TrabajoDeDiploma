using MAP.MAP_SER;
using Servicio;

namespace BLL.BLL_SER
{
    public class BLL_Bitacora_382QA
    {
        MAP_Bitacora_382QA map_bitacora;
        public BLL_Bitacora_382QA()
        {
            map_bitacora = new MAP_Bitacora_382QA();
        }
        public void RegistrarBitacora_382QA(SER_Bitacora_382QA bitacora) => map_bitacora.RegistrarBitacora_382QA(bitacora);
        public List<SER_Bitacora_382QA> Consultar_382QA() => map_bitacora.Consultar_382QA();
        public List<object> ConsultarParaGrilla_382QA()
        {
            var consulta = from b in Consultar_382QA()
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
        public List<object> ConsultarParaGrilla3Dias_382QA()
        {
            var consulta = from b in Consultar_382QA() where b.Fecha >= (DateTime.Now.AddDays(-3))
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
        public SER_Usuario_382QA ConsultarPorNombreUsuario_382QA(string nombreUsuario) 
        {
            var cta = (from b in Consultar_382QA()
                      where b.Usuario.NombreUsuario == nombreUsuario
                      select new
                      {
                          Nombre = b.Usuario.Nombre,
                          Apellido = b.Usuario.Apellido
                      }).ToList();
            SER_Usuario_382QA us = new SER_Usuario_382QA();
            us.Nombre = cta[0].Nombre;
            us.Apellido = cta[0].Apellido;
            return us;
        }
        public List<object> ConsultarFiltradoBitacora_382QA(string login,string modulo,string evento,int? criticidad,DateTime? fechaInicio,DateTime? fechaFin)
        {
            var consulta = from bit in map_bitacora.Consultar_382QA()
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
