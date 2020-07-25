﻿namespace SQLEngine.SqlServer
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
        public IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeNoDefaultValueQueryBuilder DefaultValue(AbstractSqlLiteral literal)
        {
            _defaultValue = literal.ToSqlString();
            return this;
        }

        public override void Build(ISqlWriter writer)
        {
            writer.Write(C.ALTER);
            writer.Write2(C.TABLE);
            writer.Write(_tableName);
            writer.Write2(C.ADD);
            writer.Write(C.COLUMN);
            writer.Write2(_columnName);
            writer.Write2(_type);
            if (_size != null)
            {
                writer.Write(C.BEGIN_SCOPE);
                writer.Write(_size);
                if (_scale != null)
                {
                    writer.Write(C.COMMA);
                    writer.Write(_scale);
                }
                writer.Write(C.END_SCOPE);
            }

            if (_canBeNull != null)
            {
                if (!_canBeNull.Value)
                {
                    writer.Write2(C.NOT);
                }
                writer.Write2(C.NULL);
            }

            if (_defaultValue != null)
            {
                writer.Write2(C.DEFAULT);
                writer.Write(_defaultValue);
            }
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