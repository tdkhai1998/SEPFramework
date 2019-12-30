using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SEPFramework
{
    public partial class MainForm : Form
    {
        private Database database;

        public MainForm(Database database)
        {
            InitializeComponent();
            this.database = database;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            this.database.Connection.Connect();
            List<string> list = this.database.GetTableNames();
            this.comboBox1.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                this.comboBox1.Items.Add(list[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Table t = this.database.GetTableByName(this.comboBox1.SelectedItem.ToString());
            t.dataTable = this.database.GetTableDataTable(this.comboBox1.SelectedItem.ToString());
            ReadForm r = new ReadForm(t);
            r.Show();
        }
    }
}
