namespace SQLEngine
{
    public interface IInsertNeedValueQueryBuilder : IAbstractInsertQueryBuilder
    {
        IInsertNeedValueQueryBuilder Value(string columnName, AbstractSqlLiteral columnValue);
    }
}