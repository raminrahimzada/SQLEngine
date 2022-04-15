namespace SQLEngine.SqlServer;

internal sealed class TruncateQueryBuilder : AbstractQueryBuilder, ITruncateQueryBuilder, ITruncateNoTableQueryBuilder
{
    private string _tableName;
    public ITruncateNoTableQueryBuilder Table(string tableName)
    {
        _tableName = tableName;
        return this;
    }

    public ITruncateNoTableQueryBuilder Table<TTable>() where TTable : ITable, new()
    {
        using var table = new TTable();
        return Table(table.Name);
    }

    public override void Build(ISqlWriter writer)
    {
        writer.Write(C.TRUNCATE);
        writer.Write2(C.TABLE);
        writer.Write(I(_tableName));
    }
}