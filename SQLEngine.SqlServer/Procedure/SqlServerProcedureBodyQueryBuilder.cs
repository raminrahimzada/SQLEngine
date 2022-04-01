namespace SQLEngine.SqlServer;

internal sealed class SqlServerProcedureBodyQueryBuilder : SqlServerQueryBuilder, IProcedureBodyQueryBuilder
{
    public AbstractSqlVariable Parameter(string name)
    {
        return new SqlServerVariable(name);
    }
}