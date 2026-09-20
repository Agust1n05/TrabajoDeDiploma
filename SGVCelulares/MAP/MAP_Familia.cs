using DAL;
using Servicio;
using System.Data;

namespace MAP
{
    public class MAP_Familia : IABMC<SER_Familia>
    {
        DAO_Familia dao_familia;
        public MAP_Familia() => dao_familia = new();
        public void Agregar(SER_Familia familia) => dao_familia.Agregar(familia.Nombre);
        public void Borrar(SER_Familia familia) => dao_familia.Borrar(familia.Id);
        public bool EstaEnUso(SER_Familia familia) => dao_familia.EstaEnUso(familia.Id);

        public List<SER_Familia> Consultar()
        {
            var listaAux = new List<SER_Familia>();
            var lista = new List<SER_Familia>();
            DataTable tabla = dao_familia.Consultar();
            foreach (DataRow fila in tabla.Rows)
            {
                listaAux.Add(new SER_Familia(fila.ItemArray));
            }
            foreach (SER_Familia familia in listaAux)
            {
                lista.Add(ObtenerArbol(familia.Id));
            }
            return lista;
        }
        public List<object[]> ConsultarFamiliaFamilia()
        {
            var lista = new List<object[]>();
            DataTable tabla = dao_familia.ConsultarFamiliaFamilia();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(fila.ItemArray); // datos[0] = IdFamilia, datos[1] = IdFamilia
            }
            return lista;
        }
        public List<object[]> ConsultarFamiliaPermiso()
        {
            var lista = new List<object[]>();
            DataTable tabla = dao_familia.ConsultarFamiliaPermiso();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(fila.ItemArray); // datos[0] = IdFamilia, datos[1] = IdPermiso
            }
            return lista;
        }
        public SER_Familia ConsultarPorId(SER_Familia familia) => throw new NotImplementedException();

        public void Modificar(SER_Familia familia) => throw new NotImplementedException();
        public bool ExisteFamilia(SER_Familia familia) => dao_familia.ExisteFamilia(familia.Nombre);
        public void AgregarPermisoFamilia(SER_Familia familia, SER_Componente componente)
        {
            if (componente is SER_Permiso p)
            {
                dao_familia.AgregarPermiso(familia.Id, p.Id);
            }
            else if (componente is SER_Familia f)
            {
                dao_familia.AgregarFamilia(familia.Id, f.Id);
            }
        }
        public SER_Familia ObtenerArbol(int idFamilia)
        {
            SER_Familia familia = ConsultarFamilia(idFamilia);

            foreach (SER_Permiso permiso in ConsultarPermisos(idFamilia))
            {
                familia.Componentes.Add(permiso);
            }
            foreach (SER_Familia hija in ConsultarFamiliasHijas(idFamilia))
            {
                familia.Componentes.Add(ObtenerArbol(hija.Id));
            }
            return familia;
        }

        private SER_Familia ConsultarFamilia(int idFamilia)
        {
            DataTable tabla = dao_familia.ConsultarFamilia(idFamilia);
            return tabla.Rows.Count > 0 ? new SER_Familia(tabla.Rows[0].ItemArray) : null;
        }
        private List<SER_Permiso> ConsultarPermisos(int idFamilia)
        {
            var lista = new List<SER_Permiso>();
            DataTable tabla = dao_familia.ConsultarPermisos(idFamilia);
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new SER_Permiso(fila.ItemArray));
            }
            return lista;
        }
        private List<SER_Familia> ConsultarFamiliasHijas(int idFamilia)
        {
            var lista = new List<SER_Familia>();
            DataTable tabla = dao_familia.ConsultarFamiliasHijas(idFamilia);
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new SER_Familia(fila.ItemArray));
            }
            return lista;
        }
        public SER_Familia ObtenerIdPorNombre(SER_Familia familia)
        {
            DataTable tabla = dao_familia.ObtenerIdPorNombre(familia.Nombre);
            return tabla.Rows.Count > 0 ? new SER_Familia(tabla.Rows[0].ItemArray) : null;
        }
    }
}
