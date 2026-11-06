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
    public partial class FrmReparacionReclamo_382QA : Form, IObservadorIdioma_382QA
    {
        private static readonly Color ColorAviso = Color.FromArgb(251, 191, 36);

        private readonly int nroReclamo;
        private readonly BLL_Reclamo_382QA bll_reclamo = new BLL_Reclamo_382QA();
        private readonly BLL_Repuesto_382QA bll_repuesto = new BLL_Repuesto_382QA();
        private readonly BLL_Idioma_382QA bll_idioma = new BLL_Idioma_382QA();
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
            public BE_Repuesto_382QA Repuesto { get; }
            public OpcionRepuesto(BE_Repuesto_382QA repuesto) => Repuesto = repuesto;
            public override string ToString() => $"{Repuesto.Codigo} · {Repuesto.Descripcion}  (stock {Repuesto.StockDisponible})";
        }

        public FrmReparacionReclamo_382QA(int nroReclamo)
        {
            InitializeComponent();
            this.nroReclamo = nroReclamo;
            bll_idioma.Suscribir_382QA(this);
        }

        private void FrmReparacionReclamo_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla_382QA();
            ActualizarIdioma_382QA();

            try
            {
                BE_ReclamoResumen_382QA reclamo = bll_reclamo.ObtenerReclamoParaReparar_382QA(nroReclamo);
                lblReclamo.Text = string.Format(bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.formatoReclamo"),
                                                reclamo.NroReclamo, reclamo.FechaReclamo.ToShortDateString(), reclamo.Estado);
                lblEquipo.Text = $"{reclamo.Equipo}  ·  IMEI {reclamo.Imei}  ·  {reclamo.Cliente}";
                lblEvaluacion.Text = reclamo.DetalleEvaluacion ?? "";
                CargarRepuestos_382QA();
            }
            catch (Exception ex)
            {
                btnConfirmar.Enabled = false;
                btnAgregar.Enabled = false;
                MostrarError_382QA(ex.Message);
            }
        }

        private void CargarRepuestos_382QA()
        {
            cboRepuesto.Items.Clear();
            foreach (BE_Repuesto_382QA repuesto in bll_repuesto.Consultar_382QA())
                cboRepuesto.Items.Add(new OpcionRepuesto(repuesto));
        }

        private void ConfigurarGrilla_382QA()
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
                MostrarAtencion_382QA(bll_idioma.Traducir_382QA("FrmDiagnosticoEquipo.msgElegirRepuesto"));
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
            ActualizarAviso_382QA();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (grilla.CurrentRow?.DataBoundItem is ItemRepuesto item)
            {
                items.Remove(item);
                ActualizarAviso_382QA();
            }
        }

        private void ActualizarAviso_382QA()
        {
            List<string> sinStock = items.Where(i => i.SinStock).Select(i => i.Codigo).ToList();
            lblAvisoStock.Text = sinStock.Count == 0
                ? ""
                : string.Format(bll_idioma.Traducir_382QA("FrmReparacionReclamo.msgAvisoStock"), string.Join(", ", sinStock));
            grilla.Invalidate();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string resumen = items.Count == 0
                ? bll_idioma.Traducir_382QA("FrmReparacionReclamo.msgSinRepuestos")
                : string.Join(Environment.NewLine, items.Select(i => $"• {i.Codigo} - {i.Descripcion}: {i.Cantidad}"));
            string pregunta = string.Format(bll_idioma.Traducir_382QA("FrmReparacionReclamo.msgConfirmar"), nroReclamo, resumen);
            if (MessageBox.Show(pregunta, bll_idioma.Traducir_382QA("ABM.Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                List<BE_FaltanteStock_382QA> faltantes = bll_reclamo.RepararReclamo_382QA(
                    nroReclamo, items.ToDictionary(i => i.Codigo, i => i.Cantidad));

                if (faltantes.Count == 0)
                {
                    MessageBox.Show(string.Format(bll_idioma.Traducir_382QA("FrmReparacionReclamo.msgOk"), nroReclamo),
                                    bll_idioma.Traducir_382QA("ABM.Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Flujo alternativo: sin stock el reclamo queda esperando y se retoma cuando se reponga
                    string detalle = string.Join(Environment.NewLine, faltantes.Select(f => "• " + f));
                    MessageBox.Show(string.Format(bll_idioma.Traducir_382QA("FrmReparacionReclamo.msgEnEspera"), nroReclamo, detalle),
                                    bll_idioma.Traducir_382QA("ABM.Atencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MostrarError_382QA(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void MostrarAtencion_382QA(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir_382QA("ABM.Atencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private void MostrarError_382QA(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir_382QA("ABM.Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void ActualizarIdioma_382QA()
        {
            Text = bll_idioma.Traducir_382QA("FrmReparacionReclamo.Form");
            lblTitulo.Text = bll_idioma.Traducir_382QA("FrmReparacionReclamo.lblTitulo");
            lblPasoReclamo.Text = bll_idioma.Traducir_382QA("FrmReparacionReclamo.lblPasoReclamo");
            lblPasoRepuestos.Text = bll_idioma.Traducir_382QA("FrmReparacionReclamo.lblPasoRepuestos");
            lblTitReclamo.Text = bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.lblTitReclamo");
            lblTitEquipo.Text = bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.lblTitEquipo");
            lblTitEvaluacion.Text = bll_idioma.Traducir_382QA("FrmReparacionReclamo.lblTitEvaluacion");
            btnAgregar.Text = bll_idioma.Traducir_382QA("FrmDiagnosticoEquipo.btnAgregar");
            btnQuitar.Text = bll_idioma.Traducir_382QA("FrmDiagnosticoEquipo.btnQuitar");
            btnConfirmar.Text = bll_idioma.Traducir_382QA("FrmReparacionReclamo.btnConfirmar");
            btnCancelar.Text = bll_idioma.Traducir_382QA("ABM.btnCancelar");

            if (grilla.Columns.Count > 0)
            {
                grilla.Columns["colCodigo"].HeaderText = bll_idioma.Traducir_382QA("FrmDiagnosticoEquipo.colCodigo");
                grilla.Columns["colDescripcion"].HeaderText = bll_idioma.Traducir_382QA("FrmDiagnosticoEquipo.colDescripcion");
                grilla.Columns["colCantidad"].HeaderText = bll_idioma.Traducir_382QA("FrmDiagnosticoEquipo.colCantidad");
                grilla.Columns["colStock"].HeaderText = bll_idioma.Traducir_382QA("FrmDiagnosticoEquipo.colStock");
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_382QA(this);
            base.OnFormClosed(e);
        }
    }
}
