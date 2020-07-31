using System;

namespace SQLEngine
{
    public interface ISelectWithSelectorQueryBuilder : IJoinedQueryBuilder
    {
        ISelectWithSelectorQueryBuilder SelectAssign(AbstractSqlVariable left, ISqlExpression right);
        ISelectWithSelectorQueryBuilder SelectAssign(AbstractSqlVariable left, AbstractSqlLiteral literal);
        ISelectWithSelectorQueryBuilder SelectAssign(AbstractSqlVariable left, AbstractSqlColumn column);



        ISelectWithSelectorQueryBuilder Select(AbstractSqlColumn column);
        //this will cause conflict with column Names->strings as literals/AbstractSqlLiteral
        //ISelectWithSelectorQueryBuilder Select(AbstractSqlLiteral literal);
        ISelectWithSelectorQueryBuilder Select(Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallExpressionBuilder> customFuncExp);
        ISelectWithSelectorQueryBuilder Select(ISqlExpression expression);
        ISelectWithSelectorQueryBuilder Select(ISqlExpression selector, string alias);
        ISelectWithSelectorQueryBuilder Select(Func<ICaseWhenNeedWhenQueryBuilder, ICaseWhenQueryBuilder> caseWhen);
        ISelectWithSelectorQueryBuilder Select(Func<IAggregateFunctionBuilder, IAggregateFunctionBuilder> aggregate);
        ISelectWithSelectorQueryBuilder Select(Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallNopBuilder> aggregate);




        ISelectWithSelectorQueryBuilder SelectAs(Func<ICaseWhenNeedWhenQueryBuilder, ICaseWhenQueryBuilder> caseWhen, string alias);
        ISelectWithSelectorQueryBuilder SelectAs(Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallExpressionBuilder> func,string alias);

        ISelectWithoutFromQueryBuilder From(string tableName, string alias);
        ISelectWithoutFromQueryBuilder From(string tableName);
        ISelectWithoutFromQueryBuilder From<TTable>() where TTable : ITable,new();
        ISelectWithoutFromQueryBuilder From<TTable>(string alias) where TTable : ITable,new();

    }
}