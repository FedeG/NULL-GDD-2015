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

        public List<String> getRoles(DbComunicator db,String username) {
            db.EjecutarQuery("SELECT Rol_Nombre FROM [GD1C2015].[NULL].[Rol_Usuario] WHERE Usr_Username = '" + username + "'");
            List<String> roles = new List<string>();
            
            while (db.getLector().Read()){
                roles.Add(db.getLector()["Rol_Nombre"].ToString());
            }

            return roles;
        }

        private void button2_Click(object sender, EventArgs e){
            int usersQuantity = 0; 
            DbComunicator db = new DbComunicator();
            string username = "";

            db.EjecutarQuery("SELECT * FROM [GD1C2015].[NULL].[USUARIO] WHERE Usr_Username = '" + textBox1.Text + "' and Usr_Password = '" + this.getHashString(textBox2.Text) + "'");

            while (db.getLector().Read()) {
                username = db.getLector()["Usr_Username"].ToString();
                usersQuantity++;
            }

            if (usersQuantity == 0) {
                //TODO agregar llamado al store procedure
                MessageBox.Show("Login Invalido!");
                textBox2.Text = "";
            }
            
            if(usersQuantity > 1){
                MessageBox.Show("Hay mas de una coincidencia para el user password!");
            }

            if(usersQuantity == 1){
                List<String> roles = this.getRoles(db, username);
                if (roles.Count == 0) {
                    MessageBox.Show(username + " no tiene roles asignados");
                }

                if (roles.Count == 1) {
                    MessageBox.Show(username + " ha sido logeado");
                    new FormSeleccionDeRol(roles).Show();

                }

                if (roles.Count > 1) {
                    MessageBox.Show("Hay mas roles");
                }
            }      
       
            db.CerrarConexion();
        }
    }
}
