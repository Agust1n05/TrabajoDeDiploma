namespace BE
{
    /// <summary>Medios de pago aceptados (CU-NEG-7: solo tarjeta). Coinciden con el CHECK de Factura.MedioPago.</summary>
    public static class MediosPago
    {
        public const string Debito = "Debito";
        public const string Credito = "Credito";

        public static readonly string[] Todos = { Debito, Credito };
    }

    /// <summary>
    /// Factura que se genera al cobrar y entregar un equipo reparado (CU-NEG-3 «extend» CU-NEG-7).
    /// De la tarjeta solo se guardan el tipo (débito/crédito) y los últimos 4 dígitos: nunca el número completo ni el CVV.
    /// DER: Factura(*PK NroFactura int identity, FK NroOT int único, MontoCobrado decimal(18,2),
    ///              MedioPago varchar(10), UltimosDigitos char(4), FechaEntrega date)
    /// </summary>
    public class BE_Factura
    {
        public int NroFactura { get; set; }
        public int NroOT { get; set; }
        public decimal MontoCobrado { get; set; }
        public string MedioPago { get; set; }
        public string UltimosDigitos { get; set; }
        public DateTime FechaEntrega { get; set; }

        public BE_Factura()
        {
        }

        public BE_Factura(int nroFactura, int nroOT, decimal montoCobrado, string medioPago, string ultimosDigitos, DateTime fechaEntrega)
        {
            NroFactura = nroFactura;
            NroOT = nroOT;
            MontoCobrado = montoCobrado;
            MedioPago = medioPago;
            UltimosDigitos = ultimosDigitos;
            FechaEntrega = fechaEntrega;
        }
    }
}
