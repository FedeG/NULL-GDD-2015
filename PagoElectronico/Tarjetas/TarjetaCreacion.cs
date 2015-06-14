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
        public TarjetaCreacion()
        {
            InitializeComponent();
        }

        private void createButton_Click(object sender, EventArgs e){
            int cliCod = 100004;
            string tarjetaNumeroVisible = numeroTextBox.Text.Substring(numeroTextBox.Text.Length - 4);
            string shaTarjetaNumero = new Sha256Generator().GetHashString(numeroTextBox.Text);
            string shaCodSeguridad = new Sha256Generator().GetHashString(seguridadTextBox.Text);
            string insert = "INSERT INTO [GD1C2015].[NULL].[Tarjeta](Tarjeta_Numero, Tarjeta_Numero_Visible, Tarjeta_Codigo_Seg, Emisor_Cod, ";
            insert = insert + "Cli_Cod, Tarjeta_Fecha_Emision, Tarjeta_Fecha_Vencimiento) ";
            insert = insert + " VALUES (@ShaTarjeta, @NumeroVisible, @ShaCod, @Emisor, @Cli_cod, @Emision, @Vencimiento)";
            this.setCommand(insert, cliCod, shaTarjetaNumero, tarjetaNumeroVisible, shaCodSeguridad).ExecuteNonQuery();
        }
    }
}
