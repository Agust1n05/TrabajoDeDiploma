using BE;
using BLL.BLL_SER;
using MAP.MAP_NEG;
using Servicio;

namespace BLL.BLL_NEG
{
    /// <summary>
    /// CU-NEG-3 Gestionar entrega: el vendedor entrega el equipo al cliente.
    ///   OT "Reparado"    → se cobra con tarjeta, se genera el comprobante de cobro y la garantía de 90 días,
    ///                      y la OT pasa a "Entregado".
    ///   OT "Sin reparar" → se entrega sin cobro ni garantía y la OT pasa a "Entregado sin reparar".
    /// </summary>
    public class BLL_Entrega
    {
        private const string Modulo = "Órdenes de trabajo";

        MAP_Entrega map_entrega;
        MAP_OT map_ot;
        BLL_Cobro bll_cobro;
        BLL_Bitacora bll_bitacora;
        BLL_DV bll_dv;

        public BLL_Entrega()
        {
            map_entrega = new MAP_Entrega();
            map_ot = new MAP_OT();
            bll_cobro = new BLL_Cobro();
            bll_bitacora = new BLL_Bitacora();
            bll_dv = new BLL_DV();
        }

        /// <summary>Pasos 2 y 3: OT lista para entregar con lo que hay que cobrar (o el motivo por el que no se reparó).</summary>
        public BE_DetalleEntrega ConsultarDetalle(int nroOT)
        {
            BE_OTResumen ot = ObtenerOTParaEntregar(nroOT);
            return map_entrega.ConsultarDetalle(ot);
        }

        /// <summary>
        /// Pasos 4 a 7. Si la OT está reparada, valida y aprueba el cobro con la tarjeta y registra en una
        /// sola transacción el comprobante de cobro, la garantía (entrega + 90 días) y la OT "Entregado". Si la OT está sin reparar,
        /// la pasa a "Entregado sin reparar" (la tarjeta no se usa).
        /// </summary>
        public BE_ResultadoEntrega EntregarEquipo(int nroOT, BE_DatosTarjeta? tarjeta)
        {
            BE_DetalleEntrega detalle = ConsultarDetalle(nroOT);
            SER_Usuario usuario = SER_SesionManager.ObtenerSesion().Usuario;
            DateTime hoy = DateTime.Today;
            var resultado = new BE_ResultadoEntrega();

            if (detalle.RequiereCobro)
            {
                // Si el formato de la tarjeta no es válido, no se entrega (la OT sigue "Reparado")
                BE_ComprobanteCobro comprobante = bll_cobro.RealizarCobro(nroOT, detalle.MontoACobrar, tarjeta, hoy);
                var garantia = new BE_Garantia(nroOT, hoy.AddDays(BE_Garantia.DiasGarantia));

                comprobante.NroComprobante = map_entrega.RegistrarEntregaConCobro(comprobante, garantia, EstadosOT.Reparado, EstadosOT.Entregado);

                bll_dv.ActualizarDVNegocio(BLL_DV.TablaComprobanteCobro, comprobante.NroComprobante);
                bll_dv.ActualizarDVNegocio(BLL_DV.TablaGarantia, nroOT);
                bll_dv.ActualizarDVNegocio(BLL_DV.TablaOrdenTrabajo, nroOT);
                bll_bitacora.RegistrarBitacora(new SER_Bitacora(usuario, DateTime.Now, Modulo, "Realizar cobro", 2));
                bll_bitacora.RegistrarBitacora(new SER_Bitacora(usuario, DateTime.Now, Modulo, "Entregar equipo", 2));

                resultado.ComprobanteCobro = comprobante;
                resultado.Garantia = garantia;
            }
            else
            {
                if (!map_ot.CambiarEstado(nroOT, EstadosOT.SinReparar, EstadosOT.EntregadoSinReparar))
                    throw new Exception($"La OT N° {nroOT} cambió de estado. Actualizá el listado.");

                bll_dv.ActualizarDVNegocio(BLL_DV.TablaOrdenTrabajo, nroOT);
                bll_bitacora.RegistrarBitacora(new SER_Bitacora(usuario, DateTime.Now, Modulo, "Entregar sin reparar", 2));
            }

            resultado.OT = map_ot.ConsultarResumenPorNro(nroOT)!;
            return resultado;
        }

        private BE_OTResumen ObtenerOTParaEntregar(int nroOT)
        {
            BE_OTResumen ot = map_ot.ConsultarResumenPorNro(nroOT)
                ?? throw new Exception($"No existe la OT N° {nroOT}.");

            if (ot.Estado != EstadosOT.Reparado && ot.Estado != EstadosOT.SinReparar)
                throw new Exception($"La OT N° {nroOT} está en estado \"{ot.Estado}\": solo se entregan órdenes reparadas o sin reparar.");

            return ot;
        }
    }
}
