using DAL;
using Servicio;
using System.Data;

namespace MAP
{
    public class MAP_Rol
    {
        DAO_Rol dao_rol;
        MAP_Familia map_familia;
        public MAP_Rol()
        {
            dao_rol = new DAO_Rol();
            map_familia = new MAP_Familia();
        }
        public List<SER_Rol> Consultar()
        {
            var listaAux = new List<SER_Rol>();
            var lista = new List<SER_Rol>();
            DataTable tabla = dao_rol.Consultar();
            foreach (DataRow fila in tabla.Rows)
            {
                listaAux.Add(new SER_Rol(fila.ItemArray));
            }
            foreach (SER_Rol rol in listaAux)
            {
                lista.Add(ObtenerArbol(rol.Id));
            }
            return lista;
        }
        public List<object[]> ConsultarRolFamilia()
        {
            var lista = new List<object[]>();
            DataTable tabla = dao_rol.ConsultarRolFamilia();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(fila.ItemArray); // datos[0] = IdRol, datos[1] = IdFamilia
            }
            return lista;
        }
        public List<object[]> ConsultarRolPermiso()
        {
            var lista = new List<object[]>();
            DataTable tabla = dao_rol.ConsultarRolPermiso();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(fila.ItemArray); // datos[0] = IdRol, datos[1] = IdPermiso
            }
            return lista;
        }
        public SER_Rol ObtenerArbol(int idRol)
        {
            SER_Rol rol = ConsultarRol(idRol);
            foreach (SER_Permiso permiso in ConsultarPermisos(idRol))
            {
                rol.Componentes.Add(permiso);
            }
            foreach (SER_Familia familia in ConsultarFamilia(idRol))
            {
                rol.Componentes.Add(map_familia.ObtenerArbol(familia.Id));
            }
            return rol;
        }

        private SER_Rol ConsultarRol(int idRol)
        {
            DataTable tabla = dao_rol.ConsultarRol(idRol);
            return tabla.Rows.Count > 0 ? new SER_Rol(tabla.Rows[0].ItemArray) : null;
        }
        private List<SER_Permiso> ConsultarPermisos(int idRol)
        {
            var lista = new List<SER_Permiso>();
            DataTable tabla = dao_rol.ConsultarPermisos(idRol);
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new SER_Permiso(fila.ItemArray));
            }
            return lista;
        }
        private List<SER_Familia> ConsultarFamilia(int idRol)
        {
            var lista = new List<SER_Familia>();
            DataTable tabla = dao_rol.ConsultarFamilia(idRol);
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new SER_Familia(fila.ItemArray));
            }
            return lista;
        }
        public bool ExisteRol(SER_Rol rol) => dao_rol.ExisteRol(rol.Nombre);
        public void Agregar(SER_Rol rol) => dao_rol.Agregar(rol.Nombre);
        public void AgregarPermisoFamilia(SER_Rol rol, SER_Componente componente)
        {
            if (componente is SER_Permiso p)
            {
                dao_rol.AgregarPermiso(rol.Id, p.Id);
            }
            else if (componente is SER_Familia f)
            {
                dao_rol.AgregarFamilia(rol.Id, f.Id);
            }
        }
        public SER_Rol ObtenerIdPorNombre(SER_Rol rol)
        {
            DataTable tabla = dao_rol.ObtenerIdPorNombre(rol.Nombre);
            return tabla.Rows.Count > 0 ? new SER_Rol(tabla.Rows[0].ItemArray) : null;
        }
        public SER_Rol ObtenerRolPorId(int idRol)
        {
            DataTable tabla = dao_rol.ConsultarRol(idRol);
            return tabla.Rows.Count > 0 ? new SER_Rol(tabla.Rows[0].ItemArray) : null;
        }
        public void Borrar(SER_Rol rol) => dao_rol.Borrar(rol.Id);
        public bool EstaEnUso(SER_Rol rol) => dao_rol.EstaEnUso(rol.Id);
    }
}
