using SEPFramework.Membership;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SEPFramework
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CommonConnection connection = ConnectionFactory.createConnection("mysql", "remotemysql.com", "WEJMD9dLmJ", "WEJMD9dLmJ", "CqlKK8zDL3", 3306);
            MyContainer.RegisterInstance<CommonConnection>(connection);
            Login login = new Login
            {
                SuccessAction = roles =>
                {
                    Role role = new Role
                    {
                        isAllowedAdd = roles.Contains("C"),
                        isAllowedRead = roles.Contains("R"),
                        isAllowedUpdate = roles.Contains("U"),
                        isAllowedDelete = roles.Contains("D")
                    };
                    MyContainer.RegisterInstance<Role>(role);
                    new MainForm().Show();
                }
            };
            Application.Run(login);
        }
    }
}
