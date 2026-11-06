using BLL.BLL_SER;
using Servicio;

namespace UI
{
    public partial class FrmBackup_382QA : Form, IObservadorIdioma_382QA
    {
        BLL_Backup_382QA bll_backup;
        BLL_Idioma_382QA bll_idioma = new BLL_Idioma_382QA();
        public bool RestoreRealizado { get; private set; } = false;
        public FrmBackup_382QA()
        {
            InitializeComponent();
            bll_idioma.Suscribir_382QA(this);
            ActualizarIdioma_382QA();
        }
        private void FrmBackup_Load(object sender, EventArgs e)
        {
            bll_backup = new();
            SER_Usuario_382QA usuario = SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario;
            btnBackup.Enabled = usuario.Rol.TienePermiso_382QA("Realizar Backup");
            btnRestore.Enabled = usuario.Rol.TienePermiso_382QA("Realizar Restore");
        }
        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Backup SQL Server (*.bak)|*.bak";
                saveDialog.DefaultExt = "bak";
                saveDialog.AddExtension = true;
                saveDialog.FileName = $"bkp_sgvcelulares_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveDialog.FileName;
                    bll_backup.Backup_382QA(ruta);
                    MessageBox.Show(bll_idioma.Traducir_382QA("FrmBackup.msgOperacionExitosaBackup"), bll_idioma.Traducir_382QA("FrmBackup.msgInformacion"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Filter = "Backup SQL Server (*.bak)|*.bak";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = openDialog.FileName;
                    string nombreArchivo = Path.GetFileName(ruta);
                    DialogResult rdo = MessageBox.Show(bll_idioma.Traducir_382QA("FrmBackup.msgConfirmarRestore"), bll_idioma.Traducir_382QA("FrmBackup.msgConfirmarOperacion"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (rdo == DialogResult.Yes)
                    {
                        Cursor = Cursors.WaitCursor;
                        bll_backup.Restore_382QA(ruta);
                        Cursor = Cursors.Default;
                        MessageBox.Show(bll_idioma.Traducir_382QA("FrmBackup.msgOperacionExitosaRestore"), bll_idioma.Traducir_382QA("FrmBackup.msgInformacion"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RestoreRealizado = true;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(bll_idioma.Traducir_382QA("FrmBackup.msgError"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ActualizarIdioma_382QA()
        {
            this.Text = bll_idioma.Traducir_382QA("FrmBackup.Form");
            btnBackup.Text = bll_idioma.Traducir_382QA("FrmBackup.btnBackup");
            btnRestore.Text = bll_idioma.Traducir_382QA("FrmBackup.btnRestore");
            btnSalir.Text = bll_idioma.Traducir_382QA("FrmBackup.btnSalir");

        }
        private void btnSalir_Click(object sender, EventArgs e) => this.Close();

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_382QA(this);
            base.OnFormClosed(e);
        }
    }
}
