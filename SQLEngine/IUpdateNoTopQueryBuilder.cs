namespace SQLEngine
{
    public interface IUpdateNoTopQueryBuilder : IAbstractUpdateQueryBuilder
    {
        IUpdateNoTableQueryBuilder Table(string tableName);
    }
}