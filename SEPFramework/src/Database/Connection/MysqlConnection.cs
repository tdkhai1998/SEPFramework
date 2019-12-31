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
          MySqlCommand cmd =  createQueryCommand("create", tableName, row);
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

        public override bool Delete(string tableName, Row row)
        {
            throw new NotImplementedException();
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

        public override bool Update(string tableName, Row row, Row newRow)
        {
            throw new NotImplementedException();
        }

        private MySqlCommand createQueryCommand(String type,String tableName,Row row)
        {
       
            MySqlCommand command = new MySqlCommand();
            switch (type)
            {
                case "update":
                    return null;
                case "create":
                    List<String> fields = row.Attributes.Keys.ToList();
                    List<Attribute> values = row.Attributes.Values.ToList();
          
                    String fieldsString = this.createFieldsInsertString(fields);
                    String paramsString = this.createParamsInsertString(fields.Count);

                
                    command.CommandText = "INSERT INTO " + tableName + " VALUES " + paramsString;
                    Console.WriteLine(fields.Count);
                    for (int i = 0; i < fields.Count; i++)
                    {
                        Console.WriteLine(values[i].Value);
                        command.Parameters.AddWithValue("@param" + i, values[i].Value);
                   
               
                    }

                    Console.WriteLine(command.CommandText);
                    return command;

       
                case "delete":
                    return null;
                case "read":
                    return null;
                default:
                    return null;
            }
        }

        private String createFieldsInsertString(List<String> fields)
        {
            StringBuilder paramsString = new StringBuilder();
            if (fields.Count < 1)
                return "";
            paramsString.Append("(");
            for (int i = 0; i < fields.Count; ++i)
            {
                paramsString.Append(fields[i]);
                if (i < fields.Count - 1)
                {
                    paramsString.Append(",");
                }
            }
            paramsString.Append(")");
            return paramsString.ToString();
        }

        private String createParamsInsertString(int countParams)
        {
            StringBuilder paramsString = new StringBuilder();
            if (countParams < 1)
                return "";
            paramsString.Append("(");
            for (int i = 0; i < countParams; ++i)
            {
                paramsString.Append("@param")
                    .Append(i);
                if (i < countParams - 1)
                {
                    paramsString.Append(",");
                }
            }
            paramsString.Append(")");
            return paramsString.ToString();
        }
    }
}