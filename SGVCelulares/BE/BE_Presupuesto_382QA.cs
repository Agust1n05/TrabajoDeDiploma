namespace BE
{
    /// <summary>
    /// Entidad de negocio Presupuesto. Lo genera CU-NEG-4 (Diagnosticar equipo) en estado Pendiente
    /// y CU-NEG-2 (Gestionar aprobación de presupuesto) registra la respuesta del cliente.
    /// DER: Presupuesto(*PK IdPresupuesto int identity, TotalPresupuesto decimal(18,2),
    ///                  EstadoPresupuesto varchar(20), FechaRespuesta date NULL, FK NroOT int único)
    /// </summary>
    public class BE_Presupuesto_382QA
    {
        public int IdPresupuesto { get; set; }
        public decimal TotalPresupuesto { get; set; }
        public string EstadoPresupuesto { get; set; }
        /// <summary>Null mientras el presupuesto está Pendiente.</summary>
        public DateTime? FechaRespuesta { get; set; }
        public int NroOT { get; set; }

        public BE_Presupuesto_382QA()
        {
        }

        public BE_Presupuesto_382QA(int idPresupuesto, decimal totalPresupuesto, string estadoPresupuesto, DateTime? fechaRespuesta, int nroOT)
        {
            IdPresupuesto = idPresupuesto;
            TotalPresupuesto = totalPresupuesto;
            EstadoPresupuesto = estadoPresupuesto;
            FechaRespuesta = fechaRespuesta;
            NroOT = nroOT;
        }
    }

    /// <summary>Estados de un presupuesto. Coinciden con el CHECK de Presupuesto.EstadoPresupuesto.</summary>
    public static class EstadosPresupuesto_382QA
    {
        public const string Pendiente = "Pendiente";
        public const string Aprobado = "Aprobado";
        public const string Rechazado = "Rechazado";
    }
}
