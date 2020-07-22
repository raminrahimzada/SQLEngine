namespace SQLEngine
{
    public interface ISelectWithoutFromAndGroupQueryBuilder : IAbstractQueryBuilder
    {
        ISelectWithoutFromAndGroupQueryBuilder GroupBy(ISqlExpression expression);
        ISelectWithoutFromAndGroupNeedHavingConditionQueryBuilder Having(AbstractSqlCondition condition);
    }
}