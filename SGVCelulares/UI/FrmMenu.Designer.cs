namespace UI
{
    partial class FrmMenu
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
            pnlTopbar = new Panel();
            cboIdioma = new ComboBox();
            lblUsuario = new Label();
            lblTituloTopbar = new Label();
            pnlSidebar = new Panel();
            btnGestionRespaldo = new Button();
            btnGestionRoles = new Button();
            btnLogout = new Button();
            btnCambiarClave = new Button();
            btnRelogin = new Button();
            label1 = new Label();
            btnAyuda = new Button();
            btnReportes = new Button();
            btnBitacora = new Button();
            btnUsuarios = new Button();
            lblSeccionNav = new Label();
            pnlContenido = new Panel();
            pnlTopbar.SuspendLayout();
            pnlSidebar.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTopbar
            // 
            pnlTopbar.BackColor = Color.FromArgb(21, 32, 48);
            pnlTopbar.Controls.Add(cboIdioma);
            pnlTopbar.Controls.Add(lblUsuario);
            pnlTopbar.Controls.Add(lblTituloTopbar);
            pnlTopbar.Dock = DockStyle.Top;
            pnlTopbar.Location = new Point(0, 0);
            pnlTopbar.Name = "pnlTopbar";
            pnlTopbar.Size = new Size(960, 48);
            pnlTopbar.TabIndex = 0;
            // 
            // cboIdioma
            // 
            cboIdioma.DropDownStyle = ComboBoxStyle.DropDownList;
            cboIdioma.FormattingEnabled = true;
            cboIdioma.Location = new Point(735, 14);
            cboIdioma.Name = "cboIdioma";
            cboIdioma.Size = new Size(213, 25);
            cboIdioma.TabIndex = 2;
            cboIdioma.SelectedIndexChanged += cboIdioma_SelectedIndexChanged;
            // 
            // lblUsuario
            // 
            lblUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 9F);
            lblUsuario.ForeColor = Color.FromArgb(148, 163, 184);
            lblUsuario.Location = new Point(1600, 16);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(53, 15);
            lblUsuario.TabIndex = 1;
            lblUsuario.Text = "● Admin";
            // 
            // lblTituloTopbar
            // 
            lblTituloTopbar.AutoSize = true;
            lblTituloTopbar.Font = new Font("Segoe UI", 11F);
            lblTituloTopbar.ForeColor = Color.FromArgb(226, 232, 240);
            lblTituloTopbar.Location = new Point(16, 14);
            lblTituloTopbar.Name = "lblTituloTopbar";
            lblTituloTopbar.Size = new Size(136, 20);
            lblTituloTopbar.TabIndex = 0;
            lblTituloTopbar.Text = "Sistema de Gestión";
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(25, 37, 53);
            pnlSidebar.Controls.Add(btnGestionRespaldo);
            pnlSidebar.Controls.Add(btnGestionRoles);
            pnlSidebar.Controls.Add(btnLogout);
            pnlSidebar.Controls.Add(btnCambiarClave);
            pnlSidebar.Controls.Add(btnRelogin);
            pnlSidebar.Controls.Add(label1);
            pnlSidebar.Controls.Add(btnAyuda);
            pnlSidebar.Controls.Add(btnReportes);
            pnlSidebar.Controls.Add(btnBitacora);
            pnlSidebar.Controls.Add(btnUsuarios);
            pnlSidebar.Controls.Add(lblSeccionNav);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 48);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(211, 572);
            pnlSidebar.TabIndex = 1;
            // 
            // btnGestionRespaldo
            // 
            btnGestionRespaldo.BackColor = Color.FromArgb(25, 37, 53);
            btnGestionRespaldo.Cursor = Cursors.Hand;
            btnGestionRespaldo.FlatAppearance.BorderSize = 0;
            btnGestionRespaldo.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 60, 82);
            btnGestionRespaldo.FlatStyle = FlatStyle.Flat;
            btnGestionRespaldo.Font = new Font("Segoe UI", 9.5F);
            btnGestionRespaldo.ForeColor = Color.FromArgb(148, 163, 184);
            btnGestionRespaldo.Location = new Point(16, 160);
            btnGestionRespaldo.Name = "btnGestionRespaldo";
            btnGestionRespaldo.Padding = new Padding(10, 0, 0, 0);
            btnGestionRespaldo.Size = new Size(174, 38);
            btnGestionRespaldo.TabIndex = 13;
            btnGestionRespaldo.Text = "💾 Gestión respaldo";
            btnGestionRespaldo.TextAlign = ContentAlignment.MiddleLeft;
            btnGestionRespaldo.UseVisualStyleBackColor = false;
            btnGestionRespaldo.Click += btnGestionRespaldo_Click;
            // 
            // btnGestionRoles
            // 
            btnGestionRoles.BackColor = Color.FromArgb(25, 37, 53);
            btnGestionRoles.Cursor = Cursors.Hand;
            btnGestionRoles.FlatAppearance.BorderSize = 0;
            btnGestionRoles.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 60, 82);
            btnGestionRoles.FlatStyle = FlatStyle.Flat;
            btnGestionRoles.Font = new Font("Segoe UI", 9.5F);
            btnGestionRoles.ForeColor = Color.FromArgb(148, 163, 184);
            btnGestionRoles.Location = new Point(16, 116);
            btnGestionRoles.Name = "btnGestionRoles";
            btnGestionRoles.Padding = new Padding(10, 0, 0, 0);
            btnGestionRoles.Size = new Size(174, 38);
            btnGestionRoles.TabIndex = 12;
            btnGestionRoles.Tag = "Gestion roles";
            btnGestionRoles.Text = "👥 Gestión Roles";
            btnGestionRoles.TextAlign = ContentAlignment.MiddleLeft;
            btnGestionRoles.UseVisualStyleBackColor = false;
            btnGestionRoles.Click += btnGestionRoles_Click;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(25, 37, 53);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 60, 82);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 9.5F);
            btnLogout.ForeColor = Color.FromArgb(148, 163, 184);
            btnLogout.Location = new Point(16, 317);
            btnLogout.Name = "btnLogout";
            btnLogout.Padding = new Padding(10, 0, 0, 0);
            btnLogout.Size = new Size(174, 38);
            btnLogout.TabIndex = 11;
            btnLogout.Text = "🎚️ Cerrar sesión";
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnCambiarClave
            // 
            btnCambiarClave.BackColor = Color.FromArgb(25, 37, 53);
            btnCambiarClave.Cursor = Cursors.Hand;
            btnCambiarClave.FlatAppearance.BorderSize = 0;
            btnCambiarClave.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 60, 82);
            btnCambiarClave.FlatStyle = FlatStyle.Flat;
            btnCambiarClave.Font = new Font("Segoe UI", 9.5F);
            btnCambiarClave.ForeColor = Color.FromArgb(148, 163, 184);
            btnCambiarClave.Location = new Point(16, 273);
            btnCambiarClave.Name = "btnCambiarClave";
            btnCambiarClave.Padding = new Padding(10, 0, 0, 0);
            btnCambiarClave.Size = new Size(174, 38);
            btnCambiarClave.TabIndex = 10;
            btnCambiarClave.Text = "🔑 Cambiar clave";
            btnCambiarClave.TextAlign = ContentAlignment.MiddleLeft;
            btnCambiarClave.UseVisualStyleBackColor = false;
            btnCambiarClave.Click += btnCambiarClave_Click;
            // 
            // btnRelogin
            // 
            btnRelogin.BackColor = Color.FromArgb(25, 37, 53);
            btnRelogin.Cursor = Cursors.Hand;
            btnRelogin.FlatAppearance.BorderSize = 0;
            btnRelogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 60, 82);
            btnRelogin.FlatStyle = FlatStyle.Flat;
            btnRelogin.Font = new Font("Segoe UI", 9.5F);
            btnRelogin.ForeColor = Color.FromArgb(148, 163, 184);
            btnRelogin.Location = new Point(16, 229);
            btnRelogin.Name = "btnRelogin";
            btnRelogin.Padding = new Padding(10, 0, 0, 0);
            btnRelogin.Size = new Size(174, 38);
            btnRelogin.TabIndex = 9;
            btnRelogin.Text = "🔄 Re-Login";
            btnRelogin.TextAlign = ContentAlignment.MiddleLeft;
            btnRelogin.UseVisualStyleBackColor = false;
            btnRelogin.Click += btnRelogin_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(71, 85, 105);
            label1.Location = new Point(12, 214);
            label1.Name = "label1";
            label1.Size = new Size(50, 12);
            label1.TabIndex = 8;
            label1.Text = "USUARIO";
            // 
            // btnAyuda
            // 
            btnAyuda.BackColor = Color.FromArgb(25, 37, 53);
            btnAyuda.Cursor = Cursors.Hand;
            btnAyuda.FlatAppearance.BorderSize = 0;
            btnAyuda.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 60, 82);
            btnAyuda.FlatStyle = FlatStyle.Flat;
            btnAyuda.Font = new Font("Segoe UI", 9.5F);
            btnAyuda.ForeColor = Color.FromArgb(148, 163, 184);
            btnAyuda.Location = new Point(16, 456);
            btnAyuda.Name = "btnAyuda";
            btnAyuda.Padding = new Padding(10, 0, 0, 0);
            btnAyuda.Size = new Size(174, 38);
            btnAyuda.TabIndex = 6;
            btnAyuda.Text = "❓ Ayuda";
            btnAyuda.TextAlign = ContentAlignment.MiddleLeft;
            btnAyuda.UseVisualStyleBackColor = false;
            // 
            // btnReportes
            // 
            btnReportes.BackColor = Color.FromArgb(25, 37, 53);
            btnReportes.Cursor = Cursors.Hand;
            btnReportes.FlatAppearance.BorderSize = 0;
            btnReportes.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 60, 82);
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Segoe UI", 9.5F);
            btnReportes.ForeColor = Color.FromArgb(148, 163, 184);
            btnReportes.Location = new Point(16, 412);
            btnReportes.Name = "btnReportes";
            btnReportes.Padding = new Padding(10, 0, 0, 0);
            btnReportes.Size = new Size(174, 38);
            btnReportes.TabIndex = 3;
            btnReportes.Text = "📊 Reportes";
            btnReportes.TextAlign = ContentAlignment.MiddleLeft;
            btnReportes.UseVisualStyleBackColor = false;
            // 
            // btnBitacora
            // 
            btnBitacora.BackColor = Color.FromArgb(25, 37, 53);
            btnBitacora.Cursor = Cursors.Hand;
            btnBitacora.FlatAppearance.BorderSize = 0;
            btnBitacora.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 60, 82);
            btnBitacora.FlatStyle = FlatStyle.Flat;
            btnBitacora.Font = new Font("Segoe UI", 9.5F);
            btnBitacora.ForeColor = Color.FromArgb(148, 163, 184);
            btnBitacora.Location = new Point(16, 72);
            btnBitacora.Name = "btnBitacora";
            btnBitacora.Padding = new Padding(10, 0, 0, 0);
            btnBitacora.Size = new Size(174, 38);
            btnBitacora.TabIndex = 2;
            btnBitacora.Tag = "Gestion bitacora";
            btnBitacora.Text = "📜 Bitácora";
            btnBitacora.TextAlign = ContentAlignment.MiddleLeft;
            btnBitacora.UseVisualStyleBackColor = false;
            btnBitacora.Click += btnBitacora_Click;
            // 
            // btnUsuarios
            // 
            btnUsuarios.BackColor = Color.FromArgb(25, 37, 53);
            btnUsuarios.Cursor = Cursors.Hand;
            btnUsuarios.FlatAppearance.BorderSize = 0;
            btnUsuarios.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 60, 82);
            btnUsuarios.FlatStyle = FlatStyle.Flat;
            btnUsuarios.Font = new Font("Segoe UI", 9.5F);
            btnUsuarios.ForeColor = Color.FromArgb(148, 163, 184);
            btnUsuarios.Location = new Point(16, 28);
            btnUsuarios.Name = "btnUsuarios";
            btnUsuarios.Padding = new Padding(10, 0, 0, 0);
            btnUsuarios.Size = new Size(174, 38);
            btnUsuarios.TabIndex = 1;
            btnUsuarios.Tag = "Gestion usuario";
            btnUsuarios.Text = "\U0001f9d1‍💻 Gestión Usuarios";
            btnUsuarios.TextAlign = ContentAlignment.MiddleLeft;
            btnUsuarios.UseVisualStyleBackColor = false;
            btnUsuarios.Click += btnUsuarios_Click;
            // 
            // lblSeccionNav
            // 
            lblSeccionNav.AutoSize = true;
            lblSeccionNav.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblSeccionNav.ForeColor = Color.FromArgb(71, 85, 105);
            lblSeccionNav.Location = new Point(12, 8);
            lblSeccionNav.Name = "lblSeccionNav";
            lblSeccionNav.Size = new Size(40, 12);
            lblSeccionNav.TabIndex = 0;
            lblSeccionNav.Text = "ADMIN";
            // 
            // pnlContenido
            // 
            pnlContenido.BackColor = Color.FromArgb(30, 42, 58);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(211, 48);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Padding = new Padding(24);
            pnlContenido.Size = new Size(749, 572);
            pnlContenido.TabIndex = 2;
            // 
            // FrmMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(960, 620);
            Controls.Add(pnlContenido);
            Controls.Add(pnlSidebar);
            Controls.Add(pnlTopbar);
            Font = new Font("Segoe UI", 9.5F);
            MinimumSize = new Size(800, 500);
            Name = "FrmMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Gestión";
            FormClosing += FrmMenu_FormClosing;
            Load += FrmMenu_Load;
            pnlTopbar.ResumeLayout(false);
            pnlTopbar.PerformLayout();
            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTopbar;
        private System.Windows.Forms.Label lblTituloTopbar;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Label lblSeccionNav;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Button btnBitacora;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Button btnAyuda;
        private System.Windows.Forms.Panel pnlContenido;
        private Button btnLogout;
        private Button btnCambiarClave;
        private Button btnRelogin;
        private Label label1;
        private Button btnGestionRoles;
        private ComboBox cboIdioma;
        private Button btnGestionRespaldo;
    }
}