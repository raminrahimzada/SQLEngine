namespace SQLEngine
{
    public interface IAlterQueryBuilder : IAbstractQueryBuilder
    {
        IAlterTableNoNameQueryBuilder Table(string tableName, string schema = null);
        IAlterTableNoNameQueryBuilder Table<TTable>() where TTable:ITable,new();
    }
}