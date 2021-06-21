namespace SQLEngine
{
    public interface IDropTableQueryBuilder
    {
        IDropTableNoNameQueryBuilder Table(string tableName);
        IDropTableNoNameQueryBuilder Table<TTable>() where TTable : ITable, new();
    }
}