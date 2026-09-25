namespace BE
{
    /// <summary>
    /// Entidad de negocio Diagnóstico, registrada por el Técnico en CU-NEG-4 (Diagnosticar equipo).
    /// DER: Diagnostico(*PK idDiagnostico int, detalle varchar(50),
    ///                  costo_mano_obra varchar(50) [sic, ver nota], FK NroOT int)
    ///
    /// NOTA: el DER tipa "costo_mano_obra" como varchar(50), pero al ser un importe monetario
    /// se modela acá como decimal; conviene corregir el tipo de dato en el DER/tabla real.
    /// </summary>
    public class BE_Diagnostico
    {
        public int IdDiagnostico { get; set; }
        public string Detalle { get; set; }
        public decimal CostoManoObra { get; set; }
        public int NroOT { get; set; }

        public BE_Diagnostico()
        {
        }

        public BE_Diagnostico(int idDiagnostico, string detalle, decimal costoManoObra, int nroOT)
        {
            IdDiagnostico = idDiagnostico;
            Detalle = detalle;
            CostoManoObra = costoManoObra;
            NroOT = nroOT;
        }
    }
}
