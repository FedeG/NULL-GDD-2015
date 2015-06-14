using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using PagoElectronico.Commons;

namespace PagoElectronico.Tarjetas
{
    public partial class TarjetaData : Form
    {
        public TarjetaData()
        {
            InitializeComponent();
            DbComunicator db = new DbComunicator();
            string emisorQuery = "SELECT Emisor_Cod, Emisor_Desc FROM [GD1C2015].[NULL].[Emisor]";
            emisorComboBox.DataSource = new BindingSource(db.GetQueryDictionary(emisorQuery, "Emisor_Desc", "Emisor_Cod"), null);
            emisorComboBox.DisplayMember = "Key";
            emisorComboBox.ValueMember = "Value";
            db.CerrarConexion();
        }

        public SqlCommand setCommand(string command, int cliCod, string shaNumero, string numeroVisible, string shaCod) {
            DbComunicator db = new DbComunicator();
            
            SqlCommand insertCmd = db.GetInsert(command);

            insertCmd.Parameters.Add(new SqlParameter("@ShaTarjeta", SqlDbType.NVarChar, 255));
            insertCmd.Parameters.Add(new SqlParameter("@NumeroVisible", SqlDbType.NVarChar, 255));
            insertCmd.Parameters.Add(new SqlParameter("@ShaCod", SqlDbType.NVarChar, 255));
            insertCmd.Parameters.Add(new SqlParameter("@Emisor", SqlDbType.Int));
            insertCmd.Parameters.Add(new SqlParameter("@Cli_cod", SqlDbType.Int));
            insertCmd.Parameters.Add(new SqlParameter("@Emision", SqlDbType.DateTime));
            insertCmd.Parameters.Add(new SqlParameter("@Vencimiento", SqlDbType.DateTime));

            insertCmd.Parameters["@ShaTarjeta"].Value = shaNumero;
            insertCmd.Parameters["@NumeroVisible"].Value = numeroVisible;
            insertCmd.Parameters["@ShaCod"].Value = shaCod;
            insertCmd.Parameters["@Emisor"].Value = emisorComboBox.SelectedValue;
            insertCmd.Parameters["@Cli_Cod"].Value = cliCod;
            insertCmd.Parameters["@Emision"].Value = emisionTimePicker.Value;
            insertCmd.Parameters["@Vencimiento"].Value = vencimientoTimePicker.Value;

            return insertCmd;
        }
    }
}
