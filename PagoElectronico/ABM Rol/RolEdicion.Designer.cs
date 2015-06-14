namespace PagoElectronico.ABM_Rol
{
    partial class RolEdicion
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
            this.commitEditButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // commitEditButton
            // 
            this.commitEditButton.Location = new System.Drawing.Point(146, 238);
            this.commitEditButton.Name = "commitEditButton";
            this.commitEditButton.Size = new System.Drawing.Size(75, 23);
            this.commitEditButton.TabIndex = 8;
            this.commitEditButton.Text = "Editar";
            this.commitEditButton.UseVisualStyleBackColor = true;
            this.commitEditButton.Click += new System.EventHandler(this.commitEditButton_Click);
            // 
            // RolEdicion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 273);
            this.Controls.Add(this.commitEditButton);
            this.Name = "RolEdicion";
            this.Text = "RolEdicion";
            this.Controls.SetChildIndex(this.commitEditButton, 0);
            this.Controls.SetChildIndex(this.rolNameBox, 0);
            this.Controls.SetChildIndex(this.comboEstado, 0);
            this.Controls.SetChildIndex(this.funcionalidadesListBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button commitEditButton;
    }
}