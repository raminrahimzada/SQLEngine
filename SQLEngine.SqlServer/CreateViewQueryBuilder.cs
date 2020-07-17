namespace SQLEngine.SqlServer
{
    internal class CreateViewQueryBuilder : AbstractQueryBuilder, ICreateViewNoNameQueryBuilder,
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
            Writer.Write(C.CREATE);
            Writer.Write(C.SPACE);
            Writer.Write(C.VIEW);
            Writer.Write(C.SPACE);
            Writer.Write(_viewName);
            Writer.Write(C.SPACE);
            Writer.Write(C.AS);
            Writer.Write(C.SPACE);
            Writer.Write(_viewBody);

            return base.Build();
        }
    }
}