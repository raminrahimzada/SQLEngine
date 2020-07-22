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
        public override void Build(ISqlWriter writer)
        {
            writer.Write(C.DROP);
            writer.Write(C.SPACE);
            writer.Write(C.VIEW);
            writer.Write(C.SPACE);
            if (!string.IsNullOrWhiteSpace(_dbName))
            {
                writer.Write(_dbName);
                writer.Write(C.DOT);
            }
            if (!string.IsNullOrWhiteSpace(_schemaName))
            {
                writer.Write(_schemaName);
                writer.Write(C.DOT);
            }
            writer.Write(_viewName);
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