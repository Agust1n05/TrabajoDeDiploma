using MAP.MAP_SER;
using Servicio;

namespace BLL.BLL_SER
{
    public class BLL_Usuario_382QA : IABMC_382QA<SER_Usuario_382QA>
    {
        MAP_Usuario_382QA map_usuario;
        BLL_Bitacora_382QA bll_bitacora;
        BLL_Idioma_382QA bll_idioma;
        MAP_Rol_382QA map_rol;
        BLL_DV_382QA bll_dv;
        public BLL_Usuario_382QA()
        {
            map_usuario = new MAP_Usuario_382QA();
            bll_bitacora = new BLL_Bitacora_382QA();
            bll_idioma = new BLL_Idioma_382QA();
            map_rol = new MAP_Rol_382QA();
            bll_dv = new BLL_DV_382QA();
        }

        public void Agregar_382QA(SER_Usuario_382QA usuario)
        {
            if (map_usuario.ValidarDniRepetido_382QA(usuario)) throw new Exception("El DNI está repetido!");
            if (map_usuario.ValidarCorreoRepetido_382QA(usuario)) throw new Exception("El correo electrónico está repetido!");
            string passwordHasheado = SER_Cripto_382QA.Encriptar_382QA(usuario.Password);
            usuario.Password = passwordHasheado;
            map_usuario.Agregar_382QA(usuario);
            
            bll_dv.GenerarDVH_382QA(usuario, usuario.Dni, "usuario");
            bll_dv.GenerarDVV_382QA("usuario");
            
            SER_Bitacora_382QA bitacora = new SER_Bitacora_382QA(SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, "Usuarios", "Crear usuario", 2);
            bll_bitacora.RegistrarBitacora_382QA(bitacora);
        }

        public void Borrar_382QA(SER_Usuario_382QA usuario)
        {
            throw new NotImplementedException();
        }

        public void Modificar_382QA(SER_Usuario_382QA usuario)
        {
            SER_Usuario_382QA usuAux = map_usuario.ConsultarPorNombreUsuario_382QA(usuario);
            if (usuAux != null && usuAux.Dni != usuario.Dni) throw new Exception("Ya existe usuario con este nombre de usuario");
            map_usuario.Modificar_382QA(usuario);
            
            bll_dv.GenerarDVH_382QA(usuario, usuario.Dni, "usuario");
            bll_dv.GenerarDVV_382QA("usuario");
            
            SER_Bitacora_382QA bitacora = new SER_Bitacora_382QA(SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, "Usuarios", "Modificar usuario", 2);
            bll_bitacora.RegistrarBitacora_382QA(bitacora);
        }
        public void ModificarPassword_382QA(string claveActual, string claveNueva)
        {
            if (claveActual == claveNueva) throw new Exception("No puede usar la misma contraseña");

            SER_Usuario_382QA usuario = SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario;
            if (SER_Cripto_382QA.Encriptar_382QA(claveActual) != usuario.Password) throw new Exception("Contraseña incorrecta");
            string claveNuevaHasheada = SER_Cripto_382QA.Encriptar_382QA(claveNueva);
            map_usuario.ModificarPassword_382QA(usuario, claveNuevaHasheada);

            usuario.Password = claveNuevaHasheada;
            bll_dv.GenerarDVH_382QA(usuario, usuario.Dni, "usuario");
            bll_dv.GenerarDVV_382QA("usuario");
            
            SER_Bitacora_382QA bitacora = new SER_Bitacora_382QA(usuario, DateTime.Now, "Usuarios", "Cambiar clave", 1);
            bll_bitacora.RegistrarBitacora_382QA(bitacora);
        }
        public List<SER_Usuario_382QA> Consultar_382QA() => map_usuario.Consultar_382QA();
        public List<object> ConsultarParaGrilla_382QA()
        {
            var consulta = from u in Consultar_382QA()
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

        public SER_Usuario_382QA ConsultarPorId_382QA(SER_Usuario_382QA usuario)
        {
            return map_usuario.ConsultarPorId_382QA(usuario);
        }
        public List<object> ConsultarFiltrado_382QA(SER_Usuario_382QA us)
        {
            var consulta = from user in map_usuario.Consultar_382QA()
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
        public List<object> ConsultarActivos_382QA() 
        {
            var consulta = from u in Consultar_382QA()
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

        public bool Login_382QA(SER_Usuario_382QA usuario, out string mensajeInconsistencia)
        {
            mensajeInconsistencia = null;

            bool rdo = false;

            SER_Usuario_382QA obj = map_usuario.ConsultarPorNombreUsuario_382QA(usuario);
            if (obj == null) throw new Exception("Datos erróneos!");

            if (EstaBloqueado_382QA(obj)) throw new Exception("La cuenta está bloqueada!");
            if (!EstaActivo_382QA(obj)) throw new Exception("El usuario se encuentra desactivado!");

            // Se verifica antes de tocar la fila: si la base ya estaba alterada, recalcular los DV taparía la alteración.
            bool integro = true;

           try
           {
               bll_dv.VerificarIntegridad_382QA();
           }
           catch (Exception ex)
           {
               integro = false;
               mensajeInconsistencia = ex.Message;
           }

            if (!CompararPassword_382QA(obj, usuario.Password))
            {
                map_usuario.SumarCantidadIntento_382QA(obj);
                obj.CantIntentos += 1;

                if (integro)
                {
                    bll_dv.GenerarDVH_382QA(obj, obj.Dni, "usuario");
                    bll_dv.GenerarDVV_382QA("usuario");
                }

                if (map_usuario.ConsultarPorNombreUsuario_382QA(usuario).CantIntentos >= 3)
                {
                    map_usuario.Bloquear_382QA(obj);
                    obj.Bloqueo = true;

                    if (integro)
                    {
                        bll_dv.GenerarDVH_382QA(obj, obj.Dni, "usuario");
                        bll_dv.GenerarDVV_382QA("usuario");
                    }

                    throw new Exception("Se bloqueó el usuario por motivos de seguridad!");
                }

                throw new Exception("No se pudo iniciar sesión");
            }

            obj.Rol = map_rol.ObtenerArbol_382QA(obj.Rol.Id);

            // Con la base inconsistente solo entra quien puede repararla (FrmRepararInconsistencia_382QA ofrece el restore).
            bool puedeReparar = obj.Rol.TienePermiso_382QA("Realizar Restore");

           if (!integro && !puedeReparar)throw new Exception("El sistema no se encuentra disponible. Contacte con un administrador si el problema persiste."); 


            try
            {
                bll_idioma.CambiarIdioma_382QA(obj.IdIdioma);
            }
            catch
            {
                try { bll_idioma.CambiarIdioma_382QA("es-AR"); } catch { }
            }

            rdo = true;

            SER_SesionManager_382QA sesion = SER_SesionManager_382QA.ObtenerSesion_382QA();

            map_usuario.ReiniciarIntentos_382QA(obj);

            obj.CantIntentos = 0;
            
            if (integro)
            {
                bll_dv.GenerarDVH_382QA(obj, obj.Dni, "usuario");
                bll_dv.GenerarDVV_382QA("usuario");
            }

            sesion.Usuario = obj;

            SER_Bitacora_382QA bitacora = new SER_Bitacora_382QA(obj, DateTime.Now, "Usuarios", "Login", 1);
            bll_bitacora.RegistrarBitacora_382QA(bitacora);

            return rdo;
        }

        public void Logout_382QA()
        {
            SER_Usuario_382QA usuario = SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario;
            if (usuario != null)
            {
                // Si el admin sale de FrmRepararInconsistencia_382QA sin reparar, no se recalculan los DV (taparían la inconsistencia).
                if (bll_dv.ObtenerInconsistencias_382QA().Count == 0)
                {
                    usuario.IdIdioma = bll_idioma.IdiomaActual;
                    map_usuario.ModificarIdioma_382QA(usuario);

                    bll_dv.GenerarDVH_382QA(usuario, usuario.Dni, "usuario");
                    bll_dv.GenerarDVV_382QA("usuario");
                }

                bll_bitacora.RegistrarBitacora_382QA(new SER_Bitacora_382QA(usuario, DateTime.Now, "Usuarios", "Logout", 2));
                bll_bitacora.RegistrarBitacora_382QA(new SER_Bitacora_382QA(usuario, DateTime.Now, "Usuarios", "Cambiar Idioma", 3));
                SER_SesionManager_382QA.CerrarSesion_382QA();
            }
            bll_idioma.CambiarIdioma_382QA("es-AR");
        }

        private bool CompararPassword_382QA(SER_Usuario_382QA usuario, string password)
        {
            bool rdo = false;
            string passwordHasheada = SER_Cripto_382QA.Encriptar_382QA(password);
            if (passwordHasheada == usuario.Password) rdo = true;
            return rdo;
        }
        private bool EstaBloqueado_382QA(SER_Usuario_382QA usuario)
        {
            bool rdo = false;
            if (usuario.Bloqueo) rdo = true;
            return rdo;
        }
        private bool EstaActivo_382QA(SER_Usuario_382QA usuario)
        {
            bool rdo = false;
            if (usuario.Activo) rdo = true;
            return rdo;
        }
        public void Desbloquear_382QA(SER_Usuario_382QA usuario)
        {
            SER_Usuario_382QA usuarioAux = ConsultarPorId_382QA(usuario);
            if (!EstaBloqueado_382QA(usuarioAux)) throw new Exception("El usuario no está bloqueado!");

            map_usuario.Desbloquear_382QA(usuarioAux);
            usuarioAux.Bloqueo = false;

            string nuevaPasswordHasheada = SER_Cripto_382QA.Encriptar_382QA(usuarioAux.Dni + usuarioAux.Apellido);
            map_usuario.ModificarPassword_382QA(usuarioAux, nuevaPasswordHasheada);
            usuarioAux.Password = nuevaPasswordHasheada;

            map_usuario.ReiniciarIntentos_382QA(usuarioAux);
            usuarioAux.CantIntentos = 0;

            bll_dv.GenerarDVH_382QA(usuarioAux, usuarioAux.Dni, "usuario");
            bll_dv.GenerarDVV_382QA("usuario");

            SER_Bitacora_382QA bitacora = new SER_Bitacora_382QA(SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, "Usuarios", "Desbloquear usuario", 2);
            bll_bitacora.RegistrarBitacora_382QA(bitacora);
        }
        public void CambiarEstadoActivo_382QA(SER_Usuario_382QA usuario)
        {
            SER_Usuario_382QA usuarioAux = map_usuario.ConsultarPorId_382QA(usuario);
            if (usuarioAux != null)
            {
                usuarioAux.Activo = !usuarioAux.Activo;
                map_usuario.CambiarEstadoActivo_382QA(usuarioAux);

                bll_dv.GenerarDVH_382QA(usuarioAux, usuarioAux.Dni, "usuario");
                bll_dv.GenerarDVV_382QA("usuario");
                
                if (usuarioAux.Activo)
                {
                    SER_Bitacora_382QA bitacora = new SER_Bitacora_382QA(SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, "Usuarios", "Activar usuario", 3);
                    
                    bll_bitacora.RegistrarBitacora_382QA(bitacora);
                }
                else
                {
                    SER_Bitacora_382QA bitacora = new SER_Bitacora_382QA(SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, "Usuarios", "Desactivar usuario", 3);
                    bll_bitacora.RegistrarBitacora_382QA(bitacora);
                }
            }
        }
    }
}
