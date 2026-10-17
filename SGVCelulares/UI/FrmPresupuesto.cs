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
    public partial class FrmPresupuesto : Form, IObservadorIdioma
    {
        private static readonly CultureInfo Moneda = CultureInfo.GetCultureInfo("es-AR");

        private readonly int nroOT;
        private readonly BLL_Presupuesto bll_presupuesto = new BLL_Presupuesto();
        private readonly BLL_OT bll_ot = new BLL_OT();
        private readonly BLL_Idioma bll_idioma = new BLL_Idioma();

        public FrmPresupuesto(int nroOT)
        {
            InitializeComponent();
            this.nroOT = nroOT;
            bll_idioma.Suscribir(this);
        }

        private void FrmPresupuesto_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla();
            ActualizarIdioma();
            dtpFecha.MaxDate = DateTime.Today;
            dtpFecha.Value = DateTime.Today;

            try
            {
                BE_OTResumen ot = bll_ot.ConsultarResumenPorNro(nroOT)
                    ?? throw new Exception($"No existe la OT N° {nroOT}.");
                BE_PresupuestoDetalle presupuesto = bll_presupuesto.ConsultarDetalle(nroOT);

                lblOT.Text = $"{string.Format(bll_idioma.Traducir("FrmOrdenesTrabajo.formatoNroOT"), ot.NroOT)}  ·  {ot.Estado}";
                lblCliente.Text = string.Format(bll_idioma.Traducir("FrmPresupuesto.formatoCliente"), ot.Cliente, ot.TelefonoCliente);
                lblEquipo.Text = $"{ot.Equipo}  ·  IMEI {ot.Imei}";
                lblDetalleDiagnostico.Text = presupuesto.DetalleDiagnostico;
                grilla.DataSource = presupuesto.Lineas;
                lblManoObra.Text = FormatearMonto(presupuesto.CostoManoObra);
                lblTotal.Text = FormatearMonto(presupuesto.TotalPresupuesto);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
                btnAprobar.Enabled = false;
                btnRechazar.Enabled = false;
            }
        }

        private void ConfigurarGrilla()
        {
            grilla.AutoGenerateColumns = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.Columns.Clear();
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCodigo", DataPropertyName = "Codigo", FillWeight = 55 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDescripcion", DataPropertyName = "Descripcion", FillWeight = 160 });
            grilla.Columns.Add(ColumnaNumerica("colPrecio", "PrecioUnitario", "N2", 70));
            grilla.Columns.Add(ColumnaNumerica("colCantidad", "Cantidad", "N0", 40));
            grilla.Columns.Add(ColumnaNumerica("colSubtotal", "Subtotal", "N2", 75));
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

        private static string FormatearMonto(decimal monto) => $"$ {monto.ToString("N2", Moneda)}";

        private void btnAprobar_Click(object sender, EventArgs e) => RegistrarRespuesta(aprobado: true);

        private void btnRechazar_Click(object sender, EventArgs e) => RegistrarRespuesta(aprobado: false);

        private void RegistrarRespuesta(bool aprobado)
        {
            string pregunta = string.Format(
                bll_idioma.Traducir(aprobado ? "FrmPresupuesto.msgConfirmarAprobar" : "FrmPresupuesto.msgConfirmarRechazar"),
                nroOT, lblTotal.Text, dtpFecha.Value.ToShortDateString());
            if (MessageBox.Show(pregunta, bll_idioma.Traducir("ABM.Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                BE_OTResumen ot = bll_presupuesto.RegistrarRespuesta(nroOT, aprobado, dtpFecha.Value);
                string mensaje = string.Format(
                    bll_idioma.Traducir(aprobado ? "FrmPresupuesto.msgOkAprobado" : "FrmPresupuesto.msgOkRechazado"),
                    ot.NroOT, ot.Estado);
                MessageBox.Show(mensaje, bll_idioma.Traducir("ABM.Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void MostrarError(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir("ABM.Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void ActualizarIdioma()
        {
            Text = bll_idioma.Traducir("FrmPresupuesto.Form");
            lblTitulo.Text = bll_idioma.Traducir("FrmPresupuesto.lblTitulo");
            lblPasoDiagnostico.Text = bll_idioma.Traducir("FrmPresupuesto.lblPasoDiagnostico");
            lblPasoDetalle.Text = bll_idioma.Traducir("FrmPresupuesto.lblPasoDetalle");
            lblTitManoObra.Text = bll_idioma.Traducir("FrmPresupuesto.lblManoObra");
            lblTitTotal.Text = bll_idioma.Traducir("FrmPresupuesto.lblTotal");
            lblPasoRespuesta.Text = bll_idioma.Traducir("FrmPresupuesto.lblPasoRespuesta");
            lblFecha.Text = bll_idioma.Traducir("FrmPresupuesto.lblFecha");
            btnAprobar.Text = bll_idioma.Traducir("FrmPresupuesto.btnAprobar");
            btnRechazar.Text = bll_idioma.Traducir("FrmPresupuesto.btnRechazar");
            btnCancelar.Text = bll_idioma.Traducir("ABM.btnCancelar");

            if (grilla.Columns.Count > 0)
            {
                grilla.Columns["colCodigo"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colCodigo");
                grilla.Columns["colDescripcion"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colDescripcion");
                grilla.Columns["colPrecio"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colPrecio");
                grilla.Columns["colCantidad"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colCantidad");
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
