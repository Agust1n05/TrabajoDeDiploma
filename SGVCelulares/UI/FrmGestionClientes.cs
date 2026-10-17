using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using Servicio;

namespace UI
{
    public partial class FrmGestionClientes : Form, IObservadorIdioma
    {
        private readonly BLL_Cliente bll_cliente = new BLL_Cliente();
        private readonly BLL_Idioma bll_idioma = new BLL_Idioma();
        private List<BE_Cliente> clientes = new List<BE_Cliente>();
        private ModoEdicion modo = ModoEdicion.Consulta;

        /// <summary>
        /// DNI con el que se abrió el formulario desde la recepción de equipo (CU-NEG-6).
        /// En ese caso el formulario arranca en alta con el DNI cargado y se cierra solo al guardar.
        /// </summary>
        private readonly string? dniParaAlta;

        public FrmGestionClientes()
        {
            InitializeComponent();
            bll_idioma.Suscribir(this);
        }

        /// <summary>Abre el ABM directamente en alta, con el DNI cargado (CU-NEG-6 desde la recepción).</summary>
        public FrmGestionClientes(string dniParaAlta) : this()
        {
            this.dniParaAlta = dniParaAlta;
        }

        private void FrmGestionClientes_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla();
            ActualizarIdioma();
            CargarClientes();
            CambiarModo(ModoEdicion.Consulta);

            if (!string.IsNullOrWhiteSpace(dniParaAlta))
            {
                CambiarModo(ModoEdicion.Alta);
                MostrarCliente(null);
                txtDni.Text = dniParaAlta;
                txtDni.ReadOnly = true;
                txtNombre.Focus();
            }
        }

        private BE_Cliente? ClienteSeleccionado => grilla.CurrentRow?.DataBoundItem as BE_Cliente;

        private void ConfigurarGrilla()
        {
            grilla.AutoGenerateColumns = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.Columns.Clear();
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDni", DataPropertyName = "Dni" });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNombre", DataPropertyName = "Nombre" });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colApellido", DataPropertyName = "Apellido" });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTelefono", DataPropertyName = "Telefono" });
        }

        private void CargarClientes()
        {
            try
            {
                clientes = bll_cliente.Consultar();
            }
            catch (Exception ex)
            {
                clientes = new List<BE_Cliente>();
                MostrarError(ex.Message);
            }
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            string filtro = txtBuscar.Text.Trim();
            List<BE_Cliente> resultado = string.IsNullOrEmpty(filtro)
                ? clientes
                : clientes.Where(c => c.Dni.Contains(filtro)).ToList();

            grilla.DataSource = null;
            grilla.DataSource = resultado;
            lblNumero.Text = resultado.Count.ToString();
            MostrarCliente(ClienteSeleccionado);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e) => AplicarFiltro();

        private void grilla_SelectionChanged(object sender, EventArgs e)
        {
            if (modo == ModoEdicion.Consulta)
                MostrarCliente(ClienteSeleccionado);
        }

        private void MostrarCliente(BE_Cliente? cliente)
        {
            txtDni.Text = cliente?.Dni ?? "";
            txtNombre.Text = cliente?.Nombre ?? "";
            txtApellido.Text = cliente?.Apellido ?? "";
            txtTelefono.Text = cliente?.Telefono ?? "";
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoEdicion.Alta);
            MostrarCliente(null);
            txtDni.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (ClienteSeleccionado == null)
            {
                MostrarAtencion(bll_idioma.Traducir("ABM.msgSeleccione"));
                return;
            }
            CambiarModo(ModoEdicion.Modificacion);
            txtNombre.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            BE_Cliente? cliente = ClienteSeleccionado;
            if (cliente == null)
            {
                MostrarAtencion(bll_idioma.Traducir("ABM.msgSeleccione"));
                return;
            }

            string descripcion = $"{cliente.Nombre} {cliente.Apellido} ({cliente.Dni})";
            if (!Confirmar(string.Format(bll_idioma.Traducir("ABM.msgConfirmarEliminar"), descripcion)))
                return;

            try
            {
                bll_cliente.Borrar(cliente);
                MostrarExito(bll_idioma.Traducir("ABM.msgEliminado"));
                CargarClientes();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (modo == ModoEdicion.Consulta) return;

            var cliente = new BE_Cliente(txtDni.Text, txtNombre.Text, txtApellido.Text, txtTelefono.Text);
            try
            {
                if (modo == ModoEdicion.Alta)
                {
                    bool reactivar = bll_cliente.EstaDadoDeBaja(cliente.Dni.Trim());
                    if (reactivar && !Confirmar(string.Format(bll_idioma.Traducir("ABM.msgReactivar"), lblDni.Text)))
                        return;

                    bll_cliente.Agregar(cliente);
                    MostrarExito(bll_idioma.Traducir(reactivar ? "ABM.msgReactivado" : "ABM.msgCreado"));
                }
                else
                {
                    bll_cliente.Modificar(cliente);
                    MostrarExito(bll_idioma.Traducir("ABM.msgModificado"));
                }

                if (dniParaAlta != null)
                {
                    // Se abrió desde la recepción: vuelve a ella con el cliente ya registrado.
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                CambiarModo(ModoEdicion.Consulta);
                CargarClientes();
                SeleccionarFila(cliente.Dni);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
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
            CambiarModo(ModoEdicion.Consulta);
            MostrarCliente(ClienteSeleccionado);
        }

        private void btnSalir_Click(object sender, EventArgs e) => Close();

        private void CambiarModo(ModoEdicion nuevoModo)
        {
            modo = nuevoModo;
            bool editando = modo != ModoEdicion.Consulta;

            txtDni.ReadOnly = modo != ModoEdicion.Alta;
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

        private void SeleccionarFila(string dni)
        {
            foreach (DataGridViewRow fila in grilla.Rows)
            {
                if (fila.DataBoundItem is BE_Cliente c && c.Dni == dni.Trim())
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
            Text = bll_idioma.Traducir("FrmGestionClientes.Form");
            lblTitulo.Text = bll_idioma.Traducir("FrmGestionClientes.lblTitulo");
            lblCantidad.Text = bll_idioma.Traducir("FrmGestionClientes.lblCantidad");
            lblFiltro.Text = bll_idioma.Traducir("ABM.lblFiltro");
            lblBuscar.Text = bll_idioma.Traducir("FrmGestionClientes.lblBuscar");
            lblDni.Text = bll_idioma.Traducir("FrmGestionClientes.lblDni");
            lblNombre.Text = bll_idioma.Traducir("FrmGestionClientes.lblNombre");
            lblApellido.Text = bll_idioma.Traducir("FrmGestionClientes.lblApellido");
            lblTelefono.Text = bll_idioma.Traducir("FrmGestionClientes.lblTelefono");
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
                grilla.Columns["colTelefono"].HeaderText = lblTelefono.Text;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}
