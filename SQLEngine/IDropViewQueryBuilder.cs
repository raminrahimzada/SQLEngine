namespace SQLEngine
{
    public interface IDropViewQueryBuilder : IAbstractQueryBuilder
    {
        IDropViewQueryBuilder View(string viewName);
    }
}