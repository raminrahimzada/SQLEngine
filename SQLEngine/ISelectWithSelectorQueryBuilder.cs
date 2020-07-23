using System;

namespace SQLEngine
{
    public interface ISelectWithSelectorQueryBuilder : IJoinedQueryBuilder
    {
        ISelectWithSelectorQueryBuilder SelectAssign(AbstractSqlVariable left, ISqlExpression right);

        ISelectWithSelectorQueryBuilder Select(AbstractSqlColumn column);
        ISelectWithSelectorQueryBuilder Select(ISqlExpression expression);
        ISelectWithSelectorQueryBuilder Select(ISqlExpression selector, string alias);

        ISelectWithoutFromQueryBuilder From(string tableName, string alias);
        ISelectWithoutFromQueryBuilder From(string tableName);
        //ISelectWithoutFromQueryBuilder FromSubQuery(string query, string alias);
        ISelectWithSelectorQueryBuilder Select(Func<IAggregateFunctionBuilder, IAggregateFunctionBuilder> aggregate);
        ISelectWithSelectorQueryBuilder Select(Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallNopBuilder> aggregate);
    }
}