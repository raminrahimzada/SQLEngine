namespace SQLEngine
{
    public interface IDropTableNoNameNoSchemaQueryBuilder : IAbstractQueryBuilder
    {
        IDropTableNoNameNoSchemaNoDbQueryBuilder FromDB(string databaseName);
    }
}