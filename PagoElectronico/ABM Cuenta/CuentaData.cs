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
    public partial class CuentaData : Form
    {
        public DbComunicator db;
        public Commons.Validator validator;
        public string cliCod;
        public Dictionary<object, object> PaisDict, TipoCuenta, TipoMoneda;

        public CuentaData(){
            InitializeComponent();
            this.db = new DbComunicator();
            this.validator = new Commons.Validator();
            this.Nacionalidades_Load();
            this.Tipo_Moneda_Load();
            this.Tipo_Cuenta_Load();
        }

        private void Nacionalidades_Load(){
            this.db.ConectarConDB();
            this.PaisDict = db.GetQueryDictionary("SELECT Pais_Codigo, Pais_Desc FROM GD1C2015.[NULL].Pais WHERE Pais_Borrado = 0", "Pais_Codigo", "Pais_Desc");
            InputPaisCuenta.DataSource = new BindingSource(this.PaisDict, null);
            InputPaisCuenta.DisplayMember = "Value";
            InputPaisCuenta.ValueMember = "Key";
            this.db.CerrarConexion();
        }

        private void Tipo_Moneda_Load(){
            this.db.ConectarConDB();
            string query = "SELECT Moneda_Nombre, Moneda_Simbolo FROM [GD1C2015].[NULL].[Moneda] WHERE Moneda_Borrado = 0";
            this.TipoMoneda = db.GetQueryDictionary(query, "Moneda_Nombre", "Moneda_Simbolo");
            InputTipoMoneda.DataSource = new BindingSource(this.TipoMoneda, null);
            InputTipoMoneda.DisplayMember = "Value";
            InputTipoMoneda.ValueMember = "Key";
            this.db.CerrarConexion();
        }

        private void Tipo_Cuenta_Load(){
            this.db.ConectarConDB();
            string query = "SELECT TipoCta_Nombre FROM [GD1C2015].[NULL].[TipoCuenta] WHERE TipoCta_Borrado=0";
            this.TipoCuenta = db.GetQueryDictionary(query, "TipoCta_Nombre", "TipoCta_Nombre");
            InputTipoCuenta.DataSource = new BindingSource(this.TipoCuenta, null);
            InputTipoCuenta.DisplayMember = "Value";
            InputTipoCuenta.ValueMember = "Key";
            this.db.CerrarConexion();
        }

        public void ExecStoredProcedure(string sp_name){
            SqlCommand sp = this.db.GetStoreProcedure(sp_name);
            sp.Parameters.Add(new SqlParameter("@Cuenta_Numero", Convert.ToInt64(InputNumeroCuenta.Text)));
            sp.Parameters.Add(new SqlParameter("@Pais_Codigo", InputPaisCuenta.SelectedValue));
            sp.Parameters.Add(new SqlParameter("@Moneda_Nombre", InputTipoMoneda.SelectedValue));
            sp.Parameters.Add(new SqlParameter("@TipoCta_Nombre", InputTipoCuenta.SelectedValue));
            sp.Parameters.Add(new SqlParameter("@Cuenta_Fecha_Creacion", InputFechaApertura.Value.Date));
            sp.Parameters.Add(new SqlParameter("@Cli_Cod", this.cliCod));
            sp.ExecuteNonQuery();
        }

        private void InputNumeroCuenta_KeyPress(object sender, KeyPressEventArgs e){
            this.validator.KeyPressBinding(this.validator.validateInt, false, e);
        }

    }
}
