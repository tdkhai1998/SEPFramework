using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SEPFramework;

namespace Demo
{
    class MyAddForm: AddForm
    {
        public MyAddForm() {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MyAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(731, 517);
            this.Name = "MyAddForm";
            this.Text = "MyAddForm";
            this.ResumeLayout(false);

        }
    }
}
