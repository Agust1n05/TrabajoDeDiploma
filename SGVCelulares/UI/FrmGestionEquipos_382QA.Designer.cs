namespace UI
{
    partial class FrmGestionEquipos_382QA
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
            lblCantidad = new Label();
            lblTitulo = new Label();
            pnlContenido = new Panel();
            pnlBotones = new Panel();
            btnCrear = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            btnAplicar = new Button();
            btnCancelar = new Button();
            btnSalir = new Button();
            pnlFormulario = new Panel();
            lblMarca = new Label();
            txtMarca = new TextBox();
            lblModelo = new Label();
            txtModelo = new TextBox();
            lblImei = new Label();
            txtImei = new TextBox();
            grilla = new DataGridView();
            pnlFiltros = new Panel();
            lblFiltro = new Label();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            pnlTopbar.SuspendLayout();
            pnlContenido.SuspendLayout();
            pnlBotones.SuspendLayout();
            pnlFormulario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grilla).BeginInit();
            pnlFiltros.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTopbar
            // 
            pnlTopbar.BackColor = Color.FromArgb(21, 32, 48);
            pnlTopbar.Controls.Add(lblNumero);
            pnlTopbar.Controls.Add(lblCantidad);
            pnlTopbar.Controls.Add(lblTitulo);
            pnlTopbar.Dock = DockStyle.Top;
            pnlTopbar.Location = new Point(0, 0);
            pnlTopbar.Name = "pnlTopbar";
            pnlTopbar.Size = new Size(1060, 48);
            pnlTopbar.TabIndex = 0;
            // 
            // lblNumero
            // 
            lblNumero.AutoSize = true;
            lblNumero.Font = new Font("Segoe UI", 9F);
            lblNumero.ForeColor = Color.FromArgb(148, 163, 184);
            lblNumero.Location = new Point(960, 18);
            lblNumero.Name = "lblNumero";
            lblNumero.TabIndex = 2;
            lblNumero.Text = "X";
            lblNumero.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Font = new Font("Segoe UI", 9F);
            lblCantidad.ForeColor = Color.FromArgb(148, 163, 184);
            lblCantidad.Location = new Point(800, 18);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.TabIndex = 1;
            lblCantidad.Text = "Número de equipos:";
            lblCantidad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(226, 232, 240);
            lblTitulo.Location = new Point(16, 14);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "EQUIPOS";
            // 
            // pnlContenido
            // 
            pnlContenido.BackColor = Color.FromArgb(30, 42, 58);
            pnlContenido.Controls.Add(pnlBotones);
            pnlContenido.Controls.Add(pnlFormulario);
            pnlContenido.Controls.Add(grilla);
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
            pnlBotones.Controls.Add(btnCrear);
            pnlBotones.Controls.Add(btnModificar);
            pnlBotones.Controls.Add(btnEliminar);
            pnlBotones.Controls.Add(btnAplicar);
            pnlBotones.Controls.Add(btnCancelar);
            pnlBotones.Controls.Add(btnSalir);
            pnlBotones.Location = new Point(860, 72);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Padding = new Padding(8);
            pnlBotones.Size = new Size(172, 452);
            pnlBotones.TabIndex = 3;
            // 
            // btnCrear
            // 
            btnCrear.BackColor = Color.FromArgb(45, 60, 82);
            btnCrear.Cursor = Cursors.Hand;
            btnCrear.FlatAppearance.BorderSize = 0;
            btnCrear.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnCrear.FlatStyle = FlatStyle.Flat;
            btnCrear.Font = new Font("Segoe UI", 9.5F);
            btnCrear.ForeColor = Color.FromArgb(226, 232, 240);
            btnCrear.Location = new Point(8, 8);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(156, 38);
            btnCrear.TabIndex = 0;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = false;
            btnCrear.Click += btnCrear_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.FromArgb(45, 60, 82);
            btnModificar.Cursor = Cursors.Hand;
            btnModificar.FlatAppearance.BorderSize = 0;
            btnModificar.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnModificar.FlatStyle = FlatStyle.Flat;
            btnModificar.Font = new Font("Segoe UI", 9.5F);
            btnModificar.ForeColor = Color.FromArgb(226, 232, 240);
            btnModificar.Location = new Point(8, 54);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(156, 38);
            btnModificar.TabIndex = 1;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.FromArgb(45, 60, 82);
            btnEliminar.Cursor = Cursors.Hand;
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Segoe UI", 9.5F);
            btnEliminar.ForeColor = Color.FromArgb(226, 232, 240);
            btnEliminar.Location = new Point(8, 100);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(156, 38);
            btnEliminar.TabIndex = 2;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnAplicar
            // 
            btnAplicar.BackColor = Color.FromArgb(45, 60, 82);
            btnAplicar.Cursor = Cursors.Hand;
            btnAplicar.FlatAppearance.BorderSize = 0;
            btnAplicar.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnAplicar.FlatStyle = FlatStyle.Flat;
            btnAplicar.Font = new Font("Segoe UI", 9.5F);
            btnAplicar.ForeColor = Color.FromArgb(226, 232, 240);
            btnAplicar.Location = new Point(8, 146);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new Size(156, 38);
            btnAplicar.TabIndex = 3;
            btnAplicar.Text = "Aplicar";
            btnAplicar.UseVisualStyleBackColor = false;
            btnAplicar.Click += btnAplicar_Click;
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
            btnCancelar.Location = new Point(8, 192);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(156, 38);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
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
            btnSalir.TabIndex = 5;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // pnlFormulario
            // 
            pnlFormulario.BackColor = Color.FromArgb(25, 37, 53);
            pnlFormulario.Controls.Add(lblMarca);
            pnlFormulario.Controls.Add(txtMarca);
            pnlFormulario.Controls.Add(lblModelo);
            pnlFormulario.Controls.Add(txtModelo);
            pnlFormulario.Controls.Add(lblImei);
            pnlFormulario.Controls.Add(txtImei);
            pnlFormulario.Location = new Point(24, 308);
            pnlFormulario.Name = "pnlFormulario";
            pnlFormulario.Padding = new Padding(16);
            pnlFormulario.Size = new Size(820, 216);
            pnlFormulario.TabIndex = 2;
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.Font = new Font("Segoe UI", 9F);
            lblMarca.ForeColor = Color.FromArgb(148, 163, 184);
            lblMarca.Location = new Point(16, 16);
            lblMarca.Name = "lblMarca";
            lblMarca.TabIndex = 0;
            lblMarca.Text = "Marca";
            // 
            // txtMarca
            // 
            txtMarca.BackColor = Color.FromArgb(21, 32, 48);
            txtMarca.BorderStyle = BorderStyle.FixedSingle;
            txtMarca.Font = new Font("Segoe UI", 9.5F);
            txtMarca.ForeColor = Color.FromArgb(226, 232, 240);
            txtMarca.Location = new Point(160, 13);
            txtMarca.MaxLength = 50;
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(240, 24);
            txtMarca.TabIndex = 1;
            // 
            // lblModelo
            // 
            lblModelo.AutoSize = true;
            lblModelo.Font = new Font("Segoe UI", 9F);
            lblModelo.ForeColor = Color.FromArgb(148, 163, 184);
            lblModelo.Location = new Point(16, 54);
            lblModelo.Name = "lblModelo";
            lblModelo.TabIndex = 2;
            lblModelo.Text = "Modelo";
            // 
            // txtModelo
            // 
            txtModelo.BackColor = Color.FromArgb(21, 32, 48);
            txtModelo.BorderStyle = BorderStyle.FixedSingle;
            txtModelo.Font = new Font("Segoe UI", 9.5F);
            txtModelo.ForeColor = Color.FromArgb(226, 232, 240);
            txtModelo.Location = new Point(160, 51);
            txtModelo.MaxLength = 50;
            txtModelo.Name = "txtModelo";
            txtModelo.Size = new Size(240, 24);
            txtModelo.TabIndex = 3;
            // 
            // lblImei
            // 
            lblImei.AutoSize = true;
            lblImei.Font = new Font("Segoe UI", 9F);
            lblImei.ForeColor = Color.FromArgb(148, 163, 184);
            lblImei.Location = new Point(16, 92);
            lblImei.Name = "lblImei";
            lblImei.TabIndex = 4;
            lblImei.Text = "IMEI";
            // 
            // txtImei
            // 
            txtImei.BackColor = Color.FromArgb(21, 32, 48);
            txtImei.BorderStyle = BorderStyle.FixedSingle;
            txtImei.Font = new Font("Segoe UI", 9.5F);
            txtImei.ForeColor = Color.FromArgb(226, 232, 240);
            txtImei.Location = new Point(160, 89);
            txtImei.MaxLength = 15;
            txtImei.Name = "txtImei";
            txtImei.Size = new Size(240, 24);
            txtImei.TabIndex = 5;
            // 
            // grilla
            // 
            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToDeleteRows = false;
            grilla.AllowUserToResizeRows = false;
            grilla.BackgroundColor = Color.FromArgb(25, 37, 53);
            grilla.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(21, 32, 48);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(148, 163, 184);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            grilla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            grilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(25, 37, 53);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(226, 232, 240);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(45, 60, 82);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(226, 232, 240);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            grilla.DefaultCellStyle = dataGridViewCellStyle2;
            grilla.EnableHeadersVisualStyles = false;
            grilla.GridColor = Color.FromArgb(45, 60, 82);
            grilla.Location = new Point(24, 72);
            grilla.MultiSelect = false;
            grilla.Name = "grilla";
            grilla.ReadOnly = true;
            grilla.RowHeadersVisible = false;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.Size = new Size(820, 220);
            grilla.TabIndex = 1;
            grilla.SelectionChanged += grilla_SelectionChanged;
            // 
            // pnlFiltros
            // 
            pnlFiltros.BackColor = Color.FromArgb(25, 37, 53);
            pnlFiltros.Controls.Add(lblFiltro);
            pnlFiltros.Controls.Add(lblBuscar);
            pnlFiltros.Controls.Add(txtBuscar);
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
            lblFiltro.TabIndex = 0;
            lblFiltro.Text = "FILTRAR";
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Font = new Font("Segoe UI", 9F);
            lblBuscar.ForeColor = Color.FromArgb(148, 163, 184);
            lblBuscar.Location = new Point(80, 9);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "Buscar IMEI o modelo";
            // 
            // txtBuscar
            // 
            txtBuscar.BackColor = Color.FromArgb(21, 32, 48);
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.Font = new Font("Segoe UI", 9.5F);
            txtBuscar.ForeColor = Color.FromArgb(226, 232, 240);
            txtBuscar.Location = new Point(260, 6);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(260, 24);
            txtBuscar.TabIndex = 2;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // FrmGestionEquipos_382QA
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
            Name = "FrmGestionEquipos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Equipos";
            Load += FrmGestionEquipos_Load;
            pnlTopbar.ResumeLayout(false);
            pnlTopbar.PerformLayout();
            pnlContenido.ResumeLayout(false);
            pnlBotones.ResumeLayout(false);
            pnlFormulario.ResumeLayout(false);
            pnlFormulario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)grilla).EndInit();
            pnlFiltros.ResumeLayout(false);
            pnlFiltros.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTopbar;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.Panel pnlBotones;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Panel pnlFormulario;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.Label lblModelo;
        private System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.Label lblImei;
        private System.Windows.Forms.TextBox txtImei;
        private System.Windows.Forms.DataGridView grilla;
        private System.Windows.Forms.Panel pnlFiltros;
        private System.Windows.Forms.Label lblFiltro;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
    }
}
