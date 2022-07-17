using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Acceso : Conexion
    {
        SqlConnection con = new SqlConnection();
        private string xTextCommand;
        private SqlCommand parameters = new SqlCommand();

        protected string xCommandText
        {
            get { return xTextCommand; }
            set { xTextCommand = value; }
        }

        protected SqlCommand xParameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        private void Conectar()
        {
            con.ConnectionString = conexion;
            con.Open();
        }
        private void Desconectar()
        {
            con.Close();
        }

        public void executeNonQuery()
        {

            Conectar();

            SqlTransaction TR = con.BeginTransaction();
            SqlCommand command = new SqlCommand(xCommandText, con, TR);

            command.CommandType = CommandType.Text;
            command.Parameters.Clear();


            foreach (SqlParameter p in xParameters.Parameters)
            {
                command.Parameters.AddWithValue(p.ParameterName, p.SqlValue);
            }

            try
            {

                command.ExecuteNonQuery();
                TR.Commit();
            }

            catch (SqlException exc)
            {

                TR.Rollback();
                throw new Exception("ocurrio un Error en BD:" + exc.Message);
            }
            //otros errores
            catch (Exception exc2)
            {

                TR.Rollback();
                throw new Exception("ocurrio un Error :" + exc2.Message);
            }
            finally
            {
                Desconectar();
            }
        }


        public DataTable ExecuteReader()
        {

            Conectar();
            SqlDataReader dr;
            DataTable dt = new DataTable();
            SqlTransaction TR = con.BeginTransaction();
            SqlCommand comando = new SqlCommand(xCommandText, con, TR);

            comando.CommandType = CommandType.Text;
            comando.Parameters.Clear();


            foreach (SqlParameter p in xParameters.Parameters)
            {
                comando.Parameters.AddWithValue(p.ParameterName, p.SqlValue);
            }

            try
            {

                dr = comando.ExecuteReader();
                dt.Load(dr);
                TR.Commit();
                return dt;
            }

            catch (SqlException exc)
            {

                TR.Rollback();
                throw new Exception("ocurrio un Error en BD:" + exc.Message);
            }

            catch (Exception exc2)
            {

                TR.Rollback();
                throw new Exception("ocurrio un Error :" + exc2.Message);
            }
            finally
            {

                Desconectar();

            }

        }

        public virtual int ExecuteNonEscalar()
        {
            Conectar();
            SqlTransaction transaction = con.BeginTransaction();
            SqlCommand command = new SqlCommand(xCommandText, con, transaction);

            command.CommandType = CommandType.Text;
            command.Parameters.Clear();

            foreach (SqlParameter p in xParameters.Parameters)
            {
                command.Parameters.AddWithValue(p.ParameterName, p.SqlValue);
            }

            SqlParameter sp_return = new SqlParameter();
            sp_return.Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add(sp_return);

            int outputId = 0;

            try
            {
                outputId = (int)command.ExecuteScalar();
                transaction.Commit();
            }
            catch (SqlException exc)
            {
                transaction.Rollback();
                throw new Exception("Ocurrió un error en BD: " + exc.Message);
            }
            catch (Exception exc2)
            {
                transaction.Rollback();
                throw new Exception("Ocurrió un error: " + exc2.Message);
            }
            finally
            {
                Desconectar();
            }

            return outputId;
        }
    }
}
