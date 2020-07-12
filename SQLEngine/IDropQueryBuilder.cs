namespace SQLEngine
{
    public interface IDropQueryBuilder:IAbstractQueryBuilder
    {
        IDropTableNoNameQueryBuilder Table(string tableName);
        IDropTableNoNameQueryBuilder Table<TTable>() where TTable : ITable,new();

        IDropFunctionQueryBuilder Function(string funcName);
        IDropViewNoNameQueryBuilder View(string viewName);
        IDropDatabaseNoNameQueryBuilder Database(string databaseName);
    }
}