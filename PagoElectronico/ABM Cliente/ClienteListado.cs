using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico.ABM_Cliente
{
    public partial class ClienteListado : Form
    {
        DbComunicator db;

        public ClienteListado(){
            InitializeComponent();
            db = new DbComunicator();
        }

        private void searchButton_Click(object sender, EventArgs e){
            this.SearchClientePorUsername();
        }

        void SearchClientePorUsername() {
            clienteTable.DataSource = db.GetDataAdapter("SELECT Cliente.Usr_Username, Cli_Nombre, Cli_Apellido, Cli_Nacionalidad, Cli_Dom_Calle, Cli_Dom_Nro, Cli_Borrado, Usr_Estado FROM (SELECT Usr_Username, Cli_Nombre, Cli_Apellido, Cli_Nacionalidad, Cli_Dom_Calle, Cli_Dom_Nro, Cli_Borrado FROM GD1C2015.[NULL].Cliente WHERE Usr_Username LIKE '%" + ClienteUsername.Text + "%') AS Cliente INNER JOIN GD1C2015.[NULL].Usuario AS Usuario ON Cliente.Usr_Username=Usuario.Usr_Username").Tables[0];
        }

        void SearchClientePorDocumento(){
            clienteTable.DataSource = db.GetDataAdapter("SELECT Cliente.Usr_Username, Cli_Nombre, Cli_Apellido, Cli_Nacionalidad, Cli_Dom_Calle, Cli_Dom_Nro, Cli_Borrado, Usr_Estado FROM (SELECT Usr_Username, Cli_Nombre, Cli_Apellido, Cli_Nacionalidad, Cli_Dom_Calle, Cli_Dom_Nro, Cli_Borrado FROM GD1C2015.[NULL].Cliente WHERE Cli_Nro_Doc LIKE '%" + DocCliente.Text + "%' AND TipoDoc_Cod LIKE '%" + TipoDocCliente.Text + "%') AS Cliente INNER JOIN GD1C2015.[NULL].Usuario AS Usuario ON Cliente.Usr_Username=Usuario.Usr_Username").Tables[0];
        }

        void SearchClientePorUsername(object sender, FormClosedEventArgs e){
            this.SearchClientePorUsername();
        }

        private void searchDocumentoButton_Click(object sender, EventArgs e){
            this.SearchClientePorDocumento();
        }

        private void editarClienteButton_Click(object sender, EventArgs e){
            ClienteEdicion re = new ClienteEdicion(clienteTable.SelectedRows[0]);
            re.FormClosed += new FormClosedEventHandler(SearchClientePorUsername);
            re.Show();
        }

        private void createClienteButton_Click(object sender, EventArgs e){
            new ClienteCreacion().Show();
        }

        private void ClienteListado_Load(object sender, EventArgs e){
            clienteTable.CellClick += this.ActivarAcciones;
            clienteTable.RowHeaderMouseClick += this.ActivarAcciones;
            this.db.EjecutarQuery("SELECT TipoDoc_Desc FROM [GD1C2015].[NULL].[TipoDoc]");
            while (this.db.getLector().Read()){
                string nombre = this.db.getLector()["TipoDoc_Desc"].ToString();
                TipoDocCliente.Items.Add(nombre);
            }
        }
         
        private void ActivarAcciones(object sender, EventArgs e){
            if (!clienteTable.SelectedRows[0].Cells["Usr_Username"].Value.ToString().Equals(""))
            {
                Acciones.Enabled = true;
                if (clienteTable.SelectedRows[0].Cells["Usr_Estado"].Value.ToString().Equals("Habilitado"))
                    HabilitarButton.Enabled = false;
                if (clienteTable.SelectedRows[0].Cells["Cli_Borrado"].Value.Equals(1))
                    BorrarButton.Enabled = false;
                clienteTable.SelectionChanged += this.DesactivarAcciones;
            }
            else this.DesactivarAcciones(sender, e);
        }

        private void DesactivarAcciones(object sender, EventArgs e){
            Acciones.Enabled = false;
            clienteTable.SelectionChanged -= this.DesactivarAcciones;
        }
    }
}
