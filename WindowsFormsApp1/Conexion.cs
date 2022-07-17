using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
   public class Conexion
    {
        private readonly string server;
        private readonly string baseDatos;
        public string conexion { get; set; }

        public Conexion()
        {
            SqlConnection instancia = new SqlConnection();
            server = ConfigurationManager.AppSettings["server"];
            baseDatos = ConfigurationManager.AppSettings["base"];

            SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder()
            {
                DataSource = server,
                InitialCatalog = baseDatos,
                IntegratedSecurity = true,
            };

            conexion = sqlConnectionString.ConnectionString;
        }

        public void conectar()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = conexion;
            try
            {
                con.Open();
                con.Close();
            }
            catch (SqlException exc)
            {

                throw new Exception("ocurrio un Error en BD:" + exc.Message);
            }

        }
    }
}
