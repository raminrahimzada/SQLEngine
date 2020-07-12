namespace SQLEngine.SqlServer
{
    internal class AlterTableAlterColumnQueryBuilder : AbstractQueryBuilder
        , IAlterTableNoNameAlterColumnQueryBuilder
        , IAlterTableNoNameAlterColumnNoNewTypeQueryBuilder
        , IAlterTableNoNameAlterColumnNoNewTypeNoNullableQueryBuilder
        , IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeQueryBuilder
        , IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeNoDefaultValueQueryBuilder
    {
        private string _tableName;
        private string _columnName;
        private string _newType;
        private bool? _canBeNull;
        private byte? _size;
        private byte? _scale;
        private ISqlExpression _defaultValue;

        public AlterTableAlterColumnQueryBuilder Table(string tableName)
        {
            _tableName = tableName;
            return this;
        }

        public AlterTableAlterColumnQueryBuilder Column(string columnName)
        {
            _columnName = columnName;
            return this;
        }

        public override string Build()
        {
            Writer.Write(SQLKeywords.ALTER);
            Writer.Write2(SQLKeywords.TABLE);
            Writer.Write(_tableName);
            Writer.Write2(SQLKeywords.ALTER);
            Writer.Write(SQLKeywords.COLUMN);
            Writer.Write2(_columnName);
            Writer.Write(_newType);

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
                Writer.Write(SQLKeywords.BEGIN_SCOPE);
                Writer.Write2(_defaultValue.ToSqlString());
                Writer.Write(SQLKeywords.END_SCOPE);
            }
            return base.Build();
        }

        public IAlterTableNoNameAlterColumnNoNewTypeQueryBuilder NewType(string newType)
        {
            _newType = newType;
            return this;
        }

        public IAlterTableNoNameAlterColumnNoNewTypeNoNullableQueryBuilder NotNull(bool notNull = true)
        {
            _canBeNull = !notNull;
            return this;
        }

        public IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeQueryBuilder Size(byte size, byte? scale = null)
        {
            _size = size;
            _scale = scale;
            return this;
        }

        public IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeNoDefaultValueQueryBuilder DefaultValue(ISqlExpression expression)
        {
            _defaultValue = expression;
            return this;
        }
    }
}