namespace UI
{
    partial class FrmGestionUsuarios
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
            lblNumeroUsuarios = new Label();
            lblTitulo = new Label();
            pnlContenido = new Panel();
            pnlBotones = new Panel();
            btnCrearUsuario = new Button();
            btnDesbloquearUsuario = new Button();
            btnModificarUsuario = new Button();
            btnCambiarEstadoUsuario = new Button();
            btnAplicarCambios = new Button();
            btnSalir = new Button();
            pnlFormulario = new Panel();
            cbxRoles = new ComboBox();
            lblRol = new Label();
            label1 = new Label();
            txtNombreUsuario = new TextBox();
            lblDni = new Label();
            txtDni = new TextBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblApellido = new Label();
            txtApellido = new TextBox();
            lblCorreo = new Label();
            txtCorreo = new TextBox();
            grillaUsuarios = new DataGridView();
            pnlFiltros = new Panel();
            lblFiltro = new Label();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            pnlTopbar.SuspendLayout();
            pnlContenido.SuspendLayout();
            pnlBotones.SuspendLayout();
            pnlFormulario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grillaUsuarios).BeginInit();
            pnlFiltros.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTopbar
            // 
            pnlTopbar.BackColor = Color.FromArgb(21, 32, 48);
            pnlTopbar.Controls.Add(lblNumero);
            pnlTopbar.Controls.Add(lblNumeroUsuarios);
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
            // lblNumeroUsuarios
            // 
            lblNumeroUsuarios.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblNumeroUsuarios.AutoSize = true;
            lblNumeroUsuarios.Font = new Font("Segoe UI", 9F);
            lblNumeroUsuarios.ForeColor = Color.FromArgb(148, 163, 184);
            lblNumeroUsuarios.Location = new Point(819, 18);
            lblNumeroUsuarios.Name = "lblNumeroUsuarios";
            lblNumeroUsuarios.Size = new Size(117, 15);
            lblNumeroUsuarios.TabIndex = 1;
            lblNumeroUsuarios.Text = "Número de usuarios:";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(226, 232, 240);
            lblTitulo.Location = new Point(16, 14);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(84, 20);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "USUARIOS";
            // 
            // pnlContenido
            // 
            pnlContenido.BackColor = Color.FromArgb(30, 42, 58);
            pnlContenido.Controls.Add(pnlBotones);
            pnlContenido.Controls.Add(pnlFormulario);
            pnlContenido.Controls.Add(grillaUsuarios);
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
            pnlBotones.Controls.Add(btnCrearUsuario);
            pnlBotones.Controls.Add(btnDesbloquearUsuario);
            pnlBotones.Controls.Add(btnModificarUsuario);
            pnlBotones.Controls.Add(btnCambiarEstadoUsuario);
            pnlBotones.Controls.Add(btnAplicarCambios);
            pnlBotones.Controls.Add(btnSalir);
            pnlBotones.Location = new Point(860, 72);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Padding = new Padding(8);
            pnlBotones.Size = new Size(172, 452);
            pnlBotones.TabIndex = 3;
            // 
            // btnCrearUsuario
            // 
            btnCrearUsuario.BackColor = Color.FromArgb(45, 60, 82);
            btnCrearUsuario.Cursor = Cursors.Hand;
            btnCrearUsuario.FlatAppearance.BorderSize = 0;
            btnCrearUsuario.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnCrearUsuario.FlatStyle = FlatStyle.Flat;
            btnCrearUsuario.Font = new Font("Segoe UI", 9.5F);
            btnCrearUsuario.ForeColor = Color.FromArgb(226, 232, 240);
            btnCrearUsuario.Location = new Point(8, 8);
            btnCrearUsuario.Name = "btnCrearUsuario";
            btnCrearUsuario.Size = new Size(156, 38);
            btnCrearUsuario.TabIndex = 0;
            btnCrearUsuario.Tag = "Crear Usuario";
            btnCrearUsuario.Text = "Crear";
            btnCrearUsuario.UseVisualStyleBackColor = false;
            btnCrearUsuario.Click += btnCrearUsuario_Click;
            // 
            // btnDesbloquearUsuario
            // 
            btnDesbloquearUsuario.BackColor = Color.FromArgb(45, 60, 82);
            btnDesbloquearUsuario.Cursor = Cursors.Hand;
            btnDesbloquearUsuario.FlatAppearance.BorderSize = 0;
            btnDesbloquearUsuario.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnDesbloquearUsuario.FlatStyle = FlatStyle.Flat;
            btnDesbloquearUsuario.Font = new Font("Segoe UI", 9.5F);
            btnDesbloquearUsuario.ForeColor = Color.FromArgb(226, 232, 240);
            btnDesbloquearUsuario.Location = new Point(8, 54);
            btnDesbloquearUsuario.Name = "btnDesbloquearUsuario";
            btnDesbloquearUsuario.Size = new Size(156, 38);
            btnDesbloquearUsuario.TabIndex = 1;
            btnDesbloquearUsuario.Tag = "Desbloquear Usuario";
            btnDesbloquearUsuario.Text = "Desbloquear";
            btnDesbloquearUsuario.UseVisualStyleBackColor = false;
            btnDesbloquearUsuario.Click += btnDesbloquearUsuario_Click;
            // 
            // btnModificarUsuario
            // 
            btnModificarUsuario.BackColor = Color.FromArgb(45, 60, 82);
            btnModificarUsuario.Cursor = Cursors.Hand;
            btnModificarUsuario.FlatAppearance.BorderSize = 0;
            btnModificarUsuario.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnModificarUsuario.FlatStyle = FlatStyle.Flat;
            btnModificarUsuario.Font = new Font("Segoe UI", 9.5F);
            btnModificarUsuario.ForeColor = Color.FromArgb(226, 232, 240);
            btnModificarUsuario.Location = new Point(8, 100);
            btnModificarUsuario.Name = "btnModificarUsuario";
            btnModificarUsuario.Size = new Size(156, 38);
            btnModificarUsuario.TabIndex = 2;
            btnModificarUsuario.Tag = "Modificar Usuario";
            btnModificarUsuario.Text = "Modificar";
            btnModificarUsuario.UseVisualStyleBackColor = false;
            btnModificarUsuario.Click += btnModificarUsuario_Click;
            // 
            // btnCambiarEstadoUsuario
            // 
            btnCambiarEstadoUsuario.BackColor = Color.FromArgb(45, 60, 82);
            btnCambiarEstadoUsuario.Cursor = Cursors.Hand;
            btnCambiarEstadoUsuario.FlatAppearance.BorderSize = 0;
            btnCambiarEstadoUsuario.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnCambiarEstadoUsuario.FlatStyle = FlatStyle.Flat;
            btnCambiarEstadoUsuario.Font = new Font("Segoe UI", 9.5F);
            btnCambiarEstadoUsuario.ForeColor = Color.FromArgb(226, 232, 240);
            btnCambiarEstadoUsuario.Location = new Point(8, 146);
            btnCambiarEstadoUsuario.Name = "btnCambiarEstadoUsuario";
            btnCambiarEstadoUsuario.Size = new Size(156, 38);
            btnCambiarEstadoUsuario.TabIndex = 3;
            btnCambiarEstadoUsuario.Tag = "Activar Usuario";
            btnCambiarEstadoUsuario.Text = "Activar / Desactivar";
            btnCambiarEstadoUsuario.UseVisualStyleBackColor = false;
            btnCambiarEstadoUsuario.Click += btnCambiarEstadoUsuario_Click;
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
            btnAplicarCambios.Location = new Point(5, 247);
            btnAplicarCambios.Name = "btnAplicarCambios";
            btnAplicarCambios.Size = new Size(156, 38);
            btnAplicarCambios.TabIndex = 4;
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
            btnSalir.TabIndex = 6;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // pnlFormulario
            // 
            pnlFormulario.BackColor = Color.FromArgb(25, 37, 53);
            pnlFormulario.Controls.Add(cbxRoles);
            pnlFormulario.Controls.Add(lblRol);
            pnlFormulario.Controls.Add(label1);
            pnlFormulario.Controls.Add(txtNombreUsuario);
            pnlFormulario.Controls.Add(lblDni);
            pnlFormulario.Controls.Add(txtDni);
            pnlFormulario.Controls.Add(lblNombre);
            pnlFormulario.Controls.Add(txtNombre);
            pnlFormulario.Controls.Add(lblApellido);
            pnlFormulario.Controls.Add(txtApellido);
            pnlFormulario.Controls.Add(lblCorreo);
            pnlFormulario.Controls.Add(txtCorreo);
            pnlFormulario.Location = new Point(24, 308);
            pnlFormulario.Name = "pnlFormulario";
            pnlFormulario.Padding = new Padding(16);
            pnlFormulario.Size = new Size(820, 216);
            pnlFormulario.TabIndex = 2;
            // 
            // cbxRoles
            // 
            cbxRoles.FormattingEnabled = true;
            cbxRoles.Location = new Point(561, 54);
            cbxRoles.Name = "cbxRoles";
            cbxRoles.Size = new Size(240, 25);
            cbxRoles.TabIndex = 11;
            // 
            // lblRol
            // 
            lblRol.AutoSize = true;
            lblRol.Font = new Font("Segoe UI", 9F);
            lblRol.ForeColor = Color.FromArgb(148, 163, 184);
            lblRol.Location = new Point(487, 60);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(24, 15);
            lblRol.TabIndex = 10;
            lblRol.Text = "Rol";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F);
            label1.ForeColor = Color.FromArgb(148, 163, 184);
            label1.Location = new Point(464, 16);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 8;
            label1.Text = "Usuario";
            // 
            // txtNombreUsuario
            // 
            txtNombreUsuario.BackColor = Color.FromArgb(21, 32, 48);
            txtNombreUsuario.BorderStyle = BorderStyle.FixedSingle;
            txtNombreUsuario.Font = new Font("Segoe UI", 9.5F);
            txtNombreUsuario.ForeColor = Color.FromArgb(226, 232, 240);
            txtNombreUsuario.Location = new Point(561, 11);
            txtNombreUsuario.Name = "txtNombreUsuario";
            txtNombreUsuario.Size = new Size(240, 24);
            txtNombreUsuario.TabIndex = 9;
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
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Font = new Font("Segoe UI", 9F);
            lblCorreo.ForeColor = Color.FromArgb(148, 163, 184);
            lblCorreo.Location = new Point(16, 132);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(43, 15);
            lblCorreo.TabIndex = 6;
            lblCorreo.Text = "Correo";
            // 
            // txtCorreo
            // 
            txtCorreo.BackColor = Color.FromArgb(21, 32, 48);
            txtCorreo.BorderStyle = BorderStyle.FixedSingle;
            txtCorreo.Font = new Font("Segoe UI", 9.5F);
            txtCorreo.ForeColor = Color.FromArgb(226, 232, 240);
            txtCorreo.Location = new Point(160, 129);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(240, 24);
            txtCorreo.TabIndex = 7;
            // 
            // grillaUsuarios
            // 
            grillaUsuarios.AllowUserToAddRows = false;
            grillaUsuarios.AllowUserToDeleteRows = false;
            grillaUsuarios.BackgroundColor = Color.FromArgb(25, 37, 53);
            grillaUsuarios.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(21, 32, 48);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(148, 163, 184);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            grillaUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            grillaUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(25, 37, 53);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(226, 232, 240);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(45, 60, 82);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(226, 232, 240);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            grillaUsuarios.DefaultCellStyle = dataGridViewCellStyle2;
            grillaUsuarios.EnableHeadersVisualStyles = false;
            grillaUsuarios.GridColor = Color.FromArgb(45, 60, 82);
            grillaUsuarios.Location = new Point(24, 72);
            grillaUsuarios.Name = "grillaUsuarios";
            grillaUsuarios.ReadOnly = true;
            grillaUsuarios.RowHeadersVisible = false;
            grillaUsuarios.Size = new Size(820, 220);
            grillaUsuarios.TabIndex = 1;
            // 
            // pnlFiltros
            // 
            pnlFiltros.BackColor = Color.FromArgb(25, 37, 53);
            pnlFiltros.Controls.Add(lblFiltro);
            pnlFiltros.Controls.Add(radioButton1);
            pnlFiltros.Controls.Add(radioButton2);
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
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Font = new Font("Segoe UI", 9F);
            radioButton1.ForeColor = Color.FromArgb(148, 163, 184);
            radioButton1.Location = new Point(80, 10);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(64, 19);
            radioButton1.TabIndex = 1;
            radioButton1.TabStop = true;
            radioButton1.Text = "Activos";
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Font = new Font("Segoe UI", 9F);
            radioButton2.ForeColor = Color.FromArgb(148, 163, 184);
            radioButton2.Location = new Point(160, 10);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(57, 19);
            radioButton2.TabIndex = 2;
            radioButton2.Text = "Todos";
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // FrmGestionUsuarios
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
            Name = "FrmGestionUsuarios";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Usuarios";
            Load += FrmUsuarios_Load;
            pnlTopbar.ResumeLayout(false);
            pnlTopbar.PerformLayout();
            pnlContenido.ResumeLayout(false);
            pnlBotones.ResumeLayout(false);
            pnlFormulario.ResumeLayout(false);
            pnlFormulario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)grillaUsuarios).EndInit();
            pnlFiltros.ResumeLayout(false);
            pnlFiltros.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTopbar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblNumeroUsuarios;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.Panel pnlFiltros;
        private System.Windows.Forms.Label lblFiltro;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.DataGridView grillaUsuarios;
        private System.Windows.Forms.Panel pnlFormulario;
        private System.Windows.Forms.Label lblDni;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Panel pnlBotones;
        private System.Windows.Forms.Button btnCrearUsuario;
        private System.Windows.Forms.Button btnDesbloquearUsuario;
        private System.Windows.Forms.Button btnModificarUsuario;
        private System.Windows.Forms.Button btnCambiarEstadoUsuario;
        private System.Windows.Forms.Button btnAplicarCambios;
        private System.Windows.Forms.Button btnSalir;
        private Label label1;
        private TextBox txtNombreUsuario;
        private Label lblNumero;
        private ComboBox cbxRoles;
        private Label lblRol;
    }
}