using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.ABM_Rol
{
    public partial class RolData : Form
    {
        
        public DbComunicator db;
        
        public RolData()
        {
            InitializeComponent();
            comboEstado.Items.Add("Habilitado");
            comboEstado.Items.Add("Deshabilitado");

            this.db = new DbComunicator();
            this.db.EjecutarQuery("SELECT Func_Cod, Func_Nombre FROM [GD1C2015].[NULL].[Funcionalidad]");

            while (this.db.getLector().Read())
            {
                string nombre = this.db.getLector()["Func_Nombre"].ToString();
                int cod = Convert.ToInt16(this.db.getLector()["Func_Cod"].ToString()) - 1;
                funcionalidadesListBox.Items.Insert(cod, this.db.getLector()["Func_Nombre"]);
            }
        }
    }
}
