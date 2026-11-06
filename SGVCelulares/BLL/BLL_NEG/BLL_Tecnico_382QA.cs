using BE;
using BLL.BLL_SER;
using MAP.MAP_NEG;
using Servicio;

namespace BLL.BLL_NEG
{
    /// <summary>
    /// ABM de técnicos con baja lógica. Un técnico se da de alta a partir de un usuario del sistema
    /// con un rol de técnico: el DNI, el nombre y el apellido se toman de ese usuario.
    /// </summary>
    public class BLL_Tecnico_382QA : IABMC_382QA<BE_Tecnico_382QA>
    {
        private const string Modulo = "Técnicos";

        // Un rol es de técnico si trabaja sobre las OT (diagnostica o repara) y no atiende el mostrador
        // (recepción, presupuesto, entrega). Se decide por permisos, no por el nombre ni el id del rol.
        private static readonly string[] PermisosTaller = { "Diagnosticar Equipo", "Reparar Equipo", "Evaluar Reclamo", "Reparar Reclamo" };
        private static readonly string[] PermisosMostrador = { "Recepcionar Equipo", "Gestionar Presupuesto", "Entregar Equipo", "Registrar Reclamo", "Entregar Reclamo" };

        MAP_Tecnico_382QA map_tecnico;
        MAP_Reclamo_382QA map_reclamo;
        BLL_Usuario_382QA bll_usuario;
        BLL_Rol_382QA bll_rol;
        BLL_Bitacora_382QA bll_bitacora;
        BLL_DV_382QA bll_dv;

        public BLL_Tecnico_382QA()
        {
            map_tecnico = new MAP_Tecnico_382QA();
            map_reclamo = new MAP_Reclamo_382QA();
            bll_usuario = new BLL_Usuario_382QA();
            bll_rol = new BLL_Rol_382QA();
            bll_bitacora = new BLL_Bitacora_382QA();
            bll_dv = new BLL_DV_382QA();
        }

        /// <summary>Técnicos activos, ordenados por apellido y nombre.</summary>
        public List<BE_Tecnico_382QA> Consultar_382QA()
        {
            return map_tecnico.Consultar_382QA()
                .Where(t => t.Activo)
                .OrderBy(t => t.Apellido).ThenBy(t => t.Nombre)
                .ToList();
        }

        /// <summary>Técnico por DNI, esté activo o dado de baja.</summary>
        public BE_Tecnico_382QA ConsultarPorId_382QA(BE_Tecnico_382QA tecnico) => map_tecnico.ConsultarPorDni_382QA(tecnico.Dni)!;

        /// <summary>Técnico activo por DNI, o null si no existe o está dado de baja.</summary>
        public BE_Tecnico_382QA? ConsultarPorDni_382QA(string dni)
        {
            BE_Tecnico_382QA? tecnico = map_tecnico.ConsultarPorDni_382QA(dni);
            return tecnico != null && tecnico.Activo ? tecnico : null;
        }

        /// <summary>
        /// Técnico activo que corresponde al usuario logueado (mismo DNI), o null si el usuario no es técnico.
        /// Se usa para que cada técnico vea y trabaje solo sobre sus OT.
        /// </summary>
        public BE_Tecnico_382QA? ConsultarTecnicoDeSesion_382QA()
        {
            SER_Usuario_382QA? usuario = SER_SesionManager_382QA.ObtenerSesion_382QA()?.Usuario;
            return usuario == null ? null : ConsultarPorDni_382QA(usuario.Dni);
        }

        /// <summary>
        /// True si el usuario logueado tiene un rol de técnico, esté o no registrado como técnico activo.
        /// Así un técnico dado de baja no se confunde con un usuario no técnico.
        /// </summary>
        public bool SesionEsTecnico_382QA()
        {
            SER_Usuario_382QA? usuario = SER_SesionManager_382QA.ObtenerSesion_382QA()?.Usuario;
            return EsRolTecnico_382QA(usuario?.Rol);
        }

        /// <summary>
        /// Usuarios activos con un rol de técnico que todavía no son técnicos activos.
        /// Son las opciones del combo del alta. Incluye a los técnicos dados de baja (el alta los reactiva).
        /// </summary>
        public List<SER_Usuario_382QA> ConsultarUsuariosParaAlta_382QA()
        {
            var dnisActivos = Consultar_382QA().Select(t => t.Dni).ToHashSet();
            var rolesTecnico = IdsRolesTecnico_382QA();
            return bll_usuario.Consultar_382QA()
                .Where(u => u.Activo && u.Rol != null && rolesTecnico.Contains(u.Rol.Id) && !dnisActivos.Contains(u.Dni))
                .OrderBy(u => u.Apellido).ThenBy(u => u.Nombre)
                .ToList();
        }

        public bool EstaDadoDeBaja_382QA(string dni)
        {
            BE_Tecnico_382QA? tecnico = map_tecnico.ConsultarPorDni_382QA(dni);
            return tecnico != null && !tecnico.Activo;
        }

        /// <summary>
        /// Alta de técnico a partir de su usuario. Si el DNI pertenece a un técnico dado de baja, lo reactiva.
        /// </summary>
        public void Agregar_382QA(BE_Tecnico_382QA tecnico)
        {
            if (tecnico == null) throw new Exception("El técnico no puede ser nulo.");

            SER_Usuario_382QA usuario = ObtenerUsuarioTecnico_382QA(tecnico.Dni);
            tecnico.Dni = usuario.Dni;
            tecnico.Nombre = usuario.Nombre;
            tecnico.Apellido = usuario.Apellido;

            BE_Tecnico_382QA? existente = map_tecnico.ConsultarPorDni_382QA(tecnico.Dni);
            if (existente != null && existente.Activo)
                throw new Exception($"El usuario {usuario.NombreUsuario} ya está registrado como técnico.");

            if (existente != null)
            {
                map_tecnico.Modificar_382QA(tecnico);
                map_tecnico.CambiarActivo_382QA(tecnico.Dni, true);
                bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaTecnico, tecnico.Dni);
                RegistrarBitacora_382QA("Reactivar técnico", 1);
            }
            else
            {
                map_tecnico.Agregar_382QA(tecnico);
                bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaTecnico, tecnico.Dni);
                RegistrarBitacora_382QA("Crear técnico", 2);
            }
        }

        /// <summary>
        /// Modifica la disponibilidad del técnico. El nombre y el apellido se vuelven a copiar del usuario,
        /// por si cambiaron en la gestión de usuarios.
        /// </summary>
        public void Modificar_382QA(BE_Tecnico_382QA tecnico)
        {
            if (tecnico == null) throw new Exception("El técnico no puede ser nulo.");
            BE_Tecnico_382QA existente = ObtenerActivo_382QA(tecnico.Dni);

            SER_Usuario_382QA? usuario = bll_usuario.Consultar_382QA().FirstOrDefault(u => u.Dni == existente.Dni);
            existente.Nombre = usuario?.Nombre ?? existente.Nombre;
            existente.Apellido = usuario?.Apellido ?? existente.Apellido;
            existente.Disponible = tecnico.Disponible;

            map_tecnico.Modificar_382QA(existente);
            bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaTecnico, existente.Dni);
            RegistrarBitacora_382QA("Modificar técnico", 2);
        }

        /// <summary>Baja lógica. No se permite si el técnico tiene órdenes de trabajo o reclamos en curso.</summary>
        public void Borrar_382QA(BE_Tecnico_382QA tecnico)
        {
            if (tecnico == null) throw new Exception("El técnico no puede ser nulo.");
            BE_Tecnico_382QA existente = ObtenerActivo_382QA(tecnico.Dni);

            int otEnCurso = map_tecnico.ContarOT_382QA(existente.Dni, EstadosOT_382QA.EnCurso);
            if (otEnCurso > 0)
                throw new Exception($"No se puede eliminar el técnico: tiene {otEnCurso} orden(es) de trabajo en curso.");

            int reclamosEnCurso = map_reclamo.ContarPorTecnico_382QA(existente.Dni, EstadosReclamo_382QA.EnCurso);
            if (reclamosEnCurso > 0)
                throw new Exception($"No se puede eliminar el técnico: tiene {reclamosEnCurso} reclamo(s) en curso.");

            map_tecnico.CambiarActivo_382QA(existente.Dni, false);
            bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaTecnico, existente.Dni);
            RegistrarBitacora_382QA("Eliminar técnico", 1);
        }

        /// <summary>
        /// CU-NEG-1 paso 7: técnico activo y disponible con menos OT en curso
        /// (Recibida, En diagnostico, Presupuestado, En reparacion).
        /// </summary>
        public BE_Tecnico_382QA AsignarTecnico_382QA()
        {
            return map_tecnico.ObtenerConMenosOT_382QA(EstadosOT_382QA.EnCurso)
                ?? throw new Exception("No hay técnicos disponibles para asignar.");
        }

        private SER_Usuario_382QA ObtenerUsuarioTecnico_382QA(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new Exception("Debe seleccionar el usuario del técnico.");

            SER_Usuario_382QA usuario = bll_usuario.Consultar_382QA().FirstOrDefault(u => u.Dni == dni.Trim())
                ?? throw new Exception($"No existe un usuario con el DNI {dni}.");

            if (!usuario.Activo)
                throw new Exception($"El usuario {usuario.NombreUsuario} está desactivado.");

            if (usuario.Rol == null || !IdsRolesTecnico_382QA().Contains(usuario.Rol.Id))
                throw new Exception($"El usuario {usuario.NombreUsuario} no tiene un rol de técnico.");

            return usuario;
        }

        private BE_Tecnico_382QA ObtenerActivo_382QA(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new Exception("El DNI es obligatorio.");

            return ConsultarPorDni_382QA(dni.Trim())
                ?? throw new Exception($"No existe un técnico activo con el DNI {dni}.");
        }

        /// <summary>El rol tiene que venir con su árbol de permisos (como el de la sesión o los de BLL_Rol_382QA.Consultar).</summary>
        private static bool EsRolTecnico_382QA(SER_Rol_382QA? rol) =>
            rol != null && PermisosTaller.Any(rol.TienePermiso_382QA) && !PermisosMostrador.Any(rol.TienePermiso_382QA);

        // Los usuarios de BLL_Usuario_382QA.Consultar traen el rol sin permisos, así que se comparan por id.
        private HashSet<int> IdsRolesTecnico_382QA() =>
            bll_rol.Consultar_382QA().Where(EsRolTecnico_382QA).Select(r => r.Id).ToHashSet();

        private void RegistrarBitacora_382QA(string evento, int criticidad)
        {
            bll_bitacora.RegistrarBitacora_382QA(new SER_Bitacora_382QA(
                SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, Modulo, evento, criticidad));
        }
    }
}
