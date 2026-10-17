namespace UI
{
    partial class FrmOrdenesTrabajo
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
            lblCantidad = new Label();
            lblNumero = new Label();
            pnlFiltros = new Panel();
            lblFiltro = new Label();
            lblEstado = new Label();
            cboEstado = new ComboBox();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            lblAyudaBuscar = new Label();
            grilla = new DataGridView();
            pnlDetalle = new Panel();
            lblDetalle = new Label();
            lblNroOT = new Label();
            lblEstadoOT = new Label();
            lblTitCliente = new Label();
            lblCliente = new Label();
            lblTitTelefono = new Label();
            lblTelefono = new Label();
            lblTitEquipo = new Label();
            lblEquipo = new Label();
            lblTitImei = new Label();
            lblImei = new Label();
            lblTitTecnico = new Label();
            lblTecnico = new Label();
            lblTitPresupuesto = new Label();
            lblPresupuesto = new Label();
            lblTitFalla = new Label();
            lblFalla = new Label();
            pnlAcciones = new Panel();
            btnRecepcionar = new Button();
            btnDiagnosticar = new Button();
            btnPresupuesto = new Button();
            btnEntregar = new Button();
            btnActualizar = new Button();
            btnSalir = new Button();
            pnlTopbar.SuspendLayout();
            pnlFiltros.SuspendLayout();
            pnlDetalle.SuspendLayout();
            pnlAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grilla).BeginInit();
            SuspendLayout();
            // 
            // pnlTopbar
            // 
            pnlTopbar.BackColor = Color.FromArgb(21, 32, 48);
            pnlTopbar.Controls.Add(lblTitulo);
            pnlTopbar.Controls.Add(lblCantidad);
            pnlTopbar.Controls.Add(lblNumero);
            pnlTopbar.Dock = DockStyle.Top;
            pnlTopbar.Name = "pnlTopbar";
            pnlTopbar.Size = new Size(1100, 48);
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
            lblTitulo.Text = "ÓRDENES DE TRABAJO";
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Font = new Font("Segoe UI", 9F);
            lblCantidad.ForeColor = Color.FromArgb(148, 163, 184);
            lblCantidad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCantidad.Location = new Point(910, 18);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.TabIndex = 1;
            lblCantidad.Text = "Órdenes:";
            // 
            // lblNumero
            // 
            lblNumero.AutoSize = true;
            lblNumero.Font = new Font("Segoe UI", 9F);
            lblNumero.ForeColor = Color.FromArgb(148, 163, 184);
            lblNumero.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblNumero.Location = new Point(990, 18);
            lblNumero.Name = "lblNumero";
            lblNumero.TabIndex = 2;
            lblNumero.Text = "0";
            // 
            // pnlFiltros
            // 
            pnlFiltros.BackColor = Color.FromArgb(25, 37, 53);
            pnlFiltros.Controls.Add(lblFiltro);
            pnlFiltros.Controls.Add(lblEstado);
            pnlFiltros.Controls.Add(cboEstado);
            pnlFiltros.Controls.Add(lblBuscar);
            pnlFiltros.Controls.Add(txtBuscar);
            pnlFiltros.Controls.Add(lblAyudaBuscar);
            pnlFiltros.Location = new Point(24, 64);
            pnlFiltros.Name = "pnlFiltros";
            pnlFiltros.Size = new Size(1052, 40);
            pnlFiltros.TabIndex = 1;
            // 
            // lblFiltro
            // 
            lblFiltro.AutoSize = true;
            lblFiltro.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblFiltro.ForeColor = Color.FromArgb(71, 85, 105);
            lblFiltro.Location = new Point(12, 14);
            lblFiltro.Name = "lblFiltro";
            lblFiltro.TabIndex = 0;
            lblFiltro.Text = "FILTRAR";
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Segoe UI", 9F);
            lblEstado.ForeColor = Color.FromArgb(148, 163, 184);
            lblEstado.Location = new Point(84, 12);
            lblEstado.Name = "lblEstado";
            lblEstado.TabIndex = 1;
            lblEstado.Text = "Estado";
            // 
            // cboEstado
            // 
            cboEstado.BackColor = Color.FromArgb(21, 32, 48);
            cboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEstado.FlatStyle = FlatStyle.Flat;
            cboEstado.Font = new Font("Segoe UI", 9.5F);
            cboEstado.ForeColor = Color.FromArgb(226, 232, 240);
            cboEstado.Location = new Point(140, 8);
            cboEstado.Name = "cboEstado";
            cboEstado.Size = new Size(200, 25);
            cboEstado.TabIndex = 2;
            cboEstado.SelectedIndexChanged += cboEstado_SelectedIndexChanged;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Font = new Font("Segoe UI", 9F);
            lblBuscar.ForeColor = Color.FromArgb(148, 163, 184);
            lblBuscar.Location = new Point(372, 12);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.TabIndex = 3;
            lblBuscar.Text = "Buscar";
            // 
            // txtBuscar
            // 
            txtBuscar.BackColor = Color.FromArgb(21, 32, 48);
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.Font = new Font("Segoe UI", 9.5F);
            txtBuscar.ForeColor = Color.FromArgb(226, 232, 240);
            txtBuscar.Location = new Point(428, 8);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(320, 24);
            txtBuscar.TabIndex = 4;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // lblAyudaBuscar
            // 
            lblAyudaBuscar.AutoSize = true;
            lblAyudaBuscar.Font = new Font("Segoe UI", 9F);
            lblAyudaBuscar.ForeColor = Color.FromArgb(71, 85, 105);
            lblAyudaBuscar.Location = new Point(758, 12);
            lblAyudaBuscar.Name = "lblAyudaBuscar";
            lblAyudaBuscar.TabIndex = 5;
            lblAyudaBuscar.Text = "N° de OT, DNI, apellido o IMEI";
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
            grilla.Location = new Point(24, 116);
            grilla.Name = "grilla";
            grilla.Size = new Size(660, 420);
            grilla.TabIndex = 2;
            grilla.SelectionChanged += grilla_SelectionChanged;
            // 
            // pnlDetalle
            // 
            pnlDetalle.BackColor = Color.FromArgb(25, 37, 53);
            pnlDetalle.Controls.Add(lblDetalle);
            pnlDetalle.Controls.Add(lblNroOT);
            pnlDetalle.Controls.Add(lblEstadoOT);
            pnlDetalle.Controls.Add(lblTitCliente);
            pnlDetalle.Controls.Add(lblCliente);
            pnlDetalle.Controls.Add(lblTitTelefono);
            pnlDetalle.Controls.Add(lblTelefono);
            pnlDetalle.Controls.Add(lblTitEquipo);
            pnlDetalle.Controls.Add(lblEquipo);
            pnlDetalle.Controls.Add(lblTitImei);
            pnlDetalle.Controls.Add(lblImei);
            pnlDetalle.Controls.Add(lblTitTecnico);
            pnlDetalle.Controls.Add(lblTecnico);
            pnlDetalle.Controls.Add(lblTitPresupuesto);
            pnlDetalle.Controls.Add(lblPresupuesto);
            pnlDetalle.Controls.Add(lblTitFalla);
            pnlDetalle.Controls.Add(lblFalla);
            pnlDetalle.Location = new Point(700, 116);
            pnlDetalle.Name = "pnlDetalle";
            pnlDetalle.Padding = new Padding(16);
            pnlDetalle.Size = new Size(376, 420);
            pnlDetalle.TabIndex = 3;
            // 
            // lblDetalle
            // 
            lblDetalle.AutoSize = true;
            lblDetalle.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblDetalle.ForeColor = Color.FromArgb(71, 85, 105);
            lblDetalle.Location = new Point(16, 14);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.TabIndex = 0;
            lblDetalle.Text = "DETALLE DE LA ORDEN";
            // 
            // lblNroOT
            // 
            lblNroOT.AutoSize = false;
            lblNroOT.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblNroOT.ForeColor = Color.FromArgb(226, 232, 240);
            lblNroOT.Location = new Point(16, 34);
            lblNroOT.Name = "lblNroOT";
            lblNroOT.Size = new Size(250, 28);
            lblNroOT.TabIndex = 1;
            lblNroOT.Text = "-";
            // 
            // lblEstadoOT
            // 
            lblEstadoOT.AutoSize = false;
            lblEstadoOT.Font = new Font("Segoe UI", 9.5F);
            lblEstadoOT.ForeColor = Color.FromArgb(56, 189, 248);
            lblEstadoOT.Location = new Point(16, 62);
            lblEstadoOT.Name = "lblEstadoOT";
            lblEstadoOT.Size = new Size(344, 22);
            lblEstadoOT.TabIndex = 2;
            lblEstadoOT.Text = "-";
            // 
            // lblTitCliente
            // 
            lblTitCliente.AutoSize = true;
            lblTitCliente.Font = new Font("Segoe UI", 9F);
            lblTitCliente.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitCliente.Location = new Point(16, 102);
            lblTitCliente.Name = "lblTitCliente";
            lblTitCliente.TabIndex = 3;
            lblTitCliente.Text = "Cliente";
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = false;
            lblCliente.Font = new Font("Segoe UI", 9.5F);
            lblCliente.ForeColor = Color.FromArgb(226, 232, 240);
            lblCliente.Location = new Point(120, 100);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(240, 22);
            lblCliente.TabIndex = 4;
            lblCliente.Text = "-";
            // 
            // lblTitTelefono
            // 
            lblTitTelefono.AutoSize = true;
            lblTitTelefono.Font = new Font("Segoe UI", 9F);
            lblTitTelefono.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitTelefono.Location = new Point(16, 136);
            lblTitTelefono.Name = "lblTitTelefono";
            lblTitTelefono.TabIndex = 5;
            lblTitTelefono.Text = "Teléfono";
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = false;
            lblTelefono.Font = new Font("Segoe UI", 9.5F);
            lblTelefono.ForeColor = Color.FromArgb(226, 232, 240);
            lblTelefono.Location = new Point(120, 134);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(240, 22);
            lblTelefono.TabIndex = 6;
            lblTelefono.Text = "-";
            // 
            // lblTitEquipo
            // 
            lblTitEquipo.AutoSize = true;
            lblTitEquipo.Font = new Font("Segoe UI", 9F);
            lblTitEquipo.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitEquipo.Location = new Point(16, 170);
            lblTitEquipo.Name = "lblTitEquipo";
            lblTitEquipo.TabIndex = 7;
            lblTitEquipo.Text = "Equipo";
            // 
            // lblEquipo
            // 
            lblEquipo.AutoSize = false;
            lblEquipo.Font = new Font("Segoe UI", 9.5F);
            lblEquipo.ForeColor = Color.FromArgb(226, 232, 240);
            lblEquipo.Location = new Point(120, 168);
            lblEquipo.Name = "lblEquipo";
            lblEquipo.Size = new Size(240, 22);
            lblEquipo.TabIndex = 8;
            lblEquipo.Text = "-";
            // 
            // lblTitImei
            // 
            lblTitImei.AutoSize = true;
            lblTitImei.Font = new Font("Segoe UI", 9F);
            lblTitImei.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitImei.Location = new Point(16, 204);
            lblTitImei.Name = "lblTitImei";
            lblTitImei.TabIndex = 9;
            lblTitImei.Text = "IMEI";
            // 
            // lblImei
            // 
            lblImei.AutoSize = false;
            lblImei.Font = new Font("Segoe UI", 9.5F);
            lblImei.ForeColor = Color.FromArgb(226, 232, 240);
            lblImei.Location = new Point(120, 202);
            lblImei.Name = "lblImei";
            lblImei.Size = new Size(240, 22);
            lblImei.TabIndex = 10;
            lblImei.Text = "-";
            // 
            // lblTitTecnico
            // 
            lblTitTecnico.AutoSize = true;
            lblTitTecnico.Font = new Font("Segoe UI", 9F);
            lblTitTecnico.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitTecnico.Location = new Point(16, 238);
            lblTitTecnico.Name = "lblTitTecnico";
            lblTitTecnico.TabIndex = 11;
            lblTitTecnico.Text = "Técnico";
            // 
            // lblTecnico
            // 
            lblTecnico.AutoSize = false;
            lblTecnico.Font = new Font("Segoe UI", 9.5F);
            lblTecnico.ForeColor = Color.FromArgb(226, 232, 240);
            lblTecnico.Location = new Point(120, 236);
            lblTecnico.Name = "lblTecnico";
            lblTecnico.Size = new Size(240, 22);
            lblTecnico.TabIndex = 12;
            lblTecnico.Text = "-";
            // 
            // lblTitPresupuesto
            // 
            lblTitPresupuesto.AutoSize = true;
            lblTitPresupuesto.Font = new Font("Segoe UI", 9F);
            lblTitPresupuesto.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitPresupuesto.Location = new Point(16, 272);
            lblTitPresupuesto.Name = "lblTitPresupuesto";
            lblTitPresupuesto.TabIndex = 13;
            lblTitPresupuesto.Text = "Presupuesto";
            // 
            // lblPresupuesto
            // 
            lblPresupuesto.AutoSize = false;
            lblPresupuesto.Font = new Font("Segoe UI", 9.5F);
            lblPresupuesto.ForeColor = Color.FromArgb(226, 232, 240);
            lblPresupuesto.Location = new Point(120, 270);
            lblPresupuesto.Name = "lblPresupuesto";
            lblPresupuesto.Size = new Size(240, 22);
            lblPresupuesto.TabIndex = 14;
            lblPresupuesto.Text = "-";
            // 
            // lblTitFalla
            // 
            lblTitFalla.AutoSize = true;
            lblTitFalla.Font = new Font("Segoe UI", 9F);
            lblTitFalla.ForeColor = Color.FromArgb(148, 163, 184);
            lblTitFalla.Location = new Point(16, 306);
            lblTitFalla.Name = "lblTitFalla";
            lblTitFalla.TabIndex = 15;
            lblTitFalla.Text = "Falla reportada";
            // 
            // lblFalla
            // 
            lblFalla.AutoSize = false;
            lblFalla.Font = new Font("Segoe UI", 9.5F);
            lblFalla.ForeColor = Color.FromArgb(226, 232, 240);
            lblFalla.Location = new Point(120, 304);
            lblFalla.Name = "lblFalla";
            lblFalla.Size = new Size(240, 72);
            lblFalla.TabIndex = 16;
            lblFalla.Text = "-";
            // 
            // pnlAcciones
            // 
            pnlAcciones.BackColor = Color.FromArgb(25, 37, 53);
            pnlAcciones.Controls.Add(btnRecepcionar);
            pnlAcciones.Controls.Add(btnDiagnosticar);
            pnlAcciones.Controls.Add(btnPresupuesto);
            pnlAcciones.Controls.Add(btnEntregar);
            pnlAcciones.Controls.Add(btnActualizar);
            pnlAcciones.Controls.Add(btnSalir);
            pnlAcciones.Location = new Point(24, 552);
            pnlAcciones.Name = "pnlAcciones";
            pnlAcciones.Size = new Size(1052, 60);
            pnlAcciones.TabIndex = 4;
            // 
            // btnRecepcionar
            // 
            btnRecepcionar.BackColor = Color.FromArgb(14, 116, 144);
            btnRecepcionar.Cursor = Cursors.Hand;
            btnRecepcionar.FlatAppearance.BorderSize = 0;
            btnRecepcionar.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 94, 117);
            btnRecepcionar.FlatStyle = FlatStyle.Flat;
            btnRecepcionar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRecepcionar.ForeColor = Color.FromArgb(226, 232, 240);
            btnRecepcionar.UseVisualStyleBackColor = false;
            btnRecepcionar.Location = new Point(12, 11);
            btnRecepcionar.Name = "btnRecepcionar";
            btnRecepcionar.Size = new Size(200, 38);
            btnRecepcionar.TabIndex = 0;
            btnRecepcionar.Text = "+  Recepcionar equipo";
            btnRecepcionar.Click += btnRecepcionar_Click;
            // 
            // btnDiagnosticar
            // 
            btnDiagnosticar.BackColor = Color.FromArgb(45, 60, 82);
            btnDiagnosticar.Cursor = Cursors.Hand;
            btnDiagnosticar.FlatAppearance.BorderSize = 0;
            btnDiagnosticar.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnDiagnosticar.FlatStyle = FlatStyle.Flat;
            btnDiagnosticar.Font = new Font("Segoe UI", 9.5F);
            btnDiagnosticar.ForeColor = Color.FromArgb(226, 232, 240);
            btnDiagnosticar.UseVisualStyleBackColor = false;
            btnDiagnosticar.Location = new Point(224, 11);
            btnDiagnosticar.Name = "btnDiagnosticar";
            btnDiagnosticar.Size = new Size(200, 38);
            btnDiagnosticar.TabIndex = 1;
            btnDiagnosticar.Text = "Diagnosticar equipo";
            btnDiagnosticar.Click += btnDiagnosticar_Click;
            // 
            // btnPresupuesto
            // 
            btnPresupuesto.BackColor = Color.FromArgb(45, 60, 82);
            btnPresupuesto.Cursor = Cursors.Hand;
            btnPresupuesto.FlatAppearance.BorderSize = 0;
            btnPresupuesto.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnPresupuesto.FlatStyle = FlatStyle.Flat;
            btnPresupuesto.Font = new Font("Segoe UI", 9.5F);
            btnPresupuesto.ForeColor = Color.FromArgb(226, 232, 240);
            btnPresupuesto.UseVisualStyleBackColor = false;
            btnPresupuesto.Location = new Point(436, 11);
            btnPresupuesto.Name = "btnPresupuesto";
            btnPresupuesto.Size = new Size(200, 38);
            btnPresupuesto.TabIndex = 2;
            btnPresupuesto.Text = "Respuesta presupuesto";
            btnPresupuesto.Click += btnPresupuesto_Click;
            // 
            // btnEntregar
            // 
            btnEntregar.BackColor = Color.FromArgb(45, 60, 82);
            btnEntregar.Cursor = Cursors.Hand;
            btnEntregar.FlatAppearance.BorderSize = 0;
            btnEntregar.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnEntregar.FlatStyle = FlatStyle.Flat;
            btnEntregar.Font = new Font("Segoe UI", 9.5F);
            btnEntregar.ForeColor = Color.FromArgb(226, 232, 240);
            btnEntregar.UseVisualStyleBackColor = false;
            btnEntregar.Location = new Point(648, 11);
            btnEntregar.Name = "btnEntregar";
            btnEntregar.Size = new Size(200, 38);
            btnEntregar.TabIndex = 3;
            btnEntregar.Text = "Entregar equipo";
            btnEntregar.Click += btnEntregar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.FromArgb(45, 60, 82);
            btnActualizar.Cursor = Cursors.Hand;
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Font = new Font("Segoe UI", 9.5F);
            btnActualizar.ForeColor = Color.FromArgb(226, 232, 240);
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Location = new Point(716, 11);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(156, 38);
            btnActualizar.TabIndex = 4;
            btnActualizar.Text = "Actualizar";
            btnActualizar.Click += btnActualizar_Click;
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
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Location = new Point(884, 11);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(156, 38);
            btnSalir.TabIndex = 5;
            btnSalir.Text = "Salir";
            btnSalir.Click += btnSalir_Click;
            // 
            // FrmOrdenesTrabajo
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(1100, 628);
            Controls.Add(pnlAcciones);
            Controls.Add(pnlDetalle);
            Controls.Add(grilla);
            Controls.Add(pnlFiltros);
            Controls.Add(pnlTopbar);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmOrdenesTrabajo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Órdenes de Trabajo";
            Load += FrmOrdenesTrabajo_Load;
            pnlTopbar.ResumeLayout(false);
            pnlTopbar.PerformLayout();
            pnlFiltros.ResumeLayout(false);
            pnlFiltros.PerformLayout();
            pnlDetalle.ResumeLayout(false);
            pnlDetalle.PerformLayout();
            pnlAcciones.ResumeLayout(false);
            pnlAcciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)grilla).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTopbar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.Panel pnlFiltros;
        private System.Windows.Forms.Label lblFiltro;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.ComboBox cboEstado;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblAyudaBuscar;
        private System.Windows.Forms.DataGridView grilla;
        private System.Windows.Forms.Panel pnlDetalle;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.Label lblNroOT;
        private System.Windows.Forms.Label lblEstadoOT;
        private System.Windows.Forms.Label lblTitCliente;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblTitTelefono;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.Label lblTitEquipo;
        private System.Windows.Forms.Label lblEquipo;
        private System.Windows.Forms.Label lblTitImei;
        private System.Windows.Forms.Label lblImei;
        private System.Windows.Forms.Label lblTitTecnico;
        private System.Windows.Forms.Label lblTecnico;
        private System.Windows.Forms.Label lblTitPresupuesto;
        private System.Windows.Forms.Label lblPresupuesto;
        private System.Windows.Forms.Label lblTitFalla;
        private System.Windows.Forms.Label lblFalla;
        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.Button btnRecepcionar;
        private System.Windows.Forms.Button btnDiagnosticar;
        private System.Windows.Forms.Button btnPresupuesto;
        private System.Windows.Forms.Button btnEntregar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnSalir;
    }
}
