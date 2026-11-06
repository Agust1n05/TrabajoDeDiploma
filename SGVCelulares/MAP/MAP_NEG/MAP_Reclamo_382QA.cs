using BE;
using DAL.DAL_NEG;
using System.Data;

namespace MAP.MAP_NEG
{
    public class MAP_Reclamo_382QA
    {
        DAO_Reclamo_382QA dao_reclamo;
        public MAP_Reclamo_382QA() => dao_reclamo = new();

        public List<BE_ReclamoResumen_382QA> ConsultarResumen_382QA()
        {
            var lista = new List<BE_ReclamoResumen_382QA>();
            foreach (DataRow fila in dao_reclamo.ConsultarResumen_382QA().Rows)
                lista.Add(MapearResumen_382QA(fila));
            return lista;
        }

        public BE_ReclamoResumen_382QA? ConsultarResumenPorNro_382QA(int nroReclamo)
        {
            DataTable tabla = dao_reclamo.ConsultarResumenPorNro_382QA(nroReclamo);
            return tabla.Rows.Count > 0 ? MapearResumen_382QA(tabla.Rows[0]) : null;
        }

        public List<BE_ReparacionConGarantia_382QA> ConsultarReparacionesConGarantia_382QA(string dni, string estadoOT)
        {
            var lista = new List<BE_ReparacionConGarantia_382QA>();
            foreach (DataRow fila in dao_reclamo.ConsultarReparacionesConGarantia_382QA(dni, estadoOT).Rows)
                lista.Add(MapearReparacion_382QA(fila));
            return lista;
        }

        public BE_ReparacionConGarantia_382QA? ConsultarReparacionConGarantia_382QA(int nroOT, string estadoOT)
        {
            DataTable tabla = dao_reclamo.ConsultarReparacionConGarantia_382QA(nroOT, estadoOT);
            return tabla.Rows.Count > 0 ? MapearReparacion_382QA(tabla.Rows[0]) : null;
        }

        public int ObtenerReclamoDelEquipo_382QA(int idEquipo, IEnumerable<string> estados) =>
            dao_reclamo.ObtenerReclamoDelEquipo_382QA(idEquipo, estados);

        public int ContarPorCliente_382QA(string dni, IEnumerable<string> estados) =>
            dao_reclamo.ContarPorCliente_382QA(dni, estados);

        public int ContarPorTecnico_382QA(string dni, IEnumerable<string> estados) =>
            dao_reclamo.ContarPorTecnico_382QA(dni, estados);

        public void RegistrarReparacion_382QA(int nroReclamo, Dictionary<string, int> repuestos, string estadoEsperado, string estadoNuevo) =>
            dao_reclamo.RegistrarReparacion_382QA(nroReclamo, repuestos.Select(r => (r.Key, r.Value)).ToList(), estadoEsperado, estadoNuevo);

        public bool CambiarEstado_382QA(int nroReclamo, string estadoEsperado, string estadoNuevo) =>
            dao_reclamo.CambiarEstado_382QA(nroReclamo, estadoEsperado, estadoNuevo);

        public string? ConsultarDetalleDiagnostico_382QA(int nroOT) => dao_reclamo.ConsultarDetalleDiagnostico_382QA(nroOT);

        public bool RegistrarEvaluacion_382QA(int nroReclamo, bool fallaCubierta, string detalle, string estadoEsperado, string estadoNuevo) =>
            dao_reclamo.RegistrarEvaluacion_382QA(nroReclamo, fallaCubierta, detalle, estadoEsperado, estadoNuevo);

        /// <summary>Registra el reclamo con su estado inicial y lo pasa al estado nuevo. Devuelve el NroReclamo generado.</summary>
        public int Registrar_382QA(BE_Reclamo_382QA reclamo, string estadoNuevo) =>
            dao_reclamo.Registrar_382QA(reclamo.NroOT, reclamo.Motivo, reclamo.Descripcion, reclamo.FechaReclamo,
                                  reclamo.Estado, estadoNuevo, reclamo.DniTecnico);

        private BE_ReclamoResumen_382QA MapearResumen_382QA(DataRow fila)
        {
            return new BE_ReclamoResumen_382QA
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
                ApellidoTecnico = fila["ApellidoTecnico"] is DBNull ? null : Convert.ToString(fila["ApellidoTecnico"]),
                FallaCubierta = fila["FallaCubierta"] is DBNull ? null : Convert.ToBoolean(fila["FallaCubierta"]),
                DetalleEvaluacion = fila["DetalleEvaluacion"] is DBNull ? null : Convert.ToString(fila["DetalleEvaluacion"])
            };
        }

        private BE_ReparacionConGarantia_382QA MapearReparacion_382QA(DataRow fila)
        {
            return new BE_ReparacionConGarantia_382QA
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
