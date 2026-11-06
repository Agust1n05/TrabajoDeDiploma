using DAL.DAL_SER;
using Servicio;
using System.Data;

namespace MAP.MAP_SER
{
    public class MAP_Rol_382QA
    {
        DAO_Rol_382QA dao_rol;
        MAP_Familia_382QA map_familia;
        public MAP_Rol_382QA()
        {
            dao_rol = new DAO_Rol_382QA();
            map_familia = new MAP_Familia_382QA();
        }
        public List<SER_Rol_382QA> Consultar_382QA()
        {
            var listaAux = new List<SER_Rol_382QA>();
            var lista = new List<SER_Rol_382QA>();
            DataTable tabla = dao_rol.Consultar_382QA();
            foreach (DataRow fila in tabla.Rows)
            {
                listaAux.Add(new SER_Rol_382QA(fila.ItemArray));
            }
            foreach (SER_Rol_382QA rol in listaAux)
            {
                lista.Add(ObtenerArbol_382QA(rol.Id));
            }
            return lista;
        }
        public List<object[]> ConsultarRolFamilia_382QA()
        {
            var lista = new List<object[]>();
            DataTable tabla = dao_rol.ConsultarRolFamilia_382QA();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(fila.ItemArray); // datos[0] = IdRol, datos[1] = IdFamilia
            }
            return lista;
        }
        public List<object[]> ConsultarRolPermiso_382QA()
        {
            var lista = new List<object[]>();
            DataTable tabla = dao_rol.ConsultarRolPermiso_382QA();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(fila.ItemArray); // datos[0] = IdRol, datos[1] = IdPermiso
            }
            return lista;
        }
        public SER_Rol_382QA ObtenerArbol_382QA(int idRol)
        {
            SER_Rol_382QA rol = ConsultarRol_382QA(idRol);
            foreach (SER_Permiso_382QA permiso in ConsultarPermisos_382QA(idRol))
            {
                rol.Componentes.Add(permiso);
            }
            foreach (SER_Familia_382QA familia in ConsultarFamilia_382QA(idRol))
            {
                rol.Componentes.Add(map_familia.ObtenerArbol_382QA(familia.Id));
            }
            return rol;
        }

        private SER_Rol_382QA ConsultarRol_382QA(int idRol)
        {
            DataTable tabla = dao_rol.ConsultarRol_382QA(idRol);
            return tabla.Rows.Count > 0 ? new SER_Rol_382QA(tabla.Rows[0].ItemArray) : null;
        }
        private List<SER_Permiso_382QA> ConsultarPermisos_382QA(int idRol)
        {
            var lista = new List<SER_Permiso_382QA>();
            DataTable tabla = dao_rol.ConsultarPermisos_382QA(idRol);
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new SER_Permiso_382QA(fila.ItemArray));
            }
            return lista;
        }
        private List<SER_Familia_382QA> ConsultarFamilia_382QA(int idRol)
        {
            var lista = new List<SER_Familia_382QA>();
            DataTable tabla = dao_rol.ConsultarFamilia_382QA(idRol);
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new SER_Familia_382QA(fila.ItemArray));
            }
            return lista;
        }
        public bool ExisteRol_382QA(SER_Rol_382QA rol) => dao_rol.ExisteRol_382QA(rol.Nombre);
        public void Agregar_382QA(SER_Rol_382QA rol) => dao_rol.Agregar_382QA(rol.Nombre);
        public void AgregarPermisoFamilia_382QA(SER_Rol_382QA rol, SER_Componente_382QA componente)
        {
            if (componente is SER_Permiso_382QA p)
            {
                dao_rol.AgregarPermiso_382QA(rol.Id, p.Id);
            }
            else if (componente is SER_Familia_382QA f)
            {
                dao_rol.AgregarFamilia_382QA(rol.Id, f.Id);
            }
        }
        public SER_Rol_382QA ObtenerIdPorNombre_382QA(SER_Rol_382QA rol)
        {
            DataTable tabla = dao_rol.ObtenerIdPorNombre_382QA(rol.Nombre);
            return tabla.Rows.Count > 0 ? new SER_Rol_382QA(tabla.Rows[0].ItemArray) : null;
        }
        public SER_Rol_382QA ObtenerRolPorId_382QA(int idRol)
        {
            DataTable tabla = dao_rol.ConsultarRol_382QA(idRol);
            return tabla.Rows.Count > 0 ? new SER_Rol_382QA(tabla.Rows[0].ItemArray) : null;
        }
        public void Borrar_382QA(SER_Rol_382QA rol) => dao_rol.Borrar_382QA(rol.Id);
        public bool EstaEnUso_382QA(SER_Rol_382QA rol) => dao_rol.EstaEnUso_382QA(rol.Id);
    }
}
