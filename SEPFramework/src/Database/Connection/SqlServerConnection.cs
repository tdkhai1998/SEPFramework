using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
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
            //if (rdr.HasRows)
            //{
            //    tb.Load(rdr);
            //}
            tb.Load(rdr);

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