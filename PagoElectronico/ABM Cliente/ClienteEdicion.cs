using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico.ABM_Cliente
{
    public partial class ClienteEdicion : ClienteData
    {
        string username;

        public ClienteEdicion(DataGridViewRow selected)
        {
            InitializeComponent();
            this.db = new DbComunicator();
            this.username = selected.Cells["Usr_Username"].Value.ToString();
            this.LoadUserData();
            this.LoadClientData(selected);
        }

        private void LoadClientData(DataGridViewRow selected){
            InputNombre.Text = selected.Cells["Cli_Nombre"].Value.ToString();
            InputApellido.Text = selected.Cells["Cli_Apellido"].Value.ToString();
            InputNacCliente.Text = selected.Cells["Cli_Nacionalidad"].Value.ToString();
            InputCalle.Text = selected.Cells["Cli_Dom_Calle"].Value.ToString();
            InputNumDomicilio.Text = selected.Cells["Cli_Dom_Nro"].Value.ToString();
            db.EjecutarQuery("SELECT * FROM [GD1C2015].[NULL].[Cliente] WHERE Usr_Username = '" + username + "'");
            while (db.getLector().Read()) {
                InputMail.Text = db.getLector()["Cli_Mail"].ToString();
                InputTipoDocCliente.Text = this.TipoDocs[db.getLector()["TipoDoc_Cod"]].ToString();
                InputNumDoc.Text = db.getLector()["Cli_Nro_Doc"].ToString();
                InputPiso.Text = db.getLector()["Cli_Dom_Piso"].ToString();
                InputDepto.Text = db.getLector()["Cli_Dom_Depto"].ToString();
                InputLocalidad.Text = db.getLector()["Cli_Localidad"].ToString();
                InputFechaNacimiento.Value = Convert.ToDateTime(db.getLector()["Cli_Fecha_Nac"].ToString());
            }
        }

        private void LoadUserData(){
            InputUsername.Text = this.username;
            InputUsername.Enabled = false;
            db.EjecutarQuery("SELECT Usr_Pregunta_Secreta FROM [GD1C2015].[NULL].[Usuario] WHERE Usr_Username = '" + username + "'");
            while (db.getLector().Read()){
                InputPregunta.Text = db.getLector()["Usr_Pregunta_Secreta"].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e){
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e){
            this.ExecStoredProcedure("NULL.spEditarCliente");
            this.Close();
        }

    }
}
