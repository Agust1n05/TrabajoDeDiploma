namespace BE
{
    /// <summary>
    /// Estados posibles de una Orden de Trabajo (PN-01).
    /// Los valores coinciden con el CHECK de la columna OrdenDeTrabajo.Estado.
    /// </summary>
    public static class EstadosOT
    {
        public const string Recibida = "Recibida";
        public const string EnDiagnostico = "En diagnostico";
        public const string Presupuestado = "Presupuestado";
        public const string EnReparacion = "En reparacion";
        public const string Reparado = "Reparado";
        public const string Entregado = "Entregado";
        public const string SinReparar = "Sin reparar";              // sin stock o presupuesto rechazado, falta entregar
        public const string EntregadoSinReparar = "Entregado sin reparar";

        public static readonly string[] Todos =
        {
            Recibida, EnDiagnostico, Presupuestado, EnReparacion, Reparado, Entregado, SinReparar, EntregadoSinReparar
        };

        /// <summary>
        /// Estados que todavía requieren trabajo del técnico.
        /// Se usan para asignar la OT nueva al técnico disponible con menos OT en curso (CU-NEG-1).
        /// </summary>
        public static readonly string[] EnCurso =
        {
            Recibida, EnDiagnostico, Presupuestado, EnReparacion
        };
    }
}
