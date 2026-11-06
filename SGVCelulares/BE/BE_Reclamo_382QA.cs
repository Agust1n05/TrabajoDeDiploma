namespace BE
{
    /// <summary>
    /// Entidad de negocio Reclamo por garantía (PN-02). Va desde que el cliente presenta el equipo (CU-NEG-7)
    /// hasta su entrega (CU-NEG-10).
    /// DER: Reclamo(*PK NroReclamo int identity, FK NroOT int → Garantia, Motivo varchar(100), Descripcion varchar(200),
    ///              FechaReclamo date, Estado varchar(30), FK DniTecnico varchar(8) null, FallaCubierta bit null,
    ///              DetalleEvaluacion varchar(500) null)
    /// </summary>
    public class BE_Reclamo_382QA
    {
        public const int LargoMaximoMotivo = 100;
        public const int LargoMaximoDescripcion = 200;
        public const int LargoMaximoDetalleEvaluacion = 500;

        public int NroReclamo { get; set; }
        public int NroOT { get; set; }
        public string Motivo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaReclamo { get; set; }
        public string Estado { get; set; }
        /// <summary>Null si el reclamo se rechazó por garantía vencida (no llega al taller).</summary>
        public string? DniTecnico { get; set; }
        /// <summary>Null hasta la evaluación técnica (CU-NEG-8).</summary>
        public bool? FallaCubierta { get; set; }
        public string? DetalleEvaluacion { get; set; }

        public BE_Reclamo_382QA()
        {
        }

        public BE_Reclamo_382QA(int nroOT, string motivo, string descripcion, DateTime fechaReclamo, string estado)
        {
            NroOT = nroOT;
            Motivo = motivo;
            Descripcion = descripcion;
            FechaReclamo = fechaReclamo;
            Estado = estado;
        }
    }
}
