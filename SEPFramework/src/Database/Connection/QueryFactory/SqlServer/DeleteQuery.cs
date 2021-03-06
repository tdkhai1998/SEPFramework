﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SEPFramework { 
    class SqlServerDeleteQuery : ISqlServerCommonQuery
    {
        private readonly string tableName;
        private readonly Row row;

        public SqlServerDeleteQuery(string tableName, Row row)
        {
            this.tableName = tableName;
            this.row = row;
        }

        public SqlCommand GetQuery()
        {
            SqlCommand command = new SqlCommand();
            List<String> fields = row.Attributes.Keys.ToList();
            List<Attribute> values = row.Attributes.Values.ToList(); 
            command.CommandText = "delete from " + tableName + " where " + this.CreateParamsDeleteString(fields);
            for (int i = 0; i < fields.Count; i++)
            {
                command.Parameters.AddWithValue("@param" + i, values[i].Value);
            }   
            return command;
        }

        private string CreateParamsDeleteString(List<String> fields)
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
