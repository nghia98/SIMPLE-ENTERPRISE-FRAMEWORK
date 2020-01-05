using SEP_Framework.Membership;
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
    public partial class Login : Form
    {
        public Memberships membership;
        public Login()
        {
            InitializeComponent();
            membership = new Memberships(CurrentDatabase.getInstance().connectionString);
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }


        private void bunifuCustomLabel3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuTextbox2_OnTextChange(object sender, EventArgs e)
        {
            bunifuTextbox2._TextBox.PasswordChar = '*';
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            var username = bunifuTextbox1.text;
            var password = bunifuTextbox2.text;

            if (this.membership.Login(username, password))
            {
                this.Hide();
                DashBoard db = new DashBoard();
                db.Show();
            }
            else
            {             
                labelMessage.Text = "Thông tin đăng nhập không chính xác!";               
            }
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form formRegister = new Register();
            formRegister.Show();        
        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
