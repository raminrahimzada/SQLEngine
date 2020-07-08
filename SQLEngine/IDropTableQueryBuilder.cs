namespace SQLEngine
{
    public interface IDropTableQueryBuilder: IAbstractQueryBuilder
    {
        IDropTableNoNameQueryBuilder Table(string tableName);
    }

    public interface IDropTableNoNameQueryBuilder : IAbstractQueryBuilder
    {
        IDropTableNoNameNoSchemaQueryBuilder FromSchema(string schemaName);
    }

    public interface IDropTableNoNameNoSchemaQueryBuilder : IAbstractQueryBuilder
    {
        IDropTableNoNameNoSchemaNoDBQueryBuilder FromDB(string databaseName);
    }

    public interface IDropTableNoNameNoSchemaNoDBQueryBuilder : IAbstractQueryBuilder
    {

    }
}