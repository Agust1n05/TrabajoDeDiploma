using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using Servicio;
using System.Text.RegularExpressions;

namespace UI
{
    /// <summary>
    /// CU-NEG-1 Gestionar recepción de equipo, en tres pasos: cliente, equipo y falla.
    /// Si el DNI no está registrado se abre Gestión de Clientes en modo alta (extend CU-NEG-6).
    /// </summary>
    public partial class FrmRecepcionEquipo : Form, IObservadorIdioma
    {
        private static readonly Color ColorOk = Color.FromArgb(74, 222, 128);
        private static readonly Color ColorAviso = Color.FromArgb(251, 191, 36);
        private static readonly Color ColorNeutro = Color.FromArgb(148, 163, 184);

        private readonly BLL_OT bll_ot = new BLL_OT();
        private readonly BLL_Cliente bll_cliente = new BLL_Cliente();
        private readonly BLL_Equipo bll_equipo = new BLL_Equipo();
        private readonly BLL_Idioma bll_idioma = new BLL_Idioma();

        private BE_Cliente? cliente;
        private bool equipoBuscado;

        /// <summary>N° de la OT generada; vale 0 si se canceló la recepción.</summary>
        public int NroOTCreada { get; private set; }

        public FrmRecepcionEquipo()
        {
            InitializeComponent();
            bll_idioma.Suscribir(this);
        }

        private void FrmRecepcionEquipo_Load(object sender, EventArgs e)
        {
            ActualizarIdioma();
            ReiniciarCliente();
            ReiniciarEquipo();
            ActualizarContador();
            txtDni.Focus();
        }

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
                MostrarAtencion(bll_idioma.Traducir("FrmRecepcionEquipo.msgDniInvalido"));
                txtDni.Focus();
                return;
            }

            try
            {
                cliente = bll_cliente.ConsultarPorDni(dni);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
                return;
            }

            if (cliente != null)
            {
                lblClienteNombre.Text = $"{cliente.Apellido}, {cliente.Nombre}";
                MostrarEstado(lblClienteEstado,
                    string.Format(bll_idioma.Traducir("FrmRecepcionEquipo.msgClienteRegistrado"), cliente.Telefono), ColorOk);
                btnRegistrarCliente.Visible = false;
                txtImei.Focus();
            }
            else
            {
                lblClienteNombre.Text = "";
                MostrarEstado(lblClienteEstado, bll_idioma.Traducir("FrmRecepcionEquipo.msgClienteNoRegistrado"), ColorAviso);
                btnRegistrarCliente.Visible = true;
                btnRegistrarCliente.Focus();
            }
        }

        // CU-NEG-6 Registrar nuevo cliente: se abre el ABM de clientes en modo alta con el DNI cargado.
        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            using var frmClientes = new FrmGestionClientes(txtDni.Text.Trim());
            if (frmClientes.ShowDialog(this) == DialogResult.OK)
                BuscarCliente();
        }

        private void ReiniciarCliente()
        {
            cliente = null;
            lblClienteNombre.Text = "";
            MostrarEstado(lblClienteEstado, bll_idioma.Traducir("FrmRecepcionEquipo.msgIngreseDni"), ColorNeutro);
            btnRegistrarCliente.Visible = false;
        }

        // ---------------- Paso 2: equipo ----------------

        private void txtImei_TextChanged(object sender, EventArgs e) => ReiniciarEquipo();

        private void txtImei_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            BuscarEquipo();
        }

        private void btnBuscarEquipo_Click(object sender, EventArgs e) => BuscarEquipo();

        private void BuscarEquipo()
        {
            string imei = txtImei.Text.Trim();
            if (!Regex.IsMatch(imei, @"^\d{15}$"))
            {
                MostrarAtencion(bll_idioma.Traducir("FrmRecepcionEquipo.msgImeiInvalido"));
                txtImei.Focus();
                return;
            }

            BE_Equipo? equipo;
            int otAbierta;
            try
            {
                equipo = bll_equipo.ConsultarPorImei(imei);
                otAbierta = equipo != null ? bll_ot.ObtenerOTAbiertaDelEquipo(equipo.IdEquipo) : 0;
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
                return;
            }

            equipoBuscado = true;
            if (equipo != null)
            {
                // Equipo registrado: marca y modelo vienen del maestro y no se editan.
                txtMarca.Text = equipo.Marca;
                txtModelo.Text = equipo.Modelo;
                txtMarca.ReadOnly = true;
                txtModelo.ReadOnly = true;

                if (otAbierta > 0)
                    MostrarEstado(lblEquipoEstado,
                        string.Format(bll_idioma.Traducir("FrmRecepcionEquipo.msgEquipoConOT"), otAbierta), ColorAviso);
                else
                    MostrarEstado(lblEquipoEstado, bll_idioma.Traducir("FrmRecepcionEquipo.msgEquipoRegistrado"), ColorOk);
                txtFalla.Focus();
            }
            else
            {
                txtMarca.Text = "";
                txtModelo.Text = "";
                txtMarca.ReadOnly = false;
                txtModelo.ReadOnly = false;
                MostrarEstado(lblEquipoEstado, bll_idioma.Traducir("FrmRecepcionEquipo.msgEquipoNuevo"), ColorAviso);
                txtMarca.Focus();
            }
        }

        private void ReiniciarEquipo()
        {
            equipoBuscado = false;
            txtMarca.Text = "";
            txtModelo.Text = "";
            txtMarca.ReadOnly = true;
            txtModelo.ReadOnly = true;
            MostrarEstado(lblEquipoEstado, bll_idioma.Traducir("FrmRecepcionEquipo.msgIngreseImei"), ColorNeutro);
        }

        // ---------------- Paso 3: falla ----------------

        private void txtFalla_TextChanged(object sender, EventArgs e) => ActualizarContador();

        private void ActualizarContador() =>
            lblContador.Text = $"{txtFalla.TextLength} / {txtFalla.MaxLength}";

        // ---------------- Recepción ----------------

        private void btnRecepcionar_Click(object sender, EventArgs e)
        {
            if (cliente == null)
            {
                MostrarAtencion(bll_idioma.Traducir("FrmRecepcionEquipo.msgFaltaCliente"));
                txtDni.Focus();
                return;
            }
            if (!equipoBuscado)
            {
                MostrarAtencion(bll_idioma.Traducir("FrmRecepcionEquipo.msgFaltaEquipo"));
                txtImei.Focus();
                return;
            }

            try
            {
                BE_OTResumen ot = bll_ot.RecepcionarEquipo(cliente.Dni, txtImei.Text, txtMarca.Text, txtModelo.Text, txtFalla.Text);
                NroOTCreada = ot.NroOT;

                MessageBox.Show(
                    string.Format(bll_idioma.Traducir("FrmRecepcionEquipo.msgRecepcionOk"), ot.NroOT, ot.Estado, ot.Tecnico),
                    bll_idioma.Traducir("ABM.Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ClienteNoRegistradoException ex)
            {
                // El cliente se dio de baja mientras se cargaba la recepción
                MostrarError(ex.Message);
                BuscarCliente();
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
            Text = bll_idioma.Traducir("FrmRecepcionEquipo.Form");
            lblTitulo.Text = bll_idioma.Traducir("FrmRecepcionEquipo.lblTitulo");
            lblPasoCliente.Text = bll_idioma.Traducir("FrmRecepcionEquipo.lblPasoCliente");
            lblPasoEquipo.Text = bll_idioma.Traducir("FrmRecepcionEquipo.lblPasoEquipo");
            lblPasoFalla.Text = bll_idioma.Traducir("FrmRecepcionEquipo.lblPasoFalla");
            lblDni.Text = bll_idioma.Traducir("FrmRecepcionEquipo.lblDni");
            lblImei.Text = bll_idioma.Traducir("FrmRecepcionEquipo.lblImei");
            lblMarca.Text = bll_idioma.Traducir("FrmRecepcionEquipo.lblMarca");
            lblModelo.Text = bll_idioma.Traducir("FrmRecepcionEquipo.lblModelo");
            btnBuscarCliente.Text = bll_idioma.Traducir("FrmRecepcionEquipo.btnBuscar");
            btnBuscarEquipo.Text = bll_idioma.Traducir("FrmRecepcionEquipo.btnBuscar");
            btnRegistrarCliente.Text = bll_idioma.Traducir("FrmRecepcionEquipo.btnRegistrarCliente");
            btnRecepcionar.Text = bll_idioma.Traducir("FrmRecepcionEquipo.btnRecepcionar");
            btnCancelar.Text = bll_idioma.Traducir("ABM.btnCancelar");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}
