using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SEPFramework
{
    public partial class MainForm : Form
    {
        private Database database;
        public MainForm()
        {
            InitializeComponent();
            this.database = MyContainer.Create<Database>();
            this.database.Connection.Connect();
            this.database.LoadData();
            List<string> list = this.database.GetTableNames();
            this.tableList.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                this.tableList.Items.Add(list[i]);
            }
            tableList.SelectedIndex = 0;

            Role role = MyContainer.Create<Role>();
            button2.Enabled = role.isAllowread;
        }

        private void readBtn_Click(object sender, EventArgs e)
        {
            Table t = this.database.GetTableByName(this.tableList.SelectedItem.ToString());
            MyContainer.RegisterInstance<Table>(t);
            BaseForm r = (BaseForm) MyContainer.Create<IReadForm>();
            this.Hide();
            r.FormClosed += (s, args) => this.Show();
            r.Show();
        }

    
    }
}
