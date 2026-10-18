using BE;
using BLL.BLL_SER;
using MAP.MAP_NEG;
using Servicio;
using System.Text.RegularExpressions;

namespace BLL.BLL_NEG
{
    /// <summary>
    /// ABM de repuestos con baja lógica. Funciona como inventario simple: el precio y el stock
    /// se cargan y corrigen desde acá (la reparación descuenta el stock en CU-NEG-5).
    /// </summary>
    public class BLL_Repuesto : IABMC<BE_Repuesto>
    {
        private const string Modulo = "Repuestos";
        private const string MsgStockCambio = "El stock del repuesto cambió mientras lo editaba (por ejemplo, se usó en una reparación). Cancele y vuelva a intentar con los datos actualizados.";
        private const decimal PrecioMaximo = 9999999999999999.99m; // DECIMAL(18,2)

        MAP_Repuesto map_repuesto;
        BLL_Bitacora bll_bitacora;
        BLL_DV bll_dv;

        public BLL_Repuesto()
        {
            map_repuesto = new MAP_Repuesto();
            bll_bitacora = new BLL_Bitacora();
            bll_dv = new BLL_DV();
        }

        /// <summary>Repuestos activos, ordenados por código.</summary>
        public List<BE_Repuesto> Consultar()
        {
            return map_repuesto.Consultar()
                .Where(r => r.Activo)
                .OrderBy(r => r.Codigo)
                .ToList();
        }

        /// <summary>Repuesto por código, esté activo o dado de baja.</summary>
        public BE_Repuesto ConsultarPorId(BE_Repuesto repuesto) =>
            map_repuesto.ConsultarPorCodigo(repuesto.Codigo?.Trim().ToUpperInvariant() ?? "")!;

        /// <summary>Repuesto activo por código, o null si no existe o está dado de baja.</summary>
        public BE_Repuesto? ConsultarPorCodigo(string codigo)
        {
            BE_Repuesto? repuesto = map_repuesto.ConsultarPorCodigo(codigo?.Trim().ToUpperInvariant() ?? "");
            return repuesto != null && repuesto.Activo ? repuesto : null;
        }

        public bool EstaDadoDeBaja(string codigo)
        {
            BE_Repuesto? repuesto = map_repuesto.ConsultarPorCodigo(codigo?.Trim().ToUpperInvariant() ?? "");
            return repuesto != null && !repuesto.Activo;
        }

        /// <summary>
        /// Alta de repuesto. Si el código pertenece a un repuesto dado de baja, lo reactiva con los datos nuevos.
        /// </summary>
        public void Agregar(BE_Repuesto repuesto)
        {
            Normalizar(repuesto);
            Validar(repuesto);

            BE_Repuesto? existente = map_repuesto.ConsultarPorCodigo(repuesto.Codigo);
            if (existente != null && existente.Activo)
                throw new Exception($"Ya existe un repuesto con el código {repuesto.Codigo}.");

            if (existente != null)
            {
                if (!map_repuesto.Modificar(repuesto, existente.StockDisponible))
                    throw new Exception(MsgStockCambio);
                map_repuesto.CambiarActivo(repuesto.Codigo, true);
                bll_dv.ActualizarDVNegocio(BLL_DV.TablaRepuesto, repuesto.Codigo);
                RegistrarBitacora("Reactivar repuesto", 1);
            }
            else
            {
                map_repuesto.Agregar(repuesto);
                bll_dv.ActualizarDVNegocio(BLL_DV.TablaRepuesto, repuesto.Codigo);
                RegistrarBitacora("Crear repuesto", 2);
            }
        }

        /// <param name="stockOriginal">Stock que tenía el repuesto cuando se cargó en el formulario.</param>
        public void Modificar(BE_Repuesto repuesto, int stockOriginal)
        {
            Normalizar(repuesto);
            Validar(repuesto);
            ObtenerActivo(repuesto.Codigo);

            if (!map_repuesto.Modificar(repuesto, stockOriginal))
                throw new Exception(MsgStockCambio);
            bll_dv.ActualizarDVNegocio(BLL_DV.TablaRepuesto, repuesto.Codigo);
            RegistrarBitacora("Modificar repuesto", 2);
        }
        public void Modificar(BE_Repuesto repuesto) { }
      
        /// <summary>
        /// Baja lógica. No se permite si el repuesto está pedido en el diagnóstico de una OT en curso,
        /// porque la reparación todavía lo va a necesitar.
        /// </summary>
        public void Borrar(BE_Repuesto repuesto)
        {
            if (repuesto == null) throw new Exception("El repuesto no puede ser nulo.");
            BE_Repuesto existente = ObtenerActivo(repuesto.Codigo);

            int otEnCurso = map_repuesto.ContarOT(existente.Codigo, EstadosOT.EnCurso);
            if (otEnCurso > 0)
                throw new Exception($"No se puede eliminar el repuesto: está pedido en {otEnCurso} orden(es) de trabajo en curso.");

            map_repuesto.CambiarActivo(existente.Codigo, false);
            bll_dv.ActualizarDVNegocio(BLL_DV.TablaRepuesto, existente.Codigo);
            RegistrarBitacora("Eliminar repuesto", 1);
        }

        private BE_Repuesto ObtenerActivo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new Exception("El código es obligatorio.");

            return ConsultarPorCodigo(codigo)
                ?? throw new Exception($"No existe un repuesto activo con el código {codigo}.");
        }

        private void Normalizar(BE_Repuesto repuesto)
        {
            if (repuesto == null) throw new Exception("El repuesto no puede ser nulo.");
            repuesto.Codigo = repuesto.Codigo?.Trim().ToUpperInvariant() ?? "";
            repuesto.Descripcion = repuesto.Descripcion?.Trim() ?? "";
        }

        private void Validar(BE_Repuesto repuesto)
        {
            if (string.IsNullOrWhiteSpace(repuesto.Codigo))
                throw new Exception("El código es obligatorio.");

            if (repuesto.Codigo.Length > 20)
                throw new Exception("El código no puede superar los 20 caracteres.");

            if (!Regex.IsMatch(repuesto.Codigo, @"^[A-Z0-9-]+$"))
                throw new Exception("El código solo puede contener letras, números y guiones.");

            if (string.IsNullOrWhiteSpace(repuesto.Descripcion))
                throw new Exception("La descripción es obligatoria.");

            if (repuesto.Descripcion.Length > 100)
                throw new Exception("La descripción no puede superar los 100 caracteres.");

            if (repuesto.PrecioUnitario <= 0)
                throw new Exception("El precio unitario debe ser mayor a cero.");

            if (repuesto.PrecioUnitario > PrecioMaximo || decimal.Round(repuesto.PrecioUnitario, 2) != repuesto.PrecioUnitario)
                throw new Exception("El precio unitario no es válido (máximo dos decimales).");

            if (repuesto.StockDisponible < 0)
                throw new Exception("El stock no puede ser negativo.");
        }

        private void RegistrarBitacora(string evento, int criticidad)
        {
            bll_bitacora.RegistrarBitacora(new SER_Bitacora(
                SER_SesionManager.ObtenerSesion().Usuario, DateTime.Now, Modulo, evento, criticidad));
        }
    }
}
