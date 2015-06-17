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
            string queryGetCliCod = "SELECT Cli_Cod FROM GD1C2015.[NULL].Cliente WHERE Usr_Username LIKE '%" + username + "%'";
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
        }
    }
}
