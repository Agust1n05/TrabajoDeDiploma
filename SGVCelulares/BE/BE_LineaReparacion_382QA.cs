namespace BE
{
    /// <summary>
    /// Repuesto del diagnóstico de una OT tal como lo ve el técnico al reparar (CU-NEG-5):
    /// lo aprobado en el presupuesto, lo que realmente usa y el stock actual del repuesto.
    /// Vista de DetalleRepuesto + Repuesto; se persiste solo CantidadUtilizada.
    /// </summary>
    public class BE_LineaReparacion_382QA
    {
        public int IdDiagnostico { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int CantidadRequerida { get; set; }
        public int CantidadUtilizada { get; set; }
        public int StockDisponible { get; set; }

        /// <summary>True si con el stock actual no alcanza para la cantidad utilizada.</summary>
        public bool FaltaStock => CantidadUtilizada > StockDisponible;
    }
}
