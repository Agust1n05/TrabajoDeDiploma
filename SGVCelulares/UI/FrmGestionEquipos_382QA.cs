using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using Servicio;

namespace UI
{
    public partial class FrmGestionEquipos_382QA : Form, IObservadorIdioma_382QA
    {
        private readonly BLL_Equipo_382QA bll_equipo = new BLL_Equipo_382QA();
        private readonly BLL_Idioma_382QA bll_idioma = new BLL_Idioma_382QA();
        private List<BE_Equipo_382QA> equipos = new List<BE_Equipo_382QA>();
        private ModoEdicion_382QA modo = ModoEdicion_382QA.Consulta;

        public FrmGestionEquipos_382QA()
        {
            InitializeComponent();
            bll_idioma.Suscribir_382QA(this);
        }

        private void FrmGestionEquipos_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla_382QA();
            ActualizarIdioma_382QA();
            CargarEquipos_382QA();
            CambiarModo_382QA(ModoEdicion_382QA.Consulta);
        }

        private BE_Equipo_382QA? EquipoSeleccionado => grilla.CurrentRow?.DataBoundItem as BE_Equipo_382QA;

        private void ConfigurarGrilla_382QA()
        {
            grilla.AutoGenerateColumns = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.Columns.Clear();
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", DataPropertyName = "IdEquipo", FillWeight = 40 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMarca", DataPropertyName = "Marca" });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colModelo", DataPropertyName = "Modelo" });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colImei", DataPropertyName = "Imei" });
        }

        private void CargarEquipos_382QA()
        {
            try
            {
                equipos = bll_equipo.Consultar_382QA();
            }
            catch (Exception ex)
            {
                equipos = new List<BE_Equipo_382QA>();
                MostrarError_382QA(ex.Message);
            }
            AplicarFiltro_382QA();
        }

        private void AplicarFiltro_382QA()
        {
            string filtro = txtBuscar.Text.Trim();
            List<BE_Equipo_382QA> resultado = string.IsNullOrEmpty(filtro)
                ? equipos
                : equipos.Where(eq => eq.Imei.Contains(filtro)
                                   || eq.Modelo.Contains(filtro, StringComparison.OrdinalIgnoreCase)
                                   || eq.Marca.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();

            grilla.DataSource = null;
            grilla.DataSource = resultado;
            lblNumero.Text = resultado.Count.ToString();
            MostrarEquipo_382QA(EquipoSeleccionado);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e) => AplicarFiltro_382QA();

        private void grilla_SelectionChanged(object sender, EventArgs e)
        {
            if (modo == ModoEdicion_382QA.Consulta)
                MostrarEquipo_382QA(EquipoSeleccionado);
        }

        private void MostrarEquipo_382QA(BE_Equipo_382QA? equipo)
        {
            txtMarca.Text = equipo?.Marca ?? "";
            txtModelo.Text = equipo?.Modelo ?? "";
            txtImei.Text = equipo?.Imei ?? "";
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            CambiarModo_382QA(ModoEdicion_382QA.Alta);
            MostrarEquipo_382QA(null);
            txtMarca.Focus();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (EquipoSeleccionado == null)
            {
                MostrarAtencion_382QA(bll_idioma.Traducir_382QA("ABM.msgSeleccione"));
                return;
            }
            CambiarModo_382QA(ModoEdicion_382QA.Modificacion);
            txtMarca.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            BE_Equipo_382QA? equipo = EquipoSeleccionado;
            if (equipo == null)
            {
                MostrarAtencion_382QA(bll_idioma.Traducir_382QA("ABM.msgSeleccione"));
                return;
            }

            string descripcion = $"{equipo.Marca} {equipo.Modelo} (IMEI {equipo.Imei})";
            if (!Confirmar_382QA(string.Format(bll_idioma.Traducir_382QA("ABM.msgConfirmarEliminar"), descripcion)))
                return;

            try
            {
                bll_equipo.Borrar_382QA(equipo);
                MostrarExito_382QA(bll_idioma.Traducir_382QA("ABM.msgEliminado"));
                CargarEquipos_382QA();
            }
            catch (Exception ex)
            {
                MostrarError_382QA(ex.Message);
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (modo == ModoEdicion_382QA.Consulta) return;

            int idEquipo = modo == ModoEdicion_382QA.Modificacion ? EquipoSeleccionado?.IdEquipo ?? 0 : 0;
            var equipo = new BE_Equipo_382QA(idEquipo, txtMarca.Text, txtModelo.Text, txtImei.Text);
            try
            {
                if (modo == ModoEdicion_382QA.Alta)
                {
                    bool reactivar = bll_equipo.EstaDadoDeBaja_382QA(equipo.Imei);
                    if (reactivar && !Confirmar_382QA(string.Format(bll_idioma.Traducir_382QA("ABM.msgReactivar"), lblImei.Text)))
                        return;

                    bll_equipo.Agregar_382QA(equipo);
                    MostrarExito_382QA(bll_idioma.Traducir_382QA(reactivar ? "ABM.msgReactivado" : "ABM.msgCreado"));
                }
                else
                {
                    bll_equipo.Modificar_382QA(equipo);
                    MostrarExito_382QA(bll_idioma.Traducir_382QA("ABM.msgModificado"));
                }

                CambiarModo_382QA(ModoEdicion_382QA.Consulta);
                CargarEquipos_382QA();
                SeleccionarFila_382QA(equipo.IdEquipo);
            }
            catch (Exception ex)
            {
                MostrarError_382QA(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CambiarModo_382QA(ModoEdicion_382QA.Consulta);
            MostrarEquipo_382QA(EquipoSeleccionado);
        }

        private void btnSalir_Click(object sender, EventArgs e) => Close();

        private void CambiarModo_382QA(ModoEdicion_382QA nuevoModo)
        {
            modo = nuevoModo;
            bool editando = modo != ModoEdicion_382QA.Consulta;

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

        private void SeleccionarFila_382QA(int idEquipo)
        {
            foreach (DataGridViewRow fila in grilla.Rows)
            {
                if (fila.DataBoundItem is BE_Equipo_382QA eq && eq.IdEquipo == idEquipo)
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
            Text = bll_idioma.Traducir_382QA("FrmGestionEquipos.Form");
            lblTitulo.Text = bll_idioma.Traducir_382QA("FrmGestionEquipos.lblTitulo");
            lblCantidad.Text = bll_idioma.Traducir_382QA("FrmGestionEquipos.lblCantidad");
            lblFiltro.Text = bll_idioma.Traducir_382QA("ABM.lblFiltro");
            lblBuscar.Text = bll_idioma.Traducir_382QA("FrmGestionEquipos.lblBuscar");
            lblMarca.Text = bll_idioma.Traducir_382QA("FrmGestionEquipos.lblMarca");
            lblModelo.Text = bll_idioma.Traducir_382QA("FrmGestionEquipos.lblModelo");
            lblImei.Text = bll_idioma.Traducir_382QA("FrmGestionEquipos.lblImei");
            btnCrear.Text = bll_idioma.Traducir_382QA("ABM.btnCrear");
            btnModificar.Text = bll_idioma.Traducir_382QA("ABM.btnModificar");
            btnEliminar.Text = bll_idioma.Traducir_382QA("ABM.btnEliminar");
            btnAplicar.Text = bll_idioma.Traducir_382QA("ABM.btnAplicar");
            btnCancelar.Text = bll_idioma.Traducir_382QA("ABM.btnCancelar");
            btnSalir.Text = bll_idioma.Traducir_382QA("ABM.btnSalir");

            if (grilla.Columns.Count > 0)
            {
                grilla.Columns["colId"].HeaderText = bll_idioma.Traducir_382QA("FrmGestionEquipos.colId");
                grilla.Columns["colMarca"].HeaderText = lblMarca.Text;
                grilla.Columns["colModelo"].HeaderText = lblModelo.Text;
                grilla.Columns["colImei"].HeaderText = lblImei.Text;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_382QA(this);
            base.OnFormClosed(e);
        }
    }
}
