using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

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
            DbComunicator db = new DbComunicator();
            SqlCommand spRealizarDeposito = db.GetStoreProcedure("NULL.spRealizarDeposito");
            SqlParameter returnParameter = spRealizarDeposito.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            spRealizarDeposito.Parameters.Add(new SqlParameter("@Cuenta_Numero", comboCuenta.SelectedValue));
            spRealizarDeposito.Parameters.Add(new SqlParameter("@Moneda_Nombre", comboMoneda.SelectedValue.ToString()));
            spRealizarDeposito.Parameters.Add(new SqlParameter("@Tarjeta_Numero", comboTarjeta.SelectedValue.ToString()));
            spRealizarDeposito.Parameters.Add(new SqlParameter("@Importe", importeTextBox.Text));
            spRealizarDeposito.Parameters.Add(new SqlParameter("@Fecha_Deposito", fechaDeposito.Value));

            spRealizarDeposito.ExecuteNonQuery();           
            
            if ((int)returnParameter.Value == 0) { MessageBox.Show("Deposito realizado."); }
            if ((int)returnParameter.Value == 1) { MessageBox.Show("Importe menor a 0."); }
            if ((int)returnParameter.Value == 2) { MessageBox.Show("Tarjeta vencida."); }
            if ((int)returnParameter.Value == 3) { MessageBox.Show("Cuenta inexistente."); }
            if ((int)returnParameter.Value == 4) { MessageBox.Show("La cuenta debe encontrarse habilitada para poder realizar el deposito.");}
        }

    }
}
