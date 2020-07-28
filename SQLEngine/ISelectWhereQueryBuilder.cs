namespace SQLEngine
{
    public interface ISelectWhereQueryBuilder : IAbstractSelectQueryBuilder
    {
        //ISelectWithoutWhereQueryBuilder Where(string condition);
        ISelectWithoutWhereQueryBuilder Where(AbstractSqlCondition condition);

        ISelectWithoutWhereQueryBuilder WhereAnd(
            params AbstractSqlCondition[] conditions);
        ISelectWithoutWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlExpression right);
        ISelectWithoutWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlLiteral literal);
        ISelectWithoutWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlVariable variable);
    }
}