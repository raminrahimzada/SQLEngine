namespace SQLEngine;

public interface ICreateFunctionNoNameQueryBuilder : IAbstractQueryBuilder
{
    ICreateFunctionNoNameQueryBuilder Parameter(string paramName, string paramType);
    ICreateFunctionNoNameQueryBuilder Parameter<T>(string paramName);
    ICreateFunctionNoNameAndParametersAndReturnTypeQueryBuilder Returns(string returnType);
    ICreateFunctionNoNameAndParametersAndReturnTypeQueryBuilder Returns<T>();
    ICreateFunctionNoNameQueryBuilder Schema(string schemaName);
}