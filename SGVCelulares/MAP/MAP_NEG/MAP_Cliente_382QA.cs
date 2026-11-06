using BE;
using DAL.DAL_NEG;
using System.Data;

namespace MAP.MAP_NEG
{
    public class MAP_Cliente_382QA
    {
        DAO_Cliente_382QA dao_cliente;
        public MAP_Cliente_382QA() => dao_cliente = new();

        /// <summary>Todos los clientes, activos e inactivos.</summary>
        public List<BE_Cliente_382QA> Consultar_382QA()
        {
            var clientes = new List<BE_Cliente_382QA>();
            foreach (DataRow fila in dao_cliente.Consultar_382QA("").Rows)
                clientes.Add(Mapear_382QA(fila));
            return clientes;
        }

        /// <summary>Cliente por DNI (activo o inactivo), o null si no existe.</summary>
        public BE_Cliente_382QA? ConsultarPorDni_382QA(string dni)
        {
            DataTable tabla = dao_cliente.Consultar_382QA(dni);
            return tabla.Rows.Count > 0 ? Mapear_382QA(tabla.Rows[0]) : null;
        }

        public void Agregar_382QA(BE_Cliente_382QA cliente) =>
            dao_cliente.Agregar_382QA(cliente.Dni, cliente.Nombre, cliente.Apellido, cliente.Telefono);

        public void Modificar_382QA(BE_Cliente_382QA cliente) =>
            dao_cliente.Modificar_382QA(cliente.Dni, cliente.Nombre, cliente.Apellido, cliente.Telefono);

        public void CambiarActivo_382QA(string dni, bool activo) => dao_cliente.CambiarActivo_382QA(dni, activo);

        public int ContarOT_382QA(string dni, IEnumerable<string> estados) => dao_cliente.ContarOT_382QA(dni, estados);

        private BE_Cliente_382QA Mapear_382QA(DataRow fila)
        {
            return new BE_Cliente_382QA(
                Convert.ToString(fila["dni"]),
                Convert.ToString(fila["Nombre"]),
                Convert.ToString(fila["Apellido"]),
                Convert.ToString(fila["Telefono"]),
                Convert.ToBoolean(fila["Activo"])
            );
        }
    }
}
