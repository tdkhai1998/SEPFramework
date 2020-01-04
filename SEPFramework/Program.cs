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
            //CommonConnection connection = ConnectionFactory.createConnection("mysql", "remotemysql.com", "WEJMD9dLmJ", "WEJMD9dLmJ", "CqlKK8zDL3", 3306);
            CommonConnection connection = ConnectionFactory.createConnection("sqlserver", "DESKTOP-FRPO8I4\\SQLEXPRESS", "testDB", "", "CqlKK8zDL3", 3306);
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
                    SEPContainer.RegisterRole(role);
                    new MainForm().Show();
                }
            };
            Application.Run(new MainForm());
        }
    }
}
