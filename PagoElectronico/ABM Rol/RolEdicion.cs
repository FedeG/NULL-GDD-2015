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
    public partial class RolEdicion : RolData
    {
        string rolPk;

        public RolEdicion(DataGridViewRow selected)
        {
            InitializeComponent();
            this.enabledButtons.RegisterButton(this.commitEditButton);
            rolPk =selected.Cells["Rol_Nombre"].Value.ToString();
            rolNameBox.Text = rolPk;
            comboEstado.Text = selected.Cells["Rol_Estado"].Value.ToString();
            db.EjecutarQuery("SELECT Func_Cod FROM [GD1C2015].[NULL].[Rol_Funcionalidad] WHERE Rol_Nombre = '" + rolPk + "'");
            while (db.getLector().Read()) {
                int funcNumber = Convert.ToInt16(db.getLector()["Func_Cod"].ToString());
                funcionalidadesListBox.SetItemChecked(funcNumber - 1, true);
            }
        }

        private void commitEditButton_Click(object sender, EventArgs e)
        {
            DataTable funcionalidadesDelRol = new DataTable();
            funcionalidadesDelRol.Columns.Add("number", typeof(int));

            foreach (int checkedIndex in funcionalidadesListBox.CheckedIndices)
            {
                funcionalidadesDelRol.Rows.Add(checkedIndex + 1);
            }

            SqlCommand spCrearRol = this.db.GetStoreProcedure("NULL.spEditarRol");
            spCrearRol.Parameters.Add(new SqlParameter("@Rol_Pk", rolNameBox.Text));
            spCrearRol.Parameters.Add(new SqlParameter("@Rol_Nombre", rolNameBox.Text));
            spCrearRol.Parameters.Add(new SqlParameter("@Rol_Estado", comboEstado.SelectedItem.ToString()));
            spCrearRol.Parameters.Add(new SqlParameter("@Lista_Funcionalidades", funcionalidadesDelRol));
            spCrearRol.ExecuteNonQuery();
            this.Close();
        }
    }
}
