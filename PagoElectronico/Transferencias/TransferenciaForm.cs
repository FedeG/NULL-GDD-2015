﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico.Transferencias
{
    public partial class TransferenciaForm : Form
    {
        Commons.EnabledButtons enabledButtons;
        Commons.Validator validator;

        public TransferenciaForm(string username){
            string query = "SELECT Cuenta_Numero FROM [GD1C2015].[NULL].[Cuenta] WHERE Cli_Cod = (SELECT Cliente.Cli_Cod FROM [GD1C2015].[NULL].[Cliente] as Cliente WHERE Usr_Username = '" + username + "')";
            InitializeComponent();
            this.validator = new Commons.Validator();
            this.enabledButtons = new Commons.EnabledButtons();
            this.enabledButtons.RegisterTextBox(this.importeTextBox);
            this.enabledButtons.RegisterTextBox(this.cuentaDestinoTextBox);
            this.enabledButtons.RegisterButton(this.realizarButton);
            DbComunicator db = new DbComunicator();
            
            cuentaOrigenComboBox.DataSource = new BindingSource(db.GetQueryDictionary(query, "Cuenta_Numero", "Cuenta_Numero"), null);
            cuentaOrigenComboBox.DisplayMember = "Key";
            cuentaOrigenComboBox.ValueMember = "Value";
            
            string queryMonedas = "SELECT Moneda_Nombre, Moneda_Simbolo FROM [GD1C2015].[NULL].[Moneda]";
            comboMoneda.DataSource = new BindingSource(db.GetQueryDictionary(queryMonedas, "Moneda_Simbolo", "Moneda_Nombre"), null);
            comboMoneda.DisplayMember = "Key";
            comboMoneda.ValueMember = "Value";

            db.CerrarConexion();
            this.cuentaDestinoTextBox.KeyPress += this.Number_KeyPress;
            this.importeTextBox.KeyPress += this.NumberDouble_KeyPress;
        }

        private void Number_KeyPress(object sender, KeyPressEventArgs e){
            this.validator.KeyPressBinding(this.validator.validateInt, false, e);
        }

        private void NumberDouble_KeyPress(object sender, KeyPressEventArgs e){
            this.validator.KeyPressBinding(this.validator.validateDouble, false, e);
        }

        private void realizarButton_Click(object sender, EventArgs e)
        {
            DbComunicator db = new DbComunicator();
            SqlCommand spRealizarTransferencia = db.GetStoreProcedure("NULL.spRealizarTransferencia");
            SqlParameter returnParameter = spRealizarTransferencia.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            spRealizarTransferencia.Parameters.Add(new SqlParameter("@Cuenta_Origen", Convert.ToInt64(cuentaOrigenComboBox.SelectedValue)));
            spRealizarTransferencia.Parameters.Add(new SqlParameter("@Cuenta_Destino", Convert.ToInt64(cuentaDestinoTextBox.Text)));
            spRealizarTransferencia.Parameters.Add(new SqlParameter("@Importe", Convert.ToDouble(importeTextBox.Text)));
            spRealizarTransferencia.Parameters.Add(new SqlParameter("@Fecha_Transferencia", Properties.Settings.Default.FechaSistema));
            spRealizarTransferencia.Parameters.Add(new SqlParameter("@Moneda_Nombre", comboMoneda.SelectedValue.ToString()));

            spRealizarTransferencia.ExecuteNonQuery();

            if ((int)returnParameter.Value == 0)
            {
                MessageBox.Show("Transferencia realizada con exito.");
                this.Close();
            }

            if ((int)returnParameter.Value == 1)
            {
                MessageBox.Show("La cuenta de destino debe estar habilitada o inhabilitada para poder recibir dinero.");
            }

            if ((int)returnParameter.Value == 2)
            {
                MessageBox.Show("El Importe debe ser mayor que 0.");
            }

            if ((int)returnParameter.Value == 3)
            {
                MessageBox.Show("El saldo disponible es insuficiente para realizar la transferencia.");
            }

            if ((int)returnParameter.Value == 4)
            {
                MessageBox.Show("La cuenta de origen debe ser distinta a la de destino.");
            }

            if ((int)returnParameter.Value == 5)
            {
                MessageBox.Show("La cuenta de destino ingresada no existe.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
