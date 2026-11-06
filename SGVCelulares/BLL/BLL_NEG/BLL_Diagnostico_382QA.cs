using BE;
using BLL.BLL_SER;
using MAP.MAP_NEG;
using Servicio;

namespace BLL.BLL_NEG
{
    /// <summary>
    /// Falta stock de uno o más repuestos del diagnóstico (CU-NEG-4, flujo alternativo 6.1.1).
    /// La UI muestra los faltantes y, si el técnico confirma, vuelve a registrar aceptando que la OT quede "Sin reparar".
    /// </summary>
    public class StockInsuficienteException_382QA : Exception
    {
        public List<BE_FaltanteStock_382QA> Faltantes { get; }

        public StockInsuficienteException_382QA(List<BE_FaltanteStock_382QA> faltantes)
            : base("No hay stock suficiente de uno o más repuestos.")
        {
            Faltantes = faltantes;
        }
    }

    /// <summary>
    /// CU-NEG-4 Diagnosticar equipo.
    /// </summary>
    public class BLL_Diagnostico_382QA
    {
        private const string Modulo = "Órdenes de trabajo";
        public const int LargoMaximoDetalle = 500;

        MAP_Diagnostico_382QA map_diagnostico;
        MAP_OT_382QA map_ot;
        BLL_Repuesto_382QA bll_repuesto;
        BLL_OT_382QA bll_ot;
        BLL_Bitacora_382QA bll_bitacora;
        BLL_DV_382QA bll_dv;

        public BLL_Diagnostico_382QA()
        {
            map_diagnostico = new MAP_Diagnostico_382QA();
            map_ot = new MAP_OT_382QA();
            bll_repuesto = new BLL_Repuesto_382QA();
            bll_ot = new BLL_OT_382QA();
            bll_bitacora = new BLL_Bitacora_382QA();
            bll_dv = new BLL_DV_382QA();
        }

        /// <summary>
        /// Paso 3: el técnico selecciona su OT y el sistema la pasa a "En diagnostico".
        /// Si ya estaba en diagnóstico (se había empezado antes), la deja como está.
        /// </summary>
        public BE_OTResumen_382QA IniciarDiagnostico_382QA(int nroOT)
        {
            BE_OTResumen_382QA ot = ObtenerOTAccesible_382QA(nroOT);

            if (ot.Estado == EstadosOT_382QA.EnDiagnostico)
                return ot;

            if (ot.Estado != EstadosOT_382QA.Recibida)
                throw new Exception($"La OT N° {nroOT} está en estado \"{ot.Estado}\": solo se diagnostican órdenes recibidas.");

            if (!map_ot.CambiarEstado_382QA(nroOT, EstadosOT_382QA.Recibida, EstadosOT_382QA.EnDiagnostico))
                throw new Exception($"La OT N° {nroOT} cambió de estado. Actualizá el listado.");

            bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaOrdenTrabajo, nroOT);
            RegistrarBitacora_382QA("Iniciar diagnóstico", 3);

            return ObtenerOTAccesible_382QA(nroOT);
        }

        /// <summary>
        /// Pasos 4 a 9 (y flujos alternativos 5.1.1 y 6.1.1).
        /// Valida los datos y el stock. Si falta stock y no se aceptó, lanza StockInsuficienteException_382QA con los faltantes.
        /// Con stock: calcula el total (mano de obra + precio × cantidad de cada repuesto), genera el presupuesto
        /// Pendiente y pasa la OT a "Presupuestado". Sin stock (aceptado): guarda el diagnóstico y pasa la OT a
        /// "Sin reparar", habilitada para entregarse sin reparar. Todo se guarda en una sola transacción.
        /// </summary>
        /// <returns>La OT actualizada (el vendedor la ve en su listado con el nuevo estado).</returns>
        public BE_OTResumen_382QA RegistrarDiagnostico_382QA(int nroOT, string detalle, decimal costoManoObra,
                                                 List<BE_DetalleRepuesto_382QA> repuestos, bool aceptarSinStock)
        {
            BE_OTResumen_382QA ot = ObtenerOTAccesible_382QA(nroOT);
            if (ot.Estado != EstadosOT_382QA.EnDiagnostico)
                throw new Exception($"La OT N° {nroOT} está en estado \"{ot.Estado}\": no se puede registrar el diagnóstico.");

            // Paso 5: campos obligatorios
            detalle = detalle?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(detalle))
                throw new Exception("El detalle del diagnóstico es obligatorio.");

            if (detalle.Length > LargoMaximoDetalle)
                throw new Exception($"El detalle del diagnóstico no puede superar los {LargoMaximoDetalle} caracteres.");

            if (costoManoObra < 0 || decimal.Round(costoManoObra, 2) != costoManoObra)
                throw new Exception("El costo de mano de obra no es válido (no negativo, máximo dos decimales).");

            repuestos ??= new List<BE_DetalleRepuesto_382QA>();
            var items = new List<(BE_DetalleRepuesto_382QA Detalle, BE_Repuesto_382QA Repuesto)>();
            foreach (BE_DetalleRepuesto_382QA item in repuestos)
            {
                if (item.CantidadRequerida <= 0)
                    throw new Exception($"La cantidad del repuesto {item.Codigo} debe ser mayor a cero.");

                BE_Repuesto_382QA repuesto = bll_repuesto.ConsultarPorCodigo_382QA(item.Codigo)
                    ?? throw new Exception($"El repuesto {item.Codigo} no existe o fue dado de baja.");

                if (items.Any(i => i.Repuesto.Codigo == repuesto.Codigo))
                    throw new Exception($"El repuesto {repuesto.Codigo} está cargado más de una vez.");

                item.Codigo = repuesto.Codigo;
                item.PrecioUnitario = repuesto.PrecioUnitario;   // precio vigente: queda fijo en el presupuesto
                item.CantidadUtilizada = 0;
                items.Add((item, repuesto));
            }

            if (items.Count == 0 && costoManoObra == 0)
                throw new Exception("El presupuesto no puede quedar en $0: cargá la mano de obra o al menos un repuesto.");

            // Paso 6: validación automática de stock
            List<BE_FaltanteStock_382QA> faltantes = items
                .Where(i => i.Detalle.CantidadRequerida > i.Repuesto.StockDisponible)
                .Select(i => new BE_FaltanteStock_382QA(i.Repuesto.Codigo, i.Repuesto.Descripcion,
                                                  i.Detalle.CantidadRequerida, i.Repuesto.StockDisponible))
                .ToList();

            if (faltantes.Count > 0 && !aceptarSinStock)
                throw new StockInsuficienteException_382QA(faltantes);

            bool sinStock = faltantes.Count > 0;
            var diagnostico = new BE_Diagnostico_382QA(0, detalle, costoManoObra, nroOT);

            // Paso 7: costo total y presupuesto (solo si hay stock)
            BE_Presupuesto_382QA? presupuesto = null;
            if (!sinStock)
            {
                decimal total = costoManoObra + items.Sum(i => i.Detalle.PrecioUnitario * i.Detalle.CantidadRequerida);
                presupuesto = new BE_Presupuesto_382QA(0, total, EstadosPresupuesto_382QA.Pendiente, null, nroOT);
            }

            // Pasos 8 / 6.1.1: estado de la OT, en la misma transacción que el diagnóstico y el presupuesto
            string estadoNuevo = sinStock ? EstadosOT_382QA.SinReparar : EstadosOT_382QA.Presupuestado;
            List<BE_DetalleRepuesto_382QA> detalles = items.Select(i => i.Detalle).ToList();
            var (idDiagnostico, idPresupuesto) = map_diagnostico.Registrar_382QA(
                diagnostico, detalles, presupuesto, EstadosOT_382QA.EnDiagnostico, estadoNuevo);

            bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaDiagnostico, idDiagnostico);
            foreach (BE_DetalleRepuesto_382QA d in detalles)
                bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaDetalleRepuesto, idDiagnostico, d.Codigo);
            if (idPresupuesto > 0)
                bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaPresupuesto, idPresupuesto);
            bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaOrdenTrabajo, nroOT);

            RegistrarBitacora_382QA("Diagnosticar equipo", 2);

            // Paso 9: la notificación al vendedor es el nuevo estado de la OT, visible en su listado
            return ObtenerOTAccesible_382QA(nroOT);
        }

        /// <summary>La OT tiene que existir y, si el usuario es un técnico, estar asignada a él.</summary>
        private BE_OTResumen_382QA ObtenerOTAccesible_382QA(int nroOT) => bll_ot.ObtenerOTAccesible_382QA(nroOT);

        private void RegistrarBitacora_382QA(string evento, int criticidad)
        {
            bll_bitacora.RegistrarBitacora_382QA(new SER_Bitacora_382QA(
                SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, Modulo, evento, criticidad));
        }
    }
}
