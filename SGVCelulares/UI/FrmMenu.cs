using BLL.BLL_SER;
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

            // Negocio (RF01): las órdenes de trabajo se ven con cualquier permiso de OT (vendedor o técnico);
            // cada maestro tiene su propio permiso.
            btnGestionOT.Visible = TieneAlgun(usuario, "Recepcionar Equipo", "Diagnosticar Equipo", "Gestionar Presupuesto", "Reparar Equipo", "Entregar Equipo");
            btnClientes.Visible = TieneAlgun(usuario, "Gestionar Clientes");
            btnEquipos.Visible = TieneAlgun(usuario, "Gestionar Equipos");
            btnTecnicos.Visible = TieneAlgun(usuario, "Gestionar Tecnicos");
            btnRepuestos.Visible = TieneAlgun(usuario, "Gestionar Repuestos");
            // Negocio (RF02): reclamos por garantía.
            btnReclamos.Visible = TieneAlgun(usuario, "Registrar Reclamo", "Evaluar Reclamo");
            lblSeccionNegocio.Visible = btnGestionOT.Visible || btnReclamos.Visible || btnClientes.Visible || btnEquipos.Visible || btnTecnicos.Visible || btnRepuestos.Visible;
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
                this.Close(); // el Logout lo hace FrmMenu_FormClosing
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
                    this.Close(); // el Logout lo hace FrmMenu_FormClosing
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
            lblSeccionNegocio.Text = bll_idioma.Traducir("FrmMenu.lblSeccionNegocio");
            btnGestionOT.Text = bll_idioma.Traducir("FrmMenu.btnGestionOT");
            btnReclamos.Text = bll_idioma.Traducir("FrmMenu.btnReclamos");
            btnClientes.Text = bll_idioma.Traducir("FrmMenu.btnClientes");
            btnEquipos.Text = bll_idioma.Traducir("FrmMenu.btnEquipos");
            btnTecnicos.Text = bll_idioma.Traducir("FrmMenu.btnTecnicos");
            btnRepuestos.Text = bll_idioma.Traducir("FrmMenu.btnRepuestos");
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir(this);
            base.OnFormClosed(e);
        }

        private void btnGestionOT_Click(object sender, EventArgs e) => AbrirFormulario<FrmOrdenesTrabajo>();
        private void btnReclamos_Click(object sender, EventArgs e) => AbrirFormulario<FrmReclamos>();

        private void btnClientes_Click(object sender, EventArgs e) => AbrirFormulario<FrmGestionClientes>();
        private void btnEquipos_Click(object sender, EventArgs e) => AbrirFormulario<FrmGestionEquipos>();
        private void btnTecnicos_Click(object sender, EventArgs e) => AbrirFormulario<FrmGestionTecnicos>();
        private void btnRepuestos_Click(object sender, EventArgs e) => AbrirFormulario<FrmGestionRepuestos>();

        /// <summary>Abre el formulario como modal; si ya estuviera abierto, lo trae al frente.</summary>
        private void AbrirFormulario<T>() where T : Form, new()
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
