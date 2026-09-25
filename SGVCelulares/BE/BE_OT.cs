namespace BE
{
    /// <summary>
    /// Entidad de negocio Orden de Trabajo. Eje central de PN-01: la usan CU-NEG-1
    /// (Gestionar recepción), CU-NEG-2 (Gestionar aprobación de presupuesto),
    /// CU-NEG-3 (Gestionar entrega), CU-NEG-4 (Diagnosticar equipo) y CU-NEG-5 (Reparar equipo).
    /// DER: OrdenDeTrabajo(*PK NroOT int, Estado varchar(max), FK dni varchar(50), FK idEquipo varchar(50))
    ///
    /// Estados observados en la especificación de CU (N02):
    /// "Recibida" -> "En Diagnóstico" -> "Presupuestado" -> ("Rechazado" | "En Reparación")
    /// -> "Reparado" -> ("Entregado" | "Entregado sin reparar")
    ///
    /// NOTA: el DER no tiene un campo persistido para el técnico asignado, aunque
    /// BLL_OT_382QA expone AsignarTecnico(BE_Tecnico, BE_OT). Falta agregar, probablemente,
    /// un FK IdTecnico a esta tabla/entidad — queda pendiente de definir.
    /// </summary>
    public class BE_OT
    {
        public int NroOT { get; set; }
        public string Estado { get; set; }
        public string Dni { get; set; }
        public string IdEquipo { get; set; }

        public string DniTecnico { get; set; }

        public BE_OT()
        {
        }

        public BE_OT(int nroOT, string estado, string dni, string idEquipo, string dniTecnico)
        {
            NroOT = nroOT;
            Estado = estado;
            Dni = dni;
            IdEquipo = idEquipo;
            DniTecnico = dniTecnico;
        }
    }
}
