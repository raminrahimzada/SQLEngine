using System;
using System.Linq.Expressions;

namespace SQLEngine;

public interface ISelectWhereQueryBuilder //: IAbstractSelectQueryBuilder
{
    ISelectWithoutWhereQueryBuilder Where(AbstractSqlCondition condition);
    ISelectWithoutWhereQueryBuilder WhereColumnEquals(string columnName, ISqlExpression right);
    ISelectWithoutWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlLiteral literal);
    ISelectWithoutWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlVariable variable);
}

public interface ISelectWhereQueryBuilder<TTable> : ISelectWhereQueryBuilder
{
    ISelectWithoutWhereQueryBuilder Where(Expression<Func<TTable, bool>> condition);
}