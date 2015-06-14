using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Tarjetas
{
    public partial class TarjetaListado : Form
    {
        public TarjetaListado()
        {
            InitializeComponent();
            DbComunicator db =new DbComunicator();
            string queryTarjetas = "SELECT t.Tarjeta_Numero, t.Tarjeta_Numero_Visible Numero, ";
            queryTarjetas = queryTarjetas + "t.Tarjeta_Fecha_Emision Fecha_Emision, t.Tarjeta_Fecha_Vencimiento Fecha_Vencimiento, ";
            queryTarjetas = queryTarjetas + "  e.Emisor_Desc Emisor, e.Emisor_Cod FROM [GD1C2015].[NULL].[Tarjeta] as t, [GD1C2015].[NULL].[Emisor] as e WHERE Cli_Cod ='" + 100004 + "' AND t.Emisor_Cod = e.Emisor_Cod";
            tarjetaGridView.DataSource = db.GetDataAdapter(queryTarjetas).Tables[0];
            tarjetaGridView.Columns["Tarjeta_Numero"].Visible = false;
            tarjetaGridView.Columns["Emisor_Cod"].Visible = false;
        }

        private void desasociarButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(tarjetaGridView.SelectedColumns[1].ToString());
        }

        private void editarButton_Click(object sender, EventArgs e){
            new TarjetaEdicion(tarjetaGridView.SelectedRows[0]).Show();
        }
    }
}
