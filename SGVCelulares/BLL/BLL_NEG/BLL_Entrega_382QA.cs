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
    public class BLL_Entrega_382QA
    {
        private const string Modulo = "Órdenes de trabajo";

        MAP_Entrega_382QA map_entrega;
        MAP_OT_382QA map_ot;
        BLL_Cobro_382QA bll_cobro;
        BLL_Bitacora_382QA bll_bitacora;
        BLL_DV_382QA bll_dv;

        public BLL_Entrega_382QA()
        {
            map_entrega = new MAP_Entrega_382QA();
            map_ot = new MAP_OT_382QA();
            bll_cobro = new BLL_Cobro_382QA();
            bll_bitacora = new BLL_Bitacora_382QA();
            bll_dv = new BLL_DV_382QA();
        }

        /// <summary>Pasos 2 y 3: OT lista para entregar con lo que hay que cobrar (o el motivo por el que no se reparó).</summary>
        public BE_DetalleEntrega_382QA ConsultarDetalle_382QA(int nroOT)
        {
            BE_OTResumen_382QA ot = ObtenerOTParaEntregar_382QA(nroOT);
            return map_entrega.ConsultarDetalle_382QA(ot);
        }

        /// <summary>
        /// Pasos 4 a 7. Si la OT está reparada, valida y aprueba el cobro con la tarjeta y registra en una
        /// sola transacción el comprobante de cobro, la garantía (entrega + 90 días) y la OT "Entregado". Si la OT está sin reparar,
        /// la pasa a "Entregado sin reparar" (la tarjeta no se usa).
        /// </summary>
        public BE_ResultadoEntrega_382QA EntregarEquipo_382QA(int nroOT, BE_DatosTarjeta_382QA? tarjeta)
        {
            BE_DetalleEntrega_382QA detalle = ConsultarDetalle_382QA(nroOT);
            SER_Usuario_382QA usuario = SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario;
            DateTime hoy = DateTime.Today;
            var resultado = new BE_ResultadoEntrega_382QA();

            if (detalle.RequiereCobro)
            {
                // Si el formato de la tarjeta no es válido, no se entrega (la OT sigue "Reparado")
                BE_ComprobanteCobro_382QA comprobante = bll_cobro.RealizarCobro_382QA(nroOT, detalle.MontoACobrar, tarjeta, hoy);
                var garantia = new BE_Garantia_382QA(nroOT, hoy.AddDays(BE_Garantia_382QA.DiasGarantia));

                comprobante.NroComprobante = map_entrega.RegistrarEntregaConCobro_382QA(comprobante, garantia, EstadosOT_382QA.Reparado, EstadosOT_382QA.Entregado);

                bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaComprobanteCobro, comprobante.NroComprobante);
                bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaGarantia, nroOT);
                bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaOrdenTrabajo, nroOT);
                bll_bitacora.RegistrarBitacora_382QA(new SER_Bitacora_382QA(usuario, DateTime.Now, Modulo, "Realizar cobro", 2));
                bll_bitacora.RegistrarBitacora_382QA(new SER_Bitacora_382QA(usuario, DateTime.Now, Modulo, "Entregar equipo", 2));

                resultado.ComprobanteCobro = comprobante;
                resultado.Garantia = garantia;
            }
            else
            {
                if (!map_ot.CambiarEstado_382QA(nroOT, EstadosOT_382QA.SinReparar, EstadosOT_382QA.EntregadoSinReparar))
                    throw new Exception($"La OT N° {nroOT} cambió de estado. Actualizá el listado.");

                bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaOrdenTrabajo, nroOT);
                bll_bitacora.RegistrarBitacora_382QA(new SER_Bitacora_382QA(usuario, DateTime.Now, Modulo, "Entregar sin reparar", 2));
            }

            resultado.OT = map_ot.ConsultarResumenPorNro_382QA(nroOT)!;
            return resultado;
        }

        private BE_OTResumen_382QA ObtenerOTParaEntregar_382QA(int nroOT)
        {
            BE_OTResumen_382QA ot = map_ot.ConsultarResumenPorNro_382QA(nroOT)
                ?? throw new Exception($"No existe la OT N° {nroOT}.");

            if (ot.Estado != EstadosOT_382QA.Reparado && ot.Estado != EstadosOT_382QA.SinReparar)
                throw new Exception($"La OT N° {nroOT} está en estado \"{ot.Estado}\": solo se entregan órdenes reparadas o sin reparar.");

            return ot;
        }
    }
}
