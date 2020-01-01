using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPFramework
{
    class UpdateFactory : QueryAbstractFactory
    {
        public override MySqlCommonQuery createMySql(string tableName, Row row, Row newRow)
        {
            return new UpdateQuery(tableName, row, newRow);
        }

        public override SqlServerCommonQuery createSqlServer(string tableName, Row row, Row newRow)
        {
            return new SqlServerUpdateQuery(tableName, row, newRow);
        }
    }
}
