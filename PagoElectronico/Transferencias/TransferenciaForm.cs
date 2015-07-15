using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico.Transferencias
{
    public partial class TransferenciaForm : Form
    {
        public TransferenciaForm(string username){
            string query = "SELECT Cuenta_Numero FROM [GD1C2015].[NULL].[Cuenta] WHERE Cli_Cod = (SELECT Cliente.Cli_Cod FROM [GD1C2015].[NULL].[Cliente] as Cliente WHERE Usr_Username = '" + username + "')";
            InitializeComponent();
            DbComunicator db = new DbComunicator();
            
            cuentaOrigenComboBox.DataSource = new BindingSource(db.GetQueryDictionary(query, "Cuenta_Numero", "Cuenta_Numero"), null);
            cuentaOrigenComboBox.DisplayMember = "Key";
            cuentaOrigenComboBox.ValueMember = "Value";
            
            string queryMonedas = "SELECT Moneda_Nombre, Moneda_Simbolo FROM [GD1C2015].[NULL].[Moneda]";
            comboMoneda.DataSource = new BindingSource(db.GetQueryDictionary(queryMonedas, "Moneda_Simbolo", "Moneda_Nombre"), null);
            comboMoneda.DisplayMember = "Key";
            comboMoneda.ValueMember = "Value";

            db.CerrarConexion();
        }

        private void realizarButton_Click(object sender, EventArgs e)
        {
            DbComunicator db = new DbComunicator();
            SqlCommand spRealizarTransferencia = db.GetStoreProcedure("NULL.spRealizarTransferencia");
            SqlParameter returnParameter = spRealizarTransferencia.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            spRealizarTransferencia.Parameters.Add(new SqlParameter("@Cuenta_Origen", Convert.ToInt64(cuentaOrigenComboBox.SelectedValue)));
            spRealizarTransferencia.Parameters.Add(new SqlParameter("@Cuenta_Destino", Convert.ToInt64(cuentaDestinoTextBox.Text)));
            spRealizarTransferencia.Parameters.Add(new SqlParameter("@Importe", Convert.ToInt32(importeTextBox.Text)));
            spRealizarTransferencia.Parameters.Add(new SqlParameter("@Fecha_Transferencia", Properties.Settings.Default.FechaSistema));
            spRealizarTransferencia.Parameters.Add(new SqlParameter("@Moneda_Nombre", comboMoneda.SelectedValue.ToString()));

            //Agregar la fecha de sistema.
            spRealizarTransferencia.ExecuteNonQuery();

            if ((int)returnParameter.Value == 0)
            {
                this.Close();
            }

            if ((int)returnParameter.Value == 1)
            {
                MessageBox.Show("La cuenta de destino debe estar habilitada o inhabilitada para poder recibir dinero.");
            }

            if ((int)returnParameter.Value == 2)
            {
                MessageBox.Show("El Importe debe ser mayor que 0.");
            }

            if ((int)returnParameter.Value == 3)
            {
                MessageBox.Show("El saldo disponible es insuficiente para realizar la transferencia.");
            }
        }
    }
}
