using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using Servicio;
using System.Globalization;

namespace UI
{
    /// <summary>
    /// Listado de órdenes de trabajo, común a vendedores y técnicos. Cada botón abre la ventana de su caso de uso
    /// y se muestra según los permisos del usuario:
    ///   CU-NEG-1 Recepcionar equipo (FrmRecepcionEquipo) · CU-NEG-4 Diagnosticar equipo (FrmDiagnosticoEquipo) ·
    ///   CU-NEG-2 Gestionar aprobación de presupuesto (FrmPresupuesto).
    /// Un técnico ve solo sus OT; el resto de los usuarios ve todas.
    /// </summary>
    public partial class FrmOrdenesTrabajo : Form, IObservadorIdioma
    {
        private readonly BLL_OT bll_ot = new BLL_OT();
        private readonly BLL_Diagnostico bll_diagnostico = new BLL_Diagnostico();
        private bool esTecnico;
        private readonly BLL_Idioma bll_idioma = new BLL_Idioma();
        private List<BE_OTResumen> ordenes = new List<BE_OTResumen>();
        private bool cargandoFiltro;

        /// <summary>Opción del combo de estados: un texto y los estados que incluye (null = todos).</summary>
        private class OpcionEstado
        {
            public string Texto { get; }
            public string[]? Estados { get; }
            public OpcionEstado(string texto, string[]? estados) { Texto = texto; Estados = estados; }
            public override string ToString() => Texto;
        }

        public FrmOrdenesTrabajo()
        {
            InitializeComponent();
            bll_idioma.Suscribir(this);
        }

        private void FrmOrdenesTrabajo_Load(object sender, EventArgs e)
        {
            SER_Usuario usuario = SER_SesionManager.ObtenerSesion().Usuario;
            btnRecepcionar.Visible = usuario.Rol.TienePermiso("Recepcionar Equipo");
            btnDiagnosticar.Visible = usuario.Rol.TienePermiso("Diagnosticar Equipo");

            btnPresupuesto.Visible = usuario.Rol.TienePermiso("Gestionar Presupuesto");
            btnReparar.Visible = usuario.Rol.TienePermiso("Reparar Equipo");

            // Se habilita cuando se implemente CU-NEG-3 (Gestionar entrega).
            btnEntregar.Visible = false;
            ReubicarBotones();

            try
            {
                esTecnico = bll_ot.UsuarioEsTecnico();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }

            ConfigurarGrilla();
            ActualizarIdioma();
            CargarOrdenes();
        }

        private BE_OTResumen? OrdenSeleccionada => grilla.CurrentRow?.DataBoundItem as BE_OTResumen;

        private void ConfigurarGrilla()
        {
            grilla.AutoGenerateColumns = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.Columns.Clear();
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNro", DataPropertyName = "NroOT", FillWeight = 35 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEstado", DataPropertyName = "Estado", FillWeight = 80 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCliente", DataPropertyName = "Cliente", FillWeight = 100 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEquipo", DataPropertyName = "Equipo", FillWeight = 100 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTecnico", DataPropertyName = "Tecnico", FillWeight = 90 });
        }

        /// <summary>Arma el combo de estados con los textos del idioma actual, manteniendo la opción elegida.</summary>
        private void CargarFiltroEstados()
        {
            int seleccion = cboEstado.SelectedIndex < 0 ? 0 : cboEstado.SelectedIndex;
            cargandoFiltro = true;
            cboEstado.Items.Clear();
            cboEstado.Items.Add(new OpcionEstado(bll_idioma.Traducir("FrmOrdenesTrabajo.filtroAbiertas"), EstadosOT.Abiertas));
            cboEstado.Items.Add(new OpcionEstado(bll_idioma.Traducir("FrmOrdenesTrabajo.filtroTodas"), null));
            foreach (string estado in EstadosOT.Todos)
                cboEstado.Items.Add(new OpcionEstado(estado, new[] { estado }));
            cboEstado.SelectedIndex = seleccion;
            cargandoFiltro = false;
        }

        private void CargarOrdenes(int nroOTASeleccionar = 0)
        {
            int seleccion = nroOTASeleccionar > 0 ? nroOTASeleccionar : OrdenSeleccionada?.NroOT ?? 0;
            try
            {
                ordenes = bll_ot.ConsultarResumenSegunUsuario();
            }
            catch (Exception ex)
            {
                ordenes = new List<BE_OTResumen>();
                MostrarError(ex.Message);
            }
            AplicarFiltro();
            if (seleccion > 0) SeleccionarOrden(seleccion);
        }

        private void AplicarFiltro()
        {
            string[]? estados = (cboEstado.SelectedItem as OpcionEstado)?.Estados;
            string filtro = txtBuscar.Text.Trim();

            List<BE_OTResumen> resultado = ordenes
                .Where(o => estados == null || estados.Contains(o.Estado))
                .Where(o => filtro.Length == 0
                         || o.NroOT.ToString() == filtro
                         || o.DniCliente.Contains(filtro)
                         || o.ApellidoCliente.Contains(filtro, StringComparison.OrdinalIgnoreCase)
                         || o.Imei.Contains(filtro))
                .ToList();

            grilla.DataSource = null;
            grilla.DataSource = resultado;
            lblNumero.Text = resultado.Count.ToString();
            MostrarDetalle(OrdenSeleccionada);
            ActualizarBotones();
        }

        /// <summary>Acomoda de izquierda a derecha los botones de acción visibles, sin dejar huecos.</summary>
        private void ReubicarBotones()
        {
            int x = 12;
            foreach (Button boton in new[] { btnRecepcionar, btnDiagnosticar, btnPresupuesto, btnReparar, btnEntregar })
            {
                if (!boton.Visible) continue;
                boton.Left = x;
                x += boton.Width + 10;
            }
        }

        /// <summary>Habilita cada acción solo si la OT seleccionada está en un estado que la admite.</summary>
        private void ActualizarBotones()
        {
            BE_OTResumen? ot = OrdenSeleccionada;
            btnDiagnosticar.Enabled = ot != null
                && (ot.Estado == EstadosOT.Recibida || ot.Estado == EstadosOT.EnDiagnostico);
            btnPresupuesto.Enabled = ot != null && ot.Estado == EstadosOT.Presupuestado;
            btnReparar.Enabled = ot != null && ot.Estado == EstadosOT.EnReparacion;
        }

        private void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cargandoFiltro) AplicarFiltro();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e) => AplicarFiltro();

        private void grilla_SelectionChanged(object sender, EventArgs e)
        {
            MostrarDetalle(OrdenSeleccionada);
            ActualizarBotones();
        }

        private void MostrarDetalle(BE_OTResumen? ot)
        {
            if (ot == null)
            {
                lblNroOT.Text = "-";
                lblEstadoOT.Text = bll_idioma.Traducir("FrmOrdenesTrabajo.msgSinSeleccion");
                foreach (Label valor in new[] { lblCliente, lblTelefono, lblEquipo, lblImei, lblTecnico, lblPresupuesto, lblFalla })
                    valor.Text = "-";
                return;
            }

            lblNroOT.Text = string.Format(bll_idioma.Traducir("FrmOrdenesTrabajo.formatoNroOT"), ot.NroOT);
            lblEstadoOT.Text = ot.Estado;
            lblCliente.Text = $"{ot.Cliente} ({ot.DniCliente})";
            lblTelefono.Text = ot.TelefonoCliente;
            lblEquipo.Text = ot.Equipo;
            lblImei.Text = ot.Imei;
            lblTecnico.Text = ot.Tecnico;
            lblPresupuesto.Text = ot.TotalPresupuesto.HasValue
                ? $"$ {ot.TotalPresupuesto.Value.ToString("N2", CultureInfo.GetCultureInfo("es-AR"))} ({ot.EstadoPresupuesto})"
                : bll_idioma.Traducir("FrmOrdenesTrabajo.msgSinPresupuesto");
            lblFalla.Text = ot.DescripcionFalla;
        }

        private void SeleccionarOrden(int nroOT)
        {
            foreach (DataGridViewRow fila in grilla.Rows)
            {
                if (fila.DataBoundItem is BE_OTResumen o && o.NroOT == nroOT)
                {
                    grilla.CurrentCell = fila.Cells[0];
                    return;
                }
            }
        }

        // CU-NEG-1 Gestionar recepción de equipo
        private void btnRecepcionar_Click(object sender, EventArgs e)
        {
            using var frmRecepcion = new FrmRecepcionEquipo();
            if (frmRecepcion.ShowDialog(this) == DialogResult.OK)
            {
                // La OT nueva está en "Recibida": entra en el filtro de abiertas, pero se limpia la búsqueda.
                txtBuscar.Text = "";
                CargarOrdenes(frmRecepcion.NroOTCreada);
            }
        }

        // CU-NEG-4 Diagnosticar equipo: al abrirlo, la OT pasa a "En diagnostico" (paso 3)
        private void btnDiagnosticar_Click(object sender, EventArgs e)
        {
            BE_OTResumen? ot = OrdenSeleccionada;
            if (ot == null) return;

            try
            {
                bll_diagnostico.IniciarDiagnostico(ot.NroOT);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
                CargarOrdenes(ot.NroOT);
                return;
            }

            using (var frmDiagnostico = new FrmDiagnosticoEquipo(ot.NroOT))
            {
                frmDiagnostico.ShowDialog(this);
            }
            // Se recarga siempre: aunque se cancele, la OT ya quedó "En diagnostico".
            CargarOrdenes(ot.NroOT);
        }

        // CU-NEG-2 Gestionar aprobación de presupuesto
        private void btnPresupuesto_Click(object sender, EventArgs e)
        {
            BE_OTResumen? ot = OrdenSeleccionada;
            if (ot == null) return;

            using (var frmPresupuesto = new FrmPresupuesto(ot.NroOT))
            {
                if (frmPresupuesto.ShowDialog(this) != DialogResult.OK) return;
            }
            CargarOrdenes(ot.NroOT);
        }

        // CU-NEG-5 Reparar equipo
        private void btnReparar_Click(object sender, EventArgs e)
        {
            BE_OTResumen? ot = OrdenSeleccionada;
            if (ot == null) return;

            using (var frmReparacion = new FrmReparacionEquipo(ot.NroOT))
            {
                if (frmReparacion.ShowDialog(this) != DialogResult.OK) return;
            }
            CargarOrdenes(ot.NroOT);
        }

        // CU-NEG-3 Gestionar entrega (próxima etapa)
        private void btnEntregar_Click(object sender, EventArgs e) { }

        private void btnActualizar_Click(object sender, EventArgs e) => CargarOrdenes();

        private void btnSalir_Click(object sender, EventArgs e) => Close();

        private void MostrarError(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir("ABM.Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void ActualizarIdioma()
        {
            Text = bll_idioma.Traducir("FrmOrdenesTrabajo.Form");
            lblTitulo.Text = bll_idioma.Traducir(esTecnico ? "FrmOrdenesTrabajo.lblTituloTecnico" : "FrmOrdenesTrabajo.lblTitulo");
            lblCantidad.Text = bll_idioma.Traducir("FrmOrdenesTrabajo.lblCantidad");
            lblFiltro.Text = bll_idioma.Traducir("ABM.lblFiltro");
            lblEstado.Text = bll_idioma.Traducir("FrmOrdenesTrabajo.lblEstado");
            lblBuscar.Text = bll_idioma.Traducir("FrmOrdenesTrabajo.lblBuscar");
            lblAyudaBuscar.Text = bll_idioma.Traducir("FrmOrdenesTrabajo.lblAyudaBuscar");
            lblDetalle.Text = bll_idioma.Traducir("FrmOrdenesTrabajo.lblDetalle");
            lblTitCliente.Text = bll_idioma.Traducir("FrmOrdenesTrabajo.lblCliente");
            lblTitTelefono.Text = bll_idioma.Traducir("FrmOrdenesTrabajo.lblTelefono");
            lblTitEquipo.Text = bll_idioma.Traducir("FrmOrdenesTrabajo.lblEquipo");
            lblTitImei.Text = bll_idioma.Traducir("FrmOrdenesTrabajo.lblImei");
            lblTitTecnico.Text = bll_idioma.Traducir("FrmOrdenesTrabajo.lblTecnico");
            lblTitPresupuesto.Text = bll_idioma.Traducir("FrmOrdenesTrabajo.lblPresupuesto");
            lblTitFalla.Text = bll_idioma.Traducir("FrmOrdenesTrabajo.lblFalla");
            btnRecepcionar.Text = bll_idioma.Traducir("FrmOrdenesTrabajo.btnRecepcionar");
            btnDiagnosticar.Text = bll_idioma.Traducir("FrmOrdenesTrabajo.btnDiagnosticar");
            btnPresupuesto.Text = bll_idioma.Traducir("FrmOrdenesTrabajo.btnPresupuesto");
            btnReparar.Text = bll_idioma.Traducir("FrmOrdenesTrabajo.btnReparar");
            btnEntregar.Text = bll_idioma.Traducir("FrmOrdenesTrabajo.btnEntregar");
            btnActualizar.Text = bll_idioma.Traducir("FrmOrdenesTrabajo.btnActualizar");
            btnSalir.Text = bll_idioma.Traducir("ABM.btnSalir");

            if (grilla.Columns.Count > 0)
            {
                grilla.Columns["colNro"].HeaderText = bll_idioma.Traducir("FrmOrdenesTrabajo.colNro");
                grilla.Columns["colEstado"].HeaderText = lblEstado.Text;
                grilla.Columns["colCliente"].HeaderText = lblTitCliente.Text;
                grilla.Columns["colEquipo"].HeaderText = lblTitEquipo.Text;
                grilla.Columns["colTecnico"].HeaderText = lblTitTecnico.Text;
            }

            CargarFiltroEstados();
            MostrarDetalle(OrdenSeleccionada);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}
