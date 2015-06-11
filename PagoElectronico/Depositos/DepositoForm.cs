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
    public partial class DepositoForm : Form
    {
        public DepositoForm()
        {
            InitializeComponent();
            DbComunicator db = new DbComunicator();
            string queryMonedas = "SELECT Moneda_Nombre, Moneda_Simbolo FROM [GD1C2015].[NULL].[Moneda]";
            comboMoneda.DataSource = new BindingSource(db.GetQueryDictionary(queryMonedas, "Moneda_Simbolo", "Moneda_Nombre"), null);
            comboMoneda.DisplayMember = "Key";
            comboMoneda.ValueMember = "Value";
            db.CerrarConexion();
     

            DbComunicator db1 = new DbComunicator();/* FALTA COMPLETAR SELECT QUE TRAE TARJETA DEL CLIENTE */
            int cli_Cod = 100007;
            string queryTarjetas = "SELECT Tarjeta_Numero, Tarjeta_Numero_Visible FROM [GD1C2015].[NULL].[Tarjeta] WHERE Cli_Cod= " + cli_Cod;
            comboTarjeta.DataSource = new BindingSource(db1.GetQueryDictionary(queryTarjetas, "Tarjeta_Numero_Visible", "Tarjeta_Numero"), null);
            comboTarjeta.DisplayMember = "Key";
            comboTarjeta.ValueMember = "Value";
            db1.CerrarConexion();

            DbComunicator db2 = new DbComunicator();
            string queryCuentas = "SELECT Cuenta_Numero FROM [GD1C2015].[NULL].[Cuenta] WHERE Cli_Cod = " + cli_Cod;
            comboCuenta.DataSource = new BindingSource(db2.GetQueryDictionary(queryCuentas, "Cuenta_Numero", "Cuenta_Numero"), null);
            comboCuenta.DisplayMember = "Key";
            comboCuenta.ValueMember = "Value";
            db2.CerrarConexion();
        }
  

        private void botonRealizar_Click(object sender, EventArgs e)
        {

         /* Falta agregar las chequeos de Tarjeta y los campos incompletos (moneda y cuentas) */
            
           int num = Int32.Parse(importeTextBox.Text);
        
             if ( num >= 1 ) //& ( fechaVencTarjeta >= DateTime.Now )
               
               MessageBox.Show("Se realizo");
            else
                MessageBox.Show("Importe invalido");
           // if ( fechaVencTarjeta > DateTime.Now ) 
               
            
        }

    }
}
