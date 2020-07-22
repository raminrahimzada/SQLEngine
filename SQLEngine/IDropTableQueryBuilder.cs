namespace SQLEngine
{
    public interface IDropTableQueryBuilder
    {
        IDropTableNoNameQueryBuilder Table(string tableName);
    }
}