namespace SQLEngine.SqlServer
{
    internal class TruncateQueryBuilder : SqlServerAbstractQueryBuilder, ITruncateQueryBuilder, ITruncateNoTableQueryBuilder
    {
        private string _tableName;
        public ITruncateNoTableQueryBuilder Table(string tableName)
        {
            _tableName = tableName;
            return this;
        }

        public override string Build()
        {
            Writer.Write(SQLKeywords.TRUNCATE);
            Writer.Write2(SQLKeywords.TABLE);
            Writer.Write(I(_tableName));
            return base.Build();
        }
    }
}