using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using Servicio;

namespace UI
{
    public partial class FrmGestionTecnicos_382QA : Form, IObservadorIdioma_382QA
    {
        private readonly BLL_Tecnico_382QA bll_tecnico = new BLL_Tecnico_382QA();
        private readonly BLL_Idioma_382QA bll_idioma = new BLL_Idioma_382QA();
        private List<BE_Tecnico_382QA> tecnicos = new List<BE_Tecnico_382QA>();
        private ModoEdicion_382QA modo = ModoEdicion_382QA.Consulta;

        /// <summary>Opción del combo de usuarios del alta.</summary>
        private class OpcionUsuario
        {
            public SER_Usuario_382QA Usuario { get; }
            public string Texto => $"{Usuario.Apellido}, {Usuario.Nombre} ({Usuario.NombreUsuario})";
            public OpcionUsuario(SER_Usuario_382QA usuario) => Usuario = usuario;
        }

        public FrmGestionTecnicos_382QA()
        {
            InitializeComponent();
            bll_idioma.Suscribir_382QA(this);
        }

        private void FrmGestionTecnicos_Load(object sender, EventArgs e)
        {
            cboUsuario.DisplayMember = nameof(OpcionUsuario.Texto);
            ConfigurarGrilla_382QA();
            ActualizarIdioma_382QA();
            CargarTecnicos_382QA();
            CambiarModo_382QA(ModoEdicion_382QA.Consulta);
        }

        private BE_Tecnico_382QA? TecnicoSeleccionado => grilla.CurrentRow?.DataBoundItem as BE_Tecnico_382QA;

        private void ConfigurarGrilla_382QA()
        {
            grilla.AutoGenerateColumns = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.Columns.Clear();
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDni", DataPropertyName = "Dni" });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNombre", DataPropertyName = "Nombre" });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colApellido", DataPropertyName = "Apellido" });
            grilla.Columns.Add(new DataGridViewCheckBoxColumn { Name = "colDisponible", DataPropertyName = "Disponible" });
        }

        private void CargarTecnicos_382QA()
        {
            try
            {
                tecnicos = bll_tecnico.Consultar_382QA();
            }
            catch (Exception ex)
            {
                tecnicos = new List<BE_Tecnico_382QA>();
                MostrarError_382QA(ex.Message);
            }
            AplicarFiltro_382QA();
        }

        private void AplicarFiltro_382QA()
        {
            string filtro = txtBuscar.Text.Trim();
            List<BE_Tecnico_382QA> resultado = string.IsNullOrEmpty(filtro)
                ? tecnicos
                : tecnicos.Where(t => t.Dni.Contains(filtro)
                                   || t.Apellido.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();

            grilla.DataSource = null;
            grilla.DataSource = resultado;
            lblNumero.Text = resultado.Count.ToString();
            MostrarTecnico_382QA(TecnicoSeleccionado);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e) => AplicarFiltro_382QA();

        private void grilla_SelectionChanged(object sender, EventArgs e)
        {
            if (modo == ModoEdicion_382QA.Consulta)
                MostrarTecnico_382QA(TecnicoSeleccionado);
        }

        private void MostrarTecnico_382QA(BE_Tecnico_382QA? tecnico)
        {
            txtDni.Text = tecnico?.Dni ?? "";
            txtNombre.Text = tecnico?.Nombre ?? "";
            txtApellido.Text = tecnico?.Apellido ?? "";
            chkDisponible.Checked = tecnico?.Disponible ?? true;
        }

        private void cboUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modo != ModoEdicion_382QA.Alta) return;

            SER_Usuario_382QA? usuario = (cboUsuario.SelectedItem as OpcionUsuario)?.Usuario;
            txtDni.Text = usuario?.Dni ?? "";
            txtNombre.Text = usuario?.Nombre ?? "";
            txtApellido.Text = usuario?.Apellido ?? "";
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            List<SER_Usuario_382QA> usuarios;
            try
            {
                usuarios = bll_tecnico.ConsultarUsuariosParaAlta_382QA();
            }
            catch (Exception ex)
            {
                MostrarError_382QA(ex.Message);
                return;
            }

            if (usuarios.Count == 0)
            {
                MostrarAtencion_382QA(bll_idioma.Traducir_382QA("FrmGestionTecnicos.msgSinUsuarios"));
                return;
            }

            CambiarModo_382QA(ModoEdicion_382QA.Alta);
            MostrarTecnico_382QA(null);
            cboUsuario.DataSource = usuarios.Select(u => new OpcionUsuario(u)).ToList();
            cboUsuario.SelectedIndex = -1;
            cboUsuario.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (TecnicoSeleccionado == null)
            {
                MostrarAtencion_382QA(bll_idioma.Traducir_382QA("ABM.msgSeleccione"));
                return;
            }
            CambiarModo_382QA(ModoEdicion_382QA.Modificacion);
            chkDisponible.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            BE_Tecnico_382QA? tecnico = TecnicoSeleccionado;
            if (tecnico == null)
            {
                MostrarAtencion_382QA(bll_idioma.Traducir_382QA("ABM.msgSeleccione"));
                return;
            }

            string descripcion = $"{tecnico.Apellido}, {tecnico.Nombre} ({tecnico.Dni})";
            if (!Confirmar_382QA(string.Format(bll_idioma.Traducir_382QA("ABM.msgConfirmarEliminar"), descripcion)))
                return;

            try
            {
                bll_tecnico.Borrar_382QA(tecnico);
                MostrarExito_382QA(bll_idioma.Traducir_382QA("ABM.msgEliminado"));
                CargarTecnicos_382QA();
            }
            catch (Exception ex)
            {
                MostrarError_382QA(ex.Message);
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (modo == ModoEdicion_382QA.Consulta) return;

            var tecnico = new BE_Tecnico_382QA(txtDni.Text.Trim(), txtNombre.Text, txtApellido.Text, chkDisponible.Checked);
            try
            {
                if (modo == ModoEdicion_382QA.Alta)
                {
                    bool reactivar = bll_tecnico.EstaDadoDeBaja_382QA(tecnico.Dni);
                    if (reactivar && !Confirmar_382QA(string.Format(bll_idioma.Traducir_382QA("ABM.msgReactivar"), lblDni.Text)))
                        return;

                    bll_tecnico.Agregar_382QA(tecnico);
                    MostrarExito_382QA(bll_idioma.Traducir_382QA(reactivar ? "ABM.msgReactivado" : "ABM.msgCreado"));
                }
                else
                {
                    bll_tecnico.Modificar_382QA(tecnico);
                    MostrarExito_382QA(bll_idioma.Traducir_382QA("ABM.msgModificado"));
                }

                CambiarModo_382QA(ModoEdicion_382QA.Consulta);
                CargarTecnicos_382QA();
                SeleccionarFila_382QA(tecnico.Dni);
            }
            catch (Exception ex)
            {
                MostrarError_382QA(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CambiarModo_382QA(ModoEdicion_382QA.Consulta);
            MostrarTecnico_382QA(TecnicoSeleccionado);
        }

        private void btnSalir_Click(object sender, EventArgs e) => Close();

        private void CambiarModo_382QA(ModoEdicion_382QA nuevoModo)
        {
            modo = nuevoModo;
            bool editando = modo != ModoEdicion_382QA.Consulta;

            // El usuario solo se elige en el alta; DNI, nombre y apellido siempre vienen del usuario.
            lblUsuario.Visible = modo == ModoEdicion_382QA.Alta;
            cboUsuario.Visible = modo == ModoEdicion_382QA.Alta;
            if (modo != ModoEdicion_382QA.Alta) cboUsuario.DataSource = null;
            chkDisponible.Enabled = editando;

            grilla.Enabled = !editando;
            txtBuscar.Enabled = !editando;
            btnCrear.Enabled = !editando;
            btnModificar.Enabled = !editando;
            btnEliminar.Enabled = !editando;
            btnAplicar.Enabled = editando;
            btnCancelar.Enabled = editando;
        }

        private void SeleccionarFila_382QA(string dni)
        {
            foreach (DataGridViewRow fila in grilla.Rows)
            {
                if (fila.DataBoundItem is BE_Tecnico_382QA t && t.Dni == dni)
                {
                    grilla.CurrentCell = fila.Cells[0];
                    return;
                }
            }
        }

        private bool Confirmar_382QA(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir_382QA("ABM.Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        private void MostrarAtencion_382QA(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir_382QA("ABM.Atencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private void MostrarExito_382QA(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir_382QA("ABM.Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void MostrarError_382QA(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir_382QA("ABM.Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void ActualizarIdioma_382QA()
        {
            Text = bll_idioma.Traducir_382QA("FrmGestionTecnicos.Form");
            lblTitulo.Text = bll_idioma.Traducir_382QA("FrmGestionTecnicos.lblTitulo");
            lblCantidad.Text = bll_idioma.Traducir_382QA("FrmGestionTecnicos.lblCantidad");
            lblFiltro.Text = bll_idioma.Traducir_382QA("ABM.lblFiltro");
            lblBuscar.Text = bll_idioma.Traducir_382QA("FrmGestionTecnicos.lblBuscar");
            lblUsuario.Text = bll_idioma.Traducir_382QA("FrmGestionTecnicos.lblUsuario");
            lblDni.Text = bll_idioma.Traducir_382QA("FrmGestionTecnicos.lblDni");
            lblNombre.Text = bll_idioma.Traducir_382QA("FrmGestionTecnicos.lblNombre");
            lblApellido.Text = bll_idioma.Traducir_382QA("FrmGestionTecnicos.lblApellido");
            lblDisponible.Text = bll_idioma.Traducir_382QA("FrmGestionTecnicos.lblDisponible");
            btnCrear.Text = bll_idioma.Traducir_382QA("ABM.btnCrear");
            btnModificar.Text = bll_idioma.Traducir_382QA("ABM.btnModificar");
            btnEliminar.Text = bll_idioma.Traducir_382QA("ABM.btnEliminar");
            btnAplicar.Text = bll_idioma.Traducir_382QA("ABM.btnAplicar");
            btnCancelar.Text = bll_idioma.Traducir_382QA("ABM.btnCancelar");
            btnSalir.Text = bll_idioma.Traducir_382QA("ABM.btnSalir");

            if (grilla.Columns.Count > 0)
            {
                grilla.Columns["colDni"].HeaderText = lblDni.Text;
                grilla.Columns["colNombre"].HeaderText = lblNombre.Text;
                grilla.Columns["colApellido"].HeaderText = lblApellido.Text;
                grilla.Columns["colDisponible"].HeaderText = lblDisponible.Text;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_382QA(this);
            base.OnFormClosed(e);
        }
    }
}
