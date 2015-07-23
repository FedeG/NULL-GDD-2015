using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico.ABM_Rol
{
    public partial class RolListado : Form
    {
        DbComunicator db;
        Commons.EnabledButtons enabledButtons;
        public RolListado()
        {
            InitializeComponent();
            db = new DbComunicator();
            this.enabledButtons = new Commons.EnabledButtons();
            this.enabledButtons.RegisterTextBox(this.rolName);
            this.enabledButtons.RegisterButton(this.searchButton);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            this.SearchRol();
        }

        void SearchRol() {
           
            rolTable.DataSource = db.GetDataAdapter("SELECT * FROM [GD1C2015].[NULL].[Rol] WHERE Rol_Nombre LIKE '%" + rolName.Text + "%'").Tables[0];
        }

        void SearchRol(object sender, FormClosedEventArgs e)
        {
            this.SearchRol();
        }

        private void createRolButton_Click(object sender, EventArgs e)
        {
            new RolCreacion().ShowDialog();
            this.rolName.Text = "";
            this.SearchRol();
        }

        private void element_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                this.searchButton.PerformClick();
            }
        }

        private void editarRolButton_Click(object sender, EventArgs e)
        {
            RolEdicion re = new RolEdicion(rolTable.SelectedRows[0]);
            re.FormClosed += new FormClosedEventHandler(SearchRol);
            re.Show();
        }

        private void deshabilitarButton_Click(object sender, EventArgs e)
        {
            SqlCommand spCrearRol = this.db.GetStoreProcedure("NULL.spDeshabilitarRol");
            spCrearRol.Parameters.Add(new SqlParameter("@Rol_Pk", rolTable.SelectedRows[0].Cells["Rol_Nombre"].Value.ToString()));
            spCrearRol.ExecuteNonQuery();
            this.SearchRol();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.rolName.Text = "";
            this.SearchRol();
        }

        private void RolListado_Load(object sender, EventArgs e)
        {
            this.editarRolButton.Enabled = false;
            this.deshabilitarButton.Enabled = false;
            this.rolTable.CellClick += this.ActivarAcciones;
            this.rolTable.RowHeaderMouseClick += this.ActivarAcciones;
        }

        private void ActivarAcciones(object sender, EventArgs e)
        {
            if (!rolTable.SelectedRows[0].Cells["Rol_Nombre"].Value.ToString().Equals(""))
            {
                this.editarRolButton.Enabled = true;
                this.deshabilitarButton.Enabled = true;
                this.rolTable.SelectionChanged += this.DesactivarAcciones;
            }
            else this.DesactivarAcciones(sender, e);
        }

        private void DesactivarAcciones(object sender, EventArgs e)
        {
            this.editarRolButton.Enabled = false;
            this.deshabilitarButton.Enabled = false;
            rolTable.SelectionChanged -= this.DesactivarAcciones;
        }
        
    }
}
