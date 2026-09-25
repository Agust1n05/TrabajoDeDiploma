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
        public List<BE_Cliente> Consultar()
        {
            DataTable tabla = dao_cliente.Consultar(""); // Consulta todos los clientes
            List<BE_Cliente> clientes = new();
            foreach (DataRow fila in tabla.Rows)
            {
                clientes.Add(Mapear(fila));
            }
            return clientes;
        }

        public BE_Cliente ConsultarPorDni(string dni)
        {
            DataTable tabla = dao_cliente.Consultar(dni);
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
        public void Agregar(BE_Cliente cliente)
        {
          
            dao_cliente.Agregar(cliente.Dni, cliente.Nombre, cliente.Apellido, cliente.Telefono);
        }
        public void Borrar(string dni)
        {
           
            dao_cliente.Borrar(dni);
        }   
        public void Modificar(BE_Cliente cliente)
        {
            dao_cliente.Modificar(cliente.Dni, cliente.Nombre, cliente.Apellido, cliente.Telefono);
        }
       

    }
}
