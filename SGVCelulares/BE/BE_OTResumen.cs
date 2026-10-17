namespace BE
{
    /// <summary>
    /// Vista de una orden de trabajo con los datos de su cliente, equipo, técnico y presupuesto.
    /// Solo se usa para mostrar (grilla y detalle del listado de OT); no se persiste.
    /// </summary>
    public class BE_OTResumen
    {
        public int NroOT { get; set; }
        public string Estado { get; set; }
        public string DescripcionFalla { get; set; }

        public string DniCliente { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string TelefonoCliente { get; set; }

        public int IdEquipo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Imei { get; set; }

        public string DniTecnico { get; set; }
        public string NombreTecnico { get; set; }
        public string ApellidoTecnico { get; set; }

        /// <summary>Null mientras la OT no tiene presupuesto.</summary>
        public decimal? TotalPresupuesto { get; set; }
        public string? EstadoPresupuesto { get; set; }

        // Textos armados para la grilla
        public string Cliente => $"{ApellidoCliente}, {NombreCliente}";
        public string Equipo => $"{Marca} {Modelo}";
        public string Tecnico => $"{ApellidoTecnico}, {NombreTecnico}";
    }
}
