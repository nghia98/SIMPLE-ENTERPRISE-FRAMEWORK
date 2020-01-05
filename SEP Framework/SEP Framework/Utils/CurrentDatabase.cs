using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SEP_Framework
{
    class CurrentDatabase
    {
        private static CurrentDatabase currentDatabase;
        public string connectionString;

        private CurrentDatabase()
        {

        }

        public static CurrentDatabase getInstance()
        {
            if (currentDatabase == null)
            {
                currentDatabase = new CurrentDatabase();
            }
            return currentDatabase;
        }

        public List<string> getAllTables()
        {
            List<String> listNameTables = new List<string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT name from sys.Tables", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listNameTables.Add(dr[0].ToString());
                        }
                    }
                }
            }

            return listNameTables;
        }
    }
}
