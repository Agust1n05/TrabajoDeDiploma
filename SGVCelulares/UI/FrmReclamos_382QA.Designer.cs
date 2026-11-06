namespace UI
{
    partial class FrmReclamos_382QA
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
            DataGridViewCellStyle estiloEncabezadogrilla = new DataGridViewCellStyle();
            DataGridViewCellStyle estiloCeldagrilla = new DataGridViewCellStyle();
            pnlTopbar = new Panel();
            lblTitulo = new Label();
            lblCantidad = new Label();
            lblNumero = new Label();
            pnlFiltros = new Panel();
            lblFiltro = new Label();
            lblEstado = new Label();
            cboEstado = new ComboBox();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            rbNroReclamo = new RadioButton();
            rbNroOT = new RadioButton();
            rbDni = new RadioButton();
            rbApellido = new RadioButton();
            rbImei = new RadioButton();
            btnActualizar = new Button();
            grilla = new DataGridView();
            pnlAcciones = new Panel();
            btnRegistrar = new Button();
            btnEvaluar = new Button();
            btnReparar = new Button();
            btnEntregar = new Button();
            btnSalir = new Button();
            pnlTopbar.SuspendLayout();
            pnlFiltros.SuspendLayout();
            pnlAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grilla).BeginInit();
            SuspendLayout();
            // 
            // pnlTopbar
            // 
            pnlTopbar.BackColor = Color.FromArgb(21, 32, 48);
            pnlTopbar.Controls.Add(lblTitulo);
            pnlTopbar.Controls.Add(lblCantidad);
            pnlTopbar.Controls.Add(lblNumero);
            pnlTopbar.Dock = DockStyle.Top;
            pnlTopbar.Name = "pnlTopbar";
            pnlTopbar.Size = new Size(1100, 48);
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
            lblTitulo.Text = "RECLAMOS POR GARANTÍA";
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Font = new Font("Segoe UI", 9F);
            lblCantidad.ForeColor = Color.FromArgb(148, 163, 184);
            lblCantidad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCantidad.Location = new Point(910, 18);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.TabIndex = 1;
            lblCantidad.Text = "Reclamos:";
            // 
            // lblNumero
            // 
            lblNumero.AutoSize = true;
            lblNumero.Font = new Font("Segoe UI", 9F);
            lblNumero.ForeColor = Color.FromArgb(148, 163, 184);
            lblNumero.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblNumero.Location = new Point(990, 18);
            lblNumero.Name = "lblNumero";
            lblNumero.TabIndex = 2;
            lblNumero.Text = "0";
            // 
            // pnlFiltros
            // 
            pnlFiltros.BackColor = Color.FromArgb(25, 37, 53);
            pnlFiltros.Controls.Add(lblFiltro);
            pnlFiltros.Controls.Add(lblEstado);
            pnlFiltros.Controls.Add(cboEstado);
            pnlFiltros.Controls.Add(lblBuscar);
            pnlFiltros.Controls.Add(txtBuscar);
            pnlFiltros.Controls.Add(rbNroReclamo);
            pnlFiltros.Controls.Add(rbNroOT);
            pnlFiltros.Controls.Add(rbDni);
            pnlFiltros.Controls.Add(rbApellido);
            pnlFiltros.Controls.Add(rbImei);
            pnlFiltros.Controls.Add(btnActualizar);
            pnlFiltros.Location = new Point(24, 64);
            pnlFiltros.Name = "pnlFiltros";
            pnlFiltros.Size = new Size(1052, 72);
            pnlFiltros.TabIndex = 1;
            // 
            // lblFiltro
            // 
            lblFiltro.AutoSize = true;
            lblFiltro.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblFiltro.ForeColor = Color.FromArgb(71, 85, 105);
            lblFiltro.Location = new Point(12, 14);
            lblFiltro.Name = "lblFiltro";
            lblFiltro.TabIndex = 0;
            lblFiltro.Text = "FILTRAR";
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Segoe UI", 9F);
            lblEstado.ForeColor = Color.FromArgb(148, 163, 184);
            lblEstado.Location = new Point(84, 12);
            lblEstado.Name = "lblEstado";
            lblEstado.TabIndex = 1;
            lblEstado.Text = "Estado";
            // 
            // cboEstado
            // 
            cboEstado.BackColor = Color.FromArgb(21, 32, 48);
            cboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEstado.FlatStyle = FlatStyle.Flat;
            cboEstado.Font = new Font("Segoe UI", 9.5F);
            cboEstado.ForeColor = Color.FromArgb(226, 232, 240);
            cboEstado.Location = new Point(140, 8);
            cboEstado.Name = "cboEstado";
            cboEstado.Size = new Size(200, 25);
            cboEstado.TabIndex = 2;
            cboEstado.SelectedIndexChanged += cboEstado_SelectedIndexChanged;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Font = new Font("Segoe UI", 9F);
            lblBuscar.ForeColor = Color.FromArgb(148, 163, 184);
            lblBuscar.Location = new Point(372, 12);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.TabIndex = 3;
            lblBuscar.Text = "Buscar";
            // 
            // txtBuscar
            // 
            txtBuscar.BackColor = Color.FromArgb(21, 32, 48);
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.Font = new Font("Segoe UI", 9.5F);
            txtBuscar.ForeColor = Color.FromArgb(226, 232, 240);
            txtBuscar.Location = new Point(428, 8);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(280, 24);
            txtBuscar.TabIndex = 4;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // rbNroReclamo
            // 
            rbNroReclamo.AutoSize = false;
            rbNroReclamo.Font = new Font("Segoe UI", 9F);
            rbNroReclamo.ForeColor = Color.FromArgb(148, 163, 184);
            rbNroReclamo.Location = new Point(428, 42);
            rbNroReclamo.Name = "rbNroReclamo";
            rbNroReclamo.Size = new Size(100, 24);
            rbNroReclamo.TabIndex = 7;
            rbNroReclamo.Checked = true;
            rbNroReclamo.Text = "N° reclamo";
            rbNroReclamo.UseVisualStyleBackColor = true;
            rbNroReclamo.CheckedChanged += rbBuscar_CheckedChanged;
            // 
            // rbNroOT
            // 
            rbNroOT.AutoSize = false;
            rbNroOT.Font = new Font("Segoe UI", 9F);
            rbNroOT.ForeColor = Color.FromArgb(148, 163, 184);
            rbNroOT.Location = new Point(528, 42);
            rbNroOT.Name = "rbNroOT";
            rbNroOT.Size = new Size(100, 24);
            rbNroOT.TabIndex = 8;
            rbNroOT.Text = "N° OT";
            rbNroOT.UseVisualStyleBackColor = true;
            rbNroOT.CheckedChanged += rbBuscar_CheckedChanged;
            // 
            // rbDni
            // 
            rbDni.AutoSize = false;
            rbDni.Font = new Font("Segoe UI", 9F);
            rbDni.ForeColor = Color.FromArgb(148, 163, 184);
            rbDni.Location = new Point(628, 42);
            rbDni.Name = "rbDni";
            rbDni.Size = new Size(100, 24);
            rbDni.TabIndex = 9;
            rbDni.Text = "DNI";
            rbDni.UseVisualStyleBackColor = true;
            rbDni.CheckedChanged += rbBuscar_CheckedChanged;
            // 
            // rbApellido
            // 
            rbApellido.AutoSize = false;
            rbApellido.Font = new Font("Segoe UI", 9F);
            rbApellido.ForeColor = Color.FromArgb(148, 163, 184);
            rbApellido.Location = new Point(728, 42);
            rbApellido.Name = "rbApellido";
            rbApellido.Size = new Size(100, 24);
            rbApellido.TabIndex = 10;
            rbApellido.Text = "Apellido";
            rbApellido.UseVisualStyleBackColor = true;
            rbApellido.CheckedChanged += rbBuscar_CheckedChanged;
            // 
            // rbImei
            // 
            rbImei.AutoSize = false;
            rbImei.Font = new Font("Segoe UI", 9F);
            rbImei.ForeColor = Color.FromArgb(148, 163, 184);
            rbImei.Location = new Point(828, 42);
            rbImei.Name = "rbImei";
            rbImei.Size = new Size(100, 24);
            rbImei.TabIndex = 11;
            rbImei.Text = "IMEI";
            rbImei.UseVisualStyleBackColor = true;
            rbImei.CheckedChanged += rbBuscar_CheckedChanged;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.FromArgb(45, 60, 82);
            btnActualizar.Cursor = Cursors.Hand;
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Font = new Font("Segoe UI", 9.5F);
            btnActualizar.ForeColor = Color.FromArgb(226, 232, 240);
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Location = new Point(930, 5);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(116, 30);
            btnActualizar.TabIndex = 6;
            btnActualizar.Text = "Actualizar";
            btnActualizar.Click += btnActualizar_Click;
            // 
            // grilla
            // 
            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToDeleteRows = false;
            grilla.AllowUserToResizeRows = false;
            grilla.BackgroundColor = Color.FromArgb(25, 37, 53);
            grilla.BorderStyle = BorderStyle.None;
            estiloEncabezadogrilla.Alignment = DataGridViewContentAlignment.MiddleLeft;
            estiloEncabezadogrilla.BackColor = Color.FromArgb(21, 32, 48);
            estiloEncabezadogrilla.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            estiloEncabezadogrilla.ForeColor = Color.FromArgb(148, 163, 184);
            estiloEncabezadogrilla.SelectionBackColor = Color.FromArgb(21, 32, 48);
            estiloEncabezadogrilla.SelectionForeColor = Color.FromArgb(148, 163, 184);
            estiloEncabezadogrilla.Padding = new Padding(6, 0, 6, 0);
            estiloEncabezadogrilla.WrapMode = DataGridViewTriState.True;
            grilla.ColumnHeadersDefaultCellStyle = estiloEncabezadogrilla;
            grilla.ColumnHeadersHeight = 32;
            grilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            estiloCeldagrilla.Alignment = DataGridViewContentAlignment.MiddleLeft;
            estiloCeldagrilla.BackColor = Color.FromArgb(25, 37, 53);
            estiloCeldagrilla.Font = new Font("Segoe UI", 9.5F);
            estiloCeldagrilla.ForeColor = Color.FromArgb(226, 232, 240);
            estiloCeldagrilla.Padding = new Padding(6, 0, 6, 0);
            estiloCeldagrilla.SelectionBackColor = Color.FromArgb(45, 60, 82);
            estiloCeldagrilla.SelectionForeColor = Color.FromArgb(226, 232, 240);
            estiloCeldagrilla.WrapMode = DataGridViewTriState.False;
            grilla.DefaultCellStyle = estiloCeldagrilla;
            grilla.EnableHeadersVisualStyles = false;
            grilla.GridColor = Color.FromArgb(45, 60, 82);
            grilla.MultiSelect = false;
            grilla.ReadOnly = true;
            grilla.RowHeadersVisible = false;
            grilla.RowTemplate.Height = 30;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grilla.Location = new Point(24, 148);
            grilla.Name = "grilla";
            grilla.Size = new Size(1052, 420);
            grilla.TabIndex = 2;
            grilla.SelectionChanged += grilla_SelectionChanged;
            // 
            // pnlAcciones
            // 
            pnlAcciones.BackColor = Color.FromArgb(25, 37, 53);
            pnlAcciones.Controls.Add(btnRegistrar);
            pnlAcciones.Controls.Add(btnEvaluar);
            pnlAcciones.Controls.Add(btnReparar);
            pnlAcciones.Controls.Add(btnEntregar);
            pnlAcciones.Controls.Add(btnSalir);
            pnlAcciones.Location = new Point(24, 584);
            pnlAcciones.Name = "pnlAcciones";
            pnlAcciones.Size = new Size(1052, 60);
            pnlAcciones.TabIndex = 3;
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
            btnRegistrar.Location = new Point(12, 11);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(168, 38);
            btnRegistrar.TabIndex = 0;
            btnRegistrar.Text = "+  Registrar reclamo";
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // btnEvaluar
            // 
            btnEvaluar.BackColor = Color.FromArgb(14, 116, 144);
            btnEvaluar.Cursor = Cursors.Hand;
            btnEvaluar.FlatAppearance.BorderSize = 0;
            btnEvaluar.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 94, 117);
            btnEvaluar.FlatStyle = FlatStyle.Flat;
            btnEvaluar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnEvaluar.ForeColor = Color.FromArgb(226, 232, 240);
            btnEvaluar.UseVisualStyleBackColor = false;
            btnEvaluar.Location = new Point(190, 11);
            btnEvaluar.Name = "btnEvaluar";
            btnEvaluar.Size = new Size(168, 38);
            btnEvaluar.TabIndex = 1;
            btnEvaluar.Text = "Evaluar reclamo";
            btnEvaluar.Click += btnEvaluar_Click;
            // 
            // btnReparar
            // 
            btnReparar.BackColor = Color.FromArgb(14, 116, 144);
            btnReparar.Cursor = Cursors.Hand;
            btnReparar.FlatAppearance.BorderSize = 0;
            btnReparar.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 94, 117);
            btnReparar.FlatStyle = FlatStyle.Flat;
            btnReparar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnReparar.ForeColor = Color.FromArgb(226, 232, 240);
            btnReparar.UseVisualStyleBackColor = false;
            btnReparar.Location = new Point(368, 11);
            btnReparar.Name = "btnReparar";
            btnReparar.Size = new Size(168, 38);
            btnReparar.TabIndex = 2;
            btnReparar.Text = "Reparar reclamo";
            btnReparar.Click += btnReparar_Click;
            // 
            // btnEntregar
            // 
            btnEntregar.BackColor = Color.FromArgb(14, 116, 144);
            btnEntregar.Cursor = Cursors.Hand;
            btnEntregar.FlatAppearance.BorderSize = 0;
            btnEntregar.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 94, 117);
            btnEntregar.FlatStyle = FlatStyle.Flat;
            btnEntregar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnEntregar.ForeColor = Color.FromArgb(226, 232, 240);
            btnEntregar.UseVisualStyleBackColor = false;
            btnEntregar.Location = new Point(546, 11);
            btnEntregar.Name = "btnEntregar";
            btnEntregar.Size = new Size(168, 38);
            btnEntregar.TabIndex = 3;
            btnEntregar.Text = "Entregar reclamo";
            btnEntregar.Click += btnEntregar_Click;
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
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Location = new Point(920, 11);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(120, 38);
            btnSalir.TabIndex = 5;
            btnSalir.Text = "Salir";
            btnSalir.Click += btnSalir_Click;
            // 
            // FrmReclamos_382QA
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(1100, 660);
            Controls.Add(pnlAcciones);
            Controls.Add(grilla);
            Controls.Add(pnlFiltros);
            Controls.Add(pnlTopbar);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmReclamos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reclamos por Garantía";
            Load += FrmReclamos_Load;
            pnlTopbar.ResumeLayout(false);
            pnlTopbar.PerformLayout();
            pnlFiltros.ResumeLayout(false);
            pnlFiltros.PerformLayout();
            pnlAcciones.ResumeLayout(false);
            pnlAcciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)grilla).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTopbar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.Panel pnlFiltros;
        private System.Windows.Forms.Label lblFiltro;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.ComboBox cboEstado;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.RadioButton rbNroReclamo;
        private System.Windows.Forms.RadioButton rbNroOT;
        private System.Windows.Forms.RadioButton rbDni;
        private System.Windows.Forms.RadioButton rbApellido;
        private System.Windows.Forms.RadioButton rbImei;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.DataGridView grilla;
        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Button btnEvaluar;
        private System.Windows.Forms.Button btnReparar;
        private System.Windows.Forms.Button btnEntregar;
        private System.Windows.Forms.Button btnSalir;
    }
}
