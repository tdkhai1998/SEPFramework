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
            this.database = database;
            this.database.Connection.Connect();
            this.database.LoadData();
            List<string> list = this.database.GetTableNames();
            this.tableList.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                this.tableList.Items.Add(list[i]);
            }
            tableList.SelectedIndex = 0;
        }

        private void readBtn_Click(object sender, EventArgs e)
        {
            Table t = this.database.GetTableByName(this.tableList.SelectedItem.ToString());
            SEPContainer.RegisterInstance<Table>(t);
            IReadForm r = SEPContainer.Create<IReadForm>();
            this.Hide();
            ((BaseForm)r).FormClosed += (s, args) => this.Show();
            ((BaseForm)r).Show();
        }

    
    }
}
