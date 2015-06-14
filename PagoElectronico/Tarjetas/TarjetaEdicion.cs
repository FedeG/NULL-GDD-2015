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
    public partial class TarjetaEdicion : TarjetaData
    {
        public TarjetaEdicion(DataGridViewRow selected)
        {
            InitializeComponent();
            numeroTextBox.Text = selected.Cells["Numero"].Value.ToString();
            emisorComboBox.Text = selected.Cells["Emisor"].Value.ToString();
            emisionTimePicker.Value = Convert.ToDateTime(selected.Cells["Fecha_Emision"].Value);
            vencimientoTimePicker.Value = Convert.ToDateTime(selected.Cells["Fecha_Vencimiento"].Value);
        }

        private void editarButton_Click(object sender, EventArgs e)
        {
            string update = "UPDATE SET ";
            this.setCommand()
        }
    }
}
