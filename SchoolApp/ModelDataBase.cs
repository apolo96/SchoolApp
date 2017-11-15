using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp
{
    class ModelDataBase
    {
        private static SqlConnection conexion = null;
        private static SqlCommand command;
        private static SqlDataAdapter adapter;
        //private static DataTable dt = new DataTable();
        private static DataSet ds;
        public static string con = "Data Source=.;Initial Catalog=colegio;Integrated Security=True";
        public static int lastId;

        private ModelDataBase()
        {

        }

        public static SqlConnection connectDb()
        {
            if (conexion == null)
            {
                conexion = new SqlConnection(con);               
            }
            return conexion;
        }

        public static void executeQuery(string query)
        {
            try
            {                
                connectDb().Open();
                command = new SqlCommand(query, conexion);                
                setAdapterCommand();
            }
            catch (Exception ex)
            {
                //Mensaje.msj = ex.Message.ToString();
            }
        }

        public static void executeSp(string spName, ArrayList listParameter)
        {
            try
            {
                connectDb().Open();
                command = new SqlCommand(spName, conexion);
                command.CommandType = CommandType.StoredProcedure;
                if (listParameter != null)
                {
                    for (int x = 0; x < listParameter.Count; x++)
                    {
                        ParameterSp pSp = (ParameterSp)listParameter[x];
                        command.Parameters.Add(pSp.Name, pSp.Type).Value = pSp.Val;
                    }
                    listParameter.Clear();
                }
                setAdapterCommand();

            }
            catch (Exception ex)
            {
                
            }

        }

        private static void setAdapterCommand()
        {
            try
            {
                adapter = new SqlDataAdapter(command);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message.ToString());
            }
        }

        public static int getLastId()
        {
            return (int)command.ExecuteScalar();
        }

        public static DataView getDataSet()
        {
            return ds.Tables[0].DefaultView;
        }

        public static void setDataAdpater(string tableName)
        {
            ds = new DataSet();
            adapter.Fill(ds);
            connectDb().Close();
        }
        //public static DataTable getDataTable()
        //{
        //    return dt;
        //}
    }
}
