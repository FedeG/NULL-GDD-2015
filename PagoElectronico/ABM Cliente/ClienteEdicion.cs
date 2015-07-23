using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using PagoElectronico.Commons;

namespace PagoElectronico.ABM_Cliente
{
    public partial class ClienteEdicion : ClienteData
    {
        string username, password_hash, respuesta_hash;

        public ClienteEdicion(DataGridViewRow selected)
        {
            InitializeComponent();
            this.db = new DbComunicator();
            this.username = selected.Cells["Usr_Username"].Value.ToString();
            this.enabledButtons.RegisterButton(this.button1);
            this.LoadUserData();
            this.LoadClientData(selected);
        }

        private void LoadClientData(DataGridViewRow selected){
            this.InputMail.Enabled = false;
            this.InputNumDoc.Enabled = false;
            this.InputTipoDocCliente.Enabled = false;
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
            this.InputUsername.Text = this.username;
            this.InputUsername.Enabled = false;
            db.EjecutarQuery("SELECT Usr_Password, Usr_Pregunta_Secreta, Usr_Respuesta_Secreta FROM [GD1C2015].[NULL].[Usuario] WHERE Usr_Username = '" + username + "'");
            while (db.getLector().Read()){
                this.InputPregunta.Text = db.getLector()["Usr_Pregunta_Secreta"].ToString();
                this.InputPassword.Text = db.getLector()["Usr_Password"].ToString();
                this.password_hash = db.getLector()["Usr_Password"].ToString();
                this.InputRespuestaSecreta.Text = db.getLector()["Usr_Respuesta_Secreta"].ToString();
                this.respuesta_hash = db.getLector()["Usr_Respuesta_Secreta"].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e){
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e){
            if (this.respuesta_hash != this.InputRespuestaSecreta.Text)
                this.InputRespuestaSecreta.Text = new Sha256Generator().GetHashString(InputRespuestaSecreta.Text);
            if (this.password_hash != this.InputPassword.Text)
                this.InputPassword.Text = new Sha256Generator().GetHashString(InputPassword.Text);
            this.ExecStoredProcedure("NULL.spEditarCliente", true);
            MessageBox.Show("El usuario fue editado exitosamente.");
            this.Close();
        }

    }
}
