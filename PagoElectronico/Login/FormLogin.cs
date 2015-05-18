using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Login
{
    public partial class FormLogin : Form{
        public FormLogin(){
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e){
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e){
            DbComunicator db = new DbComunicator();
            db.EjecutarQuery("SELECT TOP(100) * FROM gd_esquema.Maestra");
            label2.Text = "";
            while (db.getLector().Read()){
                this.label2.Text += db.getLector()["Cli_Nombre"].ToString() +
                    " "+ db.getLector()["Cli_Apellido"].ToString() +
                    System.Environment.NewLine;
            };
            db.CerrarConexion();
        }
    }
}
