namespace UI
{
    partial class FrmEntregaEquipo
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
            pnlDetalle = new Panel();
            lblPasoDetalle = new Label();
            lblMotivo = new Label();
            grilla = new DataGridView();
            lblTitManoObra = new Label();
            lblManoObra = new Label();
            lblTitTotal = new Label();
            lblTotal = new Label();
            lblGarantia = new Label();
            pnlPago = new Panel();
            lblPasoPago = new Label();
            lblTipo = new Label();
            cboTipo = new ComboBox();
            lblTitular = new Label();
            txtTitular = new TextBox();
            lblNumero = new Label();
            txtNumero = new TextBox();
            lblVencimiento = new Label();
            txtVencimiento = new TextBox();
            lblCvv = new Label();
            txtCvv = new TextBox();
            lblAvisoTarjeta = new Label();
            btnEntregar = new Button();
            btnCancelar = new Button();
            pnlTopbar.SuspendLayout();
            pnlOT.SuspendLayout();
            pnlDetalle.SuspendLayout();
            pnlPago.SuspendLayout();
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
            lblTitulo.Text = "ENTREGA DE EQUIPO";
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
            // pnlDetalle
            // 
            pnlDetalle.BackColor = Color.FromArgb(25, 37, 53);
            pnlDetalle.Controls.Add(lblPasoDetalle);
            pnlDetalle.Controls.Add(lblMotivo);
            pnlDetalle.Controls.Add(grilla);
            pnlDetalle.Controls.Add(lblTitManoObra);
            pnlDetalle.Controls.Add(lblManoObra);
            pnlDetalle.Controls.Add(lblTitTotal);
            pnlDetalle.Controls.Add(lblTotal);
            pnlDetalle.Controls.Add(lblGarantia);
            pnlDetalle.Location = new Point(24, 164);
            pnlDetalle.Name = "pnlDetalle";
            pnlDetalle.Padding = new Padding(16);
            pnlDetalle.Size = new Size(672, 232);
            pnlDetalle.TabIndex = 2;
            // 
            // lblPasoDetalle
            // 
            lblPasoDetalle.AutoSize = true;
            lblPasoDetalle.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblPasoDetalle.ForeColor = Color.FromArgb(71, 85, 105);
            lblPasoDetalle.Location = new Point(16, 12);
            lblPasoDetalle.Name = "lblPasoDetalle";
            lblPasoDetalle.TabIndex = 0;
            lblPasoDetalle.Text = "1  ·  DETALLE DEL COBRO";
            // 
            // lblMotivo
            // 
            lblMotivo.AutoSize = false;
            lblMotivo.Font = new Font("Segoe UI", 9.5F);
            lblMotivo.ForeColor = Color.FromArgb(226, 232, 240);
            lblMotivo.Location = new Point(16, 34);
            lblMotivo.Name = "lblMotivo";
            lblMotivo.Size = new Size(640, 44);
            lblMotivo.TabIndex = 1;
            lblMotivo.Text = "-";
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
            grilla.Size = new Size(640, 110);
            grilla.TabIndex = 2;
            // 
            // lblTitManoObra
            // 
            lblTitManoObra.AutoSize = false;
            lblTitManoObra.Font = new Font("Segoe UI", 9F);
            lblTitManoObra.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitManoObra.Location = new Point(326, 154);
            lblTitManoObra.Name = "lblTitManoObra";
            lblTitManoObra.Size = new Size(180, 20);
            lblTitManoObra.TabIndex = 3;
            lblTitManoObra.Text = "Mano de obra";
            lblTitManoObra.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblManoObra
            // 
            lblManoObra.AutoSize = false;
            lblManoObra.Font = new Font("Segoe UI", 9.5F);
            lblManoObra.ForeColor = Color.FromArgb(226, 232, 240);
            lblManoObra.Location = new Point(516, 154);
            lblManoObra.Name = "lblManoObra";
            lblManoObra.Size = new Size(140, 20);
            lblManoObra.TabIndex = 4;
            lblManoObra.Text = "$ 0,00";
            lblManoObra.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTitTotal
            // 
            lblTitTotal.AutoSize = false;
            lblTitTotal.Font = new Font("Segoe UI", 9F);
            lblTitTotal.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitTotal.Location = new Point(326, 188);
            lblTitTotal.Name = "lblTitTotal";
            lblTitTotal.Size = new Size(180, 24);
            lblTitTotal.TabIndex = 5;
            lblTitTotal.Text = "TOTAL A COBRAR";
            lblTitTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = false;
            lblTotal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(226, 232, 240);
            lblTotal.Location = new Point(496, 186);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(160, 26);
            lblTotal.TabIndex = 6;
            lblTotal.Text = "$ 0,00";
            lblTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblGarantia
            // 
            lblGarantia.AutoSize = false;
            lblGarantia.Font = new Font("Segoe UI", 9F);
            lblGarantia.ForeColor = Color.FromArgb(74, 222, 128);
            lblGarantia.Location = new Point(16, 156);
            lblGarantia.Name = "lblGarantia";
            lblGarantia.Size = new Size(330, 60);
            lblGarantia.TabIndex = 7;
            lblGarantia.Text = "-";
            // 
            // pnlPago
            // 
            pnlPago.BackColor = Color.FromArgb(25, 37, 53);
            pnlPago.Controls.Add(lblPasoPago);
            pnlPago.Controls.Add(lblTipo);
            pnlPago.Controls.Add(cboTipo);
            pnlPago.Controls.Add(lblTitular);
            pnlPago.Controls.Add(txtTitular);
            pnlPago.Controls.Add(lblNumero);
            pnlPago.Controls.Add(txtNumero);
            pnlPago.Controls.Add(lblVencimiento);
            pnlPago.Controls.Add(txtVencimiento);
            pnlPago.Controls.Add(lblCvv);
            pnlPago.Controls.Add(txtCvv);
            pnlPago.Controls.Add(lblAvisoTarjeta);
            pnlPago.Location = new Point(24, 408);
            pnlPago.Name = "pnlPago";
            pnlPago.Padding = new Padding(16);
            pnlPago.Size = new Size(672, 150);
            pnlPago.TabIndex = 3;
            // 
            // lblPasoPago
            // 
            lblPasoPago.AutoSize = true;
            lblPasoPago.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblPasoPago.ForeColor = Color.FromArgb(71, 85, 105);
            lblPasoPago.Location = new Point(16, 12);
            lblPasoPago.Name = "lblPasoPago";
            lblPasoPago.TabIndex = 0;
            lblPasoPago.Text = "2  ·  PAGO CON TARJETA";
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Font = new Font("Segoe UI", 9F);
            lblTipo.ForeColor = Color.FromArgb(148, 163, 184);
            lblTipo.Location = new Point(16, 44);
            lblTipo.Name = "lblTipo";
            lblTipo.TabIndex = 1;
            lblTipo.Text = "Tipo";
            // 
            // cboTipo
            // 
            cboTipo.BackColor = Color.FromArgb(21, 32, 48);
            cboTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipo.FlatStyle = FlatStyle.Flat;
            cboTipo.Font = new Font("Segoe UI", 9.5F);
            cboTipo.ForeColor = Color.FromArgb(226, 232, 240);
            cboTipo.Location = new Point(110, 40);
            cboTipo.Name = "cboTipo";
            cboTipo.Size = new Size(150, 25);
            cboTipo.TabIndex = 2;
            // 
            // lblTitular
            // 
            lblTitular.AutoSize = true;
            lblTitular.Font = new Font("Segoe UI", 9F);
            lblTitular.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitular.Location = new Point(290, 44);
            lblTitular.Name = "lblTitular";
            lblTitular.TabIndex = 3;
            lblTitular.Text = "Titular";
            // 
            // txtTitular
            // 
            txtTitular.BackColor = Color.FromArgb(21, 32, 48);
            txtTitular.BorderStyle = BorderStyle.FixedSingle;
            txtTitular.Font = new Font("Segoe UI", 9.5F);
            txtTitular.ForeColor = Color.FromArgb(226, 232, 240);
            txtTitular.MaxLength = 50;
            txtTitular.Location = new Point(360, 41);
            txtTitular.Name = "txtTitular";
            txtTitular.Size = new Size(296, 24);
            txtTitular.TabIndex = 4;
            // 
            // lblNumero
            // 
            lblNumero.AutoSize = true;
            lblNumero.Font = new Font("Segoe UI", 9F);
            lblNumero.ForeColor = Color.FromArgb(148, 163, 184);
            lblNumero.Location = new Point(16, 84);
            lblNumero.Name = "lblNumero";
            lblNumero.TabIndex = 5;
            lblNumero.Text = "Número";
            // 
            // txtNumero
            // 
            txtNumero.BackColor = Color.FromArgb(21, 32, 48);
            txtNumero.BorderStyle = BorderStyle.FixedSingle;
            txtNumero.Font = new Font("Segoe UI", 9.5F);
            txtNumero.ForeColor = Color.FromArgb(226, 232, 240);
            txtNumero.MaxLength = 23;
            txtNumero.Location = new Point(110, 81);
            txtNumero.Name = "txtNumero";
            txtNumero.Size = new Size(220, 24);
            txtNumero.TabIndex = 6;
            txtNumero.TextChanged += txtNumero_TextChanged;
            // 
            // lblVencimiento
            // 
            lblVencimiento.AutoSize = true;
            lblVencimiento.Font = new Font("Segoe UI", 9F);
            lblVencimiento.ForeColor = Color.FromArgb(148, 163, 184);
            lblVencimiento.Location = new Point(350, 84);
            lblVencimiento.Name = "lblVencimiento";
            lblVencimiento.TabIndex = 7;
            lblVencimiento.Text = "Venc. (MM/AA)";
            // 
            // txtVencimiento
            // 
            txtVencimiento.BackColor = Color.FromArgb(21, 32, 48);
            txtVencimiento.BorderStyle = BorderStyle.FixedSingle;
            txtVencimiento.Font = new Font("Segoe UI", 9.5F);
            txtVencimiento.ForeColor = Color.FromArgb(226, 232, 240);
            txtVencimiento.MaxLength = 5;
            txtVencimiento.Location = new Point(452, 81);
            txtVencimiento.Name = "txtVencimiento";
            txtVencimiento.Size = new Size(70, 24);
            txtVencimiento.TabIndex = 8;
            // 
            // lblCvv
            // 
            lblCvv.AutoSize = true;
            lblCvv.Font = new Font("Segoe UI", 9F);
            lblCvv.ForeColor = Color.FromArgb(148, 163, 184);
            lblCvv.Location = new Point(546, 84);
            lblCvv.Name = "lblCvv";
            lblCvv.TabIndex = 9;
            lblCvv.Text = "CVV";
            // 
            // txtCvv
            // 
            txtCvv.BackColor = Color.FromArgb(21, 32, 48);
            txtCvv.BorderStyle = BorderStyle.FixedSingle;
            txtCvv.Font = new Font("Segoe UI", 9.5F);
            txtCvv.ForeColor = Color.FromArgb(226, 232, 240);
            txtCvv.MaxLength = 4;
            txtCvv.UseSystemPasswordChar = true;
            txtCvv.Location = new Point(586, 81);
            txtCvv.Name = "txtCvv";
            txtCvv.Size = new Size(70, 24);
            txtCvv.TabIndex = 10;
            // 
            // lblAvisoTarjeta
            // 
            lblAvisoTarjeta.AutoSize = false;
            lblAvisoTarjeta.Font = new Font("Segoe UI", 9F);
            lblAvisoTarjeta.ForeColor = Color.FromArgb(71, 85, 105);
            lblAvisoTarjeta.Location = new Point(16, 120);
            lblAvisoTarjeta.Name = "lblAvisoTarjeta";
            lblAvisoTarjeta.Size = new Size(640, 20);
            lblAvisoTarjeta.TabIndex = 11;
            lblAvisoTarjeta.Text = "Los datos de la tarjeta no se guardan: el comprobante de cobro registra solo el tipo y los últimos 4 dígitos.";
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
            btnEntregar.Location = new Point(308, 574);
            btnEntregar.Name = "btnEntregar";
            btnEntregar.Size = new Size(220, 42);
            btnEntregar.TabIndex = 4;
            btnEntregar.Text = "Cobrar y entregar";
            btnEntregar.Click += btnEntregar_Click;
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
            btnCancelar.Location = new Point(540, 574);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(156, 42);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // FrmEntregaEquipo
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(720, 632);
            Controls.Add(btnCancelar);
            Controls.Add(btnEntregar);
            Controls.Add(pnlPago);
            Controls.Add(pnlDetalle);
            Controls.Add(pnlOT);
            Controls.Add(pnlTopbar);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmEntregaEquipo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Entrega de Equipo";
            Load += FrmEntregaEquipo_Load;
            pnlTopbar.ResumeLayout(false);
            pnlTopbar.PerformLayout();
            pnlOT.ResumeLayout(false);
            pnlOT.PerformLayout();
            pnlDetalle.ResumeLayout(false);
            pnlDetalle.PerformLayout();
            pnlPago.ResumeLayout(false);
            pnlPago.PerformLayout();
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
        private System.Windows.Forms.Panel pnlDetalle;
        private System.Windows.Forms.Label lblPasoDetalle;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.DataGridView grilla;
        private System.Windows.Forms.Label lblTitManoObra;
        private System.Windows.Forms.Label lblManoObra;
        private System.Windows.Forms.Label lblTitTotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblGarantia;
        private System.Windows.Forms.Panel pnlPago;
        private System.Windows.Forms.Label lblPasoPago;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cboTipo;
        private System.Windows.Forms.Label lblTitular;
        private System.Windows.Forms.TextBox txtTitular;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label lblVencimiento;
        private System.Windows.Forms.TextBox txtVencimiento;
        private System.Windows.Forms.Label lblCvv;
        private System.Windows.Forms.TextBox txtCvv;
        private System.Windows.Forms.Label lblAvisoTarjeta;
        private System.Windows.Forms.Button btnEntregar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
