using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using Servicio;

namespace UI
{
    public partial class FrmGestionRepuestos_382QA : Form, IObservadorIdioma_382QA
    {
        private readonly BLL_Repuesto_382QA bll_repuesto = new BLL_Repuesto_382QA();
        private readonly BLL_Idioma_382QA bll_idioma = new BLL_Idioma_382QA();
        private List<BE_Repuesto_382QA> repuestos = new List<BE_Repuesto_382QA>();
        private ModoEdicion_382QA modo = ModoEdicion_382QA.Consulta;

        public FrmGestionRepuestos_382QA()
        {
            InitializeComponent();
            bll_idioma.Suscribir_382QA(this);
        }

        private void FrmGestionRepuestos_Load(object sender, EventArgs e)
        {
            txtCodigo.CharacterCasing = CharacterCasing.Upper;
            ConfigurarGrilla_382QA();
            ActualizarIdioma_382QA();
            CargarRepuestos_382QA();
            CambiarModo_382QA(ModoEdicion_382QA.Consulta);
        }

        private BE_Repuesto_382QA? RepuestoSeleccionado => grilla.CurrentRow?.DataBoundItem as BE_Repuesto_382QA;

        private void ConfigurarGrilla_382QA()
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

        private void CargarRepuestos_382QA()
        {
            try
            {
                repuestos = bll_repuesto.Consultar_382QA();
            }
            catch (Exception ex)
            {
                repuestos = new List<BE_Repuesto_382QA>();
                MostrarError_382QA(ex.Message);
            }
            AplicarFiltro_382QA();
        }

        private void AplicarFiltro_382QA()
        {
            string filtro = txtBuscar.Text.Trim();
            List<BE_Repuesto_382QA> resultado = string.IsNullOrEmpty(filtro)
                ? repuestos
                : repuestos.Where(r => r.Codigo.Contains(filtro, StringComparison.OrdinalIgnoreCase)
                                    || r.Descripcion.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();

            grilla.DataSource = null;
            grilla.DataSource = resultado;
            lblNumero.Text = resultado.Count.ToString();
            MostrarRepuesto_382QA(RepuestoSeleccionado);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e) => AplicarFiltro_382QA();

        private void grilla_SelectionChanged(object sender, EventArgs e)
        {
            if (modo == ModoEdicion_382QA.Consulta)
                MostrarRepuesto_382QA(RepuestoSeleccionado);
        }

        private void MostrarRepuesto_382QA(BE_Repuesto_382QA? repuesto)
        {
            txtCodigo.Text = repuesto?.Codigo ?? "";
            txtDescripcion.Text = repuesto?.Descripcion ?? "";
            nudPrecio.Value = Math.Min(repuesto?.PrecioUnitario ?? 0, nudPrecio.Maximum);
            nudStock.Value = Math.Min(repuesto?.StockDisponible ?? 0, nudStock.Maximum);
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            CambiarModo_382QA(ModoEdicion_382QA.Alta);
            MostrarRepuesto_382QA(null);
            txtCodigo.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (RepuestoSeleccionado == null)
            {
                MostrarAtencion_382QA(bll_idioma.Traducir_382QA("ABM.msgSeleccione"));
                return;
            }
            CambiarModo_382QA(ModoEdicion_382QA.Modificacion);
            txtDescripcion.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            BE_Repuesto_382QA? repuesto = RepuestoSeleccionado;
            if (repuesto == null)
            {
                MostrarAtencion_382QA(bll_idioma.Traducir_382QA("ABM.msgSeleccione"));
                return;
            }

            string descripcion = $"{repuesto.Codigo} - {repuesto.Descripcion}";
            if (!Confirmar_382QA(string.Format(bll_idioma.Traducir_382QA("ABM.msgConfirmarEliminar"), descripcion)))
                return;

            try
            {
                bll_repuesto.Borrar_382QA(repuesto);
                MostrarExito_382QA(bll_idioma.Traducir_382QA("ABM.msgEliminado"));
                CargarRepuestos_382QA();
            }
            catch (Exception ex)
            {
                MostrarError_382QA(ex.Message);
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (modo == ModoEdicion_382QA.Consulta) return;

            var repuesto = new BE_Repuesto_382QA(txtCodigo.Text, txtDescripcion.Text, nudPrecio.Value, (int)nudStock.Value);
            try
            {
                if (modo == ModoEdicion_382QA.Alta)
                {
                    bool reactivar = bll_repuesto.EstaDadoDeBaja_382QA(repuesto.Codigo);
                    if (reactivar && !Confirmar_382QA(string.Format(bll_idioma.Traducir_382QA("ABM.msgReactivar"), lblCodigo.Text)))
                        return;

                    bll_repuesto.Agregar_382QA(repuesto);
                    MostrarExito_382QA(bll_idioma.Traducir_382QA(reactivar ? "ABM.msgReactivado" : "ABM.msgCreado"));
                }
                else
                {
                    bll_repuesto.Modificar_382QA(repuesto, RepuestoSeleccionado!.StockDisponible);
                    MostrarExito_382QA(bll_idioma.Traducir_382QA("ABM.msgModificado"));
                }

                CambiarModo_382QA(ModoEdicion_382QA.Consulta);
                CargarRepuestos_382QA();
                SeleccionarFila_382QA(repuesto.Codigo);
            }
            catch (Exception ex)
            {
                MostrarError_382QA(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string? codigo = RepuestoSeleccionado?.Codigo;
            CambiarModo_382QA(ModoEdicion_382QA.Consulta);
            // Se recarga para que, si el stock cambió mientras se editaba, se vea el valor actual.
            CargarRepuestos_382QA();
            if (codigo != null) SeleccionarFila_382QA(codigo);
        }

        private void btnSalir_Click(object sender, EventArgs e) => Close();

        private void CambiarModo_382QA(ModoEdicion_382QA nuevoModo)
        {
            modo = nuevoModo;
            bool editando = modo != ModoEdicion_382QA.Consulta;

            txtCodigo.ReadOnly = modo != ModoEdicion_382QA.Alta;
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

        private void SeleccionarFila_382QA(string codigo)
        {
            foreach (DataGridViewRow fila in grilla.Rows)
            {
                if (fila.DataBoundItem is BE_Repuesto_382QA r && r.Codigo == codigo)
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
            Text = bll_idioma.Traducir_382QA("FrmGestionRepuestos.Form");
            lblTitulo.Text = bll_idioma.Traducir_382QA("FrmGestionRepuestos.lblTitulo");
            lblCantidad.Text = bll_idioma.Traducir_382QA("FrmGestionRepuestos.lblCantidad");
            lblFiltro.Text = bll_idioma.Traducir_382QA("ABM.lblFiltro");
            lblBuscar.Text = bll_idioma.Traducir_382QA("FrmGestionRepuestos.lblBuscar");
            lblCodigo.Text = bll_idioma.Traducir_382QA("FrmGestionRepuestos.lblCodigo");
            lblDescripcion.Text = bll_idioma.Traducir_382QA("FrmGestionRepuestos.lblDescripcion");
            lblPrecio.Text = bll_idioma.Traducir_382QA("FrmGestionRepuestos.lblPrecio");
            lblStock.Text = bll_idioma.Traducir_382QA("FrmGestionRepuestos.lblStock");
            btnCrear.Text = bll_idioma.Traducir_382QA("ABM.btnCrear");
            btnModificar.Text = bll_idioma.Traducir_382QA("ABM.btnModificar");
            btnEliminar.Text = bll_idioma.Traducir_382QA("ABM.btnEliminar");
            btnAplicar.Text = bll_idioma.Traducir_382QA("ABM.btnAplicar");
            btnCancelar.Text = bll_idioma.Traducir_382QA("ABM.btnCancelar");
            btnSalir.Text = bll_idioma.Traducir_382QA("ABM.btnSalir");

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
            bll_idioma.Desuscribir_382QA(this);
            base.OnFormClosed(e);
        }
    }
}
