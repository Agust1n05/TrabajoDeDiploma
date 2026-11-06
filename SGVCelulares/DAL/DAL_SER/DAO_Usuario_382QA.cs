using DAL.DAL_SER;
using System.Data;

namespace DAL
{
    public class DAO_Usuario_382QA
    {
        ComandoBD_382QA com;
        public DAO_Usuario_382QA() => com = new();
        public void Agregar_382QA(string dni, string nombre, string apellido, string correo, string nombreUsuario, string password, bool bloqueo, bool activo, int cantIntentos, int idRol, string idIdioma)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@nombre", SqlDbType.VarChar, nombre),
                new("@apellido", SqlDbType.VarChar, apellido),
                new("@correo", SqlDbType.VarChar, correo),
                new("@nombreUsuario", SqlDbType.VarChar, nombreUsuario),
                new("@password", SqlDbType.VarChar, password),
                new("@bloqueo", SqlDbType.Bit, bloqueo),
                new("@activo", SqlDbType.Bit, activo),
                new("@cantIntentos", SqlDbType.TinyInt, cantIntentos),
                new("@idRol", SqlDbType.TinyInt, idRol),
                new("@idIdioma", SqlDbType.VarChar, idIdioma)
            };
            com.Ejecutar_382QA("INSERT INTO usuario(dni,nombre,apellido,correo,nombreUsuario,password,bloqueo,activo,cantIntentos,idRol,idIdioma) values (@dni,@nombre,@apellido,@correo,@nombreUsuario,@password,@bloqueo,@activo,@cantIntentos,@idRol,@idIdioma)", parametros);
        }

        public void Borrar_382QA(string id)
        {
            throw new NotImplementedException();
        }
        public void Modificar_382QA(string dni, string nombre, string apellido, string nombreUsuario)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@nombre", SqlDbType.VarChar, nombre),
                new("@apellido", SqlDbType.VarChar, apellido),
                new("@nombreUsuario", SqlDbType.VarChar, nombreUsuario)
            };
            com.Ejecutar_382QA("UPDATE usuario SET nombre=@nombre,apellido=@apellido,nombreUsuario=@nombreUsuario WHERE dni=@dni", parametros);
        }
        public void ModificarPassword_382QA(string dni, string password)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@password", SqlDbType.VarChar, password)
            };
            com.Ejecutar_382QA("UPDATE usuario SET password=@password WHERE dni=@dni", parametros);
        }
        public DataTable Consultar_382QA() => com.Consultar_382QA("select * from usuario");

        public DataTable ConsultarPorId_382QA(string dni)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            return com.Consultar_382QA("SELECT * FROM usuario WHERE dni=@dni", parametros);
        }
        public DataTable ConsultarPorNombreUsuario_382QA(string nombreUsuario)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@nombreUsuario", SqlDbType.VarChar, nombreUsuario)
            };
            return com.Consultar_382QA("SELECT * FROM usuario WHERE nombreUsuario=@nombreUsuario", parametros);
        }
      
        public bool ValidarRepetido_382QA(string dni)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            object? res = com.Escalar_382QA("SELECT COUNT(*) FROM usuario WHERE dni=@dni", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public bool ExisteNombreUsuario_382QA(string nombreUsuario)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@nombreUsuario", SqlDbType.VarChar, nombreUsuario)
            };
            object? res = com.Escalar_382QA("SELECT COUNT(*) FROM usuario WHERE nombreUsuario=@nombreUsuario", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public bool ValidarCorreoRepetido_382QA(string correo)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@correo", SqlDbType.VarChar, correo)
            };
            object? res = com.Escalar_382QA("SELECT COUNT(*) FROM usuario WHERE correo=@correo", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public void ReiniciarIntentos_382QA(string dni)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            com.Ejecutar_382QA("UPDATE usuario SET cantIntentos=0 WHERE dni=@dni", parametros);
        }
        public void SumarCantidadIntento_382QA(string dni)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            com.Ejecutar_382QA("UPDATE usuario SET cantIntentos=cantIntentos+1 WHERE dni=@dni", parametros);
        }
        public void Bloquear_382QA(string dni)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            com.Ejecutar_382QA("UPDATE usuario SET bloqueo=1 WHERE dni=@dni", parametros);
        }
        public void Desbloquear_382QA(string dni)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            com.Ejecutar_382QA("UPDATE usuario SET bloqueo=0 WHERE dni=@dni", parametros);
        }
        public void CambiarEstadoActivo_382QA(string dni, bool activo)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@activo", SqlDbType.Bit, activo)
            };
            com.Ejecutar_382QA("UPDATE usuario SET activo=@activo WHERE dni=@dni", parametros);
        }
        public void ModificarIdioma_382QA(string dni, string idIdioma)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@idIdioma", SqlDbType.VarChar, idIdioma)
            };
            com.Ejecutar_382QA("UPDATE usuario SET idIdioma=@idIdioma WHERE dni=@dni", parametros);
        }
    }
}
