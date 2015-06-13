namespace PagoElectronico.ABM_Cliente
{
    partial class ClienteListado
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
            this.clienteTable = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.ClienteUsername = new System.Windows.Forms.TextBox();
            this.createClienteButton = new System.Windows.Forms.Button();
            this.searchUsernameButton = new System.Windows.Forms.Button();
            this.editarClienteButton = new System.Windows.Forms.Button();
            this.BorrarButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TipoDocCliente = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DocCliente = new System.Windows.Forms.TextBox();
            this.searchDocumentoButton = new System.Windows.Forms.Button();
            this.HabilitarButton = new System.Windows.Forms.Button();
            this.Acciones = new System.Windows.Forms.GroupBox();
            this.DeshabilitarButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.clienteTable)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Acciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // clienteTable
            // 
            this.clienteTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.clienteTable.Location = new System.Drawing.Point(12, 119);
            this.clienteTable.Name = "clienteTable";
            this.clienteTable.ReadOnly = true;
            this.clienteTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.clienteTable.Size = new System.Drawing.Size(845, 150);
            this.clienteTable.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username";
            // 
            // ClienteUsername
            // 
            this.ClienteUsername.Location = new System.Drawing.Point(67, 26);
            this.ClienteUsername.Name = "ClienteUsername";
            this.ClienteUsername.Size = new System.Drawing.Size(295, 20);
            this.ClienteUsername.TabIndex = 2;
            // 
            // createClienteButton
            // 
            this.createClienteButton.Location = new System.Drawing.Point(13, 294);
            this.createClienteButton.Name = "createClienteButton";
            this.createClienteButton.Size = new System.Drawing.Size(75, 23);
            this.createClienteButton.TabIndex = 3;
            this.createClienteButton.Text = "Crear Ciente";
            this.createClienteButton.UseVisualStyleBackColor = true;
            this.createClienteButton.Click += new System.EventHandler(this.createClienteButton_Click);
            // 
            // searchUsernameButton
            // 
            this.searchUsernameButton.Location = new System.Drawing.Point(287, 61);
            this.searchUsernameButton.Name = "searchUsernameButton";
            this.searchUsernameButton.Size = new System.Drawing.Size(75, 23);
            this.searchUsernameButton.TabIndex = 4;
            this.searchUsernameButton.Text = "Buscar";
            this.searchUsernameButton.UseVisualStyleBackColor = true;
            this.searchUsernameButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // editarClienteButton
            // 
            this.editarClienteButton.Location = new System.Drawing.Point(22, 19);
            this.editarClienteButton.Name = "editarClienteButton";
            this.editarClienteButton.Size = new System.Drawing.Size(75, 23);
            this.editarClienteButton.TabIndex = 5;
            this.editarClienteButton.Text = "Editar";
            this.editarClienteButton.UseVisualStyleBackColor = true;
            this.editarClienteButton.Click += new System.EventHandler(this.editarClienteButton_Click);
            // 
            // BorrarButton
            // 
            this.BorrarButton.Location = new System.Drawing.Point(184, 19);
            this.BorrarButton.Name = "BorrarButton";
            this.BorrarButton.Size = new System.Drawing.Size(75, 23);
            this.BorrarButton.TabIndex = 6;
            this.BorrarButton.Text = "Dar de Baja";
            this.BorrarButton.UseVisualStyleBackColor = true;
            this.BorrarButton.Click += new System.EventHandler(this.BorrarButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ClienteUsername);
            this.groupBox1.Controls.Add(this.searchUsernameButton);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 100);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar por Username";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TipoDocCliente);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.DocCliente);
            this.groupBox2.Controls.Add(this.searchDocumentoButton);
            this.groupBox2.Location = new System.Drawing.Point(398, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(459, 100);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Buscar por Documento";
            // 
            // TipoDocCliente
            // 
            this.TipoDocCliente.FormattingEnabled = true;
            this.TipoDocCliente.Location = new System.Drawing.Point(44, 26);
            this.TipoDocCliente.Name = "TipoDocCliente";
            this.TipoDocCliente.Size = new System.Drawing.Size(126, 21);
            this.TipoDocCliente.TabIndex = 6;
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
            this.DocCliente.Name = "DocCliente";
            this.DocCliente.Size = new System.Drawing.Size(214, 20);
            this.DocCliente.TabIndex = 2;
            this.DocCliente.KeyPress += this.DocCliente_KeyPress;
            // 
            // searchDocumentoButton
            // 
            this.searchDocumentoButton.Location = new System.Drawing.Point(365, 61);
            this.searchDocumentoButton.Name = "searchDocumentoButton";
            this.searchDocumentoButton.Size = new System.Drawing.Size(75, 23);
            this.searchDocumentoButton.TabIndex = 4;
            this.searchDocumentoButton.Text = "Buscar";
            this.searchDocumentoButton.UseVisualStyleBackColor = true;
            this.searchDocumentoButton.Click += new System.EventHandler(this.searchDocumentoButton_Click);
            // 
            // HabilitarButton
            // 
            this.HabilitarButton.Location = new System.Drawing.Point(103, 19);
            this.HabilitarButton.Name = "HabilitarButton";
            this.HabilitarButton.Size = new System.Drawing.Size(75, 23);
            this.HabilitarButton.TabIndex = 9;
            this.HabilitarButton.Text = "Habilitar";
            this.HabilitarButton.UseVisualStyleBackColor = true;
            this.HabilitarButton.Click += new System.EventHandler(this.HabilitarButton_Click);
            // 
            // Acciones
            // 
            this.Acciones.Controls.Add(this.DeshabilitarButton);
            this.Acciones.Controls.Add(this.BorrarButton);
            this.Acciones.Controls.Add(this.HabilitarButton);
            this.Acciones.Controls.Add(this.editarClienteButton);
            this.Acciones.Enabled = false;
            this.Acciones.Location = new System.Drawing.Point(579, 275);
            this.Acciones.Name = "Acciones";
            this.Acciones.Size = new System.Drawing.Size(278, 56);
            this.Acciones.TabIndex = 10;
            this.Acciones.TabStop = false;
            this.Acciones.Text = "Acciones";
            // 
            // DeshabilitarButton
            // 
            this.DeshabilitarButton.Location = new System.Drawing.Point(103, 19);
            this.DeshabilitarButton.Name = "DeshabilitarButton";
            this.DeshabilitarButton.Size = new System.Drawing.Size(75, 23);
            this.DeshabilitarButton.TabIndex = 11;
            this.DeshabilitarButton.Text = "Deshabilitar";
            this.DeshabilitarButton.UseVisualStyleBackColor = true;
            this.DeshabilitarButton.Visible = false;
            this.DeshabilitarButton.Click += new System.EventHandler(this.DeshabilitarButton_Click);
            // 
            // ClienteListado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 339);
            this.Controls.Add(this.Acciones);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.createClienteButton);
            this.Controls.Add(this.clienteTable);
            this.Name = "ClienteListado";
            this.Text = "Listado Clientes";
            this.Load += new System.EventHandler(this.ClienteListado_Load);
            ((System.ComponentModel.ISupportInitialize)(this.clienteTable)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.Acciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView clienteTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ClienteUsername;
        private System.Windows.Forms.Button createClienteButton;
        private System.Windows.Forms.Button searchUsernameButton;
        private System.Windows.Forms.Button editarClienteButton;
        private System.Windows.Forms.Button BorrarButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DocCliente;
        private System.Windows.Forms.Button searchDocumentoButton;
        private System.Windows.Forms.ComboBox TipoDocCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button HabilitarButton;
        private System.Windows.Forms.GroupBox Acciones;
        private System.Windows.Forms.Button DeshabilitarButton;
    }
}
