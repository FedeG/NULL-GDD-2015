﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using PagoElectronico.Commons;

namespace PagoElectronico.ABM_Cuenta
{
    public partial class CuentaListado : Form
    {
        DbComunicator db;
        Commons.Validator validator;
        string username, cliCod;
        Commons.EnabledButtons enabledButtons, enabledButtons2;
        Boolean is_admin;

        public CuentaListado(){
            InitializeComponent();
            this.db = new DbComunicator();
            this.validator = new Commons.Validator();
            this.is_admin = true;
            this.enabledButtons = new Commons.EnabledButtons();
            this.enabledButtons2 = new Commons.EnabledButtons();
            this.enabledButtons.RegisterTextBox(this.ClienteUsername);
            this.enabledButtons.RegisterButton(this.searchUsernameButton);
            this.enabledButtons2.RegisterTextBox(this.DocCliente);
            this.enabledButtons2.RegisterButton(this.searchDocumentoButton);
            this.DocCliente.KeyPress += this.InputNumField_KeyPress;
        }

        private void InputNumField_KeyPress(object sender, KeyPressEventArgs e){
            this.validator.KeyPressBinding(this.validator.validateInt, false, e);
        }

        public CuentaListado(string username){
            InitializeComponent();
            this.db = new DbComunicator();
            this.validator = new Commons.Validator();
            this.username = username;
            this.is_admin = false;
            HabilitarButton.Visible = false;
            DeshabilitarButton.Visible = false;
            BorrarButton.Visible = false;
            CerrarButton.Visible = true;
            ClienteUsername.Text = username;
            string queryGetCliCod = "SELECT Cli_Cod FROM (SELECT Cli_Cod, Usr_Username FROM GD1C2015.[NULL].Cliente WHERE Usr_Username='" + username + "') AS Cliente INNER JOIN GD1C2015.[NULL].Usuario AS Usuario ON Cliente.Usr_Username=Usuario.Usr_Username";
            this.loadCuentaTable(queryGetCliCod);
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            cuentaTable.Location = new System.Drawing.Point(12, 12);
            cuentaTable.Size = new System.Drawing.Size(845, 325);
        }

        private void editarCuentaButton_Click(object sender, EventArgs e){
            CuentaEdicion re = new CuentaEdicion(cuentaTable.SelectedRows[0]);
            re.cliCod = this.cliCod;
            re.ShowDialog();
            this.SearchCuentaPorUsername();
        }

        private void createCuentaButton_Click(object sender, EventArgs e){
            CuentaCreacion FormCreacion = new CuentaCreacion();
            FormCreacion.cliCod = this.cliCod;
            FormCreacion.ShowDialog();
            this.SearchCuentaPorUsername();
        }

        private void CuentaListado_Load(object sender, EventArgs e){
            cuentaTable.CellClick += this.ActivarAcciones;
            cuentaTable.RowHeaderMouseClick += this.ActivarAcciones;
            Dictionary<object, object> TiposDoc = this.db.GetQueryDictionary("SELECT TipoDoc_Cod,TipoDoc_Desc FROM [GD1C2015].[NULL].[TipoDoc] WHERE TipoDoc_Borrado=0;", "TipoDoc_Cod", "TipoDoc_Desc");
            TipoDoc.DataSource = new BindingSource(TiposDoc, null);
            TipoDoc.DisplayMember = "Value";
            TipoDoc.ValueMember = "Key";
        }

        private void loadCuentaTable(string queryGetCliCod){
            SqlCommand sp = db.GetStoreProcedure("NULL.spDeshabilitarCuentasVencidas");
            sp.Parameters.Add(new SqlParameter("@Hoy", Properties.Settings.Default.FechaSistema));
            sp.ExecuteNonQuery();
            db.EjecutarQuery(queryGetCliCod);
            db.getLector().Read();
            try{
                this.cliCod = db.getLector()["Cli_Cod"].ToString();
                db.CerrarConexion();
                createCuentaButton.Enabled = true;
                string query = "SELECT Cuenta_Numero,Cuenta_Estado,Cuenta_Fecha_Vencimiento,Cuenta_Saldo,TipoCta_Nombre,Moneda_Nombre,Cuenta_Borrado FROM [GD1C2015].[NULL].[Cuenta] WHERE Cli_Cod = '" + this.cliCod + "'";
                cuentaTable.DataSource = db.GetDataAdapter(query).Tables[0];
            }
            catch (System.InvalidOperationException e){
                MessageBox.Show("No existen cuentas para el usuario buscado");
            }
        }
         
        private void ActivarAcciones(object sender, EventArgs e){
            if (!cuentaTable.SelectedRows[0].Cells["Cuenta_Numero"].Value.ToString().Equals("")){
                Acciones.Enabled = true;
                this.LoadHabilitacionButton();
                this.LoadDarDeBajaButton();
                cuentaTable.SelectionChanged += this.DesactivarAcciones;
            }
            else this.DesactivarAcciones(sender, e);
        }

        private void LoadHabilitacionButton(){
            if (this.is_admin){
                if (cuentaTable.SelectedRows[0].Cells["Cuenta_Estado"].Value.ToString().Equals("Habilitada")){
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
        }

        private void LoadDarDeBajaButton(){
            BorrarButton.Enabled = !(Boolean)cuentaTable.SelectedRows[0].Cells["Cuenta_Borrado"].Value;
            CerrarButton.Enabled = !(Boolean)cuentaTable.SelectedRows[0].Cells["Cuenta_Borrado"].Value;
        }

        private void DesactivarAcciones(object sender, EventArgs e){
            Acciones.Enabled = false;
            cuentaTable.SelectionChanged -= this.DesactivarAcciones;
        }

        private void HabilitarButton_Click(object sender, EventArgs e){
            SqlCommand spHabilitarUsuario = this.db.GetStoreProcedure("NULL.spHabilitarCuenta");
            spHabilitarUsuario.Parameters.Add(new SqlParameter("@Cuenta_Numero", cuentaTable.SelectedRows[0].Cells["Cuenta_Numero"].Value.ToString()));
            spHabilitarUsuario.ExecuteNonQuery();
            this.SearchCuentaPorUsername();
        }

        private void DeshabilitarButton_Click(object sender, EventArgs e){
            SqlCommand spDeshabilitarUsuario = this.db.GetStoreProcedure("NULL.spDeshabilitarCuenta");
            spDeshabilitarUsuario.Parameters.Add(new SqlParameter("@Cuenta_Numero", cuentaTable.SelectedRows[0].Cells["Cuenta_Numero"].Value.ToString()));
            spDeshabilitarUsuario.Parameters.Add(new SqlParameter("@Hoy", PagoElectronico.Properties.Settings.Default.FechaSistema));
            spDeshabilitarUsuario.ExecuteNonQuery();
            this.SearchCuentaPorUsername();
        }

        private void BorrarButton_Click(object sender, EventArgs e){
            SqlCommand spDarDeBajaCuenta = this.db.GetStoreProcedure("NULL.spDarDeBajaCuenta");
            spDarDeBajaCuenta.Parameters.Add(new SqlParameter("@Cuenta_Numero", cuentaTable.SelectedRows[0].Cells["Cuenta_Numero"].Value.ToString()));
            spDarDeBajaCuenta.ExecuteNonQuery();
            this.SearchCuentaPorUsername();
        }

        private void cambiarTipo_Click(object sender, EventArgs e){
            FormSeleccionTipo formTipo = new FormSeleccionTipo();
            formTipo.ShowDialog();
            // spConsultaCambioTipoCuenta
            SqlCommand spConsultaCambioTipoCuenta = this.db.GetStoreProcedure("NULL.spConsultaCambioTipoCuenta");
            spConsultaCambioTipoCuenta.Parameters.Add(new SqlParameter("@Cuenta_Numero", Convert.ToInt64(cuentaTable.SelectedRows[0].Cells["Cuenta_Numero"].Value)));
            spConsultaCambioTipoCuenta.Parameters.Add(new SqlParameter("@Hoy", Properties.Settings.Default.FechaSistema.Date));
            SqlParameter returnParameter = spConsultaCambioTipoCuenta.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            spConsultaCambioTipoCuenta.ExecuteNonQuery();
            if (Convert.ToInt64(returnParameter.Value) >= 0){
                int dinero = Convert.ToInt32(returnParameter.Value);
                DialogResult dialogResult = MessageBox.Show("¿Usted esta seguro de cambiar el tipo de cuenta (en el caso de que usted confirme se se le acreditaran " + dinero.ToString() + ")?", "Confirmación", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes){
                    SqlCommand spCambiarTipoCuenta = this.db.GetStoreProcedure("NULL.spCambiarTipoCuenta");
                    spCambiarTipoCuenta.Parameters.Add(new SqlParameter("@Cuenta_Numero", Convert.ToInt64(cuentaTable.SelectedRows[0].Cells["Cuenta_Numero"].Value)));
                    spCambiarTipoCuenta.Parameters.Add(new SqlParameter("@TipoCta_Nombre", formTipo.tipoSeleccionado));
                    spCambiarTipoCuenta.Parameters.Add(new SqlParameter("@Hoy", Properties.Settings.Default.FechaSistema));
                    spCambiarTipoCuenta.ExecuteNonQuery();
                }
            }
            else MessageBox.Show("Su cuenta no puede ser modificada");
            this.SearchCuentaPorUsername();
        }

        private void searchButton_Click(object sender, EventArgs e){
            this.SearchCuentaPorUsername();
        }

        void SearchCuentaPorUsername(){
            string queryGetCliCod;
            if (this.is_admin)
            {
                queryGetCliCod = "SELECT Cli_Cod FROM (SELECT Cli_Cod, Usr_Username FROM GD1C2015.[NULL].Cliente WHERE Usr_Username LIKE '%" + ClienteUsername.Text + "%') AS Cliente INNER JOIN GD1C2015.[NULL].Usuario AS Usuario ON Cliente.Usr_Username=Usuario.Usr_Username";
            }
            else {
                queryGetCliCod = "SELECT Cli_Cod FROM GD1C2015.[NULL].Cliente WHERE Usr_Username = '" + this.username + "'";
            }
            
            this.loadCuentaTable(queryGetCliCod);
        }

        void SearchCuentaPorDocumento(){
            string queryGetCliCod = "SELECT Cli_Cod FROM (SELECT Usr_Username, Cli_Cod FROM GD1C2015.[NULL].Cliente WHERE Cli_Nro_Doc LIKE '%" + DocCliente.Text + "%' AND TipoDoc_Cod=" + this.TipoDoc.SelectedValue + ") AS Cliente INNER JOIN GD1C2015.[NULL].Usuario AS Usuario ON Cliente.Usr_Username=Usuario.Usr_Username";
            MessageBox.Show(queryGetCliCod);
            this.loadCuentaTable(queryGetCliCod);
        }

        private void searchUsernameButton_Click(object sender, EventArgs e){
            this.SearchCuentaPorUsername();
        }

        private void searchDocumentoButton_Click_1(object sender, EventArgs e){
            this.SearchCuentaPorDocumento();
        }

        private void btnSuscripcion_Click(object sender, EventArgs e){
            FormSuscripcion formSuscripcion = new FormSuscripcion(Convert.ToInt64(cuentaTable.SelectedRows[0].Cells["Cuenta_Numero"].Value));
            formSuscripcion.ShowDialog();
            this.SearchCuentaPorUsername();
        }

        private void CerrarButton_Click(object sender, EventArgs e)
        {
            SqlCommand spConsultaCierreCuenta = this.db.GetStoreProcedure("NULL.spConsultaCierreCuenta");
            spConsultaCierreCuenta.Parameters.Add(new SqlParameter("@Cuenta_Numero", Convert.ToInt64(cuentaTable.SelectedRows[0].Cells["Cuenta_Numero"].Value)));
            SqlParameter returnParameter = spConsultaCierreCuenta.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            spConsultaCierreCuenta.ExecuteNonQuery();
            if (Convert.ToInt64(returnParameter.Value) == 1){
                DialogResult dialogResult = MessageBox.Show("¿Usted esta seguro de cerrar su cuenta?", "Confirmación", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlCommand spCerrarCuenta = this.db.GetStoreProcedure("NULL.spCerrarCuenta");
                    spCerrarCuenta.Parameters.Add(new SqlParameter("@Cuenta_Numero", Convert.ToInt64(cuentaTable.SelectedRows[0].Cells["Cuenta_Numero"].Value)));
                    spCerrarCuenta.Parameters.Add(new SqlParameter("@Hoy", Properties.Settings.Default.FechaSistema));
                    spCerrarCuenta.ExecuteNonQuery();
                }
            }
            else MessageBox.Show("Su cuenta no se puede cerrar porque hay deudas pendientes");
            this.SearchCuentaPorUsername();
        }

        private void button1_Click(object sender, EventArgs e){
            this.ClienteUsername.Text = "";
            this.SearchCuentaPorUsername();
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
                if (!String.IsNullOrEmpty(this.TipoDoc.Text))
                    this.searchDocumentoButton.PerformClick();
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
