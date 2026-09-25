namespace UI
{
    partial class FrmGestionClientes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            pnlTopbar = new Panel();
            lblNumero = new Label();
            lblNumeroClientes = new Label();
            lblTitulo = new Label();
            pnlContenido = new Panel();
            pnlBotones = new Panel();
            btnCrearCliente = new Button();
            btnModificarCliente = new Button();
            btnEliminarCliente = new Button();
            btnAplicarCambios = new Button();
            btnSalir = new Button();
            pnlFormulario = new Panel();
            lblDni = new Label();
            txtDni = new TextBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblApellido = new Label();
            txtApellido = new TextBox();
            lblTelefono = new Label();
            txtTelefono = new TextBox();
            grillaClientes = new DataGridView();
            pnlFiltros = new Panel();
            lblFiltro = new Label();
            lblBuscarDni = new Label();
            txtBuscarDni = new TextBox();
            pnlTopbar.SuspendLayout();
            pnlContenido.SuspendLayout();
            pnlBotones.SuspendLayout();
            pnlFormulario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grillaClientes).BeginInit();
            pnlFiltros.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTopbar
            // 
            pnlTopbar.BackColor = Color.FromArgb(21, 32, 48);
            pnlTopbar.Controls.Add(lblNumero);
            pnlTopbar.Controls.Add(lblNumeroClientes);
            pnlTopbar.Controls.Add(lblTitulo);
            pnlTopbar.Dock = DockStyle.Top;
            pnlTopbar.Location = new Point(0, 0);
            pnlTopbar.Name = "pnlTopbar";
            pnlTopbar.Size = new Size(1060, 48);
            pnlTopbar.TabIndex = 0;
            // 
            // lblNumero
            // 
            lblNumero.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblNumero.AutoSize = true;
            lblNumero.Font = new Font("Segoe UI", 9F);
            lblNumero.ForeColor = Color.FromArgb(148, 163, 184);
            lblNumero.Location = new Point(933, 18);
            lblNumero.Name = "lblNumero";
            lblNumero.Size = new Size(14, 15);
            lblNumero.TabIndex = 2;
            lblNumero.Text = "X";
            // 
            // lblNumeroClientes
            // 
            lblNumeroClientes.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblNumeroClientes.AutoSize = true;
            lblNumeroClientes.Font = new Font("Segoe UI", 9F);
            lblNumeroClientes.ForeColor = Color.FromArgb(148, 163, 184);
            lblNumeroClientes.Location = new Point(816, 18);
            lblNumeroClientes.Name = "lblNumeroClientes";
            lblNumeroClientes.Size = new Size(120, 15);
            lblNumeroClientes.TabIndex = 1;
            lblNumeroClientes.Text = "Número de clientes:";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(226, 232, 240);
            lblTitulo.Location = new Point(16, 14);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(72, 20);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "CLIENTES";
            // 
            // pnlContenido
            // 
            pnlContenido.BackColor = Color.FromArgb(30, 42, 58);
            pnlContenido.Controls.Add(pnlBotones);
            pnlContenido.Controls.Add(pnlFormulario);
            pnlContenido.Controls.Add(grillaClientes);
            pnlContenido.Controls.Add(pnlFiltros);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(0, 48);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Padding = new Padding(24);
            pnlContenido.Size = new Size(1060, 552);
            pnlContenido.TabIndex = 1;
            // 
            // pnlBotones
            // 
            pnlBotones.BackColor = Color.FromArgb(25, 37, 53);
            pnlBotones.Controls.Add(btnCrearCliente);
            pnlBotones.Controls.Add(btnModificarCliente);
            pnlBotones.Controls.Add(btnEliminarCliente);
            pnlBotones.Controls.Add(btnAplicarCambios);
            pnlBotones.Controls.Add(btnSalir);
            pnlBotones.Location = new Point(860, 72);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Padding = new Padding(8);
            pnlBotones.Size = new Size(172, 452);
            pnlBotones.TabIndex = 3;
            // 
            // btnCrearCliente
            // 
            btnCrearCliente.BackColor = Color.FromArgb(45, 60, 82);
            btnCrearCliente.Cursor = Cursors.Hand;
            btnCrearCliente.FlatAppearance.BorderSize = 0;
            btnCrearCliente.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnCrearCliente.FlatStyle = FlatStyle.Flat;
            btnCrearCliente.Font = new Font("Segoe UI", 9.5F);
            btnCrearCliente.ForeColor = Color.FromArgb(226, 232, 240);
            btnCrearCliente.Location = new Point(8, 8);
            btnCrearCliente.Name = "btnCrearCliente";
            btnCrearCliente.Size = new Size(156, 38);
            btnCrearCliente.TabIndex = 0;
            btnCrearCliente.Tag = "Crear Cliente";
            btnCrearCliente.Text = "Crear";
            btnCrearCliente.UseVisualStyleBackColor = false;
            btnCrearCliente.Click += btnCrearCliente_Click;
            // 
            // btnModificarCliente
            // 
            btnModificarCliente.BackColor = Color.FromArgb(45, 60, 82);
            btnModificarCliente.Cursor = Cursors.Hand;
            btnModificarCliente.FlatAppearance.BorderSize = 0;
            btnModificarCliente.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnModificarCliente.FlatStyle = FlatStyle.Flat;
            btnModificarCliente.Font = new Font("Segoe UI", 9.5F);
            btnModificarCliente.ForeColor = Color.FromArgb(226, 232, 240);
            btnModificarCliente.Location = new Point(8, 54);
            btnModificarCliente.Name = "btnModificarCliente";
            btnModificarCliente.Size = new Size(156, 38);
            btnModificarCliente.TabIndex = 1;
            btnModificarCliente.Tag = "Modificar Cliente";
            btnModificarCliente.Text = "Modificar";
            btnModificarCliente.UseVisualStyleBackColor = false;
            btnModificarCliente.Click += btnModificarCliente_Click;
            // 
            // btnEliminarCliente
            // 
            btnEliminarCliente.BackColor = Color.FromArgb(45, 60, 82);
            btnEliminarCliente.Cursor = Cursors.Hand;
            btnEliminarCliente.FlatAppearance.BorderSize = 0;
            btnEliminarCliente.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnEliminarCliente.FlatStyle = FlatStyle.Flat;
            btnEliminarCliente.Font = new Font("Segoe UI", 9.5F);
            btnEliminarCliente.ForeColor = Color.FromArgb(226, 232, 240);
            btnEliminarCliente.Location = new Point(8, 100);
            btnEliminarCliente.Name = "btnEliminarCliente";
            btnEliminarCliente.Size = new Size(156, 38);
            btnEliminarCliente.TabIndex = 2;
            btnEliminarCliente.Tag = "Eliminar Cliente";
            btnEliminarCliente.Text = "Eliminar";
            btnEliminarCliente.UseVisualStyleBackColor = false;
            btnEliminarCliente.Click += btnEliminarCliente_Click;
            // 
            // btnAplicarCambios
            // 
            btnAplicarCambios.BackColor = Color.FromArgb(45, 60, 82);
            btnAplicarCambios.Cursor = Cursors.Hand;
            btnAplicarCambios.FlatAppearance.BorderSize = 0;
            btnAplicarCambios.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnAplicarCambios.FlatStyle = FlatStyle.Flat;
            btnAplicarCambios.Font = new Font("Segoe UI", 9.5F);
            btnAplicarCambios.ForeColor = Color.FromArgb(226, 232, 240);
            btnAplicarCambios.Location = new Point(8, 146);
            btnAplicarCambios.Name = "btnAplicarCambios";
            btnAplicarCambios.Size = new Size(156, 38);
            btnAplicarCambios.TabIndex = 3;
            btnAplicarCambios.Text = "Aplicar";
            btnAplicarCambios.UseVisualStyleBackColor = false;
            btnAplicarCambios.Click += btnAplicarCambios_Click;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(45, 60, 82);
            btnSalir.Cursor = Cursors.Hand;
            btnSalir.FlatAppearance.BorderSize = 0;
            btnSalir.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Font = new Font("Segoe UI", 9.5F);
            btnSalir.ForeColor = Color.FromArgb(226, 232, 240);
            btnSalir.Location = new Point(8, 406);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(156, 38);
            btnSalir.TabIndex = 4;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // pnlFormulario
            // 
            pnlFormulario.BackColor = Color.FromArgb(25, 37, 53);
            pnlFormulario.Controls.Add(lblDni);
            pnlFormulario.Controls.Add(txtDni);
            pnlFormulario.Controls.Add(lblNombre);
            pnlFormulario.Controls.Add(txtNombre);
            pnlFormulario.Controls.Add(lblApellido);
            pnlFormulario.Controls.Add(txtApellido);
            pnlFormulario.Controls.Add(lblTelefono);
            pnlFormulario.Controls.Add(txtTelefono);
            pnlFormulario.Location = new Point(24, 308);
            pnlFormulario.Name = "pnlFormulario";
            pnlFormulario.Padding = new Padding(16);
            pnlFormulario.Size = new Size(820, 216);
            pnlFormulario.TabIndex = 2;
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Font = new Font("Segoe UI", 9F);
            lblDni.ForeColor = Color.FromArgb(148, 163, 184);
            lblDni.Location = new Point(16, 16);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(27, 15);
            lblDni.TabIndex = 0;
            lblDni.Text = "DNI";
            // 
            // txtDni
            // 
            txtDni.BackColor = Color.FromArgb(21, 32, 48);
            txtDni.BorderStyle = BorderStyle.FixedSingle;
            txtDni.Font = new Font("Segoe UI", 9.5F);
            txtDni.ForeColor = Color.FromArgb(226, 232, 240);
            txtDni.Location = new Point(160, 13);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(240, 24);
            txtDni.TabIndex = 1;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9F);
            lblNombre.ForeColor = Color.FromArgb(148, 163, 184);
            lblNombre.Location = new Point(16, 54);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(51, 15);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.FromArgb(21, 32, 48);
            txtNombre.BorderStyle = BorderStyle.FixedSingle;
            txtNombre.Font = new Font("Segoe UI", 9.5F);
            txtNombre.ForeColor = Color.FromArgb(226, 232, 240);
            txtNombre.Location = new Point(160, 51);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(240, 24);
            txtNombre.TabIndex = 3;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Font = new Font("Segoe UI", 9F);
            lblApellido.ForeColor = Color.FromArgb(148, 163, 184);
            lblApellido.Location = new Point(16, 93);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(51, 15);
            lblApellido.TabIndex = 4;
            lblApellido.Text = "Apellido";
            // 
            // txtApellido
            // 
            txtApellido.BackColor = Color.FromArgb(21, 32, 48);
            txtApellido.BorderStyle = BorderStyle.FixedSingle;
            txtApellido.Font = new Font("Segoe UI", 9.5F);
            txtApellido.ForeColor = Color.FromArgb(226, 232, 240);
            txtApellido.Location = new Point(160, 90);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(240, 24);
            txtApellido.TabIndex = 5;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Font = new Font("Segoe UI", 9F);
            lblTelefono.ForeColor = Color.FromArgb(148, 163, 184);
            lblTelefono.Location = new Point(16, 132);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(54, 15);
            lblTelefono.TabIndex = 6;
            lblTelefono.Text = "Teléfono";
            // 
            // txtTelefono
            // 
            txtTelefono.BackColor = Color.FromArgb(21, 32, 48);
            txtTelefono.BorderStyle = BorderStyle.FixedSingle;
            txtTelefono.Font = new Font("Segoe UI", 9.5F);
            txtTelefono.ForeColor = Color.FromArgb(226, 232, 240);
            txtTelefono.Location = new Point(160, 129);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(240, 24);
            txtTelefono.TabIndex = 7;
            // 
            // grillaClientes
            // 
            grillaClientes.AllowUserToAddRows = false;
            grillaClientes.AllowUserToDeleteRows = false;
            grillaClientes.BackgroundColor = Color.FromArgb(25, 37, 53);
            grillaClientes.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(21, 32, 48);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(148, 163, 184);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            grillaClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            grillaClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(25, 37, 53);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(226, 232, 240);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(45, 60, 82);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(226, 232, 240);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            grillaClientes.DefaultCellStyle = dataGridViewCellStyle2;
            grillaClientes.EnableHeadersVisualStyles = false;
            grillaClientes.GridColor = Color.FromArgb(45, 60, 82);
            grillaClientes.Location = new Point(24, 72);
            grillaClientes.Name = "grillaClientes";
            grillaClientes.ReadOnly = true;
            grillaClientes.RowHeadersVisible = false;
            grillaClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grillaClientes.MultiSelect = false;
            grillaClientes.Size = new Size(820, 220);
            grillaClientes.TabIndex = 1;
            grillaClientes.SelectionChanged += grillaClientes_SelectionChanged;
            // 
            // pnlFiltros
            // 
            pnlFiltros.BackColor = Color.FromArgb(25, 37, 53);
            pnlFiltros.Controls.Add(lblFiltro);
            pnlFiltros.Controls.Add(lblBuscarDni);
            pnlFiltros.Controls.Add(txtBuscarDni);
            pnlFiltros.Location = new Point(24, 24);
            pnlFiltros.Name = "pnlFiltros";
            pnlFiltros.Size = new Size(820, 36);
            pnlFiltros.TabIndex = 0;
            // 
            // lblFiltro
            // 
            lblFiltro.AutoSize = true;
            lblFiltro.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblFiltro.ForeColor = Color.FromArgb(71, 85, 105);
            lblFiltro.Location = new Point(10, 11);
            lblFiltro.Name = "lblFiltro";
            lblFiltro.Size = new Size(44, 12);
            lblFiltro.TabIndex = 0;
            lblFiltro.Text = "FILTRAR";
            // 
            // lblBuscarDni
            // 
            lblBuscarDni.AutoSize = true;
            lblBuscarDni.Font = new Font("Segoe UI", 9F);
            lblBuscarDni.ForeColor = Color.FromArgb(148, 163, 184);
            lblBuscarDni.Location = new Point(80, 9);
            lblBuscarDni.Name = "lblBuscarDni";
            lblBuscarDni.Size = new Size(60, 15);
            lblBuscarDni.TabIndex = 1;
            lblBuscarDni.Text = "Buscar DNI";
            // 
            // txtBuscarDni
            // 
            txtBuscarDni.BackColor = Color.FromArgb(21, 32, 48);
            txtBuscarDni.BorderStyle = BorderStyle.FixedSingle;
            txtBuscarDni.Font = new Font("Segoe UI", 9.5F);
            txtBuscarDni.ForeColor = Color.FromArgb(226, 232, 240);
            txtBuscarDni.Location = new Point(146, 6);
            txtBuscarDni.Name = "txtBuscarDni";
            txtBuscarDni.Size = new Size(200, 24);
            txtBuscarDni.TabIndex = 2;
            txtBuscarDni.TextChanged += txtBuscarDni_TextChanged;
            // 
            // FrmGestionClientes
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(1060, 600);
            Controls.Add(pnlContenido);
            Controls.Add(pnlTopbar);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmGestionClientes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Clientes";
            Load += FrmGestionClientes_Load;
            pnlTopbar.ResumeLayout(false);
            pnlTopbar.PerformLayout();
            pnlContenido.ResumeLayout(false);
            pnlBotones.ResumeLayout(false);
            pnlFormulario.ResumeLayout(false);
            pnlFormulario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)grillaClientes).EndInit();
            pnlFiltros.ResumeLayout(false);
            pnlFiltros.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTopbar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblNumeroClientes;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.Panel pnlFiltros;
        private System.Windows.Forms.Label lblFiltro;
        private System.Windows.Forms.Label lblBuscarDni;
        private System.Windows.Forms.TextBox txtBuscarDni;
        private System.Windows.Forms.DataGridView grillaClientes;
        private System.Windows.Forms.Panel pnlFormulario;
        private System.Windows.Forms.Label lblDni;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Panel pnlBotones;
        private System.Windows.Forms.Button btnCrearCliente;
        private System.Windows.Forms.Button btnModificarCliente;
        private System.Windows.Forms.Button btnEliminarCliente;
        private System.Windows.Forms.Button btnAplicarCambios;
        private System.Windows.Forms.Button btnSalir;
    }
}