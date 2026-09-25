using MAP.MAP_SER;
using BE;

namespace BLL.BLL_NEG
{
    public class BLL_Cliente
    {
        MAP_Cliente map_cliente;
        public BLL_Cliente() => map_cliente = new();

        public bool ExisteCliente(string dni) => map_cliente.ExisteCliente(dni);
        public BE_Cliente ConsultarPorDni(string dni) => map_cliente.ConsultarPorDni(dni);

        public List<BE_Cliente> Consultar()
        {
            return map_cliente.Consultar();
        }
        public void Agregar(BE_Cliente cliente)
        {
            Validar(cliente);

            if (map_cliente.ExisteCliente(cliente.Dni))
                throw new Exception($"Ya existe un cliente con el DNI {cliente.Dni}.");

            map_cliente.Agregar(cliente);
        }
        public void Borrar(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new Exception("El DNI es obligatorio para eliminar un cliente.");

            if (!map_cliente.ExisteCliente(dni))
                throw new Exception($"No existe un cliente con el DNI {dni}.");

            map_cliente.Borrar(dni);
        }
        public void Modificar(BE_Cliente cliente)
        {
            Validar(cliente);

            if (!map_cliente.ExisteCliente(cliente.Dni))
                throw new Exception($"No existe un cliente con el DNI {cliente.Dni}.");

            map_cliente.Modificar(cliente);
        }



        private void Validar(BE_Cliente cliente)
        {
            if (cliente == null)
                throw new Exception("El cliente no puede ser nulo.");

            // DNI
            if (string.IsNullOrWhiteSpace(cliente.Dni))
                throw new Exception("El DNI es obligatorio.");

            if (!int.TryParse(cliente.Dni, out _))
                throw new Exception("El DNI debe contener únicamente números.");

            if (cliente.Dni.Length < 7 || cliente.Dni.Length > 8)
                throw new Exception("El DNI debe tener entre 7 y 8 dígitos.");

            // Nombre
            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                throw new Exception("El nombre es obligatorio.");

            if (cliente.Nombre.Length > 50)
                throw new Exception("El nombre no puede superar los 50 caracteres.");

            if (!cliente.Nombre.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                throw new Exception("El nombre solo puede contener letras.");

            // Apellido
            if (string.IsNullOrWhiteSpace(cliente.Apellido))
                throw new Exception("El apellido es obligatorio.");

            if (cliente.Apellido.Length > 50)
                throw new Exception("El apellido no puede superar los 50 caracteres.");

            if (!cliente.Apellido.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                throw new Exception("El apellido solo puede contener letras.");
        }
    }
}
