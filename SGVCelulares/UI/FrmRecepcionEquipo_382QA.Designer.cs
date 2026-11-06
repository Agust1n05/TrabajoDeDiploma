namespace UI
{
    partial class FrmRecepcionEquipo_382QA
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
            lblTitulo = new Label();
            pnlCliente = new Panel();
            lblPasoCliente = new Label();
            lblDni = new Label();
            txtDni = new TextBox();
            btnBuscarCliente = new Button();
            lblClienteNombre = new Label();
            lblClienteEstado = new Label();
            btnRegistrarCliente = new Button();
            pnlEquipo = new Panel();
            lblPasoEquipo = new Label();
            lblImei = new Label();
            txtImei = new TextBox();
            btnBuscarEquipo = new Button();
            lblMarca = new Label();
            txtMarca = new TextBox();
            lblModelo = new Label();
            txtModelo = new TextBox();
            lblEquipoEstado = new Label();
            pnlFalla = new Panel();
            lblPasoFalla = new Label();
            txtFalla = new TextBox();
            lblContador = new Label();
            btnRecepcionar = new Button();
            btnCancelar = new Button();
            pnlTopbar.SuspendLayout();
            pnlCliente.SuspendLayout();
            pnlEquipo.SuspendLayout();
            pnlFalla.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTopbar
            // 
            pnlTopbar.BackColor = Color.FromArgb(21, 32, 48);
            pnlTopbar.Controls.Add(lblTitulo);
            pnlTopbar.Dock = DockStyle.Top;
            pnlTopbar.Name = "pnlTopbar";
            pnlTopbar.Size = new Size(640, 48);
            pnlTopbar.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(226, 232, 240);
            lblTitulo.Location = new Point(16, 14);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "RECEPCIÓN DE EQUIPO";
            // 
            // pnlCliente
            // 
            pnlCliente.BackColor = Color.FromArgb(25, 37, 53);
            pnlCliente.Controls.Add(lblPasoCliente);
            pnlCliente.Controls.Add(lblDni);
            pnlCliente.Controls.Add(txtDni);
            pnlCliente.Controls.Add(btnBuscarCliente);
            pnlCliente.Controls.Add(lblClienteNombre);
            pnlCliente.Controls.Add(lblClienteEstado);
            pnlCliente.Controls.Add(btnRegistrarCliente);
            pnlCliente.Location = new Point(24, 64);
            pnlCliente.Name = "pnlCliente";
            pnlCliente.Padding = new Padding(16);
            pnlCliente.Size = new Size(592, 132);
            pnlCliente.TabIndex = 1;
            // 
            // lblPasoCliente
            // 
            lblPasoCliente.AutoSize = true;
            lblPasoCliente.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblPasoCliente.ForeColor = Color.FromArgb(71, 85, 105);
            lblPasoCliente.Location = new Point(16, 12);
            lblPasoCliente.Name = "lblPasoCliente";
            lblPasoCliente.TabIndex = 0;
            lblPasoCliente.Text = "1  ·  CLIENTE";
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Font = new Font("Segoe UI", 9F);
            lblDni.ForeColor = Color.FromArgb(148, 163, 184);
            lblDni.Location = new Point(16, 42);
            lblDni.Name = "lblDni";
            lblDni.TabIndex = 1;
            lblDni.Text = "DNI";
            // 
            // txtDni
            // 
            txtDni.BackColor = Color.FromArgb(21, 32, 48);
            txtDni.BorderStyle = BorderStyle.FixedSingle;
            txtDni.Font = new Font("Segoe UI", 9.5F);
            txtDni.ForeColor = Color.FromArgb(226, 232, 240);
            txtDni.MaxLength = 8;
            txtDni.Location = new Point(130, 39);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(180, 24);
            txtDni.TabIndex = 2;
            txtDni.TextChanged += txtDni_TextChanged;
            txtDni.KeyDown += txtDni_KeyDown;
            // 
            // btnBuscarCliente
            // 
            btnBuscarCliente.BackColor = Color.FromArgb(45, 60, 82);
            btnBuscarCliente.Cursor = Cursors.Hand;
            btnBuscarCliente.FlatAppearance.BorderSize = 0;
            btnBuscarCliente.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnBuscarCliente.FlatStyle = FlatStyle.Flat;
            btnBuscarCliente.Font = new Font("Segoe UI", 9.5F);
            btnBuscarCliente.ForeColor = Color.FromArgb(226, 232, 240);
            btnBuscarCliente.UseVisualStyleBackColor = false;
            btnBuscarCliente.Location = new Point(320, 37);
            btnBuscarCliente.Name = "btnBuscarCliente";
            btnBuscarCliente.Size = new Size(110, 28);
            btnBuscarCliente.TabIndex = 3;
            btnBuscarCliente.Text = "Buscar";
            btnBuscarCliente.Click += btnBuscarCliente_Click;
            // 
            // lblClienteNombre
            // 
            lblClienteNombre.AutoSize = false;
            lblClienteNombre.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblClienteNombre.ForeColor = Color.FromArgb(226, 232, 240);
            lblClienteNombre.Location = new Point(130, 76);
            lblClienteNombre.Name = "lblClienteNombre";
            lblClienteNombre.Size = new Size(440, 22);
            lblClienteNombre.TabIndex = 4;
            lblClienteNombre.Text = "";
            // 
            // lblClienteEstado
            // 
            lblClienteEstado.AutoSize = false;
            lblClienteEstado.Font = new Font("Segoe UI", 9F);
            lblClienteEstado.ForeColor = Color.FromArgb(148, 163, 184);
            lblClienteEstado.Location = new Point(130, 100);
            lblClienteEstado.Name = "lblClienteEstado";
            lblClienteEstado.Size = new Size(300, 20);
            lblClienteEstado.TabIndex = 5;
            lblClienteEstado.Text = "Ingresá el DNI y presioná Buscar.";
            // 
            // btnRegistrarCliente
            // 
            btnRegistrarCliente.BackColor = Color.FromArgb(14, 116, 144);
            btnRegistrarCliente.Cursor = Cursors.Hand;
            btnRegistrarCliente.FlatAppearance.BorderSize = 0;
            btnRegistrarCliente.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 94, 117);
            btnRegistrarCliente.FlatStyle = FlatStyle.Flat;
            btnRegistrarCliente.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRegistrarCliente.ForeColor = Color.FromArgb(226, 232, 240);
            btnRegistrarCliente.UseVisualStyleBackColor = false;
            btnRegistrarCliente.Location = new Point(420, 92);
            btnRegistrarCliente.Name = "btnRegistrarCliente";
            btnRegistrarCliente.Size = new Size(150, 30);
            btnRegistrarCliente.TabIndex = 6;
            btnRegistrarCliente.Text = "Registrar cliente";
            btnRegistrarCliente.Click += btnRegistrarCliente_Click;
            // 
            // pnlEquipo
            // 
            pnlEquipo.BackColor = Color.FromArgb(25, 37, 53);
            pnlEquipo.Controls.Add(lblPasoEquipo);
            pnlEquipo.Controls.Add(lblImei);
            pnlEquipo.Controls.Add(txtImei);
            pnlEquipo.Controls.Add(btnBuscarEquipo);
            pnlEquipo.Controls.Add(lblMarca);
            pnlEquipo.Controls.Add(txtMarca);
            pnlEquipo.Controls.Add(lblModelo);
            pnlEquipo.Controls.Add(txtModelo);
            pnlEquipo.Controls.Add(lblEquipoEstado);
            pnlEquipo.Location = new Point(24, 208);
            pnlEquipo.Name = "pnlEquipo";
            pnlEquipo.Padding = new Padding(16);
            pnlEquipo.Size = new Size(592, 170);
            pnlEquipo.TabIndex = 2;
            // 
            // lblPasoEquipo
            // 
            lblPasoEquipo.AutoSize = true;
            lblPasoEquipo.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblPasoEquipo.ForeColor = Color.FromArgb(71, 85, 105);
            lblPasoEquipo.Location = new Point(16, 12);
            lblPasoEquipo.Name = "lblPasoEquipo";
            lblPasoEquipo.TabIndex = 0;
            lblPasoEquipo.Text = "2  ·  EQUIPO";
            // 
            // lblImei
            // 
            lblImei.AutoSize = true;
            lblImei.Font = new Font("Segoe UI", 9F);
            lblImei.ForeColor = Color.FromArgb(148, 163, 184);
            lblImei.Location = new Point(16, 42);
            lblImei.Name = "lblImei";
            lblImei.TabIndex = 1;
            lblImei.Text = "IMEI";
            // 
            // txtImei
            // 
            txtImei.BackColor = Color.FromArgb(21, 32, 48);
            txtImei.BorderStyle = BorderStyle.FixedSingle;
            txtImei.Font = new Font("Segoe UI", 9.5F);
            txtImei.ForeColor = Color.FromArgb(226, 232, 240);
            txtImei.MaxLength = 15;
            txtImei.Location = new Point(130, 39);
            txtImei.Name = "txtImei";
            txtImei.Size = new Size(180, 24);
            txtImei.TabIndex = 2;
            txtImei.TextChanged += txtImei_TextChanged;
            txtImei.KeyDown += txtImei_KeyDown;
            // 
            // btnBuscarEquipo
            // 
            btnBuscarEquipo.BackColor = Color.FromArgb(45, 60, 82);
            btnBuscarEquipo.Cursor = Cursors.Hand;
            btnBuscarEquipo.FlatAppearance.BorderSize = 0;
            btnBuscarEquipo.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnBuscarEquipo.FlatStyle = FlatStyle.Flat;
            btnBuscarEquipo.Font = new Font("Segoe UI", 9.5F);
            btnBuscarEquipo.ForeColor = Color.FromArgb(226, 232, 240);
            btnBuscarEquipo.UseVisualStyleBackColor = false;
            btnBuscarEquipo.Location = new Point(320, 37);
            btnBuscarEquipo.Name = "btnBuscarEquipo";
            btnBuscarEquipo.Size = new Size(110, 28);
            btnBuscarEquipo.TabIndex = 3;
            btnBuscarEquipo.Text = "Buscar";
            btnBuscarEquipo.Click += btnBuscarEquipo_Click;
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.Font = new Font("Segoe UI", 9F);
            lblMarca.ForeColor = Color.FromArgb(148, 163, 184);
            lblMarca.Location = new Point(16, 80);
            lblMarca.Name = "lblMarca";
            lblMarca.TabIndex = 4;
            lblMarca.Text = "Marca";
            // 
            // txtMarca
            // 
            txtMarca.BackColor = Color.FromArgb(21, 32, 48);
            txtMarca.BorderStyle = BorderStyle.FixedSingle;
            txtMarca.Font = new Font("Segoe UI", 9.5F);
            txtMarca.ForeColor = Color.FromArgb(226, 232, 240);
            txtMarca.MaxLength = 50;
            txtMarca.ReadOnly = true;
            txtMarca.Location = new Point(130, 77);
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(300, 24);
            txtMarca.TabIndex = 5;
            // 
            // lblModelo
            // 
            lblModelo.AutoSize = true;
            lblModelo.Font = new Font("Segoe UI", 9F);
            lblModelo.ForeColor = Color.FromArgb(148, 163, 184);
            lblModelo.Location = new Point(16, 114);
            lblModelo.Name = "lblModelo";
            lblModelo.TabIndex = 6;
            lblModelo.Text = "Modelo";
            // 
            // txtModelo
            // 
            txtModelo.BackColor = Color.FromArgb(21, 32, 48);
            txtModelo.BorderStyle = BorderStyle.FixedSingle;
            txtModelo.Font = new Font("Segoe UI", 9.5F);
            txtModelo.ForeColor = Color.FromArgb(226, 232, 240);
            txtModelo.MaxLength = 50;
            txtModelo.ReadOnly = true;
            txtModelo.Location = new Point(130, 111);
            txtModelo.Name = "txtModelo";
            txtModelo.Size = new Size(300, 24);
            txtModelo.TabIndex = 7;
            // 
            // lblEquipoEstado
            // 
            lblEquipoEstado.AutoSize = false;
            lblEquipoEstado.Font = new Font("Segoe UI", 9F);
            lblEquipoEstado.ForeColor = Color.FromArgb(148, 163, 184);
            lblEquipoEstado.Location = new Point(130, 142);
            lblEquipoEstado.Name = "lblEquipoEstado";
            lblEquipoEstado.Size = new Size(440, 20);
            lblEquipoEstado.TabIndex = 8;
            lblEquipoEstado.Text = "Ingresá el IMEI y presioná Buscar.";
            // 
            // pnlFalla
            // 
            pnlFalla.BackColor = Color.FromArgb(25, 37, 53);
            pnlFalla.Controls.Add(lblPasoFalla);
            pnlFalla.Controls.Add(txtFalla);
            pnlFalla.Controls.Add(lblContador);
            pnlFalla.Location = new Point(24, 390);
            pnlFalla.Name = "pnlFalla";
            pnlFalla.Padding = new Padding(16);
            pnlFalla.Size = new Size(592, 140);
            pnlFalla.TabIndex = 3;
            // 
            // lblPasoFalla
            // 
            lblPasoFalla.AutoSize = true;
            lblPasoFalla.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblPasoFalla.ForeColor = Color.FromArgb(71, 85, 105);
            lblPasoFalla.Location = new Point(16, 12);
            lblPasoFalla.Name = "lblPasoFalla";
            lblPasoFalla.TabIndex = 0;
            lblPasoFalla.Text = "3  ·  FALLA REPORTADA";
            // 
            // txtFalla
            // 
            txtFalla.BackColor = Color.FromArgb(21, 32, 48);
            txtFalla.BorderStyle = BorderStyle.FixedSingle;
            txtFalla.Font = new Font("Segoe UI", 9.5F);
            txtFalla.ForeColor = Color.FromArgb(226, 232, 240);
            txtFalla.MaxLength = 200;
            txtFalla.Multiline = true;
            txtFalla.ScrollBars = ScrollBars.Vertical;
            txtFalla.Location = new Point(16, 36);
            txtFalla.Name = "txtFalla";
            txtFalla.Size = new Size(560, 70);
            txtFalla.TabIndex = 1;
            txtFalla.TextChanged += txtFalla_TextChanged;
            // 
            // lblContador
            // 
            lblContador.AutoSize = false;
            lblContador.Font = new Font("Segoe UI", 9F);
            lblContador.ForeColor = Color.FromArgb(148, 163, 184);
            lblContador.Location = new Point(506, 112);
            lblContador.Name = "lblContador";
            lblContador.Size = new Size(70, 18);
            lblContador.TabIndex = 2;
            lblContador.Text = "0 / 200";
            lblContador.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnRecepcionar
            // 
            btnRecepcionar.BackColor = Color.FromArgb(14, 116, 144);
            btnRecepcionar.Cursor = Cursors.Hand;
            btnRecepcionar.FlatAppearance.BorderSize = 0;
            btnRecepcionar.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 94, 117);
            btnRecepcionar.FlatStyle = FlatStyle.Flat;
            btnRecepcionar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRecepcionar.ForeColor = Color.FromArgb(226, 232, 240);
            btnRecepcionar.UseVisualStyleBackColor = false;
            btnRecepcionar.Location = new Point(248, 546);
            btnRecepcionar.Name = "btnRecepcionar";
            btnRecepcionar.Size = new Size(200, 40);
            btnRecepcionar.TabIndex = 4;
            btnRecepcionar.Text = "Recepcionar equipo";
            btnRecepcionar.Click += btnRecepcionar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(45, 60, 82);
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 9.5F);
            btnCancelar.ForeColor = Color.FromArgb(226, 232, 240);
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Location = new Point(460, 546);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(156, 40);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // FrmRecepcionEquipo_382QA
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(640, 606);
            Controls.Add(btnCancelar);
            Controls.Add(btnRecepcionar);
            Controls.Add(pnlFalla);
            Controls.Add(pnlEquipo);
            Controls.Add(pnlCliente);
            Controls.Add(pnlTopbar);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmRecepcionEquipo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Recepción de Equipo";
            Load += FrmRecepcionEquipo_Load;
            pnlTopbar.ResumeLayout(false);
            pnlTopbar.PerformLayout();
            pnlCliente.ResumeLayout(false);
            pnlCliente.PerformLayout();
            pnlEquipo.ResumeLayout(false);
            pnlEquipo.PerformLayout();
            pnlFalla.ResumeLayout(false);
            pnlFalla.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTopbar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlCliente;
        private System.Windows.Forms.Label lblPasoCliente;
        private System.Windows.Forms.Label lblDni;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.Button btnBuscarCliente;
        private System.Windows.Forms.Label lblClienteNombre;
        private System.Windows.Forms.Label lblClienteEstado;
        private System.Windows.Forms.Button btnRegistrarCliente;
        private System.Windows.Forms.Panel pnlEquipo;
        private System.Windows.Forms.Label lblPasoEquipo;
        private System.Windows.Forms.Label lblImei;
        private System.Windows.Forms.TextBox txtImei;
        private System.Windows.Forms.Button btnBuscarEquipo;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.Label lblModelo;
        private System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.Label lblEquipoEstado;
        private System.Windows.Forms.Panel pnlFalla;
        private System.Windows.Forms.Label lblPasoFalla;
        private System.Windows.Forms.TextBox txtFalla;
        private System.Windows.Forms.Label lblContador;
        private System.Windows.Forms.Button btnRecepcionar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
