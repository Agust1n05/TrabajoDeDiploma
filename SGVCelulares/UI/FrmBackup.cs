using BLL.BLL_SER;
using Servicio;

namespace UI
{
    public partial class FrmBackup : Form, IObservadorIdioma
    {
        BLL_Backup bll_backup;
        BLL_Idioma bll_idioma = new BLL_Idioma();
        public bool RestoreRealizado { get; private set; } = false;
        public FrmBackup()
        {
            InitializeComponent();
            bll_idioma.Suscribir(this);
            ActualizarIdioma();
        }
        private void FrmBackup_Load(object sender, EventArgs e)
        {
            bll_backup = new();
            SER_Usuario usuario = SER_SesionManager.ObtenerSesion().Usuario;
            btnBackup.Enabled = usuario.Rol.TienePermiso("Realizar Backup");
            btnRestore.Enabled = usuario.Rol.TienePermiso("Realizar Restore");
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
                    bll_backup.Backup(ruta);
                    MessageBox.Show(bll_idioma.Traducir("FrmBackup.msgOperacionExitosaBackup"), bll_idioma.Traducir("FrmBackup.msgInformacion"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    DialogResult rdo = MessageBox.Show(bll_idioma.Traducir("FrmBackup.msgConfirmarRestore"), bll_idioma.Traducir("FrmBackup.msgConfirmarOperacion"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (rdo == DialogResult.Yes)
                    {
                        Cursor = Cursors.WaitCursor;
                        bll_backup.Restore(ruta);
                        Cursor = Cursors.Default;
                        MessageBox.Show(bll_idioma.Traducir("FrmBackup.msgOperacionExitosaRestore"), bll_idioma.Traducir("FrmBackup.msgInformacion"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RestoreRealizado = true;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(bll_idioma.Traducir("FrmBackup.msgError"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ActualizarIdioma()
        {
            this.Text = bll_idioma.Traducir("FrmBackup.Form");
            btnBackup.Text = bll_idioma.Traducir("FrmBackup.btnBackup");
            btnRestore.Text = bll_idioma.Traducir("FrmBackup.btnRestore");
            btnSalir.Text = bll_idioma.Traducir("FrmBackup.btnSalir");

        }
        private void btnSalir_Click(object sender, EventArgs e) => this.Close();
    }
}
