namespace SQLEngine;

public interface IUpdateNoTableAndValuesQueryBuilder : IAbstractUpdateQueryBuilder
{
    IUpdateNoTableAndValuesAndWhereQueryBuilder Where(AbstractSqlCondition condition);
    IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnEquals(string columnName, ISqlExpression rightExpression);
    IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlLiteral expression);
}