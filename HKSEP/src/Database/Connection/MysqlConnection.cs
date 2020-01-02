﻿using MySql.Data.MySqlClient;
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
            MySqlCommand cmd = QueryFactory.GetFactory(QueryType.insert).createMySql(tableName, row, null).getQuery();
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
            MySqlCommand cmd = QueryFactory.GetFactory(QueryType.update).createMySql(tableName, row, newRow).getQuery();
            Console.WriteLine(cmd.CommandText);
            cmd.Connection = connection;
            try
            {
                int check = cmd.ExecuteNonQuery();
                Console.WriteLine(check);
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
            MySqlCommand cmd = QueryFactory.GetFactory(QueryType.delete).createMySql(tableName, row, null).getQuery();
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
            Console.WriteLine(connection.State);
            if(connection.State.ToString() == "Open")
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


        private Type getType(string typeString)
        {
            switch (typeString)
            {
                case "nvarchar":
                case "nchar":
                case "ntext":
                case "text":
                case "varchar":
                    return typeof(String);
                case "int(11)":
                    return typeof(Int16);
                case "Int32":
                    return typeof(Int32);
                case "Int16":
                    return typeof(Int16);
                case "float":
                case "double":
                    return typeof(Double);
                case "datetime":
                    return typeof(DateTime);
                case "image":
                    return typeof(Byte[]);
                case "real":
                    return typeof(Single);
                case "tinyint":
                case "binary":
                    return typeof(Byte);
                case "money":
                    return typeof(Decimal);
                default:
                    return typeof(Nullable);
            }
        }

        public override List<Column> getListColByTableName(string tableName)
        {
            List<Column> colList = new List<Column>();
            var stm = "show columns from " + tableName;
            var cmd = new MySqlCommand(stm, connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine(rdr[0]);
                //type
                Console.WriteLine(rdr[1]);
               
                Column col = new Column(rdr[0].ToString(), getType(rdr[1].ToString()),false);
                colList.Add(col);
            }
            Console.WriteLine(colList.Count);
            rdr.Close();
            return colList;
        }
    }
}