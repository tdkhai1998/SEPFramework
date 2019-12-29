namespace SEPFramework
{
    public partial class ReadForm : SEPFramework.BaseForm
    {
        public ReadForm(Table table) : base(table)
        {
            InitializeComponent();
            this.dataGridView1.DataSource = table.dataTable;
        }
    }
}
