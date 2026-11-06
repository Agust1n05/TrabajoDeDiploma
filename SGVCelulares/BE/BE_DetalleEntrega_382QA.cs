namespace BE
{
    /// <summary>
    /// Lo que el vendedor necesita para entregar una OT (CU-NEG-3). No se persiste.
    ///   Reparado    → se cobra: mano de obra + repuestos efectivamente utilizados en la reparación.
    ///   Sin reparar → se entrega sin cobro ni garantía; PresupuestoRechazado indica por qué no se reparó.
    /// </summary>
    public class BE_DetalleEntrega_382QA
    {
        public BE_OTResumen_382QA OT { get; set; }

        public bool RequiereCobro => OT.Estado == EstadosOT_382QA.Reparado;

        public decimal CostoManoObra { get; set; }
        /// <summary>Repuestos utilizados (Cantidad = CantidadUtilizada), con el precio fijado en el diagnóstico.</summary>
        public List<BE_LineaPresupuesto_382QA> Lineas { get; set; } = new List<BE_LineaPresupuesto_382QA>();
        public decimal? TotalPresupuesto { get; set; }

        public decimal MontoACobrar => CostoManoObra + Lineas.Sum(l => l.Subtotal);

        /// <summary>
        /// Motivo de una OT "Sin reparar": true si el cliente rechazó el presupuesto;
        /// false si no hubo presupuesto porque faltaba stock al diagnosticar.
        /// </summary>
        public bool PresupuestoRechazado => OT.EstadoPresupuesto == EstadosPresupuesto_382QA.Rechazado;
    }

    /// <summary>Resultado de la entrega: la OT actualizada y, si hubo cobro, el comprobante de cobro y la garantía.</summary>
    public class BE_ResultadoEntrega_382QA
    {
        public BE_OTResumen_382QA OT { get; set; }
        public BE_ComprobanteCobro_382QA? ComprobanteCobro { get; set; }
        public BE_Garantia_382QA? Garantia { get; set; }
    }
}
