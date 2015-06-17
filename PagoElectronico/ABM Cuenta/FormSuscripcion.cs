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
    public partial class FormSuscripcion : Form
    {
        DbComunicator db;
        public FormSuscripcion()
        {
            InitializeComponent();
            this.db = new DbComunicator();
            string query = "SELECT TipoCta_Nombre, TipoCta_Costo_Apertura, TipoCta_Duracion FROM [GD1C2015].[NULL].[TipoCuenta] WHERE TipoCta_Borrado = 0";
            tiposTable.DataSource = db.GetDataAdapter(query).Tables[0];
            var levels = Enumerable.Range(1, 500);
            comboBox1.DataSource = levels;
        }
    }
}
