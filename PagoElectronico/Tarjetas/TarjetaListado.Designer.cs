﻿namespace PagoElectronico.Tarjetas
{
    partial class TarjetaListado
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
            this.tarjetaGridView = new System.Windows.Forms.DataGridView();
            this.crearButton = new System.Windows.Forms.Button();
            this.editarButton = new System.Windows.Forms.Button();
            this.desasociarButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tarjetaGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tarjetaGridView
            // 
            this.tarjetaGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tarjetaGridView.Location = new System.Drawing.Point(12, 12);
            this.tarjetaGridView.Name = "tarjetaGridView";
            this.tarjetaGridView.Size = new System.Drawing.Size(439, 266);
            this.tarjetaGridView.TabIndex = 0;
            // 
            // crearButton
            // 
            this.crearButton.Location = new System.Drawing.Point(12, 284);
            this.crearButton.Name = "crearButton";
            this.crearButton.Size = new System.Drawing.Size(75, 23);
            this.crearButton.TabIndex = 1;
            this.crearButton.Text = "Crear";
            this.crearButton.UseVisualStyleBackColor = true;
            // 
            // editarButton
            // 
            this.editarButton.Location = new System.Drawing.Point(196, 284);
            this.editarButton.Name = "editarButton";
            this.editarButton.Size = new System.Drawing.Size(75, 23);
            this.editarButton.TabIndex = 2;
            this.editarButton.Text = "Editar";
            this.editarButton.UseVisualStyleBackColor = true;
            this.editarButton.Click += new System.EventHandler(this.editarButton_Click);
            // 
            // desasociarButton
            // 
            this.desasociarButton.Location = new System.Drawing.Point(376, 284);
            this.desasociarButton.Name = "desasociarButton";
            this.desasociarButton.Size = new System.Drawing.Size(75, 23);
            this.desasociarButton.TabIndex = 3;
            this.desasociarButton.Text = "Desasociar";
            this.desasociarButton.UseVisualStyleBackColor = true;
            this.desasociarButton.Click += new System.EventHandler(this.desasociarButton_Click);
            // 
            // TarjetaListado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 323);
            this.Controls.Add(this.desasociarButton);
            this.Controls.Add(this.editarButton);
            this.Controls.Add(this.crearButton);
            this.Controls.Add(this.tarjetaGridView);
            this.Name = "TarjetaListado";
            this.Text = "Listado de Tarjetas";
            ((System.ComponentModel.ISupportInitialize)(this.tarjetaGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tarjetaGridView;
        private System.Windows.Forms.Button crearButton;
        private System.Windows.Forms.Button editarButton;
        private System.Windows.Forms.Button desasociarButton;
    }
}