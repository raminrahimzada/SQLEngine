namespace SQLEngine
{
    public interface IInsertQueryBuilder : IAbstractInsertQueryBuilder
    {
        IInsertNoIntoQueryBuilder Into(string tableName, string schema = null);
        IInsertNoIntoQueryBuilder Into<TTable>() where TTable : ITable, new();
    }
}