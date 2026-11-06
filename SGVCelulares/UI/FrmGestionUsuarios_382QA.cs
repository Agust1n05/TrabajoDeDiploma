using BLL.BLL_SER;
using Microsoft.VisualBasic;
using Servicio;
using System.Text.RegularExpressions;

namespace UI
{
    public partial class FrmGestionUsuarios_382QA : Form, IObservadorIdioma_382QA
    {
        BLL_Usuario_382QA bll_usuario;
        BLL_Idioma_382QA bll_idioma = new BLL_Idioma_382QA();
        BLL_Rol_382QA bll_rol;
        public FrmGestionUsuarios_382QA()
        {
            InitializeComponent();
            bll_idioma.Suscribir_382QA(this);
            ActualizarIdioma_382QA();
        }
        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            grillaUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            cbxRoles.DropDownStyle = ComboBoxStyle.DropDownList;
            grillaUsuarios.MultiSelect = false;
            bll_usuario = new BLL_Usuario_382QA();
            bll_rol = new BLL_Rol_382QA();
            Mostrar_382QA(grillaUsuarios, bll_usuario.ConsultarActivos_382QA());
            foreach (var c in bll_rol.Consultar_382QA())
            {
                cbxRoles.Items.Add(c);
            }
            SER_Usuario_382QA usuario = SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario;

            btnCrearUsuario.Enabled = usuario.Rol.TienePermiso_382QA("Crear Usuario");
            btnModificarUsuario.Enabled = usuario.Rol.TienePermiso_382QA("Modificar Usuario");
            btnDesbloquearUsuario.Enabled = usuario.Rol.TienePermiso_382QA("Desbloquear Usuario");
            btnCambiarEstadoUsuario.Enabled = usuario.Rol.TienePermiso_382QA("Activar Usuario");
        }
        private void Mostrar_382QA(DataGridView grilla, object datos)
        {
            grilla.DataSource = null;
            grilla.DataSource = datos;
            TraducirColumnas_382QA(grilla);
            ContarUsuarios_382QA(lblNumero);
        }
        private void TraducirColumnas_382QA(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;  

            grilla.Columns["DNI"].HeaderText = bll_idioma.Traducir_382QA("FrmGestionUsuarios.colDni");
            grilla.Columns["Nombre"].HeaderText = bll_idioma.Traducir_382QA("FrmGestionUsuarios.colNombre");
            grilla.Columns["Apellido"].HeaderText = bll_idioma.Traducir_382QA("FrmGestionUsuarios.colApellido");
            grilla.Columns["Usuario"].HeaderText = bll_idioma.Traducir_382QA("FrmGestionUsuarios.colUsuario");
            grilla.Columns["Activo"].HeaderText = bll_idioma.Traducir_382QA("FrmGestionUsuarios.colActivo");
            grilla.Columns["Bloqueado"].HeaderText = bll_idioma.Traducir_382QA("FrmGestionUsuarios.colBloqueado");
            grilla.Columns["Rol"].HeaderText = bll_idioma.Traducir_382QA("FrmGestionUsuarios.colRol");
        }
        private void ContarUsuarios_382QA(Label lbl) 
        {
            int cant = bll_usuario.Consultar_382QA().Count();
            lbl.Text = $"{cant}";
        }
        private bool ValidarDatos_382QA(string texto, string expresionRegular)
        {
            Regex re = new Regex(expresionRegular);
            return re.IsMatch(texto);
        }
        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                string dni = txtDni.Text;
                if (!ValidarDatos_382QA(dni, @"^\d{8}$")) throw new Exception("El formato del DNI es incorrecto!");

                string nombre = txtNombre.Text;
                if (!ValidarDatos_382QA(nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]{1,50}$")) throw new Exception("El formato del nombre es incorrecto!");

                string apellido = txtApellido.Text;
                if (!ValidarDatos_382QA(apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]{1,50}$")) throw new Exception("El formato del apellido es incorrecto!");

                string correo = txtCorreo.Text;
                if (!ValidarDatos_382QA(correo, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")) throw new Exception("El formato del correo es incorrecto!");


                SER_Rol_382QA rol = (SER_Rol_382QA)cbxRoles.SelectedItem;
                SER_Usuario_382QA usuario = new SER_Usuario_382QA(dni, nombre, apellido, correo, false, true, rol, "es-AR");
                

                bll_usuario.Agregar_382QA(usuario);
                MessageBox.Show("Usuario creado con éxito!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarTextBox_382QA();
                if (radioButton1.Checked)
                {
                    Mostrar_382QA(grillaUsuarios, bll_usuario.ConsultarActivos_382QA());
                }
                else
                {
                    Mostrar_382QA(grillaUsuarios, bll_usuario.ConsultarParaGrilla_382QA());
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
                SER_Usuario_382QA usAux = new SER_Usuario_382QA(grillaUsuarios.SelectedRows[0].Cells["DNI"].Value.ToString());
                bll_usuario.Desbloquear_382QA(usAux);
                MessageBox.Show("El usuario con DNI " + usAux.Dni + " ha sido desbloqueado correctamente ", "Desbloqueo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Mostrar_382QA(grillaUsuarios, bll_usuario.ConsultarActivos_382QA());
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
                SER_Usuario_382QA usAux = new SER_Usuario_382QA(grillaUsuarios.SelectedRows[0].Cells["DNI"].Value.ToString());
                SER_Usuario_382QA us = bll_usuario.ConsultarPorId_382QA(usAux);

                DialogResult res = MessageBox.Show($"¿Desea cambiar el estado de {us.NombreUsuario} de {(us.Activo ? "Activo" : "Inactivo")} a {(us.Activo ? "Inactivo" : "Activo")}?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    bll_usuario.CambiarEstadoActivo_382QA(usAux);
                    if (radioButton1.Checked)
                    {
                        Mostrar_382QA(grillaUsuarios, bll_usuario.ConsultarActivos_382QA());
                    }
                    else
                    {
                        Mostrar_382QA(grillaUsuarios, bll_usuario.ConsultarParaGrilla_382QA());
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
                SER_Usuario_382QA usAux = new SER_Usuario_382QA(grillaUsuarios.SelectedRows[0].Cells["DNI"].Value.ToString());
                SER_Usuario_382QA us = bll_usuario.ConsultarPorId_382QA(usAux);
                string nombre = Interaction.InputBox("Ingrese nombre del usuario", "Modificar usuario", grillaUsuarios.SelectedRows[0].Cells["Nombre"].Value.ToString());
                if (!ValidarDatos_382QA(nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]{1,50}$")) throw new Exception("El formato del nombre es incorrecto!");

                string apellido = Interaction.InputBox("Ingrese apellido del usuario", "Modificar usuario", grillaUsuarios.SelectedRows[0].Cells["Apellido"].Value.ToString());
                if (!ValidarDatos_382QA(apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]{1,50}$")) throw new Exception("El formato del apellido es incorrecto!");

                string nombreUsuario = Interaction.InputBox("Ingrese nombre de usuario", "Modificar usuario", grillaUsuarios.SelectedRows[0].Cells["Usuario"].Value.ToString());
                if (!ValidarDatos_382QA(nombreUsuario, @"^[a-zA-Z0-9_]{1,50}$")) throw new Exception("El formato del nombre de usuario es incorrecto!");

                us.Nombre = nombre;
                us.Apellido = apellido;
                us.NombreUsuario = nombreUsuario;
                bll_usuario.Modificar_382QA(us);
                MessageBox.Show("El usuario con DNI " + us.Dni + " ha sido modificado correctamente ", "Modificacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (radioButton1.Checked)
                {
                    Mostrar_382QA(grillaUsuarios, bll_usuario.ConsultarActivos_382QA());
                }
                else
                {
                    Mostrar_382QA(grillaUsuarios, bll_usuario.ConsultarParaGrilla_382QA());
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
                SER_Rol_382QA rol = (SER_Rol_382QA)cbxRoles.SelectedItem;


                SER_Usuario_382QA usuario = new SER_Usuario_382QA(dni, nombre, apellido, correo, nombreUsuario);
                usuario.Rol = rol;
                Mostrar_382QA(grillaUsuarios, bll_usuario.ConsultarFiltrado_382QA(usuario));
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
                    Mostrar_382QA(grillaUsuarios,bll_usuario.ConsultarActivos_382QA());
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
                    Mostrar_382QA(grillaUsuarios, bll_usuario.ConsultarParaGrilla_382QA());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void ActualizarIdioma_382QA()
        {
            this.Text = bll_idioma.Traducir_382QA("FrmGestionUsuarios.Form");
            lblTitulo.Text = bll_idioma.Traducir_382QA("FrmGestionUsuarios.lblTitulo");
            btnCrearUsuario.Text = bll_idioma.Traducir_382QA("FrmGestionUsuarios.btnCrearUsuario");
            btnDesbloquearUsuario.Text = bll_idioma.Traducir_382QA("FrmGestionUsuarios.btnDesbloquearUsuario");
            btnModificarUsuario.Text = bll_idioma.Traducir_382QA("FrmGestionUsuarios.btnModificarUsuario");
            btnCambiarEstadoUsuario.Text = bll_idioma.Traducir_382QA("FrmGestionUsuarios.btnCambiarEstadoUsuario");
            btnAplicarCambios.Text = bll_idioma.Traducir_382QA("FrmGestionUsuarios.btnAplicarCambios");
            btnSalir.Text = bll_idioma.Traducir_382QA("FrmGestionUsuarios.btnSalir");
            label1.Text = bll_idioma.Traducir_382QA("FrmGestionUsuarios.label1");
            lblDni.Text = bll_idioma.Traducir_382QA("FrmGestionUsuarios.lblDni");
            lblNombre.Text = bll_idioma.Traducir_382QA("FrmGestionUsuarios.lblNombre");
            lblApellido.Text = bll_idioma.Traducir_382QA("FrmGestionUsuarios.lblApellido");
            lblCorreo.Text = bll_idioma.Traducir_382QA("FrmGestionUsuarios.lblCorreo");
            lblFiltro.Text = bll_idioma.Traducir_382QA("FrmGestionUsuarios.lblFiltro");
            lblNumeroUsuarios.Text = bll_idioma.Traducir_382QA("FrmGestionUsuarios.lblNumeroUsuarios");
            radioButton1.Text = bll_idioma.Traducir_382QA("FrmGestionUsuarios.radioButton1");
            radioButton2.Text = bll_idioma.Traducir_382QA("FrmGestionUsuarios.radioButton2");
            TraducirColumnas_382QA(grillaUsuarios);   

        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_382QA(this);
            base.OnFormClosed(e);
        }
        private void LimpiarTextBox_382QA()
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
