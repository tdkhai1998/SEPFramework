using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SEPFramework;
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

            CommonConnection connection = new Mysql("remotemysql.com", "WEJMD9dLmJ", "WEJMD9dLmJ", "CqlKK8zDL3", 3306);
            SEPContainer.RegisterConnection(connection);
 
            Application.Run(new MainForm());
        }
    }
}
