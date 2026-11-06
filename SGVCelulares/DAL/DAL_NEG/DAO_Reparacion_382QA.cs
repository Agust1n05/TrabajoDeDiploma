using DAL.DAL_SER;
using System.Data;

namespace DAL.DAL_NEG
{
    public class DAO_Reparacion_382QA
    {
        ComandoBD_382QA com;
        public DAO_Reparacion_382QA() => com = new();

        /// <summary>Repuestos del diagnóstico de la OT con la cantidad aprobada, la utilizada y el stock actual.</summary>
        public DataTable ConsultarLineasPorOT_382QA(int nroOT)
        {
            var parametros = new List<ParametroBD_382QA> { new("@NroOT", SqlDbType.Int, nroOT) };
            return com.Consultar_382QA(@"
                SELECT dr.IdDiagnostico, dr.Codigo, r.Descripcion, dr.PrecioUnitario,
                       dr.cantidadRequerida, dr.CantidadUtilizada, r.stockDisponible
                FROM DetalleRepuesto dr
                INNER JOIN Diagnostico d ON d.IdDiagnostico = dr.IdDiagnostico
                INNER JOIN Repuesto r    ON r.Codigo = dr.Codigo
                WHERE d.NroOT = @NroOT
                ORDER BY dr.Codigo", parametros);
        }

        /// <summary>
        /// CU-NEG-5: en una sola transacción guarda la cantidad utilizada de cada repuesto, la descuenta del stock
        /// y pasa la OT al nuevo estado. El descuento solo se hace si el stock alcanza en ese momento
        /// (otro usuario pudo haberlo consumido), y el estado solo cambia si la OT sigue en estadoOTEsperado.
        /// Si algo falla no queda nada guardado.
        /// </summary>
        public void Registrar_382QA(int nroOT, List<(int IdDiagnostico, string Codigo, int CantidadUtilizada)> repuestos,
                              string estadoOTEsperado, string estadoOTNuevo)
        {
            com.EnTransaccion_382QA(tx =>
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

                foreach (var (idDiagnostico, codigo, cantidad) in repuestos)
                {
                    tx.Ejecutar_382QA(
                        "UPDATE DetalleRepuesto SET CantidadUtilizada = @cantidad WHERE IdDiagnostico = @id AND Codigo = @Codigo",
                        new List<ParametroBD_382QA>
                        {
                            new("@cantidad", SqlDbType.Int, cantidad),
                            new("@id", SqlDbType.Int, idDiagnostico),
                            new("@Codigo", SqlDbType.VarChar, codigo)
                        });

                    if (cantidad == 0) continue;

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
    }
}
