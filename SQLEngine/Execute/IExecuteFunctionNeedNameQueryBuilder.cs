namespace SQLEngine;

public interface IExecuteFunctionNeedNameQueryBuilder : IAbstractQueryBuilder
{
    IExecuteFunctionNeedNameQueryBuilder Arg(ISqlExpression parameterValue);
    IExecuteFunctionNeedNameQueryBuilder Arg(AbstractSqlLiteral parameterValue);
    IExecuteFunctionNeedNameQueryBuilder Arg(AbstractSqlVariable parameterValue);
    IExecuteFunctionNeedNameAndSchemaQueryBuilder Schema(string schemaName);
}