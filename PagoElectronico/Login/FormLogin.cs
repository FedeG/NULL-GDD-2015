using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace PagoElectronico.Login
{
    public partial class FormLogin : Form{
        public FormLogin(){
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e){
            this.Close();
        }

        public String getHashString(String password){
            byte[] passwordSha256 = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(password));
            string result = BitConverter.ToString(passwordSha256)
                .Replace("-", string.Empty)
                .ToLower();
            return result;
        }

        private void button2_Click(object sender, EventArgs e){
            DbComunicator db = new DbComunicator();
            db.EjecutarQuery("SELECT * FROM [GD1C2015].[NULL].[USUARIO] WHERE Usr_Username = '" + textBox1.Text + "' and Usr_Password = '" + this.getHashString(textBox2.Text) + "'");
            while (db.getLector().Read()){
                MessageBox.Show(db.getLector()["Usr_Username"].ToString() + " ha sido logeado");
            };
            db.CerrarConexion();
        }
    }
}
