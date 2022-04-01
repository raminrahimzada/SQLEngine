namespace SQLEngine.SqlServer;

internal sealed class ElseIfQueryBuilder : AbstractQueryBuilder, IElseIfQueryBuilder
{
    private readonly AbstractSqlCondition _condition;

    public ElseIfQueryBuilder(AbstractSqlCondition condition)
    {
        _condition = condition;
    }

    public override void Build(ISqlWriter writer)
    {
        writer.Write(C.ELSE);
        writer.Write(C.SPACE);
        writer.Write(C.IF);
        writer.Write(C.BEGIN_SCOPE);
        writer.Write(_condition.ToSqlString());
        writer.WriteLine(C.END_SCOPE);
    }
}