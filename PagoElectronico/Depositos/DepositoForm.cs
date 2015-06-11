using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Depositos
{
    public partial class FormDeposito : Form
    {
        public FormDeposito()
        {
            InitializeComponent();
            DbComunicator db = new DbComunicator();
            db.EjecutarQuery("SELECT Moneda_Nombre, Moneda_Simbolo FROM [GD1C2015].[NULL].[Moneda]");
            while (db.getLector().Read())
            {
                comboMoneda.Items.Add(db.getLector()["Moneda_Nombre"].ToString());
            }
            db.CerrarConexion();


     

            DbComunicator db1 = new DbComunicator();/* FALTA COMPLETAR SELECT QUE TRAE TARJETA DEL CLIENTE */
            db1.EjecutarQuery("SELECT Tarjeta_Numero_Visible FROM [GD1C2015].[NULL].[Tarjeta]");
            while (db1.getLector().Read())
            {
                comboTarjeta.Items.Add(db1.getLector()["Tarjeta_Numero_Visible"].ToString());
            }
            db1.CerrarConexion();

            DbComunicator db2 = new DbComunicator();/* FALTA COMPLETAR SELECT QUE TRAE CUENTA DEL CLIENTE */
            db2.EjecutarQuery("SELECT Cuenta_Numero FROM [GD1C2015].[NULL].[Cuenta]");
            while (db2.getLector().Read())
            {
                comboCuenta.Items.Add(db2.getLector()["Cuenta_Numero"].ToString());
            }
            db2.CerrarConexion();
            
        }
  

        private void botonRealizar_Click(object sender, EventArgs e)
        {

         /* Falta agregar las chequeos de Tarjeta y los campos incompletos (moneda y cuentas) */
            
           int num = Int32.Parse(maskedImporte.Text);
        
             if ( num >= 1 ) //& ( fechaVencTarjeta >= DateTime.Now )
               
               MessageBox.Show("Se realizo");
            else
                MessageBox.Show("Importe invalido");
           // if ( fechaVencTarjeta > DateTime.Now ) 
               
            
        }

        private void comboTarjeta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormDeposito_Load(object sender, EventArgs e)
        {

        }

        private void maskedImporte_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

       





    }
}
