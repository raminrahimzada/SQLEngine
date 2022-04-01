namespace SQLEngine;

public interface ICustomFunctionCallExpressionBuilder : IAbstractQueryBuilder
{
    ICustomFunctionCallNopBuilder Call(string functionName, params ISqlExpression[] parameters);
}