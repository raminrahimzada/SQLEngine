﻿namespace SQLEngine.SqlServer
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
        private AbstractSqlExpression _defaultValue;

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

        public override void Build(ISqlWriter writer)
        {
            writer.Write(C.ALTER);
            writer.Write2(C.TABLE);
            writer.Write(_tableName);
            writer.Write2(C.ALTER);
            writer.Write(C.COLUMN);
            writer.Write2(_columnName);
            writer.Write(_newType);

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
                writer.Write(C.BEGIN_SCOPE);
                writer.Write2(_defaultValue.ToSqlString());
                writer.Write(C.END_SCOPE);
            }
        }
        public IAlterTableNoNameAlterColumnNoNewTypeQueryBuilder Type(string newType)
        {
            _newType = newType;
            return this;
        }
        public IAlterTableNoNameAlterColumnNoNewTypeQueryBuilder Type<T>()
        {
            _newType = Query.Settings.TypeConvertor.ToSqlType<T>();
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

        public IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeNoDefaultValueQueryBuilder DefaultValue(AbstractSqlExpression expression)
        {
            _defaultValue = expression;
            return this;
        }
        public IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeNoDefaultValueQueryBuilder DefaultValue(AbstractSqlLiteral literal)
        {
            _defaultValue = literal;
            return this;
        }
    }
}