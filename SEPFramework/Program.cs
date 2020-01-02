using System;
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

            ////CommonConnection connection =  ConnectionFactory.createConnection("sqlserver",@"DESKTOP-FRPO8I4\SQLEXPRESS", "testDB", "dffd", "sfd", 1433);
            //CommonConnection connection = ConnectionFactory.createConnection("sqlserver", @"DESKTOP-FRPO8I4\SQLEXPRESS", "testDB", "", "", 1433);

            MyContainer.RegisterInstance<CommonConnection>(connection);
            Role role = new Role();
            role.isAllowAdd = false;
            MyContainer.RegisterInstance<Role>(role);
     
            Application.Run(new MainForm());
        }
    }
}
