namespace BE
{
    /// <summary>
    /// Entidad de negocio Cliente. Maestro con ABM propio (FrmGestionClientes) y usado en
    /// CU-NEG-1 (Gestionar recepción de equipo) y su extend CU-NEG-6 (Registrar nuevo cliente).
    /// DER: Cliente(*PK dni varchar(8), Nombre varchar(50), Apellido varchar(50), Telefono varchar(20), Activo bit)
    /// </summary>
    public class BE_Cliente
    {
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public bool Activo { get; set; } = true;

        public BE_Cliente()
        {
        }

        public BE_Cliente(string dni, string nombre, string apellido, string telefono, bool activo = true)
        {
            Dni = dni;
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
            Activo = activo;
        }
    }
}
