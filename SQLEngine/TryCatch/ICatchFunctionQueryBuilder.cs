namespace SQLEngine;

public interface ICatchFunctionQueryBuilder : IQueryBuilder
{
    ISqlExpression ErrorNumber();
    ISqlExpression ErrorMessage();
}