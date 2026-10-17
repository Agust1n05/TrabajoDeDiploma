namespace BE
{
    /// <summary>
    /// Entidad de negocio Repuesto. Maestro con ABM propio (FrmGestionRepuestos), que hace de
    /// inventario simple: el stock se carga y corrige a mano. CU-NEG-4 valida el stock y usa el
    /// precio para calcular el presupuesto; CU-NEG-5 descuenta las cantidades utilizadas.
    /// DER: Repuesto(*PK Codigo varchar(20), Descripcion varchar(100), PrecioUnitario decimal(18,2),
    ///               stockDisponible int, Activo bit)
    /// </summary>
    public class BE_Repuesto
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int StockDisponible { get; set; }
        public bool Activo { get; set; } = true;

        public BE_Repuesto()
        {
        }

        public BE_Repuesto(string codigo, string descripcion, decimal precioUnitario, int stockDisponible, bool activo = true)
        {
            Codigo = codigo;
            Descripcion = descripcion;
            PrecioUnitario = precioUnitario;
            StockDisponible = stockDisponible;
            Activo = activo;
        }
    }
}
