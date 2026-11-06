namespace UI
{
    partial class FrmReparacionEquipo_382QA
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
            lblDetalleDiagnostico = new Label();
            pnlRepuestos = new Panel();
            lblPasoRepuestos = new Label();
            lblAyuda = new Label();
            grilla = new DataGridView();
            lblAvisoStock = new Label();
            btnConfirmar = new Button();
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
            pnlTopbar.Size = new Size(760, 48);
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
            lblTitulo.Text = "REPARACIÓN DE EQUIPO";
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
            pnlOT.Size = new Size(712, 96);
            pnlOT.TabIndex = 1;
            // 
            // lblOT
            // 
            lblOT.AutoSize = false;
            lblOT.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblOT.ForeColor = Color.FromArgb(226, 232, 240);
            lblOT.Location = new Point(16, 12);
            lblOT.Name = "lblOT";
            lblOT.Size = new Size(680, 24);
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
            lblEquipo.Size = new Size(680, 20);
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
            lblFalla.Size = new Size(680, 20);
            lblFalla.TabIndex = 2;
            lblFalla.Text = "-";
            // 
            // pnlDiagnostico
            // 
            pnlDiagnostico.BackColor = Color.FromArgb(25, 37, 53);
            pnlDiagnostico.Controls.Add(lblPasoDiagnostico);
            pnlDiagnostico.Controls.Add(lblDetalleDiagnostico);
            pnlDiagnostico.Location = new Point(24, 172);
            pnlDiagnostico.Name = "pnlDiagnostico";
            pnlDiagnostico.Padding = new Padding(16);
            pnlDiagnostico.Size = new Size(712, 92);
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
            lblPasoDiagnostico.Text = "1  ·  DIAGNÓSTICO APROBADO";
            // 
            // lblDetalleDiagnostico
            // 
            lblDetalleDiagnostico.AutoSize = false;
            lblDetalleDiagnostico.Font = new Font("Segoe UI", 9.5F);
            lblDetalleDiagnostico.ForeColor = Color.FromArgb(226, 232, 240);
            lblDetalleDiagnostico.Location = new Point(16, 34);
            lblDetalleDiagnostico.Name = "lblDetalleDiagnostico";
            lblDetalleDiagnostico.Size = new Size(680, 48);
            lblDetalleDiagnostico.TabIndex = 1;
            lblDetalleDiagnostico.Text = "-";
            // 
            // pnlRepuestos
            // 
            pnlRepuestos.BackColor = Color.FromArgb(25, 37, 53);
            pnlRepuestos.Controls.Add(lblPasoRepuestos);
            pnlRepuestos.Controls.Add(lblAyuda);
            pnlRepuestos.Controls.Add(grilla);
            pnlRepuestos.Controls.Add(lblAvisoStock);
            pnlRepuestos.Location = new Point(24, 276);
            pnlRepuestos.Name = "pnlRepuestos";
            pnlRepuestos.Padding = new Padding(16);
            pnlRepuestos.Size = new Size(712, 250);
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
            lblPasoRepuestos.Text = "2  ·  REPUESTOS UTILIZADOS";
            // 
            // lblAyuda
            // 
            lblAyuda.AutoSize = false;
            lblAyuda.Font = new Font("Segoe UI", 9F);
            lblAyuda.ForeColor = Color.FromArgb(148, 163, 184);
            lblAyuda.Location = new Point(16, 32);
            lblAyuda.Name = "lblAyuda";
            lblAyuda.Size = new Size(680, 20);
            lblAyuda.TabIndex = 1;
            lblAyuda.Text = "Corregí la cantidad utilizada si usaste menos de lo aprobado.";
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
            grilla.Location = new Point(16, 58);
            grilla.Name = "grilla";
            grilla.Size = new Size(680, 150);
            grilla.TabIndex = 2;
            // 
            // lblAvisoStock
            // 
            lblAvisoStock.AutoSize = false;
            lblAvisoStock.Font = new Font("Segoe UI", 9F);
            lblAvisoStock.ForeColor = Color.FromArgb(251, 191, 36);
            lblAvisoStock.Location = new Point(16, 218);
            lblAvisoStock.Name = "lblAvisoStock";
            lblAvisoStock.Size = new Size(680, 20);
            lblAvisoStock.TabIndex = 3;
            lblAvisoStock.Text = "";
            // 
            // btnConfirmar
            // 
            btnConfirmar.BackColor = Color.FromArgb(14, 116, 144);
            btnConfirmar.Cursor = Cursors.Hand;
            btnConfirmar.FlatAppearance.BorderSize = 0;
            btnConfirmar.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 94, 117);
            btnConfirmar.FlatStyle = FlatStyle.Flat;
            btnConfirmar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnConfirmar.ForeColor = Color.FromArgb(226, 232, 240);
            btnConfirmar.UseVisualStyleBackColor = false;
            btnConfirmar.Location = new Point(348, 542);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(220, 42);
            btnConfirmar.TabIndex = 4;
            btnConfirmar.Text = "Confirmar reparación";
            btnConfirmar.Click += btnConfirmar_Click;
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
            btnCancelar.Location = new Point(580, 542);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(156, 42);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // FrmReparacionEquipo_382QA
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(760, 604);
            Controls.Add(btnCancelar);
            Controls.Add(btnConfirmar);
            Controls.Add(pnlRepuestos);
            Controls.Add(pnlDiagnostico);
            Controls.Add(pnlOT);
            Controls.Add(pnlTopbar);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmReparacionEquipo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reparación de Equipo";
            Load += FrmReparacionEquipo_Load;
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
        private System.Windows.Forms.Label lblDetalleDiagnostico;
        private System.Windows.Forms.Panel pnlRepuestos;
        private System.Windows.Forms.Label lblPasoRepuestos;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.DataGridView grilla;
        private System.Windows.Forms.Label lblAvisoStock;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
