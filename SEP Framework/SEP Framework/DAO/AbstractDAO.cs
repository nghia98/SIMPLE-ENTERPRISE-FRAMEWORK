using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEP_Framework.DAO
{
    public abstract class AbstractDAO
    {
        protected AbstractProcessData ProcessData;

        public abstract string GetPrimaryKey(string strNameTable);

        public abstract DataTable LoadData(string strNameTable);

        public abstract bool InsertData(Dictionary<string, string> data, string strNameTable);

        public abstract bool UpdateData(Dictionary<string, string> data, string strNameTable, string primaryKey);

        public abstract bool DeleteData(string strNameTable, string primaryKey, string keyValue);

        public abstract void CreateAccountTable();

        public abstract bool ValidateUser(string username, string password);

        public abstract bool CreateUser(string username, string password);

        public abstract bool SignOutUser(string username);
    }
}
