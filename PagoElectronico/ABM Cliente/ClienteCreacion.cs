using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico.ABM_Cliente
{
    public partial class ClienteCreacion : ClienteData
    {
        public ClienteCreacion(){
            InitializeComponent();
            this.enabledButtons.RegisterButton(this.button1);
        }

        private void button2_Click(object sender, EventArgs e){
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e){
            this.ExecStoredProcedure("NULL.spCrearCliente", false);
            this.Close();
        }

    }
}
