namespace SQLEngine.SqlServer;

internal sealed class AlterTableAddColumnQueryBuilder : AbstractQueryBuilder
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
    private byte? _scale;

    public IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder OfType(string type)
    {
        _type = type;
        return this;
    }

    public IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder OfType<T>()
    {
        return OfType(Query.Settings.TypeConvertor.ToSqlType<T>());
    }

    public IAlterTableNoNameAddColumnNoNameNoNullableQueryBuilder NotNull(bool notNull = true)
    {
        _canBeNull = !notNull;
        return this;
    }

    public IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeQueryBuilder Size(int size, byte? scale = null)
    {
        _size = size;
        _scale = scale;
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
        writer.Write(C.SPACE);
        writer.Write(C.TABLE);
        writer.Write(C.SPACE);
        writer.Write(_tableName);
        writer.Write(C.SPACE);
        writer.Write(C.ADD);
        writer.Write(C.SPACE);
        writer.Write(_columnName);
        writer.Write(C.SPACE);
        writer.Write(_type);
        writer.Write(C.SPACE);
        if(_size != null)
        {
            writer.Write(C.BEGIN_SCOPE);
            writer.Write(_size);
            if(_scale != null)
            {
                writer.Write(C.COMMA);
                writer.Write(_scale);
            }
            writer.Write(C.END_SCOPE);
        }

        if(_canBeNull != null)
        {
            if(!_canBeNull.Value)
            {
                writer.Write(C.SPACE);
                writer.Write(C.NOT);
            }
            writer.Write(C.SPACE);
            writer.Write(C.NULL);
        }

        if(_defaultValue != null)
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