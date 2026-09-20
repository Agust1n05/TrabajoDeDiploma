using Microsoft.VisualBasic;
using Servicio;
using BLL;

namespace UI
{
    public partial class FrmGestionRoles : Form, IObservadorIdioma
    {
        BLL_Permiso bll_permiso;
        BLL_Familia bll_familia;
        BLL_Rol bll_rol;
        List<SER_Componente> componentes;
        BLL_Idioma bll_idioma = new BLL_Idioma();

        public FrmGestionRoles()
        {
            InitializeComponent();
            bll_idioma.Suscribir(this);
            ActualizarIdioma();
        }

        private void FrmGestionRoles_Load(object sender, EventArgs e)
        {
            clbComponentes.CheckOnClick = true;
            bll_permiso = new BLL_Permiso();
            bll_familia = new BLL_Familia();
            bll_rol = new BLL_Rol();

            foreach (var control in Controls)
            {
                if (control is DataGridView grilla)
                {
                    grilla.MultiSelect = false;
                    grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            CargarDatos();

            SER_Usuario usuario = SER_SesionManager.ObtenerSesion().Usuario;
            btnBorrarRol.Enabled = usuario.Rol.TienePermiso("Eliminar Rol");
            btnBorrarFamilia.Enabled = usuario.Rol.TienePermiso("Eliminar Familia");
            AplicarPermisoCrear();

            radRol.CheckedChanged += (s, e) => AplicarPermisoCrear();
            radFamilia.CheckedChanged += (s, e) => AplicarPermisoCrear();
        }
        private void CargarDatos()
        {
            componentes = new List<SER_Componente>();
            componentes.AddRange(bll_permiso.Consultar());
            componentes.AddRange(bll_familia.Consultar());

            cbxRoles.Items.Clear();
            foreach (var rol in bll_rol.Consultar())
                cbxRoles.Items.Add(rol);

            cbxFamilias.Items.Clear();
            foreach (var c in componentes)
                if (c is SER_Familia f)
                    cbxFamilias.Items.Add(f);

            Mostrar(clbComponentes, componentes);
        }
        private void Mostrar(CheckedListBox listbox, object datos)
        {
            listbox.DataSource = null;
            listbox.DataSource = datos;
        }
        private void AplicarPermisoCrear()
        {
            SER_Usuario usuario = SER_SesionManager.ObtenerSesion().Usuario;

            if (radRol.Checked)
                btnCrear.Enabled = usuario.Rol.TienePermiso("Crear Rol");
            else
                btnCrear.Enabled = usuario.Rol.TienePermiso("Crear Familia");
        }

        private void clbComponentes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke(new Action(ActualizarTreeView));
        }

        private void ActualizarTreeView()
        {
            tvNodosComposite.BeginUpdate();
            tvNodosComposite.Nodes.Clear();
            string nombreRaiz = string.IsNullOrWhiteSpace(txtNombreFamilia.Text) ? "Nuevo rol" : txtNombreFamilia.Text;
            TreeNode raiz = new TreeNode(nombreRaiz);

            foreach (SER_Componente componente in clbComponentes.CheckedItems)
            {
                AgregarNodo(raiz, componente);
            }
            tvNodosComposite.Nodes.Add(raiz);
            raiz.ExpandAll();
            tvNodosComposite.EndUpdate();
        }
        private void txtNombreFamilia_TextChanged(object sender, EventArgs e)
        {
            ActualizarTreeView();
        }
        private void AgregarNodo(TreeNode nodoPadre, SER_Componente componente)
        {
            TreeNode nodo = new TreeNode(componente.Nombre);
            nodoPadre.Nodes.Add(nodo);
            if (componente is SER_Familia familia)
            {
                foreach (SER_Componente hijo in familia.Componentes)
                {
                    AgregarNodo(nodo, hijo);
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
                    SER_Rol rol = new SER_Rol(txtNombreFamilia.Text);
                    List<SER_Componente> permisos = new List<SER_Componente>();
                    foreach (SER_Componente c in clbComponentes.CheckedItems)
                    {
                        permisos.Add(c);
                    }

                    bll_rol.Agregar(rol, permisos);
                    MessageBox.Show($"El rol {rol.Nombre} se creó con éxito!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                    ActualizarTreeView();
                    txtNombreFamilia.Text = "";
                }
                else
                {
                    if (txtNombreFamilia.Text.Length == 0) throw new Exception("El nombre de la familia no puede estar vacía!");
                    SER_Familia familia = new SER_Familia(txtNombreFamilia.Text);
                    List<SER_Componente> permisos = new List<SER_Componente>();
                    foreach (SER_Componente c in clbComponentes.CheckedItems)
                    {
                        permisos.Add(c);
                    }

                    bll_familia.Agregar(familia, permisos);
                    MessageBox.Show($"La familia {familia.Nombre} se creó con éxito!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                    ActualizarTreeView();
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
        public void ActualizarIdioma()
        {
            this.Text = bll_idioma.Traducir("FrmGestionRoles.Form");
            label1.Text = bll_idioma.Traducir("FrmGestionRoles.label1");
            label3.Text = bll_idioma.Traducir("FrmGestionRoles.label3");
            label4.Text = bll_idioma.Traducir("FrmGestionRoles.label4");
            label5.Text = bll_idioma.Traducir("FrmGestionRoles.label5");
            label2.Text = bll_idioma.Traducir("FrmGestionRoles.label2");
            btnCrear.Text = bll_idioma.Traducir("FrmGestionRoles.btnCrear");
            radRol.Text = bll_idioma.Traducir("FrmGestionRoles.radRol");
            radFamilia.Text = bll_idioma.Traducir("FrmGestionRoles.radFamilia");
            btnEliminarSeleccionados.Text = bll_idioma.Traducir("FrmGestionRoles.btnEliminarSeleccionados");
            btnSalir.Text = bll_idioma.Traducir("FrmGestionRoles.btnSalir");
            label6.Text = bll_idioma.Traducir("FrmGestionRoles.label6");
            btnBorrarRol.Text = bll_idioma.Traducir("FrmGestionRoles.btnBorrarRol");
            btnBorrarFamilia.Text = bll_idioma.Traducir("FrmGestionRoles.btnBorrarFamilia");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir(this);
            base.OnFormClosed(e);
        }
        private void btnSalir_Click(object sender, EventArgs e) => this.Close();
        private void MostrarComponentesEnTreeView(string nombreRaiz, List<SER_Componente> componentes)
        {
            tvNodosComposite.BeginUpdate();
            tvNodosComposite.Nodes.Clear();

            TreeNode raiz = new TreeNode(nombreRaiz);
            foreach (SER_Componente componente in componentes)
                AgregarNodo(raiz, componente);

            tvNodosComposite.Nodes.Add(raiz);
            raiz.ExpandAll();
            tvNodosComposite.EndUpdate();
        }
        private void MostrarRolEnTreeView(SER_Rol rol) =>
            MostrarComponentesEnTreeView(rol.Nombre, rol.Componentes);
        private void cbxRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbxRoles.SelectedItem is SER_Rol rol)
                {
                    MostrarRolEnTreeView(rol);
                }
            }
            catch (Exception) { }
        }

        private void cbxFamilias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxFamilias.SelectedItem is SER_Familia familia)
                MostrarComponentesEnTreeView(familia.Nombre, familia.Componentes);
        }

        private void btnBorrarRol_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxRoles.SelectedItem is not SER_Rol rol)
                    throw new Exception("Seleccioná un rol para borrar.");

                if (MessageBox.Show($"¿Seguro que querés borrar el rol '{rol.Nombre}'?",
                        "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                bll_rol.Borrar(rol);
                MessageBox.Show($"El rol '{rol.Nombre}' se borró con éxito!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtNombreFamilia.Text = "";
                tvNodosComposite.Nodes.Clear();
                CargarDatos();
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
                if (cbxFamilias.SelectedItem is not SER_Familia familia)
                    throw new Exception("Seleccioná una familia para borrar.");

                if (MessageBox.Show($"¿Seguro que querés borrar la familia '{familia.Nombre}'?",
                        "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                bll_familia.Borrar(familia);
                MessageBox.Show($"La familia '{familia.Nombre}' se borró con éxito!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtNombreFamilia.Text = "";
                tvNodosComposite.Nodes.Clear();
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
