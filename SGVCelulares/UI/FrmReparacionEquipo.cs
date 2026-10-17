using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using Servicio;
using System.ComponentModel;

namespace UI
{
    /// <summary>
    /// CU-NEG-5 Reparar equipo: el técnico ve el diagnóstico aprobado, corrige la cantidad utilizada de cada
    /// repuesto (por defecto, la aprobada) y confirma el fin de la reparación. El sistema revalida el stock,
    /// lo descuenta y pasa la OT a "Reparado".
    /// </summary>
    public partial class FrmReparacionEquipo : Form, IObservadorIdioma
    {
        private static readonly Color ColorAviso = Color.FromArgb(251, 191, 36);

        private readonly int nroOT;
        private readonly BLL_Reparacion bll_reparacion = new BLL_Reparacion();
        private readonly BLL_Presupuesto bll_presupuesto = new BLL_Presupuesto();
        private readonly BLL_Idioma bll_idioma = new BLL_Idioma();
        private BindingList<BE_LineaReparacion> lineas = new BindingList<BE_LineaReparacion>();

        public FrmReparacionEquipo(int nroOT)
        {
            InitializeComponent();
            this.nroOT = nroOT;
            bll_idioma.Suscribir(this);
        }

        private void FrmReparacionEquipo_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla();
            ActualizarIdioma();

            try
            {
                BE_OTResumen ot = bll_reparacion.ObtenerOTParaReparar(nroOT);
                lblOT.Text = $"{string.Format(bll_idioma.Traducir("FrmOrdenesTrabajo.formatoNroOT"), ot.NroOT)}  ·  {ot.Estado}";
                lblEquipo.Text = $"{ot.Equipo}  ·  IMEI {ot.Imei}  ·  {ot.Cliente}";
                lblFalla.Text = string.Format(bll_idioma.Traducir("FrmDiagnosticoEquipo.formatoFalla"), ot.DescripcionFalla);
                lblDetalleDiagnostico.Text = bll_presupuesto.ConsultarDetalle(nroOT).DetalleDiagnostico;
                CargarRepuestos(bll_reparacion.ConsultarRepuestos(nroOT));
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
                btnConfirmar.Enabled = false;
            }
        }

        private void ConfigurarGrilla()
        {
            grilla.AutoGenerateColumns = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.ReadOnly = false;
            grilla.SelectionMode = DataGridViewSelectionMode.CellSelect;
            grilla.EditMode = DataGridViewEditMode.EditOnEnter;
            grilla.ScrollBars = ScrollBars.Vertical;
            grilla.Columns.Clear();
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCodigo", DataPropertyName = "Codigo", FillWeight = 55, ReadOnly = true });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDescripcion", DataPropertyName = "Descripcion", FillWeight = 170, ReadOnly = true });
            grilla.Columns.Add(ColumnaNumerica("colAprobada", "CantidadRequerida", 55, soloLectura: true));
            grilla.Columns.Add(ColumnaNumerica("colUtilizada", "CantidadUtilizada", 60, soloLectura: false));
            grilla.Columns.Add(ColumnaNumerica("colStock", "StockDisponible", 50, soloLectura: true));

            // La columna editable se distingue con fondo de input
            DataGridViewColumn utilizada = grilla.Columns["colUtilizada"];
            utilizada.DefaultCellStyle.BackColor = Color.FromArgb(45, 60, 82);
            utilizada.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            grilla.CellFormatting += grilla_CellFormatting;
            grilla.CellValidating += grilla_CellValidating;
            grilla.CellEndEdit += (s, e) => ActualizarAviso();
            grilla.DataError += (s, e) => e.ThrowException = false;
            btnCancelar.CausesValidation = false;   // se puede cancelar aunque haya una cantidad inválida en edición
        }

        private static DataGridViewTextBoxColumn ColumnaNumerica(string nombre, string propiedad, int peso, bool soloLectura)
        {
            var columna = new DataGridViewTextBoxColumn { Name = nombre, DataPropertyName = propiedad, FillWeight = peso, ReadOnly = soloLectura };
            columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            return columna;
        }

        private void CargarRepuestos(List<BE_LineaReparacion> nuevas)
        {
            lineas = new BindingList<BE_LineaReparacion>(nuevas);
            grilla.DataSource = lineas;
            lblAyuda.Text = bll_idioma.Traducir(lineas.Count == 0 ? "FrmReparacionEquipo.msgSinRepuestos" : "FrmReparacionEquipo.lblAyuda");
            ActualizarAviso();
        }

        /// <summary>La cantidad utilizada tiene que ser un entero entre 0 y la cantidad aprobada.</summary>
        private void grilla_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (grilla.Columns[e.ColumnIndex].Name != "colUtilizada" || e.RowIndex < 0 || e.RowIndex >= lineas.Count) return;

            int aprobada = lineas[e.RowIndex].CantidadRequerida;
            if (!int.TryParse(Convert.ToString(e.FormattedValue), out int cantidad) || cantidad < 0 || cantidad > aprobada)
            {
                MostrarError(string.Format(bll_idioma.Traducir("FrmReparacionEquipo.msgCantidadInvalida"), aprobada));
                e.Cancel = true;
            }
        }

        private void grilla_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= lineas.Count || e.CellStyle == null) return;
            if (lineas[e.RowIndex].FaltaStock)
            {
                e.CellStyle.ForeColor = ColorAviso;
                e.CellStyle.SelectionForeColor = ColorAviso;
            }
        }

        private void ActualizarAviso()
        {
            List<string> sinStock = lineas.Where(l => l.FaltaStock).Select(l => l.Codigo).ToList();
            lblAvisoStock.Text = sinStock.Count == 0
                ? ""
                : string.Format(bll_idioma.Traducir("FrmReparacionEquipo.msgAvisoStock"), string.Join(", ", sinStock));
            grilla.Invalidate();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!grilla.EndEdit()) return;   // una cantidad inválida queda en edición

            string resumen = lineas.Count == 0
                ? bll_idioma.Traducir("FrmReparacionEquipo.msgSinRepuestos")
                : string.Join(Environment.NewLine, lineas.Select(l => $"• {l.Codigo} - {l.Descripcion}: {l.CantidadUtilizada}"));
            string pregunta = string.Format(bll_idioma.Traducir("FrmReparacionEquipo.msgConfirmar"), nroOT, resumen);
            if (MessageBox.Show(pregunta, bll_idioma.Traducir("ABM.Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                BE_OTResumen ot = bll_reparacion.RegistrarReparacion(
                    nroOT, lineas.ToDictionary(l => l.Codigo, l => l.CantidadUtilizada));

                MessageBox.Show(string.Format(bll_idioma.Traducir("FrmReparacionEquipo.msgOk"), ot.NroOT, ot.Estado),
                                bll_idioma.Traducir("ABM.Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (StockInsuficienteException ex)
            {
                // Flujo alternativo: el stock cambió desde el diagnóstico. La OT sigue "En reparacion".
                string detalle = string.Join(Environment.NewLine, ex.Faltantes.Select(f => "• " + f));
                MessageBox.Show(string.Format(bll_idioma.Traducir("FrmReparacionEquipo.msgSinStock"), detalle),
                                bll_idioma.Traducir("ABM.Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RefrescarStock();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
                RefrescarStock();
            }
        }

        /// <summary>Vuelve a leer el stock actual sin perder las cantidades que cargó el técnico.</summary>
        private void RefrescarStock()
        {
            try
            {
                Dictionary<string, int> cargadas = lineas.ToDictionary(l => l.Codigo, l => l.CantidadUtilizada);
                List<BE_LineaReparacion> nuevas = bll_reparacion.ConsultarRepuestos(nroOT);
                foreach (BE_LineaReparacion linea in nuevas)
                    if (cargadas.TryGetValue(linea.Codigo, out int cantidad))
                        linea.CantidadUtilizada = Math.Min(cantidad, linea.CantidadRequerida);
                CargarRepuestos(nuevas);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            grilla.CancelEdit();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void MostrarError(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir("ABM.Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void ActualizarIdioma()
        {
            Text = bll_idioma.Traducir("FrmReparacionEquipo.Form");
            lblTitulo.Text = bll_idioma.Traducir("FrmReparacionEquipo.lblTitulo");
            lblPasoDiagnostico.Text = bll_idioma.Traducir("FrmReparacionEquipo.lblPasoDiagnostico");
            lblPasoRepuestos.Text = bll_idioma.Traducir("FrmReparacionEquipo.lblPasoRepuestos");
            lblAyuda.Text = bll_idioma.Traducir(lineas.Count == 0 && grilla.DataSource != null
                ? "FrmReparacionEquipo.msgSinRepuestos" : "FrmReparacionEquipo.lblAyuda");
            btnConfirmar.Text = bll_idioma.Traducir("FrmReparacionEquipo.btnConfirmar");
            btnCancelar.Text = bll_idioma.Traducir("ABM.btnCancelar");

            if (grilla.Columns.Count > 0)
            {
                grilla.Columns["colCodigo"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colCodigo");
                grilla.Columns["colDescripcion"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colDescripcion");
                grilla.Columns["colAprobada"].HeaderText = bll_idioma.Traducir("FrmReparacionEquipo.colAprobada");
                grilla.Columns["colUtilizada"].HeaderText = bll_idioma.Traducir("FrmReparacionEquipo.colUtilizada");
                grilla.Columns["colStock"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colStock");
            }
            ActualizarAviso();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}
