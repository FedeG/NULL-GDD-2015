using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Login
{
    public partial class FormSeleccionDeRol : Form
    {

        public FormSeleccionDeRol(DbComunicator db)
        {
            InitializeComponent();
            
            while (db.getLector().Read())
            {
                comboBox1.Items.Add(db.getLector()["Rol_Nombre"].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void FormSeleccionDeRol_Load(object sender, EventArgs e)
        {

        }

        
    }
}
