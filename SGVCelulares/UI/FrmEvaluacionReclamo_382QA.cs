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
    public partial class FrmEvaluacionReclamo_382QA : Form, IObservadorIdioma_382QA
    {
        private readonly BLL_Reclamo_382QA bll_reclamo = new BLL_Reclamo_382QA();
        private readonly BLL_Idioma_382QA bll_idioma = new BLL_Idioma_382QA();
        private readonly int nroReclamo;

        public FrmEvaluacionReclamo_382QA(int nroReclamo)
        {
            InitializeComponent();
            this.nroReclamo = nroReclamo;
            bll_idioma.Suscribir_382QA(this);
        }

        private void FrmEvaluacionReclamo_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_382QA();
            ActualizarContador_382QA();

            try
            {
                BE_ReclamoResumen_382QA reclamo = bll_reclamo.ObtenerReclamoAccesible_382QA(nroReclamo);
                BE_ReparacionOriginal_382QA original = bll_reclamo.ConsultarReparacionOriginal_382QA(nroReclamo);

                lblReclamo.Text = string.Format(bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.formatoReclamo"),
                                                reclamo.NroReclamo, reclamo.FechaReclamo.ToShortDateString(), reclamo.Estado);
                lblCliente.Text = reclamo.Cliente;
                lblEquipo.Text = $"{reclamo.Equipo}  ·  IMEI {reclamo.Imei}";
                lblMotivo.Text = reclamo.Motivo;
                lblDescripcion.Text = reclamo.Descripcion;
                lblGarantia.Text = string.Format(bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.formatoGarantia"),
                                                 reclamo.NroOT, reclamo.FechaVencimiento.ToShortDateString());

                lblFalla.Text = original.DescripcionFalla;
                lblDiagnostico.Text = original.DetalleDiagnostico;
                txtRepuestos.Text = original.Repuestos.Count == 0
                    ? bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.msgSinRepuestos")
                    : string.Join(Environment.NewLine, original.Repuestos.Select(r => $"• {r.Codigo} - {r.Descripcion}: {r.CantidadUtilizada}"));

                if (reclamo.Estado != EstadosReclamo_382QA.EnEvaluacion)
                {
                    btnConfirmar.Enabled = false;
                    MostrarError_382QA(string.Format(bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.msgEstadoInvalido"), reclamo.NroReclamo, reclamo.Estado));
                }
            }
            catch (Exception ex)
            {
                btnConfirmar.Enabled = false;
                MostrarError_382QA(ex.Message);
            }
        }

        private void txtDetalle_TextChanged(object sender, EventArgs e) => ActualizarContador_382QA();

        private void ActualizarContador_382QA() =>
            lblContador.Text = $"{txtDetalle.TextLength} / {txtDetalle.MaxLength}";

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            bool? cubierta = rbCubiertaSi.Checked ? true : rbCubiertaNo.Checked ? false : null;

            // Si no eligió Sí/No no hay qué confirmar: la BLL informa que falta indicarlo.
            if (cubierta != null)
            {
                string resultado = cubierta.Value ? EstadosReclamo_382QA.Aprobado : EstadosReclamo_382QA.Rechazado;
                string pregunta = string.Format(bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.msgConfirmar"), nroReclamo, resultado);
                if (MessageBox.Show(pregunta, bll_idioma.Traducir_382QA("ABM.Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }

            try
            {
                bll_reclamo.EvaluarReclamo_382QA(nroReclamo, cubierta, txtDetalle.Text);
                MessageBox.Show(string.Format(bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.msgOk"), nroReclamo),
                                bll_idioma.Traducir_382QA("ABM.Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Text = bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.Form");
            lblTitulo.Text = bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.lblTitulo");
            lblPasoReclamo.Text = bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.lblPasoReclamo");
            lblPasoReparacion.Text = bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.lblPasoReparacion");
            lblPasoEvaluacion.Text = bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.lblPasoEvaluacion");
            lblTitReclamo.Text = bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.lblTitReclamo");
            lblTitCliente.Text = bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.lblTitCliente");
            lblTitEquipo.Text = bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.lblTitEquipo");
            lblTitMotivo.Text = bll_idioma.Traducir_382QA("FrmRegistroReclamo.lblMotivo");
            lblTitDescripcion.Text = bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.lblTitDescripcion");
            lblTitGarantia.Text = bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.lblTitGarantia");
            lblTitFalla.Text = bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.lblTitFalla");
            lblTitDiagnostico.Text = bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.lblTitDiagnostico");
            lblTitRepuestos.Text = bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.lblTitRepuestos");
            lblCubierta.Text = bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.lblCubierta");
            rbCubiertaSi.Text = bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.rbSi");
            rbCubiertaNo.Text = bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.rbNo");
            lblDetalle.Text = bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.lblDetalle");
            btnConfirmar.Text = bll_idioma.Traducir_382QA("FrmEvaluacionReclamo.btnConfirmar");
            btnCancelar.Text = bll_idioma.Traducir_382QA("ABM.btnCancelar");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_382QA(this);
            base.OnFormClosed(e);
        }
    }
}
