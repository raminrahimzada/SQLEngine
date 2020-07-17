namespace SQLEngine.SqlServer
{
    internal class CreateIndexQueryBuilder :
        AbstractQueryBuilder 
        ,ICreateIndexNoNameQueryBuilder
        , ICreateIndexNoTableNameQueryBuilder
        , ICreateIndexNoTableNameNoColumnNamesQueryBuilder
        , ICreateIndexNoTableNameNoColumnNamesNoUniqueQueryBuilder
    {
        private string _tableName;
        private bool? _isUnique;
        private string[] _columnNames;
        private string _indexName;

        public ICreateIndexNoTableNameQueryBuilder OnTable(string tableName)
        {
            this._tableName = tableName;
            return this;
        }

        public ICreateIndexNoTableNameQueryBuilder OnTable<TTable>() where TTable : ITable, new()
        {
            using (var table=new TTable())
            {
                this._tableName = table.Name;
            }
            return this;
        }

        public ICreateIndexNoTableNameNoColumnNamesQueryBuilder Columns(params string[] columnNames)
        {
            if (columnNames.Length == 0)
            {
                throw Bomb("At Least one column should be given");
            }
            this._columnNames = columnNames;
            return this;
        }

        public ICreateIndexNoTableNameNoColumnNamesNoUniqueQueryBuilder Unique(bool isUnique = true)
        {
            this._isUnique = isUnique;
            return this;
        }
        public ICreateIndexNoNameQueryBuilder Name(string indexName)
        {
            this._indexName = indexName;
            return this;
        }
        public override string Build()
        {
            Writer.Write(C.CREATE);
            if (_isUnique ?? false)
            {
                Writer.Write2(C.UNIQUE);
            }
            Writer.Write2(C.INDEX);
            Writer.Write(this._indexName);
            Writer.Write2(C.ON);
            Writer.Write(_tableName);
            Writer.Write2(C.BEGIN_SCOPE);
            bool first = true;
            foreach (var columnName in _columnNames)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    Writer.Write(C.COMMA);
                }
                Writer.Write(columnName);
            }
            Writer.Write2(C.END_SCOPE);
            return base.Build();
        }
    }
}