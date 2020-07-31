namespace SQLEngine
{
    public interface IUpdateQueryBuilder : IAbstractUpdateQueryBuilder
    {
        IUpdateNoTopQueryBuilder Top(int? count);
        IUpdateNoTableQueryBuilder Table(string tableName);
        IUpdateNoTableQueryBuilder Table<TTable>() where TTable : ITable, new();
    }
}