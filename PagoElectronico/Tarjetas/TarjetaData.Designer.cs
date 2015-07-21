namespace PagoElectronico.Tarjetas
{
    partial class TarjetaData
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
            this.label2 = new System.Windows.Forms.Label();
            this.emisorComboBox = new System.Windows.Forms.ComboBox();
            this.emisionTimePicker = new System.Windows.Forms.DateTimePicker();
            this.vencimientoTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.seguridadTextBox = new System.Windows.Forms.TextBox();
            this.cancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Emisor";
            // 
            // emisorComboBox
            // 
            this.emisorComboBox.FormattingEnabled = true;
            this.emisorComboBox.Location = new System.Drawing.Point(131, 43);
            this.emisorComboBox.Name = "emisorComboBox";
            this.emisorComboBox.Size = new System.Drawing.Size(197, 21);
            this.emisorComboBox.TabIndex = 3;
            // 
            // emisionTimePicker
            // 
            this.emisionTimePicker.Location = new System.Drawing.Point(131, 75);
            this.emisionTimePicker.Name = "emisionTimePicker";
            this.emisionTimePicker.Size = new System.Drawing.Size(200, 20);
            this.emisionTimePicker.TabIndex = 4;
            // 
            // vencimientoTimePicker
            // 
            this.vencimientoTimePicker.Location = new System.Drawing.Point(131, 106);
            this.vencimientoTimePicker.Name = "vencimientoTimePicker";
            this.vencimientoTimePicker.Size = new System.Drawing.Size(200, 20);
            this.vencimientoTimePicker.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Fecha de emision";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Fecha de vencimiento";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Codigo de Seguridad";
            // 
            // seguridadTextBox
            // 
            this.seguridadTextBox.Location = new System.Drawing.Point(131, 138);
            this.seguridadTextBox.Name = "seguridadTextBox";
            this.seguridadTextBox.PasswordChar = '*';
            this.seguridadTextBox.Size = new System.Drawing.Size(197, 20);
            this.seguridadTextBox.TabIndex = 9;
            // 
            // cancelar
            // 
            this.cancelar.Location = new System.Drawing.Point(16, 172);
            this.cancelar.Name = "cancelar";
            this.cancelar.Size = new System.Drawing.Size(87, 31);
            this.cancelar.TabIndex = 10;
            this.cancelar.Text = "Cancelar";
            this.cancelar.UseVisualStyleBackColor = true;
            this.cancelar.Click += new System.EventHandler(this.cancelar_Click);
            // 
            // TarjetaData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 215);
            this.Controls.Add(this.cancelar);
            this.Controls.Add(this.seguridadTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.vencimientoTimePicker);
            this.Controls.Add(this.emisionTimePicker);
            this.Controls.Add(this.emisorComboBox);
            this.Controls.Add(this.label2);
            this.Name = "TarjetaData";
            this.Text = "Datos de Tarjeta";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox emisorComboBox;
        public System.Windows.Forms.DateTimePicker emisionTimePicker;
        public System.Windows.Forms.DateTimePicker vencimientoTimePicker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox seguridadTextBox;
        private System.Windows.Forms.Button cancelar;
    }
}