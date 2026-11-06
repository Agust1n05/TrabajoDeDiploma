using BLL.BLL_SER;
using Servicio;

namespace UI
{
    public partial class FrmMenu_382QA : Form, IObservadorIdioma_382QA
    {
        BLL_Usuario_382QA bll_usuario;
        BLL_Idioma_382QA bll_idioma = new BLL_Idioma_382QA();
        bool cargandoIdiomas;
        public FrmMenu_382QA()
        {
            InitializeComponent();
            bll_idioma.Suscribir_382QA(this);
            CargarIdiomas_382QA();
            ActualizarIdioma_382QA();
        }
        private void FrmMenu_Load(object sender, EventArgs e)
        {
            bll_usuario = new BLL_Usuario_382QA();

            SER_Usuario_382QA usuario = SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario;

            btnUsuarios.Visible = TieneAlgun_382QA(usuario, "Crear Usuario", "Modificar Usuario", "Desbloquear Usuario", "Activar Usuario");
            btnGestionRoles.Visible = TieneAlgun_382QA(usuario, "Crear Rol", "Crear Familia", "Eliminar Rol", "Eliminar Familia");
            btnBitacora.Visible = TieneAlgun_382QA(usuario, "Consultar Bitácora");
            btnGestionRespaldo.Visible = TieneAlgun_382QA(usuario, "Realizar Backup", "Realizar Restore");

            // Negocio (RF01): las órdenes de trabajo se ven con cualquier permiso de OT (vendedor o técnico);
            // cada maestro tiene su propio permiso.
            btnGestionOT.Visible = TieneAlgun_382QA(usuario, "Recepcionar Equipo", "Diagnosticar Equipo", "Gestionar Presupuesto", "Reparar Equipo", "Entregar Equipo");
            btnClientes.Visible = TieneAlgun_382QA(usuario, "Gestionar Clientes");
            btnEquipos.Visible = TieneAlgun_382QA(usuario, "Gestionar Equipos");
            btnTecnicos.Visible = TieneAlgun_382QA(usuario, "Gestionar Tecnicos");
            btnRepuestos.Visible = TieneAlgun_382QA(usuario, "Gestionar Repuestos");
            // Negocio (RF02): reclamos por garantía.
            btnReclamos.Visible = TieneAlgun_382QA(usuario, "Registrar Reclamo", "Evaluar Reclamo", "Reparar Reclamo", "Entregar Reclamo");
            lblSeccionNegocio.Visible = btnGestionOT.Visible || btnReclamos.Visible;
            lblSeccionMaestros.Visible = btnClientes.Visible || btnEquipos.Visible || btnTecnicos.Visible || btnRepuestos.Visible;
        }
        private bool TieneAlgun_382QA(SER_Usuario_382QA usuario, params string[] permisos)
        {
            foreach (string permiso in permisos)
            {
                if (usuario.Rol.TienePermiso_382QA(permiso))
                    return true;
            }
            return false;
        }
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmUsuarios"];

            if (frm == null)
            {
                FrmGestionUsuarios_382QA frmUsuarios = new FrmGestionUsuarios_382QA();
                frmUsuarios.ShowDialog();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }
        private void btnBitacora_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmBitacora"];

            if (frm == null)
            {
                FrmBitacora_382QA frmBitacora = new FrmBitacora_382QA();
                frmBitacora.ShowDialog();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }

        private void btnCambiarClave_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmCambiarClave"];

            if (frm == null)
            {
                frmCambiarClave_382QA frmCambiarClave = new frmCambiarClave_382QA();
                frmCambiarClave.ShowDialog();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(
                bll_idioma.Traducir_382QA("FrmMenu.msgCerrarSesion"),
                bll_idioma.Traducir_382QA("FrmMenu.msgConfirmacion"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
                this.Close(); // el Logout lo hace FrmMenu_FormClosing
        }

        private void FrmMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            bll_usuario.Logout_382QA();
        }

        private void btnRelogin_Click(object sender, EventArgs e)
        {
            FrmLogin_382QA frmLogin = new FrmLogin_382QA();
            this.Hide();
            frmLogin.ShowDialog();

        }

        private void btnGestionRoles_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmRoles"];

            if (frm == null)
            {
                FrmGestionRoles_382QA frmRoles = new FrmGestionRoles_382QA();
                frmRoles.ShowDialog();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }
        private void btnGestionRespaldo_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmBackup"];
            if (frm == null)
            {
                FrmBackup_382QA frmBackup = new FrmBackup_382QA();
                frmBackup.ShowDialog();
                if (frmBackup.RestoreRealizado)
                    this.Close(); // el Logout lo hace FrmMenu_FormClosing
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }
        private void CargarIdiomas_382QA()
        {
            cargandoIdiomas = true;
            cboIdioma.DisplayMember = "Nombre";
            cboIdioma.ValueMember = "Codigo";
            cboIdioma.DataSource = bll_idioma.ObtenerIdiomasDisponibles_382QA();
            cboIdioma.SelectedValue = bll_idioma.IdiomaActual;
            cargandoIdiomas = false;
        }

        private void cboIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoIdiomas) return;
            try
            {
                bll_idioma.CambiarIdioma_382QA(cboIdioma.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ActualizarIdioma_382QA()
        {
            this.Text = bll_idioma.Traducir_382QA("FrmMenu.Form");
            lblTituloTopbar.Text = bll_idioma.Traducir_382QA("FrmMenu.lblTituloTopbar");
            lblSeccionNav.Text = bll_idioma.Traducir_382QA("FrmMenu.lblSeccionNav");
            label1.Text = bll_idioma.Traducir_382QA("FrmMenu.label1");
            btnUsuarios.Text = bll_idioma.Traducir_382QA("FrmMenu.btnUsuarios");
            btnBitacora.Text = bll_idioma.Traducir_382QA("FrmMenu.btnBitacora");
            btnGestionRoles.Text = bll_idioma.Traducir_382QA("FrmMenu.btnGestionRoles");
            btnGestionRespaldo.Text = bll_idioma.Traducir_382QA("FrmMenu.btnGestionRespaldo");
            btnReportes.Text = bll_idioma.Traducir_382QA("FrmMenu.btnReportes");
            btnAyuda.Text = bll_idioma.Traducir_382QA("FrmMenu.btnAyuda");
            btnRelogin.Text = bll_idioma.Traducir_382QA("FrmMenu.btnRelogin");
            btnCambiarClave.Text = bll_idioma.Traducir_382QA("FrmMenu.btnCambiarClave");
            btnLogout.Text = bll_idioma.Traducir_382QA("FrmMenu.btnLogout");
            lblSeccionNegocio.Text = bll_idioma.Traducir_382QA("FrmMenu.lblSeccionNegocio");
            lblSeccionMaestros.Text = bll_idioma.Traducir_382QA("FrmMenu.lblSeccionMaestros");
            btnGestionOT.Text = bll_idioma.Traducir_382QA("FrmMenu.btnGestionOT");
            btnReclamos.Text = bll_idioma.Traducir_382QA("FrmMenu.btnReclamos");
            btnClientes.Text = bll_idioma.Traducir_382QA("FrmMenu.btnClientes");
            btnEquipos.Text = bll_idioma.Traducir_382QA("FrmMenu.btnEquipos");
            btnTecnicos.Text = bll_idioma.Traducir_382QA("FrmMenu.btnTecnicos");
            btnRepuestos.Text = bll_idioma.Traducir_382QA("FrmMenu.btnRepuestos");
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_382QA(this);
            base.OnFormClosed(e);
        }

        private void btnGestionOT_Click(object sender, EventArgs e) => AbrirFormulario_382QA<FrmOrdenesTrabajo_382QA>();
        private void btnReclamos_Click(object sender, EventArgs e) => AbrirFormulario_382QA<FrmReclamos_382QA>();

        private void btnClientes_Click(object sender, EventArgs e) => AbrirFormulario_382QA<FrmGestionClientes_382QA>();
        private void btnEquipos_Click(object sender, EventArgs e) => AbrirFormulario_382QA<FrmGestionEquipos_382QA>();
        private void btnTecnicos_Click(object sender, EventArgs e) => AbrirFormulario_382QA<FrmGestionTecnicos_382QA>();
        private void btnRepuestos_Click(object sender, EventArgs e) => AbrirFormulario_382QA<FrmGestionRepuestos_382QA>();

        /// <summary>Abre el formulario como modal; si ya estuviera abierto, lo trae al frente.</summary>
        private void AbrirFormulario_382QA<T>() where T : Form, new()
        {
            Form? abierto = Application.OpenForms.OfType<T>().FirstOrDefault();
            if (abierto != null)
            {
                abierto.WindowState = FormWindowState.Normal;
                abierto.BringToFront();
                abierto.Focus();
                return;
            }
            using T formulario = new T();
            formulario.ShowDialog();
        }
    }
}
