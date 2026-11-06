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
    ///   CU-NEG-1 Recepcionar equipo (FrmRecepcionEquipo_382QA) · CU-NEG-4 Diagnosticar equipo (FrmDiagnosticoEquipo_382QA) ·
    ///   CU-NEG-2 Gestionar aprobación de presupuesto (FrmPresupuesto_382QA).
    /// Un técnico ve solo sus OT; el resto de los usuarios ve todas.
    /// </summary>
    public partial class FrmOrdenesTrabajo_382QA : Form, IObservadorIdioma_382QA
    {
        private readonly BLL_OT_382QA bll_ot = new BLL_OT_382QA();
        private readonly BLL_Diagnostico_382QA bll_diagnostico = new BLL_Diagnostico_382QA();
        private bool esTecnico;
        private readonly BLL_Idioma_382QA bll_idioma = new BLL_Idioma_382QA();
        private List<BE_OTResumen_382QA> ordenes = new List<BE_OTResumen_382QA>();
        private bool cargandoFiltro;

        /// <summary>Opción del combo de estados: un texto y los estados que incluye (null = todos).</summary>
        private class OpcionEstado
        {
            public string Texto { get; }
            public string[]? Estados { get; }
            public OpcionEstado(string texto, string[]? estados) { Texto = texto; Estados = estados; }
            public override string ToString() => Texto;
        }

        public FrmOrdenesTrabajo_382QA()
        {
            InitializeComponent();
            bll_idioma.Suscribir_382QA(this);
        }

        private void FrmOrdenesTrabajo_Load(object sender, EventArgs e)
        {
            SER_Usuario_382QA usuario = SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario;
            btnRecepcionar.Visible = usuario.Rol.TienePermiso_382QA("Recepcionar Equipo");
            btnDiagnosticar.Visible = usuario.Rol.TienePermiso_382QA("Diagnosticar Equipo");

            btnPresupuesto.Visible = usuario.Rol.TienePermiso_382QA("Gestionar Presupuesto");
            btnReparar.Visible = usuario.Rol.TienePermiso_382QA("Reparar Equipo");

            btnEntregar.Visible = usuario.Rol.TienePermiso_382QA("Entregar Equipo");
            ReubicarBotones_382QA();

            try
            {
                esTecnico = bll_ot.UsuarioEsTecnico_382QA();
            }
            catch (Exception ex)
            {
                MostrarError_382QA(ex.Message);
            }

            ConfigurarGrilla_382QA();
            ActualizarIdioma_382QA();
            CargarOrdenes_382QA();
        }

        private BE_OTResumen_382QA? OrdenSeleccionada => grilla.CurrentRow?.DataBoundItem as BE_OTResumen_382QA;

        private void ConfigurarGrilla_382QA()
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
        private void CargarFiltroEstados_382QA()
        {
            int seleccion = cboEstado.SelectedIndex < 0 ? 0 : cboEstado.SelectedIndex;
            cargandoFiltro = true;
            cboEstado.Items.Clear();
            cboEstado.Items.Add(new OpcionEstado(bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.filtroAbiertas"), EstadosOT_382QA.Abiertas));
            cboEstado.Items.Add(new OpcionEstado(bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.filtroTodas"), null));
            foreach (string estado in EstadosOT_382QA.Todos)
                cboEstado.Items.Add(new OpcionEstado(estado, new[] { estado }));
            cboEstado.SelectedIndex = seleccion;
            cargandoFiltro = false;
        }

        private void CargarOrdenes_382QA(int nroOTASeleccionar = 0)
        {
            int seleccion = nroOTASeleccionar > 0 ? nroOTASeleccionar : OrdenSeleccionada?.NroOT ?? 0;
            try
            {
                ordenes = bll_ot.ConsultarResumenSegunUsuario_382QA();
            }
            catch (Exception ex)
            {
                ordenes = new List<BE_OTResumen_382QA>();
                MostrarError_382QA(ex.Message);
            }
            AplicarFiltro_382QA();
            if (seleccion > 0) SeleccionarOrden_382QA(seleccion);
        }

        private void AplicarFiltro_382QA()
        {
            string[]? estados = (cboEstado.SelectedItem as OpcionEstado)?.Estados;
            string filtro = txtBuscar.Text.Trim();

            List<BE_OTResumen_382QA> resultado = ordenes
                .Where(o => estados == null || estados.Contains(o.Estado))
                .Where(o => filtro.Length == 0 || CampoBuscado_382QA(o).Contains(filtro, StringComparison.OrdinalIgnoreCase))
                .ToList();

            grilla.DataSource = null;
            grilla.DataSource = resultado;
            lblNumero.Text = resultado.Count.ToString();
            MostrarDetalle_382QA(OrdenSeleccionada);
            ActualizarBotones_382QA();
        }

        /// <summary>Acomoda de izquierda a derecha los botones de acción visibles, sin dejar huecos.</summary>
        private void ReubicarBotones_382QA()
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
        private void ActualizarBotones_382QA()
        {
            BE_OTResumen_382QA? ot = OrdenSeleccionada;
            btnDiagnosticar.Enabled = ot != null
                && (ot.Estado == EstadosOT_382QA.Recibida || ot.Estado == EstadosOT_382QA.EnDiagnostico);
            btnPresupuesto.Enabled = ot != null && ot.Estado == EstadosOT_382QA.Presupuestado;
            btnReparar.Enabled = ot != null && ot.Estado == EstadosOT_382QA.EnReparacion;
            btnEntregar.Enabled = ot != null
                && (ot.Estado == EstadosOT_382QA.Reparado || ot.Estado == EstadosOT_382QA.SinReparar);
        }

        private void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cargandoFiltro) AplicarFiltro_382QA();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e) => AplicarFiltro_382QA();

        // CheckedChanged salta también en el radio que se desmarca: se filtra una sola vez.
        private void rbBuscar_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked) AplicarFiltro_382QA();
        }

        /// <summary>Devuelve el campo de la OT sobre el que se busca, según el radio elegido.</summary>
        private string CampoBuscado_382QA(BE_OTResumen_382QA o)
        {
            if (rbNroOT.Checked) return o.NroOT.ToString();
            if (rbDni.Checked) return o.DniCliente;
            if (rbApellido.Checked) return o.ApellidoCliente;
            return o.Imei;
        }

        private void grilla_SelectionChanged(object sender, EventArgs e)
        {
            MostrarDetalle_382QA(OrdenSeleccionada);
            ActualizarBotones_382QA();
        }

        private void MostrarDetalle_382QA(BE_OTResumen_382QA? ot)
        {
            if (ot == null)
            {
                lblNroOT.Text = "-";
                lblEstadoOT.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.msgSinSeleccion");
                foreach (Label valor in new[] { lblCliente, lblTelefono, lblEquipo, lblImei, lblTecnico, lblPresupuesto, lblFalla })
                    valor.Text = "-";
                return;
            }

            lblNroOT.Text = string.Format(bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.formatoNroOT"), ot.NroOT);
            lblEstadoOT.Text = ot.Estado;
            lblCliente.Text = $"{ot.Cliente} ({ot.DniCliente})";
            lblTelefono.Text = ot.TelefonoCliente;
            lblEquipo.Text = ot.Equipo;
            lblImei.Text = ot.Imei;
            lblTecnico.Text = ot.Tecnico;
            lblPresupuesto.Text = ot.TotalPresupuesto.HasValue
                ? $"$ {ot.TotalPresupuesto.Value.ToString("N2", CultureInfo.GetCultureInfo("es-AR"))} ({ot.EstadoPresupuesto})"
                : bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.msgSinPresupuesto");
            lblFalla.Text = ot.DescripcionFalla;
        }

        private void SeleccionarOrden_382QA(int nroOT)
        {
            foreach (DataGridViewRow fila in grilla.Rows)
            {
                if (fila.DataBoundItem is BE_OTResumen_382QA o && o.NroOT == nroOT)
                {
                    grilla.CurrentCell = fila.Cells[0];
                    return;
                }
            }
        }

        // CU-NEG-1 Gestionar recepción de equipo
        private void btnRecepcionar_Click(object sender, EventArgs e)
        {
            using var frmRecepcion = new FrmRecepcionEquipo_382QA();
            if (frmRecepcion.ShowDialog(this) == DialogResult.OK)
            {
                // La OT nueva está en "Recibida": entra en el filtro de abiertas, pero se limpia la búsqueda.
                txtBuscar.Text = "";
                CargarOrdenes_382QA(frmRecepcion.NroOTCreada);
            }
        }

        // CU-NEG-4 Diagnosticar equipo: al abrirlo, la OT pasa a "En diagnostico" (paso 3)
        private void btnDiagnosticar_Click(object sender, EventArgs e)
        {
            BE_OTResumen_382QA? ot = OrdenSeleccionada;
            if (ot == null) return;

            try
            {
                bll_diagnostico.IniciarDiagnostico_382QA(ot.NroOT);
            }
            catch (Exception ex)
            {
                MostrarError_382QA(ex.Message);
                CargarOrdenes_382QA(ot.NroOT);
                return;
            }

            using (var frmDiagnostico = new FrmDiagnosticoEquipo_382QA(ot.NroOT))
            {
                frmDiagnostico.ShowDialog(this);
            }
            // Se recarga siempre: aunque se cancele, la OT ya quedó "En diagnostico".
            CargarOrdenes_382QA(ot.NroOT);
        }

        // CU-NEG-2 Gestionar aprobación de presupuesto
        private void btnPresupuesto_Click(object sender, EventArgs e)
        {
            BE_OTResumen_382QA? ot = OrdenSeleccionada;
            if (ot == null) return;

            using (var frmPresupuesto = new FrmPresupuesto_382QA(ot.NroOT))
            {
                if (frmPresupuesto.ShowDialog(this) != DialogResult.OK) return;
            }
            CargarOrdenes_382QA(ot.NroOT);
        }

        // CU-NEG-5 Reparar equipo
        private void btnReparar_Click(object sender, EventArgs e)
        {
            BE_OTResumen_382QA? ot = OrdenSeleccionada;
            if (ot == null) return;

            using (var frmReparacion = new FrmReparacionEquipo_382QA(ot.NroOT))
            {
                if (frmReparacion.ShowDialog(this) != DialogResult.OK) return;
            }
            CargarOrdenes_382QA(ot.NroOT);
        }

        // CU-NEG-3 Gestionar entrega (con el cobro si la OT está reparada)
        private void btnEntregar_Click(object sender, EventArgs e)
        {
            BE_OTResumen_382QA? ot = OrdenSeleccionada;
            if (ot == null) return;

            using (var frmEntrega = new FrmEntregaEquipo_382QA(ot.NroOT))
            {
                if (frmEntrega.ShowDialog(this) != DialogResult.OK) return;
            }
            // La OT entregada sale del filtro "Abiertas": se mantiene la posición en el listado si sigue visible.
            CargarOrdenes_382QA(ot.NroOT);
        }

        private void btnActualizar_Click(object sender, EventArgs e) => CargarOrdenes_382QA();

        private void btnSalir_Click(object sender, EventArgs e) => Close();

        private void MostrarError_382QA(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir_382QA("ABM.Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void ActualizarIdioma_382QA()
        {
            Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.Form");
            lblTitulo.Text = bll_idioma.Traducir_382QA(esTecnico ? "FrmOrdenesTrabajo.lblTituloTecnico" : "FrmOrdenesTrabajo.lblTitulo");
            lblCantidad.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.lblCantidad");
            lblFiltro.Text = bll_idioma.Traducir_382QA("ABM.lblFiltro");
            lblEstado.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.lblEstado");
            lblBuscar.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.lblBuscar");
            rbNroOT.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.rbNroOT");
            rbDni.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.rbDni");
            rbApellido.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.rbApellido");
            rbImei.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.rbImei");
            lblDetalle.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.lblDetalle");
            lblTitCliente.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.lblCliente");
            lblTitTelefono.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.lblTelefono");
            lblTitEquipo.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.lblEquipo");
            lblTitImei.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.lblImei");
            lblTitTecnico.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.lblTecnico");
            lblTitPresupuesto.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.lblPresupuesto");
            lblTitFalla.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.lblFalla");
            btnRecepcionar.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.btnRecepcionar");
            btnDiagnosticar.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.btnDiagnosticar");
            btnPresupuesto.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.btnPresupuesto");
            btnReparar.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.btnReparar");
            btnEntregar.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.btnEntregar");
            btnActualizar.Text = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.btnActualizar");
            btnSalir.Text = bll_idioma.Traducir_382QA("ABM.btnSalir");

            if (grilla.Columns.Count > 0)
            {
                grilla.Columns["colNro"].HeaderText = bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.colNro");
                grilla.Columns["colEstado"].HeaderText = lblEstado.Text;
                grilla.Columns["colCliente"].HeaderText = lblTitCliente.Text;
                grilla.Columns["colEquipo"].HeaderText = lblTitEquipo.Text;
                grilla.Columns["colTecnico"].HeaderText = lblTitTecnico.Text;
            }

            CargarFiltroEstados_382QA();
            MostrarDetalle_382QA(OrdenSeleccionada);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_382QA(this);
            base.OnFormClosed(e);
        }
    }
}
