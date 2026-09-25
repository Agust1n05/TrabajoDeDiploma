using BE;
using BLL;
using BLL.BLL_NEG;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;

namespace UI
{
    public partial class FrmGestionClientes : Form
    {
        private readonly BLL_Cliente _blCliente;
        private List<BE_Cliente> _clientes;
        private BE_Cliente _clienteSeleccionado;

        public FrmGestionClientes()
        {
            InitializeComponent();
            _blCliente = new BLL_Cliente();
        }

        private void FrmGestionClientes_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla();
            CargarClientes();
            LimpiarFormulario();
        }

        private void ConfigurarGrilla()
        {
            grillaClientes.AutoGenerateColumns = false;
            grillaClientes.Columns.Clear();

            grillaClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDni",
                HeaderText = "DNI",
                DataPropertyName = "Dni",
                Width = 120
            });
            grillaClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 220
            });
            grillaClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colApellido",
                HeaderText = "Apellido",
                DataPropertyName = "Apellido",
                Width = 220
            });
            grillaClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTelefono",
                HeaderText = "Teléfono",
                DataPropertyName = "Telefono",
                Width = 220
            });
        }

        private void CargarClientes()
        {
            try
            {
                _clientes = _blCliente.Consultar();
                ActualizarGrilla(_clientes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los clientes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _clientes = new List<BE_Cliente>();
            }
        }

        private void ActualizarGrilla(List<BE_Cliente> clientes)
        {
            grillaClientes.DataSource = null;
            grillaClientes.DataSource = clientes;
            lblNumero.Text = clientes.Count.ToString();
        }

        private void txtBuscarDni_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscarDni.Text.Trim();

            var resultado = string.IsNullOrEmpty(filtro)
                ? _clientes
                : _clientes.Where(c => c.Dni != null && c.Dni.Contains(filtro)).ToList();

            ActualizarGrilla(resultado);
        }

        private void grillaClientes_SelectionChanged(object sender, EventArgs e)
        {
            if (grillaClientes.CurrentRow == null || grillaClientes.CurrentRow.DataBoundItem == null)
            {
                _clienteSeleccionado = null;
                return;
            }

            _clienteSeleccionado = grillaClientes.CurrentRow.DataBoundItem as BE_Cliente;

            if (_clienteSeleccionado != null)
            {
                txtDni.Text = _clienteSeleccionado.Dni;
                txtNombre.Text = _clienteSeleccionado.Nombre;
                txtApellido.Text = _clienteSeleccionado.Apellido;
                txtTelefono.Text = _clienteSeleccionado.Telefono;
            }
        }

        private void btnCrearCliente_Click(object sender, EventArgs e)
        {
            _clienteSeleccionado = null;
            LimpiarFormulario();
            txtDni.Focus();
        }

        private void btnModificarCliente_Click(object sender, EventArgs e)
        {
            if (_clienteSeleccionado == null)
            {
                MessageBox.Show("Seleccioná un cliente de la grilla para modificar.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtDni.Focus();
        }

        private void btnEliminarCliente_Click(object sender, EventArgs e)
        {
            if (_clienteSeleccionado == null)
            {
                MessageBox.Show("Seleccioná un cliente de la grilla para eliminar.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show(
                $"¿Seguro que querés eliminar al cliente {_clienteSeleccionado.Nombre} {_clienteSeleccionado.Apellido}?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes)
                return;

            try
            {
                _blCliente.Borrar(_clienteSeleccionado.Dni);
                MessageBox.Show("Cliente eliminado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _clienteSeleccionado = null;
                LimpiarFormulario();
                CargarClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el cliente: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAplicarCambios_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
                return;

            try
            {
                var cliente = new BE_Cliente(
                    txtDni.Text.Trim(),
                    txtNombre.Text.Trim(),
                    txtApellido.Text.Trim(),
                    txtTelefono.Text.Trim());

                if (_clienteSeleccionado == null)
                {
                    _blCliente.Agregar(cliente);
                    MessageBox.Show("Cliente creado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _blCliente.Modificar(cliente);
                    MessageBox.Show("Cliente modificado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                _clienteSeleccionado = null;
                LimpiarFormulario();
                CargarClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el cliente: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtDni.Text))
            {
                MessageBox.Show("El DNI es obligatorio.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDni.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El apellido es obligatorio.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarFormulario()
        {
            txtDni.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
