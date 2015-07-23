using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using PagoElectronico.Commons;

namespace PagoElectronico.ABM_Cliente
{
    public partial class ClienteData : Form
    {
        public DbComunicator db;
        public Commons.Validator validator;
        public Commons.EnabledButtons enabledButtons;
        public Dictionary<object, object> TipoDocs, NacionalidadesDict;

        public ClienteData(){
            InitializeComponent();
            this.db = new DbComunicator();
            this.validator = new Commons.Validator();
            this.loadEnabledButtons();
            this.Nacionalidades_Load();
            this.Tipo_Docs_Load();
        }

        private void Nacionalidades_Load(){
            this.db.ConectarConDB();
            this.NacionalidadesDict = db.GetQueryDictionary("SELECT Nac_Pais_Cod, Nac_Nombre FROM GD1C2015.[NULL].Nacionalidad", "Nac_Pais_Cod", "Nac_Nombre");
            InputNacCliente.DataSource = new BindingSource(this.NacionalidadesDict, null);
            InputNacCliente.DisplayMember = "Value";
            InputNacCliente.ValueMember = "Key";
            this.db.CerrarConexion();
        }

        public void loadEnabledButtons(){
            this.enabledButtons = new Commons.EnabledButtons();
            this.enabledButtons.RegisterTextBox(this.InputApellido);
            this.enabledButtons.RegisterTextBox(this.InputCalle);
            this.enabledButtons.RegisterTextBox(this.InputDepto);
            this.enabledButtons.RegisterTextBox(this.InputLocalidad);
            this.enabledButtons.RegisterTextBox(this.InputMail);
            this.enabledButtons.RegisterTextBox(this.InputNombre);
            this.enabledButtons.RegisterTextBox(this.InputNumDoc);
            this.enabledButtons.RegisterTextBox(this.InputNumDomicilio);
            this.enabledButtons.RegisterTextBox(this.InputPassword);
            this.enabledButtons.RegisterTextBox(this.InputPiso);
            this.enabledButtons.RegisterTextBox(this.InputPregunta);
            this.enabledButtons.RegisterTextBox(this.InputRespuestaSecreta);
            this.enabledButtons.RegisterTextBox(this.InputUsername);
        }

        private void Tipo_Docs_Load(){
            this.db.ConectarConDB();
            this.TipoDocs = db.GetQueryDictionary("SELECT TipoDoc_Cod, TipoDoc_Desc FROM GD1C2015.[NULL].TipoDoc WHERE TipoDoc_Borrado=0", "TipoDoc_Cod", "TipoDoc_Desc");
            InputTipoDocCliente.DataSource = new BindingSource(this.TipoDocs, null);
            InputTipoDocCliente.DisplayMember = "Value";
            InputTipoDocCliente.ValueMember = "Key";
            this.db.CerrarConexion();
        }

        public int ExecStoredProcedure(string sp_name, Boolean hashed){
            SqlCommand sp = this.db.GetStoreProcedure(sp_name);
            SqlParameter returnParameter = sp.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            sp.Parameters.Add(new SqlParameter("@Usr_Username", InputUsername.Text));
            string password = this.InputPassword.Text;
            if (!hashed)
               password = new Sha256Generator().GetHashString(InputPassword.Text);
            sp.Parameters.Add(new SqlParameter("@Usr_Password", password));
            sp.Parameters.Add(new SqlParameter("@Usr_Pregunta_Secreta", InputPregunta.Text));
            string respuesta_secreta = this.InputRespuestaSecreta.Text;
            if (!hashed)
                respuesta_secreta = new Sha256Generator().GetHashString(InputRespuestaSecreta.Text);
            sp.Parameters.Add(new SqlParameter("@Usr_Respuesta_Secreta", respuesta_secreta));
            sp.Parameters.Add(new SqlParameter("@Cli_Nombre", InputNombre.Text));
            sp.Parameters.Add(new SqlParameter("@Cli_Apellido", InputApellido.Text));
            sp.Parameters.Add(new SqlParameter("@Cli_Nro_Doc", InputNumDoc.Text));
            sp.Parameters.Add(new SqlParameter("@Cli_Dom_Calle", InputCalle.Text));
            sp.Parameters.Add(new SqlParameter("@Cli_Localidad", InputLocalidad.Text));
            sp.Parameters.Add(new SqlParameter("@Cli_Mail", InputMail.Text));
            sp.Parameters.Add(new SqlParameter("@Cli_Nacionalidad", InputNacCliente.Text)); 
            sp.Parameters.Add(new SqlParameter("@Cli_Dom_Nro", InputNumDomicilio.Text));
            sp.Parameters.Add(new SqlParameter("@Cli_Dom_Piso", InputPiso.Text));
            sp.Parameters.Add(new SqlParameter("@Cli_Dom_Depto", InputDepto.Text));
            sp.Parameters.Add(new SqlParameter("@TipoDoc_Cod", InputTipoDocCliente.SelectedValue));
            sp.Parameters.Add(new SqlParameter("@Pais_Codigo", InputNacCliente.SelectedValue));
            sp.Parameters.Add(new SqlParameter("@Cli_Fecha_Nac", InputFechaNacimiento.Value.Date));
            sp.Parameters.Add(new SqlParameter("@Fecha_Sistema", Properties.Settings.Default.FechaSistema));
            sp.ExecuteNonQuery();
            return (int) returnParameter.Value;
        }

        private void InputNumField_KeyPress(object sender, KeyPressEventArgs e){
            this.validator.KeyPressBinding(this.validator.validateInt, false, e);
        }

    }
}
