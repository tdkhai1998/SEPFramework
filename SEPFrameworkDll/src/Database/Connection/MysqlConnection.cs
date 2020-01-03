using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SEPFramework
{
    public class Mysql : CommonConnection
    {
        MySqlConnection connection = new MySqlConnection();

        public Mysql(string host, string dbname, string username, string pass, int port) : base(host, dbname, username, pass, port)
        {

        }

        public override bool Add(string tableName, Row row)
        {
            MySqlCommand cmd = QueryFactory.GetFactory(QueryType.insert).CreateMySql(tableName, row, null).GetQuery();
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
        public override bool Update(string tableName, Row row, Row newRow)
        {
            MySqlCommand cmd = QueryFactory.GetFactory(QueryType.update).CreateMySql(tableName, row, newRow).GetQuery();
            Console.WriteLine(cmd.CommandText);
            cmd.Connection = connection;
            try
            {
                int check = cmd.ExecuteNonQuery();
                Console.WriteLine(check);
                return check == 1;
            }
            catch
            {
                MessageBox.Show("Lỗi");
                return false;
            }
        }


        public override bool Delete(string tableName, Row row)
        {
            MySqlCommand cmd = QueryFactory.GetFactory(QueryType.delete).CreateMySql(tableName, row, null).GetQuery();
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
            {
                string connectionString = String.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};", host, port, dbname, username, pass);
                connection = new MySqlConnection(connectionString);
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
        }




        public override List<string> GetListTableName()
        {
            List<string> tableList = new List<string>();
            var stm = "show tables";
            Console.WriteLine(connection.State);
            if (connection.State.ToString() == "Open")
            {
                var cmd = new MySqlCommand(stm, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    tableList.Add(rdr[0].ToString());
                }
                rdr.Close();
            }
            else
            {
                MessageBox.Show("Connection has been closed !");
            }



            return tableList;
        }

        public override DataTable GetTable(string tableName)
        {
            var stm = "SELECT * FROM " + tableName;
            DataTable tb = new DataTable();
            var cmd = new MySqlCommand(stm, connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            tb.Load(rdr);
            rdr.Close();
            return tb;
        }

        public override bool CreateMembershipTable()
        {
            List<String> stms = new List<String>
            {
                "CREATE TABLE IF NOT EXISTS account ( username VARCHAR(255) NOT NULL PRIMARY KEY , password VARCHAR(255) NOT NULL)",
                "CREATE TABLE IF NOT EXISTS  role ( roleid INT NOT NULL , rolename VARCHAR(255) NOT NULL)",
                "CREATE TABLE IF NOT EXISTS  account_role ( username VARCHAR(255) NOT NULL, roleid INT NOT NULL)"
            };
            int check = 0;
            for (int i = 0; i < 3; i++)
            {
                if (connection.State.ToString() == "Open")
                {
                    var cmd = new MySqlCommand(stms[i], connection);
                    if (cmd.ExecuteNonQuery() > 0)
                        check += 1;
                }
            }
            return check >= 3;
        }

        public override DataTable Login(string username, string password)
        {
            var stm = "SELECT * FROM account WHERE username = @param0 AND password = @param1";
            DataTable tb = new DataTable();
            var cmd = new MySqlCommand(stm, connection);
            cmd.Parameters.AddWithValue("@param0", username);
            cmd.Parameters.AddWithValue("@param1", password);
            MySqlDataReader rdr = cmd.ExecuteReader();
            tb.Load(rdr);
            rdr.Close();
            if (tb.Rows.Count != 0)
            {
                var stm2 = $"SELECT rolename from role, account_role where account_role.username = '{username}' and role.roleid = account_role.roleid";
                MySqlDataAdapter rdr2 = new MySqlDataAdapter(stm2,connection);
                DataTable roleTable = new DataTable();
                rdr2.Fill(roleTable);
                Console.WriteLine(roleTable.Rows.Count);
                rdr2.Dispose();
                return roleTable;
            }
            return null;
        }

        public override bool Register(string username, string password)
        {
            var stm = "INSERT INTO account VALUES (@param0,@param1)";
            var cmd = new MySqlCommand(stm, connection);
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
    }
}