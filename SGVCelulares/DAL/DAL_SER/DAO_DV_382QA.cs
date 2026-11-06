using System.Data;

namespace DAL.DAL_SER
{
    public class DAO_DV_382QA
    {
        ComandoBD_382QA com;
        public DAO_DV_382QA() => com = new();
        public void AgregarDVH_382QA(string nombreTabla, string idRegistro, string valor)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@idRegistro", SqlDbType.VarChar, idRegistro),
                new("@valor", SqlDbType.VarChar, valor)
            };
            com.Ejecutar_382QA("insert into dvh (nombreTabla,idRegistro,valor) values (@nombreTabla,@idRegistro,@valor)", parametros);
        }
        public void BorrarDVH_382QA(string nombreTabla, string idRegistro)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@idRegistro", SqlDbType.VarChar, idRegistro),
            };
            com.Ejecutar_382QA("DELETE FROM dvh WHERE NombreTabla=@nombreTabla AND IdRegistro=@idRegistro", parametros);
        }

        public void ModificarDVH_382QA(string nombreTabla, string idRegistro, string valor)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@idRegistro", SqlDbType.VarChar, idRegistro),
                new("@valor", SqlDbType.VarChar, valor)
            };
            com.Ejecutar_382QA("UPDATE dvh SET Valor=@Valor WHERE NombreTabla=@nombreTabla AND IdRegistro=@idRegistro", parametros);
        }

        public void AgregarDVV_382QA(string nombreTabla, string valor)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@valor", SqlDbType.VarChar, valor)
            };
            com.Ejecutar_382QA("INSERT INTO dvv (NombreTabla, Valor) VALUES (@nombreTabla, @valor)", parametros);
        }
        public void BorrarDVV_382QA(string nombreTabla)
        {
            var parametros = new List<ParametroBD_382QA> { new("@nombreTabla", SqlDbType.VarChar, nombreTabla) };
            com.Ejecutar_382QA("DELETE FROM dvv WHERE NombreTabla=@nombreTabla", parametros);
        }
        public void ModificarDVV_382QA(string nombreTabla, string valor)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@valor", SqlDbType.VarChar, valor)
            };
            com.Ejecutar_382QA("UPDATE dvv SET Valor=@valor WHERE NombreTabla=@nombreTabla", parametros);
        }

        public bool ExisteDVH_382QA(string nombreTabla, string idRegistro)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@idRegistro", SqlDbType.VarChar, idRegistro)
            };
            object? res = com.Escalar_382QA("SELECT COUNT(*) FROM dvh WHERE NombreTabla=@nombreTabla AND IdRegistro=@idRegistro", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public bool ExisteDVV_382QA(string nombreTabla)
        {
            var parametros = new List<ParametroBD_382QA> { new("@nombreTabla", SqlDbType.VarChar, nombreTabla) };
            object? res = com.Escalar_382QA("SELECT COUNT(*) FROM dvv WHERE NombreTabla=@nombreTabla", parametros);
            return Convert.ToInt32(res) > 0;
        }
        public DataTable ObtenerDVH_382QA(string nombreTabla, string idRegistro)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@nombreTabla", SqlDbType.VarChar, nombreTabla),
                new("@idRegistro", SqlDbType.VarChar, idRegistro)
            };
            return com.Consultar_382QA("SELECT NombreTabla, IdRegistro, Valor FROM dvh WHERE NombreTabla=@nombreTabla AND IdRegistro=@idRegistro", parametros);
        }
        public DataTable ObtenerDVV_382QA(string nombreTabla)
        {
            var parametros = new List<ParametroBD_382QA> { new("@nombreTabla", SqlDbType.VarChar, nombreTabla) };
            return com.Consultar_382QA("SELECT NombreTabla, Valor FROM dvv WHERE NombreTabla = @nombreTabla", parametros);
        }
        public DataTable ConsultarDVH_382QA(string nombreTabla)
        {
            DataTable tabla = null;
            var parametros = new List<ParametroBD_382QA> { new("@nombreTabla", SqlDbType.VarChar, nombreTabla) };
            if (nombreTabla == "rol" || nombreTabla == "familia" || nombreTabla == "permiso")
            {
                tabla = com.Consultar_382QA("SELECT * FROM dvh WHERE NombreTabla=@nombreTabla ORDER BY CAST(IdRegistro AS INT)", parametros);
            }
            else if (nombreTabla == "usuario")
            {
                tabla = com.Consultar_382QA("SELECT * FROM dvh WHERE NombreTabla=@nombreTabla ORDER BY IdRegistro", parametros);
            }
            else // tablas intermedias: rol_familia, rol_permiso, familia_familia, permiso_familia, etc.
            {
                tabla = com.Consultar_382QA("SELECT * FROM dvh WHERE NombreTabla=@nombreTabla ORDER BY CAST(SUBSTRING(IdRegistro, 1, CHARINDEX('-', IdRegistro) - 1) AS INT), CAST(SUBSTRING(IdRegistro, CHARINDEX('-', IdRegistro) + 1, LEN(IdRegistro)) AS INT)", parametros);
            }
            return tabla;
        }

        // ---------------- Tablas de negocio ----------------
        // El nombre de la tabla y de sus columnas no puede ir como parámetro SQL.
        // Solo se reciben nombres de la lista fija de tablas de negocio definida en BLL_DV_382QA.

        /// <summary>Todas las filas de una tabla de negocio, con todas sus columnas en orden.</summary>
        public DataTable ConsultarFilasNegocio_382QA(string nombreTabla)
        {
            return com.Consultar_382QA($"SELECT * FROM [{nombreTabla}]");
        }

        /// <summary>Una fila de una tabla de negocio buscada por su clave (simple o compuesta).</summary>
        public DataTable ConsultarFilaNegocio_382QA(string nombreTabla, string[] columnasId, string[] valoresId)
        {
            var parametros = new List<ParametroBD_382QA>();
            var condiciones = new List<string>();
            for (int i = 0; i < columnasId.Length; i++)
            {
                condiciones.Add($"[{columnasId[i]}] = @id{i}");
                parametros.Add(new($"@id{i}", SqlDbType.VarChar, valoresId[i]));
            }
            return com.Consultar_382QA($"SELECT * FROM [{nombreTabla}] WHERE {string.Join(" AND ", condiciones)}", parametros);
        }

        /// <summary>DVH guardados de una tabla de negocio (sin orden: el orden lo resuelve la BLL).</summary>
        public DataTable ConsultarDVHNegocio_382QA(string nombreTabla)
        {
            var parametros = new List<ParametroBD_382QA> { new("@nombreTabla", SqlDbType.VarChar, nombreTabla) };
            return com.Consultar_382QA("SELECT IdRegistro, Valor FROM dvh WHERE NombreTabla=@nombreTabla", parametros);
        }
    }
}
