namespace SQLEngine
{
    public interface IAlterQueryBuilder //: IAbstractQueryBuilder
    {
        IAlterTableNoNameQueryBuilder Table(string tableName);
        IAlterTableNoNameQueryBuilder Table<TTable>() where TTable:ITable,new();
    }
}