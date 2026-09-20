using System.Data;

namespace DAL
{
    public class DAO_DV
    {
        ComandoBD com;
        public DAO_DV() => com = new();
        public void AgregarDVH(string nombreTabla, string idRegistro, string valor)
        {
            var parametros = new List<ParametroBD>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@idRegistro", SqlDbType.VarChar, idRegistro),
                new("@valor", SqlDbType.VarChar, valor)
            };
            com.Ejecutar("insert into dvh (nombreTabla,idRegistro,valor) values (@nombreTabla,@idRegistro,@valor)", parametros);
        }
        public void BorrarDVH(string nombreTabla, string idRegistro)
        {
            var parametros = new List<ParametroBD>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@idRegistro", SqlDbType.VarChar, idRegistro),
            };
            com.Ejecutar("DELETE FROM dvh WHERE NombreTabla=@nombreTabla AND IdRegistro=@idRegistro", parametros);
        }

        public void ModificarDVH(string nombreTabla, string idRegistro, string valor)
        {
            var parametros = new List<ParametroBD>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@idRegistro", SqlDbType.VarChar, idRegistro),
                new("@valor", SqlDbType.VarChar, valor)
            };
            com.Ejecutar("UPDATE dvh SET Valor=@Valor WHERE NombreTabla=@nombreTabla AND IdRegistro=@idRegistro", parametros);
        }

        public void AgregarDVV(string nombreTabla, string valor)
        {
            var parametros = new List<ParametroBD>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@valor", SqlDbType.VarChar, valor)
            };
            com.Ejecutar("INSERT INTO dvv (NombreTabla, Valor) VALUES (@nombreTabla, @valor)", parametros);
        }
        public void BorrarDVV(string nombreTabla)
        {
            var parametros = new List<ParametroBD> { new("@nombreTabla", SqlDbType.VarChar, nombreTabla) };
            com.Ejecutar("DELETE FROM dvv WHERE NombreTabla=@nombreTabla", parametros);
        }
        public void ModificarDVV(string nombreTabla, string valor)
        {
            var parametros = new List<ParametroBD>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@valor", SqlDbType.VarChar, valor)
            };
            com.Ejecutar("UPDATE dvv SET Valor=@valor WHERE NombreTabla=@nombreTabla", parametros);
        }

        public bool ExisteDVH(string nombreTabla, string idRegistro)
        {
            var parametros = new List<ParametroBD>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@idRegistro", SqlDbType.VarChar, idRegistro)
            };
            object? res = com.Escalar("SELECT COUNT(*) FROM dvh WHERE NombreTabla=@nombreTabla AND IdRegistro=@idRegistro", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public bool ExisteDVV(string nombreTabla)
        {
            var parametros = new List<ParametroBD> { new("@nombreTabla", SqlDbType.VarChar, nombreTabla) };
            object? res = com.Escalar("SELECT COUNT(*) FROM dvv WHERE NombreTabla=@nombreTabla", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public DataTable ObtenerDVH(string nombreTabla, string idRegistro)
        {
            var parametros = new List<ParametroBD>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@idRegistro", SqlDbType.VarChar, idRegistro)
            };
            return com.Consultar("SELECT NombreTabla, IdRegistro, Valor FROM dvh WHERE NombreTabla=@nombreTabla AND IdRegistro=@idRegistro", parametros);
        }
        public DataTable ObtenerDVV(string nombreTabla)
        {
            var parametros = new List<ParametroBD> { new("@nombreTabla", SqlDbType.VarChar, nombreTabla) };
            return com.Consultar("SELECT NombreTabla, Valor FROM dvv WHERE NombreTabla = @nombreTabla", parametros);
        }
        public DataTable ConsultarDVH(string nombreTabla)
        {
            DataTable tabla = null;
            var parametros = new List<ParametroBD> { new("@nombreTabla", SqlDbType.VarChar, nombreTabla) };
            if (nombreTabla == "rol" || nombreTabla == "familia")
            {
                tabla = com.Consultar("SELECT * FROM dvh WHERE NombreTabla=@nombreTabla ORDER BY CAST(IdRegistro AS INT)", parametros);
            }
            else if (nombreTabla == "usuario")
            {
                tabla = com.Consultar("SELECT * FROM dvh WHERE NombreTabla=@nombreTabla ORDER BY IdRegistro", parametros);
            }
            else // tablas intermedias: rol_familia, rol_permiso, familia_familia, permiso_familia, etc.
            {
                tabla = com.Consultar("SELECT * FROM dvh WHERE NombreTabla=@nombreTabla ORDER BY CAST(SUBSTRING(IdRegistro, 1, CHARINDEX('-', IdRegistro) - 1) AS INT), CAST(SUBSTRING(IdRegistro, CHARINDEX('-', IdRegistro) + 1, LEN(IdRegistro)) AS INT)", parametros);
            }
            return tabla;
        }
    }
}
