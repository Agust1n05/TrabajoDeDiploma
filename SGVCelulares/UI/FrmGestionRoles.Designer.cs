namespace UI
{
    partial class FrmGestionRoles
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
            label1 = new Label();
            label3 = new Label();
            clbComponentes = new CheckedListBox();
            label4 = new Label();
            tvNodosComposite = new TreeView();
            label2 = new Label();
            txtNombreFamilia = new TextBox();
            radRol = new RadioButton();
            radFamilia = new RadioButton();
            btnCrear = new Button();
            btnEliminarSeleccionados = new Button();
            label5 = new Label();
            cbxRoles = new ComboBox();
            btnBorrarRol = new Button();
            label6 = new Label();
            cbxFamilias = new ComboBox();
            btnBorrarFamilia = new Button();
            btnSalir = new Button();
            pnlTitulo.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTitulo
            // 
            pnlTitulo.BackColor = Color.FromArgb(21, 32, 48);
            pnlTitulo.Controls.Add(label1);
            pnlTitulo.Dock = DockStyle.Top;
            pnlTitulo.Location = new Point(0, 0);
            pnlTitulo.Name = "pnlTitulo";
            pnlTitulo.Size = new Size(872, 48);
            pnlTitulo.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.ForeColor = Color.FromArgb(226, 232, 240);
            label1.Location = new Point(16, 14);
            label1.Name = "label1";
            label1.Size = new Size(127, 20);
            label1.TabIndex = 0;
            label1.Text = "Gestión de roles";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(71, 85, 105);
            label3.Location = new Point(24, 64);
            label3.Name = "label3";
            label3.Size = new Size(95, 12);
            label3.TabIndex = 1;
            label3.Text = "COMPONENTES";
            // 
            // clbComponentes
            // 
            clbComponentes.BackColor = Color.FromArgb(21, 32, 48);
            clbComponentes.BorderStyle = BorderStyle.FixedSingle;
            clbComponentes.CheckOnClick = true;
            clbComponentes.ForeColor = Color.FromArgb(226, 232, 240);
            clbComponentes.FormattingEnabled = true;
            clbComponentes.Location = new Point(24, 82);
            clbComponentes.Name = "clbComponentes";
            clbComponentes.Size = new Size(250, 234);
            clbComponentes.TabIndex = 2;
            clbComponentes.ItemCheck += clbComponentes_ItemCheck;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(71, 85, 105);
            label4.Location = new Point(290, 64);
            label4.Name = "label4";
            label4.Size = new Size(133, 12);
            label4.TabIndex = 3;
            label4.Text = "VISTA PREVIA DEL ROL";
            // 
            // tvNodosComposite
            // 
            tvNodosComposite.BackColor = Color.FromArgb(21, 32, 48);
            tvNodosComposite.BorderStyle = BorderStyle.FixedSingle;
            tvNodosComposite.ForeColor = Color.FromArgb(226, 232, 240);
            tvNodosComposite.LineColor = Color.FromArgb(71, 85, 105);
            tvNodosComposite.Location = new Point(290, 82);
            tvNodosComposite.Name = "tvNodosComposite";
            tvNodosComposite.Size = new Size(250, 234);
            tvNodosComposite.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F);
            label2.ForeColor = Color.FromArgb(148, 163, 184);
            label2.Location = new Point(560, 64);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 5;
            label2.Text = "Nombre";
            // 
            // txtNombreFamilia
            // 
            txtNombreFamilia.BackColor = Color.FromArgb(21, 32, 48);
            txtNombreFamilia.BorderStyle = BorderStyle.FixedSingle;
            txtNombreFamilia.Font = new Font("Segoe UI", 9.5F);
            txtNombreFamilia.ForeColor = Color.FromArgb(226, 232, 240);
            txtNombreFamilia.Location = new Point(560, 82);
            txtNombreFamilia.Name = "txtNombreFamilia";
            txtNombreFamilia.Size = new Size(288, 24);
            txtNombreFamilia.TabIndex = 6;
            txtNombreFamilia.TextChanged += txtNombreFamilia_TextChanged;
            // 
            // radRol
            // 
            radRol.AutoSize = true;
            radRol.Checked = true;
            radRol.ForeColor = Color.FromArgb(226, 232, 240);
            radRol.Location = new Point(564, 122);
            radRol.Name = "radRol";
            radRol.Size = new Size(42, 19);
            radRol.TabIndex = 7;
            radRol.TabStop = true;
            radRol.Text = "Rol";
            radRol.UseVisualStyleBackColor = true;
            // 
            // radFamilia
            // 
            radFamilia.AutoSize = true;
            radFamilia.ForeColor = Color.FromArgb(226, 232, 240);
            radFamilia.Location = new Point(664, 122);
            radFamilia.Name = "radFamilia";
            radFamilia.Size = new Size(63, 19);
            radFamilia.TabIndex = 8;
            radFamilia.Text = "Familia";
            radFamilia.UseVisualStyleBackColor = true;
            // 
            // btnCrear
            // 
            btnCrear.BackColor = Color.FromArgb(45, 60, 82);
            btnCrear.Cursor = Cursors.Hand;
            btnCrear.FlatAppearance.BorderSize = 0;
            btnCrear.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnCrear.FlatStyle = FlatStyle.Flat;
            btnCrear.Font = new Font("Segoe UI", 9.5F);
            btnCrear.ForeColor = Color.FromArgb(226, 232, 240);
            btnCrear.Location = new Point(560, 158);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(288, 38);
            btnCrear.TabIndex = 9;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = false;
            btnCrear.Click += btnCrear_Click;
            // 
            // btnEliminarSeleccionados
            // 
            btnEliminarSeleccionados.BackColor = Color.FromArgb(45, 60, 82);
            btnEliminarSeleccionados.Cursor = Cursors.Hand;
            btnEliminarSeleccionados.FlatAppearance.BorderSize = 0;
            btnEliminarSeleccionados.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnEliminarSeleccionados.FlatStyle = FlatStyle.Flat;
            btnEliminarSeleccionados.Font = new Font("Segoe UI", 9.5F);
            btnEliminarSeleccionados.ForeColor = Color.FromArgb(226, 232, 240);
            btnEliminarSeleccionados.Location = new Point(560, 204);
            btnEliminarSeleccionados.Name = "btnEliminarSeleccionados";
            btnEliminarSeleccionados.Size = new Size(288, 38);
            btnEliminarSeleccionados.TabIndex = 10;
            btnEliminarSeleccionados.Text = "Eliminar seleccionados";
            btnEliminarSeleccionados.UseVisualStyleBackColor = false;
            btnEliminarSeleccionados.Click += btnEliminarSeleccionados_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F);
            label5.ForeColor = Color.FromArgb(148, 163, 184);
            label5.Location = new Point(24, 332);
            label5.Name = "label5";
            label5.Size = new Size(35, 15);
            label5.TabIndex = 11;
            label5.Text = "Roles";
            // 
            // cbxRoles
            // 
            cbxRoles.BackColor = Color.FromArgb(21, 32, 48);
            cbxRoles.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxRoles.FlatStyle = FlatStyle.Flat;
            cbxRoles.ForeColor = Color.FromArgb(226, 232, 240);
            cbxRoles.FormattingEnabled = true;
            cbxRoles.Location = new Point(24, 350);
            cbxRoles.Name = "cbxRoles";
            cbxRoles.Size = new Size(224, 25);
            cbxRoles.TabIndex = 12;
            cbxRoles.SelectedIndexChanged += cbxRoles_SelectedIndexChanged;
            // 
            // btnBorrarRol
            // 
            btnBorrarRol.BackColor = Color.FromArgb(45, 60, 82);
            btnBorrarRol.Cursor = Cursors.Hand;
            btnBorrarRol.FlatAppearance.BorderSize = 0;
            btnBorrarRol.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnBorrarRol.FlatStyle = FlatStyle.Flat;
            btnBorrarRol.Font = new Font("Segoe UI", 9.5F);
            btnBorrarRol.ForeColor = Color.FromArgb(226, 232, 240);
            btnBorrarRol.Location = new Point(254, 349);
            btnBorrarRol.Name = "btnBorrarRol";
            btnBorrarRol.Size = new Size(96, 28);
            btnBorrarRol.TabIndex = 13;
            btnBorrarRol.Text = "Borrar";
            btnBorrarRol.UseVisualStyleBackColor = false;
            btnBorrarRol.Click += btnBorrarRol_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F);
            label6.ForeColor = Color.FromArgb(148, 163, 184);
            label6.Location = new Point(374, 332);
            label6.Name = "label6";
            label6.Size = new Size(50, 15);
            label6.TabIndex = 14;
            label6.Text = "Familias";
            // 
            // cbxFamilias
            // 
            cbxFamilias.BackColor = Color.FromArgb(21, 32, 48);
            cbxFamilias.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxFamilias.FlatStyle = FlatStyle.Flat;
            cbxFamilias.ForeColor = Color.FromArgb(226, 232, 240);
            cbxFamilias.FormattingEnabled = true;
            cbxFamilias.Location = new Point(374, 350);
            cbxFamilias.Name = "cbxFamilias";
            cbxFamilias.Size = new Size(224, 25);
            cbxFamilias.TabIndex = 15;
            cbxFamilias.SelectedIndexChanged += cbxFamilias_SelectedIndexChanged;
            // 
            // btnBorrarFamilia
            // 
            btnBorrarFamilia.BackColor = Color.FromArgb(45, 60, 82);
            btnBorrarFamilia.Cursor = Cursors.Hand;
            btnBorrarFamilia.FlatAppearance.BorderSize = 0;
            btnBorrarFamilia.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnBorrarFamilia.FlatStyle = FlatStyle.Flat;
            btnBorrarFamilia.Font = new Font("Segoe UI", 9.5F);
            btnBorrarFamilia.ForeColor = Color.FromArgb(226, 232, 240);
            btnBorrarFamilia.Location = new Point(604, 349);
            btnBorrarFamilia.Name = "btnBorrarFamilia";
            btnBorrarFamilia.Size = new Size(96, 28);
            btnBorrarFamilia.TabIndex = 16;
            btnBorrarFamilia.Text = "Borrar";
            btnBorrarFamilia.UseVisualStyleBackColor = false;
            btnBorrarFamilia.Click += btnBorrarFamilia_Click;
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
            btnSalir.Location = new Point(752, 349);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(96, 28);
            btnSalir.TabIndex = 17;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // FrmGestionRoles
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(872, 416);
            Controls.Add(btnSalir);
            Controls.Add(btnBorrarFamilia);
            Controls.Add(cbxFamilias);
            Controls.Add(label6);
            Controls.Add(btnBorrarRol);
            Controls.Add(cbxRoles);
            Controls.Add(label5);
            Controls.Add(btnEliminarSeleccionados);
            Controls.Add(btnCrear);
            Controls.Add(radFamilia);
            Controls.Add(radRol);
            Controls.Add(txtNombreFamilia);
            Controls.Add(label2);
            Controls.Add(tvNodosComposite);
            Controls.Add(label4);
            Controls.Add(clbComponentes);
            Controls.Add(label3);
            Controls.Add(pnlTitulo);
            Font = new Font("Segoe UI", 9.5F);
            Name = "FrmGestionRoles";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de roles";
            Load += FrmGestionRoles_Load;
            pnlTitulo.ResumeLayout(false);
            pnlTitulo.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel pnlTitulo;
        private Label label1;
        private Label label3;
        private CheckedListBox clbComponentes;
        private Label label4;
        private TreeView tvNodosComposite;
        private Label label2;
        private TextBox txtNombreFamilia;
        private RadioButton radRol;
        private RadioButton radFamilia;
        private Button btnCrear;
        private Button btnEliminarSeleccionados;
        private Label label5;
        private ComboBox cbxRoles;
        private Button btnBorrarRol;
        private Label label6;
        private ComboBox cbxFamilias;
        private Button btnBorrarFamilia;
        private Button btnSalir;
    }
}