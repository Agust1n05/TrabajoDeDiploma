using DAL.DAL_SER;
using System.Data;

namespace DAL.DAL_NEG
{
    public class DAO_Presupuesto_382QA
    {
        ComandoBD_382QA com;
        public DAO_Presupuesto_382QA() => com = new();

        /// <summary>Presupuesto de la OT junto con el diagnóstico (0 o 1 fila).</summary>
        public DataTable ConsultarPorOT_382QA(int nroOT)
        {
            var parametros = new List<ParametroBD_382QA> { new("@NroOT", SqlDbType.Int, nroOT) };
            return com.Consultar_382QA(@"
                SELECT p.IdPresupuesto, p.TotalPresupuesto, p.EstadoPresupuesto, p.FechaRespuesta, p.NroOT,
                       d.detalle, d.costo_mano_obra
                FROM Presupuesto p
                INNER JOIN Diagnostico d ON d.NroOT = p.NroOT
                WHERE p.NroOT = @NroOT", parametros);
        }

        /// <summary>Repuestos del diagnóstico de la OT, con su descripción y el precio fijado al diagnosticar.</summary>
        public DataTable ConsultarLineasPorOT_382QA(int nroOT)
        {
            var parametros = new List<ParametroBD_382QA> { new("@NroOT", SqlDbType.Int, nroOT) };
            return com.Consultar_382QA(@"
                SELECT dr.Codigo, r.Descripcion, dr.PrecioUnitario, dr.cantidadRequerida
                FROM DetalleRepuesto dr
                INNER JOIN Diagnostico d ON d.IdDiagnostico = dr.IdDiagnostico
                INNER JOIN Repuesto r    ON r.Codigo = dr.Codigo
                WHERE d.NroOT = @NroOT
                ORDER BY dr.Codigo", parametros);
        }

        /// <summary>
        /// CU-NEG-2: registra la respuesta del cliente y el nuevo estado de la OT en una sola transacción.
        /// Solo actúa si el presupuesto sigue Pendiente y la OT sigue Presupuestada.
        /// </summary>
        public void RegistrarRespuesta_382QA(int idPresupuesto, int nroOT, string estadoPresupuesto, DateTime fechaRespuesta,
                                       string estadoPresupuestoEsperado, string estadoOTEsperado, string estadoOTNuevo)
        {
            com.EnTransaccion_382QA(tx =>
            {
                int filas = tx.Ejecutar_382QA(
                    @"UPDATE Presupuesto SET EstadoPresupuesto = @estado, FechaRespuesta = @fecha
                      WHERE IdPresupuesto = @id AND EstadoPresupuesto = @esperado",
                    new List<ParametroBD_382QA>
                    {
                        new("@estado", SqlDbType.VarChar, estadoPresupuesto),
                        new("@fecha", SqlDbType.Date, fechaRespuesta.Date),
                        new("@id", SqlDbType.Int, idPresupuesto),
                        new("@esperado", SqlDbType.VarChar, estadoPresupuestoEsperado)
                    });
                if (filas != 1)
                    throw new InvalidOperationException("El presupuesto ya tiene una respuesta registrada.");

                filas = tx.Ejecutar_382QA(
                    "UPDATE OrdenDeTrabajo SET Estado = @nuevo WHERE NroOT = @NroOT AND Estado = @esperado",
                    new List<ParametroBD_382QA>
                    {
                        new("@nuevo", SqlDbType.VarChar, estadoOTNuevo),
                        new("@NroOT", SqlDbType.Int, nroOT),
                        new("@esperado", SqlDbType.VarChar, estadoOTEsperado)
                    });
                if (filas != 1)
                    throw new InvalidOperationException($"La OT N° {nroOT} ya no está en estado \"{estadoOTEsperado}\".");

                return 0;
            });
        }
    }
}
