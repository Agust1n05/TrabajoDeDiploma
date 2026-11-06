using QuestPDF.Fluent;
using System.Data;
using System.Diagnostics;
using Servicio;
using BLL.BLL_SER;

namespace UI
{
    public partial class FrmBitacora_382QA : Form, IObservadorIdioma_382QA
    {
        BLL_Bitacora_382QA bll_bitacora;
        BLL_Usuario_382QA bll_usuario;
        BLL_Idioma_382QA bll_idioma = new BLL_Idioma_382QA();

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
            },
            {
                "Clientes",
                new[] { "Crear cliente", "Modificar cliente", "Eliminar cliente", "Reactivar cliente" }
            },
            {
                "Equipos",
                new[] { "Crear equipo", "Modificar equipo", "Eliminar equipo", "Reactivar equipo" }
            },
            {
                "Técnicos",
                new[] { "Crear técnico", "Modificar técnico", "Eliminar técnico", "Reactivar técnico" }
            },
            {
                "Repuestos",
                new[] { "Crear repuesto", "Modificar repuesto", "Eliminar repuesto", "Reactivar repuesto" }
            },
            {
                "Órdenes de trabajo",
                new[] { "Recepcionar equipo", "Iniciar diagnóstico", "Diagnosticar equipo", "Aprobar presupuesto", "Rechazar presupuesto", "Reparar equipo", "Realizar cobro", "Entregar equipo", "Entregar sin reparar" }
            },
            {
                "Reclamos",
                new[] { "Registrar reclamo", "Evaluar reclamo", "Reparar reclamo", "Reclamo en espera de repuesto", "Entregar reclamo", "Entregar reclamo sin reparar" }
            }

        };

        public FrmBitacora_382QA()
        {
            InitializeComponent();
            bll_idioma.Suscribir_382QA(this);
            ActualizarIdioma_382QA();
        }

        private void FrmBitacora_Load(object sender, EventArgs e)
        {
            bll_bitacora = new BLL_Bitacora_382QA();
            bll_usuario = new BLL_Usuario_382QA();
            grillaBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grillaBitacora.MultiSelect = false;
            grillaBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dtpFechaInicio.MaxDate = DateTime.Today;
            dtpFechaFin.MaxDate = DateTime.Today;

            cbxCriticidad.Items.AddRange(new string[] { "Alta", "Media", "Baja" });
            MostrarTodosLosEventos_382QA();
            txtNombre.ReadOnly = true;
            txtApellido.ReadOnly = true;
            Mostrar_382QA(grillaBitacora, bll_bitacora.ConsultarParaGrilla3Dias_382QA());
            ActualizarTxt_382QA();
            SER_Usuario_382QA usuario = SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario;
            btnImprimirBitacora.Enabled = usuario.Rol.TienePermiso_382QA("Exportar Bitácora");
        }

        private void MostrarTodosLosEventos_382QA()
        {
            cbxModulo.Items.AddRange(new string[] { "Usuarios", "Roles", "Bitácora", "Clientes", "Equipos", "Técnicos", "Repuestos", "Órdenes de trabajo", "Reclamos" });
            cbxEvento.Items.AddRange(new string[] {
                "Login", "Cambiar clave", "Logout", "Crear usuario", "Desbloquear usuario", "Modificar usuario", "Activar Usuario", "Desactivar Usuario", "Cambiar idioma",
                "Crear rol", "Modificar rol", "Borrar rol", "Crear familia", "Borrar familia",
                "Auditar bitácora",
                "Crear cliente", "Modificar cliente", "Eliminar cliente", "Reactivar cliente",
                "Crear equipo", "Modificar equipo", "Eliminar equipo", "Reactivar equipo",
                "Crear técnico", "Modificar técnico", "Eliminar técnico", "Reactivar técnico",
                "Crear repuesto", "Modificar repuesto", "Eliminar repuesto", "Reactivar repuesto",
                "Recepcionar equipo", "Iniciar diagnóstico", "Diagnosticar equipo", "Aprobar presupuesto", "Rechazar presupuesto", "Reparar equipo", "Realizar cobro", "Entregar equipo", "Entregar sin reparar",
                "Registrar reclamo", "Evaluar reclamo", "Reparar reclamo", "Reclamo en espera de repuesto", "Entregar reclamo", "Entregar reclamo sin reparar"
            });
        }

        private void ActualizarTxt_382QA()
        {
            try
            {
                if (grillaBitacora.SelectedRows.Count == 0)
                    return;

                if (grillaBitacora.SelectedRows[0].Cells["Usuario"].Value == null)
                    return;

                string login = grillaBitacora.SelectedRows[0].Cells["Usuario"].Value.ToString();

                SER_Usuario_382QA us = bll_bitacora.ConsultarPorNombreUsuario_382QA(login);

                if (us == null)
                    return;

                txtNombre.Text = us.Nombre;
                txtApellido.Text = us.Apellido;
            }
            catch
            {

            }
        }
        private void Mostrar_382QA(DataGridView grilla, object datos)
        {
            grilla.DataSource = null;
            grilla.DataSource = datos;
            TraducirColumnas_382QA(grilla);
        }

        private void TraducirColumnas_382QA(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;

            grilla.Columns["Usuario"].HeaderText = bll_idioma.Traducir_382QA("FrmBitacora.colUsuario");
            grilla.Columns["Fecha"].HeaderText = bll_idioma.Traducir_382QA("FrmBitacora.colFecha");
            grilla.Columns["Hora"].HeaderText = bll_idioma.Traducir_382QA("FrmBitacora.colHora");
            grilla.Columns["Modulo"].HeaderText = bll_idioma.Traducir_382QA("FrmBitacora.colModulo");
            grilla.Columns["Evento"].HeaderText = bll_idioma.Traducir_382QA("FrmBitacora.colEvento");
            grilla.Columns["Criticidad"].HeaderText = bll_idioma.Traducir_382QA("FrmBitacora.colCriticidad");
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
                Mostrar_382QA(grillaBitacora, bll_bitacora.ConsultarFiltradoBitacora_382QA(nombreUsuario, modulo, evento, criticidad, fechaInicio, fechaFin));
                if (grillaBitacora.Rows.Count == 0)
                {
                    txtApellido.Text = "";
                    txtNombre.Text = "";
                }
                else
                {
                    ActualizarTxt_382QA();
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
                Mostrar_382QA(grillaBitacora, bll_bitacora.ConsultarParaGrilla3Dias_382QA());
                cbxModulo.Items.Clear(); cbxEvento.Items.Clear();
                MostrarTodosLosEventos_382QA();
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
                if (grillaBitacora.Rows.Count > 0) ActualizarTxt_382QA();
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
                    GuardarPdf_382QA(columnasVisibles, filas, saveDialog);
                    AbrirPdf_382QA(saveDialog.FileName);
                    MessageBox.Show("PDF generado correctamente!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GuardarPdf_382QA(List<DataGridViewColumn> columnasVisibles, List<DataGridViewRow> filas, SaveFileDialog saveDialog)
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
        private void AbrirPdf_382QA(string ruta)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = ruta,
                UseShellExecute = true
            });
        }
        public void ActualizarIdioma_382QA()
        {
            this.Text = bll_idioma.Traducir_382QA("FrmBitacora.Form");
            lblTitulo.Text = bll_idioma.Traducir_382QA("FrmBitacora.lblTitulo");
            lblNombre.Text = bll_idioma.Traducir_382QA("FrmBitacora.lblNombre");
            lblApellido.Text = bll_idioma.Traducir_382QA("FrmBitacora.lblApellido");
            lblLogin.Text = bll_idioma.Traducir_382QA("FrmBitacora.lblLogin");
            lblModulo.Text = bll_idioma.Traducir_382QA("FrmBitacora.lblModulo");
            lblFechaInicio.Text = bll_idioma.Traducir_382QA("FrmBitacora.lblFechaInicio");
            lblEvento.Text = bll_idioma.Traducir_382QA("FrmBitacora.lblEvento");
            lblFechaFin.Text = bll_idioma.Traducir_382QA("FrmBitacora.lblFechaFin");
            lblCriticidad.Text = bll_idioma.Traducir_382QA("FrmBitacora.lblCriticidad");
            btnImprimirBitacora.Text = bll_idioma.Traducir_382QA("FrmBitacora.btnImprimirBitacora");
            btnAplicar.Text = bll_idioma.Traducir_382QA("FrmBitacora.btnAplicar");
            btnLimpiar.Text = bll_idioma.Traducir_382QA("FrmBitacora.btnLimpiar");
            btnSalir.Text = bll_idioma.Traducir_382QA("FrmBitacora.btnSalir");
            TraducirColumnas_382QA(grillaBitacora);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_382QA(this);
            base.OnFormClosed(e);
        }
    }
}
