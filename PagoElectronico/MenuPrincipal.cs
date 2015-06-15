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
        string rolSeleccionado, username;

        public MenuPrincipal(string rol, string username){
            InitializeComponent();
            this.rolSeleccionado = rol;
            this.username = username;
            this.db = new DbComunicator();
            this.LoadFuncionalidades();
        }

        private void LoadFuncionalidades(){
            this.db.ConectarConDB();
            this.FuncionalidadesDict = this.db.GetQueryDictionary("SELECT Funcionalidad.Func_Cod, Func_Nombre FROM (SELECT Func_Cod FROM GD1C2015.[NULL].Rol_Funcionalidad WHERE Rol_Nombre='" + this.rolSeleccionado + "') AS Rol_Funcionalidad INNER JOIN GD1C2015.[NULL].Funcionalidad AS Funcionalidad ON Funcionalidad.Func_Cod=Rol_Funcionalidad.Func_Cod WHERE Func_Borrado=0", "Func_Nombre", "Func_Cod");
            this.db.CerrarConexion();
            Funcionalidades.DataSource = new BindingSource(this.FuncionalidadesDict, null);
            Funcionalidades.DisplayMember = "Key";
            Funcionalidades.ValueMember = "Value";
        }

        private Form SearchForm(Int16 Func_Cod){
            Form form = null;
            switch (Func_Cod){
                case 1: form = new  PagoElectronico.ABM_Rol.RolListado(); break;
                case 2: form = new PagoElectronico.ABM_de_Usuario.Form1(); break;
                case 3: form = new  PagoElectronico.ABM_Cliente.ClienteListado(); break;
                case 4: form = new  PagoElectronico.ABM_Cuenta.Form1(); break;
                // case 5: form = new  PagoElectronico.Tarjeta.Form1(); break;
                case 6: form = new PagoElectronico.Depositos.DepositoForm(this.username); break;
                case 7: form = new  PagoElectronico.Retiros.FormRetiro(); break;
                case 8: form = new PagoElectronico.Transferencias.TransferenciaForm(this.username); break;
                case 9: form = new  PagoElectronico.Facturacion.FormFacturacion(this.username); break;
                case 10: form = new  PagoElectronico.Consulta_Saldos.ConsultaForm(this.username); break;
                case 11: form = new PagoElectronico.Listados.Form1(); break;
            }
            return form;
        }

        private void Salir_Click(object sender, EventArgs e){
            this.Close();
        }

        private void Abrir_Click(object sender, EventArgs e){
            this.SearchForm(Convert.ToInt16(Funcionalidades.SelectedValue)).ShowDialog();
        }
    }
}
