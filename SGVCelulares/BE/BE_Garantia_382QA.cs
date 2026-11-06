namespace BE
{
    /// <summary>
    /// Garantía de un equipo reparado y entregado (CU-NEG-3): vence 90 días después de la entrega.
    /// La usará PN-02 (reclamo por garantía) para saber si sigue vigente.
    /// DER: Garantia(*PK/FK NroOT int, FechaVencimiento date)
    /// </summary>
    public class BE_Garantia_382QA
    {
        public const int DiasGarantia = 90;

        public int NroOT { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public bool EstaVigente_382QA(DateTime fecha) => fecha.Date <= FechaVencimiento.Date;

        public BE_Garantia_382QA()
        {
        }

        public BE_Garantia_382QA(int nroOT, DateTime fechaVencimiento)
        {
            NroOT = nroOT;
            FechaVencimiento = fechaVencimiento;
        }
    }
}
