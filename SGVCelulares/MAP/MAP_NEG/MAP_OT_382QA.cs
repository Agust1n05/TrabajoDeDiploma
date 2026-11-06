using BE;
using DAL.DAL_NEG;
using System.Data;

namespace MAP.MAP_NEG
{
    public class MAP_OT_382QA
    {
        DAO_OT_382QA dao_ot;
        public MAP_OT_382QA() => dao_ot = new();

        public List<BE_OTResumen_382QA> ConsultarResumen_382QA()
        {
            var lista = new List<BE_OTResumen_382QA>();
            foreach (DataRow fila in dao_ot.ConsultarResumen_382QA().Rows)
                lista.Add(MapearResumen_382QA(fila));
            return lista;
        }

        public BE_OTResumen_382QA? ConsultarResumenPorNro_382QA(int nroOT)
        {
            DataTable tabla = dao_ot.ConsultarResumenPorNro_382QA(nroOT);
            return tabla.Rows.Count > 0 ? MapearResumen_382QA(tabla.Rows[0]) : null;
        }

        /// <summary>Inserta la OT y devuelve el NroOT generado.</summary>
        public int Agregar_382QA(BE_OT_382QA ot) =>
            dao_ot.Agregar_382QA(ot.Estado, ot.Dni, ot.IdEquipo, ot.DniTecnico, ot.DescripcionFalla);

        public bool CambiarEstado_382QA(int nroOT, string estadoEsperado, string estadoNuevo) =>
            dao_ot.CambiarEstado_382QA(nroOT, estadoEsperado, estadoNuevo);

        public int ObtenerOTDelEquipo_382QA(int idEquipo, IEnumerable<string> estados) =>
            dao_ot.ObtenerOTDelEquipo_382QA(idEquipo, estados);

        private BE_OTResumen_382QA MapearResumen_382QA(DataRow fila)
        {
            return new BE_OTResumen_382QA
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
