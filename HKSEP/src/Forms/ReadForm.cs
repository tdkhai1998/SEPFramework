using System;
using System.Drawing;
using System.Windows.Forms;

namespace SEPFramework
{
    public partial class ReadForm : SEPFramework.BaseForm, IReadForm
    {
        int currentRow = -1;
        public ReadForm(Table table) : base(table)
        {
            InitializeComponent();
            this.dataGridView.DataSource = table.dataTable;
            updateBtn.Enabled = false;
            deleteBtn.Enabled = false;
            dataGridView.Size = new Size(this.Width - 70, this.Height - 200);
            addBtn.Location = new Point(40, dataGridView.Location.Y + dataGridView.Size.Height + 20);
            updateBtn.Location = new Point(addBtn.Location.X + addBtn.Size.Width + 10, dataGridView.Location.Y + dataGridView.Size.Height + 20);
            deleteBtn.Location = new Point(updateBtn.Location.X + updateBtn.Size.Width + 10, dataGridView.Location.Y + dataGridView.Size.Height + 20);
            if (table.Rows.Count > 0)
                this.SetFocusRow(0);
        }
        public ReadForm()
        {
            InitializeComponent();
            updateBtn.Enabled = false;
            deleteBtn.Enabled = false;
            dataGridView.Size = new Size(this.Width - 70, this.Height - 200);
            addBtn.Location = new Point(40, dataGridView.Location.Y + dataGridView.Size.Height + 20);
            updateBtn.Location = new Point(addBtn.Location.X + addBtn.Size.Width + 10, dataGridView.Location.Y + dataGridView.Size.Height + 20);
            deleteBtn.Location = new Point(updateBtn.Location.X + updateBtn.Size.Width + 10, dataGridView.Location.Y + dataGridView.Size.Height + 20);
            if (table != null)
                this.dataGridView.DataSource = table.dataTable;
        }

        public void SetFocusRow(int rowIndex)
        {
            if (rowIndex >= table.Rows.Count||rowIndex<0) return;
            foreach (DataGridViewRow row in this.dataGridView.SelectedRows)
            {
                this.dataGridView.Rows.RemoveAt(row.Index);
            }
            this.dataGridView.ClearSelection();
            this.dataGridView.CurrentCell = null;
            this.dataGridView.Rows[rowIndex].Selected = true;
            this.dataGridView.Rows[rowIndex].Cells[0].Selected = true;
            this.dataGridView.FirstDisplayedScrollingRowIndex = this.dataGridView.SelectedRows[0].Index;
        }

        private void AddBtn_Click(object sender, System.EventArgs e)
        {
            BaseForm r = (BaseForm) MyContainer.Create<IAddForm>();
            r.done = Done;
            r.ShowDialog();
        }

        private void UpdateBtn_Click(object sender, System.EventArgs e)
        {
            if (currentRow < 0) return;
            MyContainer.RegisterInstance<int>(currentRow);
            BaseForm r = (BaseForm)MyContainer.Create<IUpdateForm>();
            r.done = this.Done;
            r.ShowDialog();
        }

        private void Done(int rowFocus)
        {
            this.dataGridView.DataSource = table.dataTable;
            this.SetFocusRow(rowFocus);
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (currentRow<0)
            {
                MessageBox.Show("Không có row được chọn.");
                return;
            }
            table.Delete(table.Rows[currentRow]);
            table.Refresh();
            Done(currentRow-1);
        }
        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= table.Rows.Count) return;
            MyContainer.RegisterInstance<int>(e.RowIndex);
            BaseForm r = (BaseForm)MyContainer.Create<IUpdateForm>();
            r.done = this.Done;
            r.ShowDialog();
        }

        private void DataGridView_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < table.Rows.Count)
            {
                updateBtn.Enabled = true;
                deleteBtn.Enabled = true;
                currentRow = e.RowIndex;
            }
            else
            {
                updateBtn.Enabled = false;
                deleteBtn.Enabled = false;
                currentRow = -1;
            }
        }

        private void ReadForm_SizeChanged(object sender, EventArgs e)
        {
            updateBtn.Enabled = false;
            deleteBtn.Enabled = false;
            dataGridView.Size = new Size(this.Width - 70, this.Height - 200);
            addBtn.Location = new Point(40, dataGridView.Location.Y + dataGridView.Size.Height + 20);
            updateBtn.Location = new Point(addBtn.Location.X + addBtn.Size.Width + 10, dataGridView.Location.Y + dataGridView.Size.Height+ 20);
            deleteBtn.Location = new Point(updateBtn.Location.X +updateBtn.Size.Width + 10, dataGridView.Location.Y + dataGridView.Size.Height + 20);
        }
    }
}
