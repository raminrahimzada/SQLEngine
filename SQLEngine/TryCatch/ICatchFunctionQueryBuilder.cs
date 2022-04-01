namespace SQLEngine;

public interface ICatchFunctionQueryBuilder : IQueryBuilder
{
    ISqlExpression ErrorMessage();
    ISqlExpression ErrorNumber();
}