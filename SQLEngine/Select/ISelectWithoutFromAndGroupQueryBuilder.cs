namespace SQLEngine
{
    public interface ISelectWithoutFromAndGroupQueryBuilder : IAbstractQueryBuilder
    {
        ISelectWithoutFromAndGroupQueryBuilder GroupBy(ISqlExpression expression);
        ISelectWithoutFromAndGroupQueryBuilder GroupBy(AbstractSqlColumn expression);
        ISelectWithoutFromAndGroupQueryBuilder GroupBy(string columnName);
        ISelectWithoutFromAndGroupNeedHavingConditionQueryBuilder Having(AbstractSqlCondition condition);
    }
}