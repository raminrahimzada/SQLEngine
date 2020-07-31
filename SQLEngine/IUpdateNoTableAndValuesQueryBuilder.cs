namespace SQLEngine
{
    public interface IUpdateNoTableAndValuesQueryBuilder : IAbstractUpdateQueryBuilder
    {
        IUpdateNoTableAndValuesAndWhereQueryBuilder Where(AbstractSqlCondition condition);
        IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnEquals(string columnName,ISqlExpression expression);
        IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnEquals(string columnName,AbstractSqlLiteral expression);
    }
}