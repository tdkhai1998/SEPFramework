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
                this.table = SEPContainer.Create<Table>();
            }
            catch (Exception)
            {

            }


        }
        protected virtual void SetUpUi() {  }
    }
}
