using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Depositos
{
    public partial class FormDeposito : Form
    {
        public FormDeposito()
        {
            InitializeComponent();
            DbComunicator db = new DbComunicator();
            db.EjecutarQuery("SELECT Moneda_Nombre, Moneda_Simbolo FROM [GD1C2015].[NULL].[Moneda]");
            while (db.getLector().Read())
            {
                comboMoneda.Items.Add(db.getLector()["Moneda_Nombre"].ToString());
            }
        }

        private void botonRealizar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Se realizo");
        }


    }
}
