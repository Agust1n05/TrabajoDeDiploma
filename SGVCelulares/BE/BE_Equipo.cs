namespace BE
{
    /// <summary>
    /// Entidad de negocio Equipo (celular). Maestro con ABM propio (FrmGestionEquipos).
    /// Se reutiliza entre órdenes de trabajo y se identifica por su IMEI.
    /// No guarda dueño ni falla: el cliente y la descripción de la falla pertenecen a cada OT.
    /// DER: Equipo(*PK idEquipo int identity, marca varchar(50), modelo varchar(50), imei char(15) único, Activo bit)
    /// </summary>
    public class BE_Equipo
    {
        public int IdEquipo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Imei { get; set; }
        public bool Activo { get; set; } = true;

        public BE_Equipo()
        {
        }

        public BE_Equipo(int idEquipo, string marca, string modelo, string imei, bool activo = true)
        {
            IdEquipo = idEquipo;
            Marca = marca;
            Modelo = modelo;
            Imei = imei;
            Activo = activo;
        }
    }
}
