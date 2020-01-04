using System;
using System.Linq;
using System.Windows.Forms;
using Demo.Forms;
using SEPFramework;
using SEPFramework.Membership;
namespace Demo
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
            SEPContainer.RegisterConnection(connection);
            SEPContainer.RegisterForm<IAddForm, MyAddForm>();
            SEPContainer.RegisterForm<IReadForm, MyReadForm>();
            SEPContainer.RegisterForm<IUpdateForm, MyUpdateForm>();
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

            Application.Run(login);
        }
    }
}
