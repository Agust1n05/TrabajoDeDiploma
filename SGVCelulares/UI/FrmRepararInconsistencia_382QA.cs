using BLL.BLL_SER;
using Servicio;

namespace UI
{
    public partial class FrmRepararInconsistencia_382QA : Form, IObservadorIdioma_382QA
    {
        BLL_DV_382QA bll_dv;
        BLL_Backup_382QA bll_backup;
        BLL_Idioma_382QA bll_idioma = new BLL_Idioma_382QA();
        BLL_Usuario_382QA bLL_Usuario;
        private List<SER_Inconsistencia_382QA> inconsistencias;

        public FrmRepararInconsistencia_382QA()
        {
            InitializeComponent();
            bll_idioma.Suscribir_382QA(this);
            ActualizarIdioma_382QA();
        }

        private void FrmRepararInconsistencia_Load(object sender, EventArgs e)
        {
            bll_dv = new BLL_DV_382QA();
            bll_backup = new BLL_Backup_382QA();
            bLL_Usuario = new BLL_Usuario_382QA();
            try
            {
                inconsistencias = bll_dv.ObtenerInconsistencias_382QA();
                grillaInconsistencias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grillaInconsistencias.MultiSelect = false;
                grillaInconsistencias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                MostrarGrilla_382QA();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MostrarGrilla_382QA()
        {
            grillaInconsistencias.AutoGenerateColumns = false;
            grillaInconsistencias.Columns.Clear();

            grillaInconsistencias.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreTabla",
                HeaderText = "Tabla",
                DataPropertyName = "NombreTabla",
            });

            grillaInconsistencias.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdRegistro",
                HeaderText = "Id Registro",
                DataPropertyName = "IdRegistro",
            });

            grillaInconsistencias.DataSource = null; ; 
            grillaInconsistencias.DataSource = inconsistencias;
            TraducirColumnas_382QA(grillaInconsistencias);
        }
        private void TraducirColumnas_382QA(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;

            grilla.Columns["NombreTabla"].HeaderText = bll_idioma.Traducir_382QA("FrmRepararInconsistencia.colNombreTabla");
            grilla.Columns["IdRegistro"].HeaderText = bll_idioma.Traducir_382QA("FrmRepararInconsistencia.colIdRegistro");
        }
        private void btnRealizarRespaldo_Click(object sender, EventArgs e)
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

        private void btnRestaurarDV_Click(object sender, EventArgs e)
        {
            bll_dv.RecalcularDV_382QA();
            MessageBox.Show("Los valores del DV fueron recalculados correctamente.", "Recalcular DV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();

        }

        private void grillaInconsistencias_SelectionChanged(object sender, EventArgs e)
        {
            if (grillaInconsistencias.SelectedRows.Count == 0)
            {
                richTextBox1.Clear();
                return;
            }

            var seleccionada = (SER_Inconsistencia_382QA)grillaInconsistencias.SelectedRows[0].DataBoundItem;
            richTextBox1.Text = seleccionada.Mensaje;
        }
        public void ActualizarIdioma_382QA()
        {
            this.Text = bll_idioma.Traducir_382QA("FrmRepararInconsistencia.Form");
            btnRealizarRespaldo.Text = bll_idioma.Traducir_382QA("FrmRepararInconsistencia.btnRealizarRespaldo");
            btnRestaurarDV.Text = bll_idioma.Traducir_382QA("FrmRepararInconsistencia.btnRestaurarDV");
            lblTitulo.Text = bll_idioma.Traducir_382QA("FrmRepararInconsistencia.lblTitulo");
            btnSalir.Text = bll_idioma.Traducir_382QA("FrmRepararInconsistencia.btnSalir");
            TraducirColumnas_382QA(grillaInconsistencias);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            bll_idioma.Desuscribir_382QA(this);
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_382QA(this);
            bLL_Usuario.Logout_382QA();
            base.OnFormClosed(e);
        }
    }
}
