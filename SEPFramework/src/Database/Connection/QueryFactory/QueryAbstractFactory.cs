using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPFramework
{
    abstract class QueryAbstractFactory
    {
        public abstract IMySqlCommonQuery CreateMySql(String tableName, Row row, Row newRow);
        public abstract ISqlServerCommonQuery CreateSqlServer( String tableName, Row row, Row newRow);
     }
}
