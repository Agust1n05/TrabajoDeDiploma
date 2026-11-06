using System.Data;

namespace DAL.DAL_SER
{
    public class DAO_Familia_382QA
    {
        ComandoBD_382QA com;
        public DAO_Familia_382QA() => com = new();
        public void Agregar_382QA(string nombre)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@nombre", SqlDbType.VarChar, nombre)
            };
            com.Ejecutar_382QA("INSERT INTO familia(nombre) values (@nombre)", parametros);
        }
        public void Borrar_382QA(int idFamilia)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            com.Ejecutar_382QA("DELETE FROM permiso_familia WHERE idFamilia=@idFamilia; DELETE FROM familia_familia WHERE idFamiliaPadre=@idFamilia; DELETE FROM familia WHERE idFamilia=@idFamilia", parametros);
        }
        public void Modificar_382QA(string id, params object[] datos) => throw new NotImplementedException();
        public DataTable Consultar_382QA() => com.Consultar_382QA("SELECT * FROM familia");
        public DataTable ConsultarFamiliaFamilia_382QA() => com.Consultar_382QA("select * from familia_familia");
        public DataTable ConsultarFamiliaPermiso_382QA() => com.Consultar_382QA("select * from permiso_familia");
        public void AgregarPermiso_382QA(int idFamilia, int idPermiso)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia),
                new("@idPermiso", SqlDbType.Int, idPermiso)
            };
            com.Ejecutar_382QA("INSERT INTO permiso_familia(idFamilia, idPermiso) values (@idFamilia, @idPermiso)", parametros);
        }
        public void AgregarFamilia_382QA(int idFamiliaPadre, int idFamiliaHija)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@idFamiliaPadre", SqlDbType.Int, idFamiliaPadre),
                new("@idFamiliaHija", SqlDbType.Int, idFamiliaHija)
            };
            com.Ejecutar_382QA("INSERT INTO familia_familia(idFamiliaPadre,idFamiliaHija) values (@idFamiliaPadre,@idFamiliaHija)", parametros);
        }
        public DataTable ObtenerIdPorNombre_382QA(string nombre)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@nombre", SqlDbType.VarChar, nombre)
            };
            return com.Consultar_382QA("SELECT TOP 1 idFamilia, nombre FROM familia WHERE nombre=@nombre", parametros);
        }

        // Métodos que van a servir para armar recursivamente los nodos de un arbol determinado
        public DataTable ConsultarPermisos_382QA(int idFamilia)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            return com.Consultar_382QA("select p.idPermiso, p.nombre from permiso p inner join permiso_familia pf on p.idPermiso=pf.idPermiso where pf.idFamilia=@idFamilia", parametros);
        }
        public DataTable ConsultarFamilia_382QA(int idFamilia)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            return com.Consultar_382QA("select idFamilia, nombre from familia where idFamilia=@idFamilia", parametros);
        }
        public DataTable ConsultarFamiliasHijas_382QA(int idFamilia)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            return com.Consultar_382QA("select f.idFamilia, f.nombre from familia f inner join familia_familia ff on f.idFamilia=ff.idFamiliaHija where ff.idFamiliaPadre=@idFamilia", parametros);
        }
        public bool ExisteFamilia_382QA(string nombreFamilia)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@familia", SqlDbType.VarChar, nombreFamilia)
            };
            object? res = com.Escalar_382QA("SELECT COUNT(*) FROM familia WHERE nombre=@familia", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public bool EstaEnUso_382QA(int idFamilia)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            object? res = com.Escalar_382QA("SELECT (SELECT COUNT(*) FROM rol_familia WHERE idFamilia=@idFamilia) (SELECT COUNT(*) FROM familia_familia WHERE idFamiliaHija=@idFamilia)", parametros);
            return Convert.ToInt32(res) > 0;
        }
    }
}
