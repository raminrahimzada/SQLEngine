namespace SQLEngine
{
    public interface IInsertNoIntoQueryBuilder : IInsertWithValuesQueryBuilder
    {
        IInsertNeedValueQueryBuilder Value(string columnName, string columnValue);
        IInsertNoIntoWithColumns Columns(params string[] columnNames);
    }
}