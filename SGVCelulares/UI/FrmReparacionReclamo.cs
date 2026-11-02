using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using Servicio;
using System.ComponentModel;

namespace UI
{
    /// <summary>
    /// CU-NEG-9 Reparar equipo por garantía. El técnico elige los repuestos que usa (sin cargo) y confirma:
    /// con stock el reclamo pasa a "Reparado"; sin stock queda "En espera de repuesto".
    /// </summary>
    public partial class FrmReparacionReclamo : Form, IObservadorIdioma
    {
        private static readonly Color ColorAviso = Color.FromArgb(251, 191, 36);

        private readonly int nroReclamo;
        private readonly BLL_Reclamo bll_reclamo = new BLL_Reclamo();
        private readonly BLL_Repuesto bll_repuesto = new BLL_Repuesto();
        private readonly BLL_Idioma bll_idioma = new BLL_Idioma();
        private readonly BindingList<ItemRepuesto> items = new BindingList<ItemRepuesto>();

        /// <summary>Repuesto cargado en la reparación (fila de la grilla).</summary>
        private class ItemRepuesto
        {
            public string Codigo { get; set; } = "";
            public string Descripcion { get; set; } = "";
            public int Cantidad { get; set; }
            public int Stock { get; set; }
            public bool SinStock => Cantidad > Stock;
        }

        /// <summary>Opción del combo de repuestos.</summary>
        private class OpcionRepuesto
        {
            public BE_Repuesto Repuesto { get; }
            public OpcionRepuesto(BE_Repuesto repuesto) => Repuesto = repuesto;
            public override string ToString() => $"{Repuesto.Codigo} · {Repuesto.Descripcion}  (stock {Repuesto.StockDisponible})";
        }

        public FrmReparacionReclamo(int nroReclamo)
        {
            InitializeComponent();
            this.nroReclamo = nroReclamo;
            bll_idioma.Suscribir(this);
        }

        private void FrmReparacionReclamo_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla();
            ActualizarIdioma();

            try
            {
                BE_ReclamoResumen reclamo = bll_reclamo.ObtenerReclamoParaReparar(nroReclamo);
                lblReclamo.Text = string.Format(bll_idioma.Traducir("FrmEvaluacionReclamo.formatoReclamo"),
                                                reclamo.NroReclamo, reclamo.FechaReclamo.ToShortDateString(), reclamo.Estado);
                lblEquipo.Text = $"{reclamo.Equipo}  ·  IMEI {reclamo.Imei}  ·  {reclamo.Cliente}";
                lblEvaluacion.Text = reclamo.DetalleEvaluacion ?? "";
                CargarRepuestos();
            }
            catch (Exception ex)
            {
                btnConfirmar.Enabled = false;
                btnAgregar.Enabled = false;
                MostrarError(ex.Message);
            }
        }

        private void CargarRepuestos()
        {
            cboRepuesto.Items.Clear();
            foreach (BE_Repuesto repuesto in bll_repuesto.Consultar())
                cboRepuesto.Items.Add(new OpcionRepuesto(repuesto));
        }

        private void ConfigurarGrilla()
        {
            grilla.AutoGenerateColumns = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.Columns.Clear();
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCodigo", DataPropertyName = "Codigo", FillWeight = 55 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDescripcion", DataPropertyName = "Descripcion", FillWeight = 150 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCantidad", DataPropertyName = "Cantidad", FillWeight = 40 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStock", DataPropertyName = "Stock", FillWeight = 40 });
            grilla.DataSource = items;
            grilla.CellFormatting += grilla_CellFormatting;
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
                    Cantidad = cantidad,
                    Stock = opcion.Repuesto.StockDisponible
                });
            }

            nudCantidad.Value = 1;
            ActualizarAviso();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (grilla.CurrentRow?.DataBoundItem is ItemRepuesto item)
            {
                items.Remove(item);
                ActualizarAviso();
            }
        }

        private void ActualizarAviso()
        {
            List<string> sinStock = items.Where(i => i.SinStock).Select(i => i.Codigo).ToList();
            lblAvisoStock.Text = sinStock.Count == 0
                ? ""
                : string.Format(bll_idioma.Traducir("FrmReparacionReclamo.msgAvisoStock"), string.Join(", ", sinStock));
            grilla.Invalidate();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string resumen = items.Count == 0
                ? bll_idioma.Traducir("FrmReparacionReclamo.msgSinRepuestos")
                : string.Join(Environment.NewLine, items.Select(i => $"• {i.Codigo} - {i.Descripcion}: {i.Cantidad}"));
            string pregunta = string.Format(bll_idioma.Traducir("FrmReparacionReclamo.msgConfirmar"), nroReclamo, resumen);
            if (MessageBox.Show(pregunta, bll_idioma.Traducir("ABM.Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                List<BE_FaltanteStock> faltantes = bll_reclamo.RepararReclamo(
                    nroReclamo, items.ToDictionary(i => i.Codigo, i => i.Cantidad));

                if (faltantes.Count == 0)
                {
                    MessageBox.Show(string.Format(bll_idioma.Traducir("FrmReparacionReclamo.msgOk"), nroReclamo),
                                    bll_idioma.Traducir("ABM.Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Flujo alternativo: sin stock el reclamo queda esperando y se retoma cuando se reponga
                    string detalle = string.Join(Environment.NewLine, faltantes.Select(f => "• " + f));
                    MessageBox.Show(string.Format(bll_idioma.Traducir("FrmReparacionReclamo.msgEnEspera"), nroReclamo, detalle),
                                    bll_idioma.Traducir("ABM.Atencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                DialogResult = DialogResult.OK;
                Close();
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
            Text = bll_idioma.Traducir("FrmReparacionReclamo.Form");
            lblTitulo.Text = bll_idioma.Traducir("FrmReparacionReclamo.lblTitulo");
            lblPasoReclamo.Text = bll_idioma.Traducir("FrmReparacionReclamo.lblPasoReclamo");
            lblPasoRepuestos.Text = bll_idioma.Traducir("FrmReparacionReclamo.lblPasoRepuestos");
            lblTitReclamo.Text = bll_idioma.Traducir("FrmEvaluacionReclamo.lblTitReclamo");
            lblTitEquipo.Text = bll_idioma.Traducir("FrmEvaluacionReclamo.lblTitEquipo");
            lblTitEvaluacion.Text = bll_idioma.Traducir("FrmReparacionReclamo.lblTitEvaluacion");
            btnAgregar.Text = bll_idioma.Traducir("FrmDiagnosticoEquipo.btnAgregar");
            btnQuitar.Text = bll_idioma.Traducir("FrmDiagnosticoEquipo.btnQuitar");
            btnConfirmar.Text = bll_idioma.Traducir("FrmReparacionReclamo.btnConfirmar");
            btnCancelar.Text = bll_idioma.Traducir("ABM.btnCancelar");

            if (grilla.Columns.Count > 0)
            {
                grilla.Columns["colCodigo"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colCodigo");
                grilla.Columns["colDescripcion"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colDescripcion");
                grilla.Columns["colCantidad"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colCantidad");
                grilla.Columns["colStock"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colStock");
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}
