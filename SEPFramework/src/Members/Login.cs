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
    public partial class Login : MembershipBaseForm
    {

        private List<string> roles = new List<string>();

        public Action<List<string>> SuccessAction;

        public Login()
        {
            InitializeComponent();
            this.database.Connection.Connect();
            this.database.Connection.CreateMembershipTable();
        }

        private void Register_Click(object sender, EventArgs e)
        {
            if (!this.database.Connection.Connect())
            {
                MessageBox.Show("Không thể kết nối đến database!");
                Environment.Exit(0);
            }
            MembershipBaseForm r = (MembershipBaseForm)MyContainer.Create<Register>();
            this.Hide();
            r.FormClosed += (s, args) => this.Show();
            r.Show();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (!this.database.Connection.Connect())
            {
                MessageBox.Show("Không thể kết nối đến database!");
                Environment.Exit(0);
            }
            roles = database.Login(user.Text, pass.Text);
            if (roles != null)
            {
                MessageBox.Show("Login thành công");
                this.SuccessAction(roles);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login thất bại");
            }
        }
    }
}
