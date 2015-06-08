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
        public ClienteListado()
        {
            InitializeComponent();
            db = new DbComunicator();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            this.SearchClientePorUsername();
        }

        void SearchClientePorUsername() {
            clienteTable.DataSource = db.GetDataAdapter("SELECT * FROM [GD1C2015].[NULL].[Cliente] WHERE Cli_Nombre LIKE '%" + ClienteUsername.Text + "%'").Tables[0];
        }

        void SearchClientePorDocumento()
        {
            clienteTable.DataSource = db.GetDataAdapter("SELECT * FROM [GD1C2015].[NULL].[Cliente] WHERE Cli_Nro_Doc LIKE '%" + DocCliente.Text + "%' AND TipoDoc_Cod LIKE '%" + TipoDocCliente.Text + "%'").Tables[0];
        }

        void SearchClientePorUsername(object sender, FormClosedEventArgs e)
        {
            this.SearchClientePorUsername();
        }

        private void searchDocumentoButton_Click(object sender, EventArgs e)
        {
            this.SearchClientePorDocumento();
        }

        private void editarClienteButton_Click(object sender, EventArgs e)
        {
            ClienteEdicion re = new ClienteEdicion(clienteTable.SelectedRows[0]);
            re.FormClosed += new FormClosedEventHandler(SearchClientePorUsername);
            re.Show();
        }

        private void createClienteButton_Click(object sender, EventArgs e)
        {
            new ClienteCreacion().Show();
        }
        
    }
}
