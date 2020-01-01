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
            this.setFocusRow(0);
        }
        public ReadForm()
        {
            InitializeComponent();
            if (table != null)
                this.dataGridView.DataSource = table.dataTable;
        }

        public void setFocusRow(int rowIndex)
        {
            foreach (DataGridViewRow row in this.dataGridView.SelectedRows)
            {
                this.dataGridView.Rows.RemoveAt(row.Index);
            }
            //foreach (DataGridViewCell cell in this.dataGridView.SelectedCells)
            //{
            //    this.dataGridView. = "";
            //}
            this.dataGridView.ClearSelection();
            this.dataGridView.CurrentCell = null;

            this.dataGridView.Rows[rowIndex].Selected = true;
            this.dataGridView.Rows[rowIndex].Cells[0].Selected = true;

            this.dataGridView.FirstDisplayedScrollingRowIndex = this.dataGridView.SelectedRows[0].Index;


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
        //private void Done()
        //{
        //    this.dataGridView.DataSource = table.dataTable;
        //}
        private void Done(int rowFocus)
        {
            this.dataGridView.DataSource = table.dataTable;
            this.setFocusRow(rowFocus);
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            table.Delete(table.Rows[currentRow]);
            table.Refresh();
            Done(currentRow-1);
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
