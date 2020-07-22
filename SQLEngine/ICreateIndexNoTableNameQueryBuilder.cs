namespace SQLEngine
{
    public interface ICreateIndexNoTableNameQueryBuilder : IAbstractQueryBuilder
    {
        ICreateIndexNoTableNameNoColumnNamesQueryBuilder Columns(params string[] columnNames);
    }
}