namespace UI
{
    partial class FrmDiagnosticoEquipo_382QA
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
            pnlOT = new Panel();
            lblOT = new Label();
            lblEquipo = new Label();
            lblFalla = new Label();
            pnlDiagnostico = new Panel();
            lblPasoDiagnostico = new Label();
            txtDetalle = new TextBox();
            lblContador = new Label();
            lblManoObra = new Label();
            nudManoObra = new NumericUpDown();
            pnlRepuestos = new Panel();
            lblPasoRepuestos = new Label();
            cboRepuesto = new ComboBox();
            lblCantidad = new Label();
            nudCantidad = new NumericUpDown();
            btnAgregar = new Button();
            grilla = new DataGridView();
            btnQuitar = new Button();
            lblAvisoStock = new Label();
            lblTotalTitulo = new Label();
            lblTotal = new Label();
            btnRegistrar = new Button();
            btnCancelar = new Button();
            pnlTopbar.SuspendLayout();
            pnlOT.SuspendLayout();
            pnlDiagnostico.SuspendLayout();
            pnlRepuestos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grilla).BeginInit();
            SuspendLayout();
            // 
            // pnlTopbar
            // 
            pnlTopbar.BackColor = Color.FromArgb(21, 32, 48);
            pnlTopbar.Controls.Add(lblTitulo);
            pnlTopbar.Dock = DockStyle.Top;
            pnlTopbar.Name = "pnlTopbar";
            pnlTopbar.Size = new Size(780, 48);
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
            lblTitulo.Text = "DIAGNÓSTICO DE EQUIPO";
            // 
            // pnlOT
            // 
            pnlOT.BackColor = Color.FromArgb(25, 37, 53);
            pnlOT.Controls.Add(lblOT);
            pnlOT.Controls.Add(lblEquipo);
            pnlOT.Controls.Add(lblFalla);
            pnlOT.Location = new Point(24, 64);
            pnlOT.Name = "pnlOT";
            pnlOT.Padding = new Padding(16);
            pnlOT.Size = new Size(732, 96);
            pnlOT.TabIndex = 1;
            // 
            // lblOT
            // 
            lblOT.AutoSize = false;
            lblOT.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblOT.ForeColor = Color.FromArgb(226, 232, 240);
            lblOT.Location = new Point(16, 12);
            lblOT.Name = "lblOT";
            lblOT.Size = new Size(700, 24);
            lblOT.TabIndex = 0;
            lblOT.Text = "OT N° -";
            // 
            // lblEquipo
            // 
            lblEquipo.AutoSize = false;
            lblEquipo.Font = new Font("Segoe UI", 9F);
            lblEquipo.ForeColor = Color.FromArgb(148, 163, 184);
            lblEquipo.Location = new Point(16, 40);
            lblEquipo.Name = "lblEquipo";
            lblEquipo.Size = new Size(700, 20);
            lblEquipo.TabIndex = 1;
            lblEquipo.Text = "-";
            // 
            // lblFalla
            // 
            lblFalla.AutoSize = false;
            lblFalla.Font = new Font("Segoe UI", 9F);
            lblFalla.ForeColor = Color.FromArgb(56, 189, 248);
            lblFalla.Location = new Point(16, 64);
            lblFalla.Name = "lblFalla";
            lblFalla.Size = new Size(700, 20);
            lblFalla.TabIndex = 2;
            lblFalla.Text = "-";
            // 
            // pnlDiagnostico
            // 
            pnlDiagnostico.BackColor = Color.FromArgb(25, 37, 53);
            pnlDiagnostico.Controls.Add(lblPasoDiagnostico);
            pnlDiagnostico.Controls.Add(txtDetalle);
            pnlDiagnostico.Controls.Add(lblContador);
            pnlDiagnostico.Controls.Add(lblManoObra);
            pnlDiagnostico.Controls.Add(nudManoObra);
            pnlDiagnostico.Location = new Point(24, 172);
            pnlDiagnostico.Name = "pnlDiagnostico";
            pnlDiagnostico.Padding = new Padding(16);
            pnlDiagnostico.Size = new Size(732, 150);
            pnlDiagnostico.TabIndex = 2;
            // 
            // lblPasoDiagnostico
            // 
            lblPasoDiagnostico.AutoSize = true;
            lblPasoDiagnostico.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblPasoDiagnostico.ForeColor = Color.FromArgb(71, 85, 105);
            lblPasoDiagnostico.Location = new Point(16, 12);
            lblPasoDiagnostico.Name = "lblPasoDiagnostico";
            lblPasoDiagnostico.TabIndex = 0;
            lblPasoDiagnostico.Text = "1  ·  DIAGNÓSTICO";
            // 
            // txtDetalle
            // 
            txtDetalle.BackColor = Color.FromArgb(21, 32, 48);
            txtDetalle.BorderStyle = BorderStyle.FixedSingle;
            txtDetalle.Font = new Font("Segoe UI", 9.5F);
            txtDetalle.ForeColor = Color.FromArgb(226, 232, 240);
            txtDetalle.MaxLength = 500;
            txtDetalle.Multiline = true;
            txtDetalle.ScrollBars = ScrollBars.Vertical;
            txtDetalle.Location = new Point(16, 34);
            txtDetalle.Name = "txtDetalle";
            txtDetalle.Size = new Size(700, 64);
            txtDetalle.TabIndex = 1;
            txtDetalle.TextChanged += txtDetalle_TextChanged;
            // 
            // lblContador
            // 
            lblContador.AutoSize = false;
            lblContador.Font = new Font("Segoe UI", 9F);
            lblContador.ForeColor = Color.FromArgb(148, 163, 184);
            lblContador.Location = new Point(636, 102);
            lblContador.Name = "lblContador";
            lblContador.Size = new Size(80, 18);
            lblContador.TabIndex = 2;
            lblContador.Text = "0 / 500";
            lblContador.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblManoObra
            // 
            lblManoObra.AutoSize = true;
            lblManoObra.Font = new Font("Segoe UI", 9F);
            lblManoObra.ForeColor = Color.FromArgb(148, 163, 184);
            lblManoObra.Location = new Point(16, 114);
            lblManoObra.Name = "lblManoObra";
            lblManoObra.TabIndex = 3;
            lblManoObra.Text = "Costo de mano de obra  $";
            // 
            // nudManoObra
            // 
            nudManoObra.BackColor = Color.FromArgb(21, 32, 48);
            nudManoObra.BorderStyle = BorderStyle.FixedSingle;
            nudManoObra.DecimalPlaces = 2;
            nudManoObra.Font = new Font("Segoe UI", 9.5F);
            nudManoObra.ForeColor = Color.FromArgb(226, 232, 240);
            nudManoObra.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            nudManoObra.TextAlign = HorizontalAlignment.Right;
            nudManoObra.ThousandsSeparator = true;
            nudManoObra.Location = new Point(190, 111);
            nudManoObra.Name = "nudManoObra";
            nudManoObra.Size = new Size(150, 24);
            nudManoObra.TabIndex = 4;
            nudManoObra.ValueChanged += nudManoObra_ValueChanged;
            // 
            // pnlRepuestos
            // 
            pnlRepuestos.BackColor = Color.FromArgb(25, 37, 53);
            pnlRepuestos.Controls.Add(lblPasoRepuestos);
            pnlRepuestos.Controls.Add(cboRepuesto);
            pnlRepuestos.Controls.Add(lblCantidad);
            pnlRepuestos.Controls.Add(nudCantidad);
            pnlRepuestos.Controls.Add(btnAgregar);
            pnlRepuestos.Controls.Add(grilla);
            pnlRepuestos.Controls.Add(btnQuitar);
            pnlRepuestos.Controls.Add(lblAvisoStock);
            pnlRepuestos.Location = new Point(24, 334);
            pnlRepuestos.Name = "pnlRepuestos";
            pnlRepuestos.Padding = new Padding(16);
            pnlRepuestos.Size = new Size(732, 262);
            pnlRepuestos.TabIndex = 3;
            // 
            // lblPasoRepuestos
            // 
            lblPasoRepuestos.AutoSize = true;
            lblPasoRepuestos.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblPasoRepuestos.ForeColor = Color.FromArgb(71, 85, 105);
            lblPasoRepuestos.Location = new Point(16, 12);
            lblPasoRepuestos.Name = "lblPasoRepuestos";
            lblPasoRepuestos.TabIndex = 0;
            lblPasoRepuestos.Text = "2  ·  REPUESTOS REQUERIDOS";
            // 
            // cboRepuesto
            // 
            cboRepuesto.BackColor = Color.FromArgb(21, 32, 48);
            cboRepuesto.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRepuesto.FlatStyle = FlatStyle.Flat;
            cboRepuesto.Font = new Font("Segoe UI", 9.5F);
            cboRepuesto.ForeColor = Color.FromArgb(226, 232, 240);
            cboRepuesto.Location = new Point(16, 36);
            cboRepuesto.Name = "cboRepuesto";
            cboRepuesto.Size = new Size(430, 25);
            cboRepuesto.TabIndex = 1;
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Font = new Font("Segoe UI", 9F);
            lblCantidad.ForeColor = Color.FromArgb(148, 163, 184);
            lblCantidad.Location = new Point(462, 40);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.TabIndex = 2;
            lblCantidad.Text = "Cant.";
            // 
            // nudCantidad
            // 
            nudCantidad.BackColor = Color.FromArgb(21, 32, 48);
            nudCantidad.BorderStyle = BorderStyle.FixedSingle;
            nudCantidad.DecimalPlaces = 0;
            nudCantidad.Font = new Font("Segoe UI", 9.5F);
            nudCantidad.ForeColor = Color.FromArgb(226, 232, 240);
            nudCantidad.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidad.TextAlign = HorizontalAlignment.Right;
            nudCantidad.ThousandsSeparator = true;
            nudCantidad.Location = new Point(506, 37);
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(70, 24);
            nudCantidad.TabIndex = 3;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.FromArgb(45, 60, 82);
            btnAgregar.Cursor = Cursors.Hand;
            btnAgregar.FlatAppearance.BorderSize = 0;
            btnAgregar.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.Font = new Font("Segoe UI", 9.5F);
            btnAgregar.ForeColor = Color.FromArgb(226, 232, 240);
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Location = new Point(592, 35);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(124, 28);
            btnAgregar.TabIndex = 4;
            btnAgregar.Text = "Agregar";
            btnAgregar.Click += btnAgregar_Click;
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
            grilla.Location = new Point(16, 74);
            grilla.Name = "grilla";
            grilla.Size = new Size(700, 138);
            grilla.TabIndex = 5;
            // 
            // btnQuitar
            // 
            btnQuitar.BackColor = Color.FromArgb(45, 60, 82);
            btnQuitar.Cursor = Cursors.Hand;
            btnQuitar.FlatAppearance.BorderSize = 0;
            btnQuitar.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnQuitar.FlatStyle = FlatStyle.Flat;
            btnQuitar.Font = new Font("Segoe UI", 9.5F);
            btnQuitar.ForeColor = Color.FromArgb(226, 232, 240);
            btnQuitar.UseVisualStyleBackColor = false;
            btnQuitar.Location = new Point(16, 222);
            btnQuitar.Name = "btnQuitar";
            btnQuitar.Size = new Size(124, 28);
            btnQuitar.TabIndex = 6;
            btnQuitar.Text = "Quitar";
            btnQuitar.Click += btnQuitar_Click;
            // 
            // lblAvisoStock
            // 
            lblAvisoStock.AutoSize = false;
            lblAvisoStock.Font = new Font("Segoe UI", 9F);
            lblAvisoStock.ForeColor = Color.FromArgb(251, 191, 36);
            lblAvisoStock.Location = new Point(152, 228);
            lblAvisoStock.Name = "lblAvisoStock";
            lblAvisoStock.Size = new Size(564, 20);
            lblAvisoStock.TabIndex = 7;
            lblAvisoStock.Text = "";
            // 
            // lblTotalTitulo
            // 
            lblTotalTitulo.AutoSize = true;
            lblTotalTitulo.Font = new Font("Segoe UI", 9F);
            lblTotalTitulo.ForeColor = Color.FromArgb(148, 163, 184);
            lblTotalTitulo.Location = new Point(24, 618);
            lblTotalTitulo.Name = "lblTotalTitulo";
            lblTotalTitulo.TabIndex = 4;
            lblTotalTitulo.Text = "TOTAL DEL PRESUPUESTO";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(226, 232, 240);
            lblTotal.Location = new Point(24, 638);
            lblTotal.Name = "lblTotal";
            lblTotal.TabIndex = 5;
            lblTotal.Text = "$ 0,00";
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
            btnRegistrar.Location = new Point(368, 620);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(220, 42);
            btnRegistrar.TabIndex = 6;
            btnRegistrar.Text = "Registrar diagnóstico";
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
            btnCancelar.Location = new Point(600, 620);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(156, 42);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // FrmDiagnosticoEquipo_382QA
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(780, 684);
            Controls.Add(btnCancelar);
            Controls.Add(btnRegistrar);
            Controls.Add(lblTotal);
            Controls.Add(lblTotalTitulo);
            Controls.Add(pnlRepuestos);
            Controls.Add(pnlDiagnostico);
            Controls.Add(pnlOT);
            Controls.Add(pnlTopbar);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmDiagnosticoEquipo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Diagnóstico de Equipo";
            Load += FrmDiagnosticoEquipo_Load;
            pnlTopbar.ResumeLayout(false);
            pnlTopbar.PerformLayout();
            pnlOT.ResumeLayout(false);
            pnlOT.PerformLayout();
            pnlDiagnostico.ResumeLayout(false);
            pnlDiagnostico.PerformLayout();
            pnlRepuestos.ResumeLayout(false);
            pnlRepuestos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)grilla).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTopbar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlOT;
        private System.Windows.Forms.Label lblOT;
        private System.Windows.Forms.Label lblEquipo;
        private System.Windows.Forms.Label lblFalla;
        private System.Windows.Forms.Panel pnlDiagnostico;
        private System.Windows.Forms.Label lblPasoDiagnostico;
        private System.Windows.Forms.TextBox txtDetalle;
        private System.Windows.Forms.Label lblContador;
        private System.Windows.Forms.Label lblManoObra;
        private System.Windows.Forms.NumericUpDown nudManoObra;
        private System.Windows.Forms.Panel pnlRepuestos;
        private System.Windows.Forms.Label lblPasoRepuestos;
        private System.Windows.Forms.ComboBox cboRepuesto;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView grilla;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Label lblAvisoStock;
        private System.Windows.Forms.Label lblTotalTitulo;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
