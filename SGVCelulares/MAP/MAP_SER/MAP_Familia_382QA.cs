using DAL.DAL_SER;
using Servicio;
using System.Data;

namespace MAP.MAP_SER
{
    public class MAP_Familia_382QA : IABMC_382QA<SER_Familia_382QA>
    {
        DAO_Familia_382QA dao_familia;
        public MAP_Familia_382QA() => dao_familia = new();
        public void Agregar_382QA(SER_Familia_382QA familia) => dao_familia.Agregar_382QA(familia.Nombre);
        public void Borrar_382QA(SER_Familia_382QA familia) => dao_familia.Borrar_382QA(familia.Id);
        public bool EstaEnUso_382QA(SER_Familia_382QA familia) => dao_familia.EstaEnUso_382QA(familia.Id);

        public List<SER_Familia_382QA> Consultar_382QA()
        {
            var listaAux = new List<SER_Familia_382QA>();
            var lista = new List<SER_Familia_382QA>();
            DataTable tabla = dao_familia.Consultar_382QA();
            foreach (DataRow fila in tabla.Rows)
            {
                listaAux.Add(new SER_Familia_382QA(fila.ItemArray));
            }
            foreach (SER_Familia_382QA familia in listaAux)
            {
                lista.Add(ObtenerArbol_382QA(familia.Id));
            }
            return lista;
        }
        public List<object[]> ConsultarFamiliaFamilia_382QA()
        {
            var lista = new List<object[]>();
            DataTable tabla = dao_familia.ConsultarFamiliaFamilia_382QA();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(fila.ItemArray); // datos[0] = IdFamilia, datos[1] = IdFamilia
            }
            return lista;
        }
        public List<object[]> ConsultarFamiliaPermiso_382QA()
        {
            var lista = new List<object[]>();
            DataTable tabla = dao_familia.ConsultarFamiliaPermiso_382QA();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(fila.ItemArray); // datos[0] = IdFamilia, datos[1] = IdPermiso
            }
            return lista;
        }
        public SER_Familia_382QA ConsultarPorId_382QA(SER_Familia_382QA familia) => throw new NotImplementedException();

        public void Modificar_382QA(SER_Familia_382QA familia) => throw new NotImplementedException();
        public bool ExisteFamilia_382QA(SER_Familia_382QA familia) => dao_familia.ExisteFamilia_382QA(familia.Nombre);
        public void AgregarPermisoFamilia_382QA(SER_Familia_382QA familia, SER_Componente_382QA componente)
        {
            if (componente is SER_Permiso_382QA p)
            {
                dao_familia.AgregarPermiso_382QA(familia.Id, p.Id);
            }
            else if (componente is SER_Familia_382QA f)
            {
                dao_familia.AgregarFamilia_382QA(familia.Id, f.Id);
            }
        }
        public SER_Familia_382QA ObtenerArbol_382QA(int idFamilia)
        {
            SER_Familia_382QA familia = ConsultarFamilia_382QA(idFamilia);

            foreach (SER_Permiso_382QA permiso in ConsultarPermisos_382QA(idFamilia))
            {
                familia.Componentes.Add(permiso);
            }
            foreach (SER_Familia_382QA hija in ConsultarFamiliasHijas_382QA(idFamilia))
            {
                familia.Componentes.Add(ObtenerArbol_382QA(hija.Id));
            }
            return familia;
        }

        private SER_Familia_382QA ConsultarFamilia_382QA(int idFamilia)
        {
            DataTable tabla = dao_familia.ConsultarFamilia_382QA(idFamilia);
            return tabla.Rows.Count > 0 ? new SER_Familia_382QA(tabla.Rows[0].ItemArray) : null;
        }
        private List<SER_Permiso_382QA> ConsultarPermisos_382QA(int idFamilia)
        {
            var lista = new List<SER_Permiso_382QA>();
            DataTable tabla = dao_familia.ConsultarPermisos_382QA(idFamilia);
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new SER_Permiso_382QA(fila.ItemArray));
            }
            return lista;
        }
        private List<SER_Familia_382QA> ConsultarFamiliasHijas_382QA(int idFamilia)
        {
            var lista = new List<SER_Familia_382QA>();
            DataTable tabla = dao_familia.ConsultarFamiliasHijas_382QA(idFamilia);
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new SER_Familia_382QA(fila.ItemArray));
            }
            return lista;
        }
        public SER_Familia_382QA ObtenerIdPorNombre_382QA(SER_Familia_382QA familia)
        {
            DataTable tabla = dao_familia.ObtenerIdPorNombre_382QA(familia.Nombre);
            return tabla.Rows.Count > 0 ? new SER_Familia_382QA(tabla.Rows[0].ItemArray) : null;
        }
    }
}
