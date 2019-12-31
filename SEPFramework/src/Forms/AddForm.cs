﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SEPFramework
{
    public partial class AddForm : SEPFramework.BaseForm,IAddForm
    {
        List<Control> LabelList = new List<Control>();
        List<Control> TextBoxList = new List<Control>();
        public AddForm(Table table) : base(table)
        {
            InitializeComponent();
            this.SetUpUi();
        }
        public AddForm() {
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
                    Label label = new Label();
                    label.Text = col.Name;
                    label.Size = new Size(100, 20);
                    label.Location = new Point(20, 20 + i * 40);
                    label.Parent = this;
                    this.Controls.Add(label);

                    TextBox textBox = new TextBox
                    {
                        Size = new Size(300, 20),
                        Location = new Point(230, 20 + i * 40),
                        Parent = this
                    };
                    this.Controls.Add(textBox);

                    LabelList.Add(label);
                    TextBoxList.Add(textBox);
                    i++;
                }
            }
        
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
