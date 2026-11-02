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
    public class BLL_Tecnico : IABMC<BE_Tecnico>
    {
        private const string Modulo = "Técnicos";

        // Un rol es de técnico si trabaja sobre las OT (diagnostica o repara) y no atiende el mostrador
        // (recepción, presupuesto, entrega). Se decide por permisos, no por el nombre ni el id del rol.
        private static readonly string[] PermisosTaller = { "Diagnosticar Equipo", "Reparar Equipo", "Evaluar Reclamo", "Reparar Reclamo" };
        private static readonly string[] PermisosMostrador = { "Recepcionar Equipo", "Gestionar Presupuesto", "Entregar Equipo", "Registrar Reclamo" };

        MAP_Tecnico map_tecnico;
        MAP_Reclamo map_reclamo;
        BLL_Usuario bll_usuario;
        BLL_Rol bll_rol;
        BLL_Bitacora bll_bitacora;
        BLL_DV bll_dv;

        public BLL_Tecnico()
        {
            map_tecnico = new MAP_Tecnico();
            map_reclamo = new MAP_Reclamo();
            bll_usuario = new BLL_Usuario();
            bll_rol = new BLL_Rol();
            bll_bitacora = new BLL_Bitacora();
            bll_dv = new BLL_DV();
        }

        /// <summary>Técnicos activos, ordenados por apellido y nombre.</summary>
        public List<BE_Tecnico> Consultar()
        {
            return map_tecnico.Consultar()
                .Where(t => t.Activo)
                .OrderBy(t => t.Apellido).ThenBy(t => t.Nombre)
                .ToList();
        }

        /// <summary>Técnico por DNI, esté activo o dado de baja.</summary>
        public BE_Tecnico ConsultarPorId(BE_Tecnico tecnico) => map_tecnico.ConsultarPorDni(tecnico.Dni)!;

        /// <summary>Técnico activo por DNI, o null si no existe o está dado de baja.</summary>
        public BE_Tecnico? ConsultarPorDni(string dni)
        {
            BE_Tecnico? tecnico = map_tecnico.ConsultarPorDni(dni);
            return tecnico != null && tecnico.Activo ? tecnico : null;
        }

        /// <summary>
        /// Técnico activo que corresponde al usuario logueado (mismo DNI), o null si el usuario no es técnico.
        /// Se usa para que cada técnico vea y trabaje solo sobre sus OT.
        /// </summary>
        public BE_Tecnico? ConsultarTecnicoDeSesion()
        {
            SER_Usuario? usuario = SER_SesionManager.ObtenerSesion()?.Usuario;
            return usuario == null ? null : ConsultarPorDni(usuario.Dni);
        }

        /// <summary>
        /// True si el usuario logueado tiene un rol de técnico, esté o no registrado como técnico activo.
        /// Así un técnico dado de baja no se confunde con un usuario no técnico.
        /// </summary>
        public bool SesionEsTecnico()
        {
            SER_Usuario? usuario = SER_SesionManager.ObtenerSesion()?.Usuario;
            return EsRolTecnico(usuario?.Rol);
        }

        /// <summary>
        /// Usuarios activos con un rol de técnico que todavía no son técnicos activos.
        /// Son las opciones del combo del alta. Incluye a los técnicos dados de baja (el alta los reactiva).
        /// </summary>
        public List<SER_Usuario> ConsultarUsuariosParaAlta()
        {
            var dnisActivos = Consultar().Select(t => t.Dni).ToHashSet();
            var rolesTecnico = IdsRolesTecnico();
            return bll_usuario.Consultar()
                .Where(u => u.Activo && u.Rol != null && rolesTecnico.Contains(u.Rol.Id) && !dnisActivos.Contains(u.Dni))
                .OrderBy(u => u.Apellido).ThenBy(u => u.Nombre)
                .ToList();
        }

        public bool EstaDadoDeBaja(string dni)
        {
            BE_Tecnico? tecnico = map_tecnico.ConsultarPorDni(dni);
            return tecnico != null && !tecnico.Activo;
        }

        /// <summary>
        /// Alta de técnico a partir de su usuario. Si el DNI pertenece a un técnico dado de baja, lo reactiva.
        /// </summary>
        public void Agregar(BE_Tecnico tecnico)
        {
            if (tecnico == null) throw new Exception("El técnico no puede ser nulo.");

            SER_Usuario usuario = ObtenerUsuarioTecnico(tecnico.Dni);
            tecnico.Dni = usuario.Dni;
            tecnico.Nombre = usuario.Nombre;
            tecnico.Apellido = usuario.Apellido;

            BE_Tecnico? existente = map_tecnico.ConsultarPorDni(tecnico.Dni);
            if (existente != null && existente.Activo)
                throw new Exception($"El usuario {usuario.NombreUsuario} ya está registrado como técnico.");

            if (existente != null)
            {
                map_tecnico.Modificar(tecnico);
                map_tecnico.CambiarActivo(tecnico.Dni, true);
                bll_dv.ActualizarDVNegocio(BLL_DV.TablaTecnico, tecnico.Dni);
                RegistrarBitacora("Reactivar técnico", 1);
            }
            else
            {
                map_tecnico.Agregar(tecnico);
                bll_dv.ActualizarDVNegocio(BLL_DV.TablaTecnico, tecnico.Dni);
                RegistrarBitacora("Crear técnico", 2);
            }
        }

        /// <summary>
        /// Modifica la disponibilidad del técnico. El nombre y el apellido se vuelven a copiar del usuario,
        /// por si cambiaron en la gestión de usuarios.
        /// </summary>
        public void Modificar(BE_Tecnico tecnico)
        {
            if (tecnico == null) throw new Exception("El técnico no puede ser nulo.");
            BE_Tecnico existente = ObtenerActivo(tecnico.Dni);

            SER_Usuario? usuario = bll_usuario.Consultar().FirstOrDefault(u => u.Dni == existente.Dni);
            existente.Nombre = usuario?.Nombre ?? existente.Nombre;
            existente.Apellido = usuario?.Apellido ?? existente.Apellido;
            existente.Disponible = tecnico.Disponible;

            map_tecnico.Modificar(existente);
            bll_dv.ActualizarDVNegocio(BLL_DV.TablaTecnico, existente.Dni);
            RegistrarBitacora("Modificar técnico", 2);
        }

        /// <summary>Baja lógica. No se permite si el técnico tiene órdenes de trabajo o reclamos en curso.</summary>
        public void Borrar(BE_Tecnico tecnico)
        {
            if (tecnico == null) throw new Exception("El técnico no puede ser nulo.");
            BE_Tecnico existente = ObtenerActivo(tecnico.Dni);

            int otEnCurso = map_tecnico.ContarOT(existente.Dni, EstadosOT.EnCurso);
            if (otEnCurso > 0)
                throw new Exception($"No se puede eliminar el técnico: tiene {otEnCurso} orden(es) de trabajo en curso.");

            int reclamosEnCurso = map_reclamo.ContarPorTecnico(existente.Dni, EstadosReclamo.EnCurso);
            if (reclamosEnCurso > 0)
                throw new Exception($"No se puede eliminar el técnico: tiene {reclamosEnCurso} reclamo(s) en curso.");

            map_tecnico.CambiarActivo(existente.Dni, false);
            bll_dv.ActualizarDVNegocio(BLL_DV.TablaTecnico, existente.Dni);
            RegistrarBitacora("Eliminar técnico", 1);
        }

        /// <summary>
        /// CU-NEG-1 paso 7: técnico activo y disponible con menos OT en curso
        /// (Recibida, En diagnostico, Presupuestado, En reparacion).
        /// </summary>
        public BE_Tecnico AsignarTecnico()
        {
            return map_tecnico.ObtenerConMenosOT(EstadosOT.EnCurso)
                ?? throw new Exception("No hay técnicos disponibles para asignar.");
        }

        private SER_Usuario ObtenerUsuarioTecnico(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new Exception("Debe seleccionar el usuario del técnico.");

            SER_Usuario usuario = bll_usuario.Consultar().FirstOrDefault(u => u.Dni == dni.Trim())
                ?? throw new Exception($"No existe un usuario con el DNI {dni}.");

            if (!usuario.Activo)
                throw new Exception($"El usuario {usuario.NombreUsuario} está desactivado.");

            if (usuario.Rol == null || !IdsRolesTecnico().Contains(usuario.Rol.Id))
                throw new Exception($"El usuario {usuario.NombreUsuario} no tiene un rol de técnico.");

            return usuario;
        }

        private BE_Tecnico ObtenerActivo(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new Exception("El DNI es obligatorio.");

            return ConsultarPorDni(dni.Trim())
                ?? throw new Exception($"No existe un técnico activo con el DNI {dni}.");
        }

        /// <summary>El rol tiene que venir con su árbol de permisos (como el de la sesión o los de BLL_Rol.Consultar).</summary>
        private static bool EsRolTecnico(SER_Rol? rol) =>
            rol != null && PermisosTaller.Any(rol.TienePermiso) && !PermisosMostrador.Any(rol.TienePermiso);

        // Los usuarios de BLL_Usuario.Consultar traen el rol sin permisos, así que se comparan por id.
        private HashSet<int> IdsRolesTecnico() =>
            bll_rol.Consultar().Where(EsRolTecnico).Select(r => r.Id).ToHashSet();

        private void RegistrarBitacora(string evento, int criticidad)
        {
            bll_bitacora.RegistrarBitacora(new SER_Bitacora(
                SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, Modulo, evento, criticidad));
        }
    }
}
