namespace UI
{
    partial class frmCambiarClave
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
            pnlTitulo = new Panel();
            lblTitulo = new Label();
            pnlContenido = new Panel();
            lblClaveActual = new Label();
            txtClaveActual = new TextBox();
            lblClaveNueva = new Label();
            txtClaveNueva = new TextBox();
            btnConfirmar = new Button();
            pnlTitulo.SuspendLayout();
            pnlContenido.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTitulo
            // 
            pnlTitulo.BackColor = Color.FromArgb(21, 32, 48);
            pnlTitulo.Controls.Add(lblTitulo);
            pnlTitulo.Dock = DockStyle.Top;
            pnlTitulo.Location = new Point(0, 0);
            pnlTitulo.Name = "pnlTitulo";
            pnlTitulo.Size = new Size(380, 48);
            pnlTitulo.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 11F);
            lblTitulo.ForeColor = Color.FromArgb(226, 232, 240);
            lblTitulo.Location = new Point(16, 14);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(103, 20);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Cambiar clave";
            // 
            // pnlContenido
            // 
            pnlContenido.BackColor = Color.FromArgb(30, 42, 58);
            pnlContenido.Controls.Add(lblClaveActual);
            pnlContenido.Controls.Add(txtClaveActual);
            pnlContenido.Controls.Add(lblClaveNueva);
            pnlContenido.Controls.Add(txtClaveNueva);
            pnlContenido.Controls.Add(btnConfirmar);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(0, 48);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Padding = new Padding(24);
            pnlContenido.Size = new Size(380, 222);
            pnlContenido.TabIndex = 1;
            // 
            // lblClaveActual
            // 
            lblClaveActual.AutoSize = true;
            lblClaveActual.Font = new Font("Segoe UI", 9F);
            lblClaveActual.ForeColor = Color.FromArgb(148, 163, 184);
            lblClaveActual.Location = new Point(24, 24);
            lblClaveActual.Name = "lblClaveActual";
            lblClaveActual.Size = new Size(71, 15);
            lblClaveActual.TabIndex = 0;
            lblClaveActual.Text = "Clave actual";
            // 
            // txtClaveActual
            // 
            txtClaveActual.BackColor = Color.FromArgb(21, 32, 48);
            txtClaveActual.BorderStyle = BorderStyle.FixedSingle;
            txtClaveActual.Font = new Font("Segoe UI", 9.5F);
            txtClaveActual.ForeColor = Color.FromArgb(226, 232, 240);
            txtClaveActual.Location = new Point(24, 44);
            txtClaveActual.Name = "txtClaveActual";
            txtClaveActual.PasswordChar = '●';
            txtClaveActual.Size = new Size(332, 24);
            txtClaveActual.TabIndex = 1;
            // 
            // lblClaveNueva
            // 
            lblClaveNueva.AutoSize = true;
            lblClaveNueva.Font = new Font("Segoe UI", 9F);
            lblClaveNueva.ForeColor = Color.FromArgb(148, 163, 184);
            lblClaveNueva.Location = new Point(24, 90);
            lblClaveNueva.Name = "lblClaveNueva";
            lblClaveNueva.Size = new Size(71, 15);
            lblClaveNueva.TabIndex = 2;
            lblClaveNueva.Text = "Clave nueva";
            // 
            // txtClaveNueva
            // 
            txtClaveNueva.BackColor = Color.FromArgb(21, 32, 48);
            txtClaveNueva.BorderStyle = BorderStyle.FixedSingle;
            txtClaveNueva.Font = new Font("Segoe UI", 9.5F);
            txtClaveNueva.ForeColor = Color.FromArgb(226, 232, 240);
            txtClaveNueva.Location = new Point(24, 110);
            txtClaveNueva.Name = "txtClaveNueva";
            txtClaveNueva.PasswordChar = '●';
            txtClaveNueva.Size = new Size(332, 24);
            txtClaveNueva.TabIndex = 3;
            // 
            // btnConfirmar
            // 
            btnConfirmar.BackColor = Color.FromArgb(45, 60, 82);
            btnConfirmar.Cursor = Cursors.Hand;
            btnConfirmar.FlatAppearance.BorderSize = 0;
            btnConfirmar.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnConfirmar.FlatStyle = FlatStyle.Flat;
            btnConfirmar.Font = new Font("Segoe UI", 9.5F);
            btnConfirmar.ForeColor = Color.FromArgb(226, 232, 240);
            btnConfirmar.Location = new Point(24, 158);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(332, 38);
            btnConfirmar.TabIndex = 4;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = false;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // frmCambiarClave
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(380, 270);
            Controls.Add(pnlContenido);
            Controls.Add(pnlTitulo);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmCambiarClave";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cambiar clave";
            Load += frmCambiarClave_Load;
            pnlTitulo.ResumeLayout(false);
            pnlTitulo.PerformLayout();
            pnlContenido.ResumeLayout(false);
            pnlContenido.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.Label lblClaveActual;
        private System.Windows.Forms.TextBox txtClaveActual;
        private System.Windows.Forms.Label lblClaveNueva;
        private System.Windows.Forms.TextBox txtClaveNueva;
        private System.Windows.Forms.Button btnConfirmar;
    }
}