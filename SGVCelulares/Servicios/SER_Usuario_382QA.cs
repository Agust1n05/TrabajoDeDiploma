namespace Servicio
{
    public class SER_Usuario_382QA
    {
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public bool Bloqueo { get; set; }
        public bool Activo { get; set; }
        public int CantIntentos { get; set; }
        public SER_Rol_382QA Rol { get; set; }
        public string IdIdioma { get; set; }
        public SER_Usuario_382QA() { }
        public SER_Usuario_382QA(string dni)
        {
            Dni = dni;
        }
        public SER_Usuario_382QA(string dni, string nombre, string apellido, string correo,string nombreUsuario)
        {
            Dni = dni;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            NombreUsuario = nombreUsuario;
            
        }
        public SER_Usuario_382QA(string nombreUsuario, string password)
        {
            NombreUsuario = nombreUsuario;
            Password = password;
        }
        public SER_Usuario_382QA(string dni, string nombre, string apellido, string correo, string nombreUsuario, string password, bool bloqueo, bool activo)
        {
            Dni = dni;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            NombreUsuario = nombreUsuario;
            Password = password;
            Bloqueo = bloqueo;
            Activo = activo;
        }
        public SER_Usuario_382QA(string dni, string nombre, string apellido, string correo, string nombreUsuario, string password, bool bloqueo, bool activo, int cantIntentos, SER_Rol_382QA rol, string idIdioma)
        {
            Dni = dni;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            NombreUsuario = nombreUsuario;
            Password = password;
            Bloqueo = bloqueo;
            Activo = activo;
            CantIntentos = cantIntentos;
            Rol = rol;
            IdIdioma = idIdioma;
        }
        public SER_Usuario_382QA(string dni, string nombre, string apellido, string correo, string nombreUsuario, bool bloqueo, bool activo, SER_Rol_382QA rol)
        {
            Dni = dni;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            NombreUsuario = nombreUsuario;
            Bloqueo = bloqueo;
            Activo = activo;
            Rol = rol;
        }
        public SER_Usuario_382QA(string dni, string nombre, string apellido, string correo, bool bloqueo, bool activo, SER_Rol_382QA rol, string idIdioma)
        {
            Dni = dni;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            NombreUsuario = apellido.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries)[0] + dni;
            Password = dni + apellido.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];
            Bloqueo = bloqueo;
            Activo = activo;
            CantIntentos = 0;
            Rol = rol;
            IdIdioma = idIdioma;
        }
        public SER_Usuario_382QA(object[] datos)
        {
            Dni = Convert.ToString(datos[0]);
            Nombre = Convert.ToString(datos[1]);
            Apellido = Convert.ToString(datos[2]);
            Correo = Convert.ToString(datos[3]);
            NombreUsuario = Convert.ToString(datos[4]);
            Password = Convert.ToString(datos[5]);
            Bloqueo = Convert.ToBoolean(datos[6]);
            Activo = Convert.ToBoolean(datos[7]);
            CantIntentos = Convert.ToInt16(datos[8]);
            IdIdioma = Convert.ToString(datos[10]);
        }
    }
}
