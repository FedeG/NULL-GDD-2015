using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PagoElectronico.Commons;
using System.Data.SqlClient;

namespace PagoElectronico.Tarjetas
{
    public partial class TarjetaEdicion : TarjetaData
    {
        string tarjetaNumero;
        string tarjetaCodigo;
        string tarjetaNumeroVisible;
        Commons.Validator validator;

        public TarjetaEdicion(DataGridViewRow selected)
        {
            InitializeComponent();
            this.validator = new Commons.Validator();
            this.seguridadTextBox.KeyPress += this.Number_KeyPress;
            this.numeroTextBox.KeyPress += this.Number_KeyPress;
            this.enabledButtons.RegisterTextBox(this.numeroTextBox);
            this.enabledButtons.RegisterButton(this.editarButton);
            tarjetaNumero = selected.Cells["Tarjeta_Numero"].Value.ToString();
            tarjetaCodigo = selected.Cells["Tarjeta_Codigo_Seg"].Value.ToString();
            tarjetaNumeroVisible = selected.Cells["Numero"].Value.ToString();
            emisorComboBox.Text = selected.Cells["Emisor"].Value.ToString();
            emisionTimePicker.Value = Convert.ToDateTime(selected.Cells["Fecha_Emision"].Value);
            vencimientoTimePicker.Value = Convert.ToDateTime(selected.Cells["Fecha_Vencimiento"].Value);
        }

        private void editarButton_Click(object sender, EventArgs e)
        {
            DbComunicator db = new DbComunicator();
            string shaNumero = tarjetaNumero;
            string shaCod = tarjetaCodigo;
            string numeroVisible = tarjetaNumeroVisible;
            int cambioPk = 0;

            int comparacionFechaVencimiento = DateTime.Compare(vencimientoTimePicker.Value, Properties.Settings.Default.FechaSistema);

            if (comparacionFechaVencimiento < 0)
            {
                MessageBox.Show("La fecha de vencimiento de la tarjeta debe ser posterior a la fecha actual del sistema: " + Properties.Settings.Default.FechaSistema.ToString());
                return;
            }

            int comparacionFechaEmision = DateTime.Compare(emisionTimePicker.Value, Properties.Settings.Default.FechaSistema);

            if (comparacionFechaEmision > 0)
            {
                MessageBox.Show("La fecha de emision de la tarjeta debe ser anterior a la fecha actual del sistema: " + Properties.Settings.Default.FechaSistema.ToString());
                return;
            }
            
            if (numeroTextBox.Text.Length > 0) {
                shaNumero = new Sha256Generator().GetHashString(numeroTextBox.Text);
                numeroVisible = numeroTextBox.Text.Substring(numeroTextBox.Text.Length - 4);
                cambioPk = 1;
            }

            if (seguridadTextBox.Text.Length > 0) {
                shaCod = new Sha256Generator().GetHashString(seguridadTextBox.Text);
            }
            
            SqlCommand spEditarTarjeta = db.GetStoreProcedure("NULL.spEditarTarjeta");
            SqlParameter returnParameter = spEditarTarjeta.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            spEditarTarjeta.Parameters.Add(new SqlParameter("@Tarjeta_Pk", tarjetaNumero));
            spEditarTarjeta.Parameters.Add(new SqlParameter("@Tarjeta_Numero", shaNumero));
            spEditarTarjeta.Parameters.Add(new SqlParameter("@Tarjeta_Numero_Visible", numeroVisible));
            spEditarTarjeta.Parameters.Add(new SqlParameter("@Tarjeta_Codigo_Seg", shaCod));
            spEditarTarjeta.Parameters.Add(new SqlParameter("@Emisor_Cod", Convert.ToInt32(emisorComboBox.SelectedValue)));
            spEditarTarjeta.Parameters.Add(new SqlParameter("@Tarjeta_Fecha_Vencimiento", vencimientoTimePicker.Value));
            spEditarTarjeta.Parameters.Add(new SqlParameter("@Tarjeta_Fecha_Emision", emisionTimePicker.Value));
            spEditarTarjeta.Parameters.Add(new SqlParameter("@Cambio_Pk ", cambioPk));
            spEditarTarjeta.ExecuteNonQuery();
            db.CerrarConexion();

            if ((int)returnParameter.Value == 1) {
                MessageBox.Show("El numero de tarjeta ingresado ya se encuentra cargado en el sistema");
            }
            
            this.Close();
        }

        private void Number_KeyPress(object sender, KeyPressEventArgs e){
            this.validator.KeyPressBinding(this.validator.validateInt, false, e);
        }
    }
}
