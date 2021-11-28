using System;
using System.Linq.Expressions;

namespace SQLEngine
{
    public interface ISelectWhereQueryBuilder //: IAbstractSelectQueryBuilder
    {
        ISelectWithoutWhereQueryBuilder Where(AbstractSqlCondition condition);

        
        //ISelectWithoutWhereQueryBuilder WhereAnd(params AbstractSqlCondition[] conditions);
        //ISelectWithoutWhereQueryBuilder WhereOr(params AbstractSqlCondition[] conditions);


        ISelectWithoutWhereQueryBuilder WhereColumnEquals(string columnName, ISqlExpression right);
        ISelectWithoutWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlLiteral literal);
        ISelectWithoutWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlVariable variable);
        
        //ISelectWithoutWhereQueryBuilder WhereColumnEquals(AbstractSqlColumn column, ISqlExpression right);
        //ISelectWithoutWhereQueryBuilder WhereColumnEquals(AbstractSqlColumn column, AbstractSqlLiteral literal);
        //ISelectWithoutWhereQueryBuilder WhereColumnEquals(AbstractSqlColumn column, AbstractSqlVariable variable);
    }

    public interface ISelectWhereQueryBuilder<TTable> : ISelectWhereQueryBuilder
    {
        ISelectWithoutWhereQueryBuilder Where(Expression<Func<TTable,bool>> condition);
    }
}