using MAP.MAP_SER;
using Servicio;

namespace BLL.BLL_SER
{
    public class BLL_Familia_382QA : IABMC_382QA<SER_Familia_382QA>
    {
        MAP_Familia_382QA map_familia;
        BLL_Bitacora_382QA bll_bitacora;
        BLL_DV_382QA bll_dv;

        public BLL_Familia_382QA()
        {
            map_familia = new MAP_Familia_382QA();
            bll_bitacora = new BLL_Bitacora_382QA();
            bll_dv = new BLL_DV_382QA();
        }
        public void Agregar_382QA(SER_Familia_382QA familia)
        {
            map_familia.Agregar_382QA(familia);
          
        }

        public void Borrar_382QA(SER_Familia_382QA familia)
        {
            if (map_familia.EstaEnUso_382QA(familia))
                throw new Exception("No se puede borrar: la familia está asignada a un rol u otra familia.");

            map_familia.Borrar_382QA(familia);

            bll_dv.BorrarDVH_382QA("familia", familia.Id.ToString());

            foreach (var componente in familia.Componentes)
            {
                string tabla = componente is SER_Familia_382QA ? "familia_familia" : "permiso_familia";
                string id = $"{familia.Id}-{componente.Id}";
                bll_dv.BorrarDVH_382QA(tabla, id);
            }

            bll_dv.GenerarDVV_382QA("familia");
            bll_dv.GenerarDVV_382QA("familia_familia");
            bll_dv.GenerarDVV_382QA("permiso_familia");

            bll_bitacora.RegistrarBitacora_382QA(new SER_Bitacora_382QA(SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, "Roles", "Borrar familia", 1));
        }

        public List<SER_Familia_382QA> Consultar_382QA()
        {
            return map_familia.Consultar_382QA();
        }
        public List<object[]> ConsultarFamiliaFamilia_382QA()
        {
            return map_familia.ConsultarFamiliaFamilia_382QA();
        }
        public List<object[]> ConsultarFamiliaPermiso_382QA()
        {
            return map_familia.ConsultarFamiliaPermiso_382QA();
        }
        public SER_Familia_382QA ConsultarPorId_382QA(SER_Familia_382QA familia)
        {
            return map_familia.ObtenerArbol_382QA(familia.Id);
        }

        public void Modificar_382QA(SER_Familia_382QA familia)
        {
            throw new NotImplementedException();
        }
        public void Agregar_382QA(SER_Familia_382QA familia, List<SER_Componente_382QA> componentes)
        {
            if (componentes.Count == 0) throw new Exception("No hay permisos o familias cargadas!");
            if (map_familia.ExisteFamilia_382QA(familia)) throw new Exception("Ya existe la familia!");
            if (ValidarPermisosRepetidos_382QA(componentes)) throw new Exception("Existe un permiso repetido!");
            map_familia.Agregar_382QA(familia);
            familia.Id = map_familia.ObtenerIdPorNombre_382QA(familia).Id;
            
            bll_dv.GenerarDVH_382QA(familia, familia.Id.ToString(),"familia");
            bll_dv.GenerarDVV_382QA("familia");
            
            foreach (SER_Componente_382QA c in componentes)
            {
                map_familia.AgregarPermisoFamilia_382QA(familia, c);
                if (c is SER_Familia_382QA familiaHijo)
                {
                    bll_dv.GenerarDVH_382QA(new List<string>() { familia.Id.ToString(), c.Id.ToString() }, "familia_familia");
                    bll_dv.GenerarDVV_382QA("familia_familia");
                }
                else if (c is SER_Permiso_382QA permiso)
                {
                    bll_dv.GenerarDVH_382QA(new List<string>() { familia.Id.ToString(), c.Id.ToString() }, "permiso_familia");
                    bll_dv.GenerarDVV_382QA("permiso_familia");
                }
            }
            bll_bitacora.RegistrarBitacora_382QA(new SER_Bitacora_382QA(SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, "Roles", "Crear familia", 1));
        }
        public bool ValidarPermisosRepetidos_382QA(List<SER_Componente_382QA> componentes)
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
    }
}
