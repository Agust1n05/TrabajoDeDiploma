using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using Servicio;
using System.Globalization;

namespace UI
{
    /// <summary>
    /// CU-NEG-2 Gestionar aprobación de presupuesto: el vendedor ve el diagnóstico y el detalle del presupuesto,
    /// se lo comunica al cliente y registra su respuesta con la fecha.
    /// </summary>
    public partial class FrmPresupuesto_382QA : Form, IObservadorIdioma_382QA
    {
        private static readonly CultureInfo Moneda = CultureInfo.GetCultureInfo("es-AR");

        private readonly int nroOT;
        private readonly BLL_Presupuesto_382QA bll_presupuesto = new BLL_Presupuesto_382QA();
        private readonly BLL_OT_382QA bll_ot = new BLL_OT_382QA();
        private readonly BLL_Idioma_382QA bll_idioma = new BLL_Idioma_382QA();

        public FrmPresupuesto_382QA(int nroOT)
        {
            InitializeComponent();
            this.nroOT = nroOT;
            bll_idioma.Suscribir_382QA(this);
        }

        private void FrmPresupuesto_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla_382QA();
            ActualizarIdioma_382QA();
            dtpFecha.MaxDate = DateTime.Today;
            dtpFecha.Value = DateTime.Today;

            try
            {
                BE_OTResumen_382QA ot = bll_ot.ConsultarResumenPorNro_382QA(nroOT)
                    ?? throw new Exception($"No existe la OT N° {nroOT}.");
                BE_PresupuestoDetalle_382QA presupuesto = bll_presupuesto.ConsultarDetalle_382QA(nroOT);

                lblOT.Text = $"{string.Format(bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.formatoNroOT"), ot.NroOT)}  ·  {ot.Estado}";
                lblCliente.Text = string.Format(bll_idioma.Traducir_382QA("FrmPresupuesto.formatoCliente"), ot.Cliente, ot.TelefonoCliente);
                lblEquipo.Text = $"{ot.Equipo}  ·  IMEI {ot.Imei}";
                lblDetalleDiagnostico.Text = presupuesto.DetalleDiagnostico;
                grilla.DataSource = presupuesto.Lineas;
                lblManoObra.Text = FormatearMonto_382QA(presupuesto.CostoManoObra);
                lblTotal.Text = FormatearMonto_382QA(presupuesto.TotalPresupuesto);
            }
            catch (Exception ex)
            {
                MostrarError_382QA(ex.Message);
                btnAprobar.Enabled = false;
                btnRechazar.Enabled = false;
            }
        }

        private void ConfigurarGrilla_382QA()
        {
            grilla.AutoGenerateColumns = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.Columns.Clear();
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCodigo", DataPropertyName = "Codigo", FillWeight = 55 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDescripcion", DataPropertyName = "Descripcion", FillWeight = 160 });
            grilla.Columns.Add(ColumnaNumerica_382QA("colPrecio", "PrecioUnitario", "N2", 70));
            grilla.Columns.Add(ColumnaNumerica_382QA("colCantidad", "Cantidad", "N0", 40));
            grilla.Columns.Add(ColumnaNumerica_382QA("colSubtotal", "Subtotal", "N2", 75));
        }

        private static DataGridViewTextBoxColumn ColumnaNumerica_382QA(string nombre, string propiedad, string formato, int peso)
        {
            var columna = new DataGridViewTextBoxColumn { Name = nombre, DataPropertyName = propiedad, FillWeight = peso };
            columna.DefaultCellStyle.Format = formato;
            columna.DefaultCellStyle.FormatProvider = Moneda;
            columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            return columna;
        }

        private static string FormatearMonto_382QA(decimal monto) => $"$ {monto.ToString("N2", Moneda)}";

        private void btnAprobar_Click(object sender, EventArgs e) => RegistrarRespuesta_382QA(aprobado: true);

        private void btnRechazar_Click(object sender, EventArgs e) => RegistrarRespuesta_382QA(aprobado: false);

        private void RegistrarRespuesta_382QA(bool aprobado)
        {
            string pregunta = string.Format(
                bll_idioma.Traducir_382QA(aprobado ? "FrmPresupuesto.msgConfirmarAprobar" : "FrmPresupuesto.msgConfirmarRechazar"),
                nroOT, lblTotal.Text, dtpFecha.Value.ToShortDateString());
            if (MessageBox.Show(pregunta, bll_idioma.Traducir_382QA("ABM.Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                BE_OTResumen_382QA ot = bll_presupuesto.RegistrarRespuesta_382QA(nroOT, aprobado, dtpFecha.Value);
                string mensaje = string.Format(
                    bll_idioma.Traducir_382QA(aprobado ? "FrmPresupuesto.msgOkAprobado" : "FrmPresupuesto.msgOkRechazado"),
                    ot.NroOT, ot.Estado);
                MessageBox.Show(mensaje, bll_idioma.Traducir_382QA("ABM.Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void MostrarError_382QA(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir_382QA("ABM.Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void ActualizarIdioma_382QA()
        {
            Text = bll_idioma.Traducir_382QA("FrmPresupuesto.Form");
            lblTitulo.Text = bll_idioma.Traducir_382QA("FrmPresupuesto.lblTitulo");
            lblPasoDiagnostico.Text = bll_idioma.Traducir_382QA("FrmPresupuesto.lblPasoDiagnostico");
            lblPasoDetalle.Text = bll_idioma.Traducir_382QA("FrmPresupuesto.lblPasoDetalle");
            lblTitManoObra.Text = bll_idioma.Traducir_382QA("FrmPresupuesto.lblManoObra");
            lblTitTotal.Text = bll_idioma.Traducir_382QA("FrmPresupuesto.lblTotal");
            lblPasoRespuesta.Text = bll_idioma.Traducir_382QA("FrmPresupuesto.lblPasoRespuesta");
            lblFecha.Text = bll_idioma.Traducir_382QA("FrmPresupuesto.lblFecha");
            btnAprobar.Text = bll_idioma.Traducir_382QA("FrmPresupuesto.btnAprobar");
            btnRechazar.Text = bll_idioma.Traducir_382QA("FrmPresupuesto.btnRechazar");
            btnCancelar.Text = bll_idioma.Traducir_382QA("ABM.btnCancelar");

            if (grilla.Columns.Count > 0)
            {
                grilla.Columns["colCodigo"].HeaderText = bll_idioma.Traducir_382QA("FrmDiagnosticoEquipo.colCodigo");
                grilla.Columns["colDescripcion"].HeaderText = bll_idioma.Traducir_382QA("FrmDiagnosticoEquipo.colDescripcion");
                grilla.Columns["colPrecio"].HeaderText = bll_idioma.Traducir_382QA("FrmDiagnosticoEquipo.colPrecio");
                grilla.Columns["colCantidad"].HeaderText = bll_idioma.Traducir_382QA("FrmDiagnosticoEquipo.colCantidad");
                grilla.Columns["colSubtotal"].HeaderText = bll_idioma.Traducir_382QA("FrmDiagnosticoEquipo.colSubtotal");
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_382QA(this);
            base.OnFormClosed(e);
        }
    }
}
