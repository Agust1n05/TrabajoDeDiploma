using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using QuestPDF.Fluent;
using Servicio;
using System.Diagnostics;
using System.Globalization;

namespace UI
{
    /// <summary>
    /// CU-NEG-3 Gestionar entrega. Dos modos según el estado de la OT:
    ///   Reparado    → detalle del cobro + pago con tarjeta → comprobante de cobro, garantía y OT "Entregado".
    ///   Sin reparar → motivo, sin cobro ni garantía → OT "Entregado sin reparar".
    /// </summary>
    public partial class FrmEntregaEquipo_382QA : Form, IObservadorIdioma_382QA
    {
        private static readonly CultureInfo Moneda = CultureInfo.GetCultureInfo("es-AR");

        private readonly int nroOT;
        private readonly BLL_Entrega_382QA bll_entrega = new BLL_Entrega_382QA();
        private readonly BLL_Idioma_382QA bll_idioma = new BLL_Idioma_382QA();
        private BE_DetalleEntrega_382QA? detalle;
        private bool formateandoNumero;

        private sealed class OpcionTipo
        {
            public string Texto { get; }
            public string Valor { get; }
            public OpcionTipo(string texto, string valor) { Texto = texto; Valor = valor; }
            public override string ToString() => Texto;
        }

        public FrmEntregaEquipo_382QA(int nroOT)
        {
            InitializeComponent();
            this.nroOT = nroOT;
            bll_idioma.Suscribir_382QA(this);
        }

        private void FrmEntregaEquipo_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla_382QA();

            try
            {
                detalle = bll_entrega.ConsultarDetalle_382QA(nroOT);
            }
            catch (Exception ex)
            {
                MostrarError_382QA(ex.Message);
                btnEntregar.Enabled = false;
                ActualizarIdioma_382QA();
                return;
            }

            BE_OTResumen_382QA ot = detalle.OT;
            lblCliente.Text = string.Format(bll_idioma.Traducir_382QA("FrmPresupuesto.formatoCliente"), ot.Cliente, ot.TelefonoCliente);
            lblEquipo.Text = $"{ot.Equipo}  ·  IMEI {ot.Imei}";

            if (detalle.RequiereCobro)
            {
                grilla.DataSource = detalle.Lineas;
                lblManoObra.Text = FormatearMonto_382QA(detalle.CostoManoObra);
                lblTotal.Text = FormatearMonto_382QA(detalle.MontoACobrar);
                lblMotivo.Visible = false;
            }
            else
            {
                AcomodarSinReparar_382QA();
            }

            ActualizarIdioma_382QA();
        }

        /// <summary>Sin cobro: se muestra solo el motivo y se achica la ventana.</summary>
        private void AcomodarSinReparar_382QA()
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

        private void ConfigurarGrilla_382QA()
        {
            grilla.AutoGenerateColumns = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.ScrollBars = ScrollBars.Vertical;
            grilla.Columns.Clear();
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCodigo", DataPropertyName = "Codigo", FillWeight = 55 });
            grilla.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDescripcion", DataPropertyName = "Descripcion", FillWeight = 160 });
            grilla.Columns.Add(ColumnaNumerica_382QA("colPrecio", "PrecioUnitario", "N2", 70));
            grilla.Columns.Add(ColumnaNumerica_382QA("colCantidad", "Cantidad", "N0", 40));
            grilla.Columns.Add(ColumnaNumerica_382QA("colSubtotal", "Subtotal", "N2", 75));
        }

        private static DataGridViewTextBoxColumn ColumnaNumerica_382QA(string nombre, string propiedad, string formato, int peso)
        {
            var columna = new DataGridViewTextBoxColumn { Name = nombre, DataPropertyName = propiedad, FillWeight = peso };
            columna.DefaultCellStyle.Format = formato;
            columna.DefaultCellStyle.FormatProvider = Moneda;
            columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            return columna;
        }

        private static string FormatearMonto_382QA(decimal monto) => $"$ {monto.ToString("N2", Moneda)}";

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

            BE_DatosTarjeta_382QA? tarjeta = null;
            string pregunta;
            if (detalle.RequiereCobro)
            {
                tarjeta = new BE_DatosTarjeta_382QA
                {
                    Tipo = (cboTipo.SelectedItem as OpcionTipo)?.Valor,
                    Numero = txtNumero.Text,
                    Titular = txtTitular.Text,
                    Vencimiento = txtVencimiento.Text,
                    Cvv = txtCvv.Text
                };
                pregunta = string.Format(bll_idioma.Traducir_382QA("FrmEntregaEquipo.msgConfirmarCobro"),
                                         lblTotal.Text, cboTipo.Text, tarjeta.UltimosDigitos, nroOT);
            }
            else
            {
                pregunta = string.Format(bll_idioma.Traducir_382QA("FrmEntregaEquipo.msgConfirmarSinReparar"), nroOT);
            }

            if (MessageBox.Show(pregunta, bll_idioma.Traducir_382QA("ABM.Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                BE_ResultadoEntrega_382QA resultado = bll_entrega.EntregarEquipo_382QA(nroOT, tarjeta);
                string mensaje = resultado.ComprobanteCobro != null
                    ? string.Format(bll_idioma.Traducir_382QA("FrmEntregaEquipo.msgOkCobro"), resultado.OT.NroOT,
                                    resultado.ComprobanteCobro.NroComprobante, FormatearMonto_382QA(resultado.ComprobanteCobro.MontoCobrado),
                                    resultado.Garantia!.FechaVencimiento.ToShortDateString())
                    : string.Format(bll_idioma.Traducir_382QA("FrmEntregaEquipo.msgOkSinReparar"), resultado.OT.NroOT, resultado.OT.Estado);
                MessageBox.Show(mensaje, bll_idioma.Traducir_382QA("ABM.Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (resultado.ComprobanteCobro != null)
                {
                    // La entrega ya quedó registrada: si falla el PDF solo se avisa, no se trata como error del cobro
                    try
                    {
                        string ruta = Path.Combine(Path.GetTempPath(), $"ComprobanteCobro_{resultado.ComprobanteCobro.NroComprobante}.pdf");
                        GenerarPdfComprobante_382QA(resultado, ruta);
                        Process.Start(new ProcessStartInfo { FileName = ruta, UseShellExecute = true });
                    }
                    catch (Exception exPdf)
                    {
                        MessageBox.Show("No se pudo generar el PDF del comprobante: " + exPdf.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MostrarError_382QA(ex.Message);
            }
            finally
            {
                txtCvv.Clear();   // el CVV no se conserva ni en pantalla después de intentar el cobro
            }
        }

        /// <summary>PDF del comprobante de cobro (mismo uso de QuestPDF que el reporte de bitácora).</summary>
        private void GenerarPdfComprobante_382QA(BE_ResultadoEntrega_382QA resultado, string ruta)
        {
            BE_ComprobanteCobro_382QA comprobante = resultado.ComprobanteCobro!;
            BE_OTResumen_382QA ot = detalle!.OT;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Column(col =>
                    {
                        col.Item().Text("San Tsung - Comprobante de cobro").FontSize(18).Bold();
                        col.Item().Text($"Comprobante N° {comprobante.NroComprobante}   ·   Fecha: {comprobante.FechaEntrega.ToShortDateString()}   ·   OT N° {comprobante.NroOT}");
                    });

                    page.Content().PaddingVertical(15).Column(col =>
                    {
                        col.Spacing(6);
                        col.Item().Text($"Cliente: {ot.Cliente}   ·   DNI: {ot.DniCliente}   ·   Tel.: {ot.TelefonoCliente}");
                        col.Item().Text($"Equipo: {ot.Equipo}   ·   IMEI: {ot.Imei}");

                        col.Item().PaddingTop(10).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(1.3f);
                                columns.RelativeColumn(0.8f);
                                columns.RelativeColumn(1.3f);
                            });
                            table.Header(header =>
                            {
                                foreach (string titulo in new[] { "Código", "Descripción", "Precio unit.", "Cant.", "Subtotal" })
                                    header.Cell().Border(1).Padding(5).Text(titulo).Bold();
                            });
                            foreach (BE_LineaPresupuesto_382QA linea in detalle.Lineas)
                            {
                                table.Cell().Border(1).Padding(5).Text(linea.Codigo);
                                table.Cell().Border(1).Padding(5).Text(linea.Descripcion);
                                table.Cell().Border(1).Padding(5).AlignRight().Text(FormatearMonto_382QA(linea.PrecioUnitario));
                                table.Cell().Border(1).Padding(5).AlignRight().Text(linea.Cantidad.ToString());
                                table.Cell().Border(1).Padding(5).AlignRight().Text(FormatearMonto_382QA(linea.Subtotal));
                            }
                        });

                        col.Item().AlignRight().Text($"Mano de obra: {FormatearMonto_382QA(detalle.CostoManoObra)}");
                        col.Item().AlignRight().Text($"Total cobrado: {FormatearMonto_382QA(comprobante.MontoCobrado)}").FontSize(14).Bold();
                        col.Item().PaddingTop(10).Text($"Medio de pago: tarjeta de {(comprobante.MedioPago == MediosPago_382QA.Debito ? "débito" : "crédito")} terminada en {comprobante.UltimosDigitos}");
                        col.Item().Text($"Garantía: {BE_Garantia_382QA.DiasGarantia} días, vence el {resultado.Garantia!.FechaVencimiento.ToShortDateString()}");
                    });

                    page.Footer().AlignCenter().Text("Comprobante interno, no válido como factura.").FontSize(9);
                });
            }).GeneratePdf(ruta);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void MostrarError_382QA(string mensaje) =>
            MessageBox.Show(mensaje, bll_idioma.Traducir_382QA("ABM.Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void ActualizarIdioma_382QA()
        {
            bool cobro = detalle?.RequiereCobro ?? true;

            Text = bll_idioma.Traducir_382QA("FrmEntregaEquipo.Form");
            lblTitulo.Text = bll_idioma.Traducir_382QA("FrmEntregaEquipo.lblTitulo");
            lblPasoDetalle.Text = bll_idioma.Traducir_382QA(cobro ? "FrmEntregaEquipo.lblPasoDetalle" : "FrmEntregaEquipo.lblPasoSinReparar");
            lblTitManoObra.Text = bll_idioma.Traducir_382QA("FrmPresupuesto.lblManoObra");
            lblTitTotal.Text = bll_idioma.Traducir_382QA("FrmEntregaEquipo.lblTotal");
            lblGarantia.Text = cobro
                ? string.Format(bll_idioma.Traducir_382QA("FrmEntregaEquipo.lblGarantia"), BE_Garantia_382QA.DiasGarantia,
                                DateTime.Today.AddDays(BE_Garantia_382QA.DiasGarantia).ToShortDateString())
                : bll_idioma.Traducir_382QA("FrmEntregaEquipo.lblSinGarantia");
            lblPasoPago.Text = bll_idioma.Traducir_382QA("FrmEntregaEquipo.lblPasoPago");
            lblTipo.Text = bll_idioma.Traducir_382QA("FrmEntregaEquipo.lblTipo");
            lblTitular.Text = bll_idioma.Traducir_382QA("FrmEntregaEquipo.lblTitular");
            lblNumero.Text = bll_idioma.Traducir_382QA("FrmEntregaEquipo.lblNumero");
            lblVencimiento.Text = bll_idioma.Traducir_382QA("FrmEntregaEquipo.lblVencimiento");
            lblCvv.Text = bll_idioma.Traducir_382QA("FrmEntregaEquipo.lblCvv");
            lblAvisoTarjeta.Text = bll_idioma.Traducir_382QA("FrmEntregaEquipo.lblAvisoTarjeta");
            btnEntregar.Text = bll_idioma.Traducir_382QA(cobro ? "FrmEntregaEquipo.btnCobrar" : "FrmEntregaEquipo.btnEntregarSinReparar");
            btnCancelar.Text = bll_idioma.Traducir_382QA("ABM.btnCancelar");

            if (detalle != null)
            {
                lblOT.Text = $"{string.Format(bll_idioma.Traducir_382QA("FrmOrdenesTrabajo.formatoNroOT"), detalle.OT.NroOT)}  ·  {detalle.OT.Estado}";
                if (!cobro)
                    lblMotivo.Text = bll_idioma.Traducir_382QA(detalle.PresupuestoRechazado
                        ? "FrmEntregaEquipo.motivoRechazado" : "FrmEntregaEquipo.motivoSinStock");
            }

            // Combo de tipo: textos del idioma, manteniendo la opción elegida
            int seleccion = cboTipo.SelectedIndex;
            cboTipo.Items.Clear();
            cboTipo.Items.Add(new OpcionTipo(bll_idioma.Traducir_382QA("FrmEntregaEquipo.tipoDebito"), MediosPago_382QA.Debito));
            cboTipo.Items.Add(new OpcionTipo(bll_idioma.Traducir_382QA("FrmEntregaEquipo.tipoCredito"), MediosPago_382QA.Credito));
            cboTipo.SelectedIndex = seleccion;

            if (grilla.Columns.Count > 0)
            {
                grilla.Columns["colCodigo"].HeaderText = bll_idioma.Traducir_382QA("FrmDiagnosticoEquipo.colCodigo");
                grilla.Columns["colDescripcion"].HeaderText = bll_idioma.Traducir_382QA("FrmDiagnosticoEquipo.colDescripcion");
                grilla.Columns["colPrecio"].HeaderText = bll_idioma.Traducir_382QA("FrmDiagnosticoEquipo.colPrecio");
                grilla.Columns["colCantidad"].HeaderText = bll_idioma.Traducir_382QA("FrmDiagnosticoEquipo.colCantidad");
                grilla.Columns["colSubtotal"].HeaderText = bll_idioma.Traducir_382QA("FrmDiagnosticoEquipo.colSubtotal");
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_382QA(this);
            base.OnFormClosed(e);
        }
    }
}
