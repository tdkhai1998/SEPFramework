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
            this.database = SEPFramework.Container.Create<Database>();
            
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
            SEPFramework.Container.RegisterInstance<Table>(t);
            IReadForm r = SEPFramework.Container.Create<IReadForm>();
            this.Hide();
            ((BaseForm)r).FormClosed += (s, args) => this.Show();
            ((BaseForm)r).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
