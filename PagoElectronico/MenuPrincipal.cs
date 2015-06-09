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
        Dictionary<object, object> FuncionalidadesDict, FormsDict;

        public MenuPrincipal(string rol){
            InitializeComponent();
            this.db = new DbComunicator();
            this.LoadFuncionalidades(rol);
        }

        private void LoadFuncionalidades(string rol){
            this.db.ConectarConDB();
            this.FuncionalidadesDict = this.db.GetQueryDictionary("SELECT Func_Cod, Func_Nombre FROM [GD1C2015].[NULL].[Funcionalidad] WHERE Func_Borrado = 0", "Func_Cod", "Func_Nombre");
            this.db.CerrarConexion();
            foreach (object Func_Cod in FuncionalidadesDict.Keys){
                string Func_Nombre = FuncionalidadesDict[Func_Cod].ToString();
                if (Func_Nombre != "") Funcionalidades.Items.Add(Func_Nombre);
            }
        }

        private void LoadForms(){
            //this.FormsDict.Add(1, new  PagoElectronico.ABM_Rol.Form1);
            this.FormsDict.Add(2, new  PagoElectronico.ABM_de_Usuario.Form1());
            this.FormsDict.Add(3, new  PagoElectronico.ABM_Cliente.Form1());
            this.FormsDict.Add(4, new  PagoElectronico.ABM_Cuenta.Form1());
            //this.FormsDict.Add(5, new  PagoElectronico.Tarjeta.Form1());
            this.FormsDict.Add(6, new  PagoElectronico.Depositos.Form1());
            this.FormsDict.Add(7, new  PagoElectronico.Retiros.Form1());
            this.FormsDict.Add(8, new  PagoElectronico.Transferencias.Form1());
            this.FormsDict.Add(9, new  PagoElectronico.Facturacion.Form1());
            this.FormsDict.Add(10, new  PagoElectronico.Consulta_Saldos.Form1());
            this.FormsDict.Add(11, new PagoElectronico.Listados.Form1());
        }

        private void Salir_Click(object sender, EventArgs e){
            this.Close();
        }
    }
}
