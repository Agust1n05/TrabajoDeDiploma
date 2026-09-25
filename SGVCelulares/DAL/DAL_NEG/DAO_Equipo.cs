using DAL.DAL_SER;
using System.Data;

namespace DAL
{
    public class DAO_Equipo
    {
        ComandoBD com;
        public DAO_Equipo() => com = new();

        public void Agregar(string idEquipo, string marca, string modelo, string imei, string descripcionFalla)
        {
            var parametros = new List<ParametroBD>
            {
                new("@idEquipo", SqlDbType.VarChar, idEquipo),
                new("@marca", SqlDbType.VarChar, marca),
                new("@modelo", SqlDbType.VarChar, modelo),
                new("@imei", SqlDbType.VarChar, imei),
                new("@descripcionFalla", SqlDbType.VarChar, descripcionFalla)
            };
            com.Ejecutar("INSERT INTO equipo(idEquipo,marca,modelo,imei,descripcionFalla) VALUES (@idEquipo,@marca,@modelo,@imei,@descripcionFalla)", parametros);
        }
    }
}
