using System;

namespace SQLEngine
{
    public interface ISelectWithoutFromQueryBuilder : ISelectWithSelectorQueryBuilder, ISelectOrderBuilder
    {
        //IJoinedQueryBuilder InnerJoin(string alias, string tableName
        //    , string mainTableColumnName, string referenceTableColumnName);

        //IJoinedQueryBuilder RightJoin(string alias, string tableName, string mainTableColumnName,
        //    string referenceTableColumnName);

        //IJoinedQueryBuilder LeftJoin(string alias, string tableName,
        //    string mainTableColumnName, string referenceTableColumnName);
        ISelectOrderBuilder OrderBy(ISqlExpression expression);
        ISelectOrderBuilder OrderByDesc(ISqlExpression expression);
        
        ISelectWithoutFromAndGroupQueryBuilder GroupBy(ISqlExpression expression);
        ISelectWithoutFromAndGroupQueryBuilder GroupByDesc(ISqlExpression expression);
    }

    public interface ISelectWithoutFromAndGroupQueryBuilder : IAbstractQueryBuilder
    {
        ISelectWithoutFromAndGroupQueryBuilder GroupBy(ISqlExpression expression);
        ISelectWithoutFromAndGroupNeedHavingConditionQueryBuilder Having(AbstractSqlCondition condition);
    }

    public interface ISelectWithoutFromAndGroupNoNeedHavingConditionNeedOrderByQueryBuilder:IAbstractQueryBuilder
    {

    }
    public interface ISelectWithoutFromAndGroupNeedHavingConditionQueryBuilder : IAbstractQueryBuilder
    {
        ISelectWithoutFromAndGroupNoNeedHavingConditionNeedOrderByQueryBuilder OrderBy(Func<IAggregateFunctionBuilder, IAggregateFunctionBuilder> aggregate); 
        ISelectWithoutFromAndGroupNoNeedHavingConditionNeedOrderByQueryBuilder OrderByDesc(Func<IAggregateFunctionBuilder, IAggregateFunctionBuilder> aggregate); 
    }

    public interface IAggregateFunctionBuilder : IAbstractQueryBuilder
    {
        IAggregateFunctionBuilder Min(ISqlExpression expression);
        IAggregateFunctionBuilder Max(ISqlExpression expression);
        IAggregateFunctionBuilder Count(ISqlExpression expression);
        IAggregateFunctionBuilder Sum(ISqlExpression expression);
        IAggregateFunctionBuilder Avg(ISqlExpression expression);
        IAggregateFunctionBuilder Distinct();
        IAggregateFunctionBuilder All();
    }
}