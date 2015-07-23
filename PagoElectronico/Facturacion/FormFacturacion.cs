using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico.Facturacion {

    public partial class FormFacturacion : Form
    {
        DbComunicator db;
        string username;

        public FormFacturacion(){
            InitializeComponent();
            this.db = new DbComunicator();
            string queryTransaccAPagar = "SELECT Transacc_Codigo, Transacc_Cantidad, Transacc_Importe, Transacc_Detalle, Moneda_Nombre FROM [GD1C2015].[NULL].[Transaccion] WHERE Transacc_Borrado = 0 AND Transacc_Facturada = 0";
            transaccTable.DataSource = db.GetDataAdapter(queryTransaccAPagar).Tables[0];
            btnFacturar.Enabled = false;
        }

        public FormFacturacion(string username){
            InitializeComponent();
            this.db = new DbComunicator();
            this.username = username;
            this.loadTransaccTable();

            string query = "SELECT Cli_Cod FROM [GD1C2015].[NULL].[Cliente] WHERE Usr_Username = '" + username + "'";
            this.db.EjecutarQuery(query);
            this.db.getLector().Read();
            int cli_Cod = Convert.ToInt32(this.db.getLector()["Cli_Cod"]);
            this.db.CerrarConexion();

            string queryCuentas = "SELECT Cuenta_Numero FROM [GD1C2015].[NULL].[Cuenta] WHERE Cli_Cod = " + cli_Cod;
            Dictionary<object, object> cuentas = this.db.GetQueryDictionary(queryCuentas, "Cuenta_Numero", "Cuenta_Numero");
            cuentas.Add("Todas las cuentas", "Todas las cuentas");
            comboCuenta.DataSource = new BindingSource(cuentas, null);
            comboCuenta.DisplayMember = "Key";
            comboCuenta.ValueMember = "Value";
                       
            this.db.CerrarConexion();
        }

        private void loadTransaccTable(){
            string queryGetCliCod = "SELECT Cli_Cod FROM GD1C2015.[NULL].Cliente WHERE Usr_Username LIKE '%" + this.username + "%'";
            string queryTransaccAPagar = "SELECT Transacc_Codigo, Transacc_Cantidad, Transacc_Importe, Transacc_Detalle, Moneda_Nombre FROM [GD1C2015].[NULL].[Transaccion] WHERE Cli_Cod = (" + queryGetCliCod + ") AND Transacc_Borrado = 0 AND Transacc_Facturada = 0";
            transaccTable.DataSource = db.GetDataAdapter(queryTransaccAPagar).Tables[0];
        }

        private void btnSalir_Click(object sender, EventArgs e){
            this.Close();
        }

        private void btnFacturar_Click(object sender, EventArgs e){
            SqlCommand sp = this.db.GetStoreProcedure("NULL.spGenerarFactura");
            sp.Parameters.Add(new SqlParameter("@Usr_Username", this.username));
            sp.Parameters.Add(new SqlParameter("@Hoy", Properties.Settings.Default.FechaSistema));
            sp.ExecuteNonQuery();
            MessageBox.Show("Factura generada exitosamente");
            this.loadTransaccTable();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (comboCuenta.SelectedValue.ToString() == "Todas las cuentas") {
                this.loadTransaccTable();
                return;
            }
            
            string queryTransaccAPagar = "SELECT Transacc_Codigo, Transacc_Cantidad, Transacc_Importe, Transacc_Detalle, Moneda_Nombre FROM [GD1C2015].[NULL].[Transaccion] WHERE Cuenta_Numero = " + comboCuenta.SelectedValue + " AND Transacc_Borrado = 0 AND Transacc_Facturada = 0";
            transaccTable.DataSource = db.GetDataAdapter(queryTransaccAPagar).Tables[0];
        }

        private void element_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                this.btnConsultar.PerformClick();
            }
        }
    }
}
