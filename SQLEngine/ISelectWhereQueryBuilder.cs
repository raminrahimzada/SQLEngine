namespace SQLEngine
{
    public interface ISelectWhereQueryBuilder : IAbstractSelectQueryBuilder
    {
        ISelectWithoutWhereQueryBuilder Where(string condition);

        ISelectWithoutWhereQueryBuilder WhereAnd(
            params string[] filters);
        //ISelectWithoutWhereQueryBuilder WhereAnd(
        //    params Func<BinaryConditionExpressionBuilder, BinaryConditionExpressionBuilder>[] builders);

        //ISelectWithoutWhereQueryBuilder Where(Func<ConditionBuilder, ConditionBuilder> builder);
        ISelectWithoutWhereQueryBuilder WhereEquals(string left, string right);
        ISelectWithoutWhereQueryBuilder WhereIDIs(long id);
        ISelectWithoutWhereQueryBuilder WhereIDIs(string sqlVariable);


        //ISelectWithoutWhereQueryBuilder Where(
        //    Func<BinaryConditionExpressionBuilder, BinaryConditionExpressionBuilder> builder);

        //ISelectWithoutWhereQueryBuilder Where(
        //    Func<IBinaryConditionExpressionBuilder, IBinaryConditionExpressionBuilder> builder);

        //ISelectWithoutWhereQueryBuilder Where(Func<AbstractConditionBuilder, AbstractConditionBuilder> builder);
    }
}