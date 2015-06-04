using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data.SqlClient;

namespace PagoElectronico.Login
{
    public partial class FormLogin : Form{
        public FormLogin(){
            InitializeComponent();
        }

        public int getCountUsers(DbComunicator dbCount, string username)
        {
            dbCount.EjecutarQuery("SELECT count(*) FROM [GD1C2015].[NULL].[USUARIO] WHERE Usr_Username = '"
            + textBox1.Text + "'");
            
            dbCount.getLector().Read();
            int count = dbCount.getLector().GetInt32(0);
            dbCount.CerrarConexion();
            
            return count;
        }

        public void LlamarProcedureLogin(string username, int estado) {
            DbComunicator dbStoreProcedure = new DbComunicator();
            SqlCommand storeProcedure = dbStoreProcedure.GetStoreProcedure("NULL.spLoginRealizado");
            storeProcedure.Parameters.Add(new SqlParameter("@User", username));
            storeProcedure.Parameters.Add(new SqlParameter("@EstadoLogin", estado));
            storeProcedure.ExecuteNonQuery();
            dbStoreProcedure.CerrarConexion();
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

        public DbComunicator getRoles(DbComunicator db,String username) {
            db.EjecutarQuery("SELECT r.Rol_Nombre FROM [GD1C2015].[NULL].[Rol_Usuario] AS ru, [GD1C2015].[NULL].[Rol] AS r " + 
                "WHERE ru.Usr_Username = '" + username + "' AND ru.Rol_Nombre = r.Rol_Nombre AND r.Rol_Estado = 'Habilitado'");
            return db;
        }

        private void button2_Click(object sender, EventArgs e){
            int usersQuantity = 0; 
            DbComunicator db1 = new DbComunicator();
            string username = textBox1.Text;
            string userState = "";
            string password = this.getHashString(textBox2.Text);

            db1.EjecutarQuery("SELECT * FROM [GD1C2015].[NULL].[USUARIO] WHERE Usr_Username = '" 
                + username + "' and Usr_Password = '" 
                + password + "'");

            while (db1.getLector().Read()) {
                userState = db1.getLector()["Usr_Estado"].ToString();
                usersQuantity++;
            }

            if (usersQuantity == 0) {
                //TODO agregar llamado al store procedure
                int count = this.getCountUsers(db1, username);
                
                if (count > 0) {
                    this.LlamarProcedureLogin(username, 1);
                }

                MessageBox.Show("Login Invalido!");
                textBox2.Text = "";
            }
            
            if(usersQuantity > 1){
                MessageBox.Show("Hay mas de una coincidencia para el user password!");
            }

            if(usersQuantity == 1){
                if (userState == "Habilitado") {
                    this.LlamarProcedureLogin(username, 0);
                    FormSeleccionDeRol form = new FormSeleccionDeRol(this.getRoles(db1, username));
                    form.Show();
                }

                if (userState == "Deshabilitado") {
                    MessageBox.Show("El usuario se encuentra deshabiltado, contacte un Administrador");
                    textBox2.Text = "";
                    password = "";
                }
            }      
       
            db1.CerrarConexion();
        }
    }
}
