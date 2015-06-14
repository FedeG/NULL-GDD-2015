using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico
{
    public partial class MenuPrincipal : Form
    {
        DbComunicator db;
        Dictionary<object, object> FuncionalidadesDict;
        Dictionary<object, Form> FormsDict;

        public MenuPrincipal(string rol){
            InitializeComponent();
            this.db = new DbComunicator();
            this.LoadFuncionalidades(rol);
        }

        private void LoadFuncionalidades(string rol){
            this.db.ConectarConDB();
            this.FuncionalidadesDict = this.db.GetQueryDictionary("SELECT Funcionalidad.Func_Cod, Func_Nombre FROM (SELECT Func_Cod FROM GD1C2015.[NULL].Rol_Funcionalidad WHERE Rol_Nombre='" + rol + "') AS Rol_Funcionalidad INNER JOIN GD1C2015.[NULL].Funcionalidad AS Funcionalidad ON Funcionalidad.Func_Cod=Rol_Funcionalidad.Func_Cod WHERE Func_Borrado=0", "Func_Cod", "Func_Nombre");
            this.db.CerrarConexion();
            Funcionalidades.DataSource = new BindingSource(this.FuncionalidadesDict, null);
            Funcionalidades.DisplayMember = "Value";
            Funcionalidades.ValueMember = "Key";
        }

        private Form SearchForm(Int16 Func_Cod){
            Form form = null;
            switch (Func_Cod){
                //case 1: 
                    //form = new  PagoElectronico.ABM_Rol.Form1();
                    //break;
                case 2: 
                    form = new PagoElectronico.ABM_de_Usuario.Form1();
                    break;
                case 3:
                    form = new  PagoElectronico.ABM_Cliente.ClienteListado();
                    break;
                case 4:
                    form = new  PagoElectronico.ABM_Cuenta.Form1();
                    break;
                //case 5: 
                    //form = new  PagoElectronico.Tarjeta.Form1();
                    //break;
                case 6:
                    form = new  PagoElectronico.Depositos.DepositoForm();
                    break;
                case 7: 
                    form = new  PagoElectronico.Retiros.Form1();
                    break;
                case 8: 
                    form = new  PagoElectronico.Transferencias.Form1();
                    break;
                case 9: 
                    form = new  PagoElectronico.Facturacion.Form1();
                    break;
                case 10: 
                    form = new  PagoElectronico.Consulta_Saldos.Form1();
                    break;
                case 11: 
                    form = new PagoElectronico.Listados.Form1();
                    break;
            }
            return form;
        }

        private void Salir_Click(object sender, EventArgs e){
            this.Close();
        }

        private void Abrir_Click(object sender, EventArgs e){
            Form form = this.SearchForm(Convert.ToInt16(Funcionalidades.SelectedValue));
            form.Show();
            this.Close();
        }
    }
}
