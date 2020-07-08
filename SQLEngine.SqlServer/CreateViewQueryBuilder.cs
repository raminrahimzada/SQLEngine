namespace SQLEngine.SqlServer
{
    internal class CreateViewQueryBuilder : SqlServerQueryBuilder, ICreateViewNoNameQueryBuilder,
        ICreateViewNoNameNoBodyQueryBuilder
    {
        private string _viewName;
        private string _viewBody;
        public ICreateViewNoNameNoBodyQueryBuilder As(string selection)
        {
            _viewBody = selection;
            return this;
        }

        public ICreateViewNoNameQueryBuilder Name(string viewName)
        {
            _viewName = viewName;
            return this;
        }

        public override string Build()
        {
            Writer.Write(SQLKeywords.CREATE);
            Writer.Write(SQLKeywords.SPACE);
            Writer.Write(SQLKeywords.VIEW);
            Writer.Write(SQLKeywords.SPACE);
            Writer.Write(_viewName);
            Writer.Write(SQLKeywords.SPACE);
            Writer.Write(SQLKeywords.AS);
            Writer.Write(SQLKeywords.SPACE);
            Writer.Write(_viewBody);

            return base.Build();
        }
    }
}