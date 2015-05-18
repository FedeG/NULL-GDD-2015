using System;
using System.Data;
using System.Data.SqlClient;

namespace PagoElectronico{

    public class DbComunicator{
        private SqlConnection ConexionConBD;
        private SqlCommand Consulta;
        private SqlDataReader Lector;

        public void EjecutarQuery(string query){
            this.ConectarConDB();
            this.getQuery(query);
            this.CerrarConexion();
        }

        public void ObtenerQuery(string query){
            Consulta = new SqlCommand(query, this.ConexionConBD);
            this.Lector = Consulta.ExecuteReader();
        }

        public void ConectarConDB(){
            // Crear la conexión con la base de datos
            string strConexión = "Data Source=.\\sqlexpress2005;Initial Catalog=" + Properties.Settings.Default.DbName + ";Integrated Security=True";
            this.ConexionConBD = new SqlConnection(strConexión);

            // Abrir la base de datos
            this.ConexionConBD.Open();
        }

        public void CerrarConexion(){
            // Cerrar la conexión cuando ya no sea necesaria
            if (Lector != null)
                Lector.Close();
            if (ConexionConBD != null)
                ConexionConBD.Close();
        }

        public void ExecuteTransaction(string nombre, List<string> querys){
            this.ConectarConDB();
            SqlCommand command = this.ConexionConBD.CreateCommand();
            SqlTransaction transaction;
            transaction = this.ConexionConBD.BeginTransaction(nombre);
            command.Connection = this.ConexionConBD;
            command.Transaction = transaction;
            try{
                foreach (string query in querys){
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (Exception ex){
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine(" Message: {0}", ex.Message);
                try{
                    transaction.Rollback();
                } catch (Exception ex2){
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine(" Message: {0}", ex2.Message);
                }
            }
            this.CerrarConexion();
        }

        // Ejemplo de uso
        public static void Main(){
            DbComunicator db = new DbComunicator();
            try {
                db.EjecutarQuery("SELECT Nombre FROM CLIENT");
            }
            catch (Exception e){
                Console.WriteLine("Error: " + e.Message);
            }
            finally {
                Console.WriteLine("Los nombres de los clientes son: ");
                while (db.Lector.Read()){
                    Console.WriteLine(" - " + db.Lector["Nombre"]);
                };
            }
        }

    }

}
