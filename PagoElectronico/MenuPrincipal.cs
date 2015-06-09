using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal(string rol){
            InitializeComponent();
        }

        private void Salir_Click(object sender, EventArgs e){
            this.Close();
        }
    }
}
