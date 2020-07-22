namespace SQLEngine.SqlServer
{
    internal class DropTableQueryBuilder : AbstractQueryBuilder, 
        IDropTableQueryBuilder, IDropTableNoNameQueryBuilder,
        IDropTableNoNameNoSchemaQueryBuilder,
        IDropTableNoNameNoSchemaNoDbQueryBuilder
    {
        private string _databaseName;
        private string _tableName;
        private string _schemaName;

        public IDropTableNoNameQueryBuilder Table(string tableName)
        {
            _tableName = tableName;
            return this;
        }


        public IDropTableNoNameNoSchemaQueryBuilder FromSchema(string schemaName)
        {
            _schemaName = schemaName;
            return this;
        }
        public IDropTableNoNameNoSchemaNoDbQueryBuilder FromDB(string databaseName)
        {
            _databaseName = databaseName;
            return this;
        }
        public DropTableQueryBuilder DropTable(string tableName, string schemaName, string databaseName = null)
        {
            _schemaName = schemaName;
            _tableName = tableName;
            _databaseName = databaseName;
            return this;
        }

        protected override void ValidateAndThrow()
        {
            if (string.IsNullOrEmpty(_tableName))
            {
                Bomb();
            }
            base.ValidateAndThrow();
        }

        public override void Build(ISqlWriter writer)
        {
            writer.Write(C.DROP);
            writer.Write2(C.TABLE);
            if (!string.IsNullOrEmpty(_databaseName))
            {
                writer.Write(I(_databaseName));
                writer.Write(C.DOT);
            }
            if (!string.IsNullOrEmpty(_schemaName))
            {
                writer.Write(I(_schemaName));
                writer.Write(C.DOT);
            }
            writer.Write(I(_tableName));
            writer.Write(C.SEMICOLON);
        }
    }
}