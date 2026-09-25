namespace BE
{
    /// <summary>
    /// Estados posibles de una Orden de Trabajo, según PN-01.
    /// Fuente única de verdad: usar estas constantes en vez de strings sueltos
    /// en BLL, MAP o el form, para no desincronizar el código con la letra del CU.
    ///
    /// Flujo confirmado por el diagrama de actividades DA_ReparacionDeCelular:
    ///
    ///   Recibida -> EnDiagnostico -> [¿repuestos?] --No--> EntregadoSinReparar (fin)
    ///                              --Si--> Presupuestado -> [¿acepta?] --No--> EntregadoSinReparar (fin)
    ///                                                     --Si--> EnReparacion -> Reparado -> Entregado
    ///
    /// OJO: no existe un estado "Rechazado" separado. Tanto el rechazo del presupuesto
    /// como la falta de stock terminan en el mismo estado: "Entregado sin reparar".
    /// </summary>
    public static class EstadosOT
    {
        public const string Recibida = "Recibida";
        // TODO: confirmar ortografía exacta. PN-01 (texto) dice "En Diagnóstico",
        // pero el datastore del diagrama muestra "En diagnostico" (sin mayúscula ni tilde).
        // Como es comparación de string contra la base, hay que usar la que 
        // efectivamente se inserte en las filas - si no coinciden, el filtro no matchea nunca.
        public const string EnDiagnostico = "En diagnostico";
        public const string Presupuestado = "Presupuestado";
        public const string EntregadoSinReparar = "Entregado sin reparar";
        public const string EnReparacion = "En reparacion";
        public const string Reparado = "Reparado";
        public const string Entregado = "Entregado";

        public static readonly string[] Todos =
        {
            Recibida, EnDiagnostico, Presupuestado, EntregadoSinReparar, EnReparacion, Reparado, Entregado
        };
    }
}
