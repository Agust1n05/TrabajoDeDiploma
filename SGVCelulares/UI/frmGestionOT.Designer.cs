using System.Drawing;
using System.Windows.Forms;

namespace UI
{
    partial class frmGestionOT
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            pnlTopbar = new Panel();
            lblNumero = new Label();
            lblNumeroOrdenes = new Label();
            lblTitulo = new Label();
            pnlContenido = new Panel();
            pnlBotones = new Panel();
            lblGrupoRecepcion = new Label();
            btnRecepcionarEquipo = new Button();
            lblGrupoPresupuesto = new Label();
            btnRegistrarAprobacion = new Button();
            btnRegistrarRechazo = new Button();
            lblGrupoEntrega = new Label();
            btnEntregarEquipo = new Button();
            btnRealizarCobro = new Button();
            btnAplicarCambios = new Button();
            btnSalir = new Button();
            pnlFormulario = new Panel();
            lblAyuda = new Label();
            grpRecepcion = new GroupBox();
            lblDni = new Label();
            txtDni = new TextBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblApellido = new Label();
            txtApellido = new TextBox();
            lblTelefono = new Label();
            txtTelefono = new TextBox();
            lblMarca = new Label();
            txtMarca = new TextBox();
            lblModelo = new Label();
            txtModelo = new TextBox();
            lblImei = new Label();
            txtImei = new TextBox();
            lblFallaReportada = new Label();
            txtFallaReportada = new TextBox();
            grpDetalleOt = new GroupBox();
            lblNroOt = new Label();
            txtNroOt = new TextBox();
            lblEstadoOt = new Label();
            txtEstadoOt = new TextBox();
            lblMontoPresupuesto = new Label();
            txtMontoPresupuesto = new TextBox();
            lblTecnicoAsignado = new Label();
            txtTecnicoAsignado = new TextBox();
            grillaOrdenes = new DataGridView();
            pnlFiltros = new Panel();
            lblFiltro = new Label();
            lblEstadoFiltro = new Label();
            cbxEstadoFiltro = new ComboBox();
            pnlTopbar.SuspendLayout();
            pnlContenido.SuspendLayout();
            pnlBotones.SuspendLayout();
            pnlFormulario.SuspendLayout();
            grpRecepcion.SuspendLayout();
            grpDetalleOt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grillaOrdenes).BeginInit();
            pnlFiltros.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTopbar
            // 
            pnlTopbar.BackColor = Color.FromArgb(21, 32, 48);
            pnlTopbar.Controls.Add(lblNumero);
            pnlTopbar.Controls.Add(lblNumeroOrdenes);
            pnlTopbar.Controls.Add(lblTitulo);
            pnlTopbar.Dock = DockStyle.Top;
            pnlTopbar.Location = new Point(0, 0);
            pnlTopbar.Name = "pnlTopbar";
            pnlTopbar.Size = new Size(1114, 48);
            pnlTopbar.TabIndex = 0;
            // 
            // lblNumero
            // 
            lblNumero.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblNumero.AutoSize = true;
            lblNumero.Font = new Font("Segoe UI", 9F);
            lblNumero.ForeColor = Color.FromArgb(148, 163, 184);
            lblNumero.Location = new Point(987, 18);
            lblNumero.Name = "lblNumero";
            lblNumero.Size = new Size(14, 15);
            lblNumero.TabIndex = 2;
            lblNumero.Text = "X";
            // 
            // lblNumeroOrdenes
            // 
            lblNumeroOrdenes.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblNumeroOrdenes.AutoSize = true;
            lblNumeroOrdenes.Font = new Font("Segoe UI", 9F);
            lblNumeroOrdenes.ForeColor = Color.FromArgb(148, 163, 184);
            lblNumeroOrdenes.Location = new Point(854, 18);
            lblNumeroOrdenes.Name = "lblNumeroOrdenes";
            lblNumeroOrdenes.Size = new Size(115, 15);
            lblNumeroOrdenes.TabIndex = 1;
            lblNumeroOrdenes.Text = "Número de órdenes:";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(226, 232, 240);
            lblTitulo.Location = new Point(16, 14);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(174, 20);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "ÓRDENES DE TRABAJO";
            // 
            // pnlContenido
            // 
            pnlContenido.BackColor = Color.FromArgb(30, 42, 58);
            pnlContenido.Controls.Add(pnlBotones);
            pnlContenido.Controls.Add(pnlFormulario);
            pnlContenido.Controls.Add(grillaOrdenes);
            pnlContenido.Controls.Add(pnlFiltros);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(0, 48);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Padding = new Padding(24);
            pnlContenido.Size = new Size(1114, 604);
            pnlContenido.TabIndex = 1;
            // 
            // pnlBotones
            // 
            pnlBotones.BackColor = Color.FromArgb(25, 37, 53);
            pnlBotones.Controls.Add(lblGrupoRecepcion);
            pnlBotones.Controls.Add(btnRecepcionarEquipo);
            pnlBotones.Controls.Add(lblGrupoPresupuesto);
            pnlBotones.Controls.Add(btnRegistrarAprobacion);
            pnlBotones.Controls.Add(btnRegistrarRechazo);
            pnlBotones.Controls.Add(lblGrupoEntrega);
            pnlBotones.Controls.Add(btnEntregarEquipo);
            pnlBotones.Controls.Add(btnRealizarCobro);
            pnlBotones.Controls.Add(btnAplicarCambios);
            pnlBotones.Controls.Add(btnSalir);
            pnlBotones.Location = new Point(860, 72);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Padding = new Padding(8);
            pnlBotones.Size = new Size(172, 496);
            pnlBotones.TabIndex = 3;
            // 
            // lblGrupoRecepcion
            // 
            lblGrupoRecepcion.AutoSize = true;
            lblGrupoRecepcion.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblGrupoRecepcion.ForeColor = Color.FromArgb(71, 85, 105);
            lblGrupoRecepcion.Location = new Point(8, 8);
            lblGrupoRecepcion.Name = "lblGrupoRecepcion";
            lblGrupoRecepcion.Size = new Size(59, 12);
            lblGrupoRecepcion.TabIndex = 0;
            lblGrupoRecepcion.Text = "RECEPCIÓN";
            // 
            // btnRecepcionarEquipo
            // 
            btnRecepcionarEquipo.BackColor = Color.FromArgb(45, 60, 82);
            btnRecepcionarEquipo.Cursor = Cursors.Hand;
            btnRecepcionarEquipo.FlatAppearance.BorderSize = 0;
            btnRecepcionarEquipo.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnRecepcionarEquipo.FlatStyle = FlatStyle.Flat;
            btnRecepcionarEquipo.Font = new Font("Segoe UI", 9.5F);
            btnRecepcionarEquipo.ForeColor = Color.FromArgb(226, 232, 240);
            btnRecepcionarEquipo.Location = new Point(8, 24);
            btnRecepcionarEquipo.Name = "btnRecepcionarEquipo";
            btnRecepcionarEquipo.Size = new Size(156, 38);
            btnRecepcionarEquipo.TabIndex = 1;
            btnRecepcionarEquipo.Tag = "Recepcionar Equipo";
            btnRecepcionarEquipo.Text = "Recepcionar Equipo";
            btnRecepcionarEquipo.UseVisualStyleBackColor = false;
            btnRecepcionarEquipo.Click += btnRecepcionarEquipo_Click;
            // 
            // lblGrupoPresupuesto
            // 
            lblGrupoPresupuesto.AutoSize = true;
            lblGrupoPresupuesto.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblGrupoPresupuesto.ForeColor = Color.FromArgb(71, 85, 105);
            lblGrupoPresupuesto.Location = new Point(8, 70);
            lblGrupoPresupuesto.Name = "lblGrupoPresupuesto";
            lblGrupoPresupuesto.Size = new Size(74, 12);
            lblGrupoPresupuesto.TabIndex = 2;
            lblGrupoPresupuesto.Text = "PRESUPUESTO";
            // 
            // btnRegistrarAprobacion
            // 
            btnRegistrarAprobacion.BackColor = Color.FromArgb(45, 60, 82);
            btnRegistrarAprobacion.Cursor = Cursors.Hand;
            btnRegistrarAprobacion.FlatAppearance.BorderSize = 0;
            btnRegistrarAprobacion.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnRegistrarAprobacion.FlatStyle = FlatStyle.Flat;
            btnRegistrarAprobacion.Font = new Font("Segoe UI", 9.5F);
            btnRegistrarAprobacion.ForeColor = Color.FromArgb(226, 232, 240);
            btnRegistrarAprobacion.Location = new Point(8, 86);
            btnRegistrarAprobacion.Name = "btnRegistrarAprobacion";
            btnRegistrarAprobacion.Size = new Size(156, 38);
            btnRegistrarAprobacion.TabIndex = 3;
            btnRegistrarAprobacion.Tag = "Registrar Aprobación de Presupuesto";
            btnRegistrarAprobacion.Text = "Aprobar Presupuesto";
            btnRegistrarAprobacion.UseVisualStyleBackColor = false;
            btnRegistrarAprobacion.Click += btnRegistrarAprobacion_Click;
            // 
            // btnRegistrarRechazo
            // 
            btnRegistrarRechazo.BackColor = Color.FromArgb(45, 60, 82);
            btnRegistrarRechazo.Cursor = Cursors.Hand;
            btnRegistrarRechazo.FlatAppearance.BorderSize = 0;
            btnRegistrarRechazo.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnRegistrarRechazo.FlatStyle = FlatStyle.Flat;
            btnRegistrarRechazo.Font = new Font("Segoe UI", 9.5F);
            btnRegistrarRechazo.ForeColor = Color.FromArgb(226, 232, 240);
            btnRegistrarRechazo.Location = new Point(8, 128);
            btnRegistrarRechazo.Name = "btnRegistrarRechazo";
            btnRegistrarRechazo.Size = new Size(156, 38);
            btnRegistrarRechazo.TabIndex = 4;
            btnRegistrarRechazo.Tag = "Registrar Rechazo de Presupuesto";
            btnRegistrarRechazo.Text = "Rechazar Presupuesto";
            btnRegistrarRechazo.UseVisualStyleBackColor = false;
            btnRegistrarRechazo.Click += btnRegistrarRechazo_Click;
            // 
            // lblGrupoEntrega
            // 
            lblGrupoEntrega.AutoSize = true;
            lblGrupoEntrega.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblGrupoEntrega.ForeColor = Color.FromArgb(71, 85, 105);
            lblGrupoEntrega.Location = new Point(8, 174);
            lblGrupoEntrega.Name = "lblGrupoEntrega";
            lblGrupoEntrega.Size = new Size(95, 12);
            lblGrupoEntrega.TabIndex = 5;
            lblGrupoEntrega.Text = "ENTREGA / COBRO";
            // 
            // btnEntregarEquipo
            // 
            btnEntregarEquipo.BackColor = Color.FromArgb(45, 60, 82);
            btnEntregarEquipo.Cursor = Cursors.Hand;
            btnEntregarEquipo.FlatAppearance.BorderSize = 0;
            btnEntregarEquipo.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnEntregarEquipo.FlatStyle = FlatStyle.Flat;
            btnEntregarEquipo.Font = new Font("Segoe UI", 9.5F);
            btnEntregarEquipo.ForeColor = Color.FromArgb(226, 232, 240);
            btnEntregarEquipo.Location = new Point(8, 190);
            btnEntregarEquipo.Name = "btnEntregarEquipo";
            btnEntregarEquipo.Size = new Size(156, 38);
            btnEntregarEquipo.TabIndex = 6;
            btnEntregarEquipo.Tag = "Entregar Equipo";
            btnEntregarEquipo.Text = "Entregar Equipo";
            btnEntregarEquipo.UseVisualStyleBackColor = false;
            btnEntregarEquipo.Click += btnEntregarEquipo_Click;
            // 
            // btnRealizarCobro
            // 
            btnRealizarCobro.BackColor = Color.FromArgb(45, 60, 82);
            btnRealizarCobro.Cursor = Cursors.Hand;
            btnRealizarCobro.FlatAppearance.BorderSize = 0;
            btnRealizarCobro.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnRealizarCobro.FlatStyle = FlatStyle.Flat;
            btnRealizarCobro.Font = new Font("Segoe UI", 9.5F);
            btnRealizarCobro.ForeColor = Color.FromArgb(226, 232, 240);
            btnRealizarCobro.Location = new Point(8, 232);
            btnRealizarCobro.Name = "btnRealizarCobro";
            btnRealizarCobro.Size = new Size(156, 38);
            btnRealizarCobro.TabIndex = 7;
            btnRealizarCobro.Tag = "Realizar Cobro";
            btnRealizarCobro.Text = "Realizar Cobro";
            btnRealizarCobro.UseVisualStyleBackColor = false;
            btnRealizarCobro.Click += btnRealizarCobro_Click;
            // 
            // btnAplicarCambios
            // 
            btnAplicarCambios.BackColor = Color.FromArgb(45, 60, 82);
            btnAplicarCambios.Cursor = Cursors.Hand;
            btnAplicarCambios.FlatAppearance.BorderSize = 0;
            btnAplicarCambios.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnAplicarCambios.FlatStyle = FlatStyle.Flat;
            btnAplicarCambios.Font = new Font("Segoe UI", 9.5F);
            btnAplicarCambios.ForeColor = Color.FromArgb(226, 232, 240);
            btnAplicarCambios.Location = new Point(8, 294);
            btnAplicarCambios.Name = "btnAplicarCambios";
            btnAplicarCambios.Size = new Size(156, 38);
            btnAplicarCambios.TabIndex = 8;
            btnAplicarCambios.Text = "Aplicar";
            btnAplicarCambios.UseVisualStyleBackColor = false;
            btnAplicarCambios.Click += btnAplicarCambios_Click;
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
            btnSalir.Location = new Point(8, 442);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(156, 38);
            btnSalir.TabIndex = 9;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // pnlFormulario
            // 
            pnlFormulario.BackColor = Color.FromArgb(25, 37, 53);
            pnlFormulario.Controls.Add(lblAyuda);
            pnlFormulario.Controls.Add(grpRecepcion);
            pnlFormulario.Controls.Add(grpDetalleOt);
            pnlFormulario.Location = new Point(24, 284);
            pnlFormulario.Name = "pnlFormulario";
            pnlFormulario.Padding = new Padding(16);
            pnlFormulario.Size = new Size(820, 284);
            pnlFormulario.TabIndex = 2;
            // 
            // lblAyuda
            // 
            lblAyuda.AutoSize = true;
            lblAyuda.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblAyuda.ForeColor = Color.FromArgb(148, 163, 184);
            lblAyuda.Location = new Point(16, 6);
            lblAyuda.Name = "lblAyuda";
            lblAyuda.Size = new Size(222, 15);
            lblAyuda.TabIndex = 0;
            lblAyuda.Text = "Ingresá el DNI del cliente para comenzar.";
            // 
            // grpRecepcion
            // 
            grpRecepcion.Controls.Add(lblDni);
            grpRecepcion.Controls.Add(txtDni);
            grpRecepcion.Controls.Add(lblNombre);
            grpRecepcion.Controls.Add(txtNombre);
            grpRecepcion.Controls.Add(lblApellido);
            grpRecepcion.Controls.Add(txtApellido);
            grpRecepcion.Controls.Add(lblTelefono);
            grpRecepcion.Controls.Add(txtTelefono);
            grpRecepcion.Controls.Add(lblMarca);
            grpRecepcion.Controls.Add(txtMarca);
            grpRecepcion.Controls.Add(lblModelo);
            grpRecepcion.Controls.Add(txtModelo);
            grpRecepcion.Controls.Add(lblImei);
            grpRecepcion.Controls.Add(txtImei);
            grpRecepcion.Controls.Add(lblFallaReportada);
            grpRecepcion.Controls.Add(txtFallaReportada);
            grpRecepcion.FlatStyle = FlatStyle.Flat;
            grpRecepcion.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            grpRecepcion.ForeColor = Color.FromArgb(148, 163, 184);
            grpRecepcion.Location = new Point(16, 28);
            grpRecepcion.Name = "grpRecepcion";
            grpRecepcion.Size = new Size(560, 240);
            grpRecepcion.TabIndex = 1;
            grpRecepcion.TabStop = false;
            grpRecepcion.Text = "NUEVA RECEPCIÓN — Cliente y Equipo";
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Font = new Font("Segoe UI", 9F);
            lblDni.ForeColor = Color.FromArgb(148, 163, 184);
            lblDni.Location = new Point(16, 30);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(27, 15);
            lblDni.TabIndex = 0;
            lblDni.Text = "DNI";
            // 
            // txtDni
            // 
            txtDni.BackColor = Color.FromArgb(21, 32, 48);
            txtDni.BorderStyle = BorderStyle.FixedSingle;
            txtDni.Font = new Font("Segoe UI", 9.5F);
            txtDni.ForeColor = Color.FromArgb(226, 232, 240);
            txtDni.Location = new Point(110, 27);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(170, 24);
            txtDni.TabIndex = 1;
            txtDni.Leave += txtDni_Leave;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9F);
            lblNombre.ForeColor = Color.FromArgb(148, 163, 184);
            lblNombre.Location = new Point(16, 68);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(51, 15);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.FromArgb(21, 32, 48);
            txtNombre.BorderStyle = BorderStyle.FixedSingle;
            txtNombre.Font = new Font("Segoe UI", 9.5F);
            txtNombre.ForeColor = Color.FromArgb(148, 163, 184);
            txtNombre.Location = new Point(110, 65);
            txtNombre.Name = "txtNombre";
            txtNombre.ReadOnly = true;
            txtNombre.Size = new Size(170, 24);
            txtNombre.TabIndex = 3;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Font = new Font("Segoe UI", 9F);
            lblApellido.ForeColor = Color.FromArgb(148, 163, 184);
            lblApellido.Location = new Point(16, 106);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(51, 15);
            lblApellido.TabIndex = 4;
            lblApellido.Text = "Apellido";
            // 
            // txtApellido
            // 
            txtApellido.BackColor = Color.FromArgb(21, 32, 48);
            txtApellido.BorderStyle = BorderStyle.FixedSingle;
            txtApellido.Font = new Font("Segoe UI", 9.5F);
            txtApellido.ForeColor = Color.FromArgb(148, 163, 184);
            txtApellido.Location = new Point(110, 103);
            txtApellido.Name = "txtApellido";
            txtApellido.ReadOnly = true;
            txtApellido.Size = new Size(170, 24);
            txtApellido.TabIndex = 5;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Font = new Font("Segoe UI", 9F);
            lblTelefono.ForeColor = Color.FromArgb(148, 163, 184);
            lblTelefono.Location = new Point(16, 144);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(53, 15);
            lblTelefono.TabIndex = 6;
            lblTelefono.Text = "Teléfono";
            // 
            // txtTelefono
            // 
            txtTelefono.BackColor = Color.FromArgb(21, 32, 48);
            txtTelefono.BorderStyle = BorderStyle.FixedSingle;
            txtTelefono.Font = new Font("Segoe UI", 9.5F);
            txtTelefono.ForeColor = Color.FromArgb(148, 163, 184);
            txtTelefono.Location = new Point(110, 141);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.ReadOnly = true;
            txtTelefono.Size = new Size(170, 24);
            txtTelefono.TabIndex = 7;
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.Font = new Font("Segoe UI", 9F);
            lblMarca.ForeColor = Color.FromArgb(148, 163, 184);
            lblMarca.Location = new Point(300, 30);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(40, 15);
            lblMarca.TabIndex = 8;
            lblMarca.Text = "Marca";
            // 
            // txtMarca
            // 
            txtMarca.BackColor = Color.FromArgb(21, 32, 48);
            txtMarca.BorderStyle = BorderStyle.FixedSingle;
            txtMarca.Font = new Font("Segoe UI", 9.5F);
            txtMarca.ForeColor = Color.FromArgb(226, 232, 240);
            txtMarca.Location = new Point(390, 27);
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(154, 24);
            txtMarca.TabIndex = 9;
            // 
            // lblModelo
            // 
            lblModelo.AutoSize = true;
            lblModelo.Font = new Font("Segoe UI", 9F);
            lblModelo.ForeColor = Color.FromArgb(148, 163, 184);
            lblModelo.Location = new Point(300, 68);
            lblModelo.Name = "lblModelo";
            lblModelo.Size = new Size(48, 15);
            lblModelo.TabIndex = 10;
            lblModelo.Text = "Modelo";
            // 
            // txtModelo
            // 
            txtModelo.BackColor = Color.FromArgb(21, 32, 48);
            txtModelo.BorderStyle = BorderStyle.FixedSingle;
            txtModelo.Font = new Font("Segoe UI", 9.5F);
            txtModelo.ForeColor = Color.FromArgb(226, 232, 240);
            txtModelo.Location = new Point(390, 65);
            txtModelo.Name = "txtModelo";
            txtModelo.Size = new Size(154, 24);
            txtModelo.TabIndex = 11;
            // 
            // lblImei
            // 
            lblImei.AutoSize = true;
            lblImei.Font = new Font("Segoe UI", 9F);
            lblImei.ForeColor = Color.FromArgb(148, 163, 184);
            lblImei.Location = new Point(300, 106);
            lblImei.Name = "lblImei";
            lblImei.Size = new Size(30, 15);
            lblImei.TabIndex = 12;
            lblImei.Text = "IMEI";
            // 
            // txtImei
            // 
            txtImei.BackColor = Color.FromArgb(21, 32, 48);
            txtImei.BorderStyle = BorderStyle.FixedSingle;
            txtImei.Font = new Font("Segoe UI", 9.5F);
            txtImei.ForeColor = Color.FromArgb(226, 232, 240);
            txtImei.Location = new Point(390, 103);
            txtImei.MaxLength = 15;
            txtImei.Name = "txtImei";
            txtImei.Size = new Size(154, 24);
            txtImei.TabIndex = 13;
            // 
            // lblFallaReportada
            // 
            lblFallaReportada.AutoSize = true;
            lblFallaReportada.Font = new Font("Segoe UI", 9F);
            lblFallaReportada.ForeColor = Color.FromArgb(148, 163, 184);
            lblFallaReportada.Location = new Point(300, 150);
            lblFallaReportada.Name = "lblFallaReportada";
            lblFallaReportada.Size = new Size(88, 15);
            lblFallaReportada.TabIndex = 14;
            lblFallaReportada.Text = "Falla Reportada";
            // 
            // txtFallaReportada
            // 
            txtFallaReportada.BackColor = Color.FromArgb(21, 32, 48);
            txtFallaReportada.BorderStyle = BorderStyle.FixedSingle;
            txtFallaReportada.Font = new Font("Segoe UI", 9.5F);
            txtFallaReportada.ForeColor = Color.FromArgb(226, 232, 240);
            txtFallaReportada.Location = new Point(390, 141);
            txtFallaReportada.Multiline = true;
            txtFallaReportada.Name = "txtFallaReportada";
            txtFallaReportada.Size = new Size(154, 60);
            txtFallaReportada.TabIndex = 15;
            // 
            // grpDetalleOt
            // 
            grpDetalleOt.Controls.Add(lblNroOt);
            grpDetalleOt.Controls.Add(txtNroOt);
            grpDetalleOt.Controls.Add(lblEstadoOt);
            grpDetalleOt.Controls.Add(txtEstadoOt);
            grpDetalleOt.Controls.Add(lblMontoPresupuesto);
            grpDetalleOt.Controls.Add(txtMontoPresupuesto);
            grpDetalleOt.Controls.Add(lblTecnicoAsignado);
            grpDetalleOt.Controls.Add(txtTecnicoAsignado);
            grpDetalleOt.FlatStyle = FlatStyle.Flat;
            grpDetalleOt.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            grpDetalleOt.ForeColor = Color.FromArgb(148, 163, 184);
            grpDetalleOt.Location = new Point(588, 28);
            grpDetalleOt.Name = "grpDetalleOt";
            grpDetalleOt.Size = new Size(216, 240);
            grpDetalleOt.TabIndex = 2;
            grpDetalleOt.TabStop = false;
            grpDetalleOt.Text = "DETALLE DE OT SELECCIONADA";
            // 
            // lblNroOt
            // 
            lblNroOt.AutoSize = true;
            lblNroOt.Font = new Font("Segoe UI", 9F);
            lblNroOt.ForeColor = Color.FromArgb(148, 163, 184);
            lblNroOt.Location = new Point(16, 30);
            lblNroOt.Name = "lblNroOt";
            lblNroOt.Size = new Size(48, 15);
            lblNroOt.TabIndex = 0;
            lblNroOt.Text = "Nro. OT";
            // 
            // txtNroOt
            // 
            txtNroOt.BackColor = Color.FromArgb(21, 32, 48);
            txtNroOt.BorderStyle = BorderStyle.FixedSingle;
            txtNroOt.Font = new Font("Segoe UI", 9.5F);
            txtNroOt.ForeColor = Color.FromArgb(148, 163, 184);
            txtNroOt.Location = new Point(96, 27);
            txtNroOt.Name = "txtNroOt";
            txtNroOt.ReadOnly = true;
            txtNroOt.Size = new Size(104, 24);
            txtNroOt.TabIndex = 1;
            // 
            // lblEstadoOt
            // 
            lblEstadoOt.AutoSize = true;
            lblEstadoOt.Font = new Font("Segoe UI", 9F);
            lblEstadoOt.ForeColor = Color.FromArgb(148, 163, 184);
            lblEstadoOt.Location = new Point(16, 68);
            lblEstadoOt.Name = "lblEstadoOt";
            lblEstadoOt.Size = new Size(42, 15);
            lblEstadoOt.TabIndex = 2;
            lblEstadoOt.Text = "Estado";
            // 
            // txtEstadoOt
            // 
            txtEstadoOt.BackColor = Color.FromArgb(21, 32, 48);
            txtEstadoOt.BorderStyle = BorderStyle.FixedSingle;
            txtEstadoOt.Font = new Font("Segoe UI", 9.5F);
            txtEstadoOt.ForeColor = Color.FromArgb(148, 163, 184);
            txtEstadoOt.Location = new Point(96, 65);
            txtEstadoOt.Name = "txtEstadoOt";
            txtEstadoOt.ReadOnly = true;
            txtEstadoOt.Size = new Size(104, 24);
            txtEstadoOt.TabIndex = 3;
            // 
            // lblMontoPresupuesto
            // 
            lblMontoPresupuesto.AutoSize = true;
            lblMontoPresupuesto.Font = new Font("Segoe UI", 9F);
            lblMontoPresupuesto.ForeColor = Color.FromArgb(148, 163, 184);
            lblMontoPresupuesto.Location = new Point(16, 106);
            lblMontoPresupuesto.Name = "lblMontoPresupuesto";
            lblMontoPresupuesto.Size = new Size(81, 15);
            lblMontoPresupuesto.TabIndex = 4;
            lblMontoPresupuesto.Text = "Presupuesto $";
            // 
            // txtMontoPresupuesto
            // 
            txtMontoPresupuesto.BackColor = Color.FromArgb(21, 32, 48);
            txtMontoPresupuesto.BorderStyle = BorderStyle.FixedSingle;
            txtMontoPresupuesto.Font = new Font("Segoe UI", 9.5F);
            txtMontoPresupuesto.ForeColor = Color.FromArgb(226, 232, 240);
            txtMontoPresupuesto.Location = new Point(96, 103);
            txtMontoPresupuesto.Name = "txtMontoPresupuesto";
            txtMontoPresupuesto.Size = new Size(104, 24);
            txtMontoPresupuesto.TabIndex = 5;
            // 
            // lblTecnicoAsignado
            // 
            lblTecnicoAsignado.AutoSize = true;
            lblTecnicoAsignado.Font = new Font("Segoe UI", 9F);
            lblTecnicoAsignado.ForeColor = Color.FromArgb(148, 163, 184);
            lblTecnicoAsignado.Location = new Point(16, 144);
            lblTecnicoAsignado.Name = "lblTecnicoAsignado";
            lblTecnicoAsignado.Size = new Size(48, 15);
            lblTecnicoAsignado.TabIndex = 6;
            lblTecnicoAsignado.Text = "Técnico";
            // 
            // txtTecnicoAsignado
            // 
            txtTecnicoAsignado.BackColor = Color.FromArgb(21, 32, 48);
            txtTecnicoAsignado.BorderStyle = BorderStyle.FixedSingle;
            txtTecnicoAsignado.Font = new Font("Segoe UI", 9.5F);
            txtTecnicoAsignado.ForeColor = Color.FromArgb(148, 163, 184);
            txtTecnicoAsignado.Location = new Point(96, 141);
            txtTecnicoAsignado.Name = "txtTecnicoAsignado";
            txtTecnicoAsignado.ReadOnly = true;
            txtTecnicoAsignado.Size = new Size(104, 24);
            txtTecnicoAsignado.TabIndex = 7;
            // 
            // grillaOrdenes
            // 
            grillaOrdenes.AllowUserToAddRows = false;
            grillaOrdenes.AllowUserToDeleteRows = false;
            grillaOrdenes.BackgroundColor = Color.FromArgb(25, 37, 53);
            grillaOrdenes.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(21, 32, 48);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(148, 163, 184);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            grillaOrdenes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            grillaOrdenes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(25, 37, 53);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(226, 232, 240);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(45, 60, 82);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(226, 232, 240);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            grillaOrdenes.DefaultCellStyle = dataGridViewCellStyle2;
            grillaOrdenes.EnableHeadersVisualStyles = false;
            grillaOrdenes.GridColor = Color.FromArgb(45, 60, 82);
            grillaOrdenes.Location = new Point(24, 72);
            grillaOrdenes.Name = "grillaOrdenes";
            grillaOrdenes.ReadOnly = true;
            grillaOrdenes.RowHeadersVisible = false;
            grillaOrdenes.Size = new Size(820, 200);
            grillaOrdenes.TabIndex = 1;
            grillaOrdenes.SelectionChanged += grillaOrdenes_SelectionChanged;
            // 
            // pnlFiltros
            // 
            pnlFiltros.BackColor = Color.FromArgb(25, 37, 53);
            pnlFiltros.Controls.Add(lblFiltro);
            pnlFiltros.Controls.Add(lblEstadoFiltro);
            pnlFiltros.Controls.Add(cbxEstadoFiltro);
            pnlFiltros.Location = new Point(24, 24);
            pnlFiltros.Name = "pnlFiltros";
            pnlFiltros.Size = new Size(820, 36);
            pnlFiltros.TabIndex = 0;
            // 
            // lblFiltro
            // 
            lblFiltro.AutoSize = true;
            lblFiltro.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblFiltro.ForeColor = Color.FromArgb(71, 85, 105);
            lblFiltro.Location = new Point(10, 11);
            lblFiltro.Name = "lblFiltro";
            lblFiltro.Size = new Size(44, 12);
            lblFiltro.TabIndex = 0;
            lblFiltro.Text = "FILTRAR";
            // 
            // lblEstadoFiltro
            // 
            lblEstadoFiltro.AutoSize = true;
            lblEstadoFiltro.Font = new Font("Segoe UI", 9F);
            lblEstadoFiltro.ForeColor = Color.FromArgb(148, 163, 184);
            lblEstadoFiltro.Location = new Point(80, 9);
            lblEstadoFiltro.Name = "lblEstadoFiltro";
            lblEstadoFiltro.Size = new Size(45, 15);
            lblEstadoFiltro.TabIndex = 1;
            lblEstadoFiltro.Text = "Estado:";
            // 
            // cbxEstadoFiltro
            // 
            cbxEstadoFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxEstadoFiltro.FormattingEnabled = true;
            cbxEstadoFiltro.Items.AddRange(new object[] { "Todas", "Recibida", "En Diagnóstico", "Presupuestado", "Entregado sin reparar", "En reparación", "Reparado", "Entregado" });
            cbxEstadoFiltro.Location = new Point(130, 6);
            cbxEstadoFiltro.Name = "cbxEstadoFiltro";
            cbxEstadoFiltro.Size = new Size(160, 25);
            cbxEstadoFiltro.TabIndex = 2;
            cbxEstadoFiltro.SelectedIndexChanged += cbxEstadoFiltro_SelectedIndexChanged;
            // 
            // frmGestionOT
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(1114, 652);
            Controls.Add(pnlContenido);
            Controls.Add(pnlTopbar);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmGestionOT";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Órdenes de Trabajo";
            Load += frmGestionOT_Load;
            pnlTopbar.ResumeLayout(false);
            pnlTopbar.PerformLayout();
            pnlContenido.ResumeLayout(false);
            pnlBotones.ResumeLayout(false);
            pnlBotones.PerformLayout();
            pnlFormulario.ResumeLayout(false);
            pnlFormulario.PerformLayout();
            grpRecepcion.ResumeLayout(false);
            grpRecepcion.PerformLayout();
            grpDetalleOt.ResumeLayout(false);
            grpDetalleOt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)grillaOrdenes).EndInit();
            pnlFiltros.ResumeLayout(false);
            pnlFiltros.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTopbar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblNumeroOrdenes;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.Panel pnlFiltros;
        private System.Windows.Forms.Label lblFiltro;
        private System.Windows.Forms.Label lblEstadoFiltro;
        private System.Windows.Forms.ComboBox cbxEstadoFiltro;
        private System.Windows.Forms.DataGridView grillaOrdenes;
        private System.Windows.Forms.Panel pnlFormulario;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.GroupBox grpRecepcion;
        private System.Windows.Forms.Label lblDni;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.Label lblModelo;
        private System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.Label lblImei;
        private System.Windows.Forms.TextBox txtImei;
        private System.Windows.Forms.Label lblFallaReportada;
        private System.Windows.Forms.TextBox txtFallaReportada;
        private System.Windows.Forms.GroupBox grpDetalleOt;
        private System.Windows.Forms.Label lblNroOt;
        private System.Windows.Forms.TextBox txtNroOt;
        private System.Windows.Forms.Label lblEstadoOt;
        private System.Windows.Forms.TextBox txtEstadoOt;
        private System.Windows.Forms.Label lblMontoPresupuesto;
        private System.Windows.Forms.TextBox txtMontoPresupuesto;
        private System.Windows.Forms.Label lblTecnicoAsignado;
        private System.Windows.Forms.TextBox txtTecnicoAsignado;
        private System.Windows.Forms.Panel pnlBotones;
        private System.Windows.Forms.Label lblGrupoRecepcion;
        private System.Windows.Forms.Button btnRecepcionarEquipo;
        private System.Windows.Forms.Label lblGrupoPresupuesto;
        private System.Windows.Forms.Button btnRegistrarAprobacion;
        private System.Windows.Forms.Button btnRegistrarRechazo;
        private System.Windows.Forms.Label lblGrupoEntrega;
        private System.Windows.Forms.Button btnEntregarEquipo;
        private System.Windows.Forms.Button btnRealizarCobro;
        private System.Windows.Forms.Button btnAplicarCambios;
        private System.Windows.Forms.Button btnSalir;
    }
}