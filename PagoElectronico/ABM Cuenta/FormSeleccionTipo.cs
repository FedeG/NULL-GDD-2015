using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Cuenta
{
    public partial class FormSeleccionTipo : Form
    {
        DbComunicator db;
        public string tipoSeleccionado;

        public FormSeleccionTipo(){
            InitializeComponent();
            this.db = new DbComunicator();
            string query = "SELECT TipoCta_Nombre FROM [GD1C2015].[NULL].[TipoCuenta] WHERE TipoCta_Borrado=0;";
            Dictionary<object, object> TiposCuenta = db.GetQueryDictionary(query, "TipoCta_Nombre", "TipoCta_Nombre");
            comboBox1.DataSource = new BindingSource(TiposCuenta, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
        }

        private void button1_Click(object sender, EventArgs e){
            this.tipoSeleccionado = comboBox1.SelectedValue.ToString();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void element_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                this.button1.PerformClick();
            }
        }
    }
}
