namespace SQLEngine
{
    public interface IUpdateNoTableAndValuesQueryBuilder : IAbstractUpdateQueryBuilder
    {
        //IUpdateNoTableAndValuesAndWhereQueryBuilder Where(string condition);
        //IUpdateNoTableAndValuesAndWhereQueryBuilder Where(Func<AbstractConditionBuilder, AbstractConditionBuilder> builder);

        //IUpdateNoTableAndValuesAndWhereQueryBuilder Where(
        //    Func<BinaryConditionExpressionBuilder, BinaryConditionExpressionBuilder> builder);

        //IUpdateNoTableAndValuesAndWhereQueryBuilder Where(Func<ExistsConditionBuilder, ExistsConditionBuilder> builder);
    }
}