using DAL.DAL_SER;
using System.Data;

namespace DAL.DAL_NEG
{
    public class DAO_Repuesto_382QA
    {
        ComandoBD_382QA com;
        public DAO_Repuesto_382QA() => com = new();

        /// <summary>Repuestos activos e inactivos. Con código vacío trae todos.</summary>
        public DataTable Consultar_382QA(string codigo)
        {
            var parametros = new List<ParametroBD_382QA> { new("@Codigo", SqlDbType.VarChar, codigo) };
            return com.Consultar_382QA("SELECT Codigo, Descripcion, PrecioUnitario, stockDisponible, Activo FROM Repuesto WHERE (@Codigo = '' OR Codigo = @Codigo)", parametros);
        }

        public void Agregar_382QA(string codigo, string descripcion, decimal precioUnitario, int stockDisponible)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@Codigo", SqlDbType.VarChar, codigo),
                new("@Descripcion", SqlDbType.VarChar, descripcion),
                new("@PrecioUnitario", SqlDbType.Decimal, precioUnitario),
                new("@stockDisponible", SqlDbType.Int, stockDisponible)
            };
            com.Ejecutar_382QA("INSERT INTO Repuesto (Codigo, Descripcion, PrecioUnitario, stockDisponible, Activo) VALUES (@Codigo, @Descripcion, @PrecioUnitario, @stockDisponible, 1)", parametros);
        }

        /// <summary>
        /// Solo modifica si el stock en la base sigue siendo stockOriginal (el que se cargó en el formulario),
        /// así no se pisa un descuento hecho por una reparación mientras se editaba. Devuelve las filas modificadas.
        /// </summary>
        public int Modificar_382QA(string codigo, string descripcion, decimal precioUnitario, int stockDisponible, int stockOriginal)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@Codigo", SqlDbType.VarChar, codigo),
                new("@Descripcion", SqlDbType.VarChar, descripcion),
                new("@PrecioUnitario", SqlDbType.Decimal, precioUnitario),
                new("@stockDisponible", SqlDbType.Int, stockDisponible),
                new("@stockOriginal", SqlDbType.Int, stockOriginal)
            };
            return com.Ejecutar_382QA("UPDATE Repuesto SET Descripcion = @Descripcion, PrecioUnitario = @PrecioUnitario, stockDisponible = @stockDisponible WHERE Codigo = @Codigo AND stockDisponible = @stockOriginal", parametros);
        }

        /// <summary>Baja lógica (activo = false) o reactivación (activo = true).</summary>
        public void CambiarActivo_382QA(string codigo, bool activo)
        {
            var parametros = new List<ParametroBD_382QA>
            {
                new("@Codigo", SqlDbType.VarChar, codigo),
                new("@Activo", SqlDbType.Bit, activo)
            };
            com.Ejecutar_382QA("UPDATE Repuesto SET Activo = @Activo WHERE Codigo = @Codigo", parametros);
        }

        /// <summary>
        /// Cantidad de OT en los estados indicados cuyo diagnóstico pide este repuesto
        /// (ej. OT presupuestadas o en reparación que todavía lo van a usar).
        /// </summary>
        public int ContarOT_382QA(string codigo, IEnumerable<string> estados)
        {
            var parametros = new List<ParametroBD_382QA> { new("@Codigo", SqlDbType.VarChar, codigo) };
            string filtroEstados = FiltroEstados_382QA.Armar_382QA(estados, parametros);
            object? res = com.Escalar_382QA($@"
                SELECT COUNT(DISTINCT ot.NroOT)
                FROM DetalleRepuesto dr
                INNER JOIN Diagnostico d    ON d.IdDiagnostico = dr.IdDiagnostico
                INNER JOIN OrdenDeTrabajo ot ON ot.NroOT = d.NroOT
                WHERE dr.Codigo = @Codigo AND ot.Estado IN ({filtroEstados})", parametros);
            return Convert.ToInt32(res);
        }
    }
}
