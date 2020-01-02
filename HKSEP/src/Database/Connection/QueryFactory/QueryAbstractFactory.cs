using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPFramework
{
    abstract class QueryAbstractFactory
    {
        public abstract MySqlCommonQuery createMySql(String tableName, Row row, Row newRow);
        public abstract SqlServerCommonQuery createSqlServer( String tableName, Row row, Row newRow);
     }
}
