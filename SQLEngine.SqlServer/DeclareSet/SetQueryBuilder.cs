namespace SQLEngine.SqlServer;

internal sealed class SetQueryBuilder : AbstractQueryBuilder, ISetNeedSetQueryBuilder, ISetNeedToQueryBuilder,
    ISetNoSetNoToQueryBuilder
{
    private AbstractSqlVariable _variable;
    private ISqlExpression _value;

    public ISetNeedToQueryBuilder Set(AbstractSqlVariable variable)
    {
        _variable = variable;
        return this;
    }

    public ISetNoSetNoToQueryBuilder To(AbstractSqlExpression value)
    {
        _value = value;
        return this;
    }

    public override void Build(ISqlWriter writer)
    {
        writer.Write(C.SET);
        writer.Write(C.SPACE);
        writer.Write(_variable.ToSqlString());
        writer.Write(C.SPACE);
        writer.Write(C.EQUALS);
        writer.Write(C.SPACE);
        writer.Write(_value.ToSqlString());
        writer.Write(C.SEMICOLON);
        writer.WriteLine();
    }

    public ISetNoSetNoToQueryBuilder To(ISqlExpression value)
    {
        _value = value;
        return this;
    }

    public ISetNoSetNoToQueryBuilder To(AbstractSqlLiteral value)
    {
        _value = value;
        return this;
    }
}