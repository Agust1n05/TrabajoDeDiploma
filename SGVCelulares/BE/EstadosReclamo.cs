namespace BE
{
    /// <summary>
    /// Estados posibles de un reclamo por garantía (PN-02).
    /// Los valores coinciden con el CHECK de la columna Reclamo.Estado.
    /// </summary>
    public static class EstadosReclamo
    {
        public const string Pendiente = "Pendiente";
        public const string EnEvaluacion = "En evaluacion";
        public const string Rechazado = "Rechazado";                       // falta devolver el equipo sin reparar
        public const string Aprobado = "Aprobado";
        public const string EnEsperaRepuesto = "En espera de repuesto";
        public const string Reparado = "Reparado";
        public const string Resuelto = "Resuelto";
        public const string EntregadoSinReparar = "Entregado sin reparar";

        public static readonly string[] Todos =
        {
            Pendiente, EnEvaluacion, Rechazado, Aprobado, EnEsperaRepuesto, Reparado, Resuelto, EntregadoSinReparar
        };

        /// <summary>
        /// Estados de un reclamo cuyo equipo todavía no se devolvió al cliente.
        /// Mientras un equipo tenga un reclamo abierto no se puede recibir en una OT nueva ni dar de baja.
        /// Coinciden con el filtro del índice UX_Reclamo_OTAbierta.
        /// </summary>
        public static readonly string[] Abiertos =
        {
            Pendiente, EnEvaluacion, Rechazado, Aprobado, EnEsperaRepuesto, Reparado
        };

        /// <summary>Estados en que el reclamo está en manos del técnico. No se puede dar de baja un técnico que los tenga.</summary>
        public static readonly string[] EnCurso = { EnEvaluacion, Aprobado, EnEsperaRepuesto };
    }
}
