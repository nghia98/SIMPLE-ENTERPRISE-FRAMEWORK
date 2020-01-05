
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
    public partial class Register : Form
    {
        public Memberships membership;
        public Register()
        {
            InitializeComponent();
            membership = new Memberships(CurrentDatabase.getInstance().connectionString);
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuTextbox2_OnTextChange(object sender, EventArgs e)
        {
            bunifuTextbox2._TextBox.PasswordChar = '*';
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form formLogin = new Login();
            formLogin.Show();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            var username = bunifuTextbox1.text;
            var password = bunifuTextbox2.text;
            if (this.membership.Register(username, password))
            {
                this.Hide();
                Login login = new Login();
                login.Show();
            }
            else
            {

                labelMessage.Text = "Tài khoản đã tồn tại !";
            }
        }
    }
}
