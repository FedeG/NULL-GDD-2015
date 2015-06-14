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
        public ConsultaForm(int clienteCod)
        {
            InitializeComponent();
            string query = "SELECT Cuenta_Numero FROM [GD1C2015].[NULL].[Cuenta] WHERE Cli_Cod = " + clienteCod;
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

            string queryDepositos = "SELECT TOP 5 Deposito_Codigo, Deposito_Importe, Deposito_Fecha, Tarjeta_Numero ";
            queryDepositos =  queryDepositos + "FROM [GD1C2015].[NULL].[Deposito] WHERE Cuenta_Numero = " + cuentaNumero;
            queryDepositos = queryDepositos + " ORDER BY Deposito_Fecha";
            depositosGridView.DataSource = db.GetDataAdapter(queryDepositos);

            string queryRetiros = "SELECT TOP 5 Retiro_Codigo, Retiro_Importe, Retiro_Fecha ";
            queryRetiros = queryRetiros + "FROM [GD1C2015].[NULL].[Retiro] WHERE Cuenta_Numero = " + cuentaNumero;
            queryRetiros = queryRetiros + " ORDER BY Retiro_Fecha";
            retirosGridView.DataSource = db.GetDataAdapter(queryRetiros);

            string queryTransferencias = "SELECT TOP 10 Transf_Codigo, Transf_Importe, Transf_Fecha, Cuenta_Origen_Numero, Cuenta_Destino_Numero";
            queryTransferencias = queryTransferencias + "FROM [GD1C2015].[NULL].[Transferencia] WHERE Cuenta_Origen_Numero = " + cuentaNumero;
            queryTransferencias = queryTransferencias + " OR Cuenta_Destino_Numero = " + cuentaNumero;
            queryTransferencias = queryTransferencias + " ORDER BY Transf_Fecha";
            retirosGridView.DataSource = db.GetDataAdapter(queryTransferencias);


        }
    }
}
