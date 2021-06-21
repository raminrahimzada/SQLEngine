namespace SQLEngine
{
    public interface IUpdateNoTableSingleValueQueryBuilder : IAbstractUpdateQueryBuilder
    {
        IUpdateNoTableSingleValueQueryBuilder Value(string columnName, AbstractSqlLiteral columnValue);
        IUpdateNoTableSingleValueQueryBuilder Value(string columnName, AbstractSqlVariable variable);
        IUpdateNoTableSingleValueQueryBuilder Value(string columnName, ISqlExpression expression);

        IUpdateNoTableAndValuesAndWhereQueryBuilder Where(AbstractSqlCondition condition);
        
        IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnEquals(string columnName, ISqlExpression right);
        IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlVariable variable);
        IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlLiteral literal);
        IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnLike(string columnName, string right);

        //IUpdateNoTableAndValuesAndWhereQueryBuilder Where(
        //    Func<BinaryConditionExpressionBuilder, BinaryConditionExpressionBuilder> builder);

        //IUpdateNoTableAndValuesAndWhereQueryBuilder Where(Func<ExistsConditionBuilder, ExistsConditionBuilder> builder);
    }
}