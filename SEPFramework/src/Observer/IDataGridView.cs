using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPFramework
{
    public interface IDataGridView
    {
        void UpdateDataSource(DataTable dataTable);
    }
}
