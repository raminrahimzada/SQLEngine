namespace SQLEngine
{
    public interface ISelectWithoutFromAndGroupQueryBuilder : IAbstractQueryBuilder
    {
        ISelectWithoutFromAndGroupQueryBuilder GroupBy(ISqlExpression expression);
        ISelectWithoutFromAndGroupQueryBuilder GroupBy(AbstractSqlColumn expression);
        ISelectWithoutFromAndGroupNeedHavingConditionQueryBuilder Having(AbstractSqlCondition condition);
    }
}