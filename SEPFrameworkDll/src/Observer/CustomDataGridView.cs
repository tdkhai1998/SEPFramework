using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEPFramework
{
    public class CustomDataGridView : DataGridView, IDataGridView
    {
        public void UpdateDataSource(DataTable dataTable)
        {
            this.DataSource = dataTable;
        }
    }
}
