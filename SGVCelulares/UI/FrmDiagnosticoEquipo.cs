using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using Servicio;
using System.ComponentModel;
using System.Globalization;

namespace UI
{
    /// <summary>
    /// CU-NEG-4 Diagnosticar equipo: el técnico registra el detalle, la mano de obra y los repuestos.
    /// El total del presupuesto y el aviso de stock se actualizan mientras se carga.
    /// </summary>
    public partial class FrmDiagnosticoEquipo : Form, IObservadorIdioma
    {
        private static readonly CultureInfo Moneda = CultureInfo.GetCultureInfo("es-AR");
        private static readonly Color ColorAviso = Color.FromArgb(251, 191, 36);

        private readonly int nroOT;
        private readonly BLL_Diagnostico bll_diagnostico = new BLL_Diagnostico();
        private readonly BLL_OT bll_ot = new BLL_OT();
        private readonly BLL_Repuesto bll_repuesto = new BLL_Repuesto();
        private readonly BLL_Idioma bll_idioma = new BLL_Idioma();
        private readonly BindingList<ItemRepuesto> items = new BindingList<ItemRepuesto>();

        /// <summary>Repuesto cargado en el diagnóstico (fila de la grilla).</summary>
        private class ItemRepuesto
        {
            public string Codigo { get; set; } = "";
            public string Descripcion { get; set; } = "";
            public decimal PrecioUnitario { get; set; }
            public int Cantidad { get; set; }
            public int Stock { get; set; }
            public decimal Subtotal => PrecioUnitario * Cantidad;
            public bool SinStock => Cantidad > Stock;
        }

        /// <summary>Opción del combo de repuestos.</summary>
        private class OpcionRepuesto
        {
            public BE_Repuesto Repuesto { get; }
            public OpcionRepuesto(BE_Repuesto repuesto) => Repuesto = repuesto;
            public override string ToString() =>
                $"{Repuesto.Codigo} · {Repuesto.Descripcion}  —  $ {Repuesto.PrecioUnitario.ToString("N2", Moneda)}  (stock {Repuesto.StockDisponible})";
        }

        public FrmDiagnosticoEquipo(int nroOT)
        {
            InitializeComponent();
            this.nroOT = nroOT;
            bll_idioma.Suscribir(this);
        }

        private void FrmDiagnosticoEquipo_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla();
            ActualizarIdioma();

            try
            {
                BE_OTResumen ot = bll_ot.ConsultarResumenPorNro(nroOT)
                    ?? throw new Exception($"No existe la OT N° {nroOT}.");
                lblOT.Text = $"{string.Format(bll_idioma.Traducir("FrmOrdenesTrabajo.formatoNroOT"), ot.NroOT)}  ·  {ot.Estado}";
                lblEquipo.Text = $"{ot.Equipo}  ·  IMEI {ot.Imei}  ·  {ot.Cliente}";
                lblFalla.Text = string.Format(bll_idioma.Traducir("FrmDiagnosticoEquipo.formatoFalla"), ot.DescripcionFalla);

                foreach (BE_Repuesto repuesto in bll_repuesto.Consultar())
                    cboRepuesto.Items.Add(new OpcionRepuesto(repuesto));
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }

            nudCantidad.Value = 1;
            ActualizarContador();
            Recalcular();
            txtDetalle.Focus();
        }

        private void ConfigurarGrilla()
        {
            grilla.AutoGenerateColumns = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.Columns.Clear();
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCodigo", DataPropertyName = "Codigo", FillWeight = 55 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDescripcion", DataPropertyName = "Descripcion", FillWeight = 150 });
            grilla.Columns.Add(ColumnaNumerica("colPrecio", "PrecioUnitario", "N2", 65));
            grilla.Columns.Add(ColumnaNumerica("colCantidad", "Cantidad", "N0", 40));
            grilla.Columns.Add(ColumnaNumerica("colStock", "Stock", "N0", 40));
            grilla.Columns.Add(ColumnaNumerica("colSubtotal", "Subtotal", "N2", 70));
            grilla.DataSource = items;
            grilla.CellFormatting += grilla_CellFormatting;
        }

        private static DataGridViewTextBoxColumn ColumnaNumerica(string nombre, string propiedad, string formato, int peso)
        {
            var columna = new DataGridViewTextBoxColumn { Name = nombre, DataPropertyName = propiedad, FillWeight = peso };
            columna.DefaultCellStyle.Format = formato;
            columna.DefaultCellStyle.FormatProvider = Moneda;
            columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            return columna;
        }

        /// <summary>Las filas de repuestos sin stock suficiente se ven en amarillo.</summary>
        private void grilla_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= items.Count || e.CellStyle == null) return;
            if (items[e.RowIndex].SinStock)
            {
                e.CellStyle.ForeColor = ColorAviso;
                e.CellStyle.SelectionForeColor = ColorAviso;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboRepuesto.SelectedItem is not OpcionRepuesto opcion)
            {
                MostrarAtencion(bll_idioma.Traducir("FrmDiagnosticoEquipo.msgElegirRepuesto"));
                return;
            }

            int cantidad = (int)nudCantidad.Value;
            ItemRepuesto? existente = items.FirstOrDefault(i => i.Codigo == opcion.Repuesto.Codigo);
            if (existente != null)
            {
                existente.Cantidad += cantidad;
                items.ResetBindings();
            }
            else
            {
                items.Add(new ItemRepuesto
                {
                    Codigo = opcion.Repuesto.Codigo,
                    Descripcion = opcion.Repuesto.Descripcion,
                    PrecioUnitario = opcion.Repuesto.PrecioUnitario,
                    Cantidad = cantidad,
                    Stock = opcion.Repuesto.StockDisponible
                });
            }

            nudCantidad.Value = 1;
            Recalcular();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (grilla.CurrentRow?.DataBoundItem is ItemRepuesto item)
            {
                items.Remove(item);
                Recalcular();
            }
        }

        private void nudManoObra_ValueChanged(object sender, EventArgs e) => Recalcular();

        private void txtDetalle_TextChanged(object sender, EventArgs e) => ActualizarContador();

        private void ActualizarContador() =>
            lblContador.Text = $"{txtDetalle.TextLength} / {txtDetalle.MaxLength}";

        /// <summary>Total = mano de obra + suma de subtotales; avisa si algún repuesto no tiene stock suficiente.</summary>
        private void Recalcular()
        {
            decimal total = nudManoObra.Value + items.Sum(i => i.Subtotal);
            lblTotal.Text = $"$ {total.ToString("N2", Moneda)}";

            List<string> sinStock = items.Where(i => i.SinStock).Select(i => i.Codigo).ToList();
            lblAvisoStock.Text = sinStock.Count == 0
                ? ""
                : string.Format(bll_idioma.Traducir("FrmDiagnosticoEquipo.msgAvisoStock"), string.Join(", ", sinStock));
            grilla.Invalidate();
        }

        private void btnRegistrar_Click(object sender, EventArgs e) => Registrar(aceptarSinStock: false);

        private void Registrar(bool aceptarSinStock)
        {
            List<BE_DetalleRepuesto> repuestos = items
                .Select(i => new BE_DetalleRepuesto(i.Codigo, 0, i.Cantidad, 0))
                .ToList();

            try
            {
                BE_OTResumen ot = bll_diagnostico.RegistrarDiagnostico(
                    nroOT, txtDetalle.Text, nudManoObra.Value, repuestos, aceptarSinStock);

                string mensaje = ot.Estado == EstadosOT.SinReparar
                    ? string.Format(bll_idioma.Traducir("FrmDiagnosticoEquipo.msgOkSinStock"), ot.NroOT)
                    : string.Format(bll_idioma.Traducir("FrmDiagnosticoEquipo.msgOkPresupuesto"),
                                    ot.NroOT, (ot.TotalPresupuesto ?? 0).ToString("N2", Moneda));
                MessageBox.Show(mensaje, bll_idioma.Traducir("ABM.Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (StockInsuficienteException ex)
            {
                // Flujo alternativo 6.1.1: se muestra qué falta y el técnico decide.
                string detalle = string.Join(Environment.NewLine, ex.Faltantes.Select(f => "• " + f));
                string pregunta = string.Format(bll_idioma.Traducir("FrmDiagnosticoEquipo.msgConfirmarSinStock"), detalle);
                if (MessageBox.Show(pregunta, bll_idioma.Traducir("ABM.Confirmar"),
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Registrar(aceptarSinStock: true);
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void MostrarAtencion(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir("ABM.Atencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private void MostrarError(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir("ABM.Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void ActualizarIdioma()
        {
            Text = bll_idioma.Traducir("FrmDiagnosticoEquipo.Form");
            lblTitulo.Text = bll_idioma.Traducir("FrmDiagnosticoEquipo.lblTitulo");
            lblPasoDiagnostico.Text = bll_idioma.Traducir("FrmDiagnosticoEquipo.lblPasoDiagnostico");
            lblManoObra.Text = bll_idioma.Traducir("FrmDiagnosticoEquipo.lblManoObra");
            lblPasoRepuestos.Text = bll_idioma.Traducir("FrmDiagnosticoEquipo.lblPasoRepuestos");
            lblCantidad.Text = bll_idioma.Traducir("FrmDiagnosticoEquipo.lblCantidad");
            btnAgregar.Text = bll_idioma.Traducir("FrmDiagnosticoEquipo.btnAgregar");
            btnQuitar.Text = bll_idioma.Traducir("FrmDiagnosticoEquipo.btnQuitar");
            lblTotalTitulo.Text = bll_idioma.Traducir("FrmDiagnosticoEquipo.lblTotalTitulo");
            btnRegistrar.Text = bll_idioma.Traducir("FrmDiagnosticoEquipo.btnRegistrar");
            btnCancelar.Text = bll_idioma.Traducir("ABM.btnCancelar");

            if (grilla.Columns.Count > 0)
            {
                grilla.Columns["colCodigo"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colCodigo");
                grilla.Columns["colDescripcion"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colDescripcion");
                grilla.Columns["colPrecio"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colPrecio");
                grilla.Columns["colCantidad"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colCantidad");
                grilla.Columns["colStock"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colStock");
                grilla.Columns["colSubtotal"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colSubtotal");
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}
