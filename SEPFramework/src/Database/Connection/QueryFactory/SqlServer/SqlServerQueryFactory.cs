using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPFramework
{


    class SqlServerQueryFactory
    {
        public static SqlServerCommonQuery createQuery(string type, String tableName, Row row, Row newRow)
        {
            switch (type)
            {
                case "insert":
                    return new SqlServerInsertQuery(tableName, row);

                case "delete":
                    return new SqlServerDeleteQuery(tableName, row);

                case "update":
                    return new SqlServerUpdateQuery(tableName, row, newRow);
                default:
                    return null;
            }
        }
    }
}
