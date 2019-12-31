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
            foreach (Column col in table.Columns)
            {
                Label label = new Label();
                label.Text = col.Name;
                label.Size = new Size(100, 20);
                label.Location = new Point(20, 20 + i * 40);
                label.Parent = this;
                this.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Size = new Size(300, 20);
                textBox.Location = new Point(230, 20 + i * 40);
                textBox.Parent = this;
                textBox.Text = table.Rows[CurrentRow][col.Name].ToString();
                this.Controls.Add(textBox);

                LabelList.Add(label);
                TextBoxList.Add(textBox);
                i++;
            }
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {

        }
    }
}
