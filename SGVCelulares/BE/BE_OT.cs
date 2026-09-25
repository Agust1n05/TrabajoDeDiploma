namespace BE
{
 
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
