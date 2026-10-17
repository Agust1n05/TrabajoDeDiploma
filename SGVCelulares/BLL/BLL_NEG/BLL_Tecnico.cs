using BE;
using BLL.BLL_SER;
using MAP.MAP_NEG;
using Servicio;

namespace BLL.BLL_NEG
{
    /// <summary>
    /// ABM de técnicos con baja lógica. Un técnico se da de alta a partir de un usuario del sistema
    /// con rol Tecnico: el DNI, el nombre y el apellido se toman de ese usuario.
    /// </summary>
    public class BLL_Tecnico : IABMC<BE_Tecnico>
    {
        private const string Modulo = "Técnicos";
        public const string RolTecnico = "Tecnico";

        MAP_Tecnico map_tecnico;
        BLL_Usuario bll_usuario;
        BLL_Bitacora bll_bitacora;
        BLL_DV bll_dv;

        public BLL_Tecnico()
        {
            map_tecnico = new MAP_Tecnico();
            bll_usuario = new BLL_Usuario();
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
        /// Usuarios activos con rol Tecnico que todavía no son técnicos activos.
        /// Son las opciones del combo del alta. Incluye a los técnicos dados de baja (el alta los reactiva).
        /// </summary>
        public List<SER_Usuario> ConsultarUsuariosParaAlta()
        {
            var dnisActivos = Consultar().Select(t => t.Dni).ToHashSet();
            return bll_usuario.Consultar()
                .Where(u => u.Activo && EsRolTecnico(u) && !dnisActivos.Contains(u.Dni))
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
                RegistrarBitacora("Reactivar técnico", 3);
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

        /// <summary>Baja lógica. No se permite si el técnico tiene órdenes de trabajo en curso.</summary>
        public void Borrar(BE_Tecnico tecnico)
        {
            if (tecnico == null) throw new Exception("El técnico no puede ser nulo.");
            BE_Tecnico existente = ObtenerActivo(tecnico.Dni);

            int otEnCurso = map_tecnico.ContarOT(existente.Dni, EstadosOT.EnCurso);
            if (otEnCurso > 0)
                throw new Exception($"No se puede eliminar el técnico: tiene {otEnCurso} orden(es) de trabajo en curso.");

            map_tecnico.CambiarActivo(existente.Dni, false);
            bll_dv.ActualizarDVNegocio(BLL_DV.TablaTecnico, existente.Dni);
            RegistrarBitacora("Eliminar técnico", 2);
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

            if (!EsRolTecnico(usuario))
                throw new Exception($"El usuario {usuario.NombreUsuario} no tiene el rol {RolTecnico}.");

            return usuario;
        }

        private BE_Tecnico ObtenerActivo(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new Exception("El DNI es obligatorio.");

            return ConsultarPorDni(dni.Trim())
                ?? throw new Exception($"No existe un técnico activo con el DNI {dni}.");
        }

        private static bool EsRolTecnico(SER_Usuario usuario) =>
            string.Equals(usuario.Rol?.Nombre, RolTecnico, StringComparison.OrdinalIgnoreCase);

        private void RegistrarBitacora(string evento, int criticidad)
        {
            bll_bitacora.RegistrarBitacora(new SER_Bitacora(
                SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, Modulo, evento, criticidad));
        }
    }
}
