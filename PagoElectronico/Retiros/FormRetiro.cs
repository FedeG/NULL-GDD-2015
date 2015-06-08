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
        string username;
        public FormRetiro()
        {
            InitializeComponent();
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
