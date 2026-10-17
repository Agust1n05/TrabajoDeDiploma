using DAL.DAL_SER;
using System.Data;

namespace DAL.DAL_NEG
{
    public class DAO_Equipo
    {
        ComandoBD com;
        public DAO_Equipo() => com = new();

        /// <summary>Equipos activos e inactivos.</summary>
        public DataTable Consultar()
        {
            return com.Consultar("SELECT idEquipo, marca, modelo, imei, Activo FROM Equipo");
        }

        public DataTable ConsultarPorId(int idEquipo)
        {
            var parametros = new List<ParametroBD> { new("@idEquipo", SqlDbType.Int, idEquipo) };
            return com.Consultar("SELECT idEquipo, marca, modelo, imei, Activo FROM Equipo WHERE idEquipo = @idEquipo", parametros);
        }

        public DataTable ConsultarPorImei(string imei)
        {
            var parametros = new List<ParametroBD> { new("@imei", SqlDbType.Char, imei) };
            return com.Consultar("SELECT idEquipo, marca, modelo, imei, Activo FROM Equipo WHERE imei = @imei", parametros);
        }

        /// <summary>Inserta el equipo y devuelve el idEquipo generado por la base.</summary>
        public int Agregar(string marca, string modelo, string imei)
        {
            var parametros = new List<ParametroBD>
            {
                new("@marca", SqlDbType.VarChar, marca),
                new("@modelo", SqlDbType.VarChar, modelo),
                new("@imei", SqlDbType.Char, imei)
            };
            object? res = com.Escalar(
                "INSERT INTO Equipo (marca, modelo, imei, Activo) OUTPUT INSERTED.idEquipo VALUES (@marca, @modelo, @imei, 1)",
                parametros);
            return Convert.ToInt32(res);
        }

        public void Modificar(int idEquipo, string marca, string modelo, string imei)
        {
            var parametros = new List<ParametroBD>
            {
                new("@idEquipo", SqlDbType.Int, idEquipo),
                new("@marca", SqlDbType.VarChar, marca),
                new("@modelo", SqlDbType.VarChar, modelo),
                new("@imei", SqlDbType.Char, imei)
            };
            com.Ejecutar("UPDATE Equipo SET marca = @marca, modelo = @modelo, imei = @imei WHERE idEquipo = @idEquipo", parametros);
        }

        /// <summary>Baja lógica (activo = false) o reactivación (activo = true).</summary>
        public void CambiarActivo(int idEquipo, bool activo)
        {
            var parametros = new List<ParametroBD>
            {
                new("@idEquipo", SqlDbType.Int, idEquipo),
                new("@Activo", SqlDbType.Bit, activo)
            };
            com.Ejecutar("UPDATE Equipo SET Activo = @Activo WHERE idEquipo = @idEquipo", parametros);
        }

        /// <summary>Cantidad de OT del equipo que están en alguno de los estados indicados.</summary>
        public int ContarOT(int idEquipo, IEnumerable<string> estados)
        {
            var parametros = new List<ParametroBD> { new("@idEquipo", SqlDbType.Int, idEquipo) };
            string filtroEstados = FiltroEstados.Armar(estados, parametros);
            object? res = com.Escalar($"SELECT COUNT(*) FROM OrdenDeTrabajo WHERE idEquipo = @idEquipo AND Estado IN ({filtroEstados})", parametros);
            return Convert.ToInt32(res);
        }
    }
}
