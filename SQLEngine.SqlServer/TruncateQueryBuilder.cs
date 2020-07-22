namespace SQLEngine.SqlServer
{
    internal class TruncateQueryBuilder : AbstractQueryBuilder, ITruncateQueryBuilder, ITruncateNoTableQueryBuilder
    {
        private string _tableName;
        public ITruncateNoTableQueryBuilder Table(string tableName)
        {
            _tableName = tableName;
            return this;
        }

        public override void Build(ISqlWriter writer)
        {
            writer.Write(C.TRUNCATE);
            writer.Write2(C.TABLE);
            writer.Write(I(_tableName));
        }
    }
}