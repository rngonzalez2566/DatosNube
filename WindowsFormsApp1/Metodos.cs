using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Metodos : Acceso
    {
        public DataTable getTabla()
        {
            DataTable dt = new DataTable();
            xCommandText = "SELECT * FROM DATOS";
            return dt = ExecuteReader();
        }

        public DataTable getPendientes()
        {
            DataTable dt = new DataTable();
            xCommandText = "SELECT * FROM DATOS WHERE subido = 0";
            return dt = ExecuteReader();
        }

        public void altaNube(string medicion, string porcentaje)
        {
            try
            {

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "sistemas-hardware.database.windows.net";
                builder.UserID = "rodrigo";
                builder.Password = "Password123";
                builder.InitialCatalog = "Sistemas_Hardware";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    
                    String sql = "INSERT INTO[dbo].[Table](FECHA, MEDICION, PORCENTAJE) VALUES(GETDATE(), @medicion, @porcentaje)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@medicion", medicion);
                        command.Parameters.AddWithValue("@porcentaje", porcentaje);
                        command.ExecuteNonQuery();
                        
                         
                    }
                }

            }
            catch
            {

                throw new Exception("Se produjo un error con la base de datos");
            }
        }

        public void marcarSubidos(int id)
        {
            try
            {
                xCommandText = "UPDATE DATOS SET SUBIDO = 1 WHERE ID = @id";
                xParameters.Parameters.Clear();
                xParameters.Parameters.AddWithValue("@id", id);
                executeNonQuery();
            }
            catch
            {

                throw new Exception("Se produjo un error con la base de datos");
            }
        }
    }
}
