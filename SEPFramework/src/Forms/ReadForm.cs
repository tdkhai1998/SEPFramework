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
            if (table != null)
                this.dataGridView1.DataSource = table.dataTable;
        }

        private void DataGridView1_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            SEPFramework.Container.RegisterInstance<int>(e.RowIndex);
            IUpdateForm r = SEPFramework.Container.Create<IUpdateForm>();
            ((BaseForm)r).done = this.Done;
            ((BaseForm)r).ShowDialog();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            IAddForm r = SEPFramework.Container.Create<IAddForm>();
            ((BaseForm)r).done = this.Done;
            ((BaseForm)r).ShowDialog();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            if (this.dataGridView1.SelectedCells.Count <= 0) return;
            SEPFramework.Container.RegisterInstance<int>(this.dataGridView1.SelectedCells[0].RowIndex);
            IUpdateForm r = SEPFramework.Container.Create<IUpdateForm>();
            ((BaseForm)r).done = this.Done;
            ((BaseForm)r).ShowDialog();
        }
        private void Done()
        {
            this.dataGridView1.DataSource = table.dataTable;
        }

    }
}
