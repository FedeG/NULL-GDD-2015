using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Listados
{
    public partial class ListadosForm : Form
    {
        Dictionary<object, object> Listado, Trimestres;
        Commons.Validator validator;
        Commons.EnabledButtons enabledButtons;
        DbComunicator db;

        public ListadosForm(){
            InitializeComponent();
            this.db = new DbComunicator();
            this.validator = new Commons.Validator();
            this.Listado = new Dictionary<object, object>();
            this.Trimestres = new Dictionary<object, object>();
            this.enabledButtons = new Commons.EnabledButtons();
            this.enabledButtons.RegisterButton(this.listarButton);
            this.enabledButtons.RegisterTextBox(this.tbAnio);
            this.loadListado();
            this.loadTrimestres();
        }

        private void listarButton_Click(object sender, EventArgs e){
            int trimestre = Convert.ToInt16(this.cbTrimestre.SelectedValue);
            int anio = Convert.ToInt16(this.tbAnio.Text);
            string querylistado = this.cbListado.SelectedValue.ToString();
            querylistado = querylistado.Replace("@year", anio.ToString())
                .Replace("@mesi", trimestre.ToString())
                .Replace("@mesf", (trimestre+2).ToString());
            this.listadoTable.DataSource = db.GetDataAdapter(querylistado).Tables[0];
        }

        private void InputNumField_KeyPress(object sender, KeyPressEventArgs e){
            this.validator.KeyPressBinding(this.validator.validateInt, false, e);
        }

        private void loadTrimestres(){
            this.Trimestres.Add("1-3", 1);
            this.Trimestres.Add("4-6", 4);
            this.Trimestres.Add("7-9", 7);
            this.Trimestres.Add("10-12", 10);
            this.cbTrimestre.DataSource = new BindingSource(this.Trimestres, null);
            this.cbTrimestre.DisplayMember = "Key";
            this.cbTrimestre.ValueMember = "Value";
        }

        private void loadListado(){
            this.Listado.Add("Top 5 Clientes que alguna de sus cuentas fueron inhabilitadas por no pagar los costos de transacción.", "SELECT TOP 5 cli.Cli_Nombre, cli.Cli_Apellido, cli.Usr_Username, cu.Cuenta_Numero FROM [GD1C2015].[NULL].[Cliente] AS cli, [GD1C2015].[NULL].[CuentaInhabilitadaLogeo] AS cuinl, [GD1C2015].[NULL].[Cuenta] AS cu WHERE cu.Cli_Cod = cli.Cli_Cod AND cuinl.Cuenta_Numero = cu.Cuenta_Numero AND cu.Cuenta_Borrado=0 AND cli.Cli_Borrado=0 AND YEAR(cuinl.Inhabilitada_Fecha) = @year AND MONTH(cuinl.Inhabilitada_Fecha) BETWEEN @mesi AND @mesf GROUP BY cli.Cli_Nombre, cli.Cli_Apellido, cli.Usr_Username, cu.Cuenta_Numero ORDER BY COUNT(*) DESC");
            this.Listado.Add("Top 5 Cliente con mayor cantidad de comisiones facturadas en todas sus cuentas.", "SELECT TOP 5 cli.Cli_Nombre, cli.Cli_Apellido, cli.Usr_Username FROM [GD1C2015].[NULL].[Cliente] AS cli, [GD1C2015].[NULL].[Transaccion] AS trans, [GD1C2015].[NULL].[Cuenta] AS cu, [GD1C2015].[NULL].[Cuenta] AS cud, [GD1C2015].[NULL].[Transferencia] AS tra WHERE trans.Cuenta_Numero = cu.Cuenta_Numero AND cli.Cli_Cod = cu.Cli_Cod AND cli.Cli_Cod = cud.Cli_Cod AND (trans.Transacc_Transf_Codigo IS NOT NULL) AND tra.Transf_Codigo = trans.Transacc_Transf_Codigo AND trans.Transacc_Facturada = 1 AND cli.Cli_Borrado=0 AND trans.Transacc_Borrado=0 AND cu.Cuenta_Borrado = 0 AND YEAR(tra.Transf_Fecha) = @year AND MONTH(tra.Transf_Fecha) BETWEEN @mesi AND @mesf GROUP BY cli.Cli_Nombre, cli.Cli_Apellido, cli.Usr_Username ORDER BY COUNT(*) DESC");
            this.Listado.Add("Top 5 Clientes con mayor cantidad de transacciones realizadas entre cuentas propias.", "SELECT TOP 5 cli.Cli_Nombre, cli.Cli_Apellido, cli.Usr_Username FROM [GD1C2015].[NULL].[Cliente] AS cli, [GD1C2015].[NULL].[Transferencia] AS tra, [GD1C2015].[NULL].[Cuenta] AS cuo, [GD1C2015].[NULL].[Cuenta] AS cud   WHERE tra.Cuenta_Origen_Numero = cuo.Cuenta_Numero AND tra.Cuenta_Destino_Numero = cud.Cuenta_Numero AND cli.Cli_Cod = cuo.Cli_Cod AND cli.Cli_Cod = cud.Cli_Cod AND cli.Cli_Borrado=0 AND tra.Transf_Borrado=0 AND YEAR(tra.Transf_Fecha) = @year AND MONTH(tra.Transf_Fecha) BETWEEN @mesi AND @mesf GROUP BY cli.Cli_Nombre, cli.Cli_Apellido, cli.Usr_Username ORDER BY COUNT(*) DESC");
            this.Listado.Add("Top 5 Países con mayor cantidad de movimientos tanto ingresos como egresos.", "SELECT pa.Pais_desc, tt.Total_Movimientos FROM [GD1C2015].[NULL].fnCantidadMovimientos(2016, 1, 3) AS tt LEFT JOIN [NULL].[Pais] pa ON tt.Pais_Codigo = pa.Pais_Codigo");
            this.Listado.Add("Top 5 Total facturado para los distintos tipos de cuentas.", "SELECT cu.TipoCta_Nombre, fi.Moneda_Nombre, fi.F_Item_Cantidad*fi.F_Item_Precio_Unitario total FROM [GD1C2015].[NULL].Factura_Cabecera AS fc, [GD1C2015].[NULL].Factura_Item AS fi, [GD1C2015].[NULL].Transaccion AS tra, [GD1C2015].[NULL].Cuenta AS cu WHERE fc.Fact_Tipo=fi.Fact_Tipo and fc.Fact_Numero=fi.Fact_Numero AND tra.Transacc_Codigo=fi.Transacc_Codigo AND tra.Cuenta_Numero = cu.Cuenta_Numero AND YEAR(fc.Fact_Fecha) = @year AND MONTH(fc.Fact_Fecha) BETWEEN @mesi AND @mesf GROUP BY cu.TipoCta_Nombre, fi.Moneda_Nombre, fi.F_Item_Cantidad*fi.F_Item_Precio_Unitario ORDER BY total DESC");
            this.cbListado.DataSource = new BindingSource(this.Listado, null);
            this.cbListado.DisplayMember = "Key";
            this.cbListado.ValueMember = "Value";
        }
    }
}
