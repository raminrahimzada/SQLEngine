namespace SQLEngine
{
    public interface ICaseWhenNeedThenQueryBuilder : ICaseWhenQueryBuilder
    {
        ICaseWhenNeedWhenQueryBuilder Then(ISqlExpression @then);
        ICaseWhenNeedWhenQueryBuilder Then(AbstractSqlColumn column);

        ICaseWhenNeedWhenQueryBuilder Then(AbstractSqlLiteral then);
        
        ICaseWhenNeedWhenQueryBuilder ThenColumn(string columnName);
        ICaseWhenNeedWhenQueryBuilder ThenColumn(string tableAlias, string columnName);

    }
}