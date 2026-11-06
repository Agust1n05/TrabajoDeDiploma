using DAL.DAL_SER;
using Servicio;
using System.Data;

namespace MAP.MAP_SER
{
    public class MAP_Bitacora_382QA
    {
        DAO_Bitacora_382QA dao_bitacora;
        MAP_Usuario_382QA map_usuario;
        public MAP_Bitacora_382QA()
        {
            dao_bitacora = new DAO_Bitacora_382QA();
            map_usuario = new MAP_Usuario_382QA();
        }
        public void RegistrarBitacora_382QA(SER_Bitacora_382QA bitacora) => dao_bitacora.Agregar_382QA(bitacora.Usuario.Dni, bitacora.Fecha, bitacora.Modulo, bitacora.Evento, bitacora.Criticidad);
        public List<SER_Bitacora_382QA> Consultar_382QA()
        {
            DataTable dt = dao_bitacora.Consultar_382QA();
            var lista = new List<SER_Bitacora_382QA>();
            foreach (DataRow fila in dt.Rows)
            {
                lista.Add(Mapear_382QA(fila));
            }
            return lista;
        }
        private SER_Bitacora_382QA Mapear_382QA(DataRow fila)
        {
            return new SER_Bitacora_382QA(
                map_usuario.ConsultarPorId_382QA(new SER_Usuario_382QA(fila["dni"].ToString())),
                Convert.ToDateTime(fila["fecha"]),
                Convert.ToString(fila["modulo"]),
                Convert.ToString(fila["evento"]),
                Convert.ToInt16(fila["criticidad"])
            );
        }
    }
}
