using System.Windows.Forms;

namespace SEPFramework
{
    public partial class BaseForm : Form
    {
        protected Table table;
        public BaseForm(Table table)
        {
            InitializeComponent();
            this.table = table;
        }
        private BaseForm()
        {
            SetUpUi();
            InitializeComponent();


        }
        protected virtual void SetUpUi() {  }
    }
}
