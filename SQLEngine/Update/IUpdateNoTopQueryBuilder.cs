namespace SQLEngine
{
    public interface IUpdateNoTopQueryBuilder : IAbstractUpdateQueryBuilder
    {
        IUpdateNoTableQueryBuilder Table(string tableName);
        IUpdateNoTableQueryBuilder Table<TTable>() where TTable : ITable, new();
    }
}