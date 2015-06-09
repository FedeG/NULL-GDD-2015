using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico.ABM_Cliente
{
    public partial class ClienteData : Form
    {
        public DbComunicator db;
        public Dictionary<object, object> TipoDocs, NacionalidadesDict;

        public ClienteData(){
            InitializeComponent();
            this.db = new DbComunicator();
            this.Nacionalidades_Load();
            this.Tipo_Docs_Load();
        }

        private void Nacionalidades_Load(){
            this.db.ConectarConDB();
            this.NacionalidadesDict = db.GetQueryDictionary("SELECT Nac_Pais_Cod, Nac_Nombre FROM GD1C2015.[NULL].Nacionalidad", "Nac_Pais_Cod", "Nac_Nombre");
            foreach (object Nac_Pais_Cod in NacionalidadesDict.Keys){
                string Nac_Nombre = NacionalidadesDict[Nac_Pais_Cod].ToString();
                if (Nac_Nombre != "") NacCliente.Items.Add(Nac_Nombre);
            }
            this.db.CerrarConexion();
        }

        private void Tipo_Docs_Load(){
            this.db.ConectarConDB();
            this.TipoDocs = db.GetQueryDictionary("SELECT TipoDoc_Cod, TipoDoc_Desc FROM GD1C2015.[NULL].TipoDoc WHERE TipoDoc_Borrado=0", "TipoDoc_Cod", "TipoDoc_Desc");
            foreach (object TipoDoc_Cod in TipoDocs.Keys){
                string TipoDoc_Desc = TipoDocs[TipoDoc_Cod].ToString();
                if (TipoDoc_Desc != "") TipoDocCliente.Items.Add(TipoDoc_Desc);
            }
            this.db.CerrarConexion();
        }

        public void ExecStoredProcedure(string sp_name){
            SqlCommand sp = this.db.GetStoreProcedure(sp_name);
            sp.Parameters.Add(new SqlParameter("@Usr_Username", Username.Text));
            sp.Parameters.Add(new SqlParameter("@Usr_Password", Password.Text));
            sp.Parameters.Add(new SqlParameter("@Usr_Pregunta_Secreta", Pregunta.Text));
            sp.Parameters.Add(new SqlParameter("@Usr_Respuesta_Secreta", RespuestaSecreta.Text));
            sp.Parameters.Add(new SqlParameter("@Cli_Nombre", Nombre.Text));
            sp.Parameters.Add(new SqlParameter("@Cli_Apellido", Apellido.Text));
            sp.Parameters.Add(new SqlParameter("@Cli_Nro_Doc", NumDoc.Text));
            sp.Parameters.Add(new SqlParameter("@Cli_Dom_Calle", Calle.Text));
            sp.Parameters.Add(new SqlParameter("@Cli_Localidad", Localidad.Text));
            sp.Parameters.Add(new SqlParameter("@Cli_Mail", Mail.Text));
            sp.Parameters.Add(new SqlParameter("@Cli_Nacionalidad", NacCliente.Text)); 
            sp.Parameters.Add(new SqlParameter("@Cli_Dom_Nro", NumDomicilio.Text));
            sp.Parameters.Add(new SqlParameter("@Cli_Dom_Piso", Piso.Text));
            sp.Parameters.Add(new SqlParameter("@Cli_Dom_Depto", Depto.Text));
            object TipoDocCod = this.TipoDocs.FirstOrDefault(TipoDoc => TipoDoc.Value.ToString().Equals(TipoDocCliente.SelectedText.ToString())).Key;
            sp.Parameters.Add(new SqlParameter("@TipoDoc_Cod", TipoDocCod));
            object PaisCod = this.NacionalidadesDict.FirstOrDefault(Pais => Pais.Value.ToString().Equals(NacCliente.SelectedText.ToString())).Key;
            sp.Parameters.Add(new SqlParameter("@Pais_Codigo", PaisCod));
            sp.Parameters.Add(new SqlParameter("@Cli_Fecha_Nac", FechaNacimiento.Value.Date));
            sp.ExecuteNonQuery();
        }

    }
}
