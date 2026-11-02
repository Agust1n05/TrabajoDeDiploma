namespace BE
{
    /// <summary>
    /// Vista de un reclamo con los datos de su OT original, cliente, equipo, técnico y garantía.
    /// Solo se usa para mostrar (listado de reclamos); no se persiste.
    /// </summary>
    public class BE_ReclamoResumen
    {
        public int NroReclamo { get; set; }
        public int NroOT { get; set; }
        public string Motivo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaReclamo { get; set; }
        public string Estado { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public string DniCliente { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }

        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Imei { get; set; }

        /// <summary>Null si el reclamo no tiene técnico asignado (rechazado por garantía vencida).</summary>
        public string? DniTecnico { get; set; }
        public string? NombreTecnico { get; set; }
        public string? ApellidoTecnico { get; set; }

        /// <summary>Null hasta la evaluación técnica (CU-NEG-8).</summary>
        public bool? FallaCubierta { get; set; }
        public string? DetalleEvaluacion { get; set; }

        // Textos armados para la grilla
        public string Cliente => $"{ApellidoCliente}, {NombreCliente}";
        public string Equipo => $"{Marca} {Modelo}";
        public string Tecnico => DniTecnico == null ? "-" : $"{ApellidoTecnico}, {NombreTecnico}";
    }
}
