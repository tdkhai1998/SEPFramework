using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SEPFramework
{
    public class SqlServer : CommonConnection
    {
        SqlConnection connection = new SqlConnection();
        public SqlServer(string host, string dbname, string username, string pass, int port) : base(host, dbname, username, pass, port)
        {
        }

        public override bool Connect()
        {
            SqlConnectionStringBuilder connBuilder = new SqlConnectionStringBuilder();
            connBuilder["Data Source"] = host;
            if (username != "" && pass != "")
            {
                connBuilder["integrated Security"] = false;
                connBuilder["Password"] = pass;
                connBuilder["UserID"] = username;
            }
            else
            {
                connBuilder["integrated Security"] = true;
            }
            connBuilder["Initial Catalog"] = dbname;
            connection = new SqlConnection(connBuilder.ConnectionString);
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public override List<string> getListTableName()
        {
            DataTable schema = connection.GetSchema("Tables");
            List<string> TableNames = new List<string>();
            foreach (DataRow row in schema.Rows)
            {
                TableNames.Add(row[2].ToString());
            }
            return TableNames;
        }

        public override DataTable getTable(string tableName)
        {
            var stm = "SELECT * FROM " + tableName;
            DataTable tb = new DataTable();
            var cmd = new SqlCommand(stm, connection);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                tb.Load(rdr);
            }

            rdr.Close();
            tb.TableName = tableName;
            return tb;
        }
    }
}