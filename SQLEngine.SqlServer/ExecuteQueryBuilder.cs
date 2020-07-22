namespace SQLEngine.SqlServer
{
    internal class ExecuteQueryBuilder :  IExecuteQueryBuilder
    {
        public IExecuteProcedureNeedArgQueryBuilder Procedure(string procedureName, bool withScope = true)
        {
            var b = new ExecuteProcedureQueryBuilder();
            return b.Name(procedureName, withScope);
        }
    }
}