using BE;
using DAL.DAL_NEG;
using System.Data;

namespace MAP.MAP_NEG
{
    public class MAP_Reclamo
    {
        DAO_Reclamo dao_reclamo;
        public MAP_Reclamo() => dao_reclamo = new();

        public List<BE_ReclamoResumen> ConsultarResumen()
        {
            var lista = new List<BE_ReclamoResumen>();
            foreach (DataRow fila in dao_reclamo.ConsultarResumen().Rows)
                lista.Add(MapearResumen(fila));
            return lista;
        }

        public BE_ReclamoResumen? ConsultarResumenPorNro(int nroReclamo)
        {
            DataTable tabla = dao_reclamo.ConsultarResumenPorNro(nroReclamo);
            return tabla.Rows.Count > 0 ? MapearResumen(tabla.Rows[0]) : null;
        }

        public List<BE_ReparacionConGarantia> ConsultarReparacionesConGarantia(string dni, string estadoOT)
        {
            var lista = new List<BE_ReparacionConGarantia>();
            foreach (DataRow fila in dao_reclamo.ConsultarReparacionesConGarantia(dni, estadoOT).Rows)
                lista.Add(MapearReparacion(fila));
            return lista;
        }

        public BE_ReparacionConGarantia? ConsultarReparacionConGarantia(int nroOT, string estadoOT)
        {
            DataTable tabla = dao_reclamo.ConsultarReparacionConGarantia(nroOT, estadoOT);
            return tabla.Rows.Count > 0 ? MapearReparacion(tabla.Rows[0]) : null;
        }

        public int ObtenerReclamoDelEquipo(int idEquipo, IEnumerable<string> estados) =>
            dao_reclamo.ObtenerReclamoDelEquipo(idEquipo, estados);

        public int ContarPorCliente(string dni, IEnumerable<string> estados) =>
            dao_reclamo.ContarPorCliente(dni, estados);

        /// <summary>Registra el reclamo con su estado inicial y lo pasa al estado nuevo. Devuelve el NroReclamo generado.</summary>
        public int Registrar(BE_Reclamo reclamo, string estadoNuevo) =>
            dao_reclamo.Registrar(reclamo.NroOT, reclamo.Motivo, reclamo.Descripcion, reclamo.FechaReclamo,
                                  reclamo.Estado, estadoNuevo, reclamo.DniTecnico);

        private BE_ReclamoResumen MapearResumen(DataRow fila)
        {
            return new BE_ReclamoResumen
            {
                NroReclamo = Convert.ToInt32(fila["NroReclamo"]),
                NroOT = Convert.ToInt32(fila["NroOT"]),
                Motivo = Convert.ToString(fila["Motivo"]),
                Descripcion = Convert.ToString(fila["Descripcion"]),
                FechaReclamo = Convert.ToDateTime(fila["FechaReclamo"]),
                Estado = Convert.ToString(fila["Estado"]),
                FechaVencimiento = Convert.ToDateTime(fila["FechaVencimiento"]),
                DniCliente = Convert.ToString(fila["DniCliente"]),
                NombreCliente = Convert.ToString(fila["NombreCliente"]),
                ApellidoCliente = Convert.ToString(fila["ApellidoCliente"]),
                Marca = Convert.ToString(fila["marca"]),
                Modelo = Convert.ToString(fila["modelo"]),
                Imei = Convert.ToString(fila["imei"])?.Trim(),
                DniTecnico = fila["DniTecnico"] is DBNull ? null : Convert.ToString(fila["DniTecnico"]),
                NombreTecnico = fila["NombreTecnico"] is DBNull ? null : Convert.ToString(fila["NombreTecnico"]),
                ApellidoTecnico = fila["ApellidoTecnico"] is DBNull ? null : Convert.ToString(fila["ApellidoTecnico"])
            };
        }

        private BE_ReparacionConGarantia MapearReparacion(DataRow fila)
        {
            return new BE_ReparacionConGarantia
            {
                NroOT = Convert.ToInt32(fila["NroOT"]),
                DniCliente = Convert.ToString(fila["DniCliente"]),
                DescripcionFalla = Convert.ToString(fila["DescripcionFalla"]),
                FechaVencimiento = Convert.ToDateTime(fila["FechaVencimiento"]),
                IdEquipo = Convert.ToInt32(fila["idEquipo"]),
                Marca = Convert.ToString(fila["marca"]),
                Modelo = Convert.ToString(fila["modelo"]),
                Imei = Convert.ToString(fila["imei"])?.Trim()
            };
        }
    }
}
