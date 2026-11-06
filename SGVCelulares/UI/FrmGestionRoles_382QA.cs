using Microsoft.VisualBasic;
using Servicio;
using BLL.BLL_SER;

namespace UI
{
    public partial class FrmGestionRoles_382QA : Form, IObservadorIdioma_382QA
    {
        BLL_Permiso_382QA bll_permiso;
        BLL_Familia_382QA bll_familia;
        BLL_Rol_382QA bll_rol;
        List<SER_Componente_382QA> componentes;
        BLL_Idioma_382QA bll_idioma = new BLL_Idioma_382QA();

        public FrmGestionRoles_382QA()
        {
            InitializeComponent();
            bll_idioma.Suscribir_382QA(this);
            ActualizarIdioma_382QA();
        }

        private void FrmGestionRoles_Load(object sender, EventArgs e)
        {
            clbComponentes.CheckOnClick = true;
            bll_permiso = new BLL_Permiso_382QA();
            bll_familia = new BLL_Familia_382QA();
            bll_rol = new BLL_Rol_382QA();

            foreach (var control in Controls)
            {
                if (control is DataGridView grilla)
                {
                    grilla.MultiSelect = false;
                    grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            CargarDatos_382QA();

            SER_Usuario_382QA usuario = SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario;
            btnBorrarRol.Enabled = usuario.Rol.TienePermiso_382QA("Eliminar Rol");
            btnBorrarFamilia.Enabled = usuario.Rol.TienePermiso_382QA("Eliminar Familia");
            AplicarPermisoCrear_382QA();

            radRol.CheckedChanged += (s, e) => AplicarPermisoCrear_382QA();
            radFamilia.CheckedChanged += (s, e) => AplicarPermisoCrear_382QA();
        }
        private void CargarDatos_382QA()
        {
            componentes = new List<SER_Componente_382QA>();
            componentes.AddRange(bll_permiso.Consultar_382QA());
            componentes.AddRange(bll_familia.Consultar_382QA());

            cbxRoles.Items.Clear();
            foreach (var rol in bll_rol.Consultar_382QA())
                cbxRoles.Items.Add(rol);

            cbxFamilias.Items.Clear();
            foreach (var c in componentes)
                if (c is SER_Familia_382QA f)
                    cbxFamilias.Items.Add(f);

            Mostrar_382QA(clbComponentes, componentes);
        }
        private void Mostrar_382QA(CheckedListBox listbox, object datos)
        {
            listbox.DataSource = null;
            listbox.DataSource = datos;
        }
        private void AplicarPermisoCrear_382QA()
        {
            SER_Usuario_382QA usuario = SER_SesionManager_382QA.ObtenerSesion_382QA().Usuario;

            if (radRol.Checked)
                btnCrear.Enabled = usuario.Rol.TienePermiso_382QA("Crear Rol");
            else
                btnCrear.Enabled = usuario.Rol.TienePermiso_382QA("Crear Familia");
        }

        private void clbComponentes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke(new Action(ActualizarTreeView_382QA));
        }

        private void ActualizarTreeView_382QA()
        {
            tvNodosComposite.BeginUpdate();
            tvNodosComposite.Nodes.Clear();
            string nombreRaiz = string.IsNullOrWhiteSpace(txtNombreFamilia.Text) ? "Nuevo rol" : txtNombreFamilia.Text;
            TreeNode raiz = new TreeNode(nombreRaiz);

            foreach (SER_Componente_382QA componente in clbComponentes.CheckedItems)
            {
                AgregarNodo_382QA(raiz, componente);
            }
            tvNodosComposite.Nodes.Add(raiz);
            raiz.ExpandAll();
            tvNodosComposite.EndUpdate();
        }
        private void txtNombreFamilia_TextChanged(object sender, EventArgs e)
        {
            ActualizarTreeView_382QA();
        }
        private void AgregarNodo_382QA(TreeNode nodoPadre, SER_Componente_382QA componente)
        {
            TreeNode nodo = new TreeNode(componente.Nombre);
            nodoPadre.Nodes.Add(nodo);
            if (componente is SER_Familia_382QA familia)
            {
                foreach (SER_Componente_382QA hijo in familia.Componentes)
                {
                    AgregarNodo_382QA(nodo, hijo);
                }
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                if (radRol.Checked)
                {
                    if (txtNombreFamilia.Text.Length == 0) throw new Exception("El nombre del rol no puede estar vacío!");
                    SER_Rol_382QA rol = new SER_Rol_382QA(txtNombreFamilia.Text);
                    List<SER_Componente_382QA> permisos = new List<SER_Componente_382QA>();
                    foreach (SER_Componente_382QA c in clbComponentes.CheckedItems)
                    {
                        permisos.Add(c);
                    }

                    bll_rol.Agregar_382QA(rol, permisos);
                    MessageBox.Show($"El rol {rol.Nombre} se creó con éxito!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos_382QA();
                    ActualizarTreeView_382QA();
                    txtNombreFamilia.Text = "";
                }
                else
                {
                    if (txtNombreFamilia.Text.Length == 0) throw new Exception("El nombre de la familia no puede estar vacía!");
                    SER_Familia_382QA familia = new SER_Familia_382QA(txtNombreFamilia.Text);
                    List<SER_Componente_382QA> permisos = new List<SER_Componente_382QA>();
                    foreach (SER_Componente_382QA c in clbComponentes.CheckedItems)
                    {
                        permisos.Add(c);
                    }

                    bll_familia.Agregar_382QA(familia, permisos);
                    MessageBox.Show($"La familia {familia.Nombre} se creó con éxito!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos_382QA();
                    ActualizarTreeView_382QA();
                    txtNombreFamilia.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarSeleccionados_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbComponentes.Items.Count; i++)
            {
                clbComponentes.SetItemChecked(i, false);
            }
            clbComponentes.ClearSelected();
        }
        public void ActualizarIdioma_382QA()
        {
            this.Text = bll_idioma.Traducir_382QA("FrmGestionRoles.Form");
            label1.Text = bll_idioma.Traducir_382QA("FrmGestionRoles.label1");
            label3.Text = bll_idioma.Traducir_382QA("FrmGestionRoles.label3");
            label4.Text = bll_idioma.Traducir_382QA("FrmGestionRoles.label4");
            label5.Text = bll_idioma.Traducir_382QA("FrmGestionRoles.label5");
            label2.Text = bll_idioma.Traducir_382QA("FrmGestionRoles.label2");
            btnCrear.Text = bll_idioma.Traducir_382QA("FrmGestionRoles.btnCrear");
            radRol.Text = bll_idioma.Traducir_382QA("FrmGestionRoles.radRol");
            radFamilia.Text = bll_idioma.Traducir_382QA("FrmGestionRoles.radFamilia");
            btnEliminarSeleccionados.Text = bll_idioma.Traducir_382QA("FrmGestionRoles.btnEliminarSeleccionados");
            btnSalir.Text = bll_idioma.Traducir_382QA("FrmGestionRoles.btnSalir");
            label6.Text = bll_idioma.Traducir_382QA("FrmGestionRoles.label6");
            btnBorrarRol.Text = bll_idioma.Traducir_382QA("FrmGestionRoles.btnBorrarRol");
            btnBorrarFamilia.Text = bll_idioma.Traducir_382QA("FrmGestionRoles.btnBorrarFamilia");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_382QA(this);
            base.OnFormClosed(e);
        }
        private void btnSalir_Click(object sender, EventArgs e) => this.Close();
        private void MostrarComponentesEnTreeView_382QA(string nombreRaiz, List<SER_Componente_382QA> componentes)
        {
            tvNodosComposite.BeginUpdate();
            tvNodosComposite.Nodes.Clear();

            TreeNode raiz = new TreeNode(nombreRaiz);
            foreach (SER_Componente_382QA componente in componentes)
                AgregarNodo_382QA(raiz, componente);

            tvNodosComposite.Nodes.Add(raiz);
            raiz.ExpandAll();
            tvNodosComposite.EndUpdate();
        }
        private void MostrarRolEnTreeView_382QA(SER_Rol_382QA rol) =>
            MostrarComponentesEnTreeView_382QA(rol.Nombre, rol.Componentes);
        private void cbxRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbxRoles.SelectedItem is SER_Rol_382QA rol)
                {
                    MostrarRolEnTreeView_382QA(rol);
                }
            }
            catch (Exception) { }
        }

        private void cbxFamilias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxFamilias.SelectedItem is SER_Familia_382QA familia)
                MostrarComponentesEnTreeView_382QA(familia.Nombre, familia.Componentes);
        }

        private void btnBorrarRol_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxRoles.SelectedItem is not SER_Rol_382QA rol)
                    throw new Exception("Seleccioná un rol para borrar.");

                if (MessageBox.Show($"¿Seguro que querés borrar el rol '{rol.Nombre}'?",
                        "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                bll_rol.Borrar_382QA(rol);
                MessageBox.Show($"El rol '{rol.Nombre}' se borró con éxito!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtNombreFamilia.Text = "";
                tvNodosComposite.Nodes.Clear();
                CargarDatos_382QA();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBorrarFamilia_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxFamilias.SelectedItem is not SER_Familia_382QA familia)
                    throw new Exception("Seleccioná una familia para borrar.");

                if (MessageBox.Show($"¿Seguro que querés borrar la familia '{familia.Nombre}'?",
                        "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                bll_familia.Borrar_382QA(familia);
                MessageBox.Show($"La familia '{familia.Nombre}' se borró con éxito!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtNombreFamilia.Text = "";
                tvNodosComposite.Nodes.Clear();
                CargarDatos_382QA();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
