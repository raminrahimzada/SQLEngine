using System;

namespace SQLEngine
{
    public interface ISelectWithSelectorQueryBuilder : IJoinedQueryBuilder
    {
        ISelectWithSelectorQueryBuilder SelectorAssign(string left,
            Func<IBinaryExpressionBuilder, IBinaryExpressionNopBuilder> right);

        ISelectWithSelectorQueryBuilder SelectorAssign(string left, string right);

        ISelectWithSelectorQueryBuilder Selector(string selector);

        ISelectWithSelectorQueryBuilder Selector(Func<ICaseWhenNeedWhenQueryBuilder, ICaseWhenNeedWhenQueryBuilder> selectorBuilder, string alias);
        ISelectWithSelectorQueryBuilder Selector(string selector, string alias);
        ISelectWithSelectorQueryBuilder Selector(Func<IConditionFilterQueryHelper, string> helperBuilder);
        ISelectWithSelectorQueryBuilder Selector(Func<IConditionFilterQueryHelper, string> helperBuilder, string alias);
        ISelectWithSelectorQueryBuilder SelectorCol(string tableAlias, string columnName, string alias = null);

        ISelectWithoutFromQueryBuilder From(string tableName, string alias);
        ISelectWithoutFromQueryBuilder From(string tableName);
        ISelectWithoutFromQueryBuilder FromSubQuery(string query, string alias);

    }
}