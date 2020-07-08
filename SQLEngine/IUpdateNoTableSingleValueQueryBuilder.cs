using System;

namespace SQLEngine
{
    public interface IUpdateNoTableSingleValueQueryBuilder : IAbstractUpdateQueryBuilder
    {
        IUpdateNoTableSingleValueQueryBuilder Value(string columnName,
            Func<IBinaryExpressionBuilder, IBinaryExpressionNopBuilder> builder);

        IUpdateNoTableSingleValueQueryBuilder Value(string columnName, string columnValue);
        IUpdateNoTableAndValuesAndWhereQueryBuilder Where(string condition);
        //IUpdateNoTableAndValuesAndWhereQueryBuilder Where(Func<AbstractConditionBuilder, AbstractConditionBuilder> builder);
        IUpdateNoTableAndValuesAndWhereQueryBuilder WhereEquals(string left, string right);

        //IUpdateNoTableAndValuesAndWhereQueryBuilder Where(
        //    Func<BinaryConditionExpressionBuilder, BinaryConditionExpressionBuilder> builder);

        //IUpdateNoTableAndValuesAndWhereQueryBuilder Where(Func<ExistsConditionBuilder, ExistsConditionBuilder> builder);
    }
}