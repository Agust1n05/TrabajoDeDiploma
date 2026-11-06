namespace BE
{
    /// <summary>
    /// Entidad de negocio Orden de Trabajo (PN-01). Representa el ciclo de una reparación
    /// desde la recepción del equipo (CU-NEG-1) hasta su entrega (CU-NEG-3).
    /// DER: OrdenDeTrabajo(*PK NroOT int identity, Estado varchar(30), FK dni varchar(8),
    ///                     FK idEquipo int, FK DniTecnico varchar(8), DescripcionFalla varchar(200))
    /// </summary>
    public class BE_OT_382QA
    {
        public int NroOT { get; set; }
        public string Estado { get; set; }
        public string Dni { get; set; }
        public int IdEquipo { get; set; }
        public string DniTecnico { get; set; }
        public string DescripcionFalla { get; set; }

        public BE_OT_382QA()
        {
        }

        public BE_OT_382QA(int nroOT, string estado, string dni, int idEquipo, string dniTecnico, string descripcionFalla)
        {
            NroOT = nroOT;
            Estado = estado;
            Dni = dni;
            IdEquipo = idEquipo;
            DniTecnico = dniTecnico;
            DescripcionFalla = descripcionFalla;
        }
    }
}
