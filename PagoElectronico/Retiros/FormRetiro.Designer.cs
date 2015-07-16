namespace PagoElectronico.Retiros
{
    partial class FormRetiro
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
            this.cuentaComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tipoDocComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nroDocTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.importeTextBox = new System.Windows.Forms.TextBox();
            this.realizarButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.bancoComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboMoneda = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cuentaComboBox
            // 
            this.cuentaComboBox.FormattingEnabled = true;
            this.cuentaComboBox.Location = new System.Drawing.Point(143, 13);
            this.cuentaComboBox.Name = "cuentaComboBox";
            this.cuentaComboBox.Size = new System.Drawing.Size(134, 21);
            this.cuentaComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cuenta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tipo de Documento";
            // 
            // tipoDocComboBox
            // 
            this.tipoDocComboBox.FormattingEnabled = true;
            this.tipoDocComboBox.Location = new System.Drawing.Point(143, 56);
            this.tipoDocComboBox.Name = "tipoDocComboBox";
            this.tipoDocComboBox.Size = new System.Drawing.Size(134, 21);
            this.tipoDocComboBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nro. de Documento";
            // 
            // nroDocTextBox
            // 
            this.nroDocTextBox.Location = new System.Drawing.Point(143, 102);
            this.nroDocTextBox.Name = "nroDocTextBox";
            this.nroDocTextBox.Size = new System.Drawing.Size(134, 20);
            this.nroDocTextBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Importe";
            // 
            // importeTextBox
            // 
            this.importeTextBox.Location = new System.Drawing.Point(143, 189);
            this.importeTextBox.Name = "importeTextBox";
            this.importeTextBox.Size = new System.Drawing.Size(134, 20);
            this.importeTextBox.TabIndex = 7;
            // 
            // realizarButton
            // 
            this.realizarButton.Location = new System.Drawing.Point(202, 279);
            this.realizarButton.Name = "realizarButton";
            this.realizarButton.Size = new System.Drawing.Size(75, 23);
            this.realizarButton.TabIndex = 8;
            this.realizarButton.Text = "Realizar";
            this.realizarButton.UseVisualStyleBackColor = true;
            this.realizarButton.Click += new System.EventHandler(this.realizarButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 235);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Banco";
            // 
            // bancoComboBox
            // 
            this.bancoComboBox.FormattingEnabled = true;
            this.bancoComboBox.Location = new System.Drawing.Point(143, 232);
            this.bancoComboBox.Name = "bancoComboBox";
            this.bancoComboBox.Size = new System.Drawing.Size(134, 21);
            this.bancoComboBox.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Tipo Moneda";
            // 
            // comboMoneda
            // 
            this.comboMoneda.FormattingEnabled = true;
            this.comboMoneda.Location = new System.Drawing.Point(143, 142);
            this.comboMoneda.Name = "comboMoneda";
            this.comboMoneda.Size = new System.Drawing.Size(134, 21);
            this.comboMoneda.TabIndex = 12;
            // 
            // FormRetiro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 314);
            this.Controls.Add(this.comboMoneda);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.bancoComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.realizarButton);
            this.Controls.Add(this.importeTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nroDocTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tipoDocComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cuentaComboBox);
            this.Name = "FormRetiro";
            this.Text = "Retiro";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cuentaComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox tipoDocComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nroDocTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox importeTextBox;
        private System.Windows.Forms.Button realizarButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox bancoComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboMoneda;
    }
}