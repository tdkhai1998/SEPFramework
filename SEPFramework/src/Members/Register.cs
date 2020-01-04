using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEPFramework.Membership
{
    public partial class Register : MembershipBaseForm
    {
        public Register()
        {
            InitializeComponent();
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            if (!this.database.Connection.Connect())
            {
                MessageBox.Show("Không thể kết nối đến database!");
                Environment.Exit(0);
            }
            bool success = database.Register(user.Text, pass.Text);
            if (success)
            {
                MessageBox.Show("Đăng ký thành công !");
                this.Close();
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại !");
            }
        }

        private void Login_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
