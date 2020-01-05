using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SEP_Framework.DAO;

namespace SEP_Framework
{
    public partial class ConnectDatabase : Form
    {
        public ConnectDatabase()
        {
            InitializeComponent();
            Databases databaseName = new Databases();
            var listDatabase = databaseName.getListDatabse();

            foreach (var item in listDatabase)
            {
                bunifuDropdown1.AddItem(item);

            }

            //IoCContainer.RegisterType<AbstractProcessData, ProcessDataSQLServer>();
            //IoCContainer.RegisterType<AbstractDAO, SQLServerDAO>();
            //var DAO = IoCContainer.Resolve<SQLServerDAO>();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                CurrentDatabase currentDatabase = CurrentDatabase.getInstance();
                currentDatabase.connectionString = string.Format(@"Data Source=.;Initial Catalog={0};Integrated Security=True", bunifuDropdown1.selectedValue);
                Form login = new Login();
                login.Show();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Vui lòng chọn Database !!!");
            }
        }
    }
}
