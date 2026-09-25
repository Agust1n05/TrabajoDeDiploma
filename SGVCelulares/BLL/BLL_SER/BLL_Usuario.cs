using MAP.MAP_SER;
using Servicio;

namespace BLL.BLL_SER
{
    public class BLL_Usuario : IABMC<SER_Usuario>
    {
        MAP_Usuario map_usuario;
        BLL_Bitacora bll_bitacora;
        BLL_Idioma bll_idioma;
        MAP_Rol map_rol;
        BLL_DV bll_dv;
        public BLL_Usuario()
        {
            map_usuario = new MAP_Usuario();
            bll_bitacora = new BLL_Bitacora();
            bll_idioma = new BLL_Idioma();
            map_rol = new MAP_Rol();
            bll_dv = new BLL_DV();
        }

        public void Agregar(SER_Usuario usuario)
        {
            try
            {
                if (map_usuario.ValidarDniRepetido(usuario)) throw new Exception("El DNI está repetido!");
                if (map_usuario.ValidarCorreoRepetido(usuario)) throw new Exception("El correo electrónico está repetido!");
                string passwordHasheado = SER_Cripto.Encriptar(usuario.Password);
                usuario.Password = passwordHasheado;
                map_usuario.Agregar(usuario);
                
                bll_dv.GenerarDVH(usuario, usuario.Dni, "usuario");
                bll_dv.GenerarDVV("usuario");
                
                SER_Bitacora bitacora = new SER_Bitacora(SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, "Usuarios", "Crear usuario", 2);
                bll_bitacora.RegistrarBitacora(bitacora);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Borrar(SER_Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public void Modificar(SER_Usuario usuario)
        {
            SER_Usuario usuAux = map_usuario.ConsultarPorNombreUsuario(usuario);
            if (usuAux != null && usuAux.Dni != usuario.Dni) throw new Exception("Ya existe usuario con este nombre de usuario");
            map_usuario.Modificar(usuario);
            
            bll_dv.GenerarDVH(usuario, usuario.Dni, "usuario");
            bll_dv.GenerarDVV("usuario");
            
            SER_Bitacora bitacora = new SER_Bitacora(SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, "Usuarios", "Modificar usuario", 2);
            bll_bitacora.RegistrarBitacora(bitacora);
        }
        public void ModificarPassword(string claveActual, string claveNueva)
        {
            if (claveActual == claveNueva) throw new Exception("No puede usar la misma contraseña");

            SER_Usuario usuario = SER_SesionManager.ObtenerSesion().Usuario;
            if (SER_Cripto.Encriptar(claveActual) != usuario.Password) throw new Exception("Contraseña incorrecta");
            string claveNuevaHasheada = SER_Cripto.Encriptar(claveNueva);
            map_usuario.ModificarPassword(usuario, claveNuevaHasheada);

            usuario.Password = claveNuevaHasheada;
            bll_dv.GenerarDVH(usuario, usuario.Dni, "usuario");
            bll_dv.GenerarDVV("usuario");
            
            SER_Bitacora bitacora = new SER_Bitacora(usuario, DateTime.Now, "Usuarios", "Cambiar clave", 1);
            bll_bitacora.RegistrarBitacora(bitacora);
        }
        public List<SER_Usuario> Consultar() => map_usuario.Consultar();
        public List<object> ConsultarParaGrilla()
        {
            var consulta = from u in Consultar()
                           select new
                           {
                               DNI = u.Dni,
                               Nombre = u.Nombre,
                               Apellido = u.Apellido,
                               Usuario = u.NombreUsuario,
                               Activo = u.Activo,
                               Bloqueado = u.Bloqueo,
                               Rol = u.Rol.Nombre
                           };
            return consulta.ToList<object>();
        }

        public SER_Usuario ConsultarPorId(SER_Usuario usuario)
        {
            return map_usuario.ConsultarPorId(usuario);
        }
        public List<object> ConsultarFiltrado(SER_Usuario us)
        {
            var consulta = from user in map_usuario.Consultar()
                           where
                           (string.IsNullOrWhiteSpace(us.Dni) || user.Dni.Contains(us.Dni))
                           &&
                           (string.IsNullOrWhiteSpace(us.Nombre) || user.Nombre.Contains(us.Nombre))
                           &&
                           (string.IsNullOrWhiteSpace(us.Apellido) || user.Apellido.Contains(us.Apellido))
                           &&
                           (string.IsNullOrWhiteSpace(us.Correo) || user.Correo.Contains(us.Correo))
                           &&
                           (string.IsNullOrWhiteSpace(us.NombreUsuario) || user.NombreUsuario.Contains(us.NombreUsuario))
                           &&
                           (string.IsNullOrWhiteSpace(us.Rol.Nombre) || user.Rol.Nombre.Contains(us.Rol.Nombre))
                           select new
                           {
                               DNI = user.Dni,
                               Nombre = user.Nombre,
                               Apellido = user.Apellido,
                               Usuario = user.NombreUsuario,
                               Activo = user.Activo,
                               Bloqueado = user.Bloqueo,
                               Rol = user.Rol.Nombre
                           };

            return consulta.ToList<object>();
        }
        public List<object> ConsultarActivos() 
        {
            var consulta = from u in Consultar()
                           where u.Activo
                           select new
                           {
                               DNI = u.Dni,
                               Nombre = u.Nombre,
                               Apellido = u.Apellido,
                               Usuario = u.NombreUsuario,
                               Activo = u.Activo,
                               Bloqueado = u.Bloqueo,
                               Rol = u.Rol.Nombre
                           };
            return consulta.ToList<object>();
        }

        public bool Login(SER_Usuario usuario, out string mensajeInconsistencia)
        {
            mensajeInconsistencia = null;

            bool rdo = false;

            SER_Usuario obj = map_usuario.ConsultarPorNombreUsuario(usuario);
            if (obj == null) throw new Exception("Datos erróneos!");

            if (EstaBloqueado(obj)) throw new Exception("La cuenta está bloqueada!");
            if (!EstaActivo(obj)) throw new Exception("El usuario se encuentra desactivado!");

            if (!CompararPassword(obj, usuario.Password))
            {
                map_usuario.SumarCantidadIntento(obj);
                obj.CantIntentos += 1;

                bll_dv.GenerarDVH(obj, obj.Dni, "usuario");
                bll_dv.GenerarDVV("usuario");

                if (map_usuario.ConsultarPorNombreUsuario(usuario).CantIntentos >= 3)
                {
                    map_usuario.Bloquear(obj);
                    obj.Bloqueo = true;

                    bll_dv.GenerarDVH(obj, obj.Dni, "usuario");
                    bll_dv.GenerarDVV("usuario");

                    throw new Exception("Se bloqueó el usuario por motivos de seguridad!");
                }

                throw new Exception("No se pudo iniciar sesión");
            }

            bool integro = true;

           try
           {
               bll_dv.VerificarIntegridad();
           }
           catch (Exception ex)
           {
               integro = false;
               mensajeInconsistencia = ex.Message;
           }

           bool esAdmin = obj.Rol.Id == 1;

           if (!integro && !esAdmin)throw new Exception("El sistema no se encuentra disponible. Contacte con un administrador si el problema persiste."); 


            try
            {
                bll_idioma.CambiarIdioma(obj.IdIdioma);
            }
            catch
            {
                try { bll_idioma.CambiarIdioma("es-AR"); } catch { }
            }

            rdo = true;

            SER_SesionManager sesion = SER_SesionManager.ObtenerSesion();

            map_usuario.ReiniciarIntentos(obj);

            obj.CantIntentos = 0;
            
            if (integro)
            {
                bll_dv.GenerarDVH(obj, obj.Dni, "usuario");
                bll_dv.GenerarDVV("usuario");
            }

           
            obj.Rol = map_rol.ObtenerArbol(obj.Rol.Id);

            sesion.Usuario = obj;

            SER_Bitacora bitacora = new SER_Bitacora(obj, DateTime.Now, "Usuarios", "Login", 1);
            bll_bitacora.RegistrarBitacora(bitacora);

            return rdo;
        }

        public void Logout()
        {
            SER_Usuario usuario = SER_SesionManager.ObtenerSesion().Usuario;
            if (usuario != null)
            {
                usuario.IdIdioma = bll_idioma.IdiomaActual;
                map_usuario.ModificarIdioma(usuario);
                
                bll_dv.GenerarDVH(usuario, usuario.Dni, "usuario");
                bll_dv.GenerarDVV("usuario");
                
                bll_bitacora.RegistrarBitacora(new SER_Bitacora(usuario, DateTime.Now, "Usuarios", "Logout", 2));
                bll_bitacora.RegistrarBitacora(new SER_Bitacora(usuario, DateTime.Now, "Usuarios", "Cambiar Idioma", 3));
                SER_SesionManager.CerrarSesion();
            }
            bll_idioma.CambiarIdioma("es-AR");
        }

        private bool CompararPassword(SER_Usuario usuario, string password)
        {
            bool rdo = false;
            string passwordHasheada = SER_Cripto.Encriptar(password);
            if (passwordHasheada == usuario.Password) rdo = true;
            return rdo;
        }
        private bool EstaBloqueado(SER_Usuario usuario)
        {
            bool rdo = false;
            if (usuario.Bloqueo) rdo = true;
            return rdo;
        }
        private bool EstaActivo(SER_Usuario usuario)
        {
            bool rdo = false;
            if (usuario.Activo) rdo = true;
            return rdo;
        }
        public void Desbloquear(SER_Usuario usuario)
        {
            try
            {
                SER_Usuario usuarioAux = ConsultarPorId(usuario);
                if (!EstaBloqueado(usuarioAux)) throw new Exception("El usuario no está bloqueado!");

                map_usuario.Desbloquear(usuarioAux);
                usuarioAux.Bloqueo = false;

                string nuevaPasswordHasheada = SER_Cripto.Encriptar(usuarioAux.Dni + usuarioAux.Apellido);
                map_usuario.ModificarPassword(usuarioAux, nuevaPasswordHasheada);
                usuarioAux.Password = nuevaPasswordHasheada;

                map_usuario.ReiniciarIntentos(usuarioAux);
                usuarioAux.CantIntentos = 0;

                bll_dv.GenerarDVH(usuarioAux, usuarioAux.Dni, "usuario");
                bll_dv.GenerarDVV("usuario");

                SER_Bitacora bitacora = new SER_Bitacora(SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, "Usuarios", "Desbloquear usuario", 2);
                bll_bitacora.RegistrarBitacora(bitacora);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void CambiarEstadoActivo(SER_Usuario usuario)
        {
            SER_Usuario usuarioAux = map_usuario.ConsultarPorId(usuario);
            if (usuarioAux != null)
            {
                usuarioAux.Activo = !usuarioAux.Activo;
                map_usuario.CambiarEstadoActivo(usuarioAux);

                bll_dv.GenerarDVH(usuarioAux, usuarioAux.Dni, "usuario");
                bll_dv.GenerarDVV("usuario");
                
                if (usuarioAux.Activo)
                {
                    SER_Bitacora bitacora = new SER_Bitacora(SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, "Usuarios", "Activar usuario", 3);
                    
                    bll_bitacora.RegistrarBitacora(bitacora);
                }
                else
                {
                    SER_Bitacora bitacora = new SER_Bitacora(SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, "Usuarios", "Desactivar usuario", 3);
                    bll_bitacora.RegistrarBitacora(bitacora);
                }
            }
        }
    }
}
