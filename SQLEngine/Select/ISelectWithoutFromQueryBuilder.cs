using System;
using System.Linq.Expressions;

namespace SQLEngine
{
    public interface ISelectWithoutFromQueryBuilder : ISelectOrderBuilder, IJoinedQueryBuilder
    {
        ISelectWithoutFromQueryBuilder Schema(string schema);

        ISelectOrderBuilder OrderBy(ISqlExpression expression);
        ISelectOrderBuilder OrderByDesc(ISqlExpression expression);
        
        ISelectOrderBuilder OrderBy(AbstractSqlColumn column);
        ISelectOrderBuilder OrderByDesc(AbstractSqlColumn column);
        ISelectOrderBuilder OrderByDesc(string columnName);
        ISelectOrderBuilder OrderBy(string columnName);
        
        ISelectWithoutFromAndGroupQueryBuilder GroupBy(params ISqlExpression[] expressions);
        ISelectWithoutFromAndGroupQueryBuilder GroupBy(params AbstractSqlColumn[] columns);
        ISelectWithoutFromAndGroupQueryBuilder GroupBy(params string[] columnNames);
    }

    public interface ISelectWithoutFromQueryBuilder<TTable> : ISelectWithoutFromQueryBuilder, ISelectOrderBuilder, IJoinedQueryBuilder
    {
        ISelectWithoutWhereQueryBuilder Where(Expression<Func<TTable, bool>> expression);
    }
}