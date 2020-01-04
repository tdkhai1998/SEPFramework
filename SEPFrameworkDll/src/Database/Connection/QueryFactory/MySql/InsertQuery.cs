using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPFramework
{
    class InsertQuery : IMySqlCommonQuery
    {
        private readonly string tableName;
        private readonly Row row;

        public InsertQuery(string tableName, Row row)
        {
            this.tableName = tableName;
            this.row = row;
        }

        public MySqlCommand GetQuery()
        {
            MySqlCommand command = new MySqlCommand();
            List<string> fields = row.Attributes.Keys.ToList();
            List<Attribute> values = row.Attributes.Values.ToList();
            string paramsString = this.CreateParamsInsertString(fields.Count, values);

            command.CommandText = "INSERT INTO " + tableName + " VALUES " + paramsString;
            Console.WriteLine(fields.Count);
            for (int i = 0; i < fields.Count; i++)
            {
                if (values[i].ReadOnly)
                    continue;
                command.Parameters.AddWithValue("@param" + i, values[i].Value);
            }
            return command;
        }

        private string CreateParamsInsertString(int countParams, List<Attribute> values)
        {
            StringBuilder paramsString = new StringBuilder();
            if (countParams < 1)
                return "";
            paramsString.Append("(");
            for (int i = 0; i < countParams; ++i)
            {
                if (values[i].ReadOnly)
                    continue;
                paramsString.Append("@param").Append(i);
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
//private string CreateFieldsInsertString(List<string> fields)
//{
//    StringBuilder paramsString = new StringBuilder();
//    if (fields.Count < 1)
//        return "";
//    paramsString.Append("(");
//    for (int i = 0; i < fields.Count; ++i)
//    {
//        paramsString.Append(fields[i]);
//        if (i < fields.Count - 1)
//        {
//            paramsString.Append(",");
//        }
//    }
//    paramsString.Append(")");
//    return paramsString.ToString();
//}