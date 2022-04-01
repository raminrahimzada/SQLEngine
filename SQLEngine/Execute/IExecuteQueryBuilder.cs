namespace SQLEngine;

public interface IExecuteQueryBuilder
{
    IExecuteFunctionNeedNameQueryBuilder Function(string functionName);
    IExecuteProcedureNeedArgQueryBuilder Procedure(string procedureName);
}