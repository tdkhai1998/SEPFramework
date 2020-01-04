using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPFramework
{
    class DeleteFactory : QueryAbstractFactory
    {
        public override IMySqlCommonQuery CreateMySql(string tableName, Row row, Row newRow)
        {
            return new DeleteQuery(tableName, row);
        }

        public override ISqlServerCommonQuery CreateSqlServer(string tableName, Row row, Row newRow)
        {
            return new SqlServerDeleteQuery(tableName, row);
        }
    }
}
