namespace BE
{
    /// <summary>
    /// Repuesto pedido en un diagnóstico que no tiene stock suficiente (CU-NEG-4, flujo alternativo 6.1.1).
    /// Solo informa; no se persiste.
    /// </summary>
    public class BE_FaltanteStock_382QA
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int CantidadRequerida { get; set; }
        public int StockDisponible { get; set; }

        public BE_FaltanteStock_382QA(string codigo, string descripcion, int cantidadRequerida, int stockDisponible)
        {
            Codigo = codigo;
            Descripcion = descripcion;
            CantidadRequerida = cantidadRequerida;
            StockDisponible = stockDisponible;
        }

        public override string ToString() =>
            $"{Codigo} - {Descripcion}: se piden {CantidadRequerida}, hay {StockDisponible}";
    }
}
