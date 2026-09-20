using QuestPDF.Fluent;
using System.Data;
using System.Diagnostics;
using BLL;
using Servicio;

namespace UI
{
    public partial class FrmBitacora : Form, IObservadorIdioma
    {
        BLL_Bitacora bll_bitacora;
        BLL_Usuario bll_usuario;
        BLL_Idioma bll_idioma = new BLL_Idioma();

        private readonly Dictionary<string, string[]> eventosPorModulo =
        new Dictionary<string, string[]>
        {
            {
                "Usuarios",
                new[]
                {
                    "Login",
                    "Cambiar clave",
                    "Logout",
                    "Crear usuario",
                    "Desbloquear usuario",
                    "Modificar usuario",
                    "Activar usuario",
                    "Desactivar usuario",
                    "Cambiar idioma"
                }
            },
            {
                "Roles",
                new[]
                {
                    "Crear rol",
                    "Modificar rol",
                    "Borrar rol",
                    "Crear familia",
                    "Borrar familia"
                }
            },
            {
                "Bitácora",
                new[]
                {
                    "Auditar bitácora"
                }
            }

        };

        public FrmBitacora()
        {
            InitializeComponent();
            bll_idioma.Suscribir(this);
            ActualizarIdioma();
        }

        private void FrmBitacora_Load(object sender, EventArgs e)
        {
            bll_bitacora = new BLL_Bitacora();
            bll_usuario = new BLL_Usuario();
            grillaBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grillaBitacora.MultiSelect = false;
            grillaBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dtpFechaInicio.MaxDate = DateTime.Today;
            dtpFechaFin.MaxDate = DateTime.Today;

            cbxCriticidad.Items.AddRange(new string[] { "Alta", "Media", "Baja" });
            MostrarTodosLosEventos();
            txtNombre.ReadOnly = true;
            txtApellido.ReadOnly = true;
            Mostrar(grillaBitacora, bll_bitacora.ConsultarParaGrilla3Dias());
            ActualizarTxt();
            SER_Usuario usuario = SER_SesionManager.ObtenerSesion().Usuario;
            btnImprimirBitacora.Enabled = usuario.Rol.TienePermiso("Exportar Bitácora");
        }

        private void MostrarTodosLosEventos()
        {
            cbxModulo.Items.AddRange(new string[] { "Usuarios", "Roles", "Bitácora" });
            cbxEvento.Items.AddRange(new string[] {
                "Login", "Cambiar clave", "Logout", "Crear usuario", "Desbloquear usuario", "Modificar usuario", "Activar Usuario", "Desactivar Usuario", "Cambiar idioma",
                "Crear rol", "Modificar rol", "Borrar rol", "Crear familia", "Borrar familia",
                "Auditar bitácora"
            });
        }

        private void ActualizarTxt()
        {
            try
            {
                if (grillaBitacora.SelectedRows.Count == 0)
                    return;

                if (grillaBitacora.SelectedRows[0].Cells["Usuario"].Value == null)
                    return;

                string login = grillaBitacora.SelectedRows[0].Cells["Usuario"].Value.ToString();

                SER_Usuario us = bll_bitacora.ConsultarPorNombreUsuario(login);

                if (us == null)
                    return;

                txtNombre.Text = us.Nombre;
                txtApellido.Text = us.Apellido;
            }
            catch
            {

            }
        }
        private void Mostrar(DataGridView grilla, object datos)
        {
            grilla.DataSource = null;
            grilla.DataSource = datos;
            TraducirColumnas(grilla);
        }

        private void TraducirColumnas(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;

            grilla.Columns["Usuario"].HeaderText = bll_idioma.Traducir("FrmBitacora.colUsuario");
            grilla.Columns["Fecha"].HeaderText = bll_idioma.Traducir("FrmBitacora.colFecha");
            grilla.Columns["Hora"].HeaderText = bll_idioma.Traducir("FrmBitacora.colHora");
            grilla.Columns["Modulo"].HeaderText = bll_idioma.Traducir("FrmBitacora.colModulo");
            grilla.Columns["Evento"].HeaderText = bll_idioma.Traducir("FrmBitacora.colEvento");
            grilla.Columns["Criticidad"].HeaderText = bll_idioma.Traducir("FrmBitacora.colCriticidad");
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreUsuario = txtNombreUsuario.Text;
                string modulo = cbxModulo.SelectedItem?.ToString();
                string evento = cbxEvento.SelectedItem?.ToString();
                int? criticidad = cbxCriticidad.SelectedIndex >= 0 ? (int?)cbxCriticidad.SelectedIndex + 1 : null;
                DateTime? fechaInicio = dtpFechaInicio.Value.Date;
                DateTime? fechaFin = dtpFechaFin.Value.Date.AddDays(1).AddTicks(-1);
                if (fechaInicio > fechaFin) throw new Exception("La fecha de inicio no puede ser mayor a la fecha de fin!!");
                Mostrar(grillaBitacora, bll_bitacora.ConsultarFiltradoBitacora(nombreUsuario, modulo, evento, criticidad, fechaInicio, fechaFin));
                if (grillaBitacora.Rows.Count == 0)
                {
                    txtApellido.Text = "";
                    txtNombre.Text = "";
                }
                else
                {
                    ActualizarTxt();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                txtNombreUsuario.Clear();
                cbxCriticidad.SelectedIndex = -1;
                cbxEvento.SelectedIndex = -1;
                cbxModulo.SelectedIndex = -1;
                dtpFechaFin.Value = DateTime.Today;
                dtpFechaInicio.Value = DateTime.Today;
                Mostrar(grillaBitacora, bll_bitacora.ConsultarParaGrilla3Dias());
                cbxModulo.Items.Clear(); cbxEvento.Items.Clear();
                MostrarTodosLosEventos();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void grillaBitacora_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grillaBitacora.Rows.Count > 0) ActualizarTxt();
            }
            catch (Exception) { }
        }

        private void cbxModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxEvento.Items.Clear();
            if (cbxModulo.SelectedItem != null && eventosPorModulo.ContainsKey(cbxModulo.SelectedItem.ToString()))
            {
                cbxEvento.Items.AddRange(eventosPorModulo[cbxModulo.SelectedItem.ToString()]);
            }

            cbxEvento.SelectedIndex = -1;
        }

        private void btnImprimirBitacora_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaBitacora.Rows.Count == 0) throw new Exception("No hay registros para imprimir!");

                var columnasVisibles = grillaBitacora.Columns
                                       .Cast<DataGridViewColumn>()
                                       .Where(c => c.Visible)
                                       .ToList();
                var filas = grillaBitacora.Rows
                            .Cast<DataGridViewRow>()
                            .Where(f => !f.IsNewRow)
                            .ToList();

                using SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF (*.pdf)|*.pdf";
                saveDialog.FileName = $"Bitacora_{DateTime.Now:ddMMyyyy_HHmmss}.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    GuardarPdf(columnasVisibles, filas, saveDialog);
                    AbrirPdf(saveDialog.FileName);
                    MessageBox.Show("PDF generado correctamente!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GuardarPdf(List<DataGridViewColumn> columnasVisibles, List<DataGridViewRow> filas, SaveFileDialog saveDialog)
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);
                    page.Header()
                        .Text("Reporte de Bitácora")
                        .FontSize(18)
                        .Bold();

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            foreach (var columna in columnasVisibles)
                                columns.RelativeColumn();
                        });
                        table.Header(header =>
                        {
                            foreach (var columna in columnasVisibles)
                            {
                                header.Cell()
                                    .Border(1)
                                    .Padding(5)
                                    .Text(columna.HeaderText)
                                    .Bold();
                            }
                        });
                        foreach (var fila in filas)
                        {
                            foreach (var columna in columnasVisibles)
                            {
                                string valor =
                                    fila.Cells[columna.Index].Value?.ToString() ?? "";

                                table.Cell()
                                    .Border(1)
                                    .Padding(5)
                                    .Text(valor);
                            }
                        }
                    });
                });
            }).GeneratePdf(saveDialog.FileName);
        }
        private void AbrirPdf(string ruta)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = ruta,
                UseShellExecute = true
            });
        }
        public void ActualizarIdioma()
        {
            this.Text = bll_idioma.Traducir("FrmBitacora.Form");
            lblTitulo.Text = bll_idioma.Traducir("FrmBitacora.lblTitulo");
            lblNombre.Text = bll_idioma.Traducir("FrmBitacora.lblNombre");
            lblApellido.Text = bll_idioma.Traducir("FrmBitacora.lblApellido");
            lblLogin.Text = bll_idioma.Traducir("FrmBitacora.lblLogin");
            lblModulo.Text = bll_idioma.Traducir("FrmBitacora.lblModulo");
            lblFechaInicio.Text = bll_idioma.Traducir("FrmBitacora.lblFechaInicio");
            lblEvento.Text = bll_idioma.Traducir("FrmBitacora.lblEvento");
            lblFechaFin.Text = bll_idioma.Traducir("FrmBitacora.lblFechaFin");
            lblCriticidad.Text = bll_idioma.Traducir("FrmBitacora.lblCriticidad");
            btnImprimirBitacora.Text = bll_idioma.Traducir("FrmBitacora.btnImprimirBitacora");
            btnAplicar.Text = bll_idioma.Traducir("FrmBitacora.btnAplicar");
            btnLimpiar.Text = bll_idioma.Traducir("FrmBitacora.btnLimpiar");
            btnSalir.Text = bll_idioma.Traducir("FrmBitacora.btnSalir");
            TraducirColumnas(grillaBitacora);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}
