namespace BE
{
    /// <summary>Medios de pago aceptados (solo tarjeta). Coinciden con el CHECK de ComprobanteCobro.MedioPago.</summary>
    public static class MediosPago
    {
        public const string Debito = "Debito";
        public const string Credito = "Credito";

        public static readonly string[] Todos = { Debito, Credito };
    }

    /// <summary>
    /// Comprobante de cobro que se genera al cobrar y entregar un equipo reparado (CU-NEG-3 Gestionar entrega).
    /// De la tarjeta solo se guardan el tipo (débito/crédito) y los últimos 4 dígitos: nunca el número completo ni el CVV.
    /// DER: ComprobanteCobro(*PK NroComprobante int identity, FK NroOT int único, MontoCobrado decimal(18,2),
    ///                       MedioPago varchar(10), UltimosDigitos char(4), FechaEntrega date)
    /// </summary>
    public class BE_ComprobanteCobro
    {
        public int NroComprobante { get; set; }
        public int NroOT { get; set; }
        public decimal MontoCobrado { get; set; }
        public string MedioPago { get; set; }
        public string UltimosDigitos { get; set; }
        public DateTime FechaEntrega { get; set; }

        public BE_ComprobanteCobro()
        {
        }

        public BE_ComprobanteCobro(int nroComprobante, int nroOT, decimal montoCobrado, string medioPago, string ultimosDigitos, DateTime fechaEntrega)
        {
            NroComprobante = nroComprobante;
            NroOT = nroOT;
            MontoCobrado = montoCobrado;
            MedioPago = medioPago;
            UltimosDigitos = ultimosDigitos;
            FechaEntrega = fechaEntrega;
        }
    }
}
