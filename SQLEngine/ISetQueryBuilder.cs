namespace SQLEngine
{
    public interface ISetQueryBuilder : IAbstractQueryBuilder
    {
        ISetNoSetNoToQueryBuilder To(string value);
    }
}