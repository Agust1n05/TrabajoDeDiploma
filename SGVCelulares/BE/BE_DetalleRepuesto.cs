namespace BE
{
    /// <summary>
    /// Repuesto requerido en un diagnóstico (CU-NEG-4) y cantidad finalmente utilizada en la reparación (CU-NEG-5).
    /// Guarda el precio unitario vigente al diagnosticar, para que el presupuesto no cambie si después cambia el precio.
    /// DER: DetalleRepuesto(*PK/FK IdDiagnostico int, *PK/FK Codigo varchar(20), PrecioUnitario decimal(18,2),
    ///                      cantidadRequerida int, CantidadUtilizada int)
    /// </summary>
    public class BE_DetalleRepuesto
    {
        public string Codigo { get; set; }
        public int IdDiagnostico { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int CantidadRequerida { get; set; }
        public int CantidadUtilizada { get; set; }

        public BE_DetalleRepuesto()
        {
        }

        public BE_DetalleRepuesto(string codigo, int idDiagnostico, int cantidadRequerida, int cantidadUtilizada, decimal precioUnitario = 0)
        {
            Codigo = codigo;
            IdDiagnostico = idDiagnostico;
            CantidadRequerida = cantidadRequerida;
            CantidadUtilizada = cantidadUtilizada;
            PrecioUnitario = precioUnitario;
        }
    }
}
