using MAP.MAP_SER;
using Servicio;

namespace BLL.BLL_SER
{
    public class BLL_Rol_382QA
    {
        MAP_Rol_382QA map_rol;
        BLL_Bitacora_382QA bll_bitacora;
        BLL_DV_382QA bll_dv;

        public BLL_Rol_382QA()
        {
            map_rol = new MAP_Rol_382QA();
            bll_bitacora = new BLL_Bitacora_382QA();
            bll_dv = new BLL_DV_382QA();
        }
        public List<SER_Rol_382QA> Consultar_382QA()
        {
            return map_rol.Consultar_382QA();
        }
        public List<object[]> ConsultarRolFamilia_382QA()
        {
            return map_rol.ConsultarRolFamilia_382QA();
        }
        public List<object[]> ConsultarRolPermiso_382QA()
        {
            return map_rol.ConsultarRolPermiso_382QA();
        }

        public void Agregar_382QA(SER_Rol_382QA rol, List<SER_Componente_382QA> componentes)
        {
            if (componentes.Count == 0) throw new Exception("No hay permisos o familias cargadas!");
            if (map_rol.ExisteRol_382QA(rol)) throw new Exception("El nombre del rol está repetido!");
            if (ValidarPermisosRepetidos_382QA(componentes)) throw new Exception("El permiso repite con otro");
            map_rol.Agregar_382QA(rol);
            rol.Id = map_rol.ObtenerIdPorNombre_382QA(rol).Id;
            bll_dv.GenerarDVH_382QA(rol, rol.Id.ToString(),"rol");
            bll_dv.GenerarDVV_382QA("rol");
            
            foreach (SER_Componente_382QA c in componentes)
            {
                map_rol.AgregarPermisoFamilia_382QA(rol, c);
                if(c is SER_Familia_382QA familia)
                {
                    bll_dv.GenerarDVH_382QA(new List<string>() { rol.Id.ToString(), c.Id.ToString() }, "rol_familia");
                    bll_dv.GenerarDVV_382QA("rol_familia");
                }
                else if (c is SER_Permiso_382QA permiso)
                {
                    bll_dv.GenerarDVH_382QA(new List<string>() { rol.Id.ToString(), c.Id.ToString() }, "rol_permiso");
                    bll_dv.GenerarDVV_382QA("rol_permiso");
                }
            }
            
            bll_bitacora.RegistrarBitacora_382QA(new SER_Bitacora_382QA(SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, "Roles", "Crear rol", 1));
        }
        private bool ValidarPermisosRepetidos_382QA(List<SER_Componente_382QA> componentes)
        {
            bool rdo = false;
            HashSet<int> permisos = new HashSet<int>();
            foreach (SER_Componente_382QA componente in componentes)
            {
                if (!AgregarPermisos_382QA(componente, permisos)) rdo = true;
            }
            return rdo;
        }
        private bool AgregarPermisos_382QA(SER_Componente_382QA componente, HashSet<int> permisos)
        {
            if (componente is SER_Permiso_382QA permiso)
            {
                return permisos.Add(permiso.Id);
            }
            if (componente is SER_Familia_382QA familia)
            {
                foreach (SER_Componente_382QA hijo in familia.Componentes)
                {
                    if (!AgregarPermisos_382QA(hijo, permisos))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void Borrar_382QA(SER_Rol_382QA rol)
        {
            if (map_rol.EstaEnUso_382QA(rol))
                throw new Exception("No se puede borrar: el rol está asignado a uno o más usuarios.");

            map_rol.Borrar_382QA(rol);

            bll_dv.BorrarDVH_382QA("rol", rol.Id.ToString());

            foreach (var componente in rol.Componentes)
            {
                string tabla = componente is SER_Familia_382QA ? "rol_familia" : "rol_permiso";
                string id = $"{rol.Id}-{componente.Id}";
                bll_dv.BorrarDVH_382QA(tabla, id);
            }

            bll_dv.GenerarDVV_382QA("rol");
            bll_dv.GenerarDVV_382QA("rol_familia");
            bll_dv.GenerarDVV_382QA("rol_permiso");

            bll_bitacora.RegistrarBitacora_382QA(new SER_Bitacora_382QA(
                SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, "Roles", "Borrar rol", 1));
        }
    }
}
