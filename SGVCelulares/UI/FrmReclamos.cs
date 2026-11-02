using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using Servicio;

namespace UI
{
    /// <summary>
    /// Listado de reclamos por garantía (PN-02). Cada botón abre la ventana de su caso de uso
    /// y se muestra según los permisos del usuario:
    ///   CU-NEG-7 Registrar reclamo de garantía (FrmRegistroReclamo).
    ///   CU-NEG-8 Evaluar reclamo (FrmEvaluacionReclamo).
    /// Un técnico ve solo los reclamos que tiene asignados.
    /// </summary>
    public partial class FrmReclamos : Form, IObservadorIdioma
    {
        private readonly BLL_Reclamo bll_reclamo = new BLL_Reclamo();
        private readonly BLL_Idioma bll_idioma = new BLL_Idioma();
        private List<BE_ReclamoResumen> reclamos = new List<BE_ReclamoResumen>();
        private bool cargandoFiltro;
        private bool esTecnico;

        /// <summary>Opción del combo de estados: un texto y los estados que incluye (null = todos).</summary>
        private class OpcionEstado
        {
            public string Texto { get; }
            public string[]? Estados { get; }
            public OpcionEstado(string texto, string[]? estados) { Texto = texto; Estados = estados; }
            public override string ToString() => Texto;
        }

        public FrmReclamos()
        {
            InitializeComponent();
            bll_idioma.Suscribir(this);
        }

        private void FrmReclamos_Load(object sender, EventArgs e)
        {
            SER_Usuario usuario = SER_SesionManager.ObtenerSesion().Usuario;
            btnRegistrar.Visible = usuario.Rol.TienePermiso("Registrar Reclamo");
            btnEvaluar.Visible = usuario.Rol.TienePermiso("Evaluar Reclamo");
            ReubicarBotones();

            try
            {
                esTecnico = bll_reclamo.UsuarioEsTecnico();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }

            ConfigurarGrilla();
            ActualizarIdioma();
            CargarReclamos();
        }

        private BE_ReclamoResumen? ReclamoSeleccionado => grilla.CurrentRow?.DataBoundItem as BE_ReclamoResumen;

        private void ConfigurarGrilla()
        {
            grilla.AutoGenerateColumns = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.Columns.Clear();
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNro", DataPropertyName = "NroReclamo", FillWeight = 40 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colOT", DataPropertyName = "NroOT", FillWeight = 40 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEstado", DataPropertyName = "Estado", FillWeight = 80 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colFecha", DataPropertyName = "FechaReclamo", FillWeight = 55, DefaultCellStyle = { Format = "d" } });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCliente", DataPropertyName = "Cliente", FillWeight = 95 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEquipo", DataPropertyName = "Equipo", FillWeight = 90 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTecnico", DataPropertyName = "Tecnico", FillWeight = 85 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMotivo", DataPropertyName = "Motivo", FillWeight = 130 });
        }

        /// <summary>Arma el combo de estados con los textos del idioma actual, manteniendo la opción elegida.</summary>
        private void CargarFiltroEstados()
        {
            int seleccion = cboEstado.SelectedIndex < 0 ? 0 : cboEstado.SelectedIndex;
            cargandoFiltro = true;
            cboEstado.Items.Clear();
            cboEstado.Items.Add(new OpcionEstado(bll_idioma.Traducir("FrmReclamos.filtroAbiertos"), EstadosReclamo.Abiertos));
            cboEstado.Items.Add(new OpcionEstado(bll_idioma.Traducir("FrmReclamos.filtroTodos"), null));
            foreach (string estado in EstadosReclamo.Todos)
                cboEstado.Items.Add(new OpcionEstado(estado, new[] { estado }));
            cboEstado.SelectedIndex = seleccion;
            cargandoFiltro = false;
        }

        private void CargarReclamos(int nroReclamoASeleccionar = 0)
        {
            int seleccion = nroReclamoASeleccionar > 0 ? nroReclamoASeleccionar : ReclamoSeleccionado?.NroReclamo ?? 0;
            try
            {
                reclamos = bll_reclamo.ConsultarResumenSegunUsuario();
            }
            catch (Exception ex)
            {
                reclamos = new List<BE_ReclamoResumen>();
                MostrarError(ex.Message);
            }
            AplicarFiltro();
            if (seleccion > 0) SeleccionarReclamo(seleccion);
        }

        private void AplicarFiltro()
        {
            string[]? estados = (cboEstado.SelectedItem as OpcionEstado)?.Estados;
            string filtro = txtBuscar.Text.Trim();

            List<BE_ReclamoResumen> resultado = reclamos
                .Where(r => estados == null || estados.Contains(r.Estado))
                .Where(r => filtro.Length == 0
                         || r.NroReclamo.ToString() == filtro
                         || r.NroOT.ToString() == filtro
                         || r.DniCliente.Contains(filtro)
                         || r.ApellidoCliente.Contains(filtro, StringComparison.OrdinalIgnoreCase)
                         || r.Imei.Contains(filtro))
                .ToList();

            grilla.DataSource = null;
            grilla.DataSource = resultado;
            lblNumero.Text = resultado.Count.ToString();
            ActualizarBotones();
        }

        /// <summary>Acomoda de izquierda a derecha los botones de acción visibles, sin dejar huecos.</summary>
        private void ReubicarBotones()
        {
            int x = 12;
            foreach (Button boton in new[] { btnRegistrar, btnEvaluar })
            {
                if (!boton.Visible) continue;
                boton.Left = x;
                x += boton.Width + 10;
            }
        }

        /// <summary>Solo se puede evaluar un reclamo que está "En evaluacion".</summary>
        private void ActualizarBotones() =>
            btnEvaluar.Enabled = ReclamoSeleccionado?.Estado == EstadosReclamo.EnEvaluacion;

        private void grilla_SelectionChanged(object sender, EventArgs e) => ActualizarBotones();

        private void SeleccionarReclamo(int nroReclamo)
        {
            foreach (DataGridViewRow fila in grilla.Rows)
            {
                if (fila.DataBoundItem is BE_ReclamoResumen r && r.NroReclamo == nroReclamo)
                {
                    grilla.CurrentCell = fila.Cells[0];
                    return;
                }
            }
        }

        private void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cargandoFiltro) AplicarFiltro();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e) => AplicarFiltro();

        // CU-NEG-7 Registrar reclamo de garantía
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            using var frmRegistro = new FrmRegistroReclamo();
            if (frmRegistro.ShowDialog(this) == DialogResult.OK)
            {
                // El reclamo nuevo queda "En evaluacion" o "Rechazado": los dos entran en el filtro de abiertos.
                txtBuscar.Text = "";
                CargarReclamos(frmRegistro.NroReclamoCreado);
            }
        }

        // CU-NEG-8 Evaluar reclamo
        private void btnEvaluar_Click(object sender, EventArgs e)
        {
            BE_ReclamoResumen? reclamo = ReclamoSeleccionado;
            if (reclamo == null) return;

            using (var frmEvaluacion = new FrmEvaluacionReclamo(reclamo.NroReclamo))
            {
                if (frmEvaluacion.ShowDialog(this) != DialogResult.OK) return;
            }
            CargarReclamos(reclamo.NroReclamo);
        }

        private void btnActualizar_Click(object sender, EventArgs e) => CargarReclamos();

        private void btnSalir_Click(object sender, EventArgs e) => Close();

        private void MostrarError(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir("ABM.Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void ActualizarIdioma()
        {
            Text = bll_idioma.Traducir("FrmReclamos.Form");
            lblTitulo.Text = bll_idioma.Traducir(esTecnico ? "FrmReclamos.lblTituloTecnico" : "FrmReclamos.lblTitulo");
            lblCantidad.Text = bll_idioma.Traducir("FrmReclamos.lblCantidad");
            lblFiltro.Text = bll_idioma.Traducir("ABM.lblFiltro");
            lblEstado.Text = bll_idioma.Traducir("FrmReclamos.lblEstado");
            lblBuscar.Text = bll_idioma.Traducir("FrmReclamos.lblBuscar");
            lblAyudaBuscar.Text = bll_idioma.Traducir("FrmReclamos.lblAyudaBuscar");
            btnRegistrar.Text = bll_idioma.Traducir("FrmReclamos.btnRegistrar");
            btnEvaluar.Text = bll_idioma.Traducir("FrmReclamos.btnEvaluar");
            btnActualizar.Text = bll_idioma.Traducir("FrmReclamos.btnActualizar");
            btnSalir.Text = bll_idioma.Traducir("ABM.btnSalir");

            if (grilla.Columns.Count > 0)
            {
                grilla.Columns["colNro"].HeaderText = bll_idioma.Traducir("FrmReclamos.colNro");
                grilla.Columns["colOT"].HeaderText = bll_idioma.Traducir("FrmReclamos.colOT");
                grilla.Columns["colEstado"].HeaderText = lblEstado.Text;
                grilla.Columns["colFecha"].HeaderText = bll_idioma.Traducir("FrmReclamos.colFecha");
                grilla.Columns["colCliente"].HeaderText = bll_idioma.Traducir("FrmReclamos.colCliente");
                grilla.Columns["colEquipo"].HeaderText = bll_idioma.Traducir("FrmReclamos.colEquipo");
                grilla.Columns["colTecnico"].HeaderText = bll_idioma.Traducir("FrmReclamos.colTecnico");
                grilla.Columns["colMotivo"].HeaderText = bll_idioma.Traducir("FrmReclamos.colMotivo");
            }

            CargarFiltroEstados();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}
