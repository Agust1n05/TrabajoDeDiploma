using DAL.DAL_SER;
using System.Data;

namespace DAL.DAL_NEG
{
    public class DAO_Equipo_382QA
    {
        ComandoBD_382QA com;
        public DAO_Equipo_382QA() => com = new();

        /// <summary>Equipos activos e inactivos.</summary>
        public DataTable Consultar_382QA()
        {
            return com.Consultar_382QA("SELECT idEquipo, marca, modelo, imei, Activo FROM Equipo");
        }

        public DataTable ConsultarPorId_382QA(int idEquipo)
        {
            var parametros = new List<ParametroBD_382QA> { new("@idEquipo", SqlDbType.Int, idEquipo) };
            return com.Consultar_382QA("SELECT idEquipo, marca, modelo, imei, Activo FROM Equipo WHERE idEquipo = @idEquipo", parametros);
        }

        public DataTable ConsultarPorImei_382QA(string imei)
        {
            var parametros = new List<ParametroBD_382QA> { new("@imei", SqlDbType.Char, imei) };
            return com.Consultar_382QA("SELECT idEquipo, marca, modelo, imei, Activo FROM Equipo WHERE imei = @imei", parametros);
        }

        /// <summary>Inserta el equipo y devuelve el idEquipo generado por la base.</summary>
        public int Agregar_382QA(string marca, string modelo, string imei)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@marca", SqlDbType.VarChar, marca),
                new("@modelo", SqlDbType.VarChar, modelo),
                new("@imei", SqlDbType.Char, imei)
            };
            object? res = com.Escalar_382QA(
                "INSERT INTO Equipo (marca, modelo, imei, Activo) OUTPUT INSERTED.idEquipo VALUES (@marca, @modelo, @imei, 1)",
                parametros);
            return Convert.ToInt32(res);
        }

        public void Modificar_382QA(int idEquipo, string marca, string modelo, string imei)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@idEquipo", SqlDbType.Int, idEquipo),
                new("@marca", SqlDbType.VarChar, marca),
                new("@modelo", SqlDbType.VarChar, modelo),
                new("@imei", SqlDbType.Char, imei)
            };
            com.Ejecutar_382QA("UPDATE Equipo SET marca = @marca, modelo = @modelo, imei = @imei WHERE idEquipo = @idEquipo", parametros);
        }

        /// <summary>Baja lógica (activo = false) o reactivación (activo = true).</summary>
        public void CambiarActivo_382QA(int idEquipo, bool activo)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@idEquipo", SqlDbType.Int, idEquipo),
                new("@Activo", SqlDbType.Bit, activo)
            };
            com.Ejecutar_382QA("UPDATE Equipo SET Activo = @Activo WHERE idEquipo = @idEquipo", parametros);
        }

        /// <summary>Cantidad de OT del equipo que están en alguno de los estados indicados.</summary>
        public int ContarOT_382QA(int idEquipo, IEnumerable<string> estados)
        {
            var parametros = new List<ParametroBD_382QA> { new("@idEquipo", SqlDbType.Int, idEquipo) };
            string filtroEstados = FiltroEstados_382QA.Armar_382QA(estados, parametros);
            object? res = com.Escalar_382QA($"SELECT COUNT(*) FROM OrdenDeTrabajo WHERE idEquipo = @idEquipo AND Estado IN ({filtroEstados})", parametros);
            return Convert.ToInt32(res);
        }
    }
}
