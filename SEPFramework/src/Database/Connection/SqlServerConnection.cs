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
            if (connection.State.ToString() != "Open")
            {
                MessageBox.Show("Không thể kết nối đến database!");
                Environment.Exit(0);
            }
            SqlCommand cmd = QueryFactory.GetFactory(QueryType.insert).CreateSqlServer(tableName, row, null).GetQuery();
            Console.WriteLine(cmd.CommandText);
            cmd.Connection = connection;
            try
            {
                int check = cmd.ExecuteNonQuery();
                return check == 1;
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
                connBuilder["User ID"] = username;
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
            if (connection.State.ToString() != "Open")
            {
                MessageBox.Show("Không thể kết nối đến database!");
                Environment.Exit(0);
            }
            SqlCommand cmd = QueryFactory.GetFactory(QueryType.delete).CreateSqlServer(tableName, row, null).GetQuery();
            Console.WriteLine(cmd.CommandText);
            cmd.Connection = connection;
            try
            {
                int check = cmd.ExecuteNonQuery();
                return check == 1;
            }
            catch
            {
                MessageBox.Show("Lỗi");
                return false;
            }
            throw new NotImplementedException();
        }

        public override List<string> GetListTableName()
        {
            if (connection.State.ToString() != "Open")
            {
                MessageBox.Show("Không thể kết nối đến database!");
                Environment.Exit(0);
            }
            DataTable schema = connection.GetSchema("Tables");
            List<string> TableNames = new List<string>();
            foreach (DataRow row in schema.Rows)
            {
                TableNames.Add(row[2].ToString());
            }
            return TableNames;
        }

        public override DataTable GetTable(string tableName)
        {
            if (connection.State.ToString() != "Open")
            {
                MessageBox.Show("Không thể kết nối đến database!");
                Environment.Exit(0);
            }
            var stm = "SELECT * FROM " + tableName;
            DataTable tb = new DataTable();
            var cmd = new SqlCommand(stm, connection);
            SqlDataReader rdr = cmd.ExecuteReader();
            tb.Load(rdr);

            rdr.Close();
            tb.TableName = tableName;
            return tb;
        }

        public override bool CreateMembershipTable()
        {
            if (connection.State.ToString() != "Open")
            {
                MessageBox.Show("Không thể kết nối đến database!");
                Environment.Exit(0);
            }
            var stm0 = "IF NOT EXISTS (select * from sysobjects where name='account' and xtype='U') CREATE TABLE  account ( username VARCHAR(255) NOT NULL PRIMARY KEY , password VARCHAR(255) NOT NULL)";
            var stm1 = "IF NOT EXISTS (select * from sysobjects where name='role' and xtype='U') CREATE TABLE  role ( roleid INT NOT NULL , rolename VARCHAR(255) NOT NULL)";
            var stm2 = "IF NOT EXISTS (select * from sysobjects where name='account_role' and xtype='U') CREATE TABLE  account_role ( username VARCHAR(255) NOT NULL, roleid INT NOT NULL)";
            List<String> stms = new List<String>();
            stms.Add(stm0);
            stms.Add(stm1);
            stms.Add(stm2);
            int check = 0;
            for (int i = 0; i < 3; i++)
            {
                if (connection.State.ToString() == "Open")
                {
                    var cmd = new SqlCommand(stms[i], connection);
                    if (cmd.ExecuteNonQuery() > 0)
                        check += 1;
                }
            }
            return check >= 3;
        }


        public override DataTable Login(string username, string password)
        {
            if (connection.State.ToString() != "Open")
            {
                MessageBox.Show("Không thể kết nối đến database!");
                Environment.Exit(0);
            }
            var stm = "SELECT * FROM account WHERE username = @param0 AND password = @param1";
            DataTable tb = new DataTable();
            var cmd = new SqlCommand(stm, connection);
            cmd.Parameters.AddWithValue("@param0", username);
            cmd.Parameters.AddWithValue("@param1", password);
            SqlDataReader rdr = cmd.ExecuteReader();
            tb.Load(rdr);
            rdr.Close();
            if (tb.Rows.Count != 0)
            {
                var stm2 = $"SELECT rolename from role, account_role where account_role.username = '{username}' and role.roleid = account_role.roleid";
                SqlDataAdapter rdr2 = new SqlDataAdapter(stm2, connection);
                DataTable roleTable = new DataTable();
                rdr2.Fill(roleTable);
                rdr2.Dispose();
                return roleTable;
            }
            return null;
        }

        public override bool Register(string username, string password)
        {
            if (connection.State.ToString() != "Open")
            {
                MessageBox.Show("Không thể kết nối đến database!");
                Environment.Exit(0);
            }
            var stm = "INSERT INTO account VALUES (@param0,@param1)";
            var cmd = new SqlCommand(stm, connection);
            cmd.Parameters.AddWithValue("@param0", username);
            cmd.Parameters.AddWithValue("@param1", password);

            Console.WriteLine(cmd.CommandText);
            try
            {
                int check = cmd.ExecuteNonQuery();
                return check == 1;
            }
            catch
            {
                MessageBox.Show("Lỗi");
                return false;
            }
        }

        public override bool Update(string tableName, Row row, Row newRow)
        {
            if (connection.State.ToString() != "Open")
            {
                MessageBox.Show("Không thể kết nối đến database!");
                Environment.Exit(0);
            }
            SqlCommand cmd = QueryFactory.GetFactory(QueryType.update).CreateSqlServer(tableName, row, newRow).GetQuery();
            cmd.Connection = connection;
            try
            {
                int check = cmd.ExecuteNonQuery();
                return check == 1;
            }
            catch
            {
                MessageBox.Show("Lỗi");
                return false;
            }
        }
        //private Type getType(string typeString)
        //{
        //    switch (typeString)
        //    {
        //        case "bigint":
        //            return typeof(Int64);
        //        case "binary":
        //            return typeof(Byte[]);
        //        case "bit":
        //            return typeof(Boolean);
        //        case "char":
        //        case "nchar":
        //        case "ntext":
        //        case "nvarchar":
        //        case "text":
        //        case "varchar":
        //            return typeof(String);
        //        case "date":
        //        case "datetime":
        //        case "datetime2":
        //        case "smalldatetime":
        //            return typeof(DateTime);
        //        case "datetimeoffset":
        //            return typeof(DateTimeOffset);
        //        case "decimal":
        //            return typeof(Decimal);
        //        case "FILESTREAM":
        //        case "image":
        //        case "rowversion":
        //        case "timestamp":
        //        case "varbinary":
        //            return typeof(Byte[]);
        //        case "int":
        //            return typeof(Int32);
        //        case "smallint":
        //            return typeof(Int16);
        //        case "money":
        //        case "numeric":
        //        case "smallmoney":
        //            return typeof(Decimal);
        //        case "real":
        //            return typeof(Single);
        //        case "sql_variant":
        //            return typeof(Object);
        //        case "time":
        //            return typeof(TimeSpan);
        //        case "tinyint":
        //            return typeof(Byte);
        //        case "uniqueidentifier":
        //            return typeof(Guid);
        //        case "xml":
        //            return typeof(Xml);
        //        default:
        //            return typeof(Nullable);
        //    }
        //}

        //public override List<Column> getListColByTableName(string tableName)
        //{
        //    List<Column> colList = new List<Column>();
        //    var stm = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'" + tableName + "'";
        //    var cmd = new SqlCommand(stm, connection);
        //    SqlDataReader rdr = cmd.ExecuteReader();
        //    while (rdr.Read())
        //    {
        //        Console.WriteLine(rdr[0]);
        //        //type
        //        Console.WriteLine(rdr[1]);

        //        Column col = new Column(rdr[3].ToString(), getType(rdr[7].ToString()), false);

        //        colList.Add(col);
        //    }
        //    Console.WriteLine(colList.Count);
        //    rdr.Close();
        //    return colList;
        //}





    }
}