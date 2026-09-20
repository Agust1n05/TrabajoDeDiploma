using System.Data;

namespace DAL
{
    public class DAO_Rol
    {
        ComandoBD com;
        public DAO_Rol() => com = new();
        public void Agregar(string nombre)
        {
            var parametros = new List<ParametroBD>
            {
                new("@nombre", SqlDbType.VarChar, nombre)
            };
            com.Ejecutar("INSERT INTO rol(nombre) values (@nombre)", parametros);
        }
        public DataTable ObtenerIdPorNombre(string nombre)
        {
            var parametros = new List<ParametroBD>
            {
                new("@nombre", SqlDbType.VarChar, nombre)
            };
            return com.Consultar("SELECT TOP 1 idRol, nombre FROM rol WHERE nombre=@nombre", parametros);
        }
        public DataTable Consultar() => com.Consultar("select * from rol");
        public DataTable ConsultarRol(int idRol)
        {
            var parametros = new List<ParametroBD>
            {
                new("@idRol", SqlDbType.Int, idRol)
            };
            return com.Consultar("SELECT idRol, nombre FROM rol WHERE idRol=@idRol", parametros);
        }
        public DataTable ConsultarPermisos(int idRol)
        {
            var parametros = new List<ParametroBD>
            {
                new("@idRol", SqlDbType.Int, idRol)
            };
            return com.Consultar("select p.idPermiso, p.nombre from permiso p inner join rol_permiso rp on p.idPermiso = rp.idPermiso where rp.idRol = @idRol", parametros);
        }
        public DataTable ConsultarFamilia(int idRol)
        {
            var parametros = new List<ParametroBD>
            {
                new("@idRol", SqlDbType.Int, idRol)
            };
            return com.Consultar("select f.idFamilia, f.nombre from familia f inner join rol_familia rf on f.idFamilia = rf.idFamilia where rf.idRol = @idRol", parametros);
        }
        public DataTable ConsultarRolFamilia() => com.Consultar("select * from rol_familia");
        public DataTable ConsultarRolPermiso() => com.Consultar("select * from rol_permiso");
        public bool ExisteRol(string nombreRol)
        {
            var parametros = new List<ParametroBD>
            {
                new("@rol", SqlDbType.VarChar, nombreRol)
            };
            object? res = com.Escalar("SELECT COUNT(*) FROM rol WHERE nombre=@rol", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public void AgregarPermiso(int idRol, int idPermiso)
        {
            var parametros = new List<ParametroBD>
            {
                new("@idRol", SqlDbType.Int, idRol),
                new("@idPermiso", SqlDbType.Int, idPermiso)
            };
            com.Ejecutar("INSERT INTO rol_permiso(idRol,idPermiso) values (@idRol,@idPermiso)", parametros);
        }
        public void AgregarFamilia(int idRol, int idFamilia)
        {
            var parametros = new List<ParametroBD>
            {
                new("@idRol", SqlDbType.Int, idRol),
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            com.Ejecutar("INSERT INTO rol_familia(idRol,idFamilia) values (@idRol,@idFamilia)", parametros);
        }
        public void Borrar(int idRol)
        {
            var parametros = new List<ParametroBD>
            {
                new("@idRol", SqlDbType.Int, idRol)
            };
            com.Ejecutar("DELETE FROM rol_permiso WHERE idRol=@idRol; DELETE FROM rol_familia WHERE idRol=@idRol; DELETE FROM rol WHERE idRol=@idRol", parametros);
        }
        public bool EstaEnUso(int idRol)
        {
            var parametros = new List<ParametroBD>
            {
                new("@idRol", SqlDbType.Int, idRol)
            };
            object? res = com.Escalar("SELECT COUNT(*) FROM usuario WHERE idRol=@idRol", parametros);
            return Convert.ToInt32(res) > 0;
        }
    }
}
