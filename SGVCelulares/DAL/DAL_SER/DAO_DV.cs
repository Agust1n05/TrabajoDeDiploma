using System.Data;

namespace DAL.DAL_SER
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

        // ---------------- Tablas de negocio ----------------
        // El nombre de la tabla y de sus columnas no puede ir como parámetro SQL.
        // Solo se reciben nombres de la lista fija de tablas de negocio definida en BLL_DV.

        /// <summary>Todas las filas de una tabla de negocio, con todas sus columnas en orden.</summary>
        public DataTable ConsultarFilasNegocio(string nombreTabla)
        {
            return com.Consultar($"SELECT * FROM [{nombreTabla}]");
        }

        /// <summary>Una fila de una tabla de negocio buscada por su clave (simple o compuesta).</summary>
        public DataTable ConsultarFilaNegocio(string nombreTabla, string[] columnasId, string[] valoresId)
        {
            var parametros = new List<ParametroBD>();
            var condiciones = new List<string>();
            for (int i = 0; i < columnasId.Length; i++)
            {
                condiciones.Add($"[{columnasId[i]}] = @id{i}");
                parametros.Add(new($"@id{i}", SqlDbType.VarChar, valoresId[i]));
            }
            return com.Consultar($"SELECT * FROM [{nombreTabla}] WHERE {string.Join(" AND ", condiciones)}", parametros);
        }

        /// <summary>DVH guardados de una tabla de negocio (sin orden: el orden lo resuelve la BLL).</summary>
        public DataTable ConsultarDVHNegocio(string nombreTabla)
        {
            var parametros = new List<ParametroBD> { new("@nombreTabla", SqlDbType.VarChar, nombreTabla) };
            return com.Consultar("SELECT IdRegistro, Valor FROM dvh WHERE NombreTabla=@nombreTabla", parametros);
        }
    }
}
