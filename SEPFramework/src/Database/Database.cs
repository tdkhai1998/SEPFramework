using System.Collections.Generic;
using System.Data;

namespace SEPFramework
{

    public class Database
    {
        public readonly CommonConnection Connection;
        private readonly Dictionary<string, Table> tables = new Dictionary<string, Table>();
        public Database(CommonConnection connection)
        {
            this.Connection = connection;
        }

        public bool Create(string tableName, Row row) {
            Connection.Add(tableName, row);
            return true; }
        public bool Read(Table table)
        {
            if (tables.ContainsKey(table.Name)) {
                DataTableToTable(Connection.getTable(table.Name), table);
                return true;
            }
            return false;
        }

        public bool Update(string tableName, Row row, Row newRow) { return true; }
        public bool Delete(string tableName, Row row) { return true; }

        public Table GetTableByName(string tableName)
        {

            if (tables.ContainsKey(tableName))
            {
                return tables[tableName];
            }
            return null;
        }

        public List<string> GetTableNames()
        {
            return  new List<string>(this.tables.Keys);
        }

        public void LoadData()
        {
            List<string> tableNames = Connection.getListTableName();
            foreach(string tableName in tableNames)
            {
                DataTable data = Connection.getTable(tableName);
                Table table = new Table(Create, Read, Update, Delete, data.TableName);
                DataTableToTable(data,table);
                table.dataTable = data;
                tables.Add(table.Name, table);
            }
        }

        private Table DataTableToTable(DataTable data, Table table)
        {
            table.Columns.Clear();
            table.Rows.Clear();
            table.dataTable = data;
            foreach (DataColumn col in data.Columns)
            {
                table.Columns.Add(new Column(col.ColumnName, col.DataType));
            }
            foreach (DataRow row in data.Rows)
            {
                Row r = table.CreateEmptyRow();
                foreach (Column col in table.Columns)
                {
                    r[col.Name] = row[col.Name];
                }
                table.Rows.Add(r);
            }
            return table;
        }
    }
}