using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TesiNexus.Helpers
{
    public class DataBaseHelper
    {

        public static bool CheckingConnection(string connStrFonte)
        {
           
            using (SqlConnection connection = new SqlConnection(connStrFonte))
            {
                try
                {
                    connection.Open();
                    connection.Close();
                }
                catch (SqlException)
                {
                    return false;
                }

                return true;
            }

        }

        //private static bool IsAvailable(SqlConnection connection)
        //{
        //    try
        //    {
        //        connection.Open();
        //        connection.Close();
        //    }
        //    catch (SqlException)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

    }
}
