using BE;
using DAL.DAL_NEG;
using System.Data;

namespace MAP.MAP_NEG
{
    public class MAP_Presupuesto
    {
        DAO_Presupuesto dao_presupuesto;
        public MAP_Presupuesto() => dao_presupuesto = new();

        /// <summary>Presupuesto de la OT con su diagnóstico y repuestos, o null si la OT no tiene presupuesto.</summary>
        public BE_PresupuestoDetalle? ConsultarDetallePorOT(int nroOT)
        {
            DataTable tabla = dao_presupuesto.ConsultarPorOT(nroOT);
            if (tabla.Rows.Count == 0) return null;

            DataRow fila = tabla.Rows[0];
            var detalle = new BE_PresupuestoDetalle
            {
                NroOT = Convert.ToInt32(fila["NroOT"]),
                DetalleDiagnostico = Convert.ToString(fila["detalle"]),
                CostoManoObra = Convert.ToDecimal(fila["costo_mano_obra"]),
                IdPresupuesto = Convert.ToInt32(fila["IdPresupuesto"]),
                TotalPresupuesto = Convert.ToDecimal(fila["TotalPresupuesto"]),
                EstadoPresupuesto = Convert.ToString(fila["EstadoPresupuesto"]),
                FechaRespuesta = fila["FechaRespuesta"] is DBNull ? null : Convert.ToDateTime(fila["FechaRespuesta"])
            };

            foreach (DataRow linea in dao_presupuesto.ConsultarLineasPorOT(nroOT).Rows)
            {
                detalle.Lineas.Add(new BE_LineaPresupuesto
                {
                    Codigo = Convert.ToString(linea["Codigo"]),
                    Descripcion = Convert.ToString(linea["Descripcion"]),
                    PrecioUnitario = Convert.ToDecimal(linea["PrecioUnitario"]),
                    Cantidad = Convert.ToInt32(linea["cantidadRequerida"])
                });
            }
            return detalle;
        }

        public void RegistrarRespuesta(BE_Presupuesto presupuesto, string estadoPresupuestoEsperado,
                                       string estadoOTEsperado, string estadoOTNuevo)
        {
            dao_presupuesto.RegistrarRespuesta(
                presupuesto.IdPresupuesto, presupuesto.NroOT, presupuesto.EstadoPresupuesto,
                presupuesto.FechaRespuesta ?? DateTime.Today,
                estadoPresupuestoEsperado, estadoOTEsperado, estadoOTNuevo);
        }
    }
}
