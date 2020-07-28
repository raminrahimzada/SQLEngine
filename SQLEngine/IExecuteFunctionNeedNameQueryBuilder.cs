namespace SQLEngine
{
    public interface IExecuteFunctionNeedNameQueryBuilder : IAbstractQueryBuilder
    {
        IExecuteFunctionNeedNameQueryBuilder Arg(AbstractSqlExpression parameterValue);
        IExecuteFunctionNeedNameQueryBuilder Arg(AbstractSqlLiteral parameterValue);
        IExecuteFunctionNeedNameQueryBuilder Arg(AbstractSqlVariable parameterValue);
        IExecuteFunctionNeedNameAndSchemaQueryBuilder Schema(string schemaName);
    }
}