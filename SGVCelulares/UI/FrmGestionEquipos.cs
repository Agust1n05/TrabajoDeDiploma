using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using Servicio;

namespace UI
{
    public partial class FrmGestionEquipos : Form, IObservadorIdioma
    {
        private readonly BLL_Equipo bll_equipo = new BLL_Equipo();
        private readonly BLL_Idioma bll_idioma = new BLL_Idioma();
        private List<BE_Equipo> equipos = new List<BE_Equipo>();
        private ModoEdicion modo = ModoEdicion.Consulta;

        public FrmGestionEquipos()
        {
            InitializeComponent();
            bll_idioma.Suscribir(this);
        }

        private void FrmGestionEquipos_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla();
            ActualizarIdioma();
            CargarEquipos();
            CambiarModo(ModoEdicion.Consulta);
        }

        private BE_Equipo? EquipoSeleccionado => grilla.CurrentRow?.DataBoundItem as BE_Equipo;

        private void ConfigurarGrilla()
        {
            grilla.AutoGenerateColumns = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.Columns.Clear();
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", DataPropertyName = "IdEquipo", FillWeight = 40 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMarca", DataPropertyName = "Marca" });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colModelo", DataPropertyName = "Modelo" });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colImei", DataPropertyName = "Imei" });
        }

        private void CargarEquipos()
        {
            try
            {
                equipos = bll_equipo.Consultar();
            }
            catch (Exception ex)
            {
                equipos = new List<BE_Equipo>();
                MostrarError(ex.Message);
            }
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            string filtro = txtBuscar.Text.Trim();
            List<BE_Equipo> resultado = string.IsNullOrEmpty(filtro)
                ? equipos
                : equipos.Where(eq => eq.Imei.Contains(filtro)
                                   || eq.Modelo.Contains(filtro, StringComparison.OrdinalIgnoreCase)
                                   || eq.Marca.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();

            grilla.DataSource = null;
            grilla.DataSource = resultado;
            lblNumero.Text = resultado.Count.ToString();
            MostrarEquipo(EquipoSeleccionado);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e) => AplicarFiltro();

        private void grilla_SelectionChanged(object sender, EventArgs e)
        {
            if (modo == ModoEdicion.Consulta)
                MostrarEquipo(EquipoSeleccionado);
        }

        private void MostrarEquipo(BE_Equipo? equipo)
        {
            txtMarca.Text = equipo?.Marca ?? "";
            txtModelo.Text = equipo?.Modelo ?? "";
            txtImei.Text = equipo?.Imei ?? "";
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoEdicion.Alta);
            MostrarEquipo(null);
            txtMarca.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (EquipoSeleccionado == null)
            {
                MostrarAtencion(bll_idioma.Traducir("ABM.msgSeleccione"));
                return;
            }
            CambiarModo(ModoEdicion.Modificacion);
            txtMarca.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            BE_Equipo? equipo = EquipoSeleccionado;
            if (equipo == null)
            {
                MostrarAtencion(bll_idioma.Traducir("ABM.msgSeleccione"));
                return;
            }

            string descripcion = $"{equipo.Marca} {equipo.Modelo} (IMEI {equipo.Imei})";
            if (!Confirmar(string.Format(bll_idioma.Traducir("ABM.msgConfirmarEliminar"), descripcion)))
                return;

            try
            {
                bll_equipo.Borrar(equipo);
                MostrarExito(bll_idioma.Traducir("ABM.msgEliminado"));
                CargarEquipos();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (modo == ModoEdicion.Consulta) return;

            int idEquipo = modo == ModoEdicion.Modificacion ? EquipoSeleccionado?.IdEquipo ?? 0 : 0;
            var equipo = new BE_Equipo(idEquipo, txtMarca.Text, txtModelo.Text, txtImei.Text);
            try
            {
                if (modo == ModoEdicion.Alta)
                {
                    bool reactivar = bll_equipo.EstaDadoDeBaja(equipo.Imei);
                    if (reactivar && !Confirmar(string.Format(bll_idioma.Traducir("ABM.msgReactivar"), lblImei.Text)))
                        return;

                    bll_equipo.Agregar(equipo);
                    MostrarExito(bll_idioma.Traducir(reactivar ? "ABM.msgReactivado" : "ABM.msgCreado"));
                }
                else
                {
                    bll_equipo.Modificar(equipo);
                    MostrarExito(bll_idioma.Traducir("ABM.msgModificado"));
                }

                CambiarModo(ModoEdicion.Consulta);
                CargarEquipos();
                SeleccionarFila(equipo.IdEquipo);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoEdicion.Consulta);
            MostrarEquipo(EquipoSeleccionado);
        }

        private void btnSalir_Click(object sender, EventArgs e) => Close();

        private void CambiarModo(ModoEdicion nuevoModo)
        {
            modo = nuevoModo;
            bool editando = modo != ModoEdicion.Consulta;

            txtMarca.ReadOnly = !editando;
            txtModelo.ReadOnly = !editando;
            txtImei.ReadOnly = !editando;

            grilla.Enabled = !editando;
            txtBuscar.Enabled = !editando;
            btnCrear.Enabled = !editando;
            btnModificar.Enabled = !editando;
            btnEliminar.Enabled = !editando;
            btnAplicar.Enabled = editando;
            btnCancelar.Enabled = editando;
        }

        private void SeleccionarFila(int idEquipo)
        {
            foreach (DataGridViewRow fila in grilla.Rows)
            {
                if (fila.DataBoundItem is BE_Equipo eq && eq.IdEquipo == idEquipo)
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
            Text = bll_idioma.Traducir("FrmGestionEquipos.Form");
            lblTitulo.Text = bll_idioma.Traducir("FrmGestionEquipos.lblTitulo");
            lblCantidad.Text = bll_idioma.Traducir("FrmGestionEquipos.lblCantidad");
            lblFiltro.Text = bll_idioma.Traducir("ABM.lblFiltro");
            lblBuscar.Text = bll_idioma.Traducir("FrmGestionEquipos.lblBuscar");
            lblMarca.Text = bll_idioma.Traducir("FrmGestionEquipos.lblMarca");
            lblModelo.Text = bll_idioma.Traducir("FrmGestionEquipos.lblModelo");
            lblImei.Text = bll_idioma.Traducir("FrmGestionEquipos.lblImei");
            btnCrear.Text = bll_idioma.Traducir("ABM.btnCrear");
            btnModificar.Text = bll_idioma.Traducir("ABM.btnModificar");
            btnEliminar.Text = bll_idioma.Traducir("ABM.btnEliminar");
            btnAplicar.Text = bll_idioma.Traducir("ABM.btnAplicar");
            btnCancelar.Text = bll_idioma.Traducir("ABM.btnCancelar");
            btnSalir.Text = bll_idioma.Traducir("ABM.btnSalir");

            if (grilla.Columns.Count > 0)
            {
                grilla.Columns["colId"].HeaderText = bll_idioma.Traducir("FrmGestionEquipos.colId");
                grilla.Columns["colMarca"].HeaderText = lblMarca.Text;
                grilla.Columns["colModelo"].HeaderText = lblModelo.Text;
                grilla.Columns["colImei"].HeaderText = lblImei.Text;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}
