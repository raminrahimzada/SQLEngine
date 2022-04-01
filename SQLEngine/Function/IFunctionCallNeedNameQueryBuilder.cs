namespace SQLEngine;

public interface IFunctionCallNeedNameQueryBuilder : IFunctionCallQueryBuilder
{
    IFunctionCallNeedArgQueryBuilder Call(string functionName);
}