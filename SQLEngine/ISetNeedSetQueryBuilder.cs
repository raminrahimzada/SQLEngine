namespace SQLEngine
{
    public interface ISetNeedSetQueryBuilder : ISetQueryBuilder
    {
        ISetNeedToQueryBuilder Set(string variableName);
    }
}