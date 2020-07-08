namespace SQLEngine.SqlServer
{
    internal class ExecuteQueryBuilder : SqlServerQueryBuilder, IExecuteQueryBuilder
    {
        public IExecuteProcedureNeedArgQueryBuilder Procedure(string procedureName, bool withScope = true)
        {
            return GetDefault<ExecuteProcedureQueryBuilder>().Name(procedureName, withScope);
        }
    }
}