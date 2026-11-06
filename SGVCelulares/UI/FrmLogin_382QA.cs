using BLL.BLL_SER;
using Servicio;
using System.Text.RegularExpressions;

namespace UI
{
    public partial class FrmLogin_382QA : Form, IObservadorIdioma_382QA
    {
        BLL_Usuario_382QA bll_usuario;
        BLL_Idioma_382QA bll_idioma = new BLL_Idioma_382QA();
        bool cargandoIdiomas;
        public FrmLogin_382QA()
        {
            InitializeComponent();
            this.AcceptButton = btnEntrar;
            bll_idioma.Suscribir_382QA(this);
            CargarIdiomas_382QA();
        }
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            bll_usuario = new BLL_Usuario_382QA();
        }
        private bool ValidarDatos_382QA(string texto, string expresionRegular)
        {
            Regex re = new Regex(expresionRegular);
            return re.IsMatch(texto);
        }
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreUsuario = txtNombreUsuario.Text;
                if (!ValidarDatos_382QA(nombreUsuario, @"^[a-zA-Z0-9_]{1,50}$")) throw new Exception("Datos erróneos!");

                string password = txtPassword.Text;
                if (password.Length == 0) throw new Exception("Datos erróneos!");

                string mensajeInconsistencia;

                if (bll_usuario.Login_382QA(new SER_Usuario_382QA(nombreUsuario, password), out mensajeInconsistencia))
                {
                    if (!string.IsNullOrEmpty(mensajeInconsistencia))
                    {
                        MessageBox.Show(
                            mensajeInconsistencia,
                            "Inconsistencia detectada",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                       
                        txtNombreUsuario.Text = "";
                        txtPassword.Text = "";

                        this.Hide();
                        
                        FrmRepararInconsistencia_382QA frmRepararInconsistencia = new FrmRepararInconsistencia_382QA();
                        frmRepararInconsistencia.ShowDialog();
                        this.Show();

                    }
                    else 
                    {

                        txtNombreUsuario.Text = "";
                        txtPassword.Text = "";

                        this.Hide();

                        FrmMenu_382QA frmMenu = new FrmMenu_382QA();
                        frmMenu.ShowDialog();

                        this.Show();

                        cargandoIdiomas = true;
                        cboIdioma.SelectedValue = bll_idioma.IdiomaActual;
                        cargandoIdiomas = false;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string codigo = cboIdioma.SelectedValue.ToString();
                bll_idioma.CambiarIdioma_382QA(codigo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ActualizarIdioma_382QA()
        {
            this.Text = bll_idioma.Traducir_382QA("FrmLogin.Form");
            lblTitulo.Text = bll_idioma.Traducir_382QA("FrmLogin.lblTitulo");
            lblUsuario.Text = bll_idioma.Traducir_382QA("FrmLogin.lblUsuario");
            lblPassword.Text = bll_idioma.Traducir_382QA("FrmLogin.lblPassword");
            btnEntrar.Text = bll_idioma.Traducir_382QA("FrmLogin.btnEntrar");
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_382QA(this);
            base.OnFormClosed(e);
        }
    }
}
