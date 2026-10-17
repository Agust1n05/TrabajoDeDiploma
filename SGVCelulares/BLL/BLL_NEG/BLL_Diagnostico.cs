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
    public class StockInsuficienteException : Exception
    {
        public List<BE_FaltanteStock> Faltantes { get; }

        public StockInsuficienteException(List<BE_FaltanteStock> faltantes)
            : base("No hay stock suficiente de uno o más repuestos.")
        {
            Faltantes = faltantes;
        }
    }

    /// <summary>
    /// CU-NEG-4 Diagnosticar equipo.
    /// </summary>
    public class BLL_Diagnostico
    {
        private const string Modulo = "Órdenes de trabajo";
        public const int LargoMaximoDetalle = 500;

        MAP_Diagnostico map_diagnostico;
        MAP_OT map_ot;
        BLL_Repuesto bll_repuesto;
        BLL_Tecnico bll_tecnico;
        BLL_Bitacora bll_bitacora;
        BLL_DV bll_dv;

        public BLL_Diagnostico()
        {
            map_diagnostico = new MAP_Diagnostico();
            map_ot = new MAP_OT();
            bll_repuesto = new BLL_Repuesto();
            bll_tecnico = new BLL_Tecnico();
            bll_bitacora = new BLL_Bitacora();
            bll_dv = new BLL_DV();
        }

        /// <summary>
        /// Paso 3: el técnico selecciona su OT y el sistema la pasa a "En diagnostico".
        /// Si ya estaba en diagnóstico (se había empezado antes), la deja como está.
        /// </summary>
        public BE_OTResumen IniciarDiagnostico(int nroOT)
        {
            BE_OTResumen ot = ObtenerOTAccesible(nroOT);

            if (ot.Estado == EstadosOT.EnDiagnostico)
                return ot;

            if (ot.Estado != EstadosOT.Recibida)
                throw new Exception($"La OT N° {nroOT} está en estado \"{ot.Estado}\": solo se diagnostican órdenes recibidas.");

            if (!map_ot.CambiarEstado(nroOT, EstadosOT.Recibida, EstadosOT.EnDiagnostico))
                throw new Exception($"La OT N° {nroOT} cambió de estado. Actualizá el listado.");

            bll_dv.ActualizarDVNegocio(BLL_DV.TablaOrdenTrabajo, nroOT);
            RegistrarBitacora("Iniciar diagnóstico", 3);

            return ObtenerOTAccesible(nroOT);
        }

        /// <summary>
        /// Pasos 4 a 9 (y flujos alternativos 5.1.1 y 6.1.1).
        /// Valida los datos y el stock. Si falta stock y no se aceptó, lanza StockInsuficienteException con los faltantes.
        /// Con stock: calcula el total (mano de obra + precio × cantidad de cada repuesto), genera el presupuesto
        /// Pendiente y pasa la OT a "Presupuestado". Sin stock (aceptado): guarda el diagnóstico y pasa la OT a
        /// "Sin reparar", habilitada para entregarse sin reparar. Todo se guarda en una sola transacción.
        /// </summary>
        /// <returns>La OT actualizada (el vendedor la ve en su listado con el nuevo estado).</returns>
        public BE_OTResumen RegistrarDiagnostico(int nroOT, string detalle, decimal costoManoObra,
                                                 List<BE_DetalleRepuesto> repuestos, bool aceptarSinStock)
        {
            BE_OTResumen ot = ObtenerOTAccesible(nroOT);
            if (ot.Estado != EstadosOT.EnDiagnostico)
                throw new Exception($"La OT N° {nroOT} está en estado \"{ot.Estado}\": no se puede registrar el diagnóstico.");

            // Paso 5: campos obligatorios
            detalle = detalle?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(detalle))
                throw new Exception("El detalle del diagnóstico es obligatorio.");

            if (detalle.Length > LargoMaximoDetalle)
                throw new Exception($"El detalle del diagnóstico no puede superar los {LargoMaximoDetalle} caracteres.");

            if (costoManoObra < 0 || decimal.Round(costoManoObra, 2) != costoManoObra)
                throw new Exception("El costo de mano de obra no es válido (no negativo, máximo dos decimales).");

            repuestos ??= new List<BE_DetalleRepuesto>();
            var items = new List<(BE_DetalleRepuesto Detalle, BE_Repuesto Repuesto)>();
            foreach (BE_DetalleRepuesto item in repuestos)
            {
                if (item.CantidadRequerida <= 0)
                    throw new Exception($"La cantidad del repuesto {item.Codigo} debe ser mayor a cero.");

                BE_Repuesto repuesto = bll_repuesto.ConsultarPorCodigo(item.Codigo)
                    ?? throw new Exception($"El repuesto {item.Codigo} no existe o fue dado de baja.");

                if (items.Any(i => i.Repuesto.Codigo == repuesto.Codigo))
                    throw new Exception($"El repuesto {repuesto.Codigo} está cargado más de una vez.");

                item.Codigo = repuesto.Codigo;
                item.CantidadUtilizada = 0;
                items.Add((item, repuesto));
            }

            if (items.Count == 0 && costoManoObra == 0)
                throw new Exception("El presupuesto no puede quedar en $0: cargá la mano de obra o al menos un repuesto.");

            // Paso 6: validación automática de stock
            List<BE_FaltanteStock> faltantes = items
                .Where(i => i.Detalle.CantidadRequerida > i.Repuesto.StockDisponible)
                .Select(i => new BE_FaltanteStock(i.Repuesto.Codigo, i.Repuesto.Descripcion,
                                                  i.Detalle.CantidadRequerida, i.Repuesto.StockDisponible))
                .ToList();

            if (faltantes.Count > 0 && !aceptarSinStock)
                throw new StockInsuficienteException(faltantes);

            bool sinStock = faltantes.Count > 0;
            var diagnostico = new BE_Diagnostico(0, detalle, costoManoObra, nroOT);

            // Paso 7: costo total y presupuesto (solo si hay stock)
            BE_Presupuesto? presupuesto = null;
            if (!sinStock)
            {
                decimal total = costoManoObra + items.Sum(i => i.Repuesto.PrecioUnitario * i.Detalle.CantidadRequerida);
                presupuesto = new BE_Presupuesto(0, total, EstadosPresupuesto.Pendiente, null, nroOT);
            }

            // Pasos 8 / 6.1.1: estado de la OT, en la misma transacción que el diagnóstico y el presupuesto
            string estadoNuevo = sinStock ? EstadosOT.SinReparar : EstadosOT.Presupuestado;
            List<BE_DetalleRepuesto> detalles = items.Select(i => i.Detalle).ToList();
            var (idDiagnostico, idPresupuesto) = map_diagnostico.Registrar(
                diagnostico, detalles, presupuesto, EstadosOT.EnDiagnostico, estadoNuevo);

            bll_dv.ActualizarDVNegocio(BLL_DV.TablaDiagnostico, idDiagnostico);
            foreach (BE_DetalleRepuesto d in detalles)
                bll_dv.ActualizarDVNegocio(BLL_DV.TablaDetalleRepuesto, idDiagnostico, d.Codigo);
            if (idPresupuesto > 0)
                bll_dv.ActualizarDVNegocio(BLL_DV.TablaPresupuesto, idPresupuesto);
            bll_dv.ActualizarDVNegocio(BLL_DV.TablaOrdenTrabajo, nroOT);

            RegistrarBitacora("Diagnosticar equipo", 2);

            // Paso 9: la notificación al vendedor es el nuevo estado de la OT, visible en su listado
            return ObtenerOTAccesible(nroOT);
        }

        /// <summary>
        /// La OT tiene que existir y, si el usuario logueado es un técnico, estar asignada a él.
        /// Un usuario con permiso que no es técnico (ej. el administrador) puede trabajar sobre cualquier OT.
        /// </summary>
        private BE_OTResumen ObtenerOTAccesible(int nroOT)
        {
            BE_OTResumen ot = map_ot.ConsultarResumenPorNro(nroOT)
                ?? throw new Exception($"No existe la OT N° {nroOT}.");

            BE_Tecnico? tecnicoSesion = bll_tecnico.ConsultarTecnicoDeSesion();
            if (tecnicoSesion != null && ot.DniTecnico != tecnicoSesion.Dni)
                throw new Exception($"La OT N° {nroOT} está asignada a otro técnico.");

            return ot;
        }

        private void RegistrarBitacora(string evento, int criticidad)
        {
            bll_bitacora.RegistrarBitacora(new SER_Bitacora(
                SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, Modulo, evento, criticidad));
        }
    }
}
