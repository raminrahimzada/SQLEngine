﻿namespace SQLEngine;

public interface ICreateFunctionNoNameAndParametersQueryBuilder : IAbstractQueryBuilder
{
    ICreateFunctionNoNameAndParametersAndReturnTypeQueryBuilder Returns(string returnType);
    ICreateFunctionNoNameAndParametersAndReturnTypeQueryBuilder Returns<T>();
}