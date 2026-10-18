using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using Servicio;

namespace UI
{
    public partial class FrmGestionRepuestos : Form, IObservadorIdioma
    {
        private readonly BLL_Repuesto bll_repuesto = new BLL_Repuesto();
        private readonly BLL_Idioma bll_idioma = new BLL_Idioma();
        private List<BE_Repuesto> repuestos = new List<BE_Repuesto>();
        private ModoEdicion modo = ModoEdicion.Consulta;

        public FrmGestionRepuestos()
        {
            InitializeComponent();
            bll_idioma.Suscribir(this);
        }

        private void FrmGestionRepuestos_Load(object sender, EventArgs e)
        {
            txtCodigo.CharacterCasing = CharacterCasing.Upper;
            ConfigurarGrilla();
            ActualizarIdioma();
            CargarRepuestos();
            CambiarModo(ModoEdicion.Consulta);
        }

        private BE_Repuesto? RepuestoSeleccionado => grilla.CurrentRow?.DataBoundItem as BE_Repuesto;

        private void ConfigurarGrilla()
        {
            grilla.AutoGenerateColumns = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.Columns.Clear();
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCodigo", DataPropertyName = "Codigo", FillWeight = 60 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDescripcion", DataPropertyName = "Descripcion", FillWeight = 180 });

            var colPrecio = new DataGridViewTextBoxColumn { Name = "colPrecio", DataPropertyName = "PrecioUnitario", FillWeight = 70 };
            colPrecio.DefaultCellStyle.Format = "N2";
            colPrecio.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grilla.Columns.Add(colPrecio);

            var colStock = new DataGridViewTextBoxColumn { Name = "colStock", DataPropertyName = "StockDisponible", FillWeight = 50 };
            colStock.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grilla.Columns.Add(colStock);
        }

        private void CargarRepuestos()
        {
            try
            {
                repuestos = bll_repuesto.Consultar();
            }
            catch (Exception ex)
            {
                repuestos = new List<BE_Repuesto>();
                MostrarError(ex.Message);
            }
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            string filtro = txtBuscar.Text.Trim();
            List<BE_Repuesto> resultado = string.IsNullOrEmpty(filtro)
                ? repuestos
                : repuestos.Where(r => r.Codigo.Contains(filtro, StringComparison.OrdinalIgnoreCase)
                                    || r.Descripcion.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();

            grilla.DataSource = null;
            grilla.DataSource = resultado;
            lblNumero.Text = resultado.Count.ToString();
            MostrarRepuesto(RepuestoSeleccionado);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e) => AplicarFiltro();

        private void grilla_SelectionChanged(object sender, EventArgs e)
        {
            if (modo == ModoEdicion.Consulta)
                MostrarRepuesto(RepuestoSeleccionado);
        }

        private void MostrarRepuesto(BE_Repuesto? repuesto)
        {
            txtCodigo.Text = repuesto?.Codigo ?? "";
            txtDescripcion.Text = repuesto?.Descripcion ?? "";
            nudPrecio.Value = Math.Min(repuesto?.PrecioUnitario ?? 0, nudPrecio.Maximum);
            nudStock.Value = Math.Min(repuesto?.StockDisponible ?? 0, nudStock.Maximum);
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoEdicion.Alta);
            MostrarRepuesto(null);
            txtCodigo.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (RepuestoSeleccionado == null)
            {
                MostrarAtencion(bll_idioma.Traducir("ABM.msgSeleccione"));
                return;
            }
            CambiarModo(ModoEdicion.Modificacion);
            txtDescripcion.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            BE_Repuesto? repuesto = RepuestoSeleccionado;
            if (repuesto == null)
            {
                MostrarAtencion(bll_idioma.Traducir("ABM.msgSeleccione"));
                return;
            }

            string descripcion = $"{repuesto.Codigo} - {repuesto.Descripcion}";
            if (!Confirmar(string.Format(bll_idioma.Traducir("ABM.msgConfirmarEliminar"), descripcion)))
                return;

            try
            {
                bll_repuesto.Borrar(repuesto);
                MostrarExito(bll_idioma.Traducir("ABM.msgEliminado"));
                CargarRepuestos();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (modo == ModoEdicion.Consulta) return;

            var repuesto = new BE_Repuesto(txtCodigo.Text, txtDescripcion.Text, nudPrecio.Value, (int)nudStock.Value);
            try
            {
                if (modo == ModoEdicion.Alta)
                {
                    bool reactivar = bll_repuesto.EstaDadoDeBaja(repuesto.Codigo);
                    if (reactivar && !Confirmar(string.Format(bll_idioma.Traducir("ABM.msgReactivar"), lblCodigo.Text)))
                        return;

                    bll_repuesto.Agregar(repuesto);
                    MostrarExito(bll_idioma.Traducir(reactivar ? "ABM.msgReactivado" : "ABM.msgCreado"));
                }
                else
                {
                    bll_repuesto.Modificar(repuesto, RepuestoSeleccionado!.StockDisponible);
                    MostrarExito(bll_idioma.Traducir("ABM.msgModificado"));
                }

                CambiarModo(ModoEdicion.Consulta);
                CargarRepuestos();
                SeleccionarFila(repuesto.Codigo);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string? codigo = RepuestoSeleccionado?.Codigo;
            CambiarModo(ModoEdicion.Consulta);
            // Se recarga para que, si el stock cambió mientras se editaba, se vea el valor actual.
            CargarRepuestos();
            if (codigo != null) SeleccionarFila(codigo);
        }

        private void btnSalir_Click(object sender, EventArgs e) => Close();

        private void CambiarModo(ModoEdicion nuevoModo)
        {
            modo = nuevoModo;
            bool editando = modo != ModoEdicion.Consulta;

            txtCodigo.ReadOnly = modo != ModoEdicion.Alta;
            txtDescripcion.ReadOnly = !editando;
            nudPrecio.Enabled = editando;
            nudStock.Enabled = editando;

            grilla.Enabled = !editando;
            txtBuscar.Enabled = !editando;
            btnCrear.Enabled = !editando;
            btnModificar.Enabled = !editando;
            btnEliminar.Enabled = !editando;
            btnAplicar.Enabled = editando;
            btnCancelar.Enabled = editando;
        }

        private void SeleccionarFila(string codigo)
        {
            foreach (DataGridViewRow fila in grilla.Rows)
            {
                if (fila.DataBoundItem is BE_Repuesto r && r.Codigo == codigo)
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
            Text = bll_idioma.Traducir("FrmGestionRepuestos.Form");
            lblTitulo.Text = bll_idioma.Traducir("FrmGestionRepuestos.lblTitulo");
            lblCantidad.Text = bll_idioma.Traducir("FrmGestionRepuestos.lblCantidad");
            lblFiltro.Text = bll_idioma.Traducir("ABM.lblFiltro");
            lblBuscar.Text = bll_idioma.Traducir("FrmGestionRepuestos.lblBuscar");
            lblCodigo.Text = bll_idioma.Traducir("FrmGestionRepuestos.lblCodigo");
            lblDescripcion.Text = bll_idioma.Traducir("FrmGestionRepuestos.lblDescripcion");
            lblPrecio.Text = bll_idioma.Traducir("FrmGestionRepuestos.lblPrecio");
            lblStock.Text = bll_idioma.Traducir("FrmGestionRepuestos.lblStock");
            btnCrear.Text = bll_idioma.Traducir("ABM.btnCrear");
            btnModificar.Text = bll_idioma.Traducir("ABM.btnModificar");
            btnEliminar.Text = bll_idioma.Traducir("ABM.btnEliminar");
            btnAplicar.Text = bll_idioma.Traducir("ABM.btnAplicar");
            btnCancelar.Text = bll_idioma.Traducir("ABM.btnCancelar");
            btnSalir.Text = bll_idioma.Traducir("ABM.btnSalir");

            if (grilla.Columns.Count > 0)
            {
                grilla.Columns["colCodigo"].HeaderText = lblCodigo.Text;
                grilla.Columns["colDescripcion"].HeaderText = lblDescripcion.Text;
                grilla.Columns["colPrecio"].HeaderText = lblPrecio.Text;
                grilla.Columns["colStock"].HeaderText = lblStock.Text;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}
