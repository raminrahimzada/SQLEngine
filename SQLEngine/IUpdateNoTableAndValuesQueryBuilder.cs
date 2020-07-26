namespace SQLEngine
{
    public interface IUpdateNoTableAndValuesQueryBuilder : IAbstractUpdateQueryBuilder
    {
        IUpdateNoTableAndValuesAndWhereQueryBuilder Where(AbstractSqlCondition condition);
        //IUpdateNoTableAndValuesAndWhereQueryBuilder Where(Func<AbstractConditionBuilder, AbstractConditionBuilder> builder);

        //IUpdateNoTableAndValuesAndWhereQueryBuilder Where(
        //    Func<BinaryConditionExpressionBuilder, BinaryConditionExpressionBuilder> builder);

        //IUpdateNoTableAndValuesAndWhereQueryBuilder Where(Func<ExistsConditionBuilder, ExistsConditionBuilder> builder);
    }
}