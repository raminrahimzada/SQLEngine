namespace SQLEngine
{
    public interface ICreateViewNoNameQueryBuilder : IAbstractQueryBuilder
    {
        ICreateViewNoNameNoBodyQueryBuilder As(string selection);
    }
}