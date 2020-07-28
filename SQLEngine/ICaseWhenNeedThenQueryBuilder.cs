namespace SQLEngine
{
    public interface ICaseWhenNeedThenQueryBuilder : ICaseWhenQueryBuilder
    {
        ICaseWhenNeedWhenQueryBuilder Then(AbstractSqlExpression @then);
        ICaseWhenNeedWhenQueryBuilder Then(AbstractSqlLiteral @then);
        ICaseWhenNeedWhenQueryBuilder ThenColumn(string columnName);
        ICaseWhenNeedWhenQueryBuilder ThenColumn(string tableAlias, string columnName);

    }
}