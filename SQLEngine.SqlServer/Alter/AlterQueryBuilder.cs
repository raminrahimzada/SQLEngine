namespace SQLEngine.SqlServer;

internal class AlterQueryBuilder :AbstractQueryBuilder, IAlterQueryBuilder
{
    private IAbstractQueryBuilder _internalBuilder;
    public IAlterTableNoNameQueryBuilder Table(string tableName,string schemaName=null)
    {
        var b = new AlterTableQueryBuilder();
        _internalBuilder = b.TableName(tableName,schemaName);
        return b;
    }

    public IAlterTableNoNameQueryBuilder Table<TTable>() where TTable : ITable, new()
    {
        using (var table=new TTable())
        {
            return Table(table.Name,table.Schema);
        }
    }

    public override void Build(ISqlWriter writer)
    {
        _internalBuilder?.Build(writer);
    }
}