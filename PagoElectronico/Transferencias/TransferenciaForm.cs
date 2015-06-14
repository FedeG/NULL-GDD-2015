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
            string query = "SELECT Cuenta_Numero FROM [GD1C2015].[NULL].[Cuenta] WHERE Cli_Cod = (SELECT Cliente.Cli_Cod FROM [GD1C2015].[NULL].[Cliente] as Cliente WHERE Usr_Username = " + username + ")";
            // TODO 
            InitializeComponent();
        }

        private void realizarButton_Click(object sender, EventArgs e)
        {
            DbComunicator db = new DbComunicator();
            SqlCommand spRealizarTransferencia = db.GetStoreProcedure("NULL.spRealizarTransferencia");
            SqlParameter returnParameter = spRealizarTransferencia.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            spRealizarTransferencia.Parameters.Add(new SqlParameter("@Cuenta_Origen", cuentaOrigenComboBox.SelectedValue));
            spRealizarTransferencia.Parameters.Add(new SqlParameter("@Cuenta_Destino", cuentaDestinoTextBox.Text));
            spRealizarTransferencia.Parameters.Add(new SqlParameter("@Importe", importeTextBox.Text));
            spRealizarTransferencia.Parameters.Add(new SqlParameter("@Fecha_Transferencia", Properties.Settings.Default.FechaSistema));

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
