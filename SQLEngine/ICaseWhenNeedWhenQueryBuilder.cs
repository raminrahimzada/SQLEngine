namespace SQLEngine
{
    public interface ICaseWhenNeedWhenQueryBuilder : ICaseWhenQueryBuilder
    {
        ICaseWhenNeedThenQueryBuilder When(AbstractSqlCondition condition);
        ICaseWhenNeedThenQueryBuilder WhenColumnEquals(string columnName, AbstractSqlExpression expression);
        ICaseWhenNeedThenQueryBuilder WhenColumnEquals(string tableAlias,string columnName, AbstractSqlExpression expression);
        ICaseWhenNeedThenQueryBuilder WhenColumnEquals(string columnName, AbstractSqlLiteral literal);
        ICaseWhenNeedThenQueryBuilder WhenColumnEquals(string tableAlias, string columnName, AbstractSqlLiteral literal);
        ICaseWhenQueryBuilder Else(AbstractSqlLiteral literal);
        ICaseWhenQueryBuilder Else(AbstractSqlExpression expression);
    }
}