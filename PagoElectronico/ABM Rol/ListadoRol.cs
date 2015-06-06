using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Rol
{
    public partial class ListadoRol : Form
    {
        public ListadoRol()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            DbComunicator db = new DbComunicator();
            rolTable.DataSource = db.GetDataAdapter("SELECT * FROM [GD1C2015].[NULL].[Rol] WHERE Rol_Nombre LIKE '%" + rolName.Text + "%'").Tables[0];
        }

        
    }
}
