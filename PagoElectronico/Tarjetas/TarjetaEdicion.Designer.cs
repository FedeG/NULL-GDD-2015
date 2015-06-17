namespace PagoElectronico.Tarjetas
{
    partial class TarjetaEdicion
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
            this.editarButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numeroTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // editarButton
            // 
            this.editarButton.Location = new System.Drawing.Point(252, 180);
            this.editarButton.Name = "editarButton";
            this.editarButton.Size = new System.Drawing.Size(75, 23);
            this.editarButton.TabIndex = 10;
            this.editarButton.Text = "Editar";
            this.editarButton.UseVisualStyleBackColor = true;
            this.editarButton.Click += new System.EventHandler(this.editarButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Numero";
            // 
            // numeroTextBox
            // 
            this.numeroTextBox.Location = new System.Drawing.Point(131, 14);
            this.numeroTextBox.Name = "numeroTextBox";
            this.numeroTextBox.Size = new System.Drawing.Size(196, 20);
            this.numeroTextBox.TabIndex = 12;
            // 
            // TarjetaEdicion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 215);
            this.Controls.Add(this.numeroTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editarButton);
            this.Name = "TarjetaEdicion";
            this.Text = "Edicion Tarjeta";
            this.Controls.SetChildIndex(this.emisorComboBox, 0);
            this.Controls.SetChildIndex(this.emisionTimePicker, 0);
            this.Controls.SetChildIndex(this.vencimientoTimePicker, 0);
            this.Controls.SetChildIndex(this.seguridadTextBox, 0);
            this.Controls.SetChildIndex(this.editarButton, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.numeroTextBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button editarButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox numeroTextBox;
    }
}