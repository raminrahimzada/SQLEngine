namespace SQLEngine
{
    public interface IExecuteProcedureNeedArgQueryBuilder : IExecuteProcedureQueryBuilder
    {
        IExecuteProcedureNeedArgQueryBuilder Arg(string parameterName, string parameterValue);
        IExecuteProcedureNeedArgQueryBuilder ArgOut(string parameterName, string parameterValue);
    }
}