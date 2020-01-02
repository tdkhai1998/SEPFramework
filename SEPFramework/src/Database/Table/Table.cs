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

        List<IDataGridView> obsevers = new List<IDataGridView>();

        public void registerObserver(IDataGridView dataGridView)
        {
            this.obsevers.Add(dataGridView);
        }


        public List<Column> Columns = new List<Column>();
        public List<Row> Rows = new List<Row>();

        private readonly CreateAction CreateAction;
        private readonly UpdateAction UpdateAction;
        private readonly ReadAction ReadAction;
        private readonly DeleteAction DeleteAction;

        public bool Update(Row row, Row newRow)
        {
            bool result = UpdateAction(Name, row, newRow);
            this.Refresh();
            return result;
        }

        public bool Create(Row row)
        {
            bool result = CreateAction(Name,row);
            this.Refresh();
            return result;
        }

        public bool Refresh()
        {

            bool result = ReadAction(this);
            foreach(IDataGridView dataGridView in this.obsevers)
            {
                dataGridView.UpdateDataSource(this.dataTable);
            }

            return result;
        }

        public bool Delete(Row row)
        {
            bool result = DeleteAction(Name,row);
            this.Refresh();
            return result;
        }

        public Row CreateEmptyRow()
        {
            return new Row(this);
        }
    }
}