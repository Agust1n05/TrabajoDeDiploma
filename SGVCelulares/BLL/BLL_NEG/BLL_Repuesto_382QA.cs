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
    public class BLL_Repuesto_382QA : IABMC_382QA<BE_Repuesto_382QA>
    {
        private const string Modulo = "Repuestos";
        private const string MsgStockCambio = "El stock del repuesto cambió mientras lo editaba (por ejemplo, se usó en una reparación). Cancele y vuelva a intentar con los datos actualizados.";
        private const decimal PrecioMaximo = 9999999999999999.99m; // DECIMAL(18,2)

        MAP_Repuesto_382QA map_repuesto;
        BLL_Bitacora_382QA bll_bitacora;
        BLL_DV_382QA bll_dv;

        public BLL_Repuesto_382QA()
        {
            map_repuesto = new MAP_Repuesto_382QA();
            bll_bitacora = new BLL_Bitacora_382QA();
            bll_dv = new BLL_DV_382QA();
        }

        /// <summary>Repuestos activos, ordenados por código.</summary>
        public List<BE_Repuesto_382QA> Consultar_382QA()
        {
            return map_repuesto.Consultar_382QA()
                .Where(r => r.Activo)
                .OrderBy(r => r.Codigo)
                .ToList();
        }

        /// <summary>Repuesto por código, esté activo o dado de baja.</summary>
        public BE_Repuesto_382QA ConsultarPorId_382QA(BE_Repuesto_382QA repuesto) =>
            map_repuesto.ConsultarPorCodigo_382QA(repuesto.Codigo?.Trim().ToUpperInvariant() ?? "")!;

        /// <summary>Repuesto activo por código, o null si no existe o está dado de baja.</summary>
        public BE_Repuesto_382QA? ConsultarPorCodigo_382QA(string codigo)
        {
            BE_Repuesto_382QA? repuesto = map_repuesto.ConsultarPorCodigo_382QA(codigo?.Trim().ToUpperInvariant() ?? "");
            return repuesto != null && repuesto.Activo ? repuesto : null;
        }

        public bool EstaDadoDeBaja_382QA(string codigo)
        {
            BE_Repuesto_382QA? repuesto = map_repuesto.ConsultarPorCodigo_382QA(codigo?.Trim().ToUpperInvariant() ?? "");
            return repuesto != null && !repuesto.Activo;
        }

        /// <summary>
        /// Alta de repuesto. Si el código pertenece a un repuesto dado de baja, lo reactiva con los datos nuevos.
        /// </summary>
        public void Agregar_382QA(BE_Repuesto_382QA repuesto)
        {
            Normalizar_382QA(repuesto);
            Validar_382QA(repuesto);

            BE_Repuesto_382QA? existente = map_repuesto.ConsultarPorCodigo_382QA(repuesto.Codigo);
            if (existente != null && existente.Activo)
                throw new Exception($"Ya existe un repuesto con el código {repuesto.Codigo}.");

            if (existente != null)
            {
                if (!map_repuesto.Modificar_382QA(repuesto, existente.StockDisponible))
                    throw new Exception(MsgStockCambio);
                map_repuesto.CambiarActivo_382QA(repuesto.Codigo, true);
                bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaRepuesto, repuesto.Codigo);
                RegistrarBitacora_382QA("Reactivar repuesto", 1);
            }
            else
            {
                map_repuesto.Agregar_382QA(repuesto);
                bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaRepuesto, repuesto.Codigo);
                RegistrarBitacora_382QA("Crear repuesto", 2);
            }
        }

        /// <param name="stockOriginal">Stock que tenía el repuesto cuando se cargó en el formulario.</param>
        public void Modificar_382QA(BE_Repuesto_382QA repuesto, int stockOriginal)
        {
            Normalizar_382QA(repuesto);
            Validar_382QA(repuesto);
            ObtenerActivo_382QA(repuesto.Codigo);

            if (!map_repuesto.Modificar_382QA(repuesto, stockOriginal))
                throw new Exception(MsgStockCambio);
            bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaRepuesto, repuesto.Codigo);
            RegistrarBitacora_382QA("Modificar repuesto", 2);
        }
        public void Modificar_382QA(BE_Repuesto_382QA repuesto) { }
      
        /// <summary>
        /// Baja lógica. No se permite si el repuesto está pedido en el diagnóstico de una OT en curso,
        /// porque la reparación todavía lo va a necesitar.
        /// </summary>
        public void Borrar_382QA(BE_Repuesto_382QA repuesto)
        {
            if (repuesto == null) throw new Exception("El repuesto no puede ser nulo.");
            BE_Repuesto_382QA existente = ObtenerActivo_382QA(repuesto.Codigo);

            int otEnCurso = map_repuesto.ContarOT_382QA(existente.Codigo, EstadosOT_382QA.EnCurso);
            if (otEnCurso > 0)
                throw new Exception($"No se puede eliminar el repuesto: está pedido en {otEnCurso} orden(es) de trabajo en curso.");

            map_repuesto.CambiarActivo_382QA(existente.Codigo, false);
            bll_dv.ActualizarDVNegocio_382QA(BLL_DV_382QA.TablaRepuesto, existente.Codigo);
            RegistrarBitacora_382QA("Eliminar repuesto", 1);
        }

        private BE_Repuesto_382QA ObtenerActivo_382QA(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new Exception("El código es obligatorio.");

            return ConsultarPorCodigo_382QA(codigo)
                ?? throw new Exception($"No existe un repuesto activo con el código {codigo}.");
        }

        private void Normalizar_382QA(BE_Repuesto_382QA repuesto)
        {
            if (repuesto == null) throw new Exception("El repuesto no puede ser nulo.");
            repuesto.Codigo = repuesto.Codigo?.Trim().ToUpperInvariant() ?? "";
            repuesto.Descripcion = repuesto.Descripcion?.Trim() ?? "";
        }

        private void Validar_382QA(BE_Repuesto_382QA repuesto)
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

        private void RegistrarBitacora_382QA(string evento, int criticidad)
        {
            bll_bitacora.RegistrarBitacora_382QA(new SER_Bitacora_382QA(
                SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario, DateTime.Now, Modulo, evento, criticidad));
        }
    }
}
