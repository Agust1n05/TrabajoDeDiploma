using System.Data;

namespace DAL
{
    public class DAO_Tecnico
    {
        ComandoBD com;
        public DAO_Tecnico() => com = new();

        // Devuelve el técnico disponible con menor cantidad de OT asignadas (incluye los que tienen 0)
        public DataTable ObtenerConMenosOTs()
        {
            return com.Consultar(@"
                SELECT TOP 1 t.dni, t.Nombre, t.Apellido, t.Disponible
                FROM tecnico t
                LEFT JOIN OrdenDeTrabajo ot ON ot.DniTecnico = t.dni
                WHERE t.Disponible = 1
                GROUP BY t.dni, t.Nombre, t.Apellido, t.Disponible
                ORDER BY COUNT(ot.NroOT) ASC");
        }
    }
}
