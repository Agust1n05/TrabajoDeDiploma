using BLL.BLL_SER;
using Microsoft.VisualBasic;
using Servicio;
using System.Text.RegularExpressions;

namespace UI
{
    public partial class FrmGestionUsuarios : Form, IObservadorIdioma
    {
        BLL_Usuario bll_usuario;
        BLL_Idioma bll_idioma = new BLL_Idioma();
        BLL_Rol bll_rol;
        public FrmGestionUsuarios()
        {
            InitializeComponent();
            bll_idioma.Suscribir(this);
            ActualizarIdioma();
        }
        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            grillaUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            cbxRoles.DropDownStyle = ComboBoxStyle.DropDownList;
            grillaUsuarios.MultiSelect = false;
            bll_usuario = new BLL_Usuario();
            bll_rol = new BLL_Rol();
            Mostrar(grillaUsuarios, bll_usuario.ConsultarActivos());
            foreach (var c in bll_rol.Consultar())
            {
                cbxRoles.Items.Add(c);
            }
            SER_Usuario usuario = SER_SesionManager.ObtenerSesion().Usuario;

            btnCrearUsuario.Enabled = usuario.Rol.TienePermiso("Crear Usuario");
            btnModificarUsuario.Enabled = usuario.Rol.TienePermiso("Modificar Usuario");
            btnDesbloquearUsuario.Enabled = usuario.Rol.TienePermiso("Desbloquear Usuario");
            btnCambiarEstadoUsuario.Enabled = usuario.Rol.TienePermiso("Activar Usuario");
        }
        private void Mostrar(DataGridView grilla, object datos)
        {
            grilla.DataSource = null;
            grilla.DataSource = datos;
            TraducirColumnas(grilla);
            ContarUsuarios(lblNumero);
        }
        private void TraducirColumnas(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;  

            grilla.Columns["DNI"].HeaderText = bll_idioma.Traducir("FrmGestionUsuarios.colDni");
            grilla.Columns["Nombre"].HeaderText = bll_idioma.Traducir("FrmGestionUsuarios.colNombre");
            grilla.Columns["Apellido"].HeaderText = bll_idioma.Traducir("FrmGestionUsuarios.colApellido");
            grilla.Columns["Usuario"].HeaderText = bll_idioma.Traducir("FrmGestionUsuarios.colUsuario");
            grilla.Columns["Activo"].HeaderText = bll_idioma.Traducir("FrmGestionUsuarios.colActivo");
            grilla.Columns["Bloqueado"].HeaderText = bll_idioma.Traducir("FrmGestionUsuarios.colBloqueado");
            grilla.Columns["Rol"].HeaderText = bll_idioma.Traducir("FrmGestionUsuarios.colRol");
        }
        private void ContarUsuarios(Label lbl) 
        {
            int cant = bll_usuario.Consultar().Count();
            lbl.Text = $"{cant}";
        }
        private bool ValidarDatos(string texto, string expresionRegular)
        {
            Regex re = new Regex(expresionRegular);
            return re.IsMatch(texto);
        }
        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                string dni = txtDni.Text;
                if (!ValidarDatos(dni, @"^\d{8}$")) throw new Exception("El formato del DNI es incorrecto!");

                string nombre = txtNombre.Text;
                if (!ValidarDatos(nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]{1,50}$")) throw new Exception("El formato del nombre es incorrecto!");

                string apellido = txtApellido.Text;
                if (!ValidarDatos(apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]{1,50}$")) throw new Exception("El formato del apellido es incorrecto!");

                string correo = txtCorreo.Text;
                if (!ValidarDatos(correo, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")) throw new Exception("El formato del correo es incorrecto!");


                SER_Rol rol = (SER_Rol)cbxRoles.SelectedItem;
                SER_Usuario usuario = new SER_Usuario(dni, nombre, apellido, correo, false, true, rol, "es-AR");
                

                bll_usuario.Agregar(usuario);
                MessageBox.Show("Usuario creado con éxito!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarTextBox();
                if (radioButton1.Checked)
                {
                    Mostrar(grillaUsuarios, bll_usuario.ConsultarActivos());
                }
                else
                {
                    Mostrar(grillaUsuarios, bll_usuario.ConsultarParaGrilla());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDesbloquearUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaUsuarios.SelectedRows.Count == 0) throw new Exception("NO EXISTEN USUARIOS REGISTRADOS!");
                SER_Usuario usAux = new SER_Usuario(grillaUsuarios.SelectedRows[0].Cells["DNI"].Value.ToString());
                bll_usuario.Desbloquear(usAux);
                MessageBox.Show("El usuario con DNI " + usAux.Dni + " ha sido desbloqueado correctamente ", "Desbloqueo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Mostrar(grillaUsuarios, bll_usuario.ConsultarActivos());
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCambiarEstadoUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaUsuarios.SelectedRows.Count == 0) throw new Exception("NO EXISTEN USUARIOS REGISTRADOS!");
                SER_Usuario usAux = new SER_Usuario(grillaUsuarios.SelectedRows[0].Cells["DNI"].Value.ToString());
                SER_Usuario us = bll_usuario.ConsultarPorId(usAux);

                DialogResult res = MessageBox.Show($"¿Desea cambiar el estado de {us.NombreUsuario} de {(us.Activo ? "Activo" : "Inactivo")} a {(us.Activo ? "Inactivo" : "Activo")}?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    bll_usuario.CambiarEstadoActivo(usAux);
                    if (radioButton1.Checked)
                    {
                        Mostrar(grillaUsuarios, bll_usuario.ConsultarActivos());
                    }
                    else
                    {
                        Mostrar(grillaUsuarios, bll_usuario.ConsultarParaGrilla());
                    }
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnModificarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaUsuarios.SelectedRows.Count == 0) throw new Exception("NO EXISTEN USUARIOS REGISTRADOS");
                SER_Usuario usAux = new SER_Usuario(grillaUsuarios.SelectedRows[0].Cells["DNI"].Value.ToString());
                SER_Usuario us = bll_usuario.ConsultarPorId(usAux);
                string nombre = Interaction.InputBox("Ingrese nombre del usuario", "Modificar usuario", grillaUsuarios.SelectedRows[0].Cells["Nombre"].Value.ToString());
                if (!ValidarDatos(nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]{1,50}$")) throw new Exception("El formato del nombre es incorrecto!");

                string apellido = Interaction.InputBox("Ingrese apellido del usuario", "Modificar usuario", grillaUsuarios.SelectedRows[0].Cells["Apellido"].Value.ToString());
                if (!ValidarDatos(apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]{1,50}$")) throw new Exception("El formato del apellido es incorrecto!");

                string nombreUsuario = Interaction.InputBox("Ingrese nombre de usuario", "Modificar usuario", grillaUsuarios.SelectedRows[0].Cells["Usuario"].Value.ToString());
                if (!ValidarDatos(nombreUsuario, @"^[a-zA-Z0-9_]{1,50}$")) throw new Exception("El formato del nombre de usuario es incorrecto!");

                us.Nombre = nombre;
                us.Apellido = apellido;
                us.NombreUsuario = nombreUsuario;
                bll_usuario.Modificar(us);
                MessageBox.Show("El usuario con DNI " + us.Dni + " ha sido modificado correctamente ", "Modificacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (radioButton1.Checked)
                {
                    Mostrar(grillaUsuarios, bll_usuario.ConsultarActivos());
                }
                else
                {
                    Mostrar(grillaUsuarios, bll_usuario.ConsultarParaGrilla());
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAplicarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                
                string dni = txtDni.Text;
                string nombre = txtNombre.Text;
                string apellido = txtApellido.Text;
                string correo = txtCorreo.Text;
                string nombreUsuario = txtNombreUsuario.Text;
                SER_Rol rol = (SER_Rol)cbxRoles.SelectedItem;


                SER_Usuario usuario = new SER_Usuario(dni, nombre, apellido, correo, nombreUsuario);
                usuario.Rol = rol;
                Mostrar(grillaUsuarios, bll_usuario.ConsultarFiltrado(usuario));
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked) 
                {
                    Mostrar(grillaUsuarios,bll_usuario.ConsultarActivos());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(radioButton2.Checked)
                {
                    Mostrar(grillaUsuarios, bll_usuario.ConsultarParaGrilla());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void ActualizarIdioma()
        {
            this.Text = bll_idioma.Traducir("FrmGestionUsuarios.Form");
            lblTitulo.Text = bll_idioma.Traducir("FrmGestionUsuarios.lblTitulo");
            btnCrearUsuario.Text = bll_idioma.Traducir("FrmGestionUsuarios.btnCrearUsuario");
            btnDesbloquearUsuario.Text = bll_idioma.Traducir("FrmGestionUsuarios.btnDesbloquearUsuario");
            btnModificarUsuario.Text = bll_idioma.Traducir("FrmGestionUsuarios.btnModificarUsuario");
            btnCambiarEstadoUsuario.Text = bll_idioma.Traducir("FrmGestionUsuarios.btnCambiarEstadoUsuario");
            btnAplicarCambios.Text = bll_idioma.Traducir("FrmGestionUsuarios.btnAplicarCambios");
            btnSalir.Text = bll_idioma.Traducir("FrmGestionUsuarios.btnSalir");
            label1.Text = bll_idioma.Traducir("FrmGestionUsuarios.label1");
            lblDni.Text = bll_idioma.Traducir("FrmGestionUsuarios.lblDni");
            lblNombre.Text = bll_idioma.Traducir("FrmGestionUsuarios.lblNombre");
            lblApellido.Text = bll_idioma.Traducir("FrmGestionUsuarios.lblApellido");
            lblCorreo.Text = bll_idioma.Traducir("FrmGestionUsuarios.lblCorreo");
            lblFiltro.Text = bll_idioma.Traducir("FrmGestionUsuarios.lblFiltro");
            lblNumeroUsuarios.Text = bll_idioma.Traducir("FrmGestionUsuarios.lblNumeroUsuarios");
            radioButton1.Text = bll_idioma.Traducir("FrmGestionUsuarios.radioButton1");
            radioButton2.Text = bll_idioma.Traducir("FrmGestionUsuarios.radioButton2");
            TraducirColumnas(grillaUsuarios);   

        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir(this);
            base.OnFormClosed(e);
        }
        private void LimpiarTextBox()
        {
            txtDni.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtCorreo.Text = "";
            txtNombreUsuario.Text = "";
            cbxRoles.SelectedIndex = 0;
        }
    }
}
