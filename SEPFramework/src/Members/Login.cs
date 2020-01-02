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

        public List<string> getRoles()
        {
            return roles;
        }
        public Login()
        {
            InitializeComponent();
            this.database.Connection.Connect();
            database.Connection.CreateMembershipTable();
        }

        private void register_Click(object sender, EventArgs e)
        {

            MembershipBaseForm r = (MembershipBaseForm)MyContainer.Create<Register>();
            this.Hide();
            r.FormClosed += (s, args) => this.Show();
            r.Show();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
             roles = database.Login(user.Text, pass.Text);
            if (roles != null)
            {
                MessageBox.Show("Login thành công");
            }
            else
            {
                MessageBox.Show("Login thất bại");
            }
       
        }
    }
}
