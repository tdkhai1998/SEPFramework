﻿using System;
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
            CommonConnection connection = ConnectionFactory.createConnection("mysql","remotemysql.com", "6cjHslMrqJ", "6cjHslMrqJ", "gkYsnqNLTV", 3306);

            ////CommonConnection connection =  ConnectionFactory.createConnection("sqlserver",@"DESKTOP-FRPO8I4\SQLEXPRESS", "testDB", "dffd", "sfd", 1433);
            //CommonConnection connection = ConnectionFactory.createConnection("sqlserver", @"DESKTOP-FRPO8I4\SQLEXPRESS", "testDB", "", "", 1433);

            SEPContainer.RegisterInstance<CommonConnection>(connection);
            Type i = typeof(IAddForm);
            Application.Run(new MainForm(SEPContainer.Create<Database>()));
        }
    }
}
