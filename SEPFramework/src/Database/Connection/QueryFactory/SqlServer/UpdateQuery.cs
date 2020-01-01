using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SEPFramework
{
    class SqlServerUpdateQuery : SqlServerCommonQuery
    {
        private string tableName;
        private Row row;
        private Row newRow;

        public SqlServerUpdateQuery(string tableName, Row row, Row newRow)
        {
            this.tableName = tableName;
            this.row = row;
            this.newRow = newRow;
        }

        public SqlCommand getQuery()
        {

            SqlCommand command = new SqlCommand();

            List<String> fields = row.Attributes.Keys.ToList();
            List<Attribute> newValues = newRow.Attributes.Values.ToList();
            List<Attribute> oldValues = row.Attributes.Values.ToList();

            String paramsString = this.createParamsSetUpdateString(fields, oldValues);



            command.CommandText = "update " + tableName + " set " + paramsString;

            for (int i = 0; i < newValues.Count; i++)
            {
                command.Parameters.AddWithValue("@param" + i, newValues[i].Value);
            }
            for (int i = newValues.Count; i < newValues.Count * 2; i++)
            {
                command.Parameters.AddWithValue("@param" + i, oldValues[i - oldValues.Count].Value);
            }
            return command;
        }

        private String createParamsSetUpdateString(List<String> fields, List<Attribute> oldValues)
        {
            StringBuilder paramsString = new StringBuilder();
            if (fields.Count < 1)
                return "";
            for (int i = 0; i < fields.Count; ++i)
            {
                if (oldValues[i].IsReadOnly)
                    continue;
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
