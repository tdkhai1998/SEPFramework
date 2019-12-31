using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SEPFramework
{
    public delegate void InsertDone();
    public partial class AddForm : SEPFramework.BaseForm, IAddForm
    {
        List<Control> LabelList = new List<Control>();
        List<Control> TextBoxList = new List<Control>();
        public AddForm(Table table) : base(table)
        {
            InitializeComponent();
            this.SetUpUi();
        }
        public AddForm()
        {
            InitializeComponent();
            this.SetUpUi();
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

            if (table != null)
            {
                TextBoxList.Clear();
                LabelList.Clear();
                int i = 0;
                foreach (Column col in table.Columns)
                {
                    // {
                    if (col.ReadOnly)
                        continue;
                    Label label = new Label();
                    label.Text = col.Name + " [" + col.Type.Name + "]";
                    label.Size = new Size(100, 20);
                    label.Location = new Point(20, 20 + i * 40);
                    label.Parent = this;
                    this.Controls.Add(label);

                    TextBox textBox = new TextBox();
                    textBox.Size = new Size(300, 20);
                    textBox.Location = new Point(230, 20 + i * 40);
                    textBox.Parent = this;
                    textBox.Name = col.Name;

                    this.Controls.Add(textBox);
                    LabelList.Add(label);
                    TextBoxList.Add(textBox);
                    i++;
                }
                Button add = new Button();
                // 
                // add
                // 
                add.Location = new System.Drawing.Point(20, 20 + i * 40);
                add.Name = "add";
                add.Size = new System.Drawing.Size(123, 75);
                add.TabIndex = 0;
                add.Text = "Thêm";
                add.UseVisualStyleBackColor = true;
                this.Controls.Add(add);
            }

        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            Row newRow = table.CreateEmptyRow();
            for (int i = 0; i < TextBoxList.Count; i++)
            {
                newRow[TextBoxList[i].Name] = TextBoxList[i].Text;
            }

            table.Create(newRow);
            table.Refresh();
            done();
        }


    }
}
