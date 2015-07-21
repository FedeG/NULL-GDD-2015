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
using PagoElectronico.Commons;

namespace PagoElectronico.Login
{
    public partial class FormLogin : Form{
        Commons.EnabledButtons enabledButtons;

        public FormLogin(){
            InitializeComponent();
            this.enabledButtons = new Commons.EnabledButtons();
            this.enabledButtons.RegisterTextBox(this.InputUsername);
            this.enabledButtons.RegisterTextBox(this.InputPassword);
            this.enabledButtons.RegisterButton(this.button2);
        }

        public int GetCountUsers(DbComunicator dbCount, string username)
        {
            dbCount.EjecutarQuery("SELECT count(*) FROM [GD1C2015].[NULL].[USUARIO] WHERE Usr_Username = '"
            + InputUsername.Text + "'");
            
            dbCount.getLector().Read();
            int count = dbCount.getLector().GetInt32(0);
            dbCount.CerrarConexion();
            
            return count;
        }

        public int LlamarProcedureLogin(string username, string password) {
            DbComunicator dbStoreProcedure = new DbComunicator();
            SqlCommand storeProcedure = dbStoreProcedure.GetStoreProcedure("NULL.spRealizarLogin");
            SqlParameter returnParameter = storeProcedure.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            storeProcedure.Parameters.Add(new SqlParameter("@Username", username));
            storeProcedure.Parameters.Add(new SqlParameter("@Password", password));
            storeProcedure.ExecuteNonQuery();
            dbStoreProcedure.CerrarConexion();
            return (int) returnParameter.Value;
        }

        private void button1_Click(object sender, EventArgs e){
            this.Close();
        }

        public DbComunicator GetRoles(DbComunicator db,String username) {
            db.EjecutarQuery("SELECT r.Rol_Nombre FROM [GD1C2015].[NULL].[Rol_Usuario] AS ru, [GD1C2015].[NULL].[Rol] AS r " + 
                "WHERE ru.Usr_Username = '" + username + "' AND ru.Rol_Nombre = r.Rol_Nombre AND r.Rol_Estado = 'Habilitado'");
            return db;
        }

        private void button2_Click(object sender, EventArgs e){
            DbComunicator db1 = new DbComunicator();
            string username = InputUsername.Text;
            string password = new Sha256Generator().GetHashString(InputPassword.Text);
            int resultado = this.LlamarProcedureLogin(username, password);
            switch (resultado){
                case 0:
                    FormSeleccionDeRol formRol = new FormSeleccionDeRol(this.GetRoles(db1, username));
                    formRol.ShowDialog();
                    PagoElectronico.MenuPrincipal formMenu = new PagoElectronico.MenuPrincipal(formRol.rolSeleccionado, username);
                    formMenu.ShowDialog();
                    break;
                case 1: MessageBox.Show("Login Invalido!"); break;
                case 2: MessageBox.Show("El usuario no existe"); break;
                case 3: MessageBox.Show("El usuario se encuentra deshabilitado. Comuniquese con un administrador del sistema."); break;
            }
            InputPassword.Text = "";
            db1.CerrarConexion();
        }
    }
}
