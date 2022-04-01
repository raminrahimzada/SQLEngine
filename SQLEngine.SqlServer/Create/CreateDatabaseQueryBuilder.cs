namespace SQLEngine.SqlServer;

internal sealed class CreateDatabaseQueryBuilder : AbstractQueryBuilder, ICreateDatabaseNoNameQueryBuilder
{
    private string _databaseName;

    public ICreateDatabaseNoNameQueryBuilder Name(string databaseName)
    {
        _databaseName = databaseName;
        return this;
    }

    public override void Build(ISqlWriter writer)
    {
        writer.Write(C.CREATE);
        writer.Write2(C.DATABASE);
        writer.Write(_databaseName);
    }
}