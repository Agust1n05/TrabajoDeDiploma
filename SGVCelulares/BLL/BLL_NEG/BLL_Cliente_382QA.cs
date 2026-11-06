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
    public class BLL_Cliente_382QA : IABMC_382QA<BE_Cliente_382QA>
    {
        private const string Modulo = "Clientes";

        MAP_Cliente_382QA map_cliente;
        MAP_Reclamo_382QA map_reclamo;
        BLL_Bitacora_382QA bll_bitacora;
        BLL_DV_382QA bll_dv;

        public BLL_Cliente_382QA()
        {
            map_cliente = new MAP_Cliente_382QA();
            map_reclamo = new MAP_Reclamo_382QA();
            bll_bitacora = new BLL_Bitacora_382QA();
            bll_dv = new BLL_DV_382QA();
        }

        /// <summary>Clientes activos, ordenados por apellido y nombre.</summary>
        public List<BE_Cliente_382QA> Consultar_382QA()
        {
            return map_cliente.Consultar_382QA()
                .Where(c => c.Activo)
                .OrderBy(c => c.Apellido).ThenBy(c => c.Nombre)
                .ToList();
        }

        /// <summary>Cliente por DNI, esté activo o dado de baja.</summary>
        public BE_Cliente_382QA ConsultarPorId_382QA(BE_Cliente_382QA cliente) => map_cliente.ConsultarPorDni_382QA(cliente.Dni)!;

        /// <summary>Cliente activo por DNI, o null si no existe o está dado de baja.</summary>
        public BE_Cliente_382QA? ConsultarPorDni_382QA(string dni)
        {
            BE_Cliente_382QA? cliente = map_cliente.ConsultarPorDni_382QA(dni);
            return cliente != null && cliente.Activo ? cliente : null;
        }

        public bool ExisteCliente_382QA(string dni) => ConsultarPorDni_382QA(dni) != null;

        /// <summary>True si el DNI pertenece a un cliente dado de baja (un alta con ese DNI lo reactiva).</summary>
        public bool EstaDadoDeBaja_382QA(string dni)
        {
            BE_Cliente_382QA? cliente = map_cliente.ConsultarPorDni_382QA(dni);
            return cliente != null && !cliente.Activo;
        }

        /// <summary>
        /// Alta de cliente. Si el DNI pertenece a un cliente dado de baja, lo reactiva con los datos nuevos.
        /// </summary>
        public void Agregar_382QA(BE_Cliente_382QA cliente)
        {
            Normalizar_382QA(cliente);
            Validar_382QA(cliente);

            BE_Cliente_382QA? existente = map_cliente.ConsultarPorDni_382QA(cliente.Dni);
            if (existente != null && existente.Activo)
                throw new Exception($"Ya existe un cliente con el DNI {cliente.Dni}.");

            if (existente != null)
            {
                map_cliente.Modificar_382QA(cliente);
                map_cliente.CambiarActivo_382QA(cliente.Dni, true);
                bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaCliente, cliente.Dni);
                RegistrarBitacora_382QA("Reactivar cliente", 1);
            }
            else
            {
                map_cliente.Agregar_382QA(cliente);
                bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaCliente, cliente.Dni);
                RegistrarBitacora_382QA("Crear cliente", 2);
            }
        }

        public void Modificar_382QA(BE_Cliente_382QA cliente)
        {
            Normalizar_382QA(cliente);
            Validar_382QA(cliente);
            ObtenerActivo_382QA(cliente.Dni);

            map_cliente.Modificar_382QA(cliente);
            bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaCliente, cliente.Dni);
            RegistrarBitacora_382QA("Modificar cliente", 2);
        }

        /// <summary>Baja lógica. No se permite si el cliente tiene órdenes de trabajo sin entregar o reclamos abiertos.</summary>
        public void Borrar_382QA(BE_Cliente_382QA cliente)
        {
            BE_Cliente_382QA existente = ObtenerActivo_382QA(cliente.Dni);

            int otAbiertas = map_cliente.ContarOT_382QA(existente.Dni, EstadosOT_382QA.Abiertas);
            if (otAbiertas > 0)
                throw new Exception($"No se puede eliminar el cliente: tiene {otAbiertas} orden(es) de trabajo sin entregar.");

            int reclamosAbiertos = map_reclamo.ContarPorCliente_382QA(existente.Dni, EstadosReclamo_382QA.Abiertos);
            if (reclamosAbiertos > 0)
                throw new Exception($"No se puede eliminar el cliente: tiene {reclamosAbiertos} reclamo(s) por garantía abierto(s).");

            map_cliente.CambiarActivo_382QA(existente.Dni, false);
            bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaCliente, existente.Dni);
            RegistrarBitacora_382QA("Eliminar cliente", 1);
        }

        private BE_Cliente_382QA ObtenerActivo_382QA(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new Exception("El DNI es obligatorio.");

            return ConsultarPorDni_382QA(dni.Trim())
                ?? throw new Exception($"No existe un cliente activo con el DNI {dni}.");
        }

        private void Normalizar_382QA(BE_Cliente_382QA cliente)
        {
            if (cliente == null) throw new Exception("El cliente no puede ser nulo.");
            cliente.Dni = cliente.Dni?.Trim() ?? "";
            cliente.Nombre = cliente.Nombre?.Trim() ?? "";
            cliente.Apellido = cliente.Apellido?.Trim() ?? "";
            cliente.Telefono = cliente.Telefono?.Trim() ?? "";
        }

        private void Validar_382QA(BE_Cliente_382QA cliente)
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

        private void RegistrarBitacora_382QA(string evento, int criticidad)
        {
            bll_bitacora.RegistrarBitacora_382QA(new SER_Bitacora_382QA(
                SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, Modulo, evento, criticidad));
        }
    }
}
