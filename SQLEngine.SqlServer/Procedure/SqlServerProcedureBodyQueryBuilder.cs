namespace SQLEngine.SqlServer;

internal class SqlServerProcedureBodyQueryBuilder : SqlServerQueryBuilder, IProcedureBodyQueryBuilder
{
    public AbstractSqlVariable Parameter(string name)
    {
        return new SqlServerVariable(name);
    }
}