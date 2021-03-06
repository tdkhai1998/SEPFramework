﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPFramework
{
    class MySqlQueryFactory
    {
        public static IMySqlCommonQuery CreateQuery(string type, String tableName, Row row, Row newRow)
        {
            switch (type)
            {
                case "insert":
                    return new InsertQuery(tableName, row);

                case "delete":
                    return new DeleteQuery(tableName, row);

                case "update":
                    return new UpdateQuery(tableName, row, newRow);
                default:
                    return null;
            }
        }
    }
}
