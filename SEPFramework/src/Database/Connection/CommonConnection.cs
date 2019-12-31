using System.Collections.Generic;
using System.Data;

namespace SEPFramework
{
    public abstract class CommonConnection
    {
        protected CommonConnection(string host, string dbname, string username, string pass, int port)
        {
            this.host = host;
            this.dbname = dbname;
            this.username = username;
            this.pass = pass;
            this.port = port;
        }
        protected bool isOpen = false;
        protected string host;
        protected string dbname;
        protected string username;
        protected string pass;
        protected int port;

        public abstract bool Add(string tableName, Row row);
        public abstract bool Update(string tableName, Row row, Row newRow);
        public abstract bool Delete(string tableName, Row row);

        public abstract bool Connect();
        public abstract DataTable getTable(string tableName);
        public abstract List<string> getListTableName();

    }
}