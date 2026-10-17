using BE;
using BLL.BLL_SER;
using MAP.MAP_NEG;
using Servicio;

namespace BLL.BLL_NEG
{
    /// <summary>
    /// CU-NEG-5 Reparar equipo: el técnico registra los repuestos que usó, se descuentan del stock
    /// y la OT pasa a "Reparado", lista para que el vendedor la entregue (CU-NEG-3).
    /// </summary>
    public class BLL_Reparacion
    {
        private const string Modulo = "Órdenes de trabajo";

        MAP_Reparacion map_reparacion;
        BLL_OT bll_ot;
        BLL_Bitacora bll_bitacora;
        BLL_DV bll_dv;

        public BLL_Reparacion()
        {
            map_reparacion = new MAP_Reparacion();
            bll_ot = new BLL_OT();
            bll_bitacora = new BLL_Bitacora();
            bll_dv = new BLL_DV();
        }

        /// <summary>
        /// Paso 3: OT a reparar (asignada al técnico y "En reparacion").
        /// </summary>
        public BE_OTResumen ObtenerOTParaReparar(int nroOT)
        {
            BE_OTResumen ot = bll_ot.ObtenerOTAccesible(nroOT);
            if (ot.Estado != EstadosOT.EnReparacion)
                throw new Exception($"La OT N° {nroOT} está en estado \"{ot.Estado}\": solo se reparan órdenes en reparación.");
            return ot;
        }

        /// <summary>
        /// Repuestos aprobados en el presupuesto con el stock actual.
        /// La cantidad utilizada se propone igual a la aprobada; el técnico la puede bajar.
        /// </summary>
        public List<BE_LineaReparacion> ConsultarRepuestos(int nroOT)
        {
            List<BE_LineaReparacion> lineas = map_reparacion.ConsultarLineasPorOT(nroOT);
            foreach (BE_LineaReparacion linea in lineas)
                linea.CantidadUtilizada = linea.CantidadRequerida;
            return lineas;
        }

        /// <summary>
        /// Pasos 5 a 7. Valida las cantidades utilizadas (entre 0 y lo aprobado) y revalida el stock en ese momento:
        /// si no alcanza, lanza StockInsuficienteException y la OT sigue "En reparacion" hasta que se reponga.
        /// Con stock: en una transacción guarda las cantidades, las descuenta del inventario y pasa la OT a "Reparado".
        /// </summary>
        /// <param name="cantidadesUtilizadas">Código del repuesto → cantidad utilizada. Los que no vienen se toman como 0.</param>
        /// <returns>La OT actualizada (el vendedor la ve "Reparado" en su listado: paso 8).</returns>
        public BE_OTResumen RegistrarReparacion(int nroOT, Dictionary<string, int> cantidadesUtilizadas)
        {
            ObtenerOTParaReparar(nroOT);
            cantidadesUtilizadas ??= new Dictionary<string, int>();

            List<BE_LineaReparacion> lineas = map_reparacion.ConsultarLineasPorOT(nroOT);

            foreach (string codigo in cantidadesUtilizadas.Keys)
                if (!lineas.Any(l => l.Codigo == codigo))
                    throw new Exception($"El repuesto {codigo} no figura en el presupuesto de la OT N° {nroOT}.");

            var detalles = new List<BE_DetalleRepuesto>();
            foreach (BE_LineaReparacion linea in lineas)
            {
                linea.CantidadUtilizada = cantidadesUtilizadas.TryGetValue(linea.Codigo, out int cantidad) ? cantidad : 0;

                if (linea.CantidadUtilizada < 0)
                    throw new Exception($"La cantidad utilizada del repuesto {linea.Codigo} no puede ser negativa.");

                if (linea.CantidadUtilizada > linea.CantidadRequerida)
                    throw new Exception($"Del repuesto {linea.Codigo} se aprobaron {linea.CantidadRequerida}: " +
                                        "no se puede utilizar más de lo presupuestado.");

                detalles.Add(new BE_DetalleRepuesto(linea.Codigo, linea.IdDiagnostico, linea.CantidadRequerida,
                                                    linea.CantidadUtilizada, linea.PrecioUnitario));
            }

            // Paso 6: revalidación del stock (se pudo haber consumido desde el diagnóstico)
            List<BE_FaltanteStock> faltantes = lineas
                .Where(l => l.FaltaStock)
                .Select(l => new BE_FaltanteStock(l.Codigo, l.Descripcion, l.CantidadUtilizada, l.StockDisponible))
                .ToList();
            if (faltantes.Count > 0)
                throw new StockInsuficienteException(faltantes);

            // Pasos 6 y 7: descuento del stock y OT "Reparado", todo junto
            map_reparacion.Registrar(nroOT, detalles, EstadosOT.EnReparacion, EstadosOT.Reparado);

            foreach (BE_DetalleRepuesto d in detalles)
            {
                bll_dv.ActualizarDVNegocio(BLL_DV.TablaDetalleRepuesto, d.IdDiagnostico, d.Codigo);
                if (d.CantidadUtilizada > 0)
                    bll_dv.ActualizarDVNegocio(BLL_DV.TablaRepuesto, d.Codigo);
            }
            bll_dv.ActualizarDVNegocio(BLL_DV.TablaOrdenTrabajo, nroOT);

            bll_bitacora.RegistrarBitacora(new SER_Bitacora(
                SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, Modulo, "Reparar equipo", 2));

            // Paso 8: la notificación al vendedor es el nuevo estado de la OT, visible en su listado
            return bll_ot.ObtenerOTAccesible(nroOT);
        }
    }
}
