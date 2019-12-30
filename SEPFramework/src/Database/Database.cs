using System.Collections.Generic;
using System.Data;

namespace SEPFramework
{

    public class Database
    {

        public Database(CommonConnection connection)
        {
            this.Connection = connection;
        }
        public CommonConnection Connection;

        private Dictionary<int, string> tables;
        public bool Create(string tableName, Row row) { return true; }
        public bool Read(Table table)
        {

            DataTable data = Connection.getTable(table.Name);
            if (dataTables.ContainsKey(table.Name))
            {
                dataTables[table.Name] = data;
            }
            else
            {
                dataTables.Add(table.Name, data);
            }
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
            return true;
        }

        public bool Update(string tableName, Row row, Row newRow) { return true; }
        public bool Delete(string tableName, Row row) { return true; }

        private Dictionary<string, DataTable> dataTables = new Dictionary<string, DataTable>();

        public Table GetTableByName(string tableName)
        {

            if (tables.ContainsValue(tableName))
            {
                return new Table(Create, Read, Update, Delete, tableName);
            }
            return null;
        }

        public List<string> GetTableNames()
        {
            this.tables = new Dictionary<int, string>();
            List<string> listName = new List<string>();
            listName = this.Connection.getListTableName();
            for (int i = 0; i < listName.Count; i++)
            {
                tables.Add(i, listName[i]);
            }
            return listName;
        }
        public DataTable GetTableDataTable(string name)
        {
            if (dataTables.ContainsKey(name))
                return dataTables[name];
            else
            {
                DataTable tb = Connection.getTable(name);
                dataTables.Add(name, tb);
                return tb;
            }
        }
    }
}