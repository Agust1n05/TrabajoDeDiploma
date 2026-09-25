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
    }
}
