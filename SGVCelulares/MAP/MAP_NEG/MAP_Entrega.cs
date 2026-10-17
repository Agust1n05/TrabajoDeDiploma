using BE;
using DAL.DAL_NEG;
using System.Data;

namespace MAP.MAP_NEG
{
    public class MAP_Entrega
    {
        DAO_Entrega dao_entrega;
        public MAP_Entrega() => dao_entrega = new();

        /// <summary>
        /// Arma el detalle de entrega de la OT: mano de obra, repuestos utilizados y presupuesto.
        /// </summary>
        public BE_DetalleEntrega ConsultarDetalle(BE_OTResumen ot)
        {
            var detalle = new BE_DetalleEntrega { OT = ot };

            DataTable diagnostico = dao_entrega.ConsultarDiagnostico(ot.NroOT);
            if (diagnostico.Rows.Count > 0)
            {
                DataRow fila = diagnostico.Rows[0];
                detalle.CostoManoObra = Convert.ToDecimal(fila["costo_mano_obra"]);
                detalle.TotalPresupuesto = fila["TotalPresupuesto"] is DBNull ? null : Convert.ToDecimal(fila["TotalPresupuesto"]);
            }

            foreach (DataRow linea in dao_entrega.ConsultarRepuestosUtilizados(ot.NroOT).Rows)
            {
                detalle.Lineas.Add(new BE_LineaPresupuesto
                {
                    Codigo = Convert.ToString(linea["Codigo"]),
                    Descripcion = Convert.ToString(linea["Descripcion"]),
                    PrecioUnitario = Convert.ToDecimal(linea["PrecioUnitario"]),
                    Cantidad = Convert.ToInt32(linea["CantidadUtilizada"])
                });
            }
            return detalle;
        }

        /// <returns>N° de la factura generada.</returns>
        public int RegistrarEntregaConCobro(BE_Factura factura, BE_Garantia garantia, string estadoOTEsperado, string estadoOTNuevo)
        {
            return dao_entrega.RegistrarEntregaConCobro(
                factura.NroOT, factura.MontoCobrado, factura.MedioPago, factura.UltimosDigitos,
                factura.FechaEntrega, garantia.FechaVencimiento, estadoOTEsperado, estadoOTNuevo);
        }
    }
}
