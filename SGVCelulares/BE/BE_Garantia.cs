using System;

namespace BE
{
    /// <summary>
    /// Entidad de negocio Garantía. Aparece en el modelo conceptual de PN-01 (relación
    /// "Otorga" con OrdenDeTrabajo) y en la postcondición de CU-NEG-3 (Gestionar entrega),
    /// pero todavía no tiene especificación de CU ni DER propio (depende de CU-NEG-7).
    /// PROPUESTA a confirmar cuando se especifique CU-NEG-7.
    /// </summary>
    public class BE_Garantia
    {
        public int IdGarantia { get; set; }
        public int NroOT { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public BE_Garantia()
        {
        }

        public BE_Garantia(int idGarantia, int nroOT, DateTime fechaInicio, DateTime fechaFin)
        {
            IdGarantia = idGarantia;
            NroOT = nroOT;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }
    }
}
