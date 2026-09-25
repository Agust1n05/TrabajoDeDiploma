namespace BE
{
    /// <summary>
    /// Entidad de negocio DetalleRepuesto: relaciona un Diagnóstico con los Repuestos
    /// que requiere, registrada en CU-NEG-4 (Diagnosticar equipo) y consumida en
    /// CU-NEG-5 (Reparar equipo) para descontar stock.
    /// DER: DetalleRepuesto(*PK Codigo varchar(50) [FK Repuesto], *PK idDiagnostico int [FK Diagnostico],
    ///                      cantidadRequerida int, CantidadUtilizada int)
    /// </summary>
    public class BE_DetalleRepuesto
    {
        public string Codigo { get; set; }
        public int IdDiagnostico { get; set; }
        public int CantidadRequerida { get; set; }
        public int CantidadUtilizada { get; set; }

        public BE_DetalleRepuesto()
        {
        }

        public BE_DetalleRepuesto(string codigo, int idDiagnostico, int cantidadRequerida, int cantidadUtilizada)
        {
            Codigo = codigo;
            IdDiagnostico = idDiagnostico;
            CantidadRequerida = cantidadRequerida;
            CantidadUtilizada = cantidadUtilizada;
        }
    }
}
