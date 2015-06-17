using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico.ABM_Cuenta
{
    public partial class FormSuscripcion : Form
    {
        DbComunicator db;
        Commons.Validator validator;
        long cuentaNumero;

        public FormSuscripcion(long CuentaNumero){
            InitializeComponent();
            this.cuentaNumero = CuentaNumero;
            this.db = new DbComunicator();
            this.validator = new Commons.Validator();
            string query = "SELECT TipoCta_Nombre, TipoCta_Costo_Apertura, TipoCta_Duracion FROM [GD1C2015].[NULL].[TipoCuenta] WHERE TipoCta_Borrado = 0";
            tiposTable.DataSource = db.GetDataAdapter(query).Tables[0];
            Cantidad.KeyPress += this.InputNumeroCuenta_KeyPress;
        }

        private void InputNumeroCuenta_KeyPress(object sender, KeyPressEventArgs e){
            this.validator.KeyPressBinding(this.validator.validateInt, false, e);
        }

        private void button2_Click(object sender, EventArgs e){
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e){
            SqlCommand spAgregarSuscripcion = this.db.GetStoreProcedure("NULL.spAgregarSuscripcion");
            spAgregarSuscripcion.Parameters.Add(new SqlParameter("@Cuenta_Numero", this.cuentaNumero));
            spAgregarSuscripcion.Parameters.Add(new SqlParameter("@TipoCta_Nombre", tiposTable.SelectedRows[0].Cells["TipoCta_Nombre"].Value));
            spAgregarSuscripcion.Parameters.Add(new SqlParameter("@Cantidad", Cantidad.Text));
            SqlParameter returnParameter = spAgregarSuscripcion.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            spAgregarSuscripcion.ExecuteNonQuery();
            if (Convert.ToInt64(returnParameter.Value) >= 0) MessageBox.Show("Su cuenta fue actualizada correctamente");
            else MessageBox.Show("Su cuenta no puede ser modificada");
            this.Close();
        }
    }
}