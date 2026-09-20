using MAP;
using Servicio;

namespace BLL
{
    public class BLL_Familia : IABMC<SER_Familia>
    {
        MAP_Familia map_familia;
        BLL_Bitacora bll_bitacora;
        BLL_DV bll_dv;

        public BLL_Familia()
        {
            map_familia = new MAP_Familia();
            bll_bitacora = new BLL_Bitacora();
            bll_dv = new BLL_DV();
        }
        public void Agregar(SER_Familia familia)
        {
            map_familia.Agregar(familia);
          
        }

        public void Borrar(SER_Familia familia)
        {
            if (map_familia.EstaEnUso(familia))
                throw new Exception("No se puede borrar: la familia está asignada a un rol u otra familia.");

            map_familia.Borrar(familia);

            bll_dv.BorrarDVH("familia", familia.Id.ToString());

            foreach (var componente in familia.Componentes)
            {
                string tabla = componente is SER_Familia ? "familia_familia" : "permiso_familia";
                string id = $"{familia.Id}-{componente.Id}";
                bll_dv.BorrarDVH(tabla, id);
            }

            bll_dv.GenerarDVV("familia");
            bll_dv.GenerarDVV("familia_familia");
            bll_dv.GenerarDVV("permiso_familia");

            bll_bitacora.RegistrarBitacora(new SER_Bitacora(SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, "Roles", "Borrar familia", 1));
        }

        public List<SER_Familia> Consultar()
        {
            return map_familia.Consultar();
        }
        public List<object[]> ConsultarFamiliaFamilia()
        {
            return map_familia.ConsultarFamiliaFamilia();
        }
        public List<object[]> ConsultarFamiliaPermiso()
        {
            return map_familia.ConsultarFamiliaPermiso();
        }
        public SER_Familia ConsultarPorId(SER_Familia familia)
        {
            return map_familia.ObtenerArbol(familia.Id);
        }

        public void Modificar(SER_Familia familia)
        {
            throw new NotImplementedException();
        }
        public void Agregar(SER_Familia familia, List<SER_Componente> componentes)
        {
            if (componentes.Count == 0) throw new Exception("No hay permisos o familias cargadas!");
            if (map_familia.ExisteFamilia(familia)) throw new Exception("Ya existe la familia!");
            if (ValidarPermisosRepetidos(componentes)) throw new Exception("Existe un permiso repetido!");
            map_familia.Agregar(familia);
            familia.Id = map_familia.ObtenerIdPorNombre(familia).Id;
            
            bll_dv.GenerarDVH(familia, familia.Id.ToString(),"familia");
            bll_dv.GenerarDVV("familia");
            
            foreach (SER_Componente c in componentes)
            {
                map_familia.AgregarPermisoFamilia(familia, c);
                if (c is SER_Familia familiaHijo)
                {
                    bll_dv.GenerarDVH(new List<string>() { familia.Id.ToString(), c.Id.ToString() }, "familia_familia");
                    bll_dv.GenerarDVV("familia_familia");
                }
                else if (c is SER_Permiso permiso)
                {
                    bll_dv.GenerarDVH(new List<string>() { familia.Id.ToString(), c.Id.ToString() }, "permiso_familia");
                    bll_dv.GenerarDVV("permiso_familia");
                }
            }
            bll_bitacora.RegistrarBitacora(new SER_Bitacora(SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, "Roles", "Crear familia", 1));
        }
        public bool ValidarPermisosRepetidos(List<SER_Componente> componentes)
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
    }
}
