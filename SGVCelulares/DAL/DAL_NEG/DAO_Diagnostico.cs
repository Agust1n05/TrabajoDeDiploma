using DAL.DAL_SER;
using System.Data;

namespace DAL.DAL_NEG
{
    public class DAO_Diagnostico
    {
        ComandoBD com;
        public DAO_Diagnostico() => com = new();

        /// <summary>
        /// CU-NEG-4: registra en una sola transacción el diagnóstico, sus repuestos, el presupuesto
        /// (si se indica un total) y el cambio de estado de la OT. Si algo falla no queda nada guardado.
        /// El estado de la OT solo cambia si sigue en estadoOTEsperado (evita pisar un cambio hecho por otro usuario).
        /// </summary>
        /// <returns>Id del diagnóstico y del presupuesto generados (0 si no hay presupuesto).</returns>
        public (int IdDiagnostico, int IdPresupuesto) Registrar(
            int nroOT, string detalle, decimal costoManoObra, List<(string Codigo, decimal PrecioUnitario, int Cantidad)> repuestos,
            decimal? totalPresupuesto, string estadoPresupuesto, string estadoOTEsperado, string estadoOTNuevo)
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

                int idDiagnostico = Convert.ToInt32(tx.Escalar(
                    "INSERT INTO Diagnostico (detalle, costo_mano_obra, NroOT) OUTPUT INSERTED.IdDiagnostico VALUES (@detalle, @mano, @NroOT)",
                    new List<ParametroBD>
                    {
                        new("@detalle", SqlDbType.VarChar, detalle),
                        new("@mano", SqlDbType.Decimal, costoManoObra),
                        new("@NroOT", SqlDbType.Int, nroOT)
                    }));

                foreach (var (codigo, precioUnitario, cantidad) in repuestos)
                {
                    tx.Ejecutar(
                        "INSERT INTO DetalleRepuesto (IdDiagnostico, Codigo, PrecioUnitario, cantidadRequerida, CantidadUtilizada) VALUES (@id, @Codigo, @precio, @cantidad, 0)",
                        new List<ParametroBD>
                        {
                            new("@id", SqlDbType.Int, idDiagnostico),
                            new("@Codigo", SqlDbType.VarChar, codigo),
                            new("@precio", SqlDbType.Decimal, precioUnitario),
                            new("@cantidad", SqlDbType.Int, cantidad)
                        });
                }

                int idPresupuesto = 0;
                if (totalPresupuesto.HasValue)
                {
                    idPresupuesto = Convert.ToInt32(tx.Escalar(
                        "INSERT INTO Presupuesto (TotalPresupuesto, EstadoPresupuesto, FechaRespuesta, NroOT) OUTPUT INSERTED.IdPresupuesto VALUES (@total, @estado, NULL, @NroOT)",
                        new List<ParametroBD>
                        {
                            new("@total", SqlDbType.Decimal, totalPresupuesto.Value),
                            new("@estado", SqlDbType.VarChar, estadoPresupuesto),
                            new("@NroOT", SqlDbType.Int, nroOT)
                        }));
                }

                return (idDiagnostico, idPresupuesto);
            });
        }
    }
}
