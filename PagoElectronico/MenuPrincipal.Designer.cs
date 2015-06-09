namespace PagoElectronico
{
    partial class MenuPrincipal
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
            this.Salir = new System.Windows.Forms.Button();
            this.Abrir = new System.Windows.Forms.Button();
            this.Funcionalidades = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Seleccione el Formularios";
            // 
            // Salir
            // 
            this.Salir.Location = new System.Drawing.Point(172, 66);
            this.Salir.Name = "Salir";
            this.Salir.Size = new System.Drawing.Size(75, 23);
            this.Salir.TabIndex = 9;
            this.Salir.Text = "Salir";
            this.Salir.UseVisualStyleBackColor = true;
            this.Salir.Click += new System.EventHandler(this.Salir_Click);
            // 
            // Abrir
            // 
            this.Abrir.Location = new System.Drawing.Point(12, 66);
            this.Abrir.Name = "Abrir";
            this.Abrir.Size = new System.Drawing.Size(75, 23);
            this.Abrir.TabIndex = 10;
            this.Abrir.Text = "Abrir";
            this.Abrir.UseVisualStyleBackColor = true;
            this.Abrir.Click += new System.EventHandler(this.Abrir_Click);
            // 
            // Funcionalidades
            // 
            this.Funcionalidades.FormattingEnabled = true;
            this.Funcionalidades.Location = new System.Drawing.Point(13, 29);
            this.Funcionalidades.Name = "Funcionalidades";
            this.Funcionalidades.Size = new System.Drawing.Size(234, 21);
            this.Funcionalidades.TabIndex = 11;
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 103);
            this.Controls.Add(this.Funcionalidades);
            this.Controls.Add(this.Abrir);
            this.Controls.Add(this.Salir);
            this.Controls.Add(this.label1);
            this.Name = "MenuPrincipal";
            this.Text = "Menu Principal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Salir;
        private System.Windows.Forms.Button Abrir;
        private System.Windows.Forms.ComboBox Funcionalidades;
    }
}

