using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPFramework
{
    class InsertFactory : QueryAbstractFactory
    {
  
        public override IMySqlCommonQuery CreateMySql(string tableName, Row row, Row newRow)
        {
            return new InsertQuery(tableName, row);
        }

        public override ISqlServerCommonQuery CreateSqlServer(string tableName, Row row, Row newRow)
        {
            return new SqlServerInsertQuery(tableName, row);
        }
    }

}
