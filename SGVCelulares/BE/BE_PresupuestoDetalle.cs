namespace BE
{
    /// <summary>
    /// Vista del presupuesto de una OT con el diagnóstico que lo originó y sus líneas de repuestos.
    /// La usa CU-NEG-2 para mostrarle al vendedor qué comunicarle al cliente. No se persiste.
    /// </summary>
    public class BE_PresupuestoDetalle
    {
        public int NroOT { get; set; }
        public string DetalleDiagnostico { get; set; }
        public decimal CostoManoObra { get; set; }

        public int IdPresupuesto { get; set; }
        public decimal TotalPresupuesto { get; set; }
        public string EstadoPresupuesto { get; set; }
        public DateTime? FechaRespuesta { get; set; }

        public List<BE_LineaPresupuesto> Lineas { get; set; } = new List<BE_LineaPresupuesto>();
    }

    /// <summary>Repuesto presupuestado: precio fijado en el diagnóstico × cantidad requerida.</summary>
    public class BE_LineaPresupuesto
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal => PrecioUnitario * Cantidad;
    }
}
