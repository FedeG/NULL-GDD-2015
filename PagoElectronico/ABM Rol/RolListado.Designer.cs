namespace PagoElectronico.ABM_Rol
{
    partial class RolListado
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
            this.rolTable = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.rolName = new System.Windows.Forms.TextBox();
            this.createRolButton = new System.Windows.Forms.Button();
            this.searchButton = new System.Windows.Forms.Button();
            this.editarRolButton = new System.Windows.Forms.Button();
            this.deshabilitarButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.rolTable)).BeginInit();
            this.SuspendLayout();
            // 
            // rolTable
            // 
            this.rolTable.AllowUserToAddRows = false;
            this.rolTable.AllowUserToDeleteRows = false;
            this.rolTable.AllowUserToResizeColumns = false;
            this.rolTable.AllowUserToResizeRows = false;
            this.rolTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.rolTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rolTable.Location = new System.Drawing.Point(12, 64);
            this.rolTable.Name = "rolTable";
            this.rolTable.ReadOnly = true;
            this.rolTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.rolTable.Size = new System.Drawing.Size(330, 150);
            this.rolTable.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre";
            // 
            // rolName
            // 
            this.rolName.Location = new System.Drawing.Point(84, 19);
            this.rolName.Name = "rolName";
            this.rolName.Size = new System.Drawing.Size(100, 20);
            this.rolName.TabIndex = 2;
            this.rolName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.element_KeyPress);
            // 
            // createRolButton
            // 
            this.createRolButton.Location = new System.Drawing.Point(13, 238);
            this.createRolButton.Name = "createRolButton";
            this.createRolButton.Size = new System.Drawing.Size(75, 23);
            this.createRolButton.TabIndex = 3;
            this.createRolButton.Text = "Crear Rol";
            this.createRolButton.UseVisualStyleBackColor = true;
            this.createRolButton.Click += new System.EventHandler(this.createRolButton_Click);
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(199, 16);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(49, 23);
            this.searchButton.TabIndex = 4;
            this.searchButton.Text = "Buscar";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // editarRolButton
            // 
            this.editarRolButton.Location = new System.Drawing.Point(136, 238);
            this.editarRolButton.Name = "editarRolButton";
            this.editarRolButton.Size = new System.Drawing.Size(75, 23);
            this.editarRolButton.TabIndex = 5;
            this.editarRolButton.Text = "Editar";
            this.editarRolButton.UseVisualStyleBackColor = true;
            this.editarRolButton.Click += new System.EventHandler(this.editarRolButton_Click);
            // 
            // deshabilitarButton
            // 
            this.deshabilitarButton.Location = new System.Drawing.Point(267, 238);
            this.deshabilitarButton.Name = "deshabilitarButton";
            this.deshabilitarButton.Size = new System.Drawing.Size(75, 23);
            this.deshabilitarButton.TabIndex = 6;
            this.deshabilitarButton.Text = "Deshabilitar";
            this.deshabilitarButton.UseVisualStyleBackColor = true;
            this.deshabilitarButton.Click += new System.EventHandler(this.deshabilitarButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(254, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Mostrar Todos";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RolListado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 273);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.deshabilitarButton);
            this.Controls.Add(this.editarRolButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.createRolButton);
            this.Controls.Add(this.rolName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rolTable);
            this.Name = "RolListado";
            this.Text = "Listado roles";
            this.Load += new System.EventHandler(this.RolListado_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rolTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView rolTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox rolName;
        private System.Windows.Forms.Button createRolButton;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button editarRolButton;
        private System.Windows.Forms.Button deshabilitarButton;
        private System.Windows.Forms.Button button1;
    }
}