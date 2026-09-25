using BE;
using BLL.BLL_NEG;
using BLL.BLL_SER;
using System;
using System.Windows.Forms;

namespace UI
{
    public partial class frmGestionOT : Form
    {
        BLL_OT bll_ot = new BLL_OT();
        BLL_Cliente bll_cliente = new BLL_Cliente();

        // Se pone en true recién cuando txtDni_Leave confirma que el cliente existe,
        // para no tener que repetir la consulta a la base al apretar el botón.
        bool clienteValidado = false;

        public frmGestionOT()
        {
            InitializeComponent();
        }

        private void frmGestionOT_Load(object sender, EventArgs e)
        {
            HabilitarBotonesSegunEstado(null); // sin selección: solo "Recepcionar Equipo" activo

            // Selecciona "Todas" y dispara cbxEstadoFiltro_SelectedIndexChanged,
            // que ya se encarga de cargar la grilla sin filtro.
            cbxEstadoFiltro.SelectedIndex = 0;
        }

        private void CargarGrilla(string estadoFiltro = null)
        {
            grillaOrdenes.DataSource = null; // para refrescar correctamente
            grillaOrdenes.DataSource = bll_ot.ConsultarParaGrilla(estadoFiltro);
        }

        // ===================== Paso 2-3: lookup de cliente al salir del DNI =====================
        private void txtDni_Leave(object sender, EventArgs e)
        {
            string dni = txtDni.Text.Trim();
            clienteValidado = false;

            if (string.IsNullOrWhiteSpace(dni))
            {
                LimpiarDatosCliente();
                lblAyuda.Text = "Ingresá el DNI del cliente para comenzar.";
                return;
            }

            if (bll_cliente.ExisteCliente(dni))
            {
                var cliente = bll_cliente.ConsultarPorDni(dni);
                txtNombre.Text = cliente.Nombre;
                txtApellido.Text = cliente.Apellido;
                txtTelefono.Text = cliente.Telefono;

                clienteValidado = true;
                lblAyuda.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
                lblAyuda.Text = $"Cliente encontrado: {cliente.Nombre} {cliente.Apellido}. Completá los datos del equipo.";
            }
            else
            {
                LimpiarDatosCliente();
                lblAyuda.ForeColor = System.Drawing.Color.IndianRed;
                lblAyuda.Text = "Cliente no encontrado. Hace falta registrarlo antes de continuar.";

                var rdo = MessageBox.Show(
                    "El DNI no está registrado. ¿Desea registrar un nuevo cliente?",
                    "Cliente no encontrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rdo == DialogResult.Yes)
                {
                    // CU-NEG-6: Registrar nuevo cliente (pendiente de implementar)
                    // using (var frm = new frmRegistrarCliente(dni))
                    // {
                    //     if (frm.ShowDialog() == DialogResult.OK)
                    //         txtDni_Leave(sender, e); // reintenta la búsqueda tras registrar
                    // }
                }
            }
        }

        private void LimpiarDatosCliente()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
        }

        // ===================== CU-NEG-1: Gestionar recepción de equipo =====================
        private void btnRecepcionarEquipo_Click(object sender, EventArgs e)
        {
            try
            {
                string dni = txtDni.Text.Trim();
                if (string.IsNullOrWhiteSpace(dni))
                    throw new Exception("Debe ingresar el DNI del cliente.");

                // Por si el vendedor no tabuló fuera del campo (pegó el DNI y apretó el botón directo)
                if (!clienteValidado)
                {
                    txtDni_Leave(sender, e);
                    if (!clienteValidado) return; // sigue sin existir: se corta acá
                }

                // Pasos 4 a 8, orquestados en BLL_OT
                BE_OT ot = bll_ot.RecibirEquipo(
                    dni,
                    txtMarca.Text.Trim(),
                    txtModelo.Text.Trim(),
                    txtImei.Text.Trim(),
                    txtFallaReportada.Text.Trim());

                txtNroOt.Text = ot.NroOT.ToString();
                txtEstadoOt.Text = ot.Estado;
                txtTecnicoAsignado.Text = ot.DniTecnico;

                lblAyuda.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
                lblAyuda.Text = $"OT N° {ot.NroOT} generada y asignada a técnico {ot.DniTecnico}.";

                MessageBox.Show(
                    $"Equipo recibido correctamente.\nOrden de Trabajo N° {ot.NroOT}\nTécnico asignado: {ot.DniTecnico}",
                    "Recepción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarGrilla(cbxEstadoFiltro.SelectedItem?.ToString() == "Todas" ? null : cbxEstadoFiltro.SelectedItem?.ToString());
                LimpiarFormulario();
            }
            catch (ClienteNoRegistradoException ex)
            {
                MessageBox.Show(ex.Message, "Cliente no registrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Alt 5.1.1: campos vacíos, IMEI inválido, u otro error de negocio
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            txtDni.Clear();
            LimpiarDatosCliente();
            txtMarca.Clear();
            txtModelo.Clear();
            txtImei.Clear();
            txtFallaReportada.Clear();
            clienteValidado = false;
            lblAyuda.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblAyuda.Text = "Ingresá el DNI del cliente para comenzar.";
        }

        // ===================== Grilla y filtro (compartidos por CU-NEG-1 a 5) =====================
        private void grillaOrdenes_SelectionChanged(object sender, EventArgs e)
        {
            if (grillaOrdenes.CurrentRow == null || grillaOrdenes.CurrentRow.DataBoundItem == null)
            {
                HabilitarBotonesSegunEstado(null);
                return;
            }

            dynamic fila = grillaOrdenes.CurrentRow.DataBoundItem;

            txtNroOt.Text = fila.NroOT.ToString();
            txtEstadoOt.Text = fila.Estado;
            txtTecnicoAsignado.Text = fila.DniTecnico;

            HabilitarBotonesSegunEstado((string)fila.Estado);
        }

      
        private void HabilitarBotonesSegunEstado(string estado)
        {
            btnRecepcionarEquipo.Enabled = true; 
            btnRegistrarAprobacion.Enabled = estado == EstadosOT.Presupuestado;
            btnRegistrarRechazo.Enabled = estado == EstadosOT.Presupuestado;

         
            btnEntregarEquipo.Enabled = estado == EstadosOT.Reparado;
            btnRealizarCobro.Enabled = estado == EstadosOT.Reparado;

            bool esEstadoTerminal = estado == EstadosOT.Entregado || estado == EstadosOT.EntregadoSinReparar;
            btnAplicarCambios.Enabled = estado != null && !esEstadoTerminal;
        }

        private void cbxEstadoFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccion = cbxEstadoFiltro.SelectedItem?.ToString();
            string filtro = (string.IsNullOrEmpty(seleccion) || seleccion == "Todas") ? null : seleccion;
            CargarGrilla(filtro);
        }

        // ===================== Botones de otros CU (fuera de alcance por ahora) =====================
        private void btnRegistrarAprobacion_Click(object sender, EventArgs e)
        {
            // CU-NEG-2: Gestionar aprobación de presupuesto
        }

        private void btnRegistrarRechazo_Click(object sender, EventArgs e)
        {
            // CU-NEG-2: flujo alternativo de rechazo
        }

        private void btnEntregarEquipo_Click(object sender, EventArgs e)
        {
            // CU-NEG-3: Gestionar entrega
        }

        private void btnRealizarCobro_Click(object sender, EventArgs e)
        {
            // CU-NEG-3 «extend»: Realizar cobro
        }

        private void btnAplicarCambios_Click(object sender, EventArgs e)
        {
            // Pendiente de definir alcance
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}