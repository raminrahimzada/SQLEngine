using System;
using System.Linq.Expressions;

namespace SQLEngine;

public interface ISelectWithoutWhereQueryBuilder :
    IAbstractSelectQueryBuilder
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
}

public interface ISelectWithoutWhereQueryBuilder<TTable>
{
    ISelectOrderBuilder<TTable> OrderBy<TProperty>(Expression<Func<TTable, TProperty>> expression);
}