using DAL.DAL_SER;
using System.Data;

namespace DAL.DAL_NEG
{
    public class DAO_OT
    {
        ComandoBD com;
        public DAO_OT() => com = new();

        private const string SelectResumen = @"
            SELECT ot.NroOT, ot.Estado, ot.DescripcionFalla,
                   c.dni AS DniCliente, c.Nombre AS NombreCliente, c.Apellido AS ApellidoCliente, c.Telefono AS TelefonoCliente,
                   e.idEquipo, e.marca, e.modelo, e.imei,
                   t.Dni AS DniTecnico, t.Nombre AS NombreTecnico, t.Apellido AS ApellidoTecnico,
                   p.TotalPresupuesto, p.EstadoPresupuesto
            FROM OrdenDeTrabajo ot
            INNER JOIN Cliente c   ON c.dni = ot.dni
            INNER JOIN Equipo e    ON e.idEquipo = ot.idEquipo
            INNER JOIN Tecnico t   ON t.Dni = ot.DniTecnico
            LEFT JOIN Presupuesto p ON p.NroOT = ot.NroOT";

        /// <summary>Todas las OT con los datos de cliente, equipo, técnico y presupuesto.</summary>
        public DataTable ConsultarResumen()
        {
            return com.Consultar(SelectResumen + " ORDER BY ot.NroOT DESC");
        }

        public DataTable ConsultarResumenPorNro(int nroOT)
        {
            var parametros = new List<ParametroBD> { new("@NroOT", SqlDbType.Int, nroOT) };
            return com.Consultar(SelectResumen + " WHERE ot.NroOT = @NroOT", parametros);
        }

        /// <summary>Inserta la OT y devuelve el NroOT correlativo generado por la base.</summary>
        public int Agregar(string estado, string dni, int idEquipo, string dniTecnico, string descripcionFalla)
        {
            var parametros = new List<ParametroBD>
            {
                new("@Estado", SqlDbType.VarChar, estado),
                new("@dni", SqlDbType.VarChar, dni),
                new("@idEquipo", SqlDbType.Int, idEquipo),
                new("@DniTecnico", SqlDbType.VarChar, dniTecnico),
                new("@DescripcionFalla", SqlDbType.VarChar, descripcionFalla)
            };
            object? res = com.Escalar(
                @"INSERT INTO OrdenDeTrabajo (Estado, dni, idEquipo, DniTecnico, DescripcionFalla)
                  OUTPUT INSERTED.NroOT
                  VALUES (@Estado, @dni, @idEquipo, @DniTecnico, @DescripcionFalla)",
                parametros);
            return Convert.ToInt32(res);
        }

        /// <summary>
        /// Cambia el estado de la OT solo si todavía está en estadoEsperado.
        /// Devuelve false si la OT no existe o ya estaba en otro estado (otro usuario la modificó).
        /// </summary>
        public bool CambiarEstado(int nroOT, string estadoEsperado, string estadoNuevo)
        {
            var parametros = new List<ParametroBD>
            {
                new("@NroOT", SqlDbType.Int, nroOT),
                new("@esperado", SqlDbType.VarChar, estadoEsperado),
                new("@nuevo", SqlDbType.VarChar, estadoNuevo)
            };
            return com.Ejecutar("UPDATE OrdenDeTrabajo SET Estado = @nuevo WHERE NroOT = @NroOT AND Estado = @esperado", parametros) == 1;
        }

        /// <summary>
        /// NroOT de una OT del equipo que esté en alguno de los estados indicados (la más reciente),
        /// o 0 si no tiene ninguna.
        /// </summary>
        public int ObtenerOTDelEquipo(int idEquipo, IEnumerable<string> estados)
        {
            var parametros = new List<ParametroBD> { new("@idEquipo", SqlDbType.Int, idEquipo) };
            string filtroEstados = FiltroEstados.Armar(estados, parametros);
            object? res = com.Escalar(
                $"SELECT TOP 1 NroOT FROM OrdenDeTrabajo WHERE idEquipo = @idEquipo AND Estado IN ({filtroEstados}) ORDER BY NroOT DESC",
                parametros);
            return res == null || res is DBNull ? 0 : Convert.ToInt32(res);
        }
    }
}
