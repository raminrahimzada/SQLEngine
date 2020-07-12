namespace SQLEngine.SqlServer
{
    internal class AlterTableAddColumnQueryBuilder:AbstractQueryBuilder
        , IAlterTableNoNameAddColumnNoNameQueryBuilder
        , IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder
        , IAlterTableNoNameAddColumnNoNameNoNullableQueryBuilder
        , IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeNoDefaultValueQueryBuilder
        , IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeQueryBuilder
    {

        private string _tableName;
        private string _columnName;

        private string _type;
        private bool? _canBeNull;
        private string _defaultValue;
        private int? _size;
        private int? _scale;

        public IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder OfType(string type)
        {
            _type = type;
            return this;
        }

        public IAlterTableNoNameAddColumnNoNameNoNullableQueryBuilder NotNull(bool notNull=true)
        {
            _canBeNull = !notNull;
            return this;
        }

        public IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeQueryBuilder Size(int size, int? scale = null)
        {
            _size = size;
            _scale = scale;
            return this;
        }

        public IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeNoDefaultValueQueryBuilder DefaultValue(ISqlExpression expression)
        {
            _defaultValue = expression.ToSqlString();
            return this;
        }

        public override string Build()
        {
            Writer.Write(SQLKeywords.ALTER);
            Writer.Write2(SQLKeywords.TABLE);
            Writer.Write(_tableName);
            Writer.Write2(SQLKeywords.ADD);
            Writer.Write(SQLKeywords.COLUMN);
            Writer.Write2(_columnName);
            Writer.Write2(_type);
            if (_size != null)
            {
                Writer.Write(SQLKeywords.BEGIN_SCOPE);
                Writer.Write(_size);
                if (_scale != null)
                {
                    Writer.Write(SQLKeywords.COMMA);
                    Writer.Write(_scale);
                }
                Writer.Write(SQLKeywords.END_SCOPE);
            }

            if (_canBeNull != null)
            {
                if (!_canBeNull.Value)
                {
                    Writer.Write2(SQLKeywords.NOT);
                }
                Writer.Write2(SQLKeywords.NULL);
            }

            if (_defaultValue != null)
            {
                Writer.Write2(SQLKeywords.DEFAULT);
                Writer.Write(_defaultValue);
            }

            return base.Build();
        }

        public AlterTableAddColumnQueryBuilder Table(string tableName)
        {
            _tableName = tableName;
            return this;
        }

        public AlterTableAddColumnQueryBuilder Column(string columnName)
        {
            _columnName = columnName;
            return this;
        }
    }
}