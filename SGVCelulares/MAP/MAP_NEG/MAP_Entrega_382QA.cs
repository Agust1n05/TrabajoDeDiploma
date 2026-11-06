using BE;
using DAL.DAL_NEG;
using System.Data;

namespace MAP.MAP_NEG
{
    public class MAP_Entrega_382QA
    {
        DAO_Entrega_382QA dao_entrega;
        public MAP_Entrega_382QA() => dao_entrega = new();

        /// <summary>
        /// Arma el detalle de entrega de la OT: mano de obra, repuestos utilizados y presupuesto.
        /// </summary>
        public BE_DetalleEntrega_382QA ConsultarDetalle_382QA(BE_OTResumen_382QA ot)
        {
            var detalle = new BE_DetalleEntrega_382QA { OT = ot };

            DataTable diagnostico = dao_entrega.ConsultarDiagnostico_382QA(ot.NroOT);
            if (diagnostico.Rows.Count > 0)
            {
                DataRow fila = diagnostico.Rows[0];
                detalle.CostoManoObra = Convert.ToDecimal(fila["costo_mano_obra"]);
                detalle.TotalPresupuesto = fila["TotalPresupuesto"] is DBNull ? null : Convert.ToDecimal(fila["TotalPresupuesto"]);
            }

            foreach (DataRow linea in dao_entrega.ConsultarRepuestosUtilizados_382QA(ot.NroOT).Rows)
            {
                detalle.Lineas.Add(new BE_LineaPresupuesto_382QA
                {
                    Codigo = Convert.ToString(linea["Codigo"]),
                    Descripcion = Convert.ToString(linea["Descripcion"]),
                    PrecioUnitario = Convert.ToDecimal(linea["PrecioUnitario"]),
                    Cantidad = Convert.ToInt32(linea["CantidadUtilizada"])
                });
            }
            return detalle;
        }

        /// <returns>N° del comprobante de cobro generado.</returns>
        public int RegistrarEntregaConCobro_382QA(BE_ComprobanteCobro_382QA comprobante, BE_Garantia_382QA garantia, string estadoOTEsperado, string estadoOTNuevo)
        {
            return dao_entrega.RegistrarEntregaConCobro_382QA(
                comprobante.NroOT, comprobante.MontoCobrado, comprobante.MedioPago, comprobante.UltimosDigitos,
                comprobante.FechaEntrega, garantia.FechaVencimiento, estadoOTEsperado, estadoOTNuevo);
        }
    }
}
