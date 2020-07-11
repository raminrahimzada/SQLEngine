using System;

namespace SQLEngine
{
    public interface IUpdateNoTableSingleValueQueryBuilder : IAbstractUpdateQueryBuilder
    {
        IUpdateNoTableSingleValueQueryBuilder Value(string columnName,
            Func<IBinaryExpressionBuilder, IBinaryExpressionNopBuilder> builder);

        IUpdateNoTableSingleValueQueryBuilder Value(string columnName, AbstractSqlLiteral columnValue);

        IUpdateNoTableAndValuesAndWhereQueryBuilder Where(string condition);
        IUpdateNoTableAndValuesAndWhereQueryBuilder Where(AbstractSqlCondition condition);
        //IUpdateNoTableAndValuesAndWhereQueryBuilder Where(Func<AbstractConditionBuilder, AbstractConditionBuilder> builder);
        IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnEquals(string columnName, ISqlExpression right);

        //IUpdateNoTableAndValuesAndWhereQueryBuilder Where(
        //    Func<BinaryConditionExpressionBuilder, BinaryConditionExpressionBuilder> builder);

        //IUpdateNoTableAndValuesAndWhereQueryBuilder Where(Func<ExistsConditionBuilder, ExistsConditionBuilder> builder);
    }
}