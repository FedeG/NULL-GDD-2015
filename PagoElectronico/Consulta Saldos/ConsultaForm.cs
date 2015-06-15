using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Consulta_Saldos
{
    public partial class ConsultaForm : Form
    {
        DbComunicator db;
        public ConsultaForm(string username)
        {
            InitializeComponent();
            string query = "SELECT Cuenta_Numero FROM [GD1C2015].[NULL].[Cuenta] WHERE Cli_Cod = (SELECT Cliente.Cli_Cod FROM [GD1C2015].[NULL].[Cliente] as Cliente WHERE Usr_Username = '" + username + "')";
            db = new DbComunicator();
            cuentaComboBox.DataSource = new BindingSource(db.GetQueryDictionary(query, "Cuenta_Numero", "Cuenta_Numero"), null);
            cuentaComboBox.DisplayMember = "Key";
            cuentaComboBox.ValueMember = "Value";
        }

        private void consultaButton_Click(object sender, EventArgs e)
        {
            string cuentaNumero = cuentaComboBox.SelectedValue.ToString();
            string querySaldo = "SELECT Cuenta_Saldo FROM [GD1C2015].[NULL].[Cuenta] WHERE Cuenta_Numero = " + cuentaNumero;
            db.EjecutarQuery(querySaldo);
            db.getLector().Read();
            saldoLabel.Text = "Su saldo es : U$S " + db.getLector()["Cuenta_Saldo"].ToString();

            string queryDepositos = "SELECT TOP 5 d.Deposito_Codigo Codigo, d.Deposito_Importe Importe, d.Deposito_Fecha Fecha, t.Tarjeta_Numero_Visible Numero ";
            queryDepositos = queryDepositos + "FROM [GD1C2015].[NULL].[Deposito] as d, [GD1C2015].[NULL].[Tarjeta] as t WHERE t.Tarjeta_Numero = d.Tarjeta_Numero AND d.Cuenta_Numero = " + cuentaNumero;
            queryDepositos = queryDepositos + " ORDER BY Deposito_Fecha DESC";
            depositosGridView.DataSource = db.GetDataAdapter(queryDepositos).Tables[0];

            string queryRetiros = "SELECT TOP 5 Retiro_Codigo Codigo, Retiro_Importe Importe, Retiro_Fecha Fecha, Cheque_Numero Cheque ";
            queryRetiros = queryRetiros + "FROM [GD1C2015].[NULL].[Retiro] WHERE Cuenta_Numero = " + cuentaNumero;
            queryRetiros = queryRetiros + " ORDER BY Retiro_Fecha DESC";
            retirosGridView.DataSource = db.GetDataAdapter(queryRetiros).Tables[0];

            string queryTransferencias = "SELECT TOP 10 Transf_Codigo Codigo, Transf_Importe Importe, Transf_Fecha Fecha, Cuenta_Origen_Numero Cuenta_Origen, Cuenta_Destino_Numero Cuenta_Destino ";
            queryTransferencias = queryTransferencias + "FROM [GD1C2015].[NULL].[Transferencia] WHERE Cuenta_Origen_Numero = " + cuentaNumero;
            queryTransferencias = queryTransferencias + " OR Cuenta_Destino_Numero = " + cuentaNumero;
            queryTransferencias = queryTransferencias + " ORDER BY Transf_Fecha DESC";
            transferenciasGridView.DataSource = db.GetDataAdapter(queryTransferencias).Tables[0];


        }
    }
}
