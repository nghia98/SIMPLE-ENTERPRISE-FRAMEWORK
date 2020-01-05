using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEP_Framework.DAO
{
    public abstract class AbstractProcessData
    {
        protected SqlConnection connection;

        public abstract DataTable LoadData(string sql);

        public abstract int ExecuteData(string sql);

        public abstract bool isExist(string sql);
    }
}
