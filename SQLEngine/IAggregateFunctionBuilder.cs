namespace SQLEngine
{
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