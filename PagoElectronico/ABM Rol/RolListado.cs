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
            new RolCreacion().Show();
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
            this.SearchRol();
        }

        

        
    }
}
