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
        DbComunicator db;

        public DepositoForm(string username){
            InitializeComponent();
            this.db = new DbComunicator();
            string queryMonedas = "SELECT Moneda_Nombre, Moneda_Simbolo FROM [GD1C2015].[NULL].[Moneda]";
            comboMoneda.DataSource = new BindingSource(this.db.GetQueryDictionary(queryMonedas, "Moneda_Simbolo", "Moneda_Nombre"), null);
            comboMoneda.DisplayMember = "Key";
            comboMoneda.ValueMember = "Value";
            this.db.CerrarConexion();

            string query = "SELECT Cli_Cod FROM [GD1C2015].[NULL].[Cliente] WHERE Usr_Username = '" + username + "'";
            this.db.EjecutarQuery(query);
            this.db.getLector().Read();
            int cli_Cod = Convert.ToInt32(this.db.getLector()["Cli_Cod"]);
            this.db.CerrarConexion();

            /* FALTA COMPLETAR SELECT QUE TRAE TARJETA DEL CLIENTE */
            string queryTarjetas = "SELECT Tarjeta_Numero, Tarjeta_Numero_Visible FROM [GD1C2015].[NULL].[Tarjeta] WHERE Cli_Cod= " + cli_Cod;
            comboTarjeta.DataSource = new BindingSource(this.db.GetQueryDictionary(queryTarjetas, "Tarjeta_Numero_Visible", "Tarjeta_Numero"), null);
            comboTarjeta.DisplayMember = "Key";
            comboTarjeta.ValueMember = "Value";
            this.db.CerrarConexion();

            string queryCuentas = "SELECT Cuenta_Numero FROM [GD1C2015].[NULL].[Cuenta] WHERE Cli_Cod = " + cli_Cod;
            comboCuenta.DataSource = new BindingSource(this.db.GetQueryDictionary(queryCuentas, "Cuenta_Numero", "Cuenta_Numero"), null);
            comboCuenta.DisplayMember = "Key";
            comboCuenta.ValueMember = "Value";
            this.db.CerrarConexion();
        }
  

        private void botonRealizar_Click(object sender, EventArgs e){
            SqlCommand spRealizarDeposito = this.db.GetStoreProcedure("NULL.spRealizarDeposito");
            SqlParameter returnParameter = spRealizarDeposito.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            spRealizarDeposito.Parameters.Add(new SqlParameter("@Cuenta_Numero", comboCuenta.SelectedValue));
            spRealizarDeposito.Parameters.Add(new SqlParameter("@Importe", importeTextBox.Text));
            spRealizarDeposito.Parameters.Add(new SqlParameter("@Fecha_Deposito", Properties.Settings.Default.FechaSistema));
            spRealizarDeposito.Parameters.Add(new SqlParameter("@Moneda_Nombre", comboMoneda.SelectedValue.ToString()));
            spRealizarDeposito.Parameters.Add(new SqlParameter("@Tarjeta_Numero", comboTarjeta.SelectedValue.ToString()));            

            spRealizarDeposito.ExecuteNonQuery();           
            
            switch ((int)returnParameter.Value){
                case 0: MessageBox.Show("Deposito realizado."); break;
                case 1: MessageBox.Show("Importe menor a 0."); break;
                case 2: MessageBox.Show("Tarjeta vencida."); break;
                case 3: MessageBox.Show("Cuenta inexistente."); break;
                case 4: MessageBox.Show("La cuenta debe encontrarse habilitada para poder realizar el deposito."); break;
            }
        }

    }
}
