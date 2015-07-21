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
    public partial class FormSeleccionDeRol : Form
    {
        public string rolSeleccionado, username;

        public FormSeleccionDeRol(string username)
        {
            InitializeComponent();
            this.username = username;
            string  queryRol = "SELECT r.Rol_Nombre FROM [GD1C2015].[NULL].[Rol_Usuario] AS ru, [GD1C2015].[NULL].[Rol] AS r " + 
                "WHERE ru.Usr_Username = '" + username + "' AND ru.Rol_Nombre = r.Rol_Nombre AND r.Rol_Estado = 'Habilitado'";
            DbComunicator db = new DbComunicator();
            comboBox1.DataSource = new BindingSource(db.GetQueryDictionary(queryRol, "Rol_Nombre", "Rol_Nombre"), null);
            comboBox1.DisplayMember = "Key";
            comboBox1.ValueMember = "Value";
        }

        private void button1_Click(object sender, EventArgs e){
            if (this.comboBox1.SelectedValue.ToString() == "No hay elementos para listar"){
                MessageBox.Show("No tiene ningun rol habilitado, contactese con un administrador");
                return;
            }
            this.rolSeleccionado = comboBox1.SelectedValue.ToString();
            PagoElectronico.MenuPrincipal formMenu = new PagoElectronico.MenuPrincipal(this.rolSeleccionado, this.username);
            formMenu.ShowDialog();
            this.Close();
        }
        
    }
}
