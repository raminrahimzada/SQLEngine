using System;

namespace SQLEngine
{
    public interface ISelectWithSelectorQueryBuilder : IJoinedQueryBuilder
    {
        ISelectWithSelectorQueryBuilder SelectAssign(AbstractSqlVariable left, AbstractSqlExpression right);
        ISelectWithSelectorQueryBuilder SelectAssign(AbstractSqlVariable left, AbstractSqlLiteral literal);
        ISelectWithSelectorQueryBuilder SelectAssign(AbstractSqlVariable left, AbstractSqlColumn column);

        ISelectWithSelectorQueryBuilder Select(AbstractSqlColumn column);
        //this will cause conflict with column Names->strings as literals/AbstractSqlLiteral
        //ISelectWithSelectorQueryBuilder Select(AbstractSqlLiteral literal);
        ISelectWithSelectorQueryBuilder Select(ISqlExpression expression);
        ISelectWithSelectorQueryBuilder Select(ISqlExpression selector, string alias);
        ISelectWithSelectorQueryBuilder Select(Func<ICaseWhenNeedWhenQueryBuilder, ICaseWhenQueryBuilder> caseWhen);
        ISelectWithSelectorQueryBuilder SelectAs(Func<ICaseWhenNeedWhenQueryBuilder, ICaseWhenQueryBuilder> caseWhen, string alias);

        ISelectWithSelectorQueryBuilder Select(Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallExpressionBuilder> customFuncExp);
        ISelectWithSelectorQueryBuilder SelectAs(Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallExpressionBuilder> func,string asName);

        ISelectWithoutFromQueryBuilder From(string tableName, string alias);
        ISelectWithoutFromQueryBuilder From(string tableName);
        //ISelectWithoutFromQueryBuilder FromSubQuery(string query, string alias);
        ISelectWithSelectorQueryBuilder Select(Func<IAggregateFunctionBuilder, IAggregateFunctionBuilder> aggregate);
        ISelectWithSelectorQueryBuilder Select(Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallNopBuilder> aggregate);
    }
}