using System;

namespace SQLEngine;

public interface ICreateProcedureWithArgumentQueryBuilder : IAbstractQueryBuilder
{
    ICreateProcedureNeedBodyQueryBuilder Body(Action<IProcedureBodyQueryBuilder> body);
    ICreateProcedureWithArgumentQueryBuilder Parameter(string argName, string argType);
    ICreateProcedureWithArgumentQueryBuilder Parameter<T>(string argName);
    ICreateProcedureWithArgumentQueryBuilder ParameterOut(string argName, string argType);
    ICreateProcedureWithArgumentQueryBuilder ParameterOut<T>(string argName);
}