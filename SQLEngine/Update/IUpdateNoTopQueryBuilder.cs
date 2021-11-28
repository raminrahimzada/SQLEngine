namespace SQLEngine
{
    public interface IUpdateNoTopQueryBuilder : IAbstractUpdateQueryBuilder
    {
        IUpdateNoTableQueryBuilder Table(string tableName, string schema = null);
        IUpdateNoTableQueryBuilder Table<TTable>() where TTable : ITable, new();
    }
}