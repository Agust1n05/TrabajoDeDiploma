using System;

namespace BE
{
    /// <summary>
    /// Entidad de negocio Factura. Aparece en el modelo conceptual de PN-01 (relación
    /// "Genera" con OrdenDeTrabajo) y en la postcondición de CU-NEG-3 (Gestionar entrega:
    /// "...quedan registrados el cobro, la factura y la garantía"), pero CU-NEG-7
    /// (Realizar cobro) todavía no tiene especificación de CU ni DER propio.
    /// PROPUESTA a confirmar cuando se especifique CU-NEG-7.
    /// </summary>
    public class BE_Factura
    {
        public int IdFactura { get; set; }
        public int NroOT { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }

        public BE_Factura()
        {
        }

        public BE_Factura(int idFactura, int nroOT, DateTime fecha, decimal monto)
        {
            IdFactura = idFactura;
            NroOT = nroOT;
            Fecha = fecha;
            Monto = monto;
        }
    }
}
