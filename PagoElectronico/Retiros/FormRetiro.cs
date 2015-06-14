using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico.Retiros
{
    public partial class FormRetiro : Form
    {
        string username = "";
        public FormRetiro()
        {
            InitializeComponent();
            DbComunicator db = new DbComunicator();
            db.EjecutarQuery("SELECT Banco_Codigo, Banco_Nombre FROM [GD1C2015].[NULL].[Banco]");
            Dictionary<string, string> bancoDictionary = new Dictionary<string, string>();
            while (db.getLector().Read()) {
                string codigo = db.getLector()["Banco_Codigo"].ToString();
                string nombre = db.getLector()["Banco_Nombre"].ToString();
                bancoDictionary.Add(nombre + " (" + codigo + ") ", codigo);
            }
            db.CerrarConexion();
            db = new DbComunicator();
            bancoComboBox.DataSource = new BindingSource(bancoDictionary, null);
            bancoComboBox.DisplayMember = "Key";
            bancoComboBox.ValueMember = "Value";
            tipoDocComboBox.DataSource = new BindingSource(db.GetQueryDictionary("SELECT TipoDoc_Cod, TipoDoc_Desc FROM [GD1C2015].[NULL].[TipoDoc]", "TipoDoc_Cod", "TipoDoc_Desc"), null);
            tipoDocComboBox.DisplayMember = "Key";
            tipoDocComboBox.DisplayMember = "Value";
        }

        private void realizarButton_Click(object sender, EventArgs e)
        {
            DbComunicator db = new DbComunicator();
            SqlCommand spRealizarRetiro = db.GetStoreProcedure("NULL.spRealizarRetiro");
            SqlParameter returnParameter = spRealizarRetiro.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            spRealizarRetiro.Parameters.Add(new SqlParameter("@Username", username));
            spRealizarRetiro.Parameters.Add(new SqlParameter("@TipoDoc_Cod", tipoDocComboBox.SelectedText));
            spRealizarRetiro.Parameters.Add(new SqlParameter("@Nro_Doc", nroDocTextBox.Text));
            spRealizarRetiro.Parameters.Add(new SqlParameter("@Cuenta_Numero", cuentaComboBox.SelectedText));
            spRealizarRetiro.Parameters.Add(new SqlParameter("@Importe", importeTextBox.Text));
            spRealizarRetiro.Parameters.Add(new SqlParameter("@Banco_Cod", bancoComboBox.SelectedValue));
            spRealizarRetiro.Parameters.Add(new SqlParameter("@Moneda_Nombre", "Dólares Estadounidenses"));
            spRealizarRetiro.Parameters.Add(new SqlParameter("@Fecha_Deposito", Properties.Settings.Default.FechaSistema));

            //Agregar la fecha de sistema.
            spRealizarRetiro.ExecuteNonQuery();

            if ((int)returnParameter.Value == 0) {
                this.Close();
            }

            if ((int)returnParameter.Value == 1)
            {
                MessageBox.Show("El Tipo y/o Numero de Documento no coinciden con los del usuario logeado.");
            }
            
            if ((int)returnParameter.Value == 2)
            {
                MessageBox.Show("La Cuenta seleccionada no tiene saldo.");
            }
            
            if ((int)returnParameter.Value == 3)
            {
                MessageBox.Show("El saldo disponible es insuficiente para realizar el retiro.");
            }

            if ((int)returnParameter.Value == 4)
            {
                MessageBox.Show("La cuenta no se encuentra Habilitada.");
            }
        }

    }
}
