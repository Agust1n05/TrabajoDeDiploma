using BLL;
using Servicio;
using System.Data;
using System.Text.RegularExpressions;


namespace UI
{
    public partial class frmCambiarClave : Form, IObservadorIdioma
    {
        BLL_Usuario bll_usuario;
        BLL_Idioma bll_idioma = new BLL_Idioma();
        public frmCambiarClave()
        {
            InitializeComponent();
            this.AcceptButton = btnConfirmar;
            bll_idioma.Suscribir(this);
            ActualizarIdioma();
        }
        private bool ValidarDatos(string texto, string expresionRegular)
        {
            Regex re = new Regex(expresionRegular);
            return re.IsMatch(texto);
        }
        private void frmCambiarClave_Load(object sender, EventArgs e)
        {
            bll_usuario = new BLL_Usuario();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                string claveActual = txtClaveActual.Text;
                if (claveActual.Length == 0) throw new Exception("La clave actual está vacía!");

                string claveNueva = txtClaveNueva.Text;
                if (!ValidarDatos(claveNueva, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,50}$")) throw new Exception("El password debe tener al menos 8 caracteres, una mayúscula, una minúscula, un número y un carácter especial!");

                bll_usuario.ModificarPassword(claveActual, claveNueva);
                MessageBox.Show("Se modifico la contraseña con éxito!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var formularios = Application.OpenForms
                                  .Cast<Form>()
                                  .Where(f => f.Name != "FrmLogin")
                                  .ToList();
                
                bll_usuario.Logout();
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
        public void ActualizarIdioma()
        {
            this.Text = bll_idioma.Traducir("frmCambiarClave.Form");
            lblTitulo.Text = bll_idioma.Traducir("frmCambiarClave.lblTitulo");
            lblClaveActual.Text = bll_idioma.Traducir("frmCambiarClave.lblClaveActual");
            lblClaveNueva.Text = bll_idioma.Traducir("frmCambiarClave.lblClaveNueva");
            btnConfirmar.Text = bll_idioma.Traducir("frmCambiarClave.btnConfirmar");
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}
