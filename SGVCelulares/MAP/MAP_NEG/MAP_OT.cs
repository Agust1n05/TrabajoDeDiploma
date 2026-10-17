using BE;
using DAL.DAL_NEG;
using System.Data;

namespace MAP.MAP_NEG
{
    public class MAP_OT
    {
        DAO_OT dao_ot;
        public MAP_OT() => dao_ot = new();

        public List<BE_OTResumen> ConsultarResumen()
        {
            var lista = new List<BE_OTResumen>();
            foreach (DataRow fila in dao_ot.ConsultarResumen().Rows)
                lista.Add(MapearResumen(fila));
            return lista;
        }

        public BE_OTResumen? ConsultarResumenPorNro(int nroOT)
        {
            DataTable tabla = dao_ot.ConsultarResumenPorNro(nroOT);
            return tabla.Rows.Count > 0 ? MapearResumen(tabla.Rows[0]) : null;
        }

        /// <summary>Inserta la OT y devuelve el NroOT generado.</summary>
        public int Agregar(BE_OT ot) =>
            dao_ot.Agregar(ot.Estado, ot.Dni, ot.IdEquipo, ot.DniTecnico, ot.DescripcionFalla);

        public int ObtenerOTDelEquipo(int idEquipo, IEnumerable<string> estados) =>
            dao_ot.ObtenerOTDelEquipo(idEquipo, estados);

        private BE_OTResumen MapearResumen(DataRow fila)
        {
            return new BE_OTResumen
            {
                NroOT = Convert.ToInt32(fila["NroOT"]),
                Estado = Convert.ToString(fila["Estado"]),
                DescripcionFalla = Convert.ToString(fila["DescripcionFalla"]),
                DniCliente = Convert.ToString(fila["DniCliente"]),
                NombreCliente = Convert.ToString(fila["NombreCliente"]),
                ApellidoCliente = Convert.ToString(fila["ApellidoCliente"]),
                TelefonoCliente = Convert.ToString(fila["TelefonoCliente"]),
                IdEquipo = Convert.ToInt32(fila["idEquipo"]),
                Marca = Convert.ToString(fila["marca"]),
                Modelo = Convert.ToString(fila["modelo"]),
                Imei = Convert.ToString(fila["imei"])?.Trim(),
                DniTecnico = Convert.ToString(fila["DniTecnico"]),
                NombreTecnico = Convert.ToString(fila["NombreTecnico"]),
                ApellidoTecnico = Convert.ToString(fila["ApellidoTecnico"]),
                TotalPresupuesto = fila["TotalPresupuesto"] is DBNull ? null : Convert.ToDecimal(fila["TotalPresupuesto"]),
                EstadoPresupuesto = fila["EstadoPresupuesto"] is DBNull ? null : Convert.ToString(fila["EstadoPresupuesto"])
            };
        }
    }
}
