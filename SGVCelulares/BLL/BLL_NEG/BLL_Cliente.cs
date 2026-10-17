using BE;
using BLL.BLL_SER;
using MAP.MAP_NEG;
using Servicio;

namespace BLL.BLL_NEG
{
    /// <summary>
    /// ABM de clientes con baja lógica. Toda alta, modificación o baja recalcula el DV
    /// del registro y queda registrada en la bitácora.
    /// </summary>
    public class BLL_Cliente : IABMC<BE_Cliente>
    {
        private const string Modulo = "Clientes";

        MAP_Cliente map_cliente;
        BLL_Bitacora bll_bitacora;
        BLL_DV bll_dv;

        public BLL_Cliente()
        {
            map_cliente = new MAP_Cliente();
            bll_bitacora = new BLL_Bitacora();
            bll_dv = new BLL_DV();
        }

        /// <summary>Clientes activos, ordenados por apellido y nombre.</summary>
        public List<BE_Cliente> Consultar()
        {
            return map_cliente.Consultar()
                .Where(c => c.Activo)
                .OrderBy(c => c.Apellido).ThenBy(c => c.Nombre)
                .ToList();
        }

        /// <summary>Cliente por DNI, esté activo o dado de baja.</summary>
        public BE_Cliente ConsultarPorId(BE_Cliente cliente) => map_cliente.ConsultarPorDni(cliente.Dni)!;

        /// <summary>Cliente activo por DNI, o null si no existe o está dado de baja.</summary>
        public BE_Cliente? ConsultarPorDni(string dni)
        {
            BE_Cliente? cliente = map_cliente.ConsultarPorDni(dni);
            return cliente != null && cliente.Activo ? cliente : null;
        }

        public bool ExisteCliente(string dni) => ConsultarPorDni(dni) != null;

        /// <summary>True si el DNI pertenece a un cliente dado de baja (un alta con ese DNI lo reactiva).</summary>
        public bool EstaDadoDeBaja(string dni)
        {
            BE_Cliente? cliente = map_cliente.ConsultarPorDni(dni);
            return cliente != null && !cliente.Activo;
        }

        /// <summary>
        /// Alta de cliente. Si el DNI pertenece a un cliente dado de baja, lo reactiva con los datos nuevos.
        /// </summary>
        public void Agregar(BE_Cliente cliente)
        {
            Normalizar(cliente);
            Validar(cliente);

            BE_Cliente? existente = map_cliente.ConsultarPorDni(cliente.Dni);
            if (existente != null && existente.Activo)
                throw new Exception($"Ya existe un cliente con el DNI {cliente.Dni}.");

            if (existente != null)
            {
                map_cliente.Modificar(cliente);
                map_cliente.CambiarActivo(cliente.Dni, true);
                bll_dv.ActualizarDVNegocio(BLL_DV.TablaCliente, cliente.Dni);
                RegistrarBitacora("Reactivar cliente", 3);
            }
            else
            {
                map_cliente.Agregar(cliente);
                bll_dv.ActualizarDVNegocio(BLL_DV.TablaCliente, cliente.Dni);
                RegistrarBitacora("Crear cliente", 2);
            }
        }

        public void Modificar(BE_Cliente cliente)
        {
            Normalizar(cliente);
            Validar(cliente);
            ObtenerActivo(cliente.Dni);

            map_cliente.Modificar(cliente);
            bll_dv.ActualizarDVNegocio(BLL_DV.TablaCliente, cliente.Dni);
            RegistrarBitacora("Modificar cliente", 2);
        }

        /// <summary>Baja lógica. No se permite si el cliente tiene órdenes de trabajo sin entregar.</summary>
        public void Borrar(BE_Cliente cliente)
        {
            BE_Cliente existente = ObtenerActivo(cliente.Dni);

            int otAbiertas = map_cliente.ContarOT(existente.Dni, EstadosOT.Abiertas);
            if (otAbiertas > 0)
                throw new Exception($"No se puede eliminar el cliente: tiene {otAbiertas} orden(es) de trabajo sin entregar.");

            map_cliente.CambiarActivo(existente.Dni, false);
            bll_dv.ActualizarDVNegocio(BLL_DV.TablaCliente, existente.Dni);
            RegistrarBitacora("Eliminar cliente", 3);
        }

        private BE_Cliente ObtenerActivo(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new Exception("El DNI es obligatorio.");

            return ConsultarPorDni(dni.Trim())
                ?? throw new Exception($"No existe un cliente activo con el DNI {dni}.");
        }

        private void Normalizar(BE_Cliente cliente)
        {
            if (cliente == null) throw new Exception("El cliente no puede ser nulo.");
            cliente.Dni = cliente.Dni?.Trim() ?? "";
            cliente.Nombre = cliente.Nombre?.Trim() ?? "";
            cliente.Apellido = cliente.Apellido?.Trim() ?? "";
            cliente.Telefono = cliente.Telefono?.Trim() ?? "";
        }

        private void Validar(BE_Cliente cliente)
        {
            // DNI
            if (string.IsNullOrWhiteSpace(cliente.Dni))
                throw new Exception("El DNI es obligatorio.");

            if (!cliente.Dni.All(char.IsDigit))
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

            // Teléfono
            if (string.IsNullOrWhiteSpace(cliente.Telefono))
                throw new Exception("El teléfono es obligatorio.");

            if (!cliente.Telefono.All(char.IsDigit) || cliente.Telefono.Length < 8 || cliente.Telefono.Length > 15)
                throw new Exception("El teléfono debe tener entre 8 y 15 dígitos, sin espacios ni guiones.");
        }

        private void RegistrarBitacora(string evento, int criticidad)
        {
            bll_bitacora.RegistrarBitacora(new SER_Bitacora(
                SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, Modulo, evento, criticidad));
        }
    }
}
