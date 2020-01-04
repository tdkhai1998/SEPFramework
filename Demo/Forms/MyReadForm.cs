using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SEPFramework;
namespace Demo
{
    class MyReadForm: ReadForm
    {
        public MyReadForm()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MyReadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.BackColor = System.Drawing.Color.DarkGreen;
            this.ClientSize = new System.Drawing.Size(800, 575);
            this.ForeColor = System.Drawing.SystemColors.GrayText;
            this.Name = "MyReadForm";
            this.ResumeLayout(false);

        }
    }
}
