namespace BE
{
    /// <summary>
    /// Entidad de negocio Técnico. Se referencia en BLL_OT_382QA
    /// (AsignarTecnico(BE_Tecnico, BE_OT), ConsultarTecnicoDisponible(): BE_Tecnico),
    /// pero todavía no tiene un DER propio en la carpeta (no hay tabla Tecnico especificada).
    ///
    /// PROPUESTA a confirmar: si el Técnico es en definitiva un SER_Usuario_382QA con
    /// Rol "Técnico" (mismo criterio que Vendedor), este BE podría no ser necesario y
    /// BLL_OT debería trabajar directamente con SER_Usuario_382QA. Mientras tanto, se modela
    /// como entidad mínima con los datos de identificación + disponibilidad.
    /// </summary>
    public class BE_Tecnico
    {
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public bool Disponible { get; set; }

        public BE_Tecnico()
        {
        }

        public BE_Tecnico(string dni, string nombre, string apellido, bool disponible)
        {
            Dni = dni;
            Nombre = nombre;
            Apellido = apellido;
            Disponible = disponible;
        }
    }
}
