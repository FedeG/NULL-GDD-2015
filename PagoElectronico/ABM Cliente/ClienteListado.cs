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

namespace PagoElectronico.ABM_Cliente
{
    public partial class ClienteListado : Form
    {
        DbComunicator db;
        Commons.Validator validator;
        Commons.EnabledButtons enabledButtons, enabledButtons2;

        public ClienteListado(){
            InitializeComponent();
            this.db = new DbComunicator();
            this.validator = new Commons.Validator();
            this.enabledButtons = new Commons.EnabledButtons();
            this.enabledButtons2 = new Commons.EnabledButtons();
            this.enabledButtons.RegisterTextBox(this.ClienteUsername);
            this.enabledButtons.RegisterButton(this.searchUsernameButton);
            this.enabledButtons2.RegisterTextBox(this.DocCliente);
            this.enabledButtons2.RegisterButton(this.searchDocumentoButton);
            this.DocCliente.KeyPress += this.DocCliente_KeyPress;
        }

        private void searchButton_Click(object sender, EventArgs e){
            this.SearchClientePorUsername();
        }

        void SearchClientePorUsername() {
            clienteTable.DataSource = db.GetDataAdapter("SELECT Cliente.Usr_Username, Cli_Cod, Cli_Nombre, Cli_Apellido, Cli_Nacionalidad, Cli_Dom_Calle, Cli_Dom_Nro, Cli_Borrado, Usr_Estado FROM (SELECT Usr_Username, Cli_Nombre, Cli_Cod ,Cli_Apellido, Cli_Nacionalidad, Cli_Dom_Calle, Cli_Dom_Nro, Cli_Borrado FROM GD1C2015.[NULL].Cliente WHERE Usr_Username LIKE '%" + ClienteUsername.Text + "%') AS Cliente INNER JOIN GD1C2015.[NULL].Usuario AS Usuario ON Cliente.Usr_Username=Usuario.Usr_Username").Tables[0];
        }

        void SearchClientePorDocumento(){
            clienteTable.DataSource = db.GetDataAdapter("SELECT Cliente.Usr_Username, Cli_Cod, Cli_Nombre, Cli_Apellido, Cli_Nacionalidad, Cli_Dom_Calle, Cli_Dom_Nro, Cli_Borrado, Usr_Estado FROM (SELECT Usr_Username, Cli_Nombre, Cli_Cod ,Cli_Apellido, Cli_Nacionalidad, Cli_Dom_Calle, Cli_Dom_Nro, Cli_Borrado FROM GD1C2015.[NULL].Cliente WHERE Cli_Nro_Doc LIKE '%" + DocCliente.Text + "%' AND TipoDoc_Cod="+ this.TipoDocCliente.SelectedValue+") AS Cliente INNER JOIN GD1C2015.[NULL].Usuario AS Usuario ON Cliente.Usr_Username=Usuario.Usr_Username").Tables[0];
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
            string query = "SELECT TipoDoc_Cod, TipoDoc_Desc FROM [GD1C2015].[NULL].[TipoDoc]";
            Dictionary<object, object> TiposDoc = db.GetQueryDictionary(query, "TipoDoc_Cod", "TipoDoc_Desc");
            this.TipoDocCliente.DataSource = new BindingSource(TiposDoc, null);
            this.TipoDocCliente.DisplayMember = "Value";
            this.TipoDocCliente.ValueMember = "Key";
        }
         
        private void ActivarAcciones(object sender, EventArgs e){
            if (!clienteTable.SelectedRows[0].Cells["Usr_Username"].Value.ToString().Equals(""))
            {
                Acciones.Enabled = true;
                this.LoadHabilitacionButton();
                this.LoadDarDeBajaButton();
                clienteTable.SelectionChanged += this.DesactivarAcciones;
            }
            else this.DesactivarAcciones(sender, e);
        }

        private void LoadHabilitacionButton(){
            if (clienteTable.SelectedRows[0].Cells["Usr_Estado"].Value.ToString().Equals("Habilitado"))
            {
                HabilitarButton.Enabled = false;
                HabilitarButton.Visible = false;
                DeshabilitarButton.Visible = true;
                DeshabilitarButton.Enabled = true;
            } else {
                DeshabilitarButton.Visible = false;
                DeshabilitarButton.Enabled = false;
                HabilitarButton.Visible = true;
                HabilitarButton.Enabled = true;
            }
        }

        private void LoadDarDeBajaButton(){
            if ((Boolean) clienteTable.SelectedRows[0].Cells["Cli_Borrado"].Value)
                BorrarButton.Enabled = false;
        }

        private void DesactivarAcciones(object sender, EventArgs e){
            Acciones.Enabled = false;
            clienteTable.SelectionChanged -= this.DesactivarAcciones;
        }

        private void HabilitarButton_Click(object sender, EventArgs e){
            SqlCommand spHabilitarUsuario = this.db.GetStoreProcedure("NULL.spHabilitarUsuario");
            spHabilitarUsuario.Parameters.Add(new SqlParameter("@Usr_Username", clienteTable.SelectedRows[0].Cells["Usr_Username"].Value.ToString()));
            spHabilitarUsuario.ExecuteNonQuery();
            this.SearchClientePorUsername();
        }

        private void DeshabilitarButton_Click(object sender, EventArgs e){
            SqlCommand spDeshabilitarUsuario = this.db.GetStoreProcedure("NULL.spDeshabilitarUsuario");
            spDeshabilitarUsuario.Parameters.Add(new SqlParameter("@Usr_Username", clienteTable.SelectedRows[0].Cells["Usr_Username"].Value.ToString()));
            spDeshabilitarUsuario.ExecuteNonQuery();
            this.SearchClientePorUsername();
        }

        private void BorrarButton_Click(object sender, EventArgs e){
            SqlCommand spDarDeBajaCliente = this.db.GetStoreProcedure("NULL.spDarDeBajaCliente");
            spDarDeBajaCliente.Parameters.Add(new SqlParameter("@Usr_Username", clienteTable.SelectedRows[0].Cells["Usr_Username"].Value.ToString()));
            spDarDeBajaCliente.ExecuteNonQuery();
            this.SearchClientePorUsername();
        }

        private void DocCliente_KeyPress(object sender, KeyPressEventArgs e){
            this.validator.KeyPressBinding(this.validator.validateInt, false, e);
        }

        private void button1_Click(object sender, EventArgs e){
            this.ClienteUsername.Text = "";
            this.SearchClientePorUsername();
        }

        private void TarjetasButton_Click(object sender, EventArgs e)
        {
            string username = clienteTable.SelectedRows[0].Cells[0].Value.ToString();
            Tarjetas.TarjetaListado re = new Tarjetas.TarjetaListado(username);
            re.Show();
        }

        private void username_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                this.searchUsernameButton.PerformClick();
            }
        }

        private void documento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                if (!String.IsNullOrEmpty(this.TipoDocCliente.Text))
                    this.searchDocumentoButton.PerformClick();
            }
        }

        private void DocCliente_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
