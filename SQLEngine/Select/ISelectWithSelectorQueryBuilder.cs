using System;

namespace SQLEngine
{
    public interface ISelectWithSelectorQueryBuilder : IJoinedQueryBuilder
    {
        ISelectWithSelectorQueryBuilder SelectAssign(AbstractSqlVariable left, ISqlExpression right);
        ISelectWithSelectorQueryBuilder SelectAssign(AbstractSqlVariable left, AbstractSqlLiteral literal);
        ISelectWithSelectorQueryBuilder SelectAssign(AbstractSqlVariable left, AbstractSqlColumn column);


        //this will cause conflict with column Names->strings as literals/AbstractSqlLiteral so named like that
        ISelectWithSelectorQueryBuilder SelectLiteral(AbstractSqlLiteral literal);


        ISelectWithSelectorQueryBuilder Select(AbstractSqlColumn column);
        ISelectWithSelectorQueryBuilder Select(string columnName);
        
  

        ISelectWithSelectorQueryBuilder Select(Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallExpressionBuilder> customFunctionCallExpression);
        ISelectWithSelectorQueryBuilder Select(ISqlExpression expression);
        ISelectWithSelectorQueryBuilder Select(Func<ICaseWhenNeedWhenQueryBuilder, ICaseWhenQueryBuilder> caseWhen);
        ISelectWithSelectorQueryBuilder Select(Func<IAggregateFunctionBuilder, IAggregateFunctionBuilder> aggregate);
        ISelectWithSelectorQueryBuilder Select(Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallNopBuilder> aggregate);




        ISelectWithSelectorQueryBuilder SelectAs(Func<ICaseWhenNeedWhenQueryBuilder, ICaseWhenQueryBuilder> caseWhen, string alias);
        ISelectWithSelectorQueryBuilder SelectAs(Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallExpressionBuilder> customFunctionCallExpression,string alias);
        ISelectWithSelectorQueryBuilder SelectAs(ISqlExpression selector, string alias);

        ISelectWithoutFromQueryBuilder From(string tableName, string alias);
        ISelectWithoutFromQueryBuilder From(string tableName);
        ISelectWithoutFromQueryBuilder From<TTable>() where TTable : ITable,new();
        ISelectWithoutFromQueryBuilder From<TTable>(string alias) where TTable : ITable,new();

    }
}