namespace UI
{
    partial class FrmEvaluacionReclamo
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
            pnlTopbar = new Panel();
            lblTitulo = new Label();
            pnlReclamo = new Panel();
            lblPasoReclamo = new Label();
            lblTitReclamo = new Label();
            lblReclamo = new Label();
            lblTitCliente = new Label();
            lblCliente = new Label();
            lblTitEquipo = new Label();
            lblEquipo = new Label();
            lblTitMotivo = new Label();
            lblMotivo = new Label();
            lblTitDescripcion = new Label();
            lblDescripcion = new Label();
            lblTitGarantia = new Label();
            lblGarantia = new Label();
            pnlReparacion = new Panel();
            lblPasoReparacion = new Label();
            lblTitFalla = new Label();
            lblFalla = new Label();
            lblTitDiagnostico = new Label();
            lblDiagnostico = new Label();
            lblTitRepuestos = new Label();
            txtRepuestos = new TextBox();
            pnlEvaluacion = new Panel();
            lblPasoEvaluacion = new Label();
            lblCubierta = new Label();
            rbCubiertaSi = new RadioButton();
            rbCubiertaNo = new RadioButton();
            lblDetalle = new Label();
            txtDetalle = new TextBox();
            lblContador = new Label();
            btnConfirmar = new Button();
            btnCancelar = new Button();
            pnlTopbar.SuspendLayout();
            pnlReclamo.SuspendLayout();
            pnlReparacion.SuspendLayout();
            pnlEvaluacion.SuspendLayout();
            SuspendLayout();
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
            lblTitulo.Text = "EVALUACIÓN DE RECLAMO POR GARANTÍA";
            lblTitulo.Name = "lblTitulo";
            lblTitulo.TabIndex = 1;
            //
            // pnlReclamo
            //
            pnlReclamo.BackColor = Color.FromArgb(25, 37, 53);
            pnlReclamo.Location = new Point(24, 64);
            pnlReclamo.Padding = new Padding(16);
            pnlReclamo.Size = new Size(592, 216);
            pnlReclamo.Name = "pnlReclamo";
            pnlReclamo.TabIndex = 2;
            pnlReclamo.Controls.Add(lblPasoReclamo);
            pnlReclamo.Controls.Add(lblTitReclamo);
            pnlReclamo.Controls.Add(lblReclamo);
            pnlReclamo.Controls.Add(lblTitCliente);
            pnlReclamo.Controls.Add(lblCliente);
            pnlReclamo.Controls.Add(lblTitEquipo);
            pnlReclamo.Controls.Add(lblEquipo);
            pnlReclamo.Controls.Add(lblTitMotivo);
            pnlReclamo.Controls.Add(lblMotivo);
            pnlReclamo.Controls.Add(lblTitDescripcion);
            pnlReclamo.Controls.Add(lblDescripcion);
            pnlReclamo.Controls.Add(lblTitGarantia);
            pnlReclamo.Controls.Add(lblGarantia);
            //
            // lblPasoReclamo
            //
            lblPasoReclamo.AutoSize = true;
            lblPasoReclamo.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblPasoReclamo.ForeColor = Color.FromArgb(71, 85, 105);
            lblPasoReclamo.Location = new Point(16, 12);
            lblPasoReclamo.Text = "1  ·  RECLAMO";
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
            // lblTitCliente
            //
            lblTitCliente.AutoSize = true;
            lblTitCliente.Font = new Font("Segoe UI", 9F);
            lblTitCliente.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitCliente.Location = new Point(16, 64);
            lblTitCliente.Text = "Cliente";
            lblTitCliente.Name = "lblTitCliente";
            lblTitCliente.TabIndex = 6;
            //
            // lblCliente
            //
            lblCliente.AutoSize = false;
            lblCliente.Font = new Font("Segoe UI", 9.5F);
            lblCliente.ForeColor = Color.FromArgb(226, 232, 240);
            lblCliente.Location = new Point(130, 62);
            lblCliente.Size = new Size(446, 22);
            lblCliente.Text = "-";
            lblCliente.Name = "lblCliente";
            lblCliente.TabIndex = 7;
            //
            // lblTitEquipo
            //
            lblTitEquipo.AutoSize = true;
            lblTitEquipo.Font = new Font("Segoe UI", 9F);
            lblTitEquipo.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitEquipo.Location = new Point(16, 88);
            lblTitEquipo.Text = "Equipo";
            lblTitEquipo.Name = "lblTitEquipo";
            lblTitEquipo.TabIndex = 8;
            //
            // lblEquipo
            //
            lblEquipo.AutoSize = false;
            lblEquipo.Font = new Font("Segoe UI", 9.5F);
            lblEquipo.ForeColor = Color.FromArgb(226, 232, 240);
            lblEquipo.Location = new Point(130, 86);
            lblEquipo.Size = new Size(446, 22);
            lblEquipo.Text = "-";
            lblEquipo.Name = "lblEquipo";
            lblEquipo.TabIndex = 9;
            //
            // lblTitMotivo
            //
            lblTitMotivo.AutoSize = true;
            lblTitMotivo.Font = new Font("Segoe UI", 9F);
            lblTitMotivo.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitMotivo.Location = new Point(16, 112);
            lblTitMotivo.Text = "Motivo";
            lblTitMotivo.Name = "lblTitMotivo";
            lblTitMotivo.TabIndex = 10;
            //
            // lblMotivo
            //
            lblMotivo.AutoSize = false;
            lblMotivo.Font = new Font("Segoe UI", 9.5F);
            lblMotivo.ForeColor = Color.FromArgb(226, 232, 240);
            lblMotivo.Location = new Point(130, 110);
            lblMotivo.Size = new Size(446, 22);
            lblMotivo.Text = "-";
            lblMotivo.Name = "lblMotivo";
            lblMotivo.TabIndex = 11;
            //
            // lblTitDescripcion
            //
            lblTitDescripcion.AutoSize = true;
            lblTitDescripcion.Font = new Font("Segoe UI", 9F);
            lblTitDescripcion.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitDescripcion.Location = new Point(16, 136);
            lblTitDescripcion.Text = "Descripción";
            lblTitDescripcion.Name = "lblTitDescripcion";
            lblTitDescripcion.TabIndex = 12;
            //
            // lblDescripcion
            //
            lblDescripcion.AutoSize = false;
            lblDescripcion.Font = new Font("Segoe UI", 9.5F);
            lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240);
            lblDescripcion.Location = new Point(130, 134);
            lblDescripcion.Size = new Size(446, 52);
            lblDescripcion.Text = "-";
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.TabIndex = 13;
            //
            // lblTitGarantia
            //
            lblTitGarantia.AutoSize = true;
            lblTitGarantia.Font = new Font("Segoe UI", 9F);
            lblTitGarantia.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitGarantia.Location = new Point(16, 192);
            lblTitGarantia.Text = "Garantía";
            lblTitGarantia.Name = "lblTitGarantia";
            lblTitGarantia.TabIndex = 14;
            //
            // lblGarantia
            //
            lblGarantia.AutoSize = false;
            lblGarantia.Font = new Font("Segoe UI", 9.5F);
            lblGarantia.ForeColor = Color.FromArgb(148, 163, 184);
            lblGarantia.Location = new Point(130, 190);
            lblGarantia.Size = new Size(446, 20);
            lblGarantia.Text = "-";
            lblGarantia.Name = "lblGarantia";
            lblGarantia.TabIndex = 15;
            //
            // pnlReparacion
            //
            pnlReparacion.BackColor = Color.FromArgb(25, 37, 53);
            pnlReparacion.Location = new Point(24, 292);
            pnlReparacion.Padding = new Padding(16);
            pnlReparacion.Size = new Size(592, 184);
            pnlReparacion.Name = "pnlReparacion";
            pnlReparacion.TabIndex = 16;
            pnlReparacion.Controls.Add(lblPasoReparacion);
            pnlReparacion.Controls.Add(lblTitFalla);
            pnlReparacion.Controls.Add(lblFalla);
            pnlReparacion.Controls.Add(lblTitDiagnostico);
            pnlReparacion.Controls.Add(lblDiagnostico);
            pnlReparacion.Controls.Add(lblTitRepuestos);
            pnlReparacion.Controls.Add(txtRepuestos);
            //
            // lblPasoReparacion
            //
            lblPasoReparacion.AutoSize = true;
            lblPasoReparacion.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblPasoReparacion.ForeColor = Color.FromArgb(71, 85, 105);
            lblPasoReparacion.Location = new Point(16, 12);
            lblPasoReparacion.Text = "2  ·  REPARACIÓN ORIGINAL";
            lblPasoReparacion.Name = "lblPasoReparacion";
            lblPasoReparacion.TabIndex = 17;
            //
            // lblTitFalla
            //
            lblTitFalla.AutoSize = true;
            lblTitFalla.Font = new Font("Segoe UI", 9F);
            lblTitFalla.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitFalla.Location = new Point(16, 40);
            lblTitFalla.Text = "Falla de la OT";
            lblTitFalla.Name = "lblTitFalla";
            lblTitFalla.TabIndex = 18;
            //
            // lblFalla
            //
            lblFalla.AutoSize = false;
            lblFalla.Font = new Font("Segoe UI", 9.5F);
            lblFalla.ForeColor = Color.FromArgb(226, 232, 240);
            lblFalla.Location = new Point(130, 38);
            lblFalla.Size = new Size(446, 22);
            lblFalla.Text = "-";
            lblFalla.Name = "lblFalla";
            lblFalla.TabIndex = 19;
            //
            // lblTitDiagnostico
            //
            lblTitDiagnostico.AutoSize = true;
            lblTitDiagnostico.Font = new Font("Segoe UI", 9F);
            lblTitDiagnostico.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitDiagnostico.Location = new Point(16, 64);
            lblTitDiagnostico.Text = "Diagnóstico";
            lblTitDiagnostico.Name = "lblTitDiagnostico";
            lblTitDiagnostico.TabIndex = 20;
            //
            // lblDiagnostico
            //
            lblDiagnostico.AutoSize = false;
            lblDiagnostico.Font = new Font("Segoe UI", 9.5F);
            lblDiagnostico.ForeColor = Color.FromArgb(226, 232, 240);
            lblDiagnostico.Location = new Point(130, 62);
            lblDiagnostico.Size = new Size(446, 40);
            lblDiagnostico.Text = "-";
            lblDiagnostico.Name = "lblDiagnostico";
            lblDiagnostico.TabIndex = 21;
            //
            // lblTitRepuestos
            //
            lblTitRepuestos.AutoSize = true;
            lblTitRepuestos.Font = new Font("Segoe UI", 9F);
            lblTitRepuestos.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitRepuestos.Location = new Point(16, 110);
            lblTitRepuestos.Text = "Repuestos";
            lblTitRepuestos.Name = "lblTitRepuestos";
            lblTitRepuestos.TabIndex = 22;
            //
            // txtRepuestos
            //
            txtRepuestos.BackColor = Color.FromArgb(21, 32, 48);
            txtRepuestos.BorderStyle = BorderStyle.FixedSingle;
            txtRepuestos.Font = new Font("Segoe UI", 9.5F);
            txtRepuestos.ForeColor = Color.FromArgb(226, 232, 240);
            txtRepuestos.Multiline = true;
            txtRepuestos.ReadOnly = true;
            txtRepuestos.ScrollBars = ScrollBars.Vertical;
            txtRepuestos.TabStop = false;
            txtRepuestos.Location = new Point(130, 108);
            txtRepuestos.Size = new Size(446, 62);
            txtRepuestos.Name = "txtRepuestos";
            txtRepuestos.TabIndex = 23;
            //
            // pnlEvaluacion
            //
            pnlEvaluacion.BackColor = Color.FromArgb(25, 37, 53);
            pnlEvaluacion.Location = new Point(24, 488);
            pnlEvaluacion.Padding = new Padding(16);
            pnlEvaluacion.Size = new Size(592, 184);
            pnlEvaluacion.Name = "pnlEvaluacion";
            pnlEvaluacion.TabIndex = 24;
            pnlEvaluacion.Controls.Add(lblPasoEvaluacion);
            pnlEvaluacion.Controls.Add(lblCubierta);
            pnlEvaluacion.Controls.Add(rbCubiertaSi);
            pnlEvaluacion.Controls.Add(rbCubiertaNo);
            pnlEvaluacion.Controls.Add(lblDetalle);
            pnlEvaluacion.Controls.Add(txtDetalle);
            pnlEvaluacion.Controls.Add(lblContador);
            //
            // lblPasoEvaluacion
            //
            lblPasoEvaluacion.AutoSize = true;
            lblPasoEvaluacion.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblPasoEvaluacion.ForeColor = Color.FromArgb(71, 85, 105);
            lblPasoEvaluacion.Location = new Point(16, 12);
            lblPasoEvaluacion.Text = "3  ·  EVALUACIÓN";
            lblPasoEvaluacion.Name = "lblPasoEvaluacion";
            lblPasoEvaluacion.TabIndex = 25;
            //
            // lblCubierta
            //
            lblCubierta.AutoSize = true;
            lblCubierta.Font = new Font("Segoe UI", 9F);
            lblCubierta.ForeColor = Color.FromArgb(148, 163, 184);
            lblCubierta.Location = new Point(16, 42);
            lblCubierta.Text = "Falla cubierta";
            lblCubierta.Name = "lblCubierta";
            lblCubierta.TabIndex = 26;
            //
            // rbCubiertaSi
            //
            rbCubiertaSi.AutoSize = true;
            rbCubiertaSi.Font = new Font("Segoe UI", 9.5F);
            rbCubiertaSi.ForeColor = Color.FromArgb(226, 232, 240);
            rbCubiertaSi.Location = new Point(130, 40);
            rbCubiertaSi.Text = "Sí";
            rbCubiertaSi.UseVisualStyleBackColor = true;
            rbCubiertaSi.Name = "rbCubiertaSi";
            rbCubiertaSi.TabIndex = 27;
            //
            // rbCubiertaNo
            //
            rbCubiertaNo.AutoSize = true;
            rbCubiertaNo.Font = new Font("Segoe UI", 9.5F);
            rbCubiertaNo.ForeColor = Color.FromArgb(226, 232, 240);
            rbCubiertaNo.Location = new Point(210, 40);
            rbCubiertaNo.Text = "No";
            rbCubiertaNo.UseVisualStyleBackColor = true;
            rbCubiertaNo.Name = "rbCubiertaNo";
            rbCubiertaNo.TabIndex = 28;
            //
            // lblDetalle
            //
            lblDetalle.AutoSize = true;
            lblDetalle.Font = new Font("Segoe UI", 9F);
            lblDetalle.ForeColor = Color.FromArgb(148, 163, 184);
            lblDetalle.Location = new Point(16, 72);
            lblDetalle.Text = "Detalle de la evaluación";
            lblDetalle.Name = "lblDetalle";
            lblDetalle.TabIndex = 29;
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
            txtDetalle.Location = new Point(16, 94);
            txtDetalle.Size = new Size(560, 62);
            txtDetalle.Name = "txtDetalle";
            txtDetalle.TabIndex = 30;
            txtDetalle.TextChanged += txtDetalle_TextChanged;
            //
            // lblContador
            //
            lblContador.AutoSize = false;
            lblContador.Font = new Font("Segoe UI", 9F);
            lblContador.ForeColor = Color.FromArgb(148, 163, 184);
            lblContador.Location = new Point(496, 160);
            lblContador.Size = new Size(80, 18);
            lblContador.Text = "0 / 500";
            lblContador.TextAlign = ContentAlignment.MiddleRight;
            lblContador.Name = "lblContador";
            lblContador.TabIndex = 31;
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
            btnConfirmar.Location = new Point(248, 686);
            btnConfirmar.Size = new Size(200, 40);
            btnConfirmar.Text = "Registrar evaluación";
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.TabIndex = 32;
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
            btnCancelar.Location = new Point(460, 686);
            btnCancelar.Size = new Size(156, 40);
            btnCancelar.Text = "Cancelar";
            btnCancelar.Name = "btnCancelar";
            btnCancelar.TabIndex = 33;
            btnCancelar.Click += btnCancelar_Click;
            //
            // FrmEvaluacionReclamo
            //
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(640, 746);
            Controls.Add(btnCancelar);
            Controls.Add(btnConfirmar);
            Controls.Add(pnlEvaluacion);
            Controls.Add(pnlReparacion);
            Controls.Add(pnlReclamo);
            Controls.Add(pnlTopbar);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmEvaluacionReclamo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Evaluación de Reclamo";
            Load += FrmEvaluacionReclamo_Load;
            pnlTopbar.ResumeLayout(false);
            pnlTopbar.PerformLayout();
            pnlReclamo.ResumeLayout(false);
            pnlReclamo.PerformLayout();
            pnlReparacion.ResumeLayout(false);
            pnlReparacion.PerformLayout();
            pnlEvaluacion.ResumeLayout(false);
            pnlEvaluacion.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTopbar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlReclamo;
        private System.Windows.Forms.Label lblPasoReclamo;
        private System.Windows.Forms.Label lblTitReclamo;
        private System.Windows.Forms.Label lblReclamo;
        private System.Windows.Forms.Label lblTitCliente;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblTitEquipo;
        private System.Windows.Forms.Label lblEquipo;
        private System.Windows.Forms.Label lblTitMotivo;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.Label lblTitDescripcion;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblTitGarantia;
        private System.Windows.Forms.Label lblGarantia;
        private System.Windows.Forms.Panel pnlReparacion;
        private System.Windows.Forms.Label lblPasoReparacion;
        private System.Windows.Forms.Label lblTitFalla;
        private System.Windows.Forms.Label lblFalla;
        private System.Windows.Forms.Label lblTitDiagnostico;
        private System.Windows.Forms.Label lblDiagnostico;
        private System.Windows.Forms.Label lblTitRepuestos;
        private System.Windows.Forms.TextBox txtRepuestos;
        private System.Windows.Forms.Panel pnlEvaluacion;
        private System.Windows.Forms.Label lblPasoEvaluacion;
        private System.Windows.Forms.Label lblCubierta;
        private System.Windows.Forms.RadioButton rbCubiertaSi;
        private System.Windows.Forms.RadioButton rbCubiertaNo;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.TextBox txtDetalle;
        private System.Windows.Forms.Label lblContador;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
