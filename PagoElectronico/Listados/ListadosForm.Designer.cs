namespace PagoElectronico.Listados
{
    partial class ListadosForm
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
            this.listadoTable = new System.Windows.Forms.DataGridView();
            this.listarButton = new System.Windows.Forms.Button();
            this.cbListado = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbTrimestre = new System.Windows.Forms.ComboBox();
            this.tbAnio = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.listadoTable)).BeginInit();
            this.SuspendLayout();
            // 
            // listadoTable
            // 
            this.listadoTable.AllowUserToAddRows = false;
            this.listadoTable.AllowUserToDeleteRows = false;
            this.listadoTable.AllowUserToResizeColumns = false;
            this.listadoTable.AllowUserToResizeRows = false;
            this.listadoTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.listadoTable.Location = new System.Drawing.Point(22, 100);
            this.listadoTable.Name = "listadoTable";
            this.listadoTable.ReadOnly = true;
            this.listadoTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.listadoTable.Size = new System.Drawing.Size(575, 221);
            this.listadoTable.TabIndex = 0;
            // 
            // listarButton
            // 
            this.listarButton.Location = new System.Drawing.Point(522, 336);
            this.listarButton.Name = "listarButton";
            this.listarButton.Size = new System.Drawing.Size(75, 23);
            this.listarButton.TabIndex = 1;
            this.listarButton.Text = "Listar";
            this.listarButton.UseVisualStyleBackColor = true;
            this.listarButton.Click += new System.EventHandler(this.listarButton_Click);
            // 
            // cbListado
            // 
            this.cbListado.FormattingEnabled = true;
            this.cbListado.Location = new System.Drawing.Point(25, 61);
            this.cbListado.Name = "cbListado";
            this.cbListado.Size = new System.Drawing.Size(572, 21);
            this.cbListado.TabIndex = 2;
            this.cbListado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Trimestre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(235, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Año";
            // 
            // cbTrimestre
            // 
            this.cbTrimestre.FormattingEnabled = true;
            this.cbTrimestre.Location = new System.Drawing.Point(78, 12);
            this.cbTrimestre.Name = "cbTrimestre";
            this.cbTrimestre.Size = new System.Drawing.Size(135, 21);
            this.cbTrimestre.TabIndex = 5;
            // 
            // tbAnio
            // 
            this.tbAnio.Location = new System.Drawing.Point(267, 12);
            this.tbAnio.MaxLength = 4;
            this.tbAnio.Name = "tbAnio";
            this.tbAnio.Size = new System.Drawing.Size(121, 20);
            this.tbAnio.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Seleccionar Listado";
            // 
            // ListadosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 371);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbAnio);
            this.Controls.Add(this.cbTrimestre);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbListado);
            this.Controls.Add(this.listarButton);
            this.Controls.Add(this.listadoTable);
            this.Name = "ListadosForm";
            this.Text = "Listados Estadisticos";
            ((System.ComponentModel.ISupportInitialize)(this.listadoTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView listadoTable;
        private System.Windows.Forms.Button listarButton;
        private System.Windows.Forms.ComboBox cbListado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbTrimestre;
        private System.Windows.Forms.TextBox tbAnio;
        private System.Windows.Forms.Label label3;
    }
}