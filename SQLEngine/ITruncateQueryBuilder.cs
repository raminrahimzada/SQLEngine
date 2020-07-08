namespace SQLEngine
{
    public interface ITruncateQueryBuilder : IAbstractQueryBuilder
    {
        ITruncateNoTableQueryBuilder Table(string tableName);
    }
}