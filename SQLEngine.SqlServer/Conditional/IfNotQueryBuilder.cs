namespace SQLEngine.SqlServer
{
    internal class IfNotQueryBuilder : AbstractQueryBuilder, IIfQueryBuilder
    {
        private readonly AbstractSqlCondition _condition;

        public IfNotQueryBuilder(AbstractSqlCondition condition)
        {
            _condition = condition;
        }

        public override void Build(ISqlWriter writer)
        {
            writer.Write(C.IF);
            writer.Write(C.BEGIN_SCOPE);
            writer.Write(C.NOT);
            writer.Write(C.SPACE);
            writer.Write(_condition.ToSqlString());
            writer.WriteLine(C.END_SCOPE);
        }
    }
}