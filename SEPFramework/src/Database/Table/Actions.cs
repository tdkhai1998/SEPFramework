namespace SEPFramework
{
    public delegate bool CreateAction(string nameTable, Row row);

    public delegate bool ReadAction(Table table);

    public delegate bool UpdateAction(string nameTable, Row row, Row newRow);

    public delegate bool DeleteAction(string nameTable, Row row);
    public delegate void Done(int rowFocus);
}
