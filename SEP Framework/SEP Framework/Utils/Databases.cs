using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SEP_Framework
{
    class Databases
    {
        private string connectionString = "Data Source=.; Integrated Security=True;";
        public List<string> getListDatabse()
        {
            List<String> listNameDatabases = new List<string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listNameDatabases.Add(dr[0].ToString());
                        }
                    }
                }
            }

            return listNameDatabases;
        }
        
    }
}
