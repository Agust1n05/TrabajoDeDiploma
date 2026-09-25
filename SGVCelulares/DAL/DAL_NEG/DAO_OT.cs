using DAL.DAL_SER;
using System.Data;

namespace DAL
{
    public class DAO_OT
    {
        ComandoBD com;
        public DAO_OT() => com = new();

        public DataTable Consultar() => com.Consultar("SELECT NroOT, Estado, dni, idEquipo, DniTecnico FROM OrdenDeTrabajo");

        // Inserta la OT y devuelve el NroOT correlativo generado por la base (identity)
        public int Agregar(string estado, string dni, string idEquipo, string dniTecnico)
        {
            var parametros = new List<ParametroBD>
            {
                new("@estado", SqlDbType.VarChar, estado),
                new("@dni", SqlDbType.VarChar, dni),
                new("@idEquipo", SqlDbType.VarChar, idEquipo),
                new("@DniTecnico", SqlDbType.VarChar, dniTecnico)
            };
            object? res = com.Escalar(
                "INSERT INTO OrdenDeTrabajo(Estado,dni,idEquipo,DniTecnico) OUTPUT INSERTED.NroOT VALUES (@estado,@dni,@idEquipo,@DniTecnico)",
                parametros);
            return Convert.ToInt32(res);
        }
    }
}