﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico.Retiros
{
    public partial class FormRetiro : Form
    {
        string username = "";
        Commons.EnabledButtons enabledButtons;
        Commons.Validator validator;
        
        public FormRetiro(string username){
            InitializeComponent();
            this.validator = new Commons.Validator();
            this.nroDocTextBox.KeyPress += this.Number_KeyPress;
            this.importeTextBox.KeyPress += this.NumberDouble_KeyPress;

            this.enabledButtons = new Commons.EnabledButtons();
            this.enabledButtons.RegisterTextBox(this.nroDocTextBox);
            this.enabledButtons.RegisterTextBox(this.importeTextBox);
            this.enabledButtons.RegisterButton(this.realizarButton);
            this.username = username;

            DbComunicator db = new DbComunicator();

            string queryMonedas = "SELECT Moneda_Nombre, Moneda_Simbolo FROM [GD1C2015].[NULL].[Moneda]";
            comboMoneda.DataSource = new BindingSource(db.GetQueryDictionary(queryMonedas, "Moneda_Simbolo", "Moneda_Nombre"), null);
            comboMoneda.DisplayMember = "Key";
            comboMoneda.ValueMember = "Value";

            string query = "SELECT Cli_Cod FROM [GD1C2015].[NULL].[Cliente] WHERE Usr_Username = '" + username + "'";
            db.EjecutarQuery(query);
            db.getLector().Read();
            int cli_Cod = Convert.ToInt32(db.getLector()["Cli_Cod"]);

            string queryCuentas = "SELECT Cuenta_Numero FROM [GD1C2015].[NULL].[Cuenta] WHERE Cli_Cod = " + cli_Cod;
            cuentaComboBox.DataSource = new BindingSource(db.GetQueryDictionary(queryCuentas, "Cuenta_Numero", "Cuenta_Numero"), null);
            cuentaComboBox.DisplayMember = "Key";
            cuentaComboBox.ValueMember = "Value";
            
            db.EjecutarQuery("SELECT Banco_Codigo, Banco_Nombre FROM [GD1C2015].[NULL].[Banco]");
            Dictionary<string, string> bancoDictionary = new Dictionary<string, string>();
            while (db.getLector().Read()) {
                string codigo = db.getLector()["Banco_Codigo"].ToString();
                string nombre = db.getLector()["Banco_Nombre"].ToString();
                bancoDictionary.Add(nombre + " (" + codigo + ") ", codigo);
            }
            
            bancoComboBox.DataSource = new BindingSource(bancoDictionary, null);
            bancoComboBox.DisplayMember = "Key";
            bancoComboBox.ValueMember = "Value";
            
            tipoDocComboBox.DataSource = new BindingSource(db.GetQueryDictionary("SELECT TipoDoc_Cod, TipoDoc_Desc FROM [GD1C2015].[NULL].[TipoDoc]", "TipoDoc_Desc", "TipoDoc_Cod"), null);
            tipoDocComboBox.DisplayMember = "Key";
            tipoDocComboBox.ValueMember = "Value";

            db.CerrarConexion();
        }

        private void Number_KeyPress(object sender, KeyPressEventArgs e){
            this.validator.KeyPressBinding(this.validator.validateInt, false, e);
        }

        private void NumberDouble_KeyPress(object sender, KeyPressEventArgs e){
            this.validator.KeyPressBinding(this.validator.validateDouble, false, e);
        }

        private void realizarButton_Click(object sender, EventArgs e){
            DbComunicator db = new DbComunicator();
            SqlCommand spRealizarRetiro = db.GetStoreProcedure("NULL.spRealizarRetiro");
            SqlParameter returnParameter = spRealizarRetiro.Parameters.Add("RetVal", SqlDbType.Int);
            
            returnParameter.Direction = ParameterDirection.ReturnValue;
            spRealizarRetiro.Parameters.Add(new SqlParameter("@Username", username));
            spRealizarRetiro.Parameters.Add(new SqlParameter("@TipoDoc_Cod", Convert.ToInt32(tipoDocComboBox.SelectedValue)));
            spRealizarRetiro.Parameters.Add(new SqlParameter("@Nro_Doc", nroDocTextBox.Text));
            spRealizarRetiro.Parameters.Add(new SqlParameter("@Cuenta_Numero", Convert.ToInt64(cuentaComboBox.SelectedValue)));
            spRealizarRetiro.Parameters.Add(new SqlParameter("@Importe", Convert.ToInt32(importeTextBox.Text)));
            spRealizarRetiro.Parameters.Add(new SqlParameter("@Banco_Cod", Convert.ToInt32(bancoComboBox.SelectedValue)));
            spRealizarRetiro.Parameters.Add(new SqlParameter("@Moneda_Nombre", comboMoneda.SelectedValue.ToString()));
            spRealizarRetiro.Parameters.Add(new SqlParameter("@Fecha_Deposito", Properties.Settings.Default.FechaSistema));

            spRealizarRetiro.ExecuteNonQuery();

            switch ((int)returnParameter.Value)
            {
                case 0: MessageBox.Show("Retiro realizado."); break;
                case 1: MessageBox.Show("El Tipo y/o Numero de Documento no coinciden con los del usuario logeado."); break;
                case 2: MessageBox.Show("El importe ingreseado debe ser mayor que 0."); break;
                case 3: MessageBox.Show("l saldo disponible es insuficiente para realizar el retiro."); break;
                case 4: MessageBox.Show("La cuenta no se encuentra Habilitada."); break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
