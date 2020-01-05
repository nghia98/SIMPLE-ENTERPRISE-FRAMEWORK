using SEP_Framework.BUS;
using SEP_Framework.Factory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEP_Framework
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            var listTables = CurrentDatabase.getInstance().getAllTables();
            string connectionString = CurrentDatabase.getInstance().connectionString;
            if (listTables.Count > 0)
            {
                foreach (var table in listTables)
                {

                    FormRoot readForm = FormFactory.getForm(FormType.READ, connectionString, table);
                    readForm.ShowForm();

                    FormRoot createForm = FormFactory.getForm(FormType.CREATE, connectionString, table);
                    createForm.ShowForm();

                    FormRoot updateForm = FormFactory.getForm(FormType.UPDATE, connectionString, table);
                    updateForm.ShowForm();

                    FormRoot deleteForm = FormFactory.getForm(FormType.DELETE, connectionString, table);
                    deleteForm.ShowForm();
                }
            }
            else
            {
                MessageBox.Show("Cơ sở dữ liệu rỗng !!!", "Thông báo");
            }

        }
    }
}
