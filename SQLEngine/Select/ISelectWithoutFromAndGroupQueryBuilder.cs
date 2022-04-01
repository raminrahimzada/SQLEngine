namespace SQLEngine;

public interface ISelectWithoutFromAndGroupQueryBuilder : IAbstractQueryBuilder
{
    ISelectWithoutFromAndGroupQueryBuilder GroupBy(params ISqlExpression[] expressions);
    ISelectWithoutFromAndGroupQueryBuilder GroupBy(params AbstractSqlColumn[] expressions);
    ISelectWithoutFromAndGroupQueryBuilder GroupBy(params string[] columnNames);
    ISelectWithoutFromAndGroupNeedHavingConditionQueryBuilder Having(AbstractSqlCondition condition);
}