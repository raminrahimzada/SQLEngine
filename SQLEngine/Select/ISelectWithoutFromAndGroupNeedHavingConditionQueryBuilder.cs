using System;

namespace SQLEngine;

public interface ISelectWithoutFromAndGroupNeedHavingConditionQueryBuilder : IAbstractQueryBuilder
{
    ISelectWithoutFromAndGroupNoNeedHavingConditionNeedOrderByQueryBuilder OrderBy(Func<IAggregateFunctionBuilder, IAggregateFunctionBuilder> aggregate); 
    ISelectWithoutFromAndGroupNoNeedHavingConditionNeedOrderByQueryBuilder OrderByDesc(Func<IAggregateFunctionBuilder, IAggregateFunctionBuilder> aggregate);
}