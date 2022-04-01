namespace SQLEngine.SqlServer;

internal sealed class FunctionBodyQueryBuilder : SqlServerQueryBuilder, IFunctionBodyQueryBuilder
{
    public AbstractSqlVariable Param(string name)
    {
        return new SqlServerVariable(name);
    }
}