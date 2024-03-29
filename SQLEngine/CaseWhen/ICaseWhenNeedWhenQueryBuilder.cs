﻿namespace SQLEngine;

public interface ICaseWhenNeedWhenQueryBuilder : ICaseWhenQueryBuilder
{
    ICaseWhenQueryBuilder Else(AbstractSqlLiteral literal);
    ICaseWhenQueryBuilder Else(ISqlExpression expression);
    ICaseWhenNeedThenQueryBuilder When(AbstractSqlCondition condition);
    ICaseWhenNeedThenQueryBuilder WhenColumnEquals(string columnName, ISqlExpression expression);
    ICaseWhenNeedThenQueryBuilder WhenColumnEquals(string tableAlias, string columnName, ISqlExpression expression);
    ICaseWhenNeedThenQueryBuilder WhenColumnEquals(string columnName, AbstractSqlLiteral literal);
    ICaseWhenNeedThenQueryBuilder WhenColumnEquals(string tableAlias, string columnName, AbstractSqlLiteral literal);
}