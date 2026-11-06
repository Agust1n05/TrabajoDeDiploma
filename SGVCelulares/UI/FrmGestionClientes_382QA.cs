using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using Servicio;

namespace UI
{
    public partial class FrmGestionClientes_382QA : Form, IObservadorIdioma_382QA
    {
        private readonly BLL_Cliente_382QA bll_cliente = new BLL_Cliente_382QA();
        private readonly BLL_Idioma_382QA bll_idioma = new BLL_Idioma_382QA();
        private List<BE_Cliente_382QA> clientes = new List<BE_Cliente_382QA>();
        private ModoEdicion_382QA modo = ModoEdicion_382QA.Consulta;

        /// <summary>
        /// DNI con el que se abrió el formulario desde la recepción de equipo (CU-NEG-6).
        /// En ese caso el formulario arranca en alta con el DNI cargado y se cierra solo al guardar.
        /// </summary>
        private readonly string? dniParaAlta;

        public FrmGestionClientes_382QA()
        {
            InitializeComponent();
            bll_idioma.Suscribir_382QA(this);
        }

        /// <summary>Abre el ABM directamente en alta, con el DNI cargado (CU-NEG-6 desde la recepción).</summary>
        public FrmGestionClientes_382QA(string dniParaAlta) : this()
        {
            this.dniParaAlta = dniParaAlta;
        }

        private void FrmGestionClientes_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla_382QA();
            ActualizarIdioma_382QA();
            CargarClientes_382QA();
            CambiarModo_382QA(ModoEdicion_382QA.Consulta);

            if (!string.IsNullOrWhiteSpace(dniParaAlta))
            {
                CambiarModo_382QA(ModoEdicion_382QA.Alta);
                MostrarCliente_382QA(null);
                txtDni.Text = dniParaAlta;
                txtDni.ReadOnly = true;
                txtNombre.Focus();
            }
        }

        private BE_Cliente_382QA? ClienteSeleccionado => grilla.CurrentRow?.DataBoundItem as BE_Cliente_382QA;

        private void ConfigurarGrilla_382QA()
        {
            grilla.AutoGenerateColumns = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.Columns.Clear();
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDni", DataPropertyName = "Dni" });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNombre", DataPropertyName = "Nombre" });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colApellido", DataPropertyName = "Apellido" });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTelefono", DataPropertyName = "Telefono" });
        }

        private void CargarClientes_382QA()
        {
            try
            {
                clientes = bll_cliente.Consultar_382QA();
            }
            catch (Exception ex)
            {
                clientes = new List<BE_Cliente_382QA>();
                MostrarError_382QA(ex.Message);
            }
            AplicarFiltro_382QA();
        }

        private void AplicarFiltro_382QA()
        {
            string filtro = txtBuscar.Text.Trim();
            List<BE_Cliente_382QA> resultado = string.IsNullOrEmpty(filtro)
                ? clientes
                : clientes.Where(c => c.Dni.Contains(filtro)).ToList();

            grilla.DataSource = null;
            grilla.DataSource = resultado;
            lblNumero.Text = resultado.Count.ToString();
            MostrarCliente_382QA(ClienteSeleccionado);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e) => AplicarFiltro_382QA();

        private void grilla_SelectionChanged(object sender, EventArgs e)
        {
            if (modo == ModoEdicion_382QA.Consulta)
                MostrarCliente_382QA(ClienteSeleccionado);
        }

        private void MostrarCliente_382QA(BE_Cliente_382QA? cliente)
        {
            txtDni.Text = cliente?.Dni ?? "";
            txtNombre.Text = cliente?.Nombre ?? "";
            txtApellido.Text = cliente?.Apellido ?? "";
            txtTelefono.Text = cliente?.Telefono ?? "";
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            CambiarModo_382QA(ModoEdicion_382QA.Alta);
            MostrarCliente_382QA(null);
            txtDni.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (ClienteSeleccionado == null)
            {
                MostrarAtencion_382QA(bll_idioma.Traducir_382QA("ABM.msgSeleccione"));
                return;
            }
            CambiarModo_382QA(ModoEdicion_382QA.Modificacion);
            txtNombre.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            BE_Cliente_382QA? cliente = ClienteSeleccionado;
            if (cliente == null)
            {
                MostrarAtencion_382QA(bll_idioma.Traducir_382QA("ABM.msgSeleccione"));
                return;
            }

            string descripcion = $"{cliente.Nombre} {cliente.Apellido} ({cliente.Dni})";
            if (!Confirmar_382QA(string.Format(bll_idioma.Traducir_382QA("ABM.msgConfirmarEliminar"), descripcion)))
                return;

            try
            {
                bll_cliente.Borrar_382QA(cliente);
                MostrarExito_382QA(bll_idioma.Traducir_382QA("ABM.msgEliminado"));
                CargarClientes_382QA();
            }
            catch (Exception ex)
            {
                MostrarError_382QA(ex.Message);
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (modo == ModoEdicion_382QA.Consulta) return;

            var cliente = new BE_Cliente_382QA(txtDni.Text, txtNombre.Text, txtApellido.Text, txtTelefono.Text);
            try
            {
                if (modo == ModoEdicion_382QA.Alta)
                {
                    bool reactivar = bll_cliente.EstaDadoDeBaja_382QA(cliente.Dni.Trim());
                    if (reactivar && !Confirmar_382QA(string.Format(bll_idioma.Traducir_382QA("ABM.msgReactivar"), lblDni.Text)))
                        return;

                    bll_cliente.Agregar_382QA(cliente);
                    MostrarExito_382QA(bll_idioma.Traducir_382QA(reactivar ? "ABM.msgReactivado" : "ABM.msgCreado"));
                }
                else
                {
                    bll_cliente.Modificar_382QA(cliente);
                    MostrarExito_382QA(bll_idioma.Traducir_382QA("ABM.msgModificado"));
                }

                if (dniParaAlta != null)
                {
                    // Se abrió desde la recepción: vuelve a ella con el cliente ya registrado.
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                CambiarModo_382QA(ModoEdicion_382QA.Consulta);
                CargarClientes_382QA();
                SeleccionarFila_382QA(cliente.Dni);
            }
            catch (Exception ex)
            {
                MostrarError_382QA(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (dniParaAlta != null)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }
            CambiarModo_382QA(ModoEdicion_382QA.Consulta);
            MostrarCliente_382QA(ClienteSeleccionado);
        }

        private void btnSalir_Click(object sender, EventArgs e) => Close();

        private void CambiarModo_382QA(ModoEdicion_382QA nuevoModo)
        {
            modo = nuevoModo;
            bool editando = modo != ModoEdicion_382QA.Consulta;

            txtDni.ReadOnly = modo != ModoEdicion_382QA.Alta;
            txtNombre.ReadOnly = !editando;
            txtApellido.ReadOnly = !editando;
            txtTelefono.ReadOnly = !editando;

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
                if (fila.DataBoundItem is BE_Cliente_382QA c && c.Dni == dni.Trim())
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
            Text = bll_idioma.Traducir_382QA("FrmGestionClientes.Form");
            lblTitulo.Text = bll_idioma.Traducir_382QA("FrmGestionClientes.lblTitulo");
            lblCantidad.Text = bll_idioma.Traducir_382QA("FrmGestionClientes.lblCantidad");
            lblFiltro.Text = bll_idioma.Traducir_382QA("ABM.lblFiltro");
            lblBuscar.Text = bll_idioma.Traducir_382QA("FrmGestionClientes.lblBuscar");
            lblDni.Text = bll_idioma.Traducir_382QA("FrmGestionClientes.lblDni");
            lblNombre.Text = bll_idioma.Traducir_382QA("FrmGestionClientes.lblNombre");
            lblApellido.Text = bll_idioma.Traducir_382QA("FrmGestionClientes.lblApellido");
            lblTelefono.Text = bll_idioma.Traducir_382QA("FrmGestionClientes.lblTelefono");
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
                grilla.Columns["colTelefono"].HeaderText = lblTelefono.Text;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_382QA(this);
            base.OnFormClosed(e);
        }
    }
}
