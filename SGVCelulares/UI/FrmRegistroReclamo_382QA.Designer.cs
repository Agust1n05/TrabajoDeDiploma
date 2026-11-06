namespace UI
{
    partial class FrmRegistroReclamo_382QA
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
            pnlReparacion = new Panel();
            lblPasoReparacion = new Label();
            lblReparacion = new Label();
            cboReparacion = new ComboBox();
            lblTitFalla = new Label();
            lblFallaOriginal = new Label();
            lblVigencia = new Label();
            pnlReclamo = new Panel();
            lblPasoReclamo = new Label();
            lblMotivo = new Label();
            txtMotivo = new TextBox();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            lblContador = new Label();
            btnRegistrar = new Button();
            btnCancelar = new Button();
            pnlTopbar.SuspendLayout();
            pnlCliente.SuspendLayout();
            pnlReparacion.SuspendLayout();
            pnlReclamo.SuspendLayout();
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
            lblTitulo.Text = "REGISTRO DE RECLAMO POR GARANTÍA";
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
            pnlCliente.Location = new Point(24, 64);
            pnlCliente.Name = "pnlCliente";
            pnlCliente.Padding = new Padding(16);
            pnlCliente.Size = new Size(592, 126);
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
            lblClienteNombre.Location = new Point(130, 74);
            lblClienteNombre.Name = "lblClienteNombre";
            lblClienteNombre.Size = new Size(446, 22);
            lblClienteNombre.TabIndex = 4;
            lblClienteNombre.Text = "";
            //
            // lblClienteEstado
            //
            lblClienteEstado.AutoSize = false;
            lblClienteEstado.Font = new Font("Segoe UI", 9F);
            lblClienteEstado.ForeColor = Color.FromArgb(148, 163, 184);
            lblClienteEstado.Location = new Point(130, 98);
            lblClienteEstado.Name = "lblClienteEstado";
            lblClienteEstado.Size = new Size(446, 20);
            lblClienteEstado.TabIndex = 5;
            lblClienteEstado.Text = "Ingresá el DNI y presioná Buscar.";
            //
            // pnlReparacion
            //
            pnlReparacion.BackColor = Color.FromArgb(25, 37, 53);
            pnlReparacion.Controls.Add(lblPasoReparacion);
            pnlReparacion.Controls.Add(lblReparacion);
            pnlReparacion.Controls.Add(cboReparacion);
            pnlReparacion.Controls.Add(lblTitFalla);
            pnlReparacion.Controls.Add(lblFallaOriginal);
            pnlReparacion.Controls.Add(lblVigencia);
            pnlReparacion.Location = new Point(24, 202);
            pnlReparacion.Name = "pnlReparacion";
            pnlReparacion.Padding = new Padding(16);
            pnlReparacion.Size = new Size(592, 140);
            pnlReparacion.TabIndex = 2;
            //
            // lblPasoReparacion
            //
            lblPasoReparacion.AutoSize = true;
            lblPasoReparacion.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblPasoReparacion.ForeColor = Color.FromArgb(71, 85, 105);
            lblPasoReparacion.Location = new Point(16, 12);
            lblPasoReparacion.Name = "lblPasoReparacion";
            lblPasoReparacion.TabIndex = 0;
            lblPasoReparacion.Text = "2  ·  REPARACIÓN CON GARANTÍA";
            //
            // lblReparacion
            //
            lblReparacion.AutoSize = true;
            lblReparacion.Font = new Font("Segoe UI", 9F);
            lblReparacion.ForeColor = Color.FromArgb(148, 163, 184);
            lblReparacion.Location = new Point(16, 42);
            lblReparacion.Name = "lblReparacion";
            lblReparacion.TabIndex = 1;
            lblReparacion.Text = "Reparación";
            //
            // cboReparacion
            //
            cboReparacion.BackColor = Color.FromArgb(21, 32, 48);
            cboReparacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cboReparacion.FlatStyle = FlatStyle.Flat;
            cboReparacion.Font = new Font("Segoe UI", 9.5F);
            cboReparacion.ForeColor = Color.FromArgb(226, 232, 240);
            cboReparacion.FormattingEnabled = true;
            cboReparacion.Location = new Point(130, 38);
            cboReparacion.Name = "cboReparacion";
            cboReparacion.Size = new Size(446, 25);
            cboReparacion.TabIndex = 2;
            cboReparacion.Format += cboReparacion_Format;
            cboReparacion.SelectedIndexChanged += cboReparacion_SelectedIndexChanged;
            //
            // lblTitFalla
            //
            lblTitFalla.AutoSize = true;
            lblTitFalla.Font = new Font("Segoe UI", 9F);
            lblTitFalla.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitFalla.Location = new Point(16, 78);
            lblTitFalla.Name = "lblTitFalla";
            lblTitFalla.TabIndex = 3;
            lblTitFalla.Text = "Falla original";
            //
            // lblFallaOriginal
            //
            lblFallaOriginal.AutoSize = false;
            lblFallaOriginal.Font = new Font("Segoe UI", 9.5F);
            lblFallaOriginal.ForeColor = Color.FromArgb(226, 232, 240);
            lblFallaOriginal.Location = new Point(130, 76);
            lblFallaOriginal.Name = "lblFallaOriginal";
            lblFallaOriginal.Size = new Size(446, 22);
            lblFallaOriginal.TabIndex = 4;
            lblFallaOriginal.Text = "-";
            //
            // lblVigencia
            //
            lblVigencia.AutoSize = false;
            lblVigencia.Font = new Font("Segoe UI", 9F);
            lblVigencia.ForeColor = Color.FromArgb(148, 163, 184);
            lblVigencia.Location = new Point(130, 104);
            lblVigencia.Name = "lblVigencia";
            lblVigencia.Size = new Size(446, 20);
            lblVigencia.TabIndex = 5;
            lblVigencia.Text = "-";
            //
            // pnlReclamo
            //
            pnlReclamo.BackColor = Color.FromArgb(25, 37, 53);
            pnlReclamo.Controls.Add(lblPasoReclamo);
            pnlReclamo.Controls.Add(lblMotivo);
            pnlReclamo.Controls.Add(txtMotivo);
            pnlReclamo.Controls.Add(lblDescripcion);
            pnlReclamo.Controls.Add(txtDescripcion);
            pnlReclamo.Controls.Add(lblContador);
            pnlReclamo.Location = new Point(24, 354);
            pnlReclamo.Name = "pnlReclamo";
            pnlReclamo.Padding = new Padding(16);
            pnlReclamo.Size = new Size(592, 192);
            pnlReclamo.TabIndex = 3;
            //
            // lblPasoReclamo
            //
            lblPasoReclamo.AutoSize = true;
            lblPasoReclamo.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblPasoReclamo.ForeColor = Color.FromArgb(71, 85, 105);
            lblPasoReclamo.Location = new Point(16, 12);
            lblPasoReclamo.Name = "lblPasoReclamo";
            lblPasoReclamo.TabIndex = 0;
            lblPasoReclamo.Text = "3  ·  RECLAMO";
            //
            // lblMotivo
            //
            lblMotivo.AutoSize = true;
            lblMotivo.Font = new Font("Segoe UI", 9F);
            lblMotivo.ForeColor = Color.FromArgb(148, 163, 184);
            lblMotivo.Location = new Point(16, 42);
            lblMotivo.Name = "lblMotivo";
            lblMotivo.TabIndex = 1;
            lblMotivo.Text = "Motivo";
            //
            // txtMotivo
            //
            txtMotivo.BackColor = Color.FromArgb(21, 32, 48);
            txtMotivo.BorderStyle = BorderStyle.FixedSingle;
            txtMotivo.Font = new Font("Segoe UI", 9.5F);
            txtMotivo.ForeColor = Color.FromArgb(226, 232, 240);
            txtMotivo.MaxLength = 100;
            txtMotivo.Location = new Point(130, 39);
            txtMotivo.Name = "txtMotivo";
            txtMotivo.Size = new Size(446, 24);
            txtMotivo.TabIndex = 2;
            //
            // lblDescripcion
            //
            lblDescripcion.AutoSize = true;
            lblDescripcion.Font = new Font("Segoe UI", 9F);
            lblDescripcion.ForeColor = Color.FromArgb(148, 163, 184);
            lblDescripcion.Location = new Point(16, 76);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.TabIndex = 3;
            lblDescripcion.Text = "Descripción de la falla";
            //
            // txtDescripcion
            //
            txtDescripcion.BackColor = Color.FromArgb(21, 32, 48);
            txtDescripcion.BorderStyle = BorderStyle.FixedSingle;
            txtDescripcion.Font = new Font("Segoe UI", 9.5F);
            txtDescripcion.ForeColor = Color.FromArgb(226, 232, 240);
            txtDescripcion.MaxLength = 200;
            txtDescripcion.Multiline = true;
            txtDescripcion.ScrollBars = ScrollBars.Vertical;
            txtDescripcion.Location = new Point(16, 98);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(560, 62);
            txtDescripcion.TabIndex = 4;
            txtDescripcion.TextChanged += txtDescripcion_TextChanged;
            //
            // lblContador
            //
            lblContador.AutoSize = false;
            lblContador.Font = new Font("Segoe UI", 9F);
            lblContador.ForeColor = Color.FromArgb(148, 163, 184);
            lblContador.Location = new Point(506, 164);
            lblContador.Name = "lblContador";
            lblContador.Size = new Size(70, 18);
            lblContador.TabIndex = 5;
            lblContador.Text = "0 / 200";
            lblContador.TextAlign = ContentAlignment.MiddleRight;
            //
            // btnRegistrar
            //
            btnRegistrar.BackColor = Color.FromArgb(14, 116, 144);
            btnRegistrar.Cursor = Cursors.Hand;
            btnRegistrar.FlatAppearance.BorderSize = 0;
            btnRegistrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 94, 117);
            btnRegistrar.FlatStyle = FlatStyle.Flat;
            btnRegistrar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.FromArgb(226, 232, 240);
            btnRegistrar.UseVisualStyleBackColor = false;
            btnRegistrar.Location = new Point(248, 562);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(200, 40);
            btnRegistrar.TabIndex = 4;
            btnRegistrar.Text = "Registrar reclamo";
            btnRegistrar.Click += btnRegistrar_Click;
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
            btnCancelar.Location = new Point(460, 562);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(156, 40);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            //
            // FrmRegistroReclamo_382QA
            //
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(640, 622);
            Controls.Add(btnCancelar);
            Controls.Add(btnRegistrar);
            Controls.Add(pnlReclamo);
            Controls.Add(pnlReparacion);
            Controls.Add(pnlCliente);
            Controls.Add(pnlTopbar);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmRegistroReclamo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Registro de Reclamo";
            Load += FrmRegistroReclamo_Load;
            pnlTopbar.ResumeLayout(false);
            pnlTopbar.PerformLayout();
            pnlCliente.ResumeLayout(false);
            pnlCliente.PerformLayout();
            pnlReparacion.ResumeLayout(false);
            pnlReparacion.PerformLayout();
            pnlReclamo.ResumeLayout(false);
            pnlReclamo.PerformLayout();
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
        private System.Windows.Forms.Panel pnlReparacion;
        private System.Windows.Forms.Label lblPasoReparacion;
        private System.Windows.Forms.Label lblReparacion;
        private System.Windows.Forms.ComboBox cboReparacion;
        private System.Windows.Forms.Label lblTitFalla;
        private System.Windows.Forms.Label lblFallaOriginal;
        private System.Windows.Forms.Label lblVigencia;
        private System.Windows.Forms.Panel pnlReclamo;
        private System.Windows.Forms.Label lblPasoReclamo;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblContador;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
