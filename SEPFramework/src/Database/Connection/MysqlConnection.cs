using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
            MySqlCommand cmd = MySqlQueryFactory.createQuery("insert", tableName, row).getQuery();
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
        public override bool Update(string tableName, Row row, Row newRow)
        {
            MySqlCommand cmd = MySqlQueryFactory.createQuery("insert", tableName, row, newRow).getQuery();
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


        public override bool Delete(string tableName, Row row)
        {
            MySqlCommand cmd = MySqlQueryFactory.createQuery("delete", tableName, row).getQuery();
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




        public override List<string> getListTableName()
        {
            List<string> tableList = new List<string>();
            var stm = "show tables";
            var cmd = new MySqlCommand(stm, connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                tableList.Add(rdr[0].ToString());
            }
            rdr.Close();
            return tableList;
        }

        public override DataTable getTable(string tableName)
        {
            var stm = "SELECT * FROM " + tableName;
            DataTable tb = new DataTable();
            var cmd = new MySqlCommand(stm, connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                tb.Load(rdr);
            }

            rdr.Close();
            return tb;
        }


    }
}