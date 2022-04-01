namespace SQLEngine.SqlServer;

internal class DropTableQueryBuilder : AbstractQueryBuilder,
    IDropTableQueryBuilder, IDropTableNoNameQueryBuilder,
    IDropTableNoNameNoSchemaQueryBuilder
{
    private string _tableName;
    private string _schemaName;

    public IDropTableNoNameQueryBuilder Table(string tableName)
    {
        _tableName = tableName;
        return this;
    }

    public IDropTableNoNameQueryBuilder Table<TTable>() where TTable : ITable, new()
    {
        using(var table = new TTable())
        {
            return Table(table.Name);
        }
    }

    public IDropTableNoNameNoSchemaQueryBuilder FromSchema(string schemaName)
    {
        _schemaName = schemaName;
        return this;
    }

    protected override void ValidateAndThrow()
    {
        if(string.IsNullOrEmpty(_tableName))
        {
            Bomb();
        }
        base.ValidateAndThrow();
    }

    public override void Build(ISqlWriter writer)
    {
        writer.Write(C.DROP);
        writer.Write2(C.TABLE);

        if(!string.IsNullOrEmpty(_schemaName))
        {
            writer.Write(I(_schemaName));
            writer.Write(C.DOT);
        }
        writer.Write(I(_tableName));
        writer.Write(C.SEMICOLON);
    }
}