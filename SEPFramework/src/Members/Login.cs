﻿using System;
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
    
        public Login()
        {
            InitializeComponent();
            database.Connection.CreateMembershipTable();
        }

        private void register_Click(object sender, EventArgs e)
        {

        }

        private void loginBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
