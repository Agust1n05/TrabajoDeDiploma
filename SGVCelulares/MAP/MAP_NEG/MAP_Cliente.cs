using BE;
using DAL.DAL_NEG;
using System.Data;

namespace MAP.MAP_NEG
{
    public class MAP_Cliente
    {
        DAO_Cliente dao_cliente;
        public MAP_Cliente() => dao_cliente = new();

        /// <summary>Todos los clientes, activos e inactivos.</summary>
        public List<BE_Cliente> Consultar()
        {
            var clientes = new List<BE_Cliente>();
            foreach (DataRow fila in dao_cliente.Consultar("").Rows)
                clientes.Add(Mapear(fila));
            return clientes;
        }

        /// <summary>Cliente por DNI (activo o inactivo), o null si no existe.</summary>
        public BE_Cliente? ConsultarPorDni(string dni)
        {
            DataTable tabla = dao_cliente.Consultar(dni);
            return tabla.Rows.Count > 0 ? Mapear(tabla.Rows[0]) : null;
        }

        public void Agregar(BE_Cliente cliente) =>
            dao_cliente.Agregar(cliente.Dni, cliente.Nombre, cliente.Apellido, cliente.Telefono);

        public void Modificar(BE_Cliente cliente) =>
            dao_cliente.Modificar(cliente.Dni, cliente.Nombre, cliente.Apellido, cliente.Telefono);

        public void CambiarActivo(string dni, bool activo) => dao_cliente.CambiarActivo(dni, activo);

        public int ContarOT(string dni, IEnumerable<string> estados) => dao_cliente.ContarOT(dni, estados);

        private BE_Cliente Mapear(DataRow fila)
        {
            return new BE_Cliente(
                Convert.ToString(fila["dni"]),
                Convert.ToString(fila["Nombre"]),
                Convert.ToString(fila["Apellido"]),
                Convert.ToString(fila["Telefono"]),
                Convert.ToBoolean(fila["Activo"])
            );
        }
    }
}
