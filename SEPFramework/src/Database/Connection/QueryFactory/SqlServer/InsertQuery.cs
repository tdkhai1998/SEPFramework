﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPFramework
{
    class SqlServerInsertQuery : ISqlServerCommonQuery
    {
        private string tableName;
        private Row row;

        public SqlServerInsertQuery(string tableName, Row row)
        {
            this.tableName = tableName;
            this.row = row;

        }

        public SqlCommand GetQuery()
        {
            SqlCommand command = new SqlCommand();
            List<string> fields = row.Attributes.Keys.ToList();
            List<Attribute> values = row.Attributes.Values.ToList();
            string paramsString = this.CreateParamsInsertString(fields, values);

            command.CommandText = "INSERT INTO " + tableName + " VALUES " + paramsString;

            Console.WriteLine(fields.Count);
            for (int i = 0; i < fields.Count; i++)
            {
                Console.WriteLine(values[i].Value);
                if (values[i].ReadOnly)
                    continue;
                command.Parameters.AddWithValue("@" + fields[i], values[i].Value);
            }
            Console.WriteLine(command.CommandText);
            return command;
        }
        private string CreateParamsInsertString(List<String> fields, List<Attribute> values)
        {
            StringBuilder paramsString = new StringBuilder();
            int countParams = fields.Count;
            if (countParams < 1)
                return "";
            paramsString.Append("(");
            for (int i = 0; i < countParams; ++i)
            {
                if (values[i].ReadOnly)
                    continue;
                paramsString.Append("@")
                    .Append(fields[i]);
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



//private String CreateFieldsInsertString(List<String> fields, List<Attribute> values)
//{
//    StringBuilder paramsString = new StringBuilder();
//    if (fields.Count < 1)
//        return "";
//    paramsString.Append("(");
//    for (int i = 0; i < fields.Count; ++i)
//    {
//        if (values[i].ReadOnly)
//            continue;
//        paramsString.Append(fields[i]);
//        if (i < fields.Count - 1)
//        {
//            paramsString.Append(",");
//        }
//    }
//    paramsString.Append(")");
//    return paramsString.ToString();
//}