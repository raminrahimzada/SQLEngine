using System;

namespace SQLEngine
{
    public interface ICreateProcedureWithArgumentQueryBuilder : IAbstractQueryBuilder
    {
        ICreateProcedureWithArgumentQueryBuilder Parameter(string argName, string argType);
        ICreateProcedureWithArgumentQueryBuilder ParameterOut(string argName, string argType);
        ICreateProcedureNeedBodyQueryBuilder Body(Action<IProcedureBodyQueryBuilder> body);
    }
}