using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace SEPFramework
{
    class DeleteQuery : IMySqlCommonQuery
    {
        private readonly string tableName;
        private readonly Row row;

        public DeleteQuery(string tableName, Row row)
        {
            this.tableName = tableName;
            this.row = row;
        }

        public MySqlCommand GetQuery()
        {
            MySqlCommand command = new MySqlCommand();
            List<String> fields = row.Attributes.Keys.ToList();
            List<Attribute> values = row.Attributes.Values.ToList(); 
            command.CommandText = "delete from " + tableName + " where " + this.CreateParamsDeleteString(fields);
            for (int i = 0; i < fields.Count; i++)
            {
                command.Parameters.AddWithValue("@param" + i, values[i].Value);
            }
            return command;
        }

        private string CreateParamsDeleteString(List<string> fields)
        {
            StringBuilder paramsString = new StringBuilder();
            if (fields.Count < 1)
                return "";
            for (int i = 0; i < fields.Count; ++i)
            {
                paramsString.Append(fields[i])
                    .Append(" = ")
                    .Append("@param")
                    .Append(i);

                if (i < fields.Count - 1)
                {
                    paramsString.Append(" AND ");
                }
            }
            return paramsString.ToString();
        }
    }
}

