namespace UI
{
    partial class FrmBackup
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
            btnBackup = new Button();
            btnRestore = new Button();
            btnSalir = new Button();
            SuspendLayout();
            // 
            // btnBackup
            // 
            btnBackup.BackColor = Color.FromArgb(45, 60, 82);
            btnBackup.Cursor = Cursors.Hand;
            btnBackup.FlatAppearance.BorderSize = 0;
            btnBackup.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnBackup.FlatStyle = FlatStyle.Flat;
            btnBackup.Font = new Font("Segoe UI", 9.5F);
            btnBackup.ForeColor = Color.FromArgb(226, 232, 240);
            btnBackup.Location = new Point(50, 48);
            btnBackup.Name = "btnBackup";
            btnBackup.Size = new Size(192, 65);
            btnBackup.TabIndex = 0;
            btnBackup.Text = "Backup";
            btnBackup.UseVisualStyleBackColor = false;
            btnBackup.Click += btnBackup_Click;
            // 
            // btnRestore
            // 
            btnRestore.BackColor = Color.FromArgb(45, 60, 82);
            btnRestore.Cursor = Cursors.Hand;
            btnRestore.FlatAppearance.BorderSize = 0;
            btnRestore.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnRestore.FlatStyle = FlatStyle.Flat;
            btnRestore.Font = new Font("Segoe UI", 9.5F);
            btnRestore.ForeColor = Color.FromArgb(226, 232, 240);
            btnRestore.Location = new Point(50, 119);
            btnRestore.Name = "btnRestore";
            btnRestore.Size = new Size(192, 65);
            btnRestore.TabIndex = 3;
            btnRestore.Text = "Restore";
            btnRestore.UseVisualStyleBackColor = false;
            btnRestore.Click += btnRestore_Click;
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
            btnSalir.Location = new Point(50, 217);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(192, 37);
            btnSalir.TabIndex = 4;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // FrmBackup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(296, 315);
            Controls.Add(btnSalir);
            Controls.Add(btnRestore);
            Controls.Add(btnBackup);
            Name = "FrmBackup";
            Text = "FrmBackup";
            Load += FrmBackup_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnBackup;
        private Button btnRestore;
        private Button btnSalir;
    }
}