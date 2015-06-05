using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico.ABM_Rol
{
    public partial class RolCreacion : Form
    {
        DbComunicator db;
        public RolCreacion()
        {
            InitializeComponent();
            comboEstado.Items.Add("Habilitado");
            comboEstado.Items.Add("Deshabilitado");

            this.db = new DbComunicator();
            this.db.EjecutarQuery("SELECT Func_Cod, Func_Nombre FROM [GD1C2015].[NULL].[Funcionalidad]");

            while (this.db.getLector().Read()) {
                string nombre = this.db.getLector()["Func_Nombre"].ToString();
                int cod = Convert.ToInt16(this.db.getLector()["Func_Cod"].ToString()) - 1;
                funcionalidadesListBox.Items.Insert(cod ,this.db.getLector()["Func_Nombre"]);    
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable funcionalidadesDelRol = new DataTable();
            funcionalidadesDelRol.Columns.Add("number", typeof(int));

            foreach (int checkedIndex in funcionalidadesListBox.CheckedIndices)
            {
                funcionalidadesDelRol.Rows.Add(checkedIndex + 1);
            }

            SqlCommand spCrearRol = this.db.GetStoreProcedure("NULL.spCrearRol");
            spCrearRol.Parameters.Add(new SqlParameter("@Rol_Nombre", rolNameBox.Text));
            spCrearRol.Parameters.Add(new SqlParameter("@Rol_Estado", comboEstado.SelectedItem.ToString()));
            spCrearRol.Parameters.Add(new SqlParameter("@Lista_Funcionalidades", funcionalidadesDelRol));
            spCrearRol.ExecuteNonQuery();
        }

    }
}
