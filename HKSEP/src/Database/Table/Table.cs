using System.Collections.Generic;
using System.Data;

namespace SEPFramework
{
    public class Table
    {
        public DataTable dataTable;
        public readonly string Name;
        public Table(CreateAction create, ReadAction read, UpdateAction update, DeleteAction delete, string name)
        {
            this.CreateAction = create;
            this.ReadAction = read;
            this.UpdateAction = update;
            this.DeleteAction = delete;
            this.Name = name;
            this.Refresh();
        }

        public List<Column> Columns = new List<Column>();
        public List<Row> Rows = new List<Row>();

        private readonly CreateAction CreateAction;
        private readonly UpdateAction UpdateAction;
        private readonly ReadAction ReadAction;
        private readonly DeleteAction DeleteAction;

        public bool Update(Row row, Row newRow)
        {
            return UpdateAction(Name, row, newRow);
        }

        public bool Create(Row row)
        {
            return CreateAction(Name, row);
        }

        public bool Refresh()
        {
            return ReadAction(this);
        }

        public bool Delete(Row row)
        {
            return DeleteAction(Name, row);
        }

        public Row CreateEmptyRow()
        {
            return new Row(this);
        }
        public void Reset()
        {
            Rows.Clear();
            Columns.Clear();
        }
    }
}