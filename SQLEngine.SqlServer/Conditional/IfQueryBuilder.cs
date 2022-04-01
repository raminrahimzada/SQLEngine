namespace SQLEngine.SqlServer;

internal class IfQueryBuilder : AbstractQueryBuilder, IIfQueryBuilder
{
    private readonly AbstractSqlCondition _condition;

    public IfQueryBuilder(AbstractSqlCondition condition)
    {
        _condition = condition;
    }

    public override void Build(ISqlWriter writer)
    {
        writer.Write(C.IF);
        writer.Write(C.BEGIN_SCOPE);
        writer.Write(_condition.ToSqlString());
        writer.WriteLine(C.END_SCOPE);
    }
}