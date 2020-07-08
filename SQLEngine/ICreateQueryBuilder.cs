namespace SQLEngine
{
    public interface ICreateQueryBuilder : IAbstractQueryBuilder
    {
        ICreateTableQueryBuilder Table(string tableName);
        ICreateFunctionNoNameQueryBuilder Function(string funcName);
        ICreateProcedureNoNameQueryBuilder Procedure(string procName);
        ICreateViewNoNameQueryBuilder View(string viewName);
    }
}