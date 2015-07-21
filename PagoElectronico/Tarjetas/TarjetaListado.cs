using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico.Tarjetas
{
    public partial class TarjetaListado : Form
    {
        int cliCod;
        public TarjetaListado(string username)
        {
            InitializeComponent();
            string query = "SELECT Cli_Cod FROM [GD1C2015].[NULL].[Cliente] WHERE Usr_Username = '" + username + "'";
            DbComunicator db = new DbComunicator();
            db.EjecutarQuery(query);
            db.getLector().Read();
            this.cliCod = Convert.ToInt32(db.getLector()["Cli_Cod"]);
            db.CerrarConexion();
            this.SearchTarjetas();
        }

        private void ActivarAcciones(object sender, EventArgs e)
        {
            if (!this.tarjetaGridView.SelectedRows[0].Cells["Tarjeta_Numero"].Value.ToString().Equals(""))
            {
                this.editarButton.Enabled = true;
                this.eliminarButton.Enabled = true;
                this.asociarButton.Enabled = true;
                this.desasociarButton.Enabled = true;
                tarjetaGridView.SelectionChanged += this.DesactivarAcciones;
            }
            else this.DesactivarAcciones(sender, e);
        }

        private void DesactivarAcciones(object sender, EventArgs e)
        {
            this.editarButton.Enabled = false;
            this.eliminarButton.Enabled = false;
            this.asociarButton.Enabled = false;
            this.desasociarButton.Enabled = false;
            this.tarjetaGridView.SelectionChanged -= this.DesactivarAcciones;
        }

        private void desasociarButton_Click(object sender, EventArgs e)
        {
            string tarjetaPk = tarjetaGridView.SelectedRows[0].Cells["Tarjeta_Numero"].Value.ToString();
            string tarjetaEstado = tarjetaGridView.SelectedRows[0].Cells["Estado"].Value.ToString();

            if (tarjetaEstado == "Desasociada")
            {
                MessageBox.Show("La tarjeta seleccionada ya se encuentra desasociada.");
            }
            else
            {
                DbComunicator db = new DbComunicator();
                SqlCommand spDesasociarTarjeta = db.GetStoreProcedure("NULL.spDesasociarTarjeta");
                spDesasociarTarjeta.Parameters.Add(new SqlParameter("@Tarjeta_Pk", tarjetaPk));
                spDesasociarTarjeta.ExecuteNonQuery();
                db.CerrarConexion();
                this.SearchTarjetas();
            }
        }

        private void SearchTarjetas() {
            DbComunicator db = new DbComunicator();
            string queryTarjetas = "SELECT t.Tarjeta_Codigo_Seg, t.Tarjeta_Numero, t.Tarjeta_Numero_Visible Numero, ";
            queryTarjetas = queryTarjetas + "t.Tarjeta_Fecha_Emision Fecha_Emision, t.Tarjeta_Fecha_Vencimiento Fecha_Vencimiento, ";
            queryTarjetas = queryTarjetas + "  e.Emisor_Desc Emisor, t.Tarjeta_Estado Estado, e.Emisor_Cod FROM [GD1C2015].[NULL].[Tarjeta] as t, [GD1C2015].[NULL].[Emisor] as e WHERE Cli_Cod ='" + this.cliCod + "' AND t.Emisor_Cod = e.Emisor_Cod AND t.Tarjeta_Borrado = 0";
            tarjetaGridView.DataSource = db.GetDataAdapter(queryTarjetas).Tables[0];
            tarjetaGridView.Columns["Tarjeta_Numero"].Visible = false;
            tarjetaGridView.Columns["Emisor_Cod"].Visible = false;
            tarjetaGridView.Columns["Tarjeta_Codigo_Seg"].Visible = false;
            db.CerrarConexion();
        }

        private void editarButton_Click(object sender, EventArgs e){
            new TarjetaEdicion(tarjetaGridView.SelectedRows[0]).ShowDialog();
            this.SearchTarjetas();
        }

        private void crearButton_Click(object sender, EventArgs e)
        {
            new TarjetaCreacion(cliCod).ShowDialog();
            this.SearchTarjetas();
        }

        private void asociarButton_Click(object sender, EventArgs e)
        {
            string tarjetaPk = tarjetaGridView.SelectedRows[0].Cells["Tarjeta_Numero"].Value.ToString();
            string tarjetaEstado = tarjetaGridView.SelectedRows[0].Cells["Estado"].Value.ToString();

            if ( tarjetaEstado == "Asociada")
            {
                MessageBox.Show("La tarjeta seleccionada ya se encuentra asociada.");
            }
            else 
            {
                DbComunicator db = new DbComunicator();
                SqlCommand spAsociarTarjeta = db.GetStoreProcedure("NULL.spAsociarTarjeta");
                spAsociarTarjeta.Parameters.Add(new SqlParameter("@Tarjeta_Pk", tarjetaPk));
                spAsociarTarjeta.ExecuteNonQuery();
                db.CerrarConexion();
                this.SearchTarjetas();
            }
        }

        private void eliminarButton_Click(object sender, EventArgs e)
        {
            string tarjetaPk = tarjetaGridView.SelectedRows[0].Cells["Tarjeta_Numero"].Value.ToString();

            DialogResult dialogResult = MessageBox.Show("La tarjeta seleccionada sera eliminada. ¿Esta seguro?", "Eliminar Tarjeta", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                DbComunicator db = new DbComunicator();
                SqlCommand spEliminarTarjeta = db.GetStoreProcedure("NULL.spEliminarTarjeta");
                spEliminarTarjeta.Parameters.Add(new SqlParameter("@Tarjeta_Pk", tarjetaPk));
                spEliminarTarjeta.ExecuteNonQuery();
                db.CerrarConexion();
                this.SearchTarjetas();
            }
        }

        private void TarjetaListado_Load(object sender, EventArgs e){
            tarjetaGridView.CellClick += this.ActivarAcciones;
            tarjetaGridView.RowHeaderMouseClick += this.ActivarAcciones;
        }

        
    }
}
