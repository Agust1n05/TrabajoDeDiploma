using System.Data;

namespace DAL.DAL_SER
{
    public class DAO_Rol_382QA
    {
        ComandoBD_382QA com;
        public DAO_Rol_382QA() => com = new();
        public void Agregar_382QA(string nombre)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@nombre", SqlDbType.VarChar, nombre)
            };
            com.Ejecutar_382QA("INSERT INTO rol(nombre) values (@nombre)", parametros);
        }
        public DataTable ObtenerIdPorNombre_382QA(string nombre)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@nombre", SqlDbType.VarChar, nombre)
            };
            return com.Consultar_382QA("SELECT TOP 1 idRol, nombre FROM rol WHERE nombre=@nombre", parametros);
        }
        public DataTable Consultar_382QA() => com.Consultar_382QA("select * from rol");
        public DataTable ConsultarRol_382QA(int idRol)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@idRol", SqlDbType.Int, idRol)
            };
            return com.Consultar_382QA("SELECT idRol, nombre FROM rol WHERE idRol=@idRol", parametros);
        }
        public DataTable ConsultarPermisos_382QA(int idRol)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@idRol", SqlDbType.Int, idRol)
            };
            return com.Consultar_382QA("select p.idPermiso, p.nombre from permiso p inner join rol_permiso rp on p.idPermiso = rp.idPermiso where rp.idRol = @idRol", parametros);
        }
        public DataTable ConsultarFamilia_382QA(int idRol)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@idRol", SqlDbType.Int, idRol)
            };
            return com.Consultar_382QA("select f.idFamilia, f.nombre from familia f inner join rol_familia rf on f.idFamilia = rf.idFamilia where rf.idRol = @idRol", parametros);
        }
        public DataTable ConsultarRolFamilia_382QA() => com.Consultar_382QA("select * from rol_familia");
        public DataTable ConsultarRolPermiso_382QA() => com.Consultar_382QA("select * from rol_permiso");
        public bool ExisteRol_382QA(string nombreRol)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@rol", SqlDbType.VarChar, nombreRol)
            };
            object? res = com.Escalar_382QA("SELECT COUNT(*) FROM rol WHERE nombre=@rol", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public void AgregarPermiso_382QA(int idRol, int idPermiso)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@idRol", SqlDbType.Int, idRol),
                new("@idPermiso", SqlDbType.Int, idPermiso)
            };
            com.Ejecutar_382QA("INSERT INTO rol_permiso(idRol,idPermiso) values (@idRol,@idPermiso)", parametros);
        }
        public void AgregarFamilia_382QA(int idRol, int idFamilia)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@idRol", SqlDbType.Int, idRol),
                new("@idFamilia", SqlDbType.Int, idFamilia)
            };
            com.Ejecutar_382QA("INSERT INTO rol_familia(idRol,idFamilia) values (@idRol,@idFamilia)", parametros);
        }
        public void Borrar_382QA(int idRol)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@idRol", SqlDbType.Int, idRol)
            };
            com.Ejecutar_382QA("DELETE FROM rol_permiso WHERE idRol=@idRol; DELETE FROM rol_familia WHERE idRol=@idRol; DELETE FROM rol WHERE idRol=@idRol", parametros);
        }
        public bool EstaEnUso_382QA(int idRol)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@idRol", SqlDbType.Int, idRol)
            };
            object? res = com.Escalar_382QA("SELECT COUNT(*) FROM usuario WHERE idRol=@idRol", parametros);
            return Convert.ToInt32(res) > 0;
        }
    }
}
