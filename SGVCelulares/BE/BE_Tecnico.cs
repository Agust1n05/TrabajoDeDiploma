namespace BE
{
    /// <summary>
    /// Entidad de negocio Técnico. Maestro con ABM propio (FrmGestionTecnicos).
    /// Su DNI es el mismo que el de su usuario del sistema (rol Tecnico): así se identifica
    /// al técnico logueado para mostrarle sus OT (CU-NEG-4 y CU-NEG-5).
    /// Disponible: entra o no en la asignación automática de OT (CU-NEG-1).
    /// Activo: baja lógica.
    /// DER: Tecnico(*PK/FK Dni varchar(8), Nombre varchar(50), Apellido varchar(50), Disponible bit, Activo bit)
    /// </summary>
    public class BE_Tecnico
    {
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public bool Disponible { get; set; } = true;
        public bool Activo { get; set; } = true;

        public BE_Tecnico()
        {
        }

        public BE_Tecnico(string dni, string nombre, string apellido, bool disponible, bool activo = true)
        {
            Dni = dni;
            Nombre = nombre;
            Apellido = apellido;
            Disponible = disponible;
            Activo = activo;
        }
    }
}
