namespace BE
{
    /// <summary>
    /// Datos de la tarjeta que ingresa el vendedor para el cobro (CU-NEG-3 Gestionar entrega). Solo viven en memoria durante el cobro:
    /// no se persisten. Del comprobante de cobro queda únicamente el tipo y los últimos 4 dígitos.
    /// </summary>
    public class BE_DatosTarjeta
    {
        /// <summary>MediosPago.Debito o MediosPago.Credito.</summary>
        public string? Tipo { get; set; }
        public string Numero { get; set; } = "";
        public string Titular { get; set; } = "";
        /// <summary>Vencimiento como figura en la tarjeta: MM/AA.</summary>
        public string Vencimiento { get; set; } = "";
        public string Cvv { get; set; } = "";

        /// <summary>Número sin espacios ni guiones.</summary>
        public string NumeroLimpio => new string((Numero ?? "").Where(c => c != ' ' && c != '-').ToArray());

        public string UltimosDigitos => NumeroLimpio.Length >= 4 ? NumeroLimpio[^4..] : NumeroLimpio;
    }
}
