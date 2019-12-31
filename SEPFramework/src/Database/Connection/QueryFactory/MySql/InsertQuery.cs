using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPFramework
{
    class InsertQuery : MySqlCommonQuery
    {
        private string tableName;
        private Row row;

        public InsertQuery(string tableName, Row row)
        {
            this.tableName = tableName;
            this.row = row;

        }

        public MySqlCommand getQuery()
        {


            MySqlCommand command = new MySqlCommand();

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
