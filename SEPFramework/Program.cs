using System;
using System.Windows.Forms;
using SEPFramework;
namespace k
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
            CommonConnection connection = new Mysql("remotemysql.com", "WEJMD9dLmJ", "WEJMD9dLmJ", "CqlKK8zDL3", 3306);

            ////CommonConnection connection = new SqlServer(@"DESKTOP-FRPO8I4\SQLEXPRESS", "testDB", "dffd", "sfd", 1433);
            //CommonConnection connection = new SqlServer(@"DESKTOP-FRPO8I4\SQLEXPRESS", "testDB", "", "", 1433);
     
            Container.RegisterInstance<CommonConnection>(connection);

            Application.Run(new MainForm());
        }
    }
}
