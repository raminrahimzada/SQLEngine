using System;

namespace SQLEngine
{
    public interface ISelectWithSelectorQueryBuilder : IJoinedQueryBuilder
    {
        ISelectWithSelectorQueryBuilder SelectAssign(string left,
            Func<IBinaryExpressionBuilder, IBinaryExpressionNopBuilder> right);

        ISelectWithSelectorQueryBuilder SelectAssign(string left, string right);

        ISelectWithSelectorQueryBuilder Select(AbstractSqlColumn column);
        ISelectWithSelectorQueryBuilder Select(ISqlExpression expression);

        //ISelectWithSelectorQueryBuilder Select(Func<ICaseWhenNeedWhenQueryBuilder, ICaseWhenNeedWhenQueryBuilder> selectorBuilder, string alias);
        ISelectWithSelectorQueryBuilder Select(ISqlExpression selector, string alias);
        //ISelectWithSelectorQueryBuilder Select(Func<IConditionFilterQueryHelper, string> helperBuilder);
        //ISelectWithSelectorQueryBuilder Select(Func<IConditionFilterQueryHelper, string> helperBuilder, string alias);
        //ISelectWithSelectorQueryBuilder SelectCol(string tableAlias, string columnName, string alias = null);

        ISelectWithoutFromQueryBuilder From(string tableName, string alias);
        ISelectWithoutFromQueryBuilder From(string tableName);
        ISelectWithoutFromQueryBuilder FromSubQuery(string query, string alias);

    }
}