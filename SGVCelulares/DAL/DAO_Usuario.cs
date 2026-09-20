using System.Data;

namespace DAL
{
    public class DAO_Usuario
    {
        ComandoBD com;
        public DAO_Usuario() => com = new();
        public void Agregar(string dni, string nombre, string apellido, string correo, string nombreUsuario, string password, bool bloqueo, bool activo, int cantIntentos, int idRol, string idIdioma)
        {
            var parametros = new List<ParametroBD>
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
            com.Ejecutar("INSERT INTO usuario(dni,nombre,apellido,correo,nombreUsuario,password,bloqueo,activo,cantIntentos,idRol,idIdioma) values (@dni,@nombre,@apellido,@correo,@nombreUsuario,@password,@bloqueo,@activo,@cantIntentos,@idRol,@idIdioma)", parametros);
        }

        public void Borrar(string id)
        {
            throw new NotImplementedException();
        }
        public void Modificar(string dni, string nombre, string apellido, string nombreUsuario)
        {
            var parametros = new List<ParametroBD>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@nombre", SqlDbType.VarChar, nombre),
                new("@apellido", SqlDbType.VarChar, apellido),
                new("@nombreUsuario", SqlDbType.VarChar, nombreUsuario)
            };
            com.Ejecutar("UPDATE usuario SET nombre=@nombre,apellido=@apellido,nombreUsuario=@nombreUsuario WHERE dni=@dni", parametros);
        }
        public void ModificarPassword(string dni, string password)
        {
            var parametros = new List<ParametroBD>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@password", SqlDbType.VarChar, password)
            };
            com.Ejecutar("UPDATE usuario SET password=@password WHERE dni=@dni", parametros);
        }
        public DataTable Consultar() => com.Consultar("select * from usuario");

        public DataTable ConsultarPorId(string dni)
        {
            var parametros = new List<ParametroBD>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            return com.Consultar("SELECT * FROM usuario WHERE dni=@dni", parametros);
        }
        public DataTable ConsultarPorNombreUsuario(string nombreUsuario)
        {
            var parametros = new List<ParametroBD>
            {
                new("@nombreUsuario", SqlDbType.VarChar, nombreUsuario)
            };
            return com.Consultar("SELECT * FROM usuario WHERE nombreUsuario=@nombreUsuario", parametros);
        }
      
        public bool ValidarRepetido(string dni)
        {
            var parametros = new List<ParametroBD>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            object? res = com.Escalar("SELECT COUNT(*) FROM usuario WHERE dni=@dni", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public bool ExisteNombreUsuario(string nombreUsuario)
        {
            var parametros = new List<ParametroBD>
            {
                new("@nombreUsuario", SqlDbType.VarChar, nombreUsuario)
            };
            object? res = com.Escalar("SELECT COUNT(*) FROM usuario WHERE nombreUsuario=@nombreUsuario", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public bool ValidarCorreoRepetido(string correo)
        {
            var parametros = new List<ParametroBD>
            {
                new("@correo", SqlDbType.VarChar, correo)
            };
            object? res = com.Escalar("SELECT COUNT(*) FROM usuario WHERE correo=@correo", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public void ReiniciarIntentos(string dni)
        {
            var parametros = new List<ParametroBD>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            com.Ejecutar("UPDATE usuario SET cantIntentos=0 WHERE dni=@dni", parametros);
        }
        public void SumarCantidadIntento(string dni)
        {
            var parametros = new List<ParametroBD>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            com.Ejecutar("UPDATE usuario SET cantIntentos=cantIntentos+1 WHERE dni=@dni", parametros);
        }
        public void Bloquear(string dni)
        {
            var parametros = new List<ParametroBD>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            com.Ejecutar("UPDATE usuario SET bloqueo=1 WHERE dni=@dni", parametros);
        }
        public void Desbloquear(string dni)
        {
            var parametros = new List<ParametroBD>
            {
                new("@dni", SqlDbType.VarChar, dni)
            };
            com.Ejecutar("UPDATE usuario SET bloqueo=0 WHERE dni=@dni", parametros);
        }
        public void CambiarEstadoActivo(string dni, bool activo)
        {
            var parametros = new List<ParametroBD>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@activo", SqlDbType.Bit, activo)
            };
            com.Ejecutar("UPDATE usuario SET activo=@activo WHERE dni=@dni", parametros);
        }
        public void ModificarIdioma(string dni, string idIdioma)
        {
            var parametros = new List<ParametroBD>
            {
                new("@dni", SqlDbType.VarChar, dni),
                new("@idIdioma", SqlDbType.VarChar, idIdioma)
            };
            com.Ejecutar("UPDATE usuario SET idIdioma=@idIdioma WHERE dni=@dni", parametros);
        }
    }
}
