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
    public partial class CuentaEdicion : CuentaData
    {
        string cuentaNumero;

        public CuentaEdicion(DataGridViewRow selected){
            InitializeComponent();
            this.db = new DbComunicator();
            this.InputTipoCuenta.Enabled = false;
            this.LoadCuentaData(selected);
        }

        private void LoadCuentaData(DataGridViewRow selected){
            this.cuentaNumero = selected.Cells["Cuenta_Numero"].Value.ToString();
            InputNumeroCuenta.Text = this.cuentaNumero;
            db.EjecutarQuery("SELECT * FROM [GD1C2015].[NULL].[Cuenta] WHERE Cuenta_Numero = '" + this.cuentaNumero + "'");
            while (db.getLector().Read()) {
                InputTipoMoneda.Text = this.TipoMoneda[db.getLector()["Moneda_Nombre"]].ToString();
                InputTipoCuenta.Text = this.TipoCuenta[db.getLector()["TipoCta_Nombre"]].ToString();
                InputFechaApertura.Value = Convert.ToDateTime(db.getLector()["Cuenta_Fecha_Creacion"].ToString());
            }
            this.InputFechaApertura.Enabled = false;
            this.InputNumeroCuenta.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e){
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e){
            this.ExecStoredProcedure("NULL.spEditarCuenta");
            this.Close();
        }

    }
}
