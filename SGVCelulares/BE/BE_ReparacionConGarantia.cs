namespace BE
{
    /// <summary>
    /// OT entregada con garantía de un cliente, sobre la que se puede registrar un reclamo (CU-NEG-7 paso 1).
    /// Solo se usa para mostrar y validar; no se persiste.
    /// </summary>
    public class BE_ReparacionConGarantia
    {
        public int NroOT { get; set; }
        public string DniCliente { get; set; }
        public int IdEquipo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Imei { get; set; }
        public string DescripcionFalla { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public string Equipo => $"{Marca} {Modelo}";
    }
}
