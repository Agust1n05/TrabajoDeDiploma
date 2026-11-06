using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using Servicio;

namespace UI
{
    /// <summary>
    /// Listado de reclamos por garantía (PN-02). Cada botón abre la ventana de su caso de uso
    /// y se muestra según los permisos del usuario:
    ///   CU-NEG-7 Registrar reclamo de garantía (FrmRegistroReclamo_382QA).
    ///   CU-NEG-8 Evaluar reclamo (FrmEvaluacionReclamo_382QA).
    ///   CU-NEG-9 Reparar equipo por garantía (FrmReparacionReclamo_382QA).
    ///   CU-NEG-10 Gestionar entrega de reclamo (confirmación en este mismo listado).
    /// Un técnico ve solo los reclamos que tiene asignados.
    /// </summary>
    public partial class FrmReclamos_382QA : Form, IObservadorIdioma_382QA
    {
        private readonly BLL_Reclamo_382QA bll_reclamo = new BLL_Reclamo_382QA();
        private readonly BLL_Idioma_382QA bll_idioma = new BLL_Idioma_382QA();
        private List<BE_ReclamoResumen_382QA> reclamos = new List<BE_ReclamoResumen_382QA>();
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

        public FrmReclamos_382QA()
        {
            InitializeComponent();
            bll_idioma.Suscribir_382QA(this);
        }

        private void FrmReclamos_Load(object sender, EventArgs e)
        {
            SER_Usuario_382QA usuario = SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario;
            btnRegistrar.Visible = usuario.Rol.TienePermiso_382QA("Registrar Reclamo");
            btnEvaluar.Visible = usuario.Rol.TienePermiso_382QA("Evaluar Reclamo");
            btnReparar.Visible = usuario.Rol.TienePermiso_382QA("Reparar Reclamo");
            btnEntregar.Visible = usuario.Rol.TienePermiso_382QA("Entregar Reclamo");
            ReubicarBotones_382QA();

            try
            {
                esTecnico = bll_reclamo.UsuarioEsTecnico_382QA();
            }
            catch (Exception ex)
            {
                MostrarError_382QA(ex.Message);
            }

            ConfigurarGrilla_382QA();
            ActualizarIdioma_382QA();
            CargarReclamos_382QA();
        }

        private BE_ReclamoResumen_382QA? ReclamoSeleccionado => grilla.CurrentRow?.DataBoundItem as BE_ReclamoResumen_382QA;

        private void ConfigurarGrilla_382QA()
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
        private void CargarFiltroEstados_382QA()
        {
            int seleccion = cboEstado.SelectedIndex < 0 ? 0 : cboEstado.SelectedIndex;
            cargandoFiltro = true;
            cboEstado.Items.Clear();
            cboEstado.Items.Add(new OpcionEstado(bll_idioma.Traducir_382QA("FrmReclamos.filtroAbiertos"), EstadosReclamo_382QA.Abiertos));
            cboEstado.Items.Add(new OpcionEstado(bll_idioma.Traducir_382QA("FrmReclamos.filtroTodos"), null));
            foreach (string estado in EstadosReclamo_382QA.Todos)
                cboEstado.Items.Add(new OpcionEstado(estado, new[] { estado }));
            cboEstado.SelectedIndex = seleccion;
            cargandoFiltro = false;
        }

        private void CargarReclamos_382QA(int nroReclamoASeleccionar = 0)
        {
            int seleccion = nroReclamoASeleccionar > 0 ? nroReclamoASeleccionar : ReclamoSeleccionado?.NroReclamo ?? 0;
            try
            {
                reclamos = bll_reclamo.ConsultarResumenSegunUsuario_382QA();
            }
            catch (Exception ex)
            {
                reclamos = new List<BE_ReclamoResumen_382QA>();
                MostrarError_382QA(ex.Message);
            }
            AplicarFiltro_382QA();
            if (seleccion > 0) SeleccionarReclamo_382QA(seleccion);
        }

        private void AplicarFiltro_382QA()
        {
            string[]? estados = (cboEstado.SelectedItem as OpcionEstado)?.Estados;
            string filtro = txtBuscar.Text.Trim();

            List<BE_ReclamoResumen_382QA> resultado = reclamos
                .Where(r => estados == null || estados.Contains(r.Estado))
                .Where(r => filtro.Length == 0 || CampoBuscado_382QA(r).Contains(filtro, StringComparison.OrdinalIgnoreCase))
                .ToList();

            grilla.DataSource = null;
            grilla.DataSource = resultado;
            lblNumero.Text = resultado.Count.ToString();
            ActualizarBotones_382QA();
        }

        /// <summary>Acomoda de izquierda a derecha los botones de acción visibles, sin dejar huecos.</summary>
        private void ReubicarBotones_382QA()
        {
            int x = 12;
            foreach (Button boton in new[] { btnRegistrar, btnEvaluar, btnReparar, btnEntregar })
            {
                if (!boton.Visible) continue;
                boton.Left = x;
                x += boton.Width + 10;
            }
        }

        /// <summary>Cada acción se habilita solo si el reclamo seleccionado está en un estado que la admite.</summary>
        private void ActualizarBotones_382QA()
        {
            string? estado = ReclamoSeleccionado?.Estado;
            btnEvaluar.Enabled = estado == EstadosReclamo_382QA.EnEvaluacion;
            btnReparar.Enabled = estado == EstadosReclamo_382QA.Aprobado || estado == EstadosReclamo_382QA.EnEsperaRepuesto;
            btnEntregar.Enabled = estado == EstadosReclamo_382QA.Reparado || estado == EstadosReclamo_382QA.Rechazado;
        }

        private void grilla_SelectionChanged(object sender, EventArgs e) => ActualizarBotones_382QA();

        private void SeleccionarReclamo_382QA(int nroReclamo)
        {
            foreach (DataGridViewRow fila in grilla.Rows)
            {
                if (fila.DataBoundItem is BE_ReclamoResumen_382QA r && r.NroReclamo == nroReclamo)
                {
                    grilla.CurrentCell = fila.Cells[0];
                    return;
                }
            }
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

        /// <summary>Devuelve el campo del reclamo sobre el que se busca, según el radio elegido.</summary>
        private string CampoBuscado_382QA(BE_ReclamoResumen_382QA r)
        {
            if (rbNroReclamo.Checked) return r.NroReclamo.ToString();
            if (rbNroOT.Checked) return r.NroOT.ToString();
            if (rbDni.Checked) return r.DniCliente;
            if (rbApellido.Checked) return r.ApellidoCliente;
            return r.Imei;
        }

        // CU-NEG-7 Registrar reclamo de garantía
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            using var frmRegistro = new FrmRegistroReclamo_382QA();
            if (frmRegistro.ShowDialog(this) == DialogResult.OK)
            {
                // El reclamo nuevo queda "En evaluacion" o "Rechazado": los dos entran en el filtro de abiertos.
                txtBuscar.Text = "";
                CargarReclamos_382QA(frmRegistro.NroReclamoCreado);
            }
        }

        // CU-NEG-8 Evaluar reclamo
        private void btnEvaluar_Click(object sender, EventArgs e)
        {
            BE_ReclamoResumen_382QA? reclamo = ReclamoSeleccionado;
            if (reclamo == null) return;

            using (var frmEvaluacion = new FrmEvaluacionReclamo_382QA(reclamo.NroReclamo))
            {
                if (frmEvaluacion.ShowDialog(this) != DialogResult.OK) return;
            }
            CargarReclamos_382QA(reclamo.NroReclamo);
        }

        // CU-NEG-9 Reparar equipo por garantía
        private void btnReparar_Click(object sender, EventArgs e)
        {
            BE_ReclamoResumen_382QA? reclamo = ReclamoSeleccionado;
            if (reclamo == null) return;

            using (var frmReparacion = new FrmReparacionReclamo_382QA(reclamo.NroReclamo))
            {
                if (frmReparacion.ShowDialog(this) != DialogResult.OK) return;
            }
            CargarReclamos_382QA(reclamo.NroReclamo);
        }

        // CU-NEG-10 Gestionar entrega de reclamo
        private void btnEntregar_Click(object sender, EventArgs e)
        {
            BE_ReclamoResumen_382QA? seleccionado = ReclamoSeleccionado;
            if (seleccionado == null) return;

            try
            {
                BE_ReclamoResumen_382QA reclamo = bll_reclamo.ObtenerReclamoParaEntregar_382QA(seleccionado.NroReclamo);
                string resumen = $"{string.Format(bll_idioma.Traducir_382QA("FrmReclamos.formatoReclamo"), reclamo.NroReclamo)}\n{reclamo.Cliente}  ·  {reclamo.Equipo}\n\n";
                string pregunta;
                if (reclamo.Estado == EstadosReclamo_382QA.Reparado)
                {
                    pregunta = resumen + string.Format(bll_idioma.Traducir_382QA("FrmReclamos.msgEntregarReparado"), EstadosReclamo_382QA.Resuelto);
                }
                else
                {
                    // Rechazado: sin evaluación es por garantía vencida; con evaluación, la falla no estaba cubierta
                    string motivo = reclamo.FallaCubierta == false
                        ? string.Format(bll_idioma.Traducir_382QA("FrmReclamos.msgMotivoNoCubierta"), reclamo.DetalleEvaluacion)
                        : string.Format(bll_idioma.Traducir_382QA("FrmReclamos.msgMotivoVencida"), reclamo.FechaVencimiento.ToShortDateString());
                    pregunta = resumen + motivo + "\n\n" +
                               string.Format(bll_idioma.Traducir_382QA("FrmReclamos.msgEntregarRechazado"), EstadosReclamo_382QA.EntregadoSinReparar);
                }

                if (MessageBox.Show(pregunta, bll_idioma.Traducir_382QA("ABM.Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                reclamo = bll_reclamo.EntregarReclamo_382QA(reclamo.NroReclamo);
                MessageBox.Show(string.Format(bll_idioma.Traducir_382QA("FrmReclamos.msgEntregaOk"), reclamo.NroReclamo, reclamo.Estado),
                                bll_idioma.Traducir_382QA("ABM.Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError_382QA(ex.Message);
            }
            CargarReclamos_382QA(seleccionado.NroReclamo);
        }

        private void btnActualizar_Click(object sender, EventArgs e) => CargarReclamos_382QA();

        private void btnSalir_Click(object sender, EventArgs e) => Close();

        private void MostrarError_382QA(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir_382QA("ABM.Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void ActualizarIdioma_382QA()
        {
            Text = bll_idioma.Traducir_382QA("FrmReclamos.Form");
            lblTitulo.Text = bll_idioma.Traducir_382QA(esTecnico ? "FrmReclamos.lblTituloTecnico" : "FrmReclamos.lblTitulo");
            lblCantidad.Text = bll_idioma.Traducir_382QA("FrmReclamos.lblCantidad");
            lblFiltro.Text = bll_idioma.Traducir_382QA("ABM.lblFiltro");
            lblEstado.Text = bll_idioma.Traducir_382QA("FrmReclamos.lblEstado");
            lblBuscar.Text = bll_idioma.Traducir_382QA("FrmReclamos.lblBuscar");
            rbNroReclamo.Text = bll_idioma.Traducir_382QA("FrmReclamos.rbNroReclamo");
            rbNroOT.Text = bll_idioma.Traducir_382QA("FrmReclamos.rbNroOT");
            rbDni.Text = bll_idioma.Traducir_382QA("FrmReclamos.rbDni");
            rbApellido.Text = bll_idioma.Traducir_382QA("FrmReclamos.rbApellido");
            rbImei.Text = bll_idioma.Traducir_382QA("FrmReclamos.rbImei");
            btnRegistrar.Text = bll_idioma.Traducir_382QA("FrmReclamos.btnRegistrar");
            btnEvaluar.Text = bll_idioma.Traducir_382QA("FrmReclamos.btnEvaluar");
            btnReparar.Text = bll_idioma.Traducir_382QA("FrmReclamos.btnReparar");
            btnEntregar.Text = bll_idioma.Traducir_382QA("FrmReclamos.btnEntregar");
            btnActualizar.Text = bll_idioma.Traducir_382QA("FrmReclamos.btnActualizar");
            btnSalir.Text = bll_idioma.Traducir_382QA("ABM.btnSalir");

            if (grilla.Columns.Count > 0)
            {
                grilla.Columns["colNro"].HeaderText = bll_idioma.Traducir_382QA("FrmReclamos.colNro");
                grilla.Columns["colOT"].HeaderText = bll_idioma.Traducir_382QA("FrmReclamos.colOT");
                grilla.Columns["colEstado"].HeaderText = lblEstado.Text;
                grilla.Columns["colFecha"].HeaderText = bll_idioma.Traducir_382QA("FrmReclamos.colFecha");
                grilla.Columns["colCliente"].HeaderText = bll_idioma.Traducir_382QA("FrmReclamos.colCliente");
                grilla.Columns["colEquipo"].HeaderText = bll_idioma.Traducir_382QA("FrmReclamos.colEquipo");
                grilla.Columns["colTecnico"].HeaderText = bll_idioma.Traducir_382QA("FrmReclamos.colTecnico");
                grilla.Columns["colMotivo"].HeaderText = bll_idioma.Traducir_382QA("FrmReclamos.colMotivo");
            }

            CargarFiltroEstados_382QA();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_382QA(this);
            base.OnFormClosed(e);
        }
    }
}
