namespace SQLEngine
{
    public interface ICreateIndexNoTableNameNoColumnNamesNoUniqueQueryBuilder : IAbstractQueryBuilder
    {

    }
    public interface ICreateIndexNoTableNameNoColumnNamesQueryBuilder : IAbstractQueryBuilder
    {
        ICreateIndexNoTableNameNoColumnNamesNoUniqueQueryBuilder Unique(bool isUnique=true);
    }
    public interface ICreateIndexNoTableNameQueryBuilder : IAbstractQueryBuilder
    {
        ICreateIndexNoTableNameNoColumnNamesQueryBuilder Columns(params string[] columnNames);
    }
    public interface ICreateDatabaseNoNameQueryBuilder : IAbstractQueryBuilder
    {

    }
    public interface ICreateIndexNoNameQueryBuilder : IAbstractQueryBuilder
    {
        ICreateIndexNoTableNameQueryBuilder OnTable(string tableName);
        ICreateIndexNoTableNameQueryBuilder OnTable<TTable>() where TTable:ITable,new();
    }
    public interface ICreateViewNoNameQueryBuilder : IAbstractQueryBuilder
    {
        ICreateViewNoNameNoBodyQueryBuilder As(string selection);
    }
}