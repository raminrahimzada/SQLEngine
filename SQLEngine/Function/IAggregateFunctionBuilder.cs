namespace SQLEngine
{
    public interface IAggregateFunctionBuilder : IAbstractQueryBuilder
    {
        IAggregateFunctionBuilder Min(ISqlExpression expression);
        IAggregateFunctionBuilder Max(ISqlExpression expression);
        IAggregateFunctionBuilder Count(ISqlExpression expression);
        IAggregateFunctionBuilder Sum(ISqlExpression expression);
        IAggregateFunctionBuilder Avg(ISqlExpression expression);
        
        
        IAggregateFunctionBuilder Min(AbstractSqlColumn column);
        IAggregateFunctionBuilder Max(AbstractSqlColumn column);
        IAggregateFunctionBuilder Count(AbstractSqlColumn column);
        IAggregateFunctionBuilder Sum(AbstractSqlColumn column);
        IAggregateFunctionBuilder Avg(AbstractSqlColumn column);
        
        IAggregateFunctionBuilder Min(string columnName);
        IAggregateFunctionBuilder Max(string columnName);
        IAggregateFunctionBuilder Count(string columnName);
        IAggregateFunctionBuilder Sum(string columnName);
        IAggregateFunctionBuilder Avg(string columnName);


        IAggregateFunctionBuilder Distinct();
        IAggregateFunctionBuilder All();
    }
}