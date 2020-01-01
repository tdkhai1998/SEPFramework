using System;
using System.Windows.Forms;

namespace SEPFramework
{
    public partial class BaseForm : Form
    {
        protected Table table;
        public Done done;
        public BaseForm(Table table)
        {
            InitializeComponent();
            this.table = table;
        }
        public BaseForm()
        {

            InitializeComponent();
            SetUpUi();
            try
            {
                this.table = MyContainer.Create<Table>();
            }
            catch (Exception)
            {

            }


        }
        protected virtual void SetUpUi() {  }

        private void BaseForm_Load(object sender, EventArgs e)
        {

        }
    }
}
