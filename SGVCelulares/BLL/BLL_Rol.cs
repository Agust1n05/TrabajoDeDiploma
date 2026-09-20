using MAP;
using Servicio;

namespace BLL
{
    public class BLL_Rol
    {
        MAP_Rol map_rol;
        BLL_Bitacora bll_bitacora;
        BLL_DV bll_dv;

        public BLL_Rol()
        {
            map_rol = new MAP_Rol();
            bll_bitacora = new BLL_Bitacora();
            bll_dv = new BLL_DV();
        }
        public List<SER_Rol> Consultar()
        {
            return map_rol.Consultar();
        }
        public List<object[]> ConsultarRolFamilia()
        {
            return map_rol.ConsultarRolFamilia();
        }
        public List<object[]> ConsultarRolPermiso()
        {
            return map_rol.ConsultarRolPermiso();
        }

        public void Agregar(SER_Rol rol, List<SER_Componente> componentes)
        {
            if (componentes.Count == 0) throw new Exception("No hay permisos o familias cargadas!");
            if (map_rol.ExisteRol(rol)) throw new Exception("El nombre del rol está repetido!");
            if (ValidarPermisosRepetidos(componentes)) throw new Exception("El permiso repite con otro");
            map_rol.Agregar(rol);
            rol.Id = map_rol.ObtenerIdPorNombre(rol).Id;
            bll_dv.GenerarDVH(rol, rol.Id.ToString(),"rol");
            bll_dv.GenerarDVV("rol");
            
            foreach (SER_Componente c in componentes)
            {
                map_rol.AgregarPermisoFamilia(rol, c);
                if(c is SER_Familia familia)
                {
                    bll_dv.GenerarDVH(new List<string>() { rol.Id.ToString(), c.Id.ToString() }, "rol_familia");
                    bll_dv.GenerarDVV("rol_familia");
                }
                else if (c is SER_Permiso permiso)
                {
                    bll_dv.GenerarDVH(new List<string>() { rol.Id.ToString(), c.Id.ToString() }, "rol_permiso");
                    bll_dv.GenerarDVV("rol_permiso");
                }
            }
            
            bll_bitacora.RegistrarBitacora(new SER_Bitacora(SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, "Roles", "Crear rol", 1));
        }
        private bool ValidarPermisosRepetidos(List<SER_Componente> componentes)
        {
            bool rdo = false;
            HashSet<int> permisos = new HashSet<int>();
            foreach (SER_Componente componente in componentes)
            {
                if (!AgregarPermisos(componente, permisos)) rdo = true;
            }
            return rdo;
        }
        private bool AgregarPermisos(SER_Componente componente, HashSet<int> permisos)
        {
            if (componente is SER_Permiso permiso)
            {
                return permisos.Add(permiso.Id);
            }
            if (componente is SER_Familia familia)
            {
                foreach (SER_Componente hijo in familia.Componentes)
                {
                    if (!AgregarPermisos(hijo, permisos))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void Borrar(SER_Rol rol)
        {
            if (map_rol.EstaEnUso(rol))
                throw new Exception("No se puede borrar: el rol está asignado a uno o más usuarios.");

            map_rol.Borrar(rol);

            bll_dv.BorrarDVH("rol", rol.Id.ToString());

            foreach (var componente in rol.Componentes)
            {
                string tabla = componente is SER_Familia ? "rol_familia" : "rol_permiso";
                string id = $"{rol.Id}-{componente.Id}";
                bll_dv.BorrarDVH(tabla, id);
            }

            bll_dv.GenerarDVV("rol");
            bll_dv.GenerarDVV("rol_familia");
            bll_dv.GenerarDVV("rol_permiso");

            bll_bitacora.RegistrarBitacora(new SER_Bitacora(
                SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, "Roles", "Borrar rol", 1));
        }
    }
}
