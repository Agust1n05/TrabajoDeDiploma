using DAL.DAL_SER;
using System.Data;

namespace DAL.DAL_NEG
{
    public class DAO_Entrega_382QA
    {
        ComandoBD_382QA com;
        public DAO_Entrega_382QA() => com = new();

        /// <summary>Diagnóstico y presupuesto de la OT (0 o 1 fila; el presupuesto puede no existir si faltó stock).</summary>
        public DataTable ConsultarDiagnostico_382QA(int nroOT)
        {
            var parametros = new List<ParametroBD_382QA> { new("@NroOT", SqlDbType.Int, nroOT) };
            return com.Consultar_382QA(@"
                SELECT d.IdDiagnostico, d.costo_mano_obra, p.TotalPresupuesto, p.EstadoPresupuesto
                FROM Diagnostico d
                LEFT JOIN Presupuesto p ON p.NroOT = d.NroOT
                WHERE d.NroOT = @NroOT", parametros);
        }

        /// <summary>Repuestos utilizados en la reparación de la OT, con el precio fijado en el diagnóstico.</summary>
        public DataTable ConsultarRepuestosUtilizados_382QA(int nroOT)
        {
            var parametros = new List<ParametroBD_382QA> { new("@NroOT", SqlDbType.Int, nroOT) };
            return com.Consultar_382QA(@"
                SELECT dr.Codigo, r.Descripcion, dr.PrecioUnitario, dr.CantidadUtilizada
                FROM DetalleRepuesto dr
                INNER JOIN Diagnostico d ON d.IdDiagnostico = dr.IdDiagnostico
                INNER JOIN Repuesto r    ON r.Codigo = dr.Codigo
                WHERE d.NroOT = @NroOT AND dr.CantidadUtilizada > 0
                ORDER BY dr.Codigo", parametros);
        }

        /// <summary>
        /// CU-NEG-3 Gestionar entrega: en una sola transacción pasa la OT al nuevo estado (solo si sigue en estadoOTEsperado),
        /// genera el comprobante de cobro y la garantía. Si algo falla no queda nada guardado.
        /// </summary>
        /// <returns>N° del comprobante de cobro generado.</returns>
        public int RegistrarEntregaConCobro_382QA(int nroOT, decimal montoCobrado, string medioPago, string ultimosDigitos,
                                            DateTime fechaEntrega, DateTime vencimientoGarantia,
                                            string estadoOTEsperado, string estadoOTNuevo)
        {
            return com.EnTransaccion_382QA(tx =>
            {
                int filas = tx.Ejecutar_382QA(
                    "UPDATE OrdenDeTrabajo SET Estado = @nuevo WHERE NroOT = @NroOT AND Estado = @esperado",
                    new List<ParametroBD_382QA>
                    {
                        new("@nuevo", SqlDbType.VarChar, estadoOTNuevo),
                        new("@NroOT", SqlDbType.Int, nroOT),
                        new("@esperado", SqlDbType.VarChar, estadoOTEsperado)
                    });
                if (filas != 1)
                    throw new InvalidOperationException($"La OT N° {nroOT} ya no está en estado \"{estadoOTEsperado}\".");

                int nroComprobante = Convert.ToInt32(tx.Escalar_382QA(
                    @"INSERT INTO ComprobanteCobro (NroOT, MontoCobrado, MedioPago, UltimosDigitos, FechaEntrega)
                      OUTPUT INSERTED.NroComprobante
                      VALUES (@NroOT, @monto, @medio, @digitos, @fecha)",
                    new List<ParametroBD_382QA>
                    {
                        new("@NroOT", SqlDbType.Int, nroOT),
                        new("@monto", SqlDbType.Decimal, montoCobrado),
                        new("@medio", SqlDbType.VarChar, medioPago),
                        new("@digitos", SqlDbType.Char, ultimosDigitos),
                        new("@fecha", SqlDbType.Date, fechaEntrega.Date)
                    }));

                tx.Ejecutar_382QA(
                    "INSERT INTO Garantia (NroOT, FechaVencimiento) VALUES (@NroOT, @vence)",
                    new List<ParametroBD_382QA>
                    {
                        new("@NroOT", SqlDbType.Int, nroOT),
                        new("@vence", SqlDbType.Date, vencimientoGarantia.Date)
                    });

                return nroComprobante;
            });
        }
    }
}
