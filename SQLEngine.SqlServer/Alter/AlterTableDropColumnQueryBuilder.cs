namespace SQLEngine.SqlServer;

internal class AlterTableDropColumnQueryBuilder : AbstractQueryBuilder,
    IAlterTableNoNameDropColumnQueryBuilder
{
    private string _tableName;
    private string _columnName;
    public AlterTableDropColumnQueryBuilder Table(string tableName)
    {
        _tableName = tableName;
        return this;
    }

    public AlterTableDropColumnQueryBuilder Column(string columnName)
    {
        _columnName = columnName;
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
        writer.Write(C.DROP);
        writer.Write(C.SPACE);
        writer.Write(C.COLUMN);
        writer.Write(C.SPACE);
        writer.Write(_columnName);
    }
}