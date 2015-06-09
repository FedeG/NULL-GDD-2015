using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Cliente
{
    public partial class ClienteData : Form
    {
        public DbComunicator db;

        public ClienteData(){
            InitializeComponent();
            this.db = new DbComunicator();
        }

        private void ClienteData_Load(object sender, EventArgs e){
            this.Choice_Load("TipoDoc_Desc", "TipoDoc", TipoDocCliente);
            this.Choice_Load("Nac_nombre", "Nacionalidad", NacCliente);
        }

        private void Choice_Load(string field, string db_name, ComboBox combobox){
            this.db.EjecutarQuery("SELECT "+field+" FROM [GD1C2015].[NULL].["+db_name+"]");
            while (this.db.getLector().Read()){
                string nombre = this.db.getLector()[field].ToString();
                if (nombre != "") combobox.Items.Add(nombre);
            }
        }

    }
}
