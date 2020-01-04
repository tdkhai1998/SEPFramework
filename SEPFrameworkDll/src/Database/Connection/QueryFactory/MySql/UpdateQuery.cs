using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SEPFramework
{
    class UpdateQuery : IMySqlCommonQuery
    {
        private readonly string tableName;
        private readonly Row row;
        private readonly Row newRow;

        public UpdateQuery(string tableName, Row row, Row newRow)
        {
            this.tableName = tableName;
            this.row = row;
            this.newRow = newRow;
        }

        public MySqlCommand GetQuery()
        {
            MySqlCommand command = new MySqlCommand();
            List<string> fields = row.Attributes.Keys.ToList();
            List<Attribute> newValues = newRow.Attributes.Values.ToList();
            List<Attribute> oldValues = row.Attributes.Values.ToList();
            string paramsString = this.CreateParamsSetUpdateString(fields);

            command.CommandText = "update " + tableName + " set " + paramsString;

            for (int i = 0; i < newValues.Count; i++)
            {
                Console.WriteLine("param" + i + " la " + newValues[i].Value);
                command.Parameters.AddWithValue("@param" + i, newValues[i].Value);
            }
            for (int i = newValues.Count; i < newValues.Count * 2; i++)
            {
                Console.WriteLine("param" + i + " la " + oldValues[i - oldValues.Count].Value);
                command.Parameters.AddWithValue("@param" + i, oldValues[i - oldValues.Count].Value);
            }
            return command;
        }

        private string CreateParamsSetUpdateString(List<String> fields)
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
                    paramsString.Append(",");
                }
            }
            paramsString.Append(" where ");
            for (int i = 0; i < fields.Count; ++i)
            {
                paramsString.Append(fields[i])
                    .Append(" = ")
                    .Append("@param")
                    .Append(i + fields.Count);

                if (i < fields.Count - 1)
                {
                    paramsString.Append(" AND ");
                }
            }
            return paramsString.ToString();
        }
    }
}
