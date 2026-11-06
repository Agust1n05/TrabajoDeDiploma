using DAL.DAL_SER;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL.DAL_NEG
{
    public class DAO_Reclamo_382QA
    {
        ComandoBD_382QA com;
        public DAO_Reclamo_382QA() => com = new();

        private const string SelectResumen = @"
            SELECT r.NroReclamo, r.NroOT, r.Motivo, r.Descripcion, r.FechaReclamo, r.Estado, g.FechaVencimiento,
                   c.dni AS DniCliente, c.Nombre AS NombreCliente, c.Apellido AS ApellidoCliente,
                   e.marca, e.modelo, e.imei,
                   t.Dni AS DniTecnico, t.Nombre AS NombreTecnico, t.Apellido AS ApellidoTecnico,
                   r.FallaCubierta, r.DetalleEvaluacion
            FROM Reclamo r
            INNER JOIN Garantia g       ON g.NroOT = r.NroOT
            INNER JOIN OrdenDeTrabajo ot ON ot.NroOT = r.NroOT
            INNER JOIN Cliente c        ON c.dni = ot.dni
            INNER JOIN Equipo e         ON e.idEquipo = ot.idEquipo
            LEFT JOIN Tecnico t         ON t.Dni = r.DniTecnico";

        private const string SelectReparacion = @"
            SELECT ot.NroOT, ot.dni AS DniCliente, ot.DescripcionFalla, g.FechaVencimiento,
                   e.idEquipo, e.marca, e.modelo, e.imei
            FROM OrdenDeTrabajo ot
            INNER JOIN Garantia g ON g.NroOT = ot.NroOT
            INNER JOIN Equipo e   ON e.idEquipo = ot.idEquipo";

        /// <summary>Todos los reclamos con los datos de la OT original, cliente, equipo, técnico y garantía.</summary>
        public DataTable ConsultarResumen_382QA()
        {
            return com.Consultar_382QA(SelectResumen + " ORDER BY r.NroReclamo DESC");
        }

        public DataTable ConsultarResumenPorNro_382QA(int nroReclamo)
        {
            var parametros = new List<ParametroBD_382QA> { new("@NroReclamo", SqlDbType.Int, nroReclamo) };
            return com.Consultar_382QA(SelectResumen + " WHERE r.NroReclamo = @NroReclamo", parametros);
        }

        /// <summary>OT del cliente en el estado indicado (entregada) que tienen garantía, de la más nueva a la más vieja.</summary>
        public DataTable ConsultarReparacionesConGarantia_382QA(string dni, string estadoOT)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@estado", SqlDbType.VarChar, estadoOT)
            };
            return com.Consultar_382QA(SelectReparacion + " WHERE ot.dni = @dni AND ot.Estado = @estado ORDER BY ot.NroOT DESC", parametros);
        }

        /// <summary>La OT con su garantía si está en el estado indicado (0 o 1 fila).</summary>
        public DataTable ConsultarReparacionConGarantia_382QA(int nroOT, string estadoOT)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@NroOT", SqlDbType.Int, nroOT),
                new("@estado", SqlDbType.VarChar, estadoOT)
            };
            return com.Consultar_382QA(SelectReparacion + " WHERE ot.NroOT = @NroOT AND ot.Estado = @estado", parametros);
        }

        /// <summary>
        /// NroReclamo de un reclamo del equipo que esté en alguno de los estados indicados (el más reciente),
        /// o 0 si no tiene ninguno. El equipo se obtiene de la OT original de cada reclamo.
        /// </summary>
        public int ObtenerReclamoDelEquipo_382QA(int idEquipo, IEnumerable<string> estados)
        {
            var parametros = new List<ParametroBD_382QA> { new("@idEquipo", SqlDbType.Int, idEquipo) };
            string filtroEstados = FiltroEstados_382QA.Armar_382QA(estados, parametros);
            object? res = com.Escalar_382QA($@"
                SELECT TOP 1 r.NroReclamo
                FROM Reclamo r
                INNER JOIN OrdenDeTrabajo ot ON ot.NroOT = r.NroOT
                WHERE ot.idEquipo = @idEquipo AND r.Estado IN ({filtroEstados})
                ORDER BY r.NroReclamo DESC", parametros);
            return res == null || res is DBNull ? 0 : Convert.ToInt32(res);
        }

        /// <summary>Cantidad de reclamos del cliente (dueño de la OT original) en alguno de los estados indicados.</summary>
        public int ContarPorCliente_382QA(string dni, IEnumerable<string> estados)
        {
            var parametros = new List<ParametroBD_382QA> { new("@dni", SqlDbType.VarChar, dni) };
            string filtroEstados = FiltroEstados_382QA.Armar_382QA(estados, parametros);
            object? res = com.Escalar_382QA($@"
                SELECT COUNT(*)
                FROM Reclamo r
                INNER JOIN OrdenDeTrabajo ot ON ot.NroOT = r.NroOT
                WHERE ot.dni = @dni AND r.Estado IN ({filtroEstados})", parametros);
            return Convert.ToInt32(res);
        }

        /// <summary>Cantidad de reclamos asignados al técnico que están en alguno de los estados indicados.</summary>
        public int ContarPorTecnico_382QA(string dni, IEnumerable<string> estados)
        {
            var parametros = new List<ParametroBD_382QA> { new("@dni", SqlDbType.VarChar, dni) };
            string filtroEstados = FiltroEstados_382QA.Armar_382QA(estados, parametros);
            object? res = com.Escalar_382QA($"SELECT COUNT(*) FROM Reclamo WHERE DniTecnico = @dni AND Estado IN ({filtroEstados})", parametros);
            return Convert.ToInt32(res);
        }

        /// <summary>Detalle del diagnóstico de la OT, o null si no tiene.</summary>
        public string? ConsultarDetalleDiagnostico_382QA(int nroOT)
        {
            var parametros = new List<ParametroBD_382QA> { new("@NroOT", SqlDbType.Int, nroOT) };
            object? res = com.Escalar_382QA("SELECT TOP 1 detalle FROM Diagnostico WHERE NroOT = @NroOT ORDER BY IdDiagnostico DESC", parametros);
            return res == null || res is DBNull ? null : Convert.ToString(res);
        }

        /// <summary>
        /// CU-NEG-8: guarda la evaluación y pasa el reclamo al estado nuevo, solo si sigue en el estado esperado
        /// (otro usuario pudo haberlo cambiado). Devuelve false si ya no estaba en ese estado.
        /// </summary>
        public bool RegistrarEvaluacion_382QA(int nroReclamo, bool fallaCubierta, string detalle, string estadoEsperado, string estadoNuevo)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@Cubierta", SqlDbType.Bit, fallaCubierta),
                new("@Detalle", SqlDbType.VarChar, detalle),
                new("@Nuevo", SqlDbType.VarChar, estadoNuevo),
                new("@NroReclamo", SqlDbType.Int, nroReclamo),
                new("@Esperado", SqlDbType.VarChar, estadoEsperado)
            };
            return com.Ejecutar_382QA(@"UPDATE Reclamo SET FallaCubierta = @Cubierta, DetalleEvaluacion = @Detalle, Estado = @Nuevo
                                  WHERE NroReclamo = @NroReclamo AND Estado = @Esperado", parametros) == 1;
        }

        /// <summary>
        /// CU-NEG-9: en una sola transacción pasa el reclamo al estado nuevo, guarda los repuestos utilizados y los
        /// descuenta del stock. El estado solo cambia si el reclamo sigue en estadoEsperado, y el descuento solo se hace
        /// si el stock alcanza en ese momento (otro usuario pudo consumirlo). Si algo falla no queda nada guardado.
        /// </summary>
        public void RegistrarReparacion_382QA(int nroReclamo, List<(string Codigo, int Cantidad)> repuestos,
                                        string estadoEsperado, string estadoNuevo)
        {
            com.EnTransaccion_382QA(tx =>
            {
                int filas = tx.Ejecutar_382QA(
                    "UPDATE Reclamo SET Estado = @nuevo WHERE NroReclamo = @NroReclamo AND Estado = @esperado",
                    new List<ParametroBD_382QA>
                    {
                        new("@nuevo", SqlDbType.VarChar, estadoNuevo),
                        new("@NroReclamo", SqlDbType.Int, nroReclamo),
                        new("@esperado", SqlDbType.VarChar, estadoEsperado)
                    });
                if (filas != 1)
                    throw new InvalidOperationException($"El reclamo N° {nroReclamo} ya no está en estado \"{estadoEsperado}\".");

                foreach (var (codigo, cantidad) in repuestos)
                {
                    tx.Ejecutar_382QA(
                        "INSERT INTO DetalleRepuestoReclamo (NroReclamo, Codigo, CantidadUtilizada) VALUES (@NroReclamo, @Codigo, @cantidad)",
                        new List<ParametroBD_382QA>
                        {
                            new("@NroReclamo", SqlDbType.Int, nroReclamo),
                            new("@Codigo", SqlDbType.VarChar, codigo),
                            new("@cantidad", SqlDbType.Int, cantidad)
                        });

                    filas = tx.Ejecutar_382QA(
                        @"UPDATE Repuesto SET stockDisponible = stockDisponible - @cantidad
                          WHERE Codigo = @Codigo AND stockDisponible >= @cantidad",
                        new List<ParametroBD_382QA>
                        {
                            new("@cantidad", SqlDbType.Int, cantidad),
                            new("@Codigo", SqlDbType.VarChar, codigo)
                        });
                    if (filas != 1)
                        throw new InvalidOperationException($"No hay stock suficiente del repuesto {codigo}.");
                }

                return 0;
            });
        }

        /// <summary>Cambia el estado del reclamo solo si sigue en estadoEsperado. Devuelve false si ya cambió.</summary>
        public bool CambiarEstado_382QA(int nroReclamo, string estadoEsperado, string estadoNuevo)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@NroReclamo", SqlDbType.Int, nroReclamo),
                new("@esperado", SqlDbType.VarChar, estadoEsperado),
                new("@nuevo", SqlDbType.VarChar, estadoNuevo)
            };
            return com.Ejecutar_382QA("UPDATE Reclamo SET Estado = @nuevo WHERE NroReclamo = @NroReclamo AND Estado = @esperado", parametros) == 1;
        }

        /// <summary>
        /// CU-NEG-7: en una sola transacción registra el reclamo en estado "Pendiente" (paso 2) y lo pasa al estado
        /// que resultó de validar la garantía, con su técnico si corresponde (paso 3). Si algo falla no queda nada guardado.
        /// </summary>
        /// <returns>N° del reclamo generado.</returns>
        public int Registrar_382QA(int nroOT, string motivo, string descripcion, DateTime fechaReclamo,
                             string estadoInicial, string estadoNuevo, string? dniTecnico)
        {
            try
            {
                return com.EnTransaccion_382QA(tx =>
                {
                    int nroReclamo = Convert.ToInt32(tx.Escalar_382QA(
                        @"INSERT INTO Reclamo (NroOT, Motivo, Descripcion, FechaReclamo, Estado)
                          OUTPUT INSERTED.NroReclamo
                          VALUES (@NroOT, @Motivo, @Descripcion, @Fecha, @Estado)",
                        new List<ParametroBD_382QA>
                        {
                            new("@NroOT", SqlDbType.Int, nroOT),
                            new("@Motivo", SqlDbType.VarChar, motivo),
                            new("@Descripcion", SqlDbType.VarChar, descripcion),
                            new("@Fecha", SqlDbType.Date, fechaReclamo.Date),
                            new("@Estado", SqlDbType.VarChar, estadoInicial)
                        }));

                    tx.Ejecutar_382QA(
                        "UPDATE Reclamo SET Estado = @Estado, DniTecnico = @DniTecnico WHERE NroReclamo = @NroReclamo",
                        new List<ParametroBD_382QA>
                        {
                            new("@Estado", SqlDbType.VarChar, estadoNuevo),
                            new("@DniTecnico", SqlDbType.VarChar, (object?)dniTecnico ?? DBNull.Value),
                            new("@NroReclamo", SqlDbType.Int, nroReclamo)
                        });

                    return nroReclamo;
                });
            }
            catch (SqlException ex) when (ex.Number == 2601) // índice UX_Reclamo_OTAbierta
            {
                throw new Exception($"La OT N° {nroOT} ya tiene un reclamo abierto.");
            }
        }
    }
}
