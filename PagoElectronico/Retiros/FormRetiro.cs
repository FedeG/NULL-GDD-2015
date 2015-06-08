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
        public FormRetiro()
        {
            InitializeComponent();
        }

        private void realizarButton_Click(object sender, EventArgs e)
        {
            DbComunicator db = new DbComunicator();
            SqlCommand spCrearRol = db.GetStoreProcedure("NULL.spRealizarRetiro");
            spCrearRol.Parameters.Add(new SqlParameter("@Rol_Nombre", rolNameBox.Text));
            spCrearRol.Parameters.Add(new SqlParameter("@Rol_Estado", comboEstado.SelectedItem.ToString()));
            spCrearRol.Parameters.Add(new SqlParameter("@Lista_Funcionalidades", funcionalidadesDelRol));
            spCrearRol.ExecuteNonQuery();

            this.Close();
        }

    }
}
