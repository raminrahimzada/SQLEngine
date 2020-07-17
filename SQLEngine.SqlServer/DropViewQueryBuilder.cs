namespace SQLEngine.SqlServer
{
    internal class DropViewQueryBuilder : AbstractQueryBuilder
        ,IDropViewNoNameQueryBuilder
        , IDropViewNoSchemaQueryBuilder
        , IDropViewQueryBuilder
        , IDropViewNoSchemaNoDatabase
    {
        private string _viewName;
        private string _schemaName;
        private string _dbName;
        public IDropViewNoNameQueryBuilder View(string viewName)
        {
            _viewName = viewName;
            return this;
        }
        public override string Build()
        {
            Writer.Write(C.DROP);
            Writer.Write(C.SPACE);
            Writer.Write(C.VIEW);
            Writer.Write(C.SPACE);
            if (!string.IsNullOrWhiteSpace(_dbName))
            {
                Writer.Write(_dbName);
                Writer.Write(C.DOT);
            }
            if (!string.IsNullOrWhiteSpace(_schemaName))
            {
                Writer.Write(_schemaName);
                Writer.Write(C.DOT);
            }
            Writer.Write(_viewName);

            return base.Build();
        }

        public IDropViewNoSchemaNoDatabase FromDB(string dbName)
        {
            _dbName = dbName;
            return this;
        }

        public IDropViewNoSchemaQueryBuilder FromSchema(string schema)
        {
            _schemaName = schema;
            return this;
        }
    }
}