namespace BE
{
    /// <summary>
    /// Entidad de negocio Repuesto (catálogo/stock). Usada en CU-NEG-4 (Diagnosticar equipo,
    /// vía DescontarStock/ValidarRepuestos) y CU-NEG-5 (Reparar equipo, descuento de stock).
    /// DER: Repuesto(*PK Codigo varchar(50), stockDisponible int)
    /// </summary>
    public class BE_Repuesto
    {
        public string Codigo { get; set; }
        public int StockDisponible { get; set; }

        public BE_Repuesto()
        {
        }

        public BE_Repuesto(string codigo, int stockDisponible)
        {
            Codigo = codigo;
            StockDisponible = stockDisponible;
        }
    }
}
