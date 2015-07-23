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
    public partial class TarjetaCreacion : TarjetaData
    {
        int cliCod;
        Commons.Validator validator;

        public TarjetaCreacion(int cliCod)
        {
            InitializeComponent();
            this.cliCod = cliCod;
            this.validator = new Commons.Validator();
            this.seguridadTextBox.KeyPress += this.Number_KeyPress;
            this.numeroTextBox.KeyPress += this.Number_KeyPress;
            this.enabledButtons.RegisterTextBox(this.numeroTextBox);
            this.enabledButtons.RegisterButton(this.createButton);
        }

        private void createButton_Click(object sender, EventArgs e){
            DbComunicator db = new DbComunicator();

            int comparacionFechaVencimiento = DateTime.Compare(vencimientoTimePicker.Value, Properties.Settings.Default.FechaSistema);

            if (comparacionFechaVencimiento < 0)
            {
                MessageBox.Show("La fecha de vencimiento de la tarjeta debe ser posterior a la fecha actual del sistema: " + Properties.Settings.Default.FechaSistema.ToString());
                return ;
            }

            int comparacionFechaEmision = DateTime.Compare(emisionTimePicker.Value, Properties.Settings.Default.FechaSistema);

            if (comparacionFechaEmision > 0)
            {
                MessageBox.Show("La fecha de emision de la tarjeta debe ser anterior a la fecha actual del sistema: " + Properties.Settings.Default.FechaSistema.ToString());
                return;
            }

            string tarjetaNumeroVisible = numeroTextBox.Text.Substring(numeroTextBox.Text.Length - 4);
            string shaTarjetaNumero = new Sha256Generator().GetHashString(numeroTextBox.Text);
            string shaCodSeguridad = new Sha256Generator().GetHashString(seguridadTextBox.Text);
                
            SqlCommand spCrearTarjeta = db.GetStoreProcedure("NULL.spCrearTarjeta");
            SqlParameter returnParameter = spCrearTarjeta.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            spCrearTarjeta.Parameters.Add(new SqlParameter("@Tarjeta_Numero", shaTarjetaNumero));
            spCrearTarjeta.Parameters.Add(new SqlParameter("@Tarjeta_Numero_Visible", tarjetaNumeroVisible));
            spCrearTarjeta.Parameters.Add(new SqlParameter("@Tarjeta_Codigo_Seg", shaCodSeguridad));
            spCrearTarjeta.Parameters.Add(new SqlParameter("@Emisor_Cod", Convert.ToInt32(emisorComboBox.SelectedValue)));
            spCrearTarjeta.Parameters.Add(new SqlParameter("@Cli_Cod", this.cliCod));
            spCrearTarjeta.Parameters.Add(new SqlParameter("@Tarjeta_Fecha_Vencimiento", vencimientoTimePicker.Value));
            spCrearTarjeta.Parameters.Add(new SqlParameter("@Tarjeta_Fecha_Emision", emisionTimePicker.Value));
            spCrearTarjeta.ExecuteNonQuery();
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
