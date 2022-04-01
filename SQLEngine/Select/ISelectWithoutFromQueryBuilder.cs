using System;
using System.Linq.Expressions;

namespace SQLEngine;

public interface ISelectWithoutFromQueryBuilder : ISelectOrderBuilder, IJoinedQueryBuilder
{
    ISelectWithoutFromAndGroupQueryBuilder GroupBy(params ISqlExpression[] expressions);
    ISelectWithoutFromAndGroupQueryBuilder GroupBy(params AbstractSqlColumn[] columns);
    ISelectWithoutFromAndGroupQueryBuilder GroupBy(params string[] columnNames);

    ISelectOrderBuilder OrderBy(ISqlExpression expression);

    ISelectOrderBuilder OrderBy(AbstractSqlColumn column);
    ISelectOrderBuilder OrderBy(string columnName);
    ISelectOrderBuilder OrderByDesc(ISqlExpression expression);
    ISelectOrderBuilder OrderByDesc(AbstractSqlColumn column);
    ISelectOrderBuilder OrderByDesc(string columnName);
    ISelectWithoutFromQueryBuilder Schema(string schema);
}

public interface ISelectWithoutFromQueryBuilder<TTable> : ISelectWithoutFromQueryBuilder, ISelectOrderBuilder,
    IJoinedQueryBuilder
{
    ISelectWithoutWhereQueryBuilder Where(Expression<Func<TTable, bool>> expression);
}