namespace SQLEngine.SqlServer;

internal sealed class SqlServerColumnWithTableAlias : SqlServerColumn
{
    private readonly string _tableAlias;

    public SqlServerColumnWithTableAlias(string name, string tableAlias) : base(name)
    {
        _tableAlias = tableAlias;
    }

    public override string ToSqlString()
    {
        return _tableAlias + C.DOT + base.ToSqlString();
    }
}