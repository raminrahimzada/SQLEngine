namespace SQLEngine
{
    public interface ISetQueryBuilder : IAbstractQueryBuilder
    {
        ISetNoSetNoToQueryBuilder To(ISqlExpression value);
        ISetNoSetNoToQueryBuilder To(AbstractSqlLiteral value);
    }
}