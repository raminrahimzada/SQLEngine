namespace SQLEngine
{
    public interface IExecuteQueryBuilder
    {
        IExecuteProcedureNeedArgQueryBuilder Procedure(string procedureName);
        IExecuteFunctionNeedNameQueryBuilder Function(string functionName);
    }
}