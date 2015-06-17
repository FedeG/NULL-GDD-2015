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

namespace PagoElectronico.ABM_Cuenta
{
    public partial class CuentaListado : Form
    {
        DbComunicator db;
        Commons.Validator validator;
        string username, cliCod;

        public CuentaListado(){
            InitializeComponent();
            this.db = new DbComunicator();
            this.validator = new Commons.Validator();
        }

        public CuentaListado(string username){
            InitializeComponent();
            this.db = new DbComunicator();
            this.validator = new Commons.Validator();
            this.username = username;
            ClienteUsername.Text = username;
            this.SearchCuentaPorUsername();
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
            db.EjecutarQuery(queryGetCliCod);
            db.getLector().Read();
            this.cliCod = db.getLector()["Cli_Cod"].ToString();
            db.CerrarConexion();
            createCuentaButton.Enabled = true;
            string query = "SELECT Cuenta_Numero,Cuenta_Estado,Cuenta_Fecha_Vencimiento,Cuenta_Saldo,TipoCta_Nombre,Moneda_Nombre,Cuenta_Borrado FROM [GD1C2015].[NULL].[Cuenta] WHERE Cli_Cod = '" + this.cliCod + "'";
            cuentaTable.DataSource = db.GetDataAdapter(query).Tables[0];
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
            if (cuentaTable.SelectedRows[0].Cells["Cuenta_Estado"].Value.ToString().Equals("Habilitada"))
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
            BorrarButton.Enabled = !(Boolean) cuentaTable.SelectedRows[0].Cells["Cuenta_Borrado"].Value;
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
            string queryGetCliCod = "SELECT Cli_Cod FROM (SELECT Cli_Cod, Usr_Username FROM GD1C2015.[NULL].Cliente WHERE Usr_Username LIKE '%" + ClienteUsername.Text + "%') AS Cliente INNER JOIN GD1C2015.[NULL].Usuario AS Usuario ON Cliente.Usr_Username=Usuario.Usr_Username";
            this.loadCuentaTable(queryGetCliCod);
        }

        void SearchCuentaPorDocumento(){
            string queryGetCliCod = "SELECT Cli_Cod FROM (SELECT Usr_Username, Cli_Cod FROM GD1C2015.[NULL].Cliente WHERE Cli_Nro_Doc LIKE '%" + DocCliente.Text + "%' AND TipoDoc_Cod LIKE '%" + TipoDoc.SelectedValue + "%') AS Cliente INNER JOIN GD1C2015.[NULL].Usuario AS Usuario ON Cliente.Usr_Username=Usuario.Usr_Username";
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

    }
}
