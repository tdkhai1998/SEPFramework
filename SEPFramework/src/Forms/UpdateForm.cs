using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SEPFramework
{
    public partial class UpdateForm : SEPFramework.BaseForm, IUpdateForm
    {
        List<Control> LabelList = new List<Control>();
        List<Control> TextBoxList = new List<Control>();
        int CurrentRow;
        public UpdateForm(Table table, int CurrentRow) : base(table)
        {
            InitializeComponent();
            this.CurrentRow = CurrentRow;
            SetUpUi();
        }
        protected override void SetUpUi()
        {
            base.SetUpUi();
            foreach (Control control in LabelList)
            {
                this.Controls.Remove(control);
            }
            foreach (Control control in TextBoxList)
            {
                this.Controls.Remove(control);
            }

            TextBoxList.Clear();
            LabelList.Clear();
            int i = 0;
            Size textBoxSize = new Size(this.Width - 320, 20);
            foreach (Column col in table.Columns)
            {
                Label label = new Label();
                label.Text = col.Name;
                label.Size = new Size(100, 20);
                label.Location = new Point(20, 20 + i * 40);
                label.Parent = this;
                this.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Size = textBoxSize;
                textBox.Location = new Point(230, 20 + i * 40);
                textBox.Parent = this;
                textBox.Text = table.Rows[CurrentRow][col.Name].ToString();
                textBox.Name = col.Name;

                if (col.ReadOnly)
                {
                    textBox.ReadOnly = true;
                }
                if (validate.IsNumericType(col.Type.Name))
                {
                    textBox.KeyPress += delegate (object sender, KeyPressEventArgs e)
                    {
                        // Verify that the pressed key isn't CTRL or any non-numeric digit
                        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                        {
                            e.Handled = true;
                        }

                        // If you want, you can allow decimal (float) numbers
                        if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                        {
                            e.Handled = true;
                        }
                    };
                }

                this.Controls.Add(textBox);

                updateBtn.Location = new Point(20, 80 + i * 40);
                this.MaximumSize = new Size(int.MaxValue, 220 + i * 40);
                this.MinimumSize = new Size(this.Size.Width, 220 + i * 40);
                this.Size = new Size(this.Size.Width, 200 + i * 40);
                LabelList.Add(label);
                TextBoxList.Add(textBox);
                i++;
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            Row newRow = table.Rows[CurrentRow].Clone();
            for (int i = 0; i < TextBoxList.Count; i++)
            {
                newRow[TextBoxList[i].Name] = TextBoxList[i].Text;
            }

            table.Update(table.Rows[CurrentRow], newRow);
            table.Refresh();
            done(CurrentRow);
        }

        private void UpdateForm_SizeChanged(object sender, EventArgs e)
        {
            Size newSize = new Size(this.Width - 320, 20);
            foreach (Control textBox in TextBoxList)
            {
                textBox.Size = newSize;
            }
        }

    }
}
