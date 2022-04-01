namespace SQLEngine.SqlServer;

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
    private string _defaultValueConstraintName;

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


        if(_size != null)
        {
            writer.Write(_newType.Replace(C.BEGIN_SCOPE + C.MAX + C.END_SCOPE, string.Empty));
            writer.Write(C.BEGIN_SCOPE);
            writer.Write(_size);
            if(_scale != null)
            {
                writer.Write(C.COMMA);
                writer.Write(_scale);
            }
            writer.Write(C.END_SCOPE);
        }
        else
        {
            writer.Write(_newType);
        }

        if(_canBeNull != null)
        {
            if(!_canBeNull.Value)
            {
                writer.Write2(C.NOT);
            }
            writer.Write2(C.NULL);
        }

        if(_defaultValue != null)
        {
            if(string.IsNullOrWhiteSpace(_defaultValueConstraintName))
            {
                _defaultValueConstraintName =
                    "DF_" + _tableName + "_" + _columnName;
            }

            writer.Write(C.SPACE);
            writer.WriteLine();
            writer.Write(C.ALTER);
            writer.Write(C.SPACE);
            writer.Write(C.TABLE);
            writer.Write(C.SPACE);
            writer.Write(_tableName);
            writer.Write(C.SPACE);
            writer.Write(C.ADD);
            writer.Write(C.SPACE);
            writer.Write(C.CONSTRAINT);
            writer.Write(C.SPACE);
            writer.Write(_defaultValueConstraintName);
            writer.Write(C.SPACE);
            writer.Write(C.DEFAULT);
            writer.Write(C.SPACE);

            writer.Write(_defaultValue.ToSqlString());
            writer.Write(C.SPACE);
            writer.Write(C.FOR);
            writer.Write(C.SPACE);
            writer.Write(_columnName);
            writer.Write(C.SPACE);
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
    public IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeNoDefaultValueQueryBuilder DefaultValue(AbstractSqlLiteral literal, string defaultValueConstraintName = null)
    {
        _defaultValue = literal;
        _defaultValueConstraintName = defaultValueConstraintName;
        return this;
    }
}