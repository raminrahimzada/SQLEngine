namespace SQLEngine
{
    public interface IDropQueryBuilder:IAbstractQueryBuilder
    {
        IDropTableNoNameQueryBuilder Table(string tableName);
        IDropFunctionQueryBuilder Function(string funcName);
        IDropViewQueryBuilder View(string viewName);
    }
}