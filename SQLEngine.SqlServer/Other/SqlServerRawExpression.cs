﻿namespace SQLEngine.SqlServer;

internal sealed class SqlServerRawExpression : AbstractSqlExpression
{
    internal static void Setup()
    {
        SetCreateEmpty(() => new SqlServerRawExpression());
    }
    public string Expression { get; set; }

    public SqlServerRawExpression()
    {

    }

    public SqlServerRawExpression(string expression)
    {
        Expression = expression;
    }
    public SqlServerRawExpression(params string[] expressions)
    {
        Expression = string.Concat(expressions);
    }

    public override string ToSqlString()
    {
        return Expression;
    }

    protected override void SetFrom(AbstractSqlLiteral literal)
    {
        Expression = literal == null ? C.NULL : literal.ToSqlString();
    }
    protected override void SetFrom(AbstractSqlVariable variable)
    {
        Expression = variable.ToSqlString();
    }

    protected override AbstractSqlExpression Multiply(ISqlExpression right)
    {
        return new SqlServerRawExpression($"({Expression})*({right.ToSqlString()})");
    }
}