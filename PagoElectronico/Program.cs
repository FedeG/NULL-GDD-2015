using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PagoElectronico
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DbComunicator db = new DbComunicator();
            SqlCommand sp = db.GetStoreProcedure("NULL.spDeshabilitarCuentasVencidas");
            sp.Parameters.Add("@Hoy", Properties.Settings.Default.FechaSistema);
            sp.ExecuteNonQuery();
            Application.Run(new Login.FormLogin());
        }
    }
}
