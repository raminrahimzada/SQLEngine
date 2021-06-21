namespace SQLEngine
{
    public interface ITruncateQueryBuilder : IAbstractQueryBuilder
    {
        ITruncateNoTableQueryBuilder Table(string tableName);
        ITruncateNoTableQueryBuilder Table<TTable>() where TTable : ITable, new();
    }
}