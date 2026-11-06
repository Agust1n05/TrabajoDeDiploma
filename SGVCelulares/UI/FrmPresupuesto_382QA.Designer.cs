namespace UI
{
    partial class FrmPresupuesto_382QA
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
            lblCliente = new Label();
            lblEquipo = new Label();
            pnlDiagnostico = new Panel();
            lblPasoDiagnostico = new Label();
            lblDetalleDiagnostico = new Label();
            pnlDetalle = new Panel();
            lblPasoDetalle = new Label();
            grilla = new DataGridView();
            lblTitManoObra = new Label();
            lblManoObra = new Label();
            lblTitTotal = new Label();
            lblTotal = new Label();
            pnlRespuesta = new Panel();
            lblPasoRespuesta = new Label();
            lblFecha = new Label();
            dtpFecha = new DateTimePicker();
            btnRechazar = new Button();
            btnAprobar = new Button();
            btnCancelar = new Button();
            pnlTopbar.SuspendLayout();
            pnlOT.SuspendLayout();
            pnlDiagnostico.SuspendLayout();
            pnlDetalle.SuspendLayout();
            pnlRespuesta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grilla).BeginInit();
            SuspendLayout();
            // 
            // pnlTopbar
            // 
            pnlTopbar.BackColor = Color.FromArgb(21, 32, 48);
            pnlTopbar.Controls.Add(lblTitulo);
            pnlTopbar.Dock = DockStyle.Top;
            pnlTopbar.Name = "pnlTopbar";
            pnlTopbar.Size = new Size(720, 48);
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
            lblTitulo.Text = "RESPUESTA DEL PRESUPUESTO";
            // 
            // pnlOT
            // 
            pnlOT.BackColor = Color.FromArgb(25, 37, 53);
            pnlOT.Controls.Add(lblOT);
            pnlOT.Controls.Add(lblCliente);
            pnlOT.Controls.Add(lblEquipo);
            pnlOT.Location = new Point(24, 64);
            pnlOT.Name = "pnlOT";
            pnlOT.Padding = new Padding(16);
            pnlOT.Size = new Size(672, 88);
            pnlOT.TabIndex = 1;
            // 
            // lblOT
            // 
            lblOT.AutoSize = false;
            lblOT.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblOT.ForeColor = Color.FromArgb(226, 232, 240);
            lblOT.Location = new Point(16, 12);
            lblOT.Name = "lblOT";
            lblOT.Size = new Size(640, 24);
            lblOT.TabIndex = 0;
            lblOT.Text = "OT N° -";
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = false;
            lblCliente.Font = new Font("Segoe UI", 9.5F);
            lblCliente.ForeColor = Color.FromArgb(56, 189, 248);
            lblCliente.Location = new Point(16, 38);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(640, 20);
            lblCliente.TabIndex = 1;
            lblCliente.Text = "-";
            // 
            // lblEquipo
            // 
            lblEquipo.AutoSize = false;
            lblEquipo.Font = new Font("Segoe UI", 9F);
            lblEquipo.ForeColor = Color.FromArgb(148, 163, 184);
            lblEquipo.Location = new Point(16, 60);
            lblEquipo.Name = "lblEquipo";
            lblEquipo.Size = new Size(640, 20);
            lblEquipo.TabIndex = 2;
            lblEquipo.Text = "-";
            // 
            // pnlDiagnostico
            // 
            pnlDiagnostico.BackColor = Color.FromArgb(25, 37, 53);
            pnlDiagnostico.Controls.Add(lblPasoDiagnostico);
            pnlDiagnostico.Controls.Add(lblDetalleDiagnostico);
            pnlDiagnostico.Location = new Point(24, 164);
            pnlDiagnostico.Name = "pnlDiagnostico";
            pnlDiagnostico.Padding = new Padding(16);
            pnlDiagnostico.Size = new Size(672, 100);
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
            lblPasoDiagnostico.Text = "DIAGNÓSTICO DEL TÉCNICO";
            // 
            // lblDetalleDiagnostico
            // 
            lblDetalleDiagnostico.AutoSize = false;
            lblDetalleDiagnostico.Font = new Font("Segoe UI", 9.5F);
            lblDetalleDiagnostico.ForeColor = Color.FromArgb(226, 232, 240);
            lblDetalleDiagnostico.Location = new Point(16, 34);
            lblDetalleDiagnostico.Name = "lblDetalleDiagnostico";
            lblDetalleDiagnostico.Size = new Size(640, 56);
            lblDetalleDiagnostico.TabIndex = 1;
            lblDetalleDiagnostico.Text = "-";
            // 
            // pnlDetalle
            // 
            pnlDetalle.BackColor = Color.FromArgb(25, 37, 53);
            pnlDetalle.Controls.Add(lblPasoDetalle);
            pnlDetalle.Controls.Add(grilla);
            pnlDetalle.Controls.Add(lblTitManoObra);
            pnlDetalle.Controls.Add(lblManoObra);
            pnlDetalle.Controls.Add(lblTitTotal);
            pnlDetalle.Controls.Add(lblTotal);
            pnlDetalle.Location = new Point(24, 276);
            pnlDetalle.Name = "pnlDetalle";
            pnlDetalle.Padding = new Padding(16);
            pnlDetalle.Size = new Size(672, 246);
            pnlDetalle.TabIndex = 3;
            // 
            // lblPasoDetalle
            // 
            lblPasoDetalle.AutoSize = true;
            lblPasoDetalle.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblPasoDetalle.ForeColor = Color.FromArgb(71, 85, 105);
            lblPasoDetalle.Location = new Point(16, 12);
            lblPasoDetalle.Name = "lblPasoDetalle";
            lblPasoDetalle.TabIndex = 0;
            lblPasoDetalle.Text = "DETALLE DEL PRESUPUESTO";
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
            grilla.Location = new Point(16, 34);
            grilla.Name = "grilla";
            grilla.Size = new Size(640, 128);
            grilla.TabIndex = 1;
            // 
            // lblTitManoObra
            // 
            lblTitManoObra.AutoSize = false;
            lblTitManoObra.Font = new Font("Segoe UI", 9F);
            lblTitManoObra.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitManoObra.Location = new Point(326, 174);
            lblTitManoObra.Name = "lblTitManoObra";
            lblTitManoObra.Size = new Size(180, 20);
            lblTitManoObra.TabIndex = 2;
            lblTitManoObra.Text = "Mano de obra";
            lblTitManoObra.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblManoObra
            // 
            lblManoObra.AutoSize = false;
            lblManoObra.Font = new Font("Segoe UI", 9.5F);
            lblManoObra.ForeColor = Color.FromArgb(226, 232, 240);
            lblManoObra.Location = new Point(516, 174);
            lblManoObra.Name = "lblManoObra";
            lblManoObra.Size = new Size(140, 20);
            lblManoObra.TabIndex = 3;
            lblManoObra.Text = "$ 0,00";
            lblManoObra.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTitTotal
            // 
            lblTitTotal.AutoSize = false;
            lblTitTotal.Font = new Font("Segoe UI", 9F);
            lblTitTotal.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitTotal.Location = new Point(326, 206);
            lblTitTotal.Name = "lblTitTotal";
            lblTitTotal.Size = new Size(180, 24);
            lblTitTotal.TabIndex = 4;
            lblTitTotal.Text = "TOTAL";
            lblTitTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = false;
            lblTotal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(226, 232, 240);
            lblTotal.Location = new Point(496, 204);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(160, 26);
            lblTotal.TabIndex = 5;
            lblTotal.Text = "$ 0,00";
            lblTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlRespuesta
            // 
            pnlRespuesta.BackColor = Color.FromArgb(25, 37, 53);
            pnlRespuesta.Controls.Add(lblPasoRespuesta);
            pnlRespuesta.Controls.Add(lblFecha);
            pnlRespuesta.Controls.Add(dtpFecha);
            pnlRespuesta.Controls.Add(btnRechazar);
            pnlRespuesta.Controls.Add(btnAprobar);
            pnlRespuesta.Location = new Point(24, 534);
            pnlRespuesta.Name = "pnlRespuesta";
            pnlRespuesta.Padding = new Padding(16);
            pnlRespuesta.Size = new Size(672, 90);
            pnlRespuesta.TabIndex = 4;
            // 
            // lblPasoRespuesta
            // 
            lblPasoRespuesta.AutoSize = true;
            lblPasoRespuesta.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblPasoRespuesta.ForeColor = Color.FromArgb(71, 85, 105);
            lblPasoRespuesta.Location = new Point(16, 12);
            lblPasoRespuesta.Name = "lblPasoRespuesta";
            lblPasoRespuesta.TabIndex = 0;
            lblPasoRespuesta.Text = "RESPUESTA DEL CLIENTE";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 9F);
            lblFecha.ForeColor = Color.FromArgb(148, 163, 184);
            lblFecha.Location = new Point(16, 48);
            lblFecha.Name = "lblFecha";
            lblFecha.TabIndex = 1;
            lblFecha.Text = "Fecha de respuesta";
            // 
            // dtpFecha
            // 
            dtpFecha.Font = new Font("Segoe UI", 9.5F);
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.CalendarMonthBackground = Color.FromArgb(21, 32, 48);
            dtpFecha.Location = new Point(140, 44);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(130, 25);
            dtpFecha.TabIndex = 2;
            // 
            // btnRechazar
            // 
            btnRechazar.BackColor = Color.FromArgb(45, 60, 82);
            btnRechazar.Cursor = Cursors.Hand;
            btnRechazar.FlatAppearance.BorderSize = 0;
            btnRechazar.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnRechazar.FlatStyle = FlatStyle.Flat;
            btnRechazar.Font = new Font("Segoe UI", 9.5F);
            btnRechazar.ForeColor = Color.FromArgb(226, 232, 240);
            btnRechazar.UseVisualStyleBackColor = false;
            btnRechazar.Location = new Point(324, 38);
            btnRechazar.Name = "btnRechazar";
            btnRechazar.Size = new Size(160, 38);
            btnRechazar.TabIndex = 3;
            btnRechazar.Text = "Cliente rechazó";
            btnRechazar.Click += btnRechazar_Click;
            // 
            // btnAprobar
            // 
            btnAprobar.BackColor = Color.FromArgb(14, 116, 144);
            btnAprobar.Cursor = Cursors.Hand;
            btnAprobar.FlatAppearance.BorderSize = 0;
            btnAprobar.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 94, 117);
            btnAprobar.FlatStyle = FlatStyle.Flat;
            btnAprobar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnAprobar.ForeColor = Color.FromArgb(226, 232, 240);
            btnAprobar.UseVisualStyleBackColor = false;
            btnAprobar.Location = new Point(496, 38);
            btnAprobar.Name = "btnAprobar";
            btnAprobar.Size = new Size(160, 38);
            btnAprobar.TabIndex = 4;
            btnAprobar.Text = "Cliente aprobó";
            btnAprobar.Click += btnAprobar_Click;
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
            btnCancelar.Location = new Point(540, 638);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(156, 38);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // FrmPresupuesto_382QA
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(720, 692);
            Controls.Add(btnCancelar);
            Controls.Add(pnlRespuesta);
            Controls.Add(pnlDetalle);
            Controls.Add(pnlDiagnostico);
            Controls.Add(pnlOT);
            Controls.Add(pnlTopbar);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmPresupuesto";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Respuesta del Presupuesto";
            Load += FrmPresupuesto_Load;
            pnlTopbar.ResumeLayout(false);
            pnlTopbar.PerformLayout();
            pnlOT.ResumeLayout(false);
            pnlOT.PerformLayout();
            pnlDiagnostico.ResumeLayout(false);
            pnlDiagnostico.PerformLayout();
            pnlDetalle.ResumeLayout(false);
            pnlDetalle.PerformLayout();
            pnlRespuesta.ResumeLayout(false);
            pnlRespuesta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)grilla).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTopbar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlOT;
        private System.Windows.Forms.Label lblOT;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblEquipo;
        private System.Windows.Forms.Panel pnlDiagnostico;
        private System.Windows.Forms.Label lblPasoDiagnostico;
        private System.Windows.Forms.Label lblDetalleDiagnostico;
        private System.Windows.Forms.Panel pnlDetalle;
        private System.Windows.Forms.Label lblPasoDetalle;
        private System.Windows.Forms.DataGridView grilla;
        private System.Windows.Forms.Label lblTitManoObra;
        private System.Windows.Forms.Label lblManoObra;
        private System.Windows.Forms.Label lblTitTotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Panel pnlRespuesta;
        private System.Windows.Forms.Label lblPasoRespuesta;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Button btnRechazar;
        private System.Windows.Forms.Button btnAprobar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
