namespace SQLEngine
{
    public interface IUpdateNoTableSingleValueQueryBuilder : IAbstractUpdateQueryBuilder
    {
        IUpdateNoTableSingleValueQueryBuilder Value(string columnName, AbstractSqlLiteral columnValue);
        IUpdateNoTableSingleValueQueryBuilder Value(string columnName, AbstractSqlVariable variable);
        IUpdateNoTableSingleValueQueryBuilder Value(string columnName, ISqlExpression expression);

        IUpdateNoTableAndValuesAndWhereQueryBuilder Where(AbstractSqlCondition condition);
        
        IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnEquals(string columnName, ISqlExpression rightExpression);
        IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlVariable variable);
        IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlLiteral literal);
        IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnLike(string columnName, string right);

    }
}