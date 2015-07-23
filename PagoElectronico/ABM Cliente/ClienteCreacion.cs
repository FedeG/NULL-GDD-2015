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
            int resultado = this.ExecStoredProcedure("NULL.spCrearCliente", false);
            switch (resultado){
                case 0: MessageBox.Show("El usuario fue creado exitosamente."); this.Close();  break;
                case 1: MessageBox.Show("El username ya existe, por favor seleccione otro."); break;
                case 2: MessageBox.Show("El número de documento esta asignado a otro usuario, por favor ingrese otro numero o tipo de documento."); break;
                case 3: MessageBox.Show("El mail ingresado ya esta asignado a otro usuario, por favor seleccione otro mail."); break;
            }
        }

    }
}
