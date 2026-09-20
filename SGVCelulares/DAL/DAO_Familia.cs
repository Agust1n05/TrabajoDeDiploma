using System.Data;

namespace DAL
{
    public class DAO_Familia
    {
        ComandoBD com;
        public DAO_Familia() => com = new();
        public void Agregar(string nombre)
        {
            var parametros = new List<ParametroBD>
            {
                new("@nombre", SqlDbType.VarChar, nombre)
            };
            com.Ejecutar("INSERT INTO familia(nombre) values (@nombre)", parametros);
        }
        public void Borrar(int idFamilia)
        {
            var parametros = new List<ParametroBD>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            com.Ejecutar("DELETE FROM permiso_familia WHERE idFamilia=@idFamilia; DELETE FROM familia_familia WHERE idFamiliaPadre=@idFamilia; DELETE FROM familia WHERE idFamilia=@idFamilia", parametros);
        }
        public void Modificar(string id, params object[] datos) => throw new NotImplementedException();
        public DataTable Consultar() => com.Consultar("SELECT * FROM familia");
        public DataTable ConsultarFamiliaFamilia() => com.Consultar("select * from familia_familia");
        public DataTable ConsultarFamiliaPermiso() => com.Consultar("select * from permiso_familia");
        public void AgregarPermiso(int idFamilia, int idPermiso)
        {
            var parametros = new List<ParametroBD>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia),
                new("@idPermiso", SqlDbType.Int, idPermiso)
            };
            com.Ejecutar("INSERT INTO permiso_familia(idFamilia, idPermiso) values (@idFamilia, @idPermiso)", parametros);
        }
        public void AgregarFamilia(int idFamiliaPadre, int idFamiliaHija)
        {
            var parametros = new List<ParametroBD>
            {
                new("@idFamiliaPadre", SqlDbType.Int, idFamiliaPadre),
                new("@idFamiliaHija", SqlDbType.Int, idFamiliaHija)
            };
            com.Ejecutar("INSERT INTO familia_familia(idFamiliaPadre,idFamiliaHija) values (@idFamiliaPadre,@idFamiliaHija)", parametros);
        }
        public DataTable ObtenerIdPorNombre(string nombre)
        {
            var parametros = new List<ParametroBD>
            {
                new("@nombre", SqlDbType.VarChar, nombre)
            };
            return com.Consultar("SELECT TOP 1 idFamilia, nombre FROM familia WHERE nombre=@nombre", parametros);
        }

        // Métodos que van a servir para armar recursivamente los nodos de un arbol determinado
        public DataTable ConsultarPermisos(int idFamilia)
        {
            var parametros = new List<ParametroBD>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            return com.Consultar("select p.idPermiso, p.nombre from permiso p inner join permiso_familia pf on p.idPermiso=pf.idPermiso where pf.idFamilia=@idFamilia", parametros);
        }
        public DataTable ConsultarFamilia(int idFamilia)
        {
            var parametros = new List<ParametroBD>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            return com.Consultar("select idFamilia, nombre from familia where idFamilia=@idFamilia", parametros);
        }
        public DataTable ConsultarFamiliasHijas(int idFamilia)
        {
            var parametros = new List<ParametroBD>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            return com.Consultar("select f.idFamilia, f.nombre from familia f inner join familia_familia ff on f.idFamilia=ff.idFamiliaHija where ff.idFamiliaPadre=@idFamilia", parametros);
        }
        public bool ExisteFamilia(string nombreFamilia)
        {
            var parametros = new List<ParametroBD>
            {
                new("@familia", SqlDbType.VarChar, nombreFamilia)
            };
            object? res = com.Escalar("SELECT COUNT(*) FROM familia WHERE nombre=@familia", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public bool EstaEnUso(int idFamilia)
        {
            var parametros = new List<ParametroBD>
            {
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            object? res = com.Escalar("SELECT (SELECT COUNT(*) FROM rol_familia WHERE idFamilia=@idFamilia) (SELECT COUNT(*) FROM familia_familia WHERE idFamiliaHija=@idFamilia)", parametros);
            return Convert.ToInt32(res) > 0;
        }
    }
}
