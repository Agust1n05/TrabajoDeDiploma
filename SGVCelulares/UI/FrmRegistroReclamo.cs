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
    public partial class FrmRegistroReclamo : Form, IObservadorIdioma
    {
        private static readonly Color ColorOk = Color.FromArgb(74, 222, 128);
        private static readonly Color ColorAviso = Color.FromArgb(251, 191, 36);
        private static readonly Color ColorNeutro = Color.FromArgb(148, 163, 184);

        private readonly BLL_Reclamo bll_reclamo = new BLL_Reclamo();
        private readonly BLL_Cliente bll_cliente = new BLL_Cliente();
        private readonly BLL_Idioma bll_idioma = new BLL_Idioma();

        private BE_Cliente? cliente;

        /// <summary>N° del reclamo registrado; vale 0 si se canceló.</summary>
        public int NroReclamoCreado { get; private set; }

        public FrmRegistroReclamo()
        {
            InitializeComponent();
            bll_idioma.Suscribir(this);
        }

        private void FrmRegistroReclamo_Load(object sender, EventArgs e)
        {
            ActualizarIdioma();
            ReiniciarCliente();
            ActualizarContador();
            txtDni.Focus();
        }

        private BE_ReparacionConGarantia? ReparacionSeleccionada => cboReparacion.SelectedItem as BE_ReparacionConGarantia;

        // ---------------- Paso 1: cliente ----------------

        private void txtDni_TextChanged(object sender, EventArgs e) => ReiniciarCliente();

        private void txtDni_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            BuscarCliente();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e) => BuscarCliente();

        private void BuscarCliente()
        {
            string dni = txtDni.Text.Trim();
            if (!Regex.IsMatch(dni, @"^\d{7,8}$"))
            {
                MostrarAtencion(bll_idioma.Traducir("FrmRegistroReclamo.msgDniInvalido"));
                txtDni.Focus();
                return;
            }

            List<BE_ReparacionConGarantia> reparaciones;
            try
            {
                cliente = bll_cliente.ConsultarPorDni(dni);
                reparaciones = cliente != null ? bll_reclamo.ConsultarReparacionesConGarantia(dni) : new List<BE_ReparacionConGarantia>();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
                return;
            }

            if (cliente == null)
            {
                MostrarEstado(lblClienteEstado, bll_idioma.Traducir("FrmRegistroReclamo.msgClienteNoRegistrado"), ColorAviso);
                return;
            }

            lblClienteNombre.Text = $"{cliente.Apellido}, {cliente.Nombre}";
            cboReparacion.DataSource = reparaciones;
            MostrarReparacion(ReparacionSeleccionada);

            if (reparaciones.Count == 0)
            {
                // Paso 1 alternativo: no hay una reparación cubierta, el proceso termina.
                MostrarEstado(lblClienteEstado, bll_idioma.Traducir("FrmRegistroReclamo.msgSinReparaciones"), ColorAviso);
                return;
            }

            MostrarEstado(lblClienteEstado,
                string.Format(bll_idioma.Traducir("FrmRegistroReclamo.msgClienteRegistrado"), reparaciones.Count), ColorOk);
            cboReparacion.Focus();
        }

        private void ReiniciarCliente()
        {
            cliente = null;
            lblClienteNombre.Text = "";
            MostrarEstado(lblClienteEstado, bll_idioma.Traducir("FrmRegistroReclamo.msgIngreseDni"), ColorNeutro);
            cboReparacion.DataSource = null;
            MostrarReparacion(null);
        }

        // ---------------- Paso 2: reparación original ----------------

        private void cboReparacion_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is BE_ReparacionConGarantia r)
                e.Value = string.Format(bll_idioma.Traducir("FrmRegistroReclamo.formatoReparacion"),
                                        r.NroOT, r.Equipo, r.FechaVencimiento.ToShortDateString());
        }

        private void cboReparacion_SelectedIndexChanged(object sender, EventArgs e) => MostrarReparacion(ReparacionSeleccionada);

        /// <summary>Muestra la falla original y si la garantía está vigente (la decisión final la toma el sistema al registrar).</summary>
        private void MostrarReparacion(BE_ReparacionConGarantia? reparacion)
        {
            if (reparacion == null)
            {
                lblFallaOriginal.Text = "-";
                MostrarEstado(lblVigencia, "-", ColorNeutro);
                return;
            }

            lblFallaOriginal.Text = $"{reparacion.DescripcionFalla} (IMEI {reparacion.Imei})";
            string vencimiento = reparacion.FechaVencimiento.ToShortDateString();
            if (new BE_Garantia(reparacion.NroOT, reparacion.FechaVencimiento).EstaVigente(DateTime.Today))
                MostrarEstado(lblVigencia, string.Format(bll_idioma.Traducir("FrmRegistroReclamo.msgGarantiaVigente"), vencimiento), ColorOk);
            else
                MostrarEstado(lblVigencia, string.Format(bll_idioma.Traducir("FrmRegistroReclamo.msgGarantiaVencida"), vencimiento), ColorAviso);
        }

        // ---------------- Paso 3: reclamo ----------------

        private void txtDescripcion_TextChanged(object sender, EventArgs e) => ActualizarContador();

        private void ActualizarContador() =>
            lblContador.Text = $"{txtDescripcion.TextLength} / {txtDescripcion.MaxLength}";

        // ---------------- Registro ----------------

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (cliente == null)
            {
                MostrarAtencion(bll_idioma.Traducir("FrmRegistroReclamo.msgFaltaCliente"));
                txtDni.Focus();
                return;
            }
            BE_ReparacionConGarantia? reparacion = ReparacionSeleccionada;
            if (reparacion == null)
            {
                MostrarAtencion(bll_idioma.Traducir("FrmRegistroReclamo.msgFaltaReparacion"));
                return;
            }

            try
            {
                BE_ReclamoResumen reclamo = bll_reclamo.RegistrarReclamo(cliente.Dni, reparacion.NroOT, txtMotivo.Text, txtDescripcion.Text);
                NroReclamoCreado = reclamo.NroReclamo;

                string mensaje = reclamo.Estado == EstadosReclamo.Rechazado
                    ? string.Format(bll_idioma.Traducir("FrmRegistroReclamo.msgReclamoRechazado"),
                                    reclamo.NroReclamo, reclamo.Estado, reclamo.FechaVencimiento.ToShortDateString())
                    : string.Format(bll_idioma.Traducir("FrmRegistroReclamo.msgReclamoEnEvaluacion"),
                                    reclamo.NroReclamo, reclamo.Estado, reclamo.Tecnico);
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

        private static void MostrarEstado(Label etiqueta, string texto, Color color)
        {
            etiqueta.Text = texto;
            etiqueta.ForeColor = color;
        }

        private void MostrarAtencion(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir("ABM.Atencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private void MostrarError(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir("ABM.Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void ActualizarIdioma()
        {
            Text = bll_idioma.Traducir("FrmRegistroReclamo.Form");
            lblTitulo.Text = bll_idioma.Traducir("FrmRegistroReclamo.lblTitulo");
            lblPasoCliente.Text = bll_idioma.Traducir("FrmRegistroReclamo.lblPasoCliente");
            lblPasoReparacion.Text = bll_idioma.Traducir("FrmRegistroReclamo.lblPasoReparacion");
            lblPasoReclamo.Text = bll_idioma.Traducir("FrmRegistroReclamo.lblPasoReclamo");
            lblDni.Text = bll_idioma.Traducir("FrmRegistroReclamo.lblDni");
            lblReparacion.Text = bll_idioma.Traducir("FrmRegistroReclamo.lblReparacion");
            lblTitFalla.Text = bll_idioma.Traducir("FrmRegistroReclamo.lblFallaOriginal");
            lblMotivo.Text = bll_idioma.Traducir("FrmRegistroReclamo.lblMotivo");
            lblDescripcion.Text = bll_idioma.Traducir("FrmRegistroReclamo.lblDescripcion");
            btnBuscarCliente.Text = bll_idioma.Traducir("FrmRegistroReclamo.btnBuscar");
            btnRegistrar.Text = bll_idioma.Traducir("FrmRegistroReclamo.btnRegistrar");
            btnCancelar.Text = bll_idioma.Traducir("ABM.btnCancelar");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}
