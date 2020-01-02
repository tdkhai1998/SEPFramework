using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEPFramework;
using System.Windows.Forms;
namespace Demo
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CommonConnection connection = ConnectionFactory.createConnection("mysql", "remotemysql.com", "WEJMD9dLmJ", "WEJMD9dLmJ", "CqlKK8zDL3", 3306);
            SEPContainer.RegisterConnection(connection);
          //  SEPContainer.RegisterForm<IReadForm, f4>();

            Application.Run(new MainForm());
        }
    }
}
