﻿namespace SQLEngine
{
    public interface IUpdateNoTableSingleValueQueryBuilder : IAbstractUpdateQueryBuilder
    {
        //IUpdateNoTableSingleValueQueryBuilder Value(string columnName,
        //    Func<IBinaryExpressionBuilder, IBinaryExpressionNopBuilder> builder);

        IUpdateNoTableSingleValueQueryBuilder Value(string columnName, AbstractSqlLiteral columnValue);
        IUpdateNoTableSingleValueQueryBuilder Value(string columnName, AbstractSqlVariable variable);

        //IUpdateNoTableAndValuesAndWhereQueryBuilder Where(string condition);
        IUpdateNoTableAndValuesAndWhereQueryBuilder Where(AbstractSqlCondition condition);
        //IUpdateNoTableAndValuesAndWhereQueryBuilder Where(Func<AbstractConditionBuilder, AbstractConditionBuilder> builder);
        IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnEquals(string columnName, ISqlExpression right);
        IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlVariable variable);
        IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlLiteral literal);
        IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnLike(string columnName, ISqlExpression right);

        //IUpdateNoTableAndValuesAndWhereQueryBuilder Where(
        //    Func<BinaryConditionExpressionBuilder, BinaryConditionExpressionBuilder> builder);

        //IUpdateNoTableAndValuesAndWhereQueryBuilder Where(Func<ExistsConditionBuilder, ExistsConditionBuilder> builder);
    }
}