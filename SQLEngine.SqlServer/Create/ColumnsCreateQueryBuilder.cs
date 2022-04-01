namespace SQLEngine.SqlServer;

internal class ColumnsCreateQueryBuilder : AbstractQueryBuilder, IColumnsCreateQueryBuilder
{
    public byte DefaultPrecision => Query.Settings.DefaultPrecision;
    public byte DefaultScale => Query.Settings.DefaultScale;

    public IColumnQueryBuilder Column(string columnName)
    {
        IColumnQueryBuilder builder = new ColumnQueryBuilder();
        builder.Name(columnName);
        return builder;
    }
    public IColumnQueryBuilder Column<T>(string columnName)
    {
        var type = Query.Settings.TypeConvertor.ToSqlType<T>();
        return Column(columnName).Type(type);
    }
    public IColumnQueryBuilder Datetime(string columnName)
    {
        return New<ColumnQueryBuilder>().Name(columnName).Type(C.DATETIME);
    }
    public IColumnQueryBuilder Long(string columnName)
    {
        return New<ColumnQueryBuilder>().Name(columnName).Type(C.BIGINT);
    }
    public IColumnQueryBuilder Int(string columnName)
    {
        return New<ColumnQueryBuilder>().Name(columnName).Type(C.INT);
    }
    public IColumnQueryBuilder Date(string columnName)
    {
        return New<ColumnQueryBuilder>().Name(columnName).Type(C.DATE);
    }
    public IColumnQueryBuilder Binary(string columnName)
    {
        return New<ColumnQueryBuilder>().Name(columnName).Type(C.VARBINARYMAX);
    }
    public IColumnQueryBuilder Byte(string columnName)
    {
        return New<ColumnQueryBuilder>().Name(columnName).Type(C.TINYINT);
    }
    public IColumnQueryBuilder String(string columnName, bool isUniCode = true, bool isVariable = true)
    {
        string type;
        if (isVariable)
            type = isUniCode ? C.NVARCHAR : C.VARCHAR;
        else
            type = isUniCode ? C.NCHAR : C.CHAR;
        return New<ColumnQueryBuilder>().Name(columnName).Type(type);
    }
    public IColumnQueryBuilder Decimal(string columnName)
    {
        return New<ColumnQueryBuilder>().Name(columnName).Type(C.DECIMAL).Precision(DefaultPrecision).Scale(DefaultScale);
    }
    public IColumnQueryBuilder Decimal(string columnName, byte precision , byte scale )
    {
        return New<ColumnQueryBuilder>().Name(columnName).Type(C.DECIMAL).Precision(precision).Scale(scale);
    }
    public IColumnQueryBuilder Bool(string columnName)
    {
        return New<ColumnQueryBuilder>().Name(columnName).Type(C.BIT);
    }
    public IColumnQueryBuilder Short(string columnName)
    {
        return New<ColumnQueryBuilder>().Name(columnName).Type(C.SMALLINT);
    }
    public override void Build(ISqlWriter writer)
    {
        throw Bomb();
    }
}