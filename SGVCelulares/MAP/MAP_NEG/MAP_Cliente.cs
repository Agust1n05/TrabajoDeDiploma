using BE;
using DAL;
using Servicio;
using System.Data;

namespace MAP.MAP_SER
{
    public class MAP_Cliente
    {
        DAO_Cliente dao_cliente;
        public MAP_Cliente() => dao_cliente = new();

        public bool ExisteCliente(string dni) => dao_cliente.ExisteCliente(dni);

        public BE_Cliente ConsultarPorDni(string dni)
        {
            DataTable tabla = dao_cliente.ConsultarPorDni(dni);
            return tabla.Rows.Count > 0 ? Mapear(tabla.Rows[0]) : null;
        }

        private BE_Cliente Mapear(DataRow fila)
        {
            return new BE_Cliente(
                Convert.ToString(fila["dni"]),
                Convert.ToString(fila["Nombre"]),
                Convert.ToString(fila["Apellido"]),
                Convert.ToString(fila["Telefono"])
            );
        }
    }
}
