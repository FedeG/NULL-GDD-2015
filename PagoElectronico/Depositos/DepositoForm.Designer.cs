namespace PagoElectronico.Depositos
{
    partial class FormDeposito
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboMoneda = new System.Windows.Forms.ComboBox();
            this.botonRealizar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboTarjeta = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.maskedImporte = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboCuenta = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tipo Moneda";
            // 
            // comboMoneda
            // 
            this.comboMoneda.FormattingEnabled = true;
            this.comboMoneda.Location = new System.Drawing.Point(104, 91);
            this.comboMoneda.Name = "comboMoneda";
            this.comboMoneda.Size = new System.Drawing.Size(125, 21);
            this.comboMoneda.TabIndex = 1;
            this.comboMoneda.SelectedIndexChanged += new System.EventHandler(this.comboMoneda_SelectedIndexChanged);
            // 
            // botonRealizar
            // 
            this.botonRealizar.Location = new System.Drawing.Point(205, 238);
            this.botonRealizar.Name = "botonRealizar";
            this.botonRealizar.Size = new System.Drawing.Size(75, 23);
            this.botonRealizar.TabIndex = 2;
            this.botonRealizar.Text = "Realizar";
            this.botonRealizar.UseVisualStyleBackColor = true;
            this.botonRealizar.Click += new System.EventHandler(this.botonRealizar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(305, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tarjeta";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // comboTarjeta
            // 
            this.comboTarjeta.FormattingEnabled = true;
            this.comboTarjeta.Location = new System.Drawing.Point(360, 17);
            this.comboTarjeta.Name = "comboTarjeta";
            this.comboTarjeta.Size = new System.Drawing.Size(121, 21);
            this.comboTarjeta.TabIndex = 4;
            this.comboTarjeta.SelectedIndexChanged += new System.EventHandler(this.comboTarjeta_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(305, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Importe";
            // 
            // maskedImporte
            // 
            this.maskedImporte.Location = new System.Drawing.Point(360, 92);
            this.maskedImporte.Mask = "99999";
            this.maskedImporte.MinimumSize = new System.Drawing.Size(4, 4);
            this.maskedImporte.Name = "maskedImporte";
            this.maskedImporte.PromptChar = ' ';
            this.maskedImporte.RejectInputOnFirstFailure = true;
            this.maskedImporte.Size = new System.Drawing.Size(121, 20);
            this.maskedImporte.TabIndex = 6;
            this.maskedImporte.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePrompt;
            this.maskedImporte.ValidatingType = typeof(int);
            this.maskedImporte.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.maskedImporte_MaskInputRejected);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Cuenta";
            // 
            // comboCuenta
            // 
            this.comboCuenta.FormattingEnabled = true;
            this.comboCuenta.Location = new System.Drawing.Point(104, 17);
            this.comboCuenta.Name = "comboCuenta";
            this.comboCuenta.Size = new System.Drawing.Size(121, 21);
            this.comboCuenta.TabIndex = 8;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(104, 153);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(203, 20);
            this.dateTimePicker1.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Fecha Deposito";
            // 
            // FormDeposito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 273);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.comboCuenta);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.maskedImporte);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboTarjeta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.botonRealizar);
            this.Controls.Add(this.comboMoneda);
            this.Controls.Add(this.label1);
            this.Name = "FormDeposito";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormDeposito_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboMoneda;
        private System.Windows.Forms.Button botonRealizar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboTarjeta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox maskedImporte;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboCuenta;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label5;
    }
}