using System;
using System.Windows.Forms;

namespace SEPFramework
{
    public partial class ReadForm : SEPFramework.BaseForm, IReadForm
    {

        public ReadForm(Table table) : base(table)
        {
            InitializeComponent();
            this.dataGridView1.DataSource = table.dataTable;
        }
        public ReadForm()
        {
            InitializeComponent();
            if(table!=null)
            this.dataGridView1.DataSource = table.dataTable;
        }

        private void DataGridView1_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            UpdateForm r = new UpdateForm(table,e.RowIndex);
            r.ShowDialog();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            IAddForm r = SEPContainer.Create<IAddForm>();
            ((BaseForm)r).ShowDialog();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            if (this.dataGridView1.SelectedCells.Count <= 0) return;
            SEPContainer.RegisterInstance<int>(this.dataGridView1.SelectedCells[0].RowIndex);
            IUpdateForm r = SEPContainer.Create<IUpdateForm>();
            ((BaseForm)r).ShowDialog();
        }

   
    }
}
