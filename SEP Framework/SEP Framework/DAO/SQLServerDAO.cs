using SEP_Framework.Membership;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEP_Framework.DAO
{
    public class SQLServerDAO : AbstractDAO
    {
        public SQLServerDAO(string connectionString)
        {
            this.ProcessData = new ProcessDataSQLServer(connectionString);
        }

        public override DataTable LoadData(string strNameTable)
        {
            string sql = "Select * From " + strNameTable;
            DataTable result = ProcessData.LoadData(sql);
            return result;
        }

        public override string GetPrimaryKey(string strNameTable)
        {
            string sql = "SELECT u.COLUMN_NAME, c.CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS c INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS u ON c.CONSTRAINT_NAME = u.CONSTRAINT_NAME where u.TABLE_NAME = '" + strNameTable + "' AND c.TABLE_NAME = '" + strNameTable + "' and c.CONSTRAINT_TYPE = 'PRIMARY KEY'";
            DataTable result = ProcessData.LoadData(sql);
            return result.Rows[0].Field<string>(0);
        }

        public override bool InsertData(Dictionary<string, string> data, string strNameTable)
        {
            string sql = "Insert Into " + strNameTable + " Values(";
            for (int i = 0; i < data.Count; i++)
            {
                if (i < data.Count - 1)
                {
                    sql += ("N'" + data.ElementAt(i).Value + "', ");
                }
                else
                {
                    sql += ("N'" + data.ElementAt(i).Value + "')");
                }
            }

            try
            {
                ProcessData.ExecuteData(sql);
            }
            catch (Exception ex)
            {
                throw ex;              
            }

            return true;
        }

        public override bool UpdateData(Dictionary<string, string> data, string strNameTable, string primaryKey)
        {
            string sql = "Update " + strNameTable + " Set ";
            for (int i = 0; i < data.Count; i++)
            {
                if (data.ElementAt(i).Key != primaryKey)
                {
                    if (i < data.Count - 1)
                    {
                        sql += (data.ElementAt(i).Key + " = N'" + data.ElementAt(i).Value + "', ");
                    }
                    else
                    {
                        sql += (data.ElementAt(i).Key + " = N'" + data.ElementAt(i).Value + "'");
                    }
                }
            }
            sql += " Where " + primaryKey + " = " + data[primaryKey];

            try
            {
                ProcessData.ExecuteData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public override bool DeleteData(string strNameTable, string primaryKey, string keyValue)
        {
            string sql = "Delete From " + strNameTable + " Where " + primaryKey + " = " + keyValue;

            try
            {
                ProcessData.ExecuteData(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public override void CreateAccountTable()
        {
            if (!isExistTableAccount())
            {
                string sql = "Create Table Account(ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY, username varchar(30), password varchar(30), isLogin bit)";
                ProcessData.ExecuteData(sql);
            }
        }

        private bool isExistTableAccount()
        {
            string sql = "SELECT * FROM INFORMATION_SCHEMA.TABLES Where Table_Schema = 'dbo'  AND Table_Name = 'Account'";
            DataTable dataTable = ProcessData.LoadData(sql);
            return dataTable.Rows.Count != 0;
        }

        public override bool ValidateUser(string username, string password)
        {
            if (Authentication(username, password))
            {
                string sql = string.Format("Update Account Set isLogin = 'true' where username ='{0}'", username);
                if (ProcessData.ExecuteData(sql) != 0)
                    return true;
            }
            return false;
        }

        private bool Authentication(string username, string password)
        {
            var sql = string.Format("Select * from Account Where username = '{0}'", username);
            DataTable data = ProcessData.LoadData(sql);
            if (data.Rows.Count != 0)
            {
               
                string u = data.Rows[0][1].ToString();
                string p = HashAlgorithmType.Decrypt(data.Rows[0][2].ToString());
                return username == u && password == p;
            }
            return false;
        }

        public override bool CreateUser(string username, string password)
        {
            
            if (isExistUser(username)) return false;
            string sql = string.Format("Insert Into Account Values('{0}','{1}','false')", username, HashAlgorithmType.Encrypt(password));
            if (ProcessData.ExecuteData(sql) != 0)
                return true;
            return false;
        }

        private bool isExistUser(string username)
        {
            string sql = string.Format("Select * From Account Where username = '{0}'", username);
            var data = ProcessData.LoadData(sql);
            return data.Rows.Count != 0;
        }

        public override bool SignOutUser(string username)
        {
            string sql = string.Format("Update Account Set isLogin = 'false' where username ='{0}'", username);
            if (ProcessData.ExecuteData(sql) != 0)
                return true;
            return false;
        }
    }
}
