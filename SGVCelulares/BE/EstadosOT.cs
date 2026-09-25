namespace BE
{
  
    public static class EstadosOT
    {
        public const string Recibida = "Recibida";
    
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
