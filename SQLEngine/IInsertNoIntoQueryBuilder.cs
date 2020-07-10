namespace SQLEngine
{
    public interface IInsertNoIntoQueryBuilder : IInsertWithValuesQueryBuilder
    {
        IInsertNeedValueQueryBuilder Value(string columnName, AbstractSqlLiteral columnValue);
        IInsertNoIntoWithColumns Columns(params string[] columnNames);
    }
}