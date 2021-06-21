namespace SQLEngine
{
    public interface ISetNeedSetQueryBuilder : ISetQueryBuilder
    {
        ISetNeedToQueryBuilder Set(AbstractSqlVariable variable);
    }
}