using System;
using System.Windows.Forms;

namespace SEPFramework
{
    public partial class ReadForm : SEPFramework.BaseForm, IReadForm
    {
        int currentRow = 0;
        public ReadForm(Table table) : base(table)
        {
            InitializeComponent();
            this.dataGridView.DataSource = table.dataTable;
        }
        public ReadForm()
        {
            InitializeComponent();
            if (table != null)
                this.dataGridView.DataSource = table.dataTable;
        }


        private void addBtn_Click(object sender, System.EventArgs e)
        {
            IAddForm r = SEPContainer.Create<IAddForm>();
            ((BaseForm)r).done = this.Done;
            ((BaseForm)r).ShowDialog();
        }

        private void updateBtn_Click(object sender, System.EventArgs e)
        {
            if (this.dataGridView.SelectedCells.Count <= 0) return;
            SEPContainer.RegisterInstance<int>(this.dataGridView.SelectedCells[0].RowIndex);
            IUpdateForm r = SEPContainer.Create<IUpdateForm>();
            ((BaseForm)r).done = this.Done;
            ((BaseForm)r).ShowDialog();
        }
        private void Done()
        {
            this.dataGridView.DataSource = table.dataTable;
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            table.Delete(table.Rows[currentRow]);
            table.Refresh();
            Done();
        }

   

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SEPContainer.RegisterInstance<int>(e.RowIndex);
            IUpdateForm r = SEPContainer.Create<IUpdateForm>();
            ((BaseForm)r).done = this.Done;
            ((BaseForm)r).ShowDialog();
        }

        private void dataGridView_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            currentRow = e.RowIndex;
        }
    }
}
