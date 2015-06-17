using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico.ABM_Cuenta
{
    public partial class CuentaCreacion : CuentaData
    {
        public CuentaCreacion(){
            InitializeComponent();
            InputNumeroCuenta.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e){
            this.ExecStoredProcedure("NULL.spCrearCuenta");
            this.Close();
        }

    }
}
