using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEPFramework.Membership
{
    public partial class MembershipBaseForm : Form
    {
        protected Database database;
        public MembershipBaseForm()
        {
            InitializeComponent();
            try
            {
                this.database = MyContainer.Create<Database>();
            }
            catch { }
        }
    }
}
