namespace PagoElectronico.ABM_Cuenta
{
    partial class CuentaData
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.InputTipoCuenta = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.InputFechaApertura = new System.Windows.Forms.DateTimePicker();
            this.InputPaisCuenta = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.InputTipoMoneda = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.InputNumeroCuenta = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.InputTipoCuenta);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.InputFechaApertura);
            this.groupBox2.Controls.Add(this.InputPaisCuenta);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.InputTipoMoneda);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.InputNumeroCuenta);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Location = new System.Drawing.Point(15, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(357, 218);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cuenta";
            // 
            // InputTipoCuenta
            // 
            this.InputTipoCuenta.FormattingEnabled = true;
            this.InputTipoCuenta.Location = new System.Drawing.Point(116, 168);
            this.InputTipoCuenta.Name = "InputTipoCuenta";
            this.InputTipoCuenta.Size = new System.Drawing.Size(216, 21);
            this.InputTipoCuenta.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Tipo de Cuenta";
            // 
            // InputFechaApertura
            // 
            this.InputFechaApertura.Location = new System.Drawing.Point(116, 103);
            this.InputFechaApertura.Name = "InputFechaApertura";
            this.InputFechaApertura.Size = new System.Drawing.Size(216, 20);
            this.InputFechaApertura.TabIndex = 28;
            // 
            // InputPaisCuenta
            // 
            this.InputPaisCuenta.FormattingEnabled = true;
            this.InputPaisCuenta.Location = new System.Drawing.Point(75, 66);
            this.InputPaisCuenta.Name = "InputPaisCuenta";
            this.InputPaisCuenta.Size = new System.Drawing.Size(257, 21);
            this.InputPaisCuenta.TabIndex = 23;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(16, 107);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(95, 13);
            this.label19.TabIndex = 24;
            this.label19.Text = "Fecha de Apertura";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(16, 69);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(27, 13);
            this.label18.TabIndex = 22;
            this.label18.Text = "Pais";
            // 
            // InputTipoMoneda
            // 
            this.InputTipoMoneda.FormattingEnabled = true;
            this.InputTipoMoneda.Location = new System.Drawing.Point(116, 138);
            this.InputTipoMoneda.Name = "InputTipoMoneda";
            this.InputTipoMoneda.Size = new System.Drawing.Size(216, 21);
            this.InputTipoMoneda.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 141);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Tipo de Moneda";
            // 
            // InputNumeroCuenta
            // 
            this.InputNumeroCuenta.Location = new System.Drawing.Point(75, 34);
            this.InputNumeroCuenta.Name = "InputNumeroCuenta";
            this.InputNumeroCuenta.Size = new System.Drawing.Size(257, 20);
            this.InputNumeroCuenta.TabIndex = 11;
            this.InputNumeroCuenta.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 37);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Numero";
            // 
            // CuentaData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 247);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CuentaData";
            this.Text = "CuentaData";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.ComboBox InputTipoMoneda;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox InputNumeroCuenta;
        public System.Windows.Forms.Label label11;
        public System.Windows.Forms.ComboBox InputPaisCuenta;
        public System.Windows.Forms.Label label19;
        public System.Windows.Forms.Label label18;
        public System.Windows.Forms.DateTimePicker InputFechaApertura;
        public System.Windows.Forms.ComboBox InputTipoCuenta;
        public System.Windows.Forms.Label label1;
    }
}
