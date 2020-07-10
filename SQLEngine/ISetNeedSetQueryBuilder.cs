namespace SQLEngine
{
    public interface ISetNeedSetQueryBuilder : ISetQueryBuilder
    {
        ISetNeedToQueryBuilder Set(ISqlVariable variableName);
    }
}