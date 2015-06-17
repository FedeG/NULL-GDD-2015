namespace PagoElectronico.Consulta_Saldos
{
    partial class ConsultaForm
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
            this.cuentaComboBox = new System.Windows.Forms.ComboBox();
            this.consultarClienteButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.depositosGridView = new System.Windows.Forms.DataGridView();
            this.retirosGridView = new System.Windows.Forms.DataGridView();
            this.transferenciasGridView = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.saldoLabel = new System.Windows.Forms.Label();
            this.autocomplete = new System.Windows.Forms.TextBox();
            this.consultarAdminButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.depositosGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.retirosGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transferenciasGridView)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cuenta";
            // 
            // cuentaComboBox
            // 
            this.cuentaComboBox.FormattingEnabled = true;
            this.cuentaComboBox.Location = new System.Drawing.Point(85, 10);
            this.cuentaComboBox.Name = "cuentaComboBox";
            this.cuentaComboBox.Size = new System.Drawing.Size(121, 21);
            this.cuentaComboBox.TabIndex = 1;
            // 
            // consultarClienteButton
            // 
            this.consultarClienteButton.Location = new System.Drawing.Point(374, 441);
            this.consultarClienteButton.Name = "consultarClienteButton";
            this.consultarClienteButton.Size = new System.Drawing.Size(125, 23);
            this.consultarClienteButton.TabIndex = 2;
            this.consultarClienteButton.Text = "Consultar Saldo";
            this.consultarClienteButton.UseVisualStyleBackColor = true;
            this.consultarClienteButton.Click += new System.EventHandler(this.consultaButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ultimos 5 depositos.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(182, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ultimas 10 transferencias.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(182, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Ultimos 5 retiros.";
            // 
            // depositosGridView
            // 
            this.depositosGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.depositosGridView.Location = new System.Drawing.Point(25, 52);
            this.depositosGridView.Name = "depositosGridView";
            this.depositosGridView.Size = new System.Drawing.Size(424, 290);
            this.depositosGridView.TabIndex = 6;
            // 
            // retirosGridView
            // 
            this.retirosGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.retirosGridView.Location = new System.Drawing.Point(25, 52);
            this.retirosGridView.Name = "retirosGridView";
            this.retirosGridView.Size = new System.Drawing.Size(424, 290);
            this.retirosGridView.TabIndex = 7;
            // 
            // transferenciasGridView
            // 
            this.transferenciasGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.transferenciasGridView.Location = new System.Drawing.Point(25, 52);
            this.transferenciasGridView.Name = "transferenciasGridView";
            this.transferenciasGridView.Size = new System.Drawing.Size(424, 293);
            this.transferenciasGridView.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(16, 44);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(480, 374);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.depositosGridView);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(472, 348);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Depositos";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.retirosGridView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(472, 348);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Retiros";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.transferenciasGridView);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(472, 348);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Transferencias";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // saldoLabel
            // 
            this.saldoLabel.AutoSize = true;
            this.saldoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saldoLabel.Location = new System.Drawing.Point(17, 441);
            this.saldoLabel.Name = "saldoLabel";
            this.saldoLabel.Size = new System.Drawing.Size(0, 13);
            this.saldoLabel.TabIndex = 12;
            // 
            // autocomplete
            // 
            this.autocomplete.Location = new System.Drawing.Point(85, 10);
            this.autocomplete.Name = "autocomplete";
            this.autocomplete.Size = new System.Drawing.Size(137, 20);
            this.autocomplete.TabIndex = 13;
            // 
            // consultarAdminButton
            // 
            this.consultarAdminButton.Location = new System.Drawing.Point(374, 441);
            this.consultarAdminButton.Name = "consultarAdminButton";
            this.consultarAdminButton.Size = new System.Drawing.Size(125, 23);
            this.consultarAdminButton.TabIndex = 14;
            this.consultarAdminButton.Text = "Consultar Saldo";
            this.consultarAdminButton.UseVisualStyleBackColor = true;
            this.consultarAdminButton.Click += new System.EventHandler(this.consultarAdminButton_Click);
            // 
            // ConsultaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 476);
            this.Controls.Add(this.consultarAdminButton);
            this.Controls.Add(this.autocomplete);
            this.Controls.Add(this.saldoLabel);
            this.Controls.Add(this.consultarClienteButton);
            this.Controls.Add(this.cuentaComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Name = "ConsultaForm";
            this.Text = "Consulta Saldo";
            ((System.ComponentModel.ISupportInitialize)(this.depositosGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.retirosGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transferenciasGridView)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cuentaComboBox;
        private System.Windows.Forms.Button consultarClienteButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView depositosGridView;
        private System.Windows.Forms.DataGridView retirosGridView;
        private System.Windows.Forms.DataGridView transferenciasGridView;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label saldoLabel;
        private System.Windows.Forms.TextBox autocomplete;
        private System.Windows.Forms.Button consultarAdminButton;
    }
}