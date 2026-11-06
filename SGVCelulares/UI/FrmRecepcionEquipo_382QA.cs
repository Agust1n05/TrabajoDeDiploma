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
    public partial class FrmRecepcionEquipo_382QA : Form, IObservadorIdioma_382QA
    {
        private static readonly Color ColorOk = Color.FromArgb(74, 222, 128);
        private static readonly Color ColorAviso = Color.FromArgb(251, 191, 36);
        private static readonly Color ColorNeutro = Color.FromArgb(148, 163, 184);

        private readonly BLL_OT_382QA bll_ot = new BLL_OT_382QA();
        private readonly BLL_Cliente_382QA bll_cliente = new BLL_Cliente_382QA();
        private readonly BLL_Equipo_382QA bll_equipo = new BLL_Equipo_382QA();
        private readonly BLL_Idioma_382QA bll_idioma = new BLL_Idioma_382QA();

        private BE_Cliente_382QA? cliente;
        private bool equipoBuscado;

        /// <summary>N° de la OT generada; vale 0 si se canceló la recepción.</summary>
        public int NroOTCreada { get; private set; }

        public FrmRecepcionEquipo_382QA()
        {
            InitializeComponent();
            bll_idioma.Suscribir_382QA(this);
        }

        private void FrmRecepcionEquipo_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_382QA();
            ReiniciarCliente_382QA();
            ReiniciarEquipo_382QA();
            ActualizarContador_382QA();
            txtDni.Focus();
        }

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
                MostrarAtencion_382QA(bll_idioma.Traducir_382QA("FrmRecepcionEquipo.msgDniInvalido"));
                txtDni.Focus();
                return;
            }

            try
            {
                cliente = bll_cliente.ConsultarPorDni_382QA(dni);
            }
            catch (Exception ex)
            {
                MostrarError_382QA(ex.Message);
                return;
            }

            if (cliente != null)
            {
                lblClienteNombre.Text = $"{cliente.Apellido}, {cliente.Nombre}";
                MostrarEstado_382QA(lblClienteEstado,
                    string.Format(bll_idioma.Traducir_382QA("FrmRecepcionEquipo.msgClienteRegistrado"), cliente.Telefono), ColorOk);
                btnRegistrarCliente.Visible = false;
                txtImei.Focus();
            }
            else
            {
                lblClienteNombre.Text = "";
                MostrarEstado_382QA(lblClienteEstado, bll_idioma.Traducir_382QA("FrmRecepcionEquipo.msgClienteNoRegistrado"), ColorAviso);
                btnRegistrarCliente.Visible = true;
                btnRegistrarCliente.Focus();
            }
        }

        // CU-NEG-6 Registrar nuevo cliente: se abre el ABM de clientes en modo alta con el DNI cargado.
        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            using var frmClientes = new FrmGestionClientes_382QA(txtDni.Text.Trim());
            if (frmClientes.ShowDialog(this) == DialogResult.OK)
                BuscarCliente_382QA();
        }

        private void ReiniciarCliente_382QA()
        {
            cliente = null;
            lblClienteNombre.Text = "";
            MostrarEstado_382QA(lblClienteEstado, bll_idioma.Traducir_382QA("FrmRecepcionEquipo.msgIngreseDni"), ColorNeutro);
            btnRegistrarCliente.Visible = false;
        }

        // ---------------- Paso 2: equipo ----------------

        private void txtImei_TextChanged(object sender, EventArgs e) => ReiniciarEquipo_382QA();

        private void txtImei_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            BuscarEquipo_382QA();
        }

        private void btnBuscarEquipo_Click(object sender, EventArgs e) => BuscarEquipo_382QA();

        private void BuscarEquipo_382QA()
        {
            string imei = txtImei.Text.Trim();
            if (!Regex.IsMatch(imei, @"^\d{15}$"))
            {
                MostrarAtencion_382QA(bll_idioma.Traducir_382QA("FrmRecepcionEquipo.msgImeiInvalido"));
                txtImei.Focus();
                return;
            }

            BE_Equipo_382QA? equipo;
            int otAbierta;
            try
            {
                equipo = bll_equipo.ConsultarPorImei_382QA(imei);
                otAbierta = equipo != null ? bll_ot.ObtenerOTAbiertaDelEquipo_382QA(equipo.IdEquipo) : 0;
            }
            catch (Exception ex)
            {
                MostrarError_382QA(ex.Message);
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
                    MostrarEstado_382QA(lblEquipoEstado,
                        string.Format(bll_idioma.Traducir_382QA("FrmRecepcionEquipo.msgEquipoConOT"), otAbierta), ColorAviso);
                else
                    MostrarEstado_382QA(lblEquipoEstado, bll_idioma.Traducir_382QA("FrmRecepcionEquipo.msgEquipoRegistrado"), ColorOk);
                txtFalla.Focus();
            }
            else
            {
                txtMarca.Text = "";
                txtModelo.Text = "";
                txtMarca.ReadOnly = false;
                txtModelo.ReadOnly = false;
                MostrarEstado_382QA(lblEquipoEstado, bll_idioma.Traducir_382QA("FrmRecepcionEquipo.msgEquipoNuevo"), ColorAviso);
                txtMarca.Focus();
            }
        }

        private void ReiniciarEquipo_382QA()
        {
            equipoBuscado = false;
            txtMarca.Text = "";
            txtModelo.Text = "";
            txtMarca.ReadOnly = true;
            txtModelo.ReadOnly = true;
            MostrarEstado_382QA(lblEquipoEstado, bll_idioma.Traducir_382QA("FrmRecepcionEquipo.msgIngreseImei"), ColorNeutro);
        }

        // ---------------- Paso 3: falla ----------------

        private void txtFalla_TextChanged(object sender, EventArgs e) => ActualizarContador_382QA();

        private void ActualizarContador_382QA() =>
            lblContador.Text = $"{txtFalla.TextLength} / {txtFalla.MaxLength}";

        // ---------------- Recepción ----------------

        private void btnRecepcionar_Click(object sender, EventArgs e)
        {
            if (cliente == null)
            {
                MostrarAtencion_382QA(bll_idioma.Traducir_382QA("FrmRecepcionEquipo.msgFaltaCliente"));
                txtDni.Focus();
                return;
            }
            if (!equipoBuscado)
            {
                MostrarAtencion_382QA(bll_idioma.Traducir_382QA("FrmRecepcionEquipo.msgFaltaEquipo"));
                txtImei.Focus();
                return;
            }

            try
            {
                BE_OTResumen_382QA ot = bll_ot.RecepcionarEquipo_382QA(cliente.Dni, txtImei.Text, txtMarca.Text, txtModelo.Text, txtFalla.Text);
                NroOTCreada = ot.NroOT;

                MessageBox.Show(
                    string.Format(bll_idioma.Traducir_382QA("FrmRecepcionEquipo.msgRecepcionOk"), ot.NroOT, ot.Estado, ot.Tecnico),
                    bll_idioma.Traducir_382QA("ABM.Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ClienteNoRegistradoException_382QA ex)
            {
                // El cliente se dio de baja mientras se cargaba la recepción
                MostrarError_382QA(ex.Message);
                BuscarCliente_382QA();
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
            Text = bll_idioma.Traducir_382QA("FrmRecepcionEquipo.Form");
            lblTitulo.Text = bll_idioma.Traducir_382QA("FrmRecepcionEquipo.lblTitulo");
            lblPasoCliente.Text = bll_idioma.Traducir_382QA("FrmRecepcionEquipo.lblPasoCliente");
            lblPasoEquipo.Text = bll_idioma.Traducir_382QA("FrmRecepcionEquipo.lblPasoEquipo");
            lblPasoFalla.Text = bll_idioma.Traducir_382QA("FrmRecepcionEquipo.lblPasoFalla");
            lblDni.Text = bll_idioma.Traducir_382QA("FrmRecepcionEquipo.lblDni");
            lblImei.Text = bll_idioma.Traducir_382QA("FrmRecepcionEquipo.lblImei");
            lblMarca.Text = bll_idioma.Traducir_382QA("FrmRecepcionEquipo.lblMarca");
            lblModelo.Text = bll_idioma.Traducir_382QA("FrmRecepcionEquipo.lblModelo");
            btnBuscarCliente.Text = bll_idioma.Traducir_382QA("FrmRecepcionEquipo.btnBuscar");
            btnBuscarEquipo.Text = bll_idioma.Traducir_382QA("FrmRecepcionEquipo.btnBuscar");
            btnRegistrarCliente.Text = bll_idioma.Traducir_382QA("FrmRecepcionEquipo.btnRegistrarCliente");
            btnRecepcionar.Text = bll_idioma.Traducir_382QA("FrmRecepcionEquipo.btnRecepcionar");
            btnCancelar.Text = bll_idioma.Traducir_382QA("ABM.btnCancelar");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_382QA(this);
            base.OnFormClosed(e);
        }
    }
}
