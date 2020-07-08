namespace SQLEngine
{
    public interface IExecuteQueryBuilder
    {
        IExecuteProcedureNeedArgQueryBuilder Procedure(string procedureName, bool withScope = true);
    }
}