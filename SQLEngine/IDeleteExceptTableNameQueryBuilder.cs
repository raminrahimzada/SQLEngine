namespace SQLEngine
{
    public interface IDeleteExceptTableNameQueryBuilder : IDeleteQueryBuilder
    {
        IDeleteExceptWhereQueryBuilder Where(AbstractSqlCondition condition);
        IDeleteExceptWhereQueryBuilder WhereColumnEquals(string columnName,AbstractSqlExpression expression);
        IDeleteExceptWhereQueryBuilder WhereColumnEquals(string columnName,AbstractSqlLiteral literal);
        IDeleteExceptWhereQueryBuilder WhereColumnEquals(string columnName,AbstractSqlVariable variable);

    }
}