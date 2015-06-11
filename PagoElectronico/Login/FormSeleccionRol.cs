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
            Dictionary<object, object> itemsComboBox = new Dictionary<object, object>();
            while (db.getLector().Read())
            {
                itemsComboBox.Add(db.getLector()["Rol_Nombre"], db.getLector()["Rol_Nombre"]);
            }
            comboBox1.DataSource = new BindingSource(itemsComboBox, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        
    }
}
