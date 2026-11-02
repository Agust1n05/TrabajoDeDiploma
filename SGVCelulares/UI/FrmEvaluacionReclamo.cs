using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using Servicio;

namespace UI
{
    /// <summary>
    /// CU-NEG-8 Evaluar reclamo. El técnico revisa el reclamo y la reparación original, registra si la falla
    /// está cubierta por la garantía y el sistema pasa el reclamo a "Aprobado" o "Rechazado".
    /// </summary>
    public partial class FrmEvaluacionReclamo : Form, IObservadorIdioma
    {
        private readonly BLL_Reclamo bll_reclamo = new BLL_Reclamo();
        private readonly BLL_Idioma bll_idioma = new BLL_Idioma();
        private readonly int nroReclamo;

        public FrmEvaluacionReclamo(int nroReclamo)
        {
            InitializeComponent();
            this.nroReclamo = nroReclamo;
            bll_idioma.Suscribir(this);
        }

        private void FrmEvaluacionReclamo_Load(object sender, EventArgs e)
        {
            ActualizarIdioma();
            ActualizarContador();

            try
            {
                BE_ReclamoResumen reclamo = bll_reclamo.ObtenerReclamoAccesible(nroReclamo);
                BE_ReparacionOriginal original = bll_reclamo.ConsultarReparacionOriginal(nroReclamo);

                lblReclamo.Text = string.Format(bll_idioma.Traducir("FrmEvaluacionReclamo.formatoReclamo"),
                                                reclamo.NroReclamo, reclamo.FechaReclamo.ToShortDateString(), reclamo.Estado);
                lblCliente.Text = reclamo.Cliente;
                lblEquipo.Text = $"{reclamo.Equipo}  ·  IMEI {reclamo.Imei}";
                lblMotivo.Text = reclamo.Motivo;
                lblDescripcion.Text = reclamo.Descripcion;
                lblGarantia.Text = string.Format(bll_idioma.Traducir("FrmEvaluacionReclamo.formatoGarantia"),
                                                 reclamo.NroOT, reclamo.FechaVencimiento.ToShortDateString());

                lblFalla.Text = original.DescripcionFalla;
                lblDiagnostico.Text = original.DetalleDiagnostico;
                txtRepuestos.Text = original.Repuestos.Count == 0
                    ? bll_idioma.Traducir("FrmEvaluacionReclamo.msgSinRepuestos")
                    : string.Join(Environment.NewLine, original.Repuestos.Select(r => $"• {r.Codigo} - {r.Descripcion}: {r.CantidadUtilizada}"));

                if (reclamo.Estado != EstadosReclamo.EnEvaluacion)
                {
                    btnConfirmar.Enabled = false;
                    MostrarError(string.Format(bll_idioma.Traducir("FrmEvaluacionReclamo.msgEstadoInvalido"), reclamo.NroReclamo, reclamo.Estado));
                }
            }
            catch (Exception ex)
            {
                btnConfirmar.Enabled = false;
                MostrarError(ex.Message);
            }
        }

        private void txtDetalle_TextChanged(object sender, EventArgs e) => ActualizarContador();

        private void ActualizarContador() =>
            lblContador.Text = $"{txtDetalle.TextLength} / {txtDetalle.MaxLength}";

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            bool? cubierta = rbCubiertaSi.Checked ? true : rbCubiertaNo.Checked ? false : null;

            // Si no eligió Sí/No no hay qué confirmar: la BLL informa que falta indicarlo.
            if (cubierta != null)
            {
                string resultado = cubierta.Value ? EstadosReclamo.Aprobado : EstadosReclamo.Rechazado;
                string pregunta = string.Format(bll_idioma.Traducir("FrmEvaluacionReclamo.msgConfirmar"), nroReclamo, resultado);
                if (MessageBox.Show(pregunta, bll_idioma.Traducir("ABM.Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }

            try
            {
                bll_reclamo.EvaluarReclamo(nroReclamo, cubierta, txtDetalle.Text);
                MessageBox.Show(string.Format(bll_idioma.Traducir("FrmEvaluacionReclamo.msgOk"), nroReclamo),
                                bll_idioma.Traducir("ABM.Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Text = bll_idioma.Traducir("FrmEvaluacionReclamo.Form");
            lblTitulo.Text = bll_idioma.Traducir("FrmEvaluacionReclamo.lblTitulo");
            lblPasoReclamo.Text = bll_idioma.Traducir("FrmEvaluacionReclamo.lblPasoReclamo");
            lblPasoReparacion.Text = bll_idioma.Traducir("FrmEvaluacionReclamo.lblPasoReparacion");
            lblPasoEvaluacion.Text = bll_idioma.Traducir("FrmEvaluacionReclamo.lblPasoEvaluacion");
            lblTitReclamo.Text = bll_idioma.Traducir("FrmEvaluacionReclamo.lblTitReclamo");
            lblTitCliente.Text = bll_idioma.Traducir("FrmEvaluacionReclamo.lblTitCliente");
            lblTitEquipo.Text = bll_idioma.Traducir("FrmEvaluacionReclamo.lblTitEquipo");
            lblTitMotivo.Text = bll_idioma.Traducir("FrmRegistroReclamo.lblMotivo");
            lblTitDescripcion.Text = bll_idioma.Traducir("FrmEvaluacionReclamo.lblTitDescripcion");
            lblTitGarantia.Text = bll_idioma.Traducir("FrmEvaluacionReclamo.lblTitGarantia");
            lblTitFalla.Text = bll_idioma.Traducir("FrmEvaluacionReclamo.lblTitFalla");
            lblTitDiagnostico.Text = bll_idioma.Traducir("FrmEvaluacionReclamo.lblTitDiagnostico");
            lblTitRepuestos.Text = bll_idioma.Traducir("FrmEvaluacionReclamo.lblTitRepuestos");
            lblCubierta.Text = bll_idioma.Traducir("FrmEvaluacionReclamo.lblCubierta");
            rbCubiertaSi.Text = bll_idioma.Traducir("FrmEvaluacionReclamo.rbSi");
            rbCubiertaNo.Text = bll_idioma.Traducir("FrmEvaluacionReclamo.rbNo");
            lblDetalle.Text = bll_idioma.Traducir("FrmEvaluacionReclamo.lblDetalle");
            btnConfirmar.Text = bll_idioma.Traducir("FrmEvaluacionReclamo.btnConfirmar");
            btnCancelar.Text = bll_idioma.Traducir("ABM.btnCancelar");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}
