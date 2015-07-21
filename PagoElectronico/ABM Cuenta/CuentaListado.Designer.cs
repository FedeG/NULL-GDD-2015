namespace PagoElectronico.ABM_Cuenta
{
    partial class CuentaListado
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
            this.cuentaTable = new System.Windows.Forms.DataGridView();
            this.createCuentaButton = new System.Windows.Forms.Button();
            this.editarCuentaButton = new System.Windows.Forms.Button();
            this.BorrarButton = new System.Windows.Forms.Button();
            this.HabilitarButton = new System.Windows.Forms.Button();
            this.Acciones = new System.Windows.Forms.GroupBox();
            this.CerrarButton = new System.Windows.Forms.Button();
            this.btnSuscripcion = new System.Windows.Forms.Button();
            this.cambiarTipo = new System.Windows.Forms.Button();
            this.DeshabilitarButton = new System.Windows.Forms.Button();
            this.TipoDoc = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DocCliente = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.searchDocumentoButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ClienteUsername = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.searchUsernameButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cuentaTable)).BeginInit();
            this.Acciones.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cuentaTable
            // 
            this.cuentaTable.AllowUserToAddRows = false;
            this.cuentaTable.AllowUserToDeleteRows = false;
            this.cuentaTable.AllowUserToResizeColumns = false;
            this.cuentaTable.AllowUserToResizeRows = false;
            this.cuentaTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.cuentaTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cuentaTable.Location = new System.Drawing.Point(12, 118);
            this.cuentaTable.MultiSelect = false;
            this.cuentaTable.Name = "cuentaTable";
            this.cuentaTable.ReadOnly = true;
            this.cuentaTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cuentaTable.Size = new System.Drawing.Size(845, 225);
            this.cuentaTable.TabIndex = 0;
            // 
            // createCuentaButton
            // 
            this.createCuentaButton.Enabled = false;
            this.createCuentaButton.Location = new System.Drawing.Point(12, 368);
            this.createCuentaButton.Name = "createCuentaButton";
            this.createCuentaButton.Size = new System.Drawing.Size(98, 23);
            this.createCuentaButton.TabIndex = 3;
            this.createCuentaButton.Text = "Crear Cuenta";
            this.createCuentaButton.UseVisualStyleBackColor = true;
            this.createCuentaButton.Click += new System.EventHandler(this.createCuentaButton_Click);
            // 
            // editarCuentaButton
            // 
            this.editarCuentaButton.Location = new System.Drawing.Point(22, 19);
            this.editarCuentaButton.Name = "editarCuentaButton";
            this.editarCuentaButton.Size = new System.Drawing.Size(75, 23);
            this.editarCuentaButton.TabIndex = 5;
            this.editarCuentaButton.Text = "Editar";
            this.editarCuentaButton.UseVisualStyleBackColor = true;
            this.editarCuentaButton.Click += new System.EventHandler(this.editarCuentaButton_Click);
            // 
            // BorrarButton
            // 
            this.BorrarButton.Location = new System.Drawing.Point(277, 19);
            this.BorrarButton.Name = "BorrarButton";
            this.BorrarButton.Size = new System.Drawing.Size(75, 23);
            this.BorrarButton.TabIndex = 6;
            this.BorrarButton.Text = "Dar de Baja";
            this.BorrarButton.UseVisualStyleBackColor = true;
            this.BorrarButton.Click += new System.EventHandler(this.BorrarButton_Click);
            // 
            // HabilitarButton
            // 
            this.HabilitarButton.Location = new System.Drawing.Point(196, 19);
            this.HabilitarButton.Name = "HabilitarButton";
            this.HabilitarButton.Size = new System.Drawing.Size(75, 23);
            this.HabilitarButton.TabIndex = 9;
            this.HabilitarButton.Text = "Habilitar";
            this.HabilitarButton.UseVisualStyleBackColor = true;
            this.HabilitarButton.Click += new System.EventHandler(this.HabilitarButton_Click);
            // 
            // Acciones
            // 
            this.Acciones.Controls.Add(this.CerrarButton);
            this.Acciones.Controls.Add(this.btnSuscripcion);
            this.Acciones.Controls.Add(this.cambiarTipo);
            this.Acciones.Controls.Add(this.DeshabilitarButton);
            this.Acciones.Controls.Add(this.BorrarButton);
            this.Acciones.Controls.Add(this.HabilitarButton);
            this.Acciones.Controls.Add(this.editarCuentaButton);
            this.Acciones.Enabled = false;
            this.Acciones.Location = new System.Drawing.Point(341, 349);
            this.Acciones.Name = "Acciones";
            this.Acciones.Size = new System.Drawing.Size(516, 56);
            this.Acciones.TabIndex = 10;
            this.Acciones.TabStop = false;
            this.Acciones.Text = "Acciones";
            // 
            // CerrarButton
            // 
            this.CerrarButton.Location = new System.Drawing.Point(196, 19);
            this.CerrarButton.Name = "CerrarButton";
            this.CerrarButton.Size = new System.Drawing.Size(156, 23);
            this.CerrarButton.TabIndex = 15;
            this.CerrarButton.Text = "Cerrar Cuenta";
            this.CerrarButton.UseVisualStyleBackColor = true;
            this.CerrarButton.Visible = false;
            this.CerrarButton.Click += new System.EventHandler(this.CerrarButton_Click);
            // 
            // btnSuscripcion
            // 
            this.btnSuscripcion.Location = new System.Drawing.Point(358, 19);
            this.btnSuscripcion.Name = "btnSuscripcion";
            this.btnSuscripcion.Size = new System.Drawing.Size(133, 23);
            this.btnSuscripcion.TabIndex = 14;
            this.btnSuscripcion.Text = "Agregar Suscripciones";
            this.btnSuscripcion.UseVisualStyleBackColor = true;
            this.btnSuscripcion.Click += new System.EventHandler(this.btnSuscripcion_Click);
            // 
            // cambiarTipo
            // 
            this.cambiarTipo.Location = new System.Drawing.Point(103, 19);
            this.cambiarTipo.Name = "cambiarTipo";
            this.cambiarTipo.Size = new System.Drawing.Size(87, 23);
            this.cambiarTipo.TabIndex = 12;
            this.cambiarTipo.Text = "Cambiar Tipo";
            this.cambiarTipo.UseVisualStyleBackColor = true;
            this.cambiarTipo.Click += new System.EventHandler(this.cambiarTipo_Click);
            // 
            // DeshabilitarButton
            // 
            this.DeshabilitarButton.Location = new System.Drawing.Point(196, 19);
            this.DeshabilitarButton.Name = "DeshabilitarButton";
            this.DeshabilitarButton.Size = new System.Drawing.Size(75, 23);
            this.DeshabilitarButton.TabIndex = 11;
            this.DeshabilitarButton.Text = "Deshabilitar";
            this.DeshabilitarButton.UseVisualStyleBackColor = true;
            this.DeshabilitarButton.Visible = false;
            this.DeshabilitarButton.Click += new System.EventHandler(this.DeshabilitarButton_Click);
            // 
            // TipoDoc
            // 
            this.TipoDoc.FormattingEnabled = true;
            this.TipoDoc.Location = new System.Drawing.Point(44, 26);
            this.TipoDoc.Name = "TipoDoc";
            this.TipoDoc.Size = new System.Drawing.Size(126, 21);
            this.TipoDoc.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tipo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Número";
            // 
            // DocCliente
            // 
            this.DocCliente.Location = new System.Drawing.Point(226, 26);
            this.DocCliente.MaxLength = 20;
            this.DocCliente.Name = "DocCliente";
            this.DocCliente.Size = new System.Drawing.Size(214, 20);
            this.DocCliente.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TipoDoc);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.DocCliente);
            this.groupBox2.Controls.Add(this.searchDocumentoButton);
            this.groupBox2.Location = new System.Drawing.Point(398, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(459, 100);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Buscar por Documento";
            // 
            // searchDocumentoButton
            // 
            this.searchDocumentoButton.Location = new System.Drawing.Point(365, 61);
            this.searchDocumentoButton.Name = "searchDocumentoButton";
            this.searchDocumentoButton.Size = new System.Drawing.Size(75, 23);
            this.searchDocumentoButton.TabIndex = 4;
            this.searchDocumentoButton.Text = "Buscar";
            this.searchDocumentoButton.UseVisualStyleBackColor = true;
            this.searchDocumentoButton.Click += new System.EventHandler(this.searchDocumentoButton_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Username";
            // 
            // ClienteUsername
            // 
            this.ClienteUsername.Location = new System.Drawing.Point(67, 26);
            this.ClienteUsername.MaxLength = 255;
            this.ClienteUsername.Name = "ClienteUsername";
            this.ClienteUsername.Size = new System.Drawing.Size(295, 20);
            this.ClienteUsername.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.ClienteUsername);
            this.groupBox1.Controls.Add(this.searchUsernameButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 100);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar por Username";
            // 
            // searchUsernameButton
            // 
            this.searchUsernameButton.Location = new System.Drawing.Point(287, 61);
            this.searchUsernameButton.Name = "searchUsernameButton";
            this.searchUsernameButton.Size = new System.Drawing.Size(75, 23);
            this.searchUsernameButton.TabIndex = 4;
            this.searchUsernameButton.Text = "Buscar";
            this.searchUsernameButton.UseVisualStyleBackColor = true;
            this.searchUsernameButton.Click += new System.EventHandler(this.searchUsernameButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Mostrar Todos";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CuentaListado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 413);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Acciones);
            this.Controls.Add(this.createCuentaButton);
            this.Controls.Add(this.cuentaTable);
            this.Name = "CuentaListado";
            this.Text = "Listado Cuentas";
            this.Load += new System.EventHandler(this.CuentaListado_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cuentaTable)).EndInit();
            this.Acciones.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView cuentaTable;
        private System.Windows.Forms.Button createCuentaButton;
        private System.Windows.Forms.Button editarCuentaButton;
        private System.Windows.Forms.Button BorrarButton;
        private System.Windows.Forms.Button HabilitarButton;
        private System.Windows.Forms.GroupBox Acciones;
        private System.Windows.Forms.Button DeshabilitarButton;
        private System.Windows.Forms.Button cambiarTipo;
        private System.Windows.Forms.ComboBox TipoDoc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DocCliente;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button searchDocumentoButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ClienteUsername;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button searchUsernameButton;
        private System.Windows.Forms.Button btnSuscripcion;
        private System.Windows.Forms.Button CerrarButton;
        private System.Windows.Forms.Button button1;
    }
}
