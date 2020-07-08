namespace SQLEngine
{
    public interface IInsertQueryBuilder : IAbstractInsertQueryBuilder
    {
        IInsertNoIntoQueryBuilder Into(string tableName);
    }
}