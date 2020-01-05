using SEP_Framework.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEP_Framework.Membership
{
    public class Memberships
    {
        private AbstractDAO dao;

        public Memberships(string connectionString)
        {
            dao = new SQLServerDAO(connectionString);
            dao.CreateAccountTable();
        }

        public bool Login(string username, string password)
        {
            if (dao.ValidateUser(username, password))
            {
                return true;
            }
            return false;
        }

        public bool Register(string username, string password)
        {
            if (dao.CreateUser(username, password))
            {
                return true;
            }
            return false;
        }

        public bool Logout(string username)
        {
            if (dao.SignOutUser(username))
            {
                return true;
            }
            return false;
        }
    }
}
