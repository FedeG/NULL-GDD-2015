namespace PagoElectronico.ABM_Rol
{
    partial class RolData
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
            this.rolNameBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboEstado = new System.Windows.Forms.ComboBox();
            this.funcionalidadesListBox = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre del rol";
            // 
            // rolNameBox
            // 
            this.rolNameBox.Location = new System.Drawing.Point(121, 18);
            this.rolNameBox.Name = "rolNameBox";
            this.rolNameBox.Size = new System.Drawing.Size(100, 20);
            this.rolNameBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Estado";
            // 
            // comboEstado
            // 
            this.comboEstado.FormattingEnabled = true;
            this.comboEstado.Location = new System.Drawing.Point(121, 55);
            this.comboEstado.Name = "comboEstado";
            this.comboEstado.Size = new System.Drawing.Size(100, 21);
            this.comboEstado.TabIndex = 5;
            // 
            // funcionalidadesListBox
            // 
            this.funcionalidadesListBox.FormattingEnabled = true;
            this.funcionalidadesListBox.Location = new System.Drawing.Point(57, 128);
            this.funcionalidadesListBox.Name = "funcionalidadesListBox";
            this.funcionalidadesListBox.Size = new System.Drawing.Size(120, 94);
            this.funcionalidadesListBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(72, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Funcionalidades";
            // 
            // RolData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 273);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.funcionalidadesListBox);
            this.Controls.Add(this.comboEstado);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rolNameBox);
            this.Controls.Add(this.label1);
            this.Name = "RolData";
            this.Text = "RolData";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox rolNameBox;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox comboEstado;
        public System.Windows.Forms.CheckedListBox funcionalidadesListBox;
        private System.Windows.Forms.Label label3;
    }
}