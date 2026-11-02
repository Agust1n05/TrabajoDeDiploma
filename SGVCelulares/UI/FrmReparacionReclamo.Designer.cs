namespace UI
{
    partial class FrmReparacionReclamo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            DataGridViewCellStyle estiloEncabezadogrilla = new DataGridViewCellStyle();
            DataGridViewCellStyle estiloCeldagrilla = new DataGridViewCellStyle();
            pnlTopbar = new Panel();
            lblTitulo = new Label();
            pnlReclamo = new Panel();
            lblPasoReclamo = new Label();
            lblTitReclamo = new Label();
            lblReclamo = new Label();
            lblTitEquipo = new Label();
            lblEquipo = new Label();
            lblTitEvaluacion = new Label();
            lblEvaluacion = new Label();
            pnlRepuestos = new Panel();
            lblPasoRepuestos = new Label();
            cboRepuesto = new ComboBox();
            nudCantidad = new NumericUpDown();
            btnAgregar = new Button();
            grilla = new DataGridView();
            btnQuitar = new Button();
            lblAvisoStock = new Label();
            btnConfirmar = new Button();
            btnCancelar = new Button();
            pnlTopbar.SuspendLayout();
            pnlReclamo.SuspendLayout();
            pnlRepuestos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grilla).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            SuspendLayout();
            //
            // estilos de la grilla
            //
            estiloEncabezadogrilla.Alignment = DataGridViewContentAlignment.MiddleLeft;
            estiloEncabezadogrilla.BackColor = Color.FromArgb(21, 32, 48);
            estiloEncabezadogrilla.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            estiloEncabezadogrilla.ForeColor = Color.FromArgb(148, 163, 184);
            estiloEncabezadogrilla.SelectionBackColor = Color.FromArgb(21, 32, 48);
            estiloEncabezadogrilla.SelectionForeColor = Color.FromArgb(148, 163, 184);
            estiloEncabezadogrilla.Padding = new Padding(6, 0, 6, 0);
            estiloEncabezadogrilla.WrapMode = DataGridViewTriState.True;
            estiloCeldagrilla.Alignment = DataGridViewContentAlignment.MiddleLeft;
            estiloCeldagrilla.BackColor = Color.FromArgb(21, 32, 48);
            estiloCeldagrilla.Font = new Font("Segoe UI", 9.5F);
            estiloCeldagrilla.ForeColor = Color.FromArgb(226, 232, 240);
            estiloCeldagrilla.Padding = new Padding(6, 0, 6, 0);
            estiloCeldagrilla.SelectionBackColor = Color.FromArgb(45, 60, 82);
            estiloCeldagrilla.SelectionForeColor = Color.FromArgb(226, 232, 240);
            estiloCeldagrilla.WrapMode = DataGridViewTriState.False;
            //
            // pnlTopbar
            //
            pnlTopbar.BackColor = Color.FromArgb(21, 32, 48);
            pnlTopbar.Dock = DockStyle.Top;
            pnlTopbar.Size = new Size(640, 48);
            pnlTopbar.Name = "pnlTopbar";
            pnlTopbar.TabIndex = 0;
            pnlTopbar.Controls.Add(lblTitulo);
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(226, 232, 240);
            lblTitulo.Location = new Point(16, 14);
            lblTitulo.Text = "REPARACIÓN POR GARANTÍA";
            lblTitulo.Name = "lblTitulo";
            lblTitulo.TabIndex = 1;
            //
            // pnlReclamo
            //
            pnlReclamo.BackColor = Color.FromArgb(25, 37, 53);
            pnlReclamo.Location = new Point(24, 64);
            pnlReclamo.Padding = new Padding(16);
            pnlReclamo.Size = new Size(592, 140);
            pnlReclamo.Name = "pnlReclamo";
            pnlReclamo.TabIndex = 2;
            pnlReclamo.Controls.Add(lblPasoReclamo);
            pnlReclamo.Controls.Add(lblTitReclamo);
            pnlReclamo.Controls.Add(lblReclamo);
            pnlReclamo.Controls.Add(lblTitEquipo);
            pnlReclamo.Controls.Add(lblEquipo);
            pnlReclamo.Controls.Add(lblTitEvaluacion);
            pnlReclamo.Controls.Add(lblEvaluacion);
            //
            // lblPasoReclamo
            //
            lblPasoReclamo.AutoSize = true;
            lblPasoReclamo.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblPasoReclamo.ForeColor = Color.FromArgb(71, 85, 105);
            lblPasoReclamo.Location = new Point(16, 12);
            lblPasoReclamo.Text = "1  ·  RECLAMO APROBADO";
            lblPasoReclamo.Name = "lblPasoReclamo";
            lblPasoReclamo.TabIndex = 3;
            //
            // lblTitReclamo
            //
            lblTitReclamo.AutoSize = true;
            lblTitReclamo.Font = new Font("Segoe UI", 9F);
            lblTitReclamo.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitReclamo.Location = new Point(16, 40);
            lblTitReclamo.Text = "Reclamo";
            lblTitReclamo.Name = "lblTitReclamo";
            lblTitReclamo.TabIndex = 4;
            //
            // lblReclamo
            //
            lblReclamo.AutoSize = false;
            lblReclamo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblReclamo.ForeColor = Color.FromArgb(226, 232, 240);
            lblReclamo.Location = new Point(130, 38);
            lblReclamo.Size = new Size(446, 22);
            lblReclamo.Text = "-";
            lblReclamo.Name = "lblReclamo";
            lblReclamo.TabIndex = 5;
            //
            // lblTitEquipo
            //
            lblTitEquipo.AutoSize = true;
            lblTitEquipo.Font = new Font("Segoe UI", 9F);
            lblTitEquipo.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitEquipo.Location = new Point(16, 64);
            lblTitEquipo.Text = "Equipo";
            lblTitEquipo.Name = "lblTitEquipo";
            lblTitEquipo.TabIndex = 6;
            //
            // lblEquipo
            //
            lblEquipo.AutoSize = false;
            lblEquipo.Font = new Font("Segoe UI", 9.5F);
            lblEquipo.ForeColor = Color.FromArgb(226, 232, 240);
            lblEquipo.Location = new Point(130, 62);
            lblEquipo.Size = new Size(446, 22);
            lblEquipo.Text = "-";
            lblEquipo.Name = "lblEquipo";
            lblEquipo.TabIndex = 7;
            //
            // lblTitEvaluacion
            //
            lblTitEvaluacion.AutoSize = true;
            lblTitEvaluacion.Font = new Font("Segoe UI", 9F);
            lblTitEvaluacion.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitEvaluacion.Location = new Point(16, 88);
            lblTitEvaluacion.Text = "Evaluación";
            lblTitEvaluacion.Name = "lblTitEvaluacion";
            lblTitEvaluacion.TabIndex = 8;
            //
            // lblEvaluacion
            //
            lblEvaluacion.AutoSize = false;
            lblEvaluacion.Font = new Font("Segoe UI", 9.5F);
            lblEvaluacion.ForeColor = Color.FromArgb(226, 232, 240);
            lblEvaluacion.Location = new Point(130, 86);
            lblEvaluacion.Size = new Size(446, 44);
            lblEvaluacion.Text = "-";
            lblEvaluacion.Name = "lblEvaluacion";
            lblEvaluacion.TabIndex = 9;
            //
            // pnlRepuestos
            //
            pnlRepuestos.BackColor = Color.FromArgb(25, 37, 53);
            pnlRepuestos.Location = new Point(24, 216);
            pnlRepuestos.Padding = new Padding(16);
            pnlRepuestos.Size = new Size(592, 268);
            pnlRepuestos.Name = "pnlRepuestos";
            pnlRepuestos.TabIndex = 10;
            pnlRepuestos.Controls.Add(lblPasoRepuestos);
            pnlRepuestos.Controls.Add(cboRepuesto);
            pnlRepuestos.Controls.Add(nudCantidad);
            pnlRepuestos.Controls.Add(btnAgregar);
            pnlRepuestos.Controls.Add(grilla);
            pnlRepuestos.Controls.Add(btnQuitar);
            pnlRepuestos.Controls.Add(lblAvisoStock);
            //
            // lblPasoRepuestos
            //
            lblPasoRepuestos.AutoSize = true;
            lblPasoRepuestos.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblPasoRepuestos.ForeColor = Color.FromArgb(71, 85, 105);
            lblPasoRepuestos.Location = new Point(16, 12);
            lblPasoRepuestos.Text = "2  ·  REPUESTOS UTILIZADOS (SIN CARGO)";
            lblPasoRepuestos.Name = "lblPasoRepuestos";
            lblPasoRepuestos.TabIndex = 11;
            //
            // cboRepuesto
            //
            cboRepuesto.BackColor = Color.FromArgb(21, 32, 48);
            cboRepuesto.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRepuesto.FlatStyle = FlatStyle.Flat;
            cboRepuesto.Font = new Font("Segoe UI", 9.5F);
            cboRepuesto.ForeColor = Color.FromArgb(226, 232, 240);
            cboRepuesto.Location = new Point(16, 36);
            cboRepuesto.Size = new Size(330, 25);
            cboRepuesto.Name = "cboRepuesto";
            cboRepuesto.TabIndex = 12;
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
            nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidad.Location = new Point(356, 37);
            nudCantidad.Size = new Size(70, 24);
            nudCantidad.Name = "nudCantidad";
            nudCantidad.TabIndex = 13;
            //
            // btnAgregar
            //
            btnAgregar.BackColor = Color.FromArgb(45, 60, 82);
            btnAgregar.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnAgregar.Font = new Font("Segoe UI", 9.5F);
            btnAgregar.Cursor = Cursors.Hand;
            btnAgregar.FlatAppearance.BorderSize = 0;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.ForeColor = Color.FromArgb(226, 232, 240);
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Location = new Point(436, 34);
            btnAgregar.Size = new Size(140, 30);
            btnAgregar.Text = "Agregar";
            btnAgregar.Name = "btnAgregar";
            btnAgregar.TabIndex = 14;
            btnAgregar.Click += btnAgregar_Click;
            //
            // grilla
            //
            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToDeleteRows = false;
            grilla.AllowUserToResizeRows = false;
            grilla.BackgroundColor = Color.FromArgb(21, 32, 48);
            grilla.BorderStyle = BorderStyle.None;
            grilla.ColumnHeadersDefaultCellStyle = estiloEncabezadogrilla;
            grilla.ColumnHeadersHeight = 30;
            grilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grilla.DefaultCellStyle = estiloCeldagrilla;
            grilla.EnableHeadersVisualStyles = false;
            grilla.GridColor = Color.FromArgb(45, 60, 82);
            grilla.MultiSelect = false;
            grilla.ReadOnly = true;
            grilla.RowHeadersVisible = false;
            grilla.RowTemplate.Height = 28;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grilla.Location = new Point(16, 72);
            grilla.Size = new Size(560, 140);
            grilla.Name = "grilla";
            grilla.TabIndex = 15;
            //
            // btnQuitar
            //
            btnQuitar.BackColor = Color.FromArgb(45, 60, 82);
            btnQuitar.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnQuitar.Font = new Font("Segoe UI", 9.5F);
            btnQuitar.Cursor = Cursors.Hand;
            btnQuitar.FlatAppearance.BorderSize = 0;
            btnQuitar.FlatStyle = FlatStyle.Flat;
            btnQuitar.ForeColor = Color.FromArgb(226, 232, 240);
            btnQuitar.UseVisualStyleBackColor = false;
            btnQuitar.Location = new Point(16, 222);
            btnQuitar.Size = new Size(140, 30);
            btnQuitar.Text = "Quitar";
            btnQuitar.Name = "btnQuitar";
            btnQuitar.TabIndex = 16;
            btnQuitar.Click += btnQuitar_Click;
            //
            // lblAvisoStock
            //
            lblAvisoStock.AutoSize = false;
            lblAvisoStock.Font = new Font("Segoe UI", 9F);
            lblAvisoStock.ForeColor = Color.FromArgb(251, 191, 36);
            lblAvisoStock.Location = new Point(166, 218);
            lblAvisoStock.Size = new Size(410, 40);
            lblAvisoStock.Text = "";
            lblAvisoStock.Name = "lblAvisoStock";
            lblAvisoStock.TabIndex = 17;
            //
            // btnConfirmar
            //
            btnConfirmar.BackColor = Color.FromArgb(14, 116, 144);
            btnConfirmar.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 94, 117);
            btnConfirmar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnConfirmar.Cursor = Cursors.Hand;
            btnConfirmar.FlatAppearance.BorderSize = 0;
            btnConfirmar.FlatStyle = FlatStyle.Flat;
            btnConfirmar.ForeColor = Color.FromArgb(226, 232, 240);
            btnConfirmar.UseVisualStyleBackColor = false;
            btnConfirmar.Location = new Point(248, 500);
            btnConfirmar.Size = new Size(200, 40);
            btnConfirmar.Text = "Registrar reparación";
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.TabIndex = 18;
            btnConfirmar.Click += btnConfirmar_Click;
            //
            // btnCancelar
            //
            btnCancelar.BackColor = Color.FromArgb(45, 60, 82);
            btnCancelar.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnCancelar.Font = new Font("Segoe UI", 9.5F);
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.ForeColor = Color.FromArgb(226, 232, 240);
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Location = new Point(460, 500);
            btnCancelar.Size = new Size(156, 40);
            btnCancelar.Text = "Cancelar";
            btnCancelar.Name = "btnCancelar";
            btnCancelar.TabIndex = 19;
            btnCancelar.Click += btnCancelar_Click;
            //
            // FrmReparacionReclamo
            //
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(640, 560);
            Controls.Add(btnCancelar);
            Controls.Add(btnConfirmar);
            Controls.Add(pnlRepuestos);
            Controls.Add(pnlReclamo);
            Controls.Add(pnlTopbar);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmReparacionReclamo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reparación por Garantía";
            Load += FrmReparacionReclamo_Load;
            pnlTopbar.ResumeLayout(false);
            pnlTopbar.PerformLayout();
            pnlReclamo.ResumeLayout(false);
            pnlReclamo.PerformLayout();
            pnlRepuestos.ResumeLayout(false);
            pnlRepuestos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)grilla).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTopbar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlReclamo;
        private System.Windows.Forms.Label lblPasoReclamo;
        private System.Windows.Forms.Label lblTitReclamo;
        private System.Windows.Forms.Label lblReclamo;
        private System.Windows.Forms.Label lblTitEquipo;
        private System.Windows.Forms.Label lblEquipo;
        private System.Windows.Forms.Label lblTitEvaluacion;
        private System.Windows.Forms.Label lblEvaluacion;
        private System.Windows.Forms.Panel pnlRepuestos;
        private System.Windows.Forms.Label lblPasoRepuestos;
        private System.Windows.Forms.ComboBox cboRepuesto;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView grilla;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Label lblAvisoStock;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
