using System;

namespace SQLEngine
{
    public interface ICreateProcedureWithArgumentQueryBuilder : IAbstractQueryBuilder
    {
        ICreateProcedureWithArgumentQueryBuilder Parameter(string argName, string argType);
        ICreateProcedureWithArgumentQueryBuilder Parameter<T>(string argName);
        ICreateProcedureWithArgumentQueryBuilder ParameterOut(string argName, string argType);
        ICreateProcedureWithArgumentQueryBuilder ParameterOut<T>(string argName);
        ICreateProcedureNeedBodyQueryBuilder Body(Action<IProcedureBodyQueryBuilder> body);
    }
}