using BLL;
using Servicio;

namespace UI
{
    public partial class FrmMenu : Form, IObservadorIdioma
    {
        BLL_Usuario bll_usuario;
        BLL_Idioma bll_idioma = new BLL_Idioma();
        bool cargandoIdiomas;
        public FrmMenu()
        {
            InitializeComponent();
            bll_idioma.Suscribir(this);
            CargarIdiomas();
            ActualizarIdioma();
        }
        private void FrmMenu_Load(object sender, EventArgs e)
        {
            bll_usuario = new BLL_Usuario();

            SER_Usuario usuario = SER_SesionManager.ObtenerSesion().Usuario;

            btnUsuarios.Visible = TieneAlgun(usuario, "Crear Usuario", "Modificar Usuario", "Desbloquear Usuario", "Activar Usuario");
            btnGestionRoles.Visible = TieneAlgun(usuario, "Crear Rol", "Crear Familia", "Eliminar Rol", "Eliminar Familia");
            btnBitacora.Visible = TieneAlgun(usuario, "Consultar Bitácora");
            btnGestionRespaldo.Visible = TieneAlgun(usuario, "Realizar Backup", "Realizar Restore");
        }
        private bool TieneAlgun(SER_Usuario usuario, params string[] permisos)
        {
            foreach (string permiso in permisos)
            {
                if (usuario.Rol.TienePermiso(permiso))
                    return true;
            }
            return false;
        }
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmUsuarios"];

            if (frm == null)
            {
                FrmGestionUsuarios frmUsuarios = new FrmGestionUsuarios();
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
                FrmBitacora frmBitacora = new FrmBitacora();
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
                frmCambiarClave frmCambiarClave = new frmCambiarClave();
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
                bll_idioma.Traducir("FrmMenu.msgCerrarSesion"),
                bll_idioma.Traducir("FrmMenu.msgConfirmacion"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
            {
                bll_usuario.Logout();
                this.Close();
            }
        }

        private void FrmMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            bll_usuario.Logout();
        }

        private void btnRelogin_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            this.Hide();
            frmLogin.ShowDialog();

        }

        private void btnGestionRoles_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmRoles"];

            if (frm == null)
            {
                FrmGestionRoles frmRoles = new FrmGestionRoles();
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
                FrmBackup frmBackup = new FrmBackup();
                frmBackup.ShowDialog();
                if (frmBackup.RestoreRealizado)
                {
                    bll_usuario.Logout();
                    FrmLogin login = new FrmLogin();
                    this.Close(); 
                }
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }
        private void CargarIdiomas()
        {
            cargandoIdiomas = true;
            cboIdioma.DisplayMember = "Nombre";
            cboIdioma.ValueMember = "Codigo";
            cboIdioma.DataSource = bll_idioma.ObtenerIdiomasDisponibles();
            cboIdioma.SelectedValue = bll_idioma.IdiomaActual;
            cargandoIdiomas = false;
        }

        private void cboIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoIdiomas) return;
            try
            {
                bll_idioma.CambiarIdioma(cboIdioma.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ActualizarIdioma()
        {
            this.Text = bll_idioma.Traducir("FrmMenu.Form");
            lblTituloTopbar.Text = bll_idioma.Traducir("FrmMenu.lblTituloTopbar");
            lblSeccionNav.Text = bll_idioma.Traducir("FrmMenu.lblSeccionNav");
            label1.Text = bll_idioma.Traducir("FrmMenu.label1");
            btnUsuarios.Text = bll_idioma.Traducir("FrmMenu.btnUsuarios");
            btnBitacora.Text = bll_idioma.Traducir("FrmMenu.btnBitacora");
            btnGestionRoles.Text = bll_idioma.Traducir("FrmMenu.btnGestionRoles");
            btnGestionRespaldo.Text = bll_idioma.Traducir("FrmMenu.btnGestionRespaldo");
            btnReportes.Text = bll_idioma.Traducir("FrmMenu.btnReportes");
            btnAyuda.Text = bll_idioma.Traducir("FrmMenu.btnAyuda");
            btnRelogin.Text = bll_idioma.Traducir("FrmMenu.btnRelogin");
            btnCambiarClave.Text = bll_idioma.Traducir("FrmMenu.btnCambiarClave");
            btnLogout.Text = bll_idioma.Traducir("FrmMenu.btnLogout");
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}
