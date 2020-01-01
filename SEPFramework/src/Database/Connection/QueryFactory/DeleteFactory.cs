using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPFramework
{
    class DeleteFactory : QueryAbstractFactory
    {

        public override MySqlCommonQuery createMySql(string tableName, Row row, Row newRow)
        {
            return new DeleteQuery(tableName, row);
        }

   
        public override SqlServerCommonQuery createSqlServer(string tableName, Row row, Row newRow)
        {
            return new SqlServerDeleteQuery(tableName, row);
        }
    }
}
