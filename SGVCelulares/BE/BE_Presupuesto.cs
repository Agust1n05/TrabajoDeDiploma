using System;

namespace BE
{
    /// <summary>
    /// Entidad de negocio Presupuesto. La genera CU-NEG-4 (Diagnosticar equipo) y la
    /// gestiona CU-NEG-2 (Gestionar aprobación de presupuesto).
    /// DER: Presupuesto(*PK idPresupuesto int, TotalPresupuesto decimal(18),
    ///                  EstadoPresupuesto varchar(50), FechaRespuesta date, FK NroOT int)
    /// </summary>
    public class BE_Presupuesto
    {
        public int IdPresupuesto { get; set; }
        public decimal TotalPresupuesto { get; set; }
        public string EstadoPresupuesto { get; set; }
        public DateTime FechaRespuesta { get; set; }
        public int NroOT { get; set; }

        public BE_Presupuesto()
        {
        }

        public BE_Presupuesto(int idPresupuesto, decimal totalPresupuesto, string estadoPresupuesto, DateTime fechaRespuesta, int nroOT)
        {
            IdPresupuesto = idPresupuesto;
            TotalPresupuesto = totalPresupuesto;
            EstadoPresupuesto = estadoPresupuesto;
            FechaRespuesta = fechaRespuesta;
            NroOT = nroOT;
        }
    }
}
