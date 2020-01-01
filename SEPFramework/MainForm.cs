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
            this.database.Connection.Connect();
            this.database.LoadData();
            List<string> list = this.database.GetTableNames();
            this.comboBox1.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                this.comboBox1.Items.Add(list[i]);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

 
        private void button2_Click(object sender, EventArgs e)
        {
            Table t = this.database.GetTableByName(this.comboBox1.SelectedItem.ToString());
            SEPContainer.RegisterInstance<Table>(t);
            IReadForm r = SEPContainer.Create<IReadForm>();
            this.Hide();
            ((BaseForm)r).FormClosed += (s, args) => this.Show();
            ((BaseForm)r).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
