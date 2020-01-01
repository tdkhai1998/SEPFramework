using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPFramework
{
    class ConnectionFactory
    {
        public static CommonConnection createConnection(string type, string host, string dbname, string username, string pass, int port)
        {
            switch (type)
            {
                case "mysql":
                    return new Mysql(host, dbname, username, pass, port);
                case "sqlserver":
                    return new SqlServer(host, dbname, username, pass, port);
                default:
                    return null;
            }
        }
    }
}
