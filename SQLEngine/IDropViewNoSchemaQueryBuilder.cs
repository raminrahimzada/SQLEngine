namespace SQLEngine
{
    public interface IDropViewNoSchemaQueryBuilder : IAbstractQueryBuilder
    {
        IDropViewNoSchemaNoDatabase FromDB(string dbName);
    }
}