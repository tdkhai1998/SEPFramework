using System;
using System.Collections.Generic;
using System.Data;

namespace SEPFramework
{
    public class SqlServerConnection : CommonConnection
    {
        public SqlServerConnection(string host, string dbname, string username, string pass, int port) : base(host, dbname, username, pass, port)
        {
        }

        public override bool Connect()
        {
            throw new NotImplementedException();
        }

        public override List<string> getListTableName()
        {
            throw new NotImplementedException();
        }

        public override DataTable getTable(string tableName)
        {
            throw new NotImplementedException();
        }
    }
}