using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using Servicio;
using System.Text.RegularExpressions;

namespace UI
{
    /// <summary>
    /// CU-NEG-7 Registrar reclamo de garantía, en tres pasos: cliente, reparación original y datos del reclamo.
    /// La vigencia de la garantía y el técnico los resuelve el sistema al registrar.
    /// </summary>
    public partial class FrmRegistroReclamo_382QA : Form, IObservadorIdioma_382QA
    {
        private static readonly Color ColorOk = Color.FromArgb(74, 222, 128);
        private static readonly Color ColorAviso = Color.FromArgb(251, 191, 36);
        private static readonly Color ColorNeutro = Color.FromArgb(148, 163, 184);

        private readonly BLL_Reclamo_382QA bll_reclamo = new BLL_Reclamo_382QA();
        private readonly BLL_Cliente_382QA bll_cliente = new BLL_Cliente_382QA();
        private readonly BLL_Idioma_382QA bll_idioma = new BLL_Idioma_382QA();

        private BE_Cliente_382QA? cliente;

        /// <summary>N° del reclamo registrado; vale 0 si se canceló.</summary>
        public int NroReclamoCreado { get; private set; }

        public FrmRegistroReclamo_382QA()
        {
            InitializeComponent();
            bll_idioma.Suscribir_382QA(this);
        }

        private void FrmRegistroReclamo_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_382QA();
            ReiniciarCliente_382QA();
            ActualizarContador_382QA();
            txtDni.Focus();
        }

        private BE_ReparacionConGarantia_382QA? ReparacionSeleccionada => cboReparacion.SelectedItem as BE_ReparacionConGarantia_382QA;

        // ---------------- Paso 1: cliente ----------------

        private void txtDni_TextChanged(object sender, EventArgs e) => ReiniciarCliente_382QA();

        private void txtDni_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            BuscarCliente_382QA();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e) => BuscarCliente_382QA();

        private void BuscarCliente_382QA()
        {
            string dni = txtDni.Text.Trim();
            if (!Regex.IsMatch(dni, @"^\d{7,8}$"))
            {
                MostrarAtencion_382QA(bll_idioma.Traducir_382QA("FrmRegistroReclamo.msgDniInvalido"));
                txtDni.Focus();
                return;
            }

            List<BE_ReparacionConGarantia_382QA> reparaciones;
            try
            {
                cliente = bll_cliente.ConsultarPorDni_382QA(dni);
                reparaciones = cliente != null ? bll_reclamo.ConsultarReparacionesConGarantia_382QA(dni) : new List<BE_ReparacionConGarantia_382QA>();
            }
            catch (Exception ex)
            {
                MostrarError_382QA(ex.Message);
                return;
            }

            if (cliente == null)
            {
                MostrarEstado_382QA(lblClienteEstado, bll_idioma.Traducir_382QA("FrmRegistroReclamo.msgClienteNoRegistrado"), ColorAviso);
                return;
            }

            lblClienteNombre.Text = $"{cliente.Apellido}, {cliente.Nombre}";
            cboReparacion.DataSource = reparaciones;
            MostrarReparacion_382QA(ReparacionSeleccionada);

            if (reparaciones.Count == 0)
            {
                // Paso 1 alternativo: no hay una reparación cubierta, el proceso termina.
                MostrarEstado_382QA(lblClienteEstado, bll_idioma.Traducir_382QA("FrmRegistroReclamo.msgSinReparaciones"), ColorAviso);
                return;
            }

            MostrarEstado_382QA(lblClienteEstado,
                string.Format(bll_idioma.Traducir_382QA("FrmRegistroReclamo.msgClienteRegistrado"), reparaciones.Count), ColorOk);
            cboReparacion.Focus();
        }

        private void ReiniciarCliente_382QA()
        {
            cliente = null;
            lblClienteNombre.Text = "";
            MostrarEstado_382QA(lblClienteEstado, bll_idioma.Traducir_382QA("FrmRegistroReclamo.msgIngreseDni"), ColorNeutro);
            cboReparacion.DataSource = null;
            MostrarReparacion_382QA(null);
        }

        // ---------------- Paso 2: reparación original ----------------

        private void cboReparacion_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is BE_ReparacionConGarantia_382QA r)
                e.Value = string.Format(bll_idioma.Traducir_382QA("FrmRegistroReclamo.formatoReparacion"),
                                        r.NroOT, r.Equipo, r.FechaVencimiento.ToShortDateString());
        }

        private void cboReparacion_SelectedIndexChanged(object sender, EventArgs e) => MostrarReparacion_382QA(ReparacionSeleccionada);

        /// <summary>Muestra la falla original y si la garantía está vigente (la decisión final la toma el sistema al registrar).</summary>
        private void MostrarReparacion_382QA(BE_ReparacionConGarantia_382QA? reparacion)
        {
            if (reparacion == null)
            {
                lblFallaOriginal.Text = "-";
                MostrarEstado_382QA(lblVigencia, "-", ColorNeutro);
                return;
            }

            lblFallaOriginal.Text = $"{reparacion.DescripcionFalla} (IMEI {reparacion.Imei})";
            string vencimiento = reparacion.FechaVencimiento.ToShortDateString();
            if (new BE_Garantia_382QA(reparacion.NroOT, reparacion.FechaVencimiento).EstaVigente_382QA(DateTime.Today))
                MostrarEstado_382QA(lblVigencia, string.Format(bll_idioma.Traducir_382QA("FrmRegistroReclamo.msgGarantiaVigente"), vencimiento), ColorOk);
            else
                MostrarEstado_382QA(lblVigencia, string.Format(bll_idioma.Traducir_382QA("FrmRegistroReclamo.msgGarantiaVencida"), vencimiento), ColorAviso);
        }

        // ---------------- Paso 3: reclamo ----------------

        private void txtDescripcion_TextChanged(object sender, EventArgs e) => ActualizarContador_382QA();

        private void ActualizarContador_382QA() =>
            lblContador.Text = $"{txtDescripcion.TextLength} / {txtDescripcion.MaxLength}";

        // ---------------- Registro ----------------

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (cliente == null)
            {
                MostrarAtencion_382QA(bll_idioma.Traducir_382QA("FrmRegistroReclamo.msgFaltaCliente"));
                txtDni.Focus();
                return;
            }
            BE_ReparacionConGarantia_382QA? reparacion = ReparacionSeleccionada;
            if (reparacion == null)
            {
                MostrarAtencion_382QA(bll_idioma.Traducir_382QA("FrmRegistroReclamo.msgFaltaReparacion"));
                return;
            }

            try
            {
                BE_ReclamoResumen_382QA reclamo = bll_reclamo.RegistrarReclamo_382QA(cliente.Dni, reparacion.NroOT, txtMotivo.Text, txtDescripcion.Text);
                NroReclamoCreado = reclamo.NroReclamo;

                string mensaje = reclamo.Estado == EstadosReclamo_382QA.Rechazado
                    ? string.Format(bll_idioma.Traducir_382QA("FrmRegistroReclamo.msgReclamoRechazado"),
                                    reclamo.NroReclamo, reclamo.Estado, reclamo.FechaVencimiento.ToShortDateString())
                    : string.Format(bll_idioma.Traducir_382QA("FrmRegistroReclamo.msgReclamoEnEvaluacion"),
                                    reclamo.NroReclamo, reclamo.Estado, reclamo.Tecnico);
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

        private static void MostrarEstado_382QA(Label etiqueta, string texto, Color color)
        {
            etiqueta.Text = texto;
            etiqueta.ForeColor = color;
        }

        private void MostrarAtencion_382QA(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir_382QA("ABM.Atencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private void MostrarError_382QA(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir_382QA("ABM.Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void ActualizarIdioma_382QA()
        {
            Text = bll_idioma.Traducir_382QA("FrmRegistroReclamo.Form");
            lblTitulo.Text = bll_idioma.Traducir_382QA("FrmRegistroReclamo.lblTitulo");
            lblPasoCliente.Text = bll_idioma.Traducir_382QA("FrmRegistroReclamo.lblPasoCliente");
            lblPasoReparacion.Text = bll_idioma.Traducir_382QA("FrmRegistroReclamo.lblPasoReparacion");
            lblPasoReclamo.Text = bll_idioma.Traducir_382QA("FrmRegistroReclamo.lblPasoReclamo");
            lblDni.Text = bll_idioma.Traducir_382QA("FrmRegistroReclamo.lblDni");
            lblReparacion.Text = bll_idioma.Traducir_382QA("FrmRegistroReclamo.lblReparacion");
            lblTitFalla.Text = bll_idioma.Traducir_382QA("FrmRegistroReclamo.lblFallaOriginal");
            lblMotivo.Text = bll_idioma.Traducir_382QA("FrmRegistroReclamo.lblMotivo");
            lblDescripcion.Text = bll_idioma.Traducir_382QA("FrmRegistroReclamo.lblDescripcion");
            btnBuscarCliente.Text = bll_idioma.Traducir_382QA("FrmRegistroReclamo.btnBuscar");
            btnRegistrar.Text = bll_idioma.Traducir_382QA("FrmRegistroReclamo.btnRegistrar");
            btnCancelar.Text = bll_idioma.Traducir_382QA("ABM.btnCancelar");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_382QA(this);
            base.OnFormClosed(e);
        }
    }
}
