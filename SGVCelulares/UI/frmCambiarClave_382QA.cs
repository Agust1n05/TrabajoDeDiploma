using BLL.BLL_SER;
using Servicio;
using System.Data;
using System.Text.RegularExpressions;


namespace UI
{
    public partial class frmCambiarClave_382QA : Form, IObservadorIdioma_382QA
    {
        BLL_Usuario_382QA bll_usuario;
        BLL_Idioma_382QA bll_idioma = new BLL_Idioma_382QA();
        public frmCambiarClave_382QA()
        {
            InitializeComponent();
            this.AcceptButton = btnConfirmar;
            bll_idioma.Suscribir_382QA(this);
            ActualizarIdioma_382QA();
        }
        private bool ValidarDatos_382QA(string texto, string expresionRegular)
        {
            Regex re = new Regex(expresionRegular);
            return re.IsMatch(texto);
        }
        private void frmCambiarClave_Load(object sender, EventArgs e)
        {
            bll_usuario = new BLL_Usuario_382QA();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                string claveActual = txtClaveActual.Text;
                if (claveActual.Length == 0) throw new Exception("La clave actual está vacía!");

                string claveNueva = txtClaveNueva.Text;
                if (!ValidarDatos_382QA(claveNueva, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,50}$")) throw new Exception("El password debe tener al menos 8 caracteres, una mayúscula, una minúscula, un número y un carácter especial!");

                bll_usuario.ModificarPassword_382QA(claveActual, claveNueva);
                MessageBox.Show("Se modifico la contraseña con éxito!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var formularios = Application.OpenForms
                                  .Cast<Form>()
                                  .Where(f => f.Name != "FrmLogin")
                                  .ToList();
                
                bll_usuario.Logout_382QA();
                foreach (var form in formularios)
                {
                    form.Close();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ActualizarIdioma_382QA()
        {
            this.Text = bll_idioma.Traducir_382QA("frmCambiarClave.Form");
            lblTitulo.Text = bll_idioma.Traducir_382QA("frmCambiarClave.lblTitulo");
            lblClaveActual.Text = bll_idioma.Traducir_382QA("frmCambiarClave.lblClaveActual");
            lblClaveNueva.Text = bll_idioma.Traducir_382QA("frmCambiarClave.lblClaveNueva");
            btnConfirmar.Text = bll_idioma.Traducir_382QA("frmCambiarClave.btnConfirmar");
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_382QA(this);
            base.OnFormClosed(e);
        }
    }
}
