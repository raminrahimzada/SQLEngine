namespace SQLEngine.PostgreSql
{
    internal class DropViewQueryBuilder : AbstractQueryBuilder
        ,IDropViewNoNameQueryBuilder
        , IDropViewNoSchemaQueryBuilder
        , IDropViewQueryBuilder
    {
        private string _viewName;
        private string _schemaName;
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
            if (!string.IsNullOrWhiteSpace(_schemaName))
            {
                writer.Write(_schemaName);
                writer.Write(C.DOT);
            }
            writer.Write(_viewName);
        }


        public IDropViewNoSchemaQueryBuilder FromSchema(string schema)
        {
            _schemaName = schema;
            return this;
        }
    }
}