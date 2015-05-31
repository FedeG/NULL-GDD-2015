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
        List<String> roles;

        public FormSeleccionDeRol(List<String> roles)
        {
            InitializeComponent();

            foreach (string rol in roles)
            {
                comboBox1.Items.Add(rol);
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
