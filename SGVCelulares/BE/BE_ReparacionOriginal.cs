namespace BE
{
    /// <summary>
    /// Reparación original de un reclamo tal como la ve el técnico al evaluarlo (CU-NEG-8):
    /// falla de la OT, detalle del diagnóstico y repuestos que se utilizaron. Solo se usa para mostrar.
    /// </summary>
    public class BE_ReparacionOriginal
    {
        public int NroOT { get; set; }
        public string DescripcionFalla { get; set; }
        public string DetalleDiagnostico { get; set; }
        public List<BE_LineaReparacion> Repuestos { get; set; } = new List<BE_LineaReparacion>();
    }
}
