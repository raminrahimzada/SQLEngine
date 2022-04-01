namespace SQLEngine.SqlServer;

internal class CreateIndexQueryBuilder :
    AbstractQueryBuilder
    , ICreateIndexNoNameQueryBuilder
    , ICreateIndexNoTableNameQueryBuilder
    , ICreateIndexNoTableNameNoColumnNamesQueryBuilder
    , ICreateIndexNoTableNameNoColumnNamesNoUniqueQueryBuilder
{
    private string _tableName;
    private bool? _isUnique;
    private string[] _columnNames;
    private string _indexName;

    public ICreateIndexNoTableNameQueryBuilder OnTable(string tableName)
    {
        _tableName = tableName;
        return this;
    }

    public ICreateIndexNoTableNameQueryBuilder OnTable<TTable>() where TTable : ITable, new()
    {
        using(var table = new TTable())
        {
            _tableName = table.Name;
        }
        return this;
    }

    public ICreateIndexNoTableNameNoColumnNamesQueryBuilder Columns(params string[] columnNames)
    {
        if(columnNames.Length == 0)
        {
            throw Bomb("At Least one column should be given");
        }
        _columnNames = columnNames;
        return this;
    }

    public ICreateIndexNoTableNameNoColumnNamesNoUniqueQueryBuilder Unique(bool isUnique = true)
    {
        _isUnique = isUnique;
        return this;
    }
    public ICreateIndexNoNameQueryBuilder Name(string indexName)
    {
        _indexName = indexName;
        return this;
    }
    public override void Build(ISqlWriter writer)
    {
        writer.Write(C.CREATE);
        if(_isUnique ?? false)
        {
            writer.Write2(C.UNIQUE);
        }
        writer.Write2(C.INDEX);
        writer.Write(_indexName);
        writer.Write2(C.ON);
        writer.Write(_tableName);
        writer.Write2(C.BEGIN_SCOPE);
        bool first = true;
        foreach(var columnName in _columnNames)
        {
            if(first)
            {
                first = false;
            }
            else
            {
                writer.Write(C.COMMA);
            }
            writer.Write(columnName);
        }
        writer.Write2(C.END_SCOPE);
    }
}