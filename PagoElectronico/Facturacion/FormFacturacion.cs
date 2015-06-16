using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Facturacion
{
    public partial class FormFacturacion : Form
    {
        DbComunicator db;

        public FormFacturacion(string username){
            InitializeComponent();
            this.db = new DbComunicator();
            
            string queryItemsAPagar = "SELECT Cliente.Usr_Username, Cli_Nombre, Cli_Apellido, Cli_Nacionalidad, Cli_Dom_Calle, Cli_Dom_Nro, Cli_Borrado, Usr_Estado FROM (SELECT Usr_Username, Cli_Nombre, Cli_Apellido, Cli_Nacionalidad, Cli_Dom_Calle, Cli_Dom_Nro, Cli_Borrado FROM GD1C2015.[NULL].Cliente WHERE Usr_Username LIKE '%" + username + "%') AS Cliente INNER JOIN GD1C2015.[NULL].Usuario AS Usuario ON Cliente.Usr_Username=Usuario.Usr_Username";
            clienteTable.DataSource = db.GetDataAdapter(queryItemsAPagar).Tables[0];
        }

        private void btnSalir_Click(object sender, EventArgs e){
            this.Close();
        }
    }
}
