using DAL.DAL_SER;
using System.Data;

namespace DAL.DAL_NEG
{
    public class DAO_Entrega
    {
        ComandoBD com;
        public DAO_Entrega() => com = new();

        /// <summary>Diagnóstico y presupuesto de la OT (0 o 1 fila; el presupuesto puede no existir si faltó stock).</summary>
        public DataTable ConsultarDiagnostico(int nroOT)
        {
            var parametros = new List<ParametroBD> { new("@NroOT", SqlDbType.Int, nroOT) };
            return com.Consultar(@"
                SELECT d.IdDiagnostico, d.costo_mano_obra, p.TotalPresupuesto, p.EstadoPresupuesto
                FROM Diagnostico d
                LEFT JOIN Presupuesto p ON p.NroOT = d.NroOT
                WHERE d.NroOT = @NroOT", parametros);
        }

        /// <summary>Repuestos utilizados en la reparación de la OT, con el precio fijado en el diagnóstico.</summary>
        public DataTable ConsultarRepuestosUtilizados(int nroOT)
        {
            var parametros = new List<ParametroBD> { new("@NroOT", SqlDbType.Int, nroOT) };
            return com.Consultar(@"
                SELECT dr.Codigo, r.Descripcion, dr.PrecioUnitario, dr.CantidadUtilizada
                FROM DetalleRepuesto dr
                INNER JOIN Diagnostico d ON d.IdDiagnostico = dr.IdDiagnostico
                INNER JOIN Repuesto r    ON r.Codigo = dr.Codigo
                WHERE d.NroOT = @NroOT AND dr.CantidadUtilizada > 0
                ORDER BY dr.Codigo", parametros);
        }

        /// <summary>
        /// CU-NEG-3 + CU-NEG-7: en una sola transacción pasa la OT al nuevo estado (solo si sigue en estadoOTEsperado),
        /// genera la factura y la garantía. Si algo falla no queda nada guardado.
        /// </summary>
        /// <returns>N° de la factura generada.</returns>
        public int RegistrarEntregaConCobro(int nroOT, decimal montoCobrado, string medioPago, string ultimosDigitos,
                                            DateTime fechaEntrega, DateTime vencimientoGarantia,
                                            string estadoOTEsperado, string estadoOTNuevo)
        {
            return com.EnTransaccion(tx =>
            {
                int filas = tx.Ejecutar(
                    "UPDATE OrdenDeTrabajo SET Estado = @nuevo WHERE NroOT = @NroOT AND Estado = @esperado",
                    new List<ParametroBD>
                    {
                        new("@nuevo", SqlDbType.VarChar, estadoOTNuevo),
                        new("@NroOT", SqlDbType.Int, nroOT),
                        new("@esperado", SqlDbType.VarChar, estadoOTEsperado)
                    });
                if (filas != 1)
                    throw new InvalidOperationException($"La OT N° {nroOT} ya no está en estado \"{estadoOTEsperado}\".");

                int nroFactura = Convert.ToInt32(tx.Escalar(
                    @"INSERT INTO Factura (NroOT, MontoCobrado, MedioPago, UltimosDigitos, FechaEntrega)
                      OUTPUT INSERTED.NroFactura
                      VALUES (@NroOT, @monto, @medio, @digitos, @fecha)",
                    new List<ParametroBD>
                    {
                        new("@NroOT", SqlDbType.Int, nroOT),
                        new("@monto", SqlDbType.Decimal, montoCobrado),
                        new("@medio", SqlDbType.VarChar, medioPago),
                        new("@digitos", SqlDbType.Char, ultimosDigitos),
                        new("@fecha", SqlDbType.Date, fechaEntrega.Date)
                    }));

                tx.Ejecutar(
                    "INSERT INTO Garantia (NroOT, FechaVencimiento) VALUES (@NroOT, @vence)",
                    new List<ParametroBD>
                    {
                        new("@NroOT", SqlDbType.Int, nroOT),
                        new("@vence", SqlDbType.Date, vencimientoGarantia.Date)
                    });

                return nroFactura;
            });
        }
    }
}
