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
            this.database.LoadData();
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
            SEPContainer.RegisterInstance<Table>(t);
            ReadForm r = SEPContainer.Create<ReadForm>();
            this.Hide();
            r.FormClosed += (s, args) => this.Show();
            r.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
