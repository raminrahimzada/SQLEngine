namespace SQLEngine
{
    public interface IUpdateQueryBuilder : IAbstractUpdateQueryBuilder
    {
        IUpdateNoTopQueryBuilder Top(int? count);
        IUpdateNoTableQueryBuilder Table(string tableName);
    }
}