namespace SQLEngine
{
    public interface IInsertQueryBuilder : IAbstractInsertQueryBuilder
    {
        IInsertNoIntoQueryBuilder Into(string tableName);
        IInsertNoIntoQueryBuilder Into<TTable>() where TTable : ITable, new();
    }
}