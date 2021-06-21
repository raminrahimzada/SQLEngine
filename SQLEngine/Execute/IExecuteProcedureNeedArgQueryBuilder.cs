namespace SQLEngine
{
    public interface IExecuteProcedureNeedArgQueryBuilder : IExecuteProcedureQueryBuilder
    {
        IExecuteProcedureNeedArgQueryBuilder Schema(string schemaName);

        IExecuteProcedureNeedArgQueryBuilder Arg(string parameterName, AbstractSqlVariable parameterValue);
        IExecuteProcedureNeedArgQueryBuilder Arg(string parameterName, AbstractSqlLiteral parameterValue);

        IExecuteProcedureNeedArgQueryBuilder ArgOut(string parameterName, AbstractSqlVariable parameterValue);
        IExecuteProcedureNeedArgQueryBuilder ArgOut(string parameterName, AbstractSqlLiteral parameterValue);
    }
}