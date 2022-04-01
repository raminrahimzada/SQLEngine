namespace SQLEngine.SqlServer;

internal sealed class ExecuteQueryBuilder : AbstractQueryBuilder, IExecuteQueryBuilder
{
    private IAbstractQueryBuilder _internalBuilder;
    public IExecuteProcedureNeedArgQueryBuilder Procedure(string procedureName)
    {
        var b = new ExecuteProcedureQueryBuilder();
        _internalBuilder = b.Name(procedureName);
        return b;
    }

    public IExecuteFunctionNeedNameQueryBuilder Function(string functionName)
    {
        var b = new ExecuteFunctionQueryBuilder();
        _internalBuilder = b.Name(functionName);
        return b;
    }

    public override void Build(ISqlWriter writer)
    {
        _internalBuilder.Build(writer);
    }
}