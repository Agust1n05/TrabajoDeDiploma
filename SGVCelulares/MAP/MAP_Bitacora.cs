using DAL;
using Servicio;
using System.Data;

namespace MAP
{
    public class MAP_Bitacora
    {
        DAO_Bitacora dao_bitacora;
        MAP_Usuario map_usuario;
        public MAP_Bitacora()
        {
            dao_bitacora = new DAO_Bitacora();
            map_usuario = new MAP_Usuario();
        }
        public void RegistrarBitacora(SER_Bitacora bitacora) => dao_bitacora.Agregar(bitacora.Usuario.Dni, bitacora.Fecha, bitacora.Modulo, bitacora.Evento, bitacora.Criticidad);
        public List<SER_Bitacora> Consultar()
        {
            DataTable dt = dao_bitacora.Consultar();
            var lista = new List<SER_Bitacora>();
            foreach (DataRow fila in dt.Rows)
            {
                lista.Add(Mapear(fila));
            }
            return lista;
        }
        private SER_Bitacora Mapear(DataRow fila)
        {
            return new SER_Bitacora(
                map_usuario.ConsultarPorId(new SER_Usuario(fila["dni"].ToString())),
                Convert.ToDateTime(fila["fecha"]),
                Convert.ToString(fila["modulo"]),
                Convert.ToString(fila["evento"]),
                Convert.ToInt16(fila["criticidad"])
            );
        }
    }
}
