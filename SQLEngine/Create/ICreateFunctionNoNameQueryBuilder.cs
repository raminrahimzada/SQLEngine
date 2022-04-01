namespace SQLEngine;

public interface ICreateFunctionNoNameQueryBuilder : IAbstractQueryBuilder
{
    ICreateFunctionNoNameQueryBuilder Schema(string schemaName);
    ICreateFunctionNoNameQueryBuilder Parameter(string paramName, string paramType);
    ICreateFunctionNoNameQueryBuilder Parameter<T>(string paramName);
    ICreateFunctionNoNameAndParametersAndReturnTypeQueryBuilder Returns(string returnType);
    ICreateFunctionNoNameAndParametersAndReturnTypeQueryBuilder Returns<T>();
}