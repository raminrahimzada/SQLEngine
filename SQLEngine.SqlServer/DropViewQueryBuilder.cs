namespace SQLEngine.SqlServer
{
    internal class DropViewQueryBuilder : SqlServerAbstractQueryBuilder, IDropViewQueryBuilder
    {
        private string _viewName;
        public IDropViewQueryBuilder View(string viewName)
        {
            _viewName = viewName;
            return this;
        }

        public override string Build()
        {
            Writer.Write(SQLKeywords.DROP);
            Writer.Write(SQLKeywords.SPACE);
            Writer.Write(SQLKeywords.VIEW);
            Writer.Write(SQLKeywords.SPACE);
            Writer.Write(_viewName);

            return base.Build();
        }
    }
}