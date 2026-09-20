namespace UI
{
    partial class FrmRepararInconsistencia
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
            lblTitulo = new Label();
            pnlContenido = new Panel();
            grillaInconsistencias = new DataGridView();
            richTextBox1 = new RichTextBox();
            pnlBotones = new Panel();
            btnSalir = new Button();
            btnRealizarRespaldo = new Button();
            btnRestaurarDV = new Button();
            pnlTopbar.SuspendLayout();
            pnlContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grillaInconsistencias).BeginInit();
            pnlBotones.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTopbar
            // 
            pnlTopbar.BackColor = Color.FromArgb(21, 32, 48);
            pnlTopbar.Controls.Add(lblTitulo);
            pnlTopbar.Dock = DockStyle.Top;
            pnlTopbar.Location = new Point(0, 0);
            pnlTopbar.Name = "pnlTopbar";
            pnlTopbar.Size = new Size(900, 48);
            pnlTopbar.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(226, 232, 240);
            lblTitulo.Location = new Point(16, 14);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(213, 20);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "REPARAR INCONSISTENCIAS";
            // 
            // pnlContenido
            // 
            pnlContenido.BackColor = Color.FromArgb(30, 42, 58);
            pnlContenido.Controls.Add(grillaInconsistencias);
            pnlContenido.Controls.Add(richTextBox1);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(0, 48);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Padding = new Padding(24);
            pnlContenido.Size = new Size(900, 422);
            pnlContenido.TabIndex = 1;
            // 
            // grillaInconsistencias
            // 
            grillaInconsistencias.AllowUserToAddRows = false;
            grillaInconsistencias.AllowUserToDeleteRows = false;
            grillaInconsistencias.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            grillaInconsistencias.BackgroundColor = Color.FromArgb(25, 37, 53);
            grillaInconsistencias.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(21, 32, 48);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(148, 163, 184);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            grillaInconsistencias.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            grillaInconsistencias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(25, 37, 53);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(226, 232, 240);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(45, 60, 82);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(226, 232, 240);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            grillaInconsistencias.DefaultCellStyle = dataGridViewCellStyle2;
            grillaInconsistencias.EnableHeadersVisualStyles = false;
            grillaInconsistencias.GridColor = Color.FromArgb(45, 60, 82);
            grillaInconsistencias.Location = new Point(24, 24);
            grillaInconsistencias.Name = "grillaInconsistencias";
            grillaInconsistencias.ReadOnly = true;
            grillaInconsistencias.RowHeadersVisible = false;
            grillaInconsistencias.Size = new Size(380, 374);
            grillaInconsistencias.TabIndex = 0;
            grillaInconsistencias.SelectionChanged += grillaInconsistencias_SelectionChanged;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox1.BackColor = Color.FromArgb(21, 32, 48);
            richTextBox1.BorderStyle = BorderStyle.FixedSingle;
            richTextBox1.Font = new Font("Segoe UI", 9.5F);
            richTextBox1.ForeColor = Color.FromArgb(226, 232, 240);
            richTextBox1.Location = new Point(420, 24);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(456, 374);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // pnlBotones
            // 
            pnlBotones.BackColor = Color.FromArgb(25, 37, 53);
            pnlBotones.Controls.Add(btnSalir);
            pnlBotones.Controls.Add(btnRealizarRespaldo);
            pnlBotones.Controls.Add(btnRestaurarDV);
            pnlBotones.Dock = DockStyle.Bottom;
            pnlBotones.Location = new Point(0, 470);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Padding = new Padding(24, 8, 24, 8);
            pnlBotones.Size = new Size(900, 90);
            pnlBotones.TabIndex = 2;
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
            btnSalir.Location = new Point(708, 26);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(168, 38);
            btnSalir.TabIndex = 2;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnRealizarRespaldo
            // 
            btnRealizarRespaldo.BackColor = Color.FromArgb(45, 60, 82);
            btnRealizarRespaldo.Cursor = Cursors.Hand;
            btnRealizarRespaldo.FlatAppearance.BorderSize = 0;
            btnRealizarRespaldo.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnRealizarRespaldo.FlatStyle = FlatStyle.Flat;
            btnRealizarRespaldo.Font = new Font("Segoe UI", 9.5F);
            btnRealizarRespaldo.ForeColor = Color.FromArgb(226, 232, 240);
            btnRealizarRespaldo.Location = new Point(16, 26);
            btnRealizarRespaldo.Name = "btnRealizarRespaldo";
            btnRealizarRespaldo.Size = new Size(198, 38);
            btnRealizarRespaldo.TabIndex = 0;
            btnRealizarRespaldo.Text = "Realizar respaldo";
            btnRealizarRespaldo.UseVisualStyleBackColor = false;
            btnRealizarRespaldo.Click += btnRealizarRespaldo_Click;
            // 
            // btnRestaurarDV
            // 
            btnRestaurarDV.BackColor = Color.FromArgb(45, 60, 82);
            btnRestaurarDV.Cursor = Cursors.Hand;
            btnRestaurarDV.FlatAppearance.BorderSize = 0;
            btnRestaurarDV.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnRestaurarDV.FlatStyle = FlatStyle.Flat;
            btnRestaurarDV.Font = new Font("Segoe UI", 9.5F);
            btnRestaurarDV.ForeColor = Color.FromArgb(226, 232, 240);
            btnRestaurarDV.Location = new Point(233, 26);
            btnRestaurarDV.Name = "btnRestaurarDV";
            btnRestaurarDV.Size = new Size(198, 38);
            btnRestaurarDV.TabIndex = 1;
            btnRestaurarDV.Text = "Recalcular DV";
            btnRestaurarDV.UseVisualStyleBackColor = false;
            btnRestaurarDV.Click += btnRestaurarDV_Click;
            // 
            // FrmRepararInconsistencia
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(900, 560);
            Controls.Add(pnlContenido);
            Controls.Add(pnlBotones);
            Controls.Add(pnlTopbar);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmRepararInconsistencia";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reparar Inconsistencias";
            Load += FrmRepararInconsistencia_Load;
            pnlTopbar.ResumeLayout(false);
            pnlTopbar.PerformLayout();
            pnlContenido.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)grillaInconsistencias).EndInit();
            pnlBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlTopbar;
        private Label lblTitulo;
        private Panel pnlContenido;
        private Panel pnlBotones;
        private Button btnRealizarRespaldo;
        private Button btnRestaurarDV;
        private DataGridView grillaInconsistencias;
        private RichTextBox richTextBox1;
        private Button btnSalir;
    }
}