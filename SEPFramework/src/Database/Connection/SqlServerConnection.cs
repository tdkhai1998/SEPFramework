using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SEPFramework
{
    public class SqlServer : CommonConnection
    {
        SqlConnection connection = new SqlConnection();
        public SqlServer(string host, string dbname, string username, string pass, int port) : base(host, dbname, username, pass, port)
        {
        }

        public override bool Add(string tableName, Row row)
        {
            SqlCommand cmd = QueryFactory.GetFactory(QueryType.insert).createSqlServer(tableName, row, null).getQuery();
            Console.WriteLine(cmd.CommandText);
            cmd.Connection = connection;
            try
            {
                int check = cmd.ExecuteNonQuery();
                return check != 1;
            }
            catch
            {
                MessageBox.Show("Lỗi");
                return false;
            }
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

        public override bool Delete(string tableName, Row row)
        {
            SqlCommand cmd = QueryFactory.GetFactory(QueryType.delete).createSqlServer(tableName, row, null).getQuery();
            Console.WriteLine(cmd.CommandText);
            cmd.Connection = connection;
            try
            {
                int check = cmd.ExecuteNonQuery();
                return check != 1;
            }
            catch
            {
                MessageBox.Show("Lỗi");
                return false;
            }
            throw new NotImplementedException();
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

        public override bool Update(string tableName, Row row, Row newRow)
        {
            SqlCommand cmd = QueryFactory.GetFactory(QueryType.update).createSqlServer(tableName, row, newRow).getQuery();
            Console.WriteLine(cmd.CommandText);
            cmd.Connection = connection;
            try
            {
                int check = cmd.ExecuteNonQuery();
                return check != 1;
            }
            catch
            {
                MessageBox.Show("Lỗi");
                return false;
            }
        }
    }
}