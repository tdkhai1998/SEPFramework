﻿using System;
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

        public bool Create(string tableName, Row row)
        {
            Connection.Add(tableName, row);
            return true;
        }
        public bool Read(Table table)
        {
            if (tables.ContainsKey(table.Name))
            {
                DataTableToTable(Connection.getTable(table.Name), table);
                return true;
            }
            return false;
        }

        public bool Update(string tableName, Row row, Row newRow)
        {
            Connection.Update(tableName, row, newRow);
            return true;
        }
        public bool Delete(string tableName, Row row)
        {
            Connection.Delete(tableName, row);
            return true;
        }

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
            return new List<string>(this.tables.Keys);
        }

        public void LoadData()
        {

            List<string> tableNames = Connection.getListTableName();
            foreach (string tableName in tableNames)
            {

                DataTable data = Connection.getTable(tableName);
                Table table = new Table(Create, Read, Update, Delete, tableName);
                DataTableToTable(data, table);
                table.dataTable = data;
                tables.Add(table.Name, table);
            }
        }

        private bool IsColumnReadOnly(DataColumn col)
        {
            return col.AutoIncrement;
        }

        private Table DataTableToTable(DataTable data, Table table)
        {
            table.Reset();
            table.dataTable = data;
            if (data.Columns.Count == 0)
            {
                List<Column> cols = Connection.getListColByTableName(table.Name);
                foreach (Column col in cols)
                {
                    table.Columns.Add(col);
                    data.Columns.Add(col.Name, col.Type);
                }
            }
            else
            {
                foreach (DataColumn col in data.Columns)
                {
                    table.Columns.Add(new Column(col.ColumnName, col.DataType, IsColumnReadOnly(col)));
                }
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