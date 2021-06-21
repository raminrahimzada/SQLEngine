namespace SQLEngine
{
    public interface IInsertNoIntoQueryBuilder : IInsertWithValuesQueryBuilder
    {
        IInsertNeedValueQueryBuilder Value(string columnName, AbstractSqlLiteral columnValue);
        IInsertNeedValueQueryBuilder Value(string columnName, AbstractSqlVariable variable);
        IInsertNoIntoWithColumns Columns(params string[] columnNames);
    }
}