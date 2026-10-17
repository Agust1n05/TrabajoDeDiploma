using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using Servicio;

namespace UI
{
    public partial class FrmGestionTecnicos : Form, IObservadorIdioma
    {
        private readonly BLL_Tecnico bll_tecnico = new BLL_Tecnico();
        private readonly BLL_Idioma bll_idioma = new BLL_Idioma();
        private List<BE_Tecnico> tecnicos = new List<BE_Tecnico>();
        private ModoEdicion modo = ModoEdicion.Consulta;

        /// <summary>Opción del combo de usuarios del alta.</summary>
        private class OpcionUsuario
        {
            public SER_Usuario Usuario { get; }
            public string Texto => $"{Usuario.Apellido}, {Usuario.Nombre} ({Usuario.NombreUsuario})";
            public OpcionUsuario(SER_Usuario usuario) => Usuario = usuario;
        }

        public FrmGestionTecnicos()
        {
            InitializeComponent();
            bll_idioma.Suscribir(this);
        }

        private void FrmGestionTecnicos_Load(object sender, EventArgs e)
        {
            cboUsuario.DisplayMember = nameof(OpcionUsuario.Texto);
            ConfigurarGrilla();
            ActualizarIdioma();
            CargarTecnicos();
            CambiarModo(ModoEdicion.Consulta);
        }

        private BE_Tecnico? TecnicoSeleccionado => grilla.CurrentRow?.DataBoundItem as BE_Tecnico;

        private void ConfigurarGrilla()
        {
            grilla.AutoGenerateColumns = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.Columns.Clear();
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDni", DataPropertyName = "Dni" });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNombre", DataPropertyName = "Nombre" });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colApellido", DataPropertyName = "Apellido" });
            grilla.Columns.Add(new DataGridViewCheckBoxColumn { Name = "colDisponible", DataPropertyName = "Disponible" });
        }

        private void CargarTecnicos()
        {
            try
            {
                tecnicos = bll_tecnico.Consultar();
            }
            catch (Exception ex)
            {
                tecnicos = new List<BE_Tecnico>();
                MostrarError(ex.Message);
            }
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            string filtro = txtBuscar.Text.Trim();
            List<BE_Tecnico> resultado = string.IsNullOrEmpty(filtro)
                ? tecnicos
                : tecnicos.Where(t => t.Dni.Contains(filtro)
                                   || t.Apellido.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();

            grilla.DataSource = null;
            grilla.DataSource = resultado;
            lblNumero.Text = resultado.Count.ToString();
            MostrarTecnico(TecnicoSeleccionado);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e) => AplicarFiltro();

        private void grilla_SelectionChanged(object sender, EventArgs e)
        {
            if (modo == ModoEdicion.Consulta)
                MostrarTecnico(TecnicoSeleccionado);
        }

        private void MostrarTecnico(BE_Tecnico? tecnico)
        {
            txtDni.Text = tecnico?.Dni ?? "";
            txtNombre.Text = tecnico?.Nombre ?? "";
            txtApellido.Text = tecnico?.Apellido ?? "";
            chkDisponible.Checked = tecnico?.Disponible ?? true;
        }

        private void cboUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modo != ModoEdicion.Alta) return;

            SER_Usuario? usuario = (cboUsuario.SelectedItem as OpcionUsuario)?.Usuario;
            txtDni.Text = usuario?.Dni ?? "";
            txtNombre.Text = usuario?.Nombre ?? "";
            txtApellido.Text = usuario?.Apellido ?? "";
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            List<SER_Usuario> usuarios;
            try
            {
                usuarios = bll_tecnico.ConsultarUsuariosParaAlta();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
                return;
            }

            if (usuarios.Count == 0)
            {
                MostrarAtencion(bll_idioma.Traducir("FrmGestionTecnicos.msgSinUsuarios"));
                return;
            }

            CambiarModo(ModoEdicion.Alta);
            MostrarTecnico(null);
            cboUsuario.DataSource = usuarios.Select(u => new OpcionUsuario(u)).ToList();
            cboUsuario.SelectedIndex = -1;
            cboUsuario.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (TecnicoSeleccionado == null)
            {
                MostrarAtencion(bll_idioma.Traducir("ABM.msgSeleccione"));
                return;
            }
            CambiarModo(ModoEdicion.Modificacion);
            chkDisponible.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            BE_Tecnico? tecnico = TecnicoSeleccionado;
            if (tecnico == null)
            {
                MostrarAtencion(bll_idioma.Traducir("ABM.msgSeleccione"));
                return;
            }

            string descripcion = $"{tecnico.Apellido}, {tecnico.Nombre} ({tecnico.Dni})";
            if (!Confirmar(string.Format(bll_idioma.Traducir("ABM.msgConfirmarEliminar"), descripcion)))
                return;

            try
            {
                bll_tecnico.Borrar(tecnico);
                MostrarExito(bll_idioma.Traducir("ABM.msgEliminado"));
                CargarTecnicos();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (modo == ModoEdicion.Consulta) return;

            var tecnico = new BE_Tecnico(txtDni.Text.Trim(), txtNombre.Text, txtApellido.Text, chkDisponible.Checked);
            try
            {
                if (modo == ModoEdicion.Alta)
                {
                    bool reactivar = bll_tecnico.EstaDadoDeBaja(tecnico.Dni);
                    if (reactivar && !Confirmar(string.Format(bll_idioma.Traducir("ABM.msgReactivar"), lblDni.Text)))
                        return;

                    bll_tecnico.Agregar(tecnico);
                    MostrarExito(bll_idioma.Traducir(reactivar ? "ABM.msgReactivado" : "ABM.msgCreado"));
                }
                else
                {
                    bll_tecnico.Modificar(tecnico);
                    MostrarExito(bll_idioma.Traducir("ABM.msgModificado"));
                }

                CambiarModo(ModoEdicion.Consulta);
                CargarTecnicos();
                SeleccionarFila(tecnico.Dni);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoEdicion.Consulta);
            MostrarTecnico(TecnicoSeleccionado);
        }

        private void btnSalir_Click(object sender, EventArgs e) => Close();

        private void CambiarModo(ModoEdicion nuevoModo)
        {
            modo = nuevoModo;
            bool editando = modo != ModoEdicion.Consulta;

            // El usuario solo se elige en el alta; DNI, nombre y apellido siempre vienen del usuario.
            lblUsuario.Visible = modo == ModoEdicion.Alta;
            cboUsuario.Visible = modo == ModoEdicion.Alta;
            if (modo != ModoEdicion.Alta) cboUsuario.DataSource = null;
            chkDisponible.Enabled = editando;

            grilla.Enabled = !editando;
            txtBuscar.Enabled = !editando;
            btnCrear.Enabled = !editando;
            btnModificar.Enabled = !editando;
            btnEliminar.Enabled = !editando;
            btnAplicar.Enabled = editando;
            btnCancelar.Enabled = editando;
        }

        private void SeleccionarFila(string dni)
        {
            foreach (DataGridViewRow fila in grilla.Rows)
            {
                if (fila.DataBoundItem is BE_Tecnico t && t.Dni == dni)
                {
                    grilla.CurrentCell = fila.Cells[0];
                    return;
                }
            }
        }

        private bool Confirmar(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir("ABM.Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        private void MostrarAtencion(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir("ABM.Atencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private void MostrarExito(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir("ABM.Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void MostrarError(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir("ABM.Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void ActualizarIdioma()
        {
            Text = bll_idioma.Traducir("FrmGestionTecnicos.Form");
            lblTitulo.Text = bll_idioma.Traducir("FrmGestionTecnicos.lblTitulo");
            lblCantidad.Text = bll_idioma.Traducir("FrmGestionTecnicos.lblCantidad");
            lblFiltro.Text = bll_idioma.Traducir("ABM.lblFiltro");
            lblBuscar.Text = bll_idioma.Traducir("FrmGestionTecnicos.lblBuscar");
            lblUsuario.Text = bll_idioma.Traducir("FrmGestionTecnicos.lblUsuario");
            lblDni.Text = bll_idioma.Traducir("FrmGestionTecnicos.lblDni");
            lblNombre.Text = bll_idioma.Traducir("FrmGestionTecnicos.lblNombre");
            lblApellido.Text = bll_idioma.Traducir("FrmGestionTecnicos.lblApellido");
            lblDisponible.Text = bll_idioma.Traducir("FrmGestionTecnicos.lblDisponible");
            btnCrear.Text = bll_idioma.Traducir("ABM.btnCrear");
            btnModificar.Text = bll_idioma.Traducir("ABM.btnModificar");
            btnEliminar.Text = bll_idioma.Traducir("ABM.btnEliminar");
            btnAplicar.Text = bll_idioma.Traducir("ABM.btnAplicar");
            btnCancelar.Text = bll_idioma.Traducir("ABM.btnCancelar");
            btnSalir.Text = bll_idioma.Traducir("ABM.btnSalir");

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
            bll_idioma.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}
