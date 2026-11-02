using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using Servicio;
using System.Globalization;

namespace UI
{
    /// <summary>
    /// CU-NEG-3 Gestionar entrega. Dos modos según el estado de la OT:
    ///   Reparado    → detalle del cobro + pago con tarjeta → comprobante de cobro, garantía y OT "Entregado".
    ///   Sin reparar → motivo, sin cobro ni garantía → OT "Entregado sin reparar".
    /// </summary>
    public partial class FrmEntregaEquipo : Form, IObservadorIdioma
    {
        private static readonly CultureInfo Moneda = CultureInfo.GetCultureInfo("es-AR");

        private readonly int nroOT;
        private readonly BLL_Entrega bll_entrega = new BLL_Entrega();
        private readonly BLL_Idioma bll_idioma = new BLL_Idioma();
        private BE_DetalleEntrega? detalle;
        private bool formateandoNumero;

        private sealed class OpcionTipo
        {
            public string Texto { get; }
            public string Valor { get; }
            public OpcionTipo(string texto, string valor) { Texto = texto; Valor = valor; }
            public override string ToString() => Texto;
        }

        public FrmEntregaEquipo(int nroOT)
        {
            InitializeComponent();
            this.nroOT = nroOT;
            bll_idioma.Suscribir(this);
        }

        private void FrmEntregaEquipo_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla();

            try
            {
                detalle = bll_entrega.ConsultarDetalle(nroOT);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
                btnEntregar.Enabled = false;
                ActualizarIdioma();
                return;
            }

            BE_OTResumen ot = detalle.OT;
            lblCliente.Text = string.Format(bll_idioma.Traducir("FrmPresupuesto.formatoCliente"), ot.Cliente, ot.TelefonoCliente);
            lblEquipo.Text = $"{ot.Equipo}  ·  IMEI {ot.Imei}";

            if (detalle.RequiereCobro)
            {
                grilla.DataSource = detalle.Lineas;
                lblManoObra.Text = FormatearMonto(detalle.CostoManoObra);
                lblTotal.Text = FormatearMonto(detalle.MontoACobrar);
                lblMotivo.Visible = false;
            }
            else
            {
                AcomodarSinReparar();
            }

            ActualizarIdioma();
        }

        /// <summary>Sin cobro: se muestra solo el motivo y se achica la ventana.</summary>
        private void AcomodarSinReparar()
        {
            foreach (Control c in new Control[] { grilla, lblTitManoObra, lblManoObra, lblTitTotal, lblTotal })
                c.Visible = false;
            pnlPago.Visible = false;

            lblMotivo.Visible = true;
            lblGarantia.Top = lblMotivo.Bottom + 8;
            lblGarantia.Height = 22;
            lblGarantia.ForeColor = Color.FromArgb(148, 163, 184);   // sin garantía: gris en vez de verde
            pnlDetalle.Height = lblGarantia.Bottom + 16;

            int y = pnlDetalle.Bottom + 16;
            btnEntregar.Top = y;
            btnCancelar.Top = y;
            ClientSize = new Size(ClientSize.Width, btnCancelar.Bottom + 16);
        }

        private void ConfigurarGrilla()
        {
            grilla.AutoGenerateColumns = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.ScrollBars = ScrollBars.Vertical;
            grilla.Columns.Clear();
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCodigo", DataPropertyName = "Codigo", FillWeight = 55 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDescripcion", DataPropertyName = "Descripcion", FillWeight = 160 });
            grilla.Columns.Add(ColumnaNumerica("colPrecio", "PrecioUnitario", "N2", 70));
            grilla.Columns.Add(ColumnaNumerica("colCantidad", "Cantidad", "N0", 40));
            grilla.Columns.Add(ColumnaNumerica("colSubtotal", "Subtotal", "N2", 75));
        }

        private static DataGridViewTextBoxColumn ColumnaNumerica(string nombre, string propiedad, string formato, int peso)
        {
            var columna = new DataGridViewTextBoxColumn { Name = nombre, DataPropertyName = propiedad, FillWeight = peso };
            columna.DefaultCellStyle.Format = formato;
            columna.DefaultCellStyle.FormatProvider = Moneda;
            columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            return columna;
        }

        private static string FormatearMonto(decimal monto) => $"$ {monto.ToString("N2", Moneda)}";

        /// <summary>Deja solo dígitos y los agrupa de a 4 para que el número sea fácil de leer y controlar.</summary>
        private void txtNumero_TextChanged(object sender, EventArgs e)
        {
            if (formateandoNumero) return;
            string digitos = new string(txtNumero.Text.Where(char.IsDigit).Take(19).ToArray());
            string agrupado = string.Join(" ", Enumerable.Range(0, (digitos.Length + 3) / 4)
                .Select(i => digitos.Substring(i * 4, Math.Min(4, digitos.Length - i * 4))));
            if (agrupado == txtNumero.Text) return;

            formateandoNumero = true;
            txtNumero.Text = agrupado;
            txtNumero.SelectionStart = agrupado.Length;
            formateandoNumero = false;
        }

        private void btnEntregar_Click(object sender, EventArgs e)
        {
            if (detalle == null) return;

            BE_DatosTarjeta? tarjeta = null;
            string pregunta;
            if (detalle.RequiereCobro)
            {
                tarjeta = new BE_DatosTarjeta
                {
                    Tipo = (cboTipo.SelectedItem as OpcionTipo)?.Valor,
                    Numero = txtNumero.Text,
                    Titular = txtTitular.Text,
                    Vencimiento = txtVencimiento.Text,
                    Cvv = txtCvv.Text
                };
                pregunta = string.Format(bll_idioma.Traducir("FrmEntregaEquipo.msgConfirmarCobro"),
                                         lblTotal.Text, cboTipo.Text, tarjeta.UltimosDigitos, nroOT);
            }
            else
            {
                pregunta = string.Format(bll_idioma.Traducir("FrmEntregaEquipo.msgConfirmarSinReparar"), nroOT);
            }

            if (MessageBox.Show(pregunta, bll_idioma.Traducir("ABM.Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                BE_ResultadoEntrega resultado = bll_entrega.EntregarEquipo(nroOT, tarjeta);
                string mensaje = resultado.ComprobanteCobro != null
                    ? string.Format(bll_idioma.Traducir("FrmEntregaEquipo.msgOkCobro"), resultado.OT.NroOT,
                                    resultado.ComprobanteCobro.NroComprobante, FormatearMonto(resultado.ComprobanteCobro.MontoCobrado),
                                    resultado.Garantia!.FechaVencimiento.ToShortDateString())
                    : string.Format(bll_idioma.Traducir("FrmEntregaEquipo.msgOkSinReparar"), resultado.OT.NroOT, resultado.OT.Estado);
                MessageBox.Show(mensaje, bll_idioma.Traducir("ABM.Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
            finally
            {
                txtCvv.Clear();   // el CVV no se conserva ni en pantalla después de intentar el cobro
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void MostrarError(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir("ABM.Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void ActualizarIdioma()
        {
            bool cobro = detalle?.RequiereCobro ?? true;

            Text = bll_idioma.Traducir("FrmEntregaEquipo.Form");
            lblTitulo.Text = bll_idioma.Traducir("FrmEntregaEquipo.lblTitulo");
            lblPasoDetalle.Text = bll_idioma.Traducir(cobro ? "FrmEntregaEquipo.lblPasoDetalle" : "FrmEntregaEquipo.lblPasoSinReparar");
            lblTitManoObra.Text = bll_idioma.Traducir("FrmPresupuesto.lblManoObra");
            lblTitTotal.Text = bll_idioma.Traducir("FrmEntregaEquipo.lblTotal");
            lblGarantia.Text = cobro
                ? string.Format(bll_idioma.Traducir("FrmEntregaEquipo.lblGarantia"), BE_Garantia.DiasGarantia,
                                DateTime.Today.AddDays(BE_Garantia.DiasGarantia).ToShortDateString())
                : bll_idioma.Traducir("FrmEntregaEquipo.lblSinGarantia");
            lblPasoPago.Text = bll_idioma.Traducir("FrmEntregaEquipo.lblPasoPago");
            lblTipo.Text = bll_idioma.Traducir("FrmEntregaEquipo.lblTipo");
            lblTitular.Text = bll_idioma.Traducir("FrmEntregaEquipo.lblTitular");
            lblNumero.Text = bll_idioma.Traducir("FrmEntregaEquipo.lblNumero");
            lblVencimiento.Text = bll_idioma.Traducir("FrmEntregaEquipo.lblVencimiento");
            lblCvv.Text = bll_idioma.Traducir("FrmEntregaEquipo.lblCvv");
            lblAvisoTarjeta.Text = bll_idioma.Traducir("FrmEntregaEquipo.lblAvisoTarjeta");
            btnEntregar.Text = bll_idioma.Traducir(cobro ? "FrmEntregaEquipo.btnCobrar" : "FrmEntregaEquipo.btnEntregarSinReparar");
            btnCancelar.Text = bll_idioma.Traducir("ABM.btnCancelar");

            if (detalle != null)
            {
                lblOT.Text = $"{string.Format(bll_idioma.Traducir("FrmOrdenesTrabajo.formatoNroOT"), detalle.OT.NroOT)}  ·  {detalle.OT.Estado}";
                if (!cobro)
                    lblMotivo.Text = bll_idioma.Traducir(detalle.PresupuestoRechazado
                        ? "FrmEntregaEquipo.motivoRechazado" : "FrmEntregaEquipo.motivoSinStock");
            }

            // Combo de tipo: textos del idioma, manteniendo la opción elegida
            int seleccion = cboTipo.SelectedIndex;
            cboTipo.Items.Clear();
            cboTipo.Items.Add(new OpcionTipo(bll_idioma.Traducir("FrmEntregaEquipo.tipoDebito"), MediosPago.Debito));
            cboTipo.Items.Add(new OpcionTipo(bll_idioma.Traducir("FrmEntregaEquipo.tipoCredito"), MediosPago.Credito));
            cboTipo.SelectedIndex = seleccion;

            if (grilla.Columns.Count > 0)
            {
                grilla.Columns["colCodigo"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colCodigo");
                grilla.Columns["colDescripcion"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colDescripcion");
                grilla.Columns["colPrecio"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colPrecio");
                grilla.Columns["colCantidad"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colCantidad");
                grilla.Columns["colSubtotal"].HeaderText = bll_idioma.Traducir("FrmDiagnosticoEquipo.colSubtotal");
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}
